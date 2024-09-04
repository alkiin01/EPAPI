using EPAPI.Repository;
using EPAPI.Repository.Purchase;
using EpicorRestAPI;
using EpicorRestSharedClasses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace EPAPI.Controllers.Purchase
{
    public class PurchaseOrderController : Controller
    {
        public EpicRest epicRest = new EpicRest();
        PurchaseOrder shipmentRepository = new PurchaseOrder();
        SetApprove setApprove = new SetApprove();
        User user = new User();
        [Route("PO/ApprovePO")]
        [HttpPost]
        public dynamic ApprovePO([FromBody] PurchaseOrder purchaseOrder)
        {
            try
            {
                user.nik = purchaseOrder.nik;
                user.password = purchaseOrder.password;
                dynamic result;
                List<POApvMsg> listPO = new List<POApvMsg>();
                List<POApvMsg> listPO2 = new List<POApvMsg>();

                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    #region GetPO
                    listPO = GetPO(purchaseOrder.PONum.ToString());
                    listPO2 = GetPO(purchaseOrder.PONum.ToString());
                    #endregion
                    var SetPO2 = listPO2.Last();
                     SetPO2.RowMod = "";

                    var SetPO = listPO.Last();
                    SetPO.ApproverResponse = purchaseOrder.ApproverResponse;
                    SetPO.MsgFrom = purchaseOrder.MsgFrom;
                    SetPO.MsgTo = purchaseOrder.MsgTo;
                    SetPO.MsgText = purchaseOrder.MsgText;
                    SetPO.MsgToName = purchaseOrder.MsgToName;
                    SetPO.ApvAmt = purchaseOrder.ApvAmt;

                    SetPO.RowMod = "U";

                    #region ApprovePO
                    var poData = new
                    {
                        ds = new
                        {
                            POApvMsg = new[] { SetPO2, SetPO }
                        },
                    };
                    string preview = "";
                    var boOrderInfo = EpicorRest.BoPost("Erp.BO.POApvMsgSvc", "Update", poData);
                    if (boOrderInfo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            desc = boOrderInfo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        preview = boOrderInfo.ResponseBody.ToString();
                    }
                    POResponse newResponse = JsonConvert.DeserializeObject<POResponse>(preview);
                    //listPO = newResponse.Parameters.Ds.POHeader.ToList();
                    #endregion
                    result = new
                    {
                        code = 200,
                        desc = "Ok"
                    };
                    return result;
                }
                else
                {
                    result = new
                    {
                        code = 401,
                        desc = "Not Authorized or Server Full"
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                dynamic result;
                result = new
                {
                    code = 401,
                    desc = ex.Message.ToString()
                };
                return result;
            }
        }

        public dynamic GetPO(string PoNum)
        {
            List<POApvMsg> list = new List<POApvMsg>();
            try
            {
                bool test = epicRest.PortalBeearer(user);
                dynamic result;
                if (test)
                {
                    string rtn = "";
                    MultiMap<string, string> dic = new MultiMap<string, string>();
                    dic.Add("poNum", PoNum); //Value harus string
                    var boGet = EpicorRest.BoGet("Erp.BO.POApvMsgSvc", "GetByID", dic);
                    if (boGet.IsErrorResponse)
                    {
                        result = new
                        {
                            code = 9999,
                            desc = boGet.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        rtn = boGet.ResponseBody.ToString();
                        JObject jsonObject = JObject.Parse(rtn);
                        JArray jsonArray = (JArray)jsonObject["returnObj"]["POApvMsg"];
                        rtn = jsonArray == null ? "" : jsonArray.ToString();
                        list = JsonConvert.DeserializeObject<List<POApvMsg>>(rtn);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {

                dynamic result;
                result = new
                {
                    code = 400,
                    desc = ex.Message.ToString() + "From Get PO"
                };
                return result;
            }

        }
        [Route("PO/UpdateApprove")]
        [HttpPost]
        public dynamic SetApprover([FromBody] SetApprove setApprove)
        {
            dynamic result;

            try
            {
            EpicorRestSession ses = new EpicorRestSession();

                user.nik = setApprove.nik;
                user.password = setApprove.password;

                bool test = epicRest.PortalBeearer(user);
                List<UD39> list = new List<UD39>();
                if (test)
                {
                    string Key3 = setApprove.PONum.ToString();
                    var ShortChar03 = setApprove.NikCheck;
                    var ShortChar04 = setApprove.ActCheck;
                    var ShortChar05 = setApprove.NikApprove;
                    var ShortChar06 = setApprove.ActApprove;
                    var ShortChar07 = setApprove.NikLegalize;
                    var ShortChar08 = setApprove.ActLegalize;

                    MultiMap<string, string> dic = new MultiMap<string, string>();
                    dic.Add("whereClauseUD39", "Key3="+Key3);
                    dic.Add("pageSize", "10"); //Value harus string
                    dic.Add("absolutePage", "1"); //Value harus string
                    dic.Add("whereClauseUD39Attch", ""); //Value harus string


                    #region GetRow UD39
                    dynamic row;
                    string rtn = "";
                    var bo = EpicorRest.BoGet("Ice.BO.UD39Svc", "GetRows", dic);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = bo.ResponseStatus,
                            desc = bo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        rtn = bo.ResponseBody.ToString();
                        JObject jsonObject = JObject.Parse(rtn);
                        JArray jsonArray = (JArray)jsonObject["returnObj"]["UserFile"];
                        rtn = jsonArray == null ? "" : jsonArray.ToString();
                        list = JsonConvert.DeserializeObject<List<UD39>>(rtn);
                        var res = list.FirstOrDefault();
                        if (res != null)
                        {
                            setApprove.RowMod = "U";
                        }
                        else
                        {
                            setApprove.RowMod = "A";
                        }
                    }
                    #endregion  

                    var dset = new
                    {
                        ds = new
                        {
                            UD39 = new[]
                            {
                                setApprove
                            }
                        }
                    };

                    var boPost = EpicorRest.BoPost("Ice.BO.UD39Svc", "Update", dset);
                    if (boPost.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 401,
                            desc = boPost.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        result = new
                        {
                            code = 200,
                            desc = "ok"
                        };
                        return result;
                    }

                }
                else
                {
                    result = new
                    {
                        code = 401,
                        desc = "Not Authorized or Server Full"
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [Route("Attachment")]
        [HttpPost]
        public dynamic ShowAttach([FromBody] PurchaseOrder purchaseOrder)
        {
            byte[] attach = [];
            user.nik = purchaseOrder.nik;
            user.password = purchaseOrder.password;

            bool test = epicRest.PortalBeearer(user);
            if (test)
            {

                var SeqNum = purchaseOrder.SeqNum;
                string fileDesc = "";
                string fileExt = "";
                var pData = new
                {
                    xFileRefNum = SeqNum,
                    metadata = new
                    {

                    }
                };
                MultiMap<string, string> dic = new MultiMap<string, string>();
                dic.Add("$select ", "XFileRefNum,XFileRefDocTypeID,XFileRefXFileName,XFileRefXFileDesc"); //Value harus string
                dic.Add("$filter ", "XFileRefNum eq " + SeqNum); //Value harus string
                var getFile = EpicorRest.BoGet("Ice.BO.AttachmentSvc", "Attachments", dic);
                bool fileResult = false;
                string fileRes;
                if (getFile.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    fileResult = false;
                }
                else
                {
                    fileRes = getFile.ResponseBody.ToString();
                    var jsonObj = JsonConvert.DeserializeObject<dynamic>(fileRes);
                    var showData = jsonObj.value[0];
                    var fileName = showData.XFileRefXFileName.ToString();
                    fileDesc = Path.GetFileName(fileName);
                }
                string rtn;
                string sw = "";
                var bo = EpicorRest.BoPost("Ice.BO.AttachmentSvc", "DownloadFile", pData);
                bool result = false;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = false;
                }
                else
                {
                    rtn = bo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    var show = jsonObject["returnObj"];
                    if (show != null)
                    {
                        attach = Convert.FromBase64String(show.ToString());
                        sw = show.ToString();
                    }
                    else
                    {
                        attach = [];
                        sw = "";
                    }

                }
                return Json(new
                {
                    draw = sw,
                    fileDesc = fileDesc,
                    code = 200,
                    desc="Ok"

                });
            }
            else
            {
                dynamic result = new
                {
                    code = 401,
                    desc = "Not Authorized or Server Full"
                };
                return result;
            }
        }


    } 
}
