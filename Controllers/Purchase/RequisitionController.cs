using EPAPI.Repository;
using EPAPI.Repository.Purchase;
using EpicorRestAPI;
using EpicorRestSharedClasses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;

namespace EPAPI.Controllers.Purchase
{
    public class RequisitionController : Controller
    {
        public EpicRest epicRest = new EpicRest();
        PurchaseOrder shipmentRepository = new PurchaseOrder();
        SetApprove setApprove = new SetApprove();
        User user = new User();

        [Route("PR/ApprovePR")]
        [HttpPost]
        public dynamic ApprovePR([FromBody] ReqPayLoad payload)
        {
            try
			{
                user.nik = payload.nik;
                user.password = payload.password;
                dynamic result;
                List<ReqHead> listData = new List<ReqHead>();
                List<ReqHead> listData2 = new List<ReqHead>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    #region GetData
                    listData = GetData(payload.ReqNum.ToString());
                    listData2 = GetData(payload.ReqNum.ToString());
                    #endregion
                    var reqNum = payload.ReqNum.ToString();
                    string rtn = "";
                    MultiMap<string, string> dic = new MultiMap<string, string>();
                    dic.Add("reqNum", reqNum); //Value harus string

                    var SetData2 = listData2.Last();
                    SetData2.RowMod = "";
                    
                    var SetData = listData.Last();
                    var ReplyOption = payload.ReplyOption;
                   
                    SetData.RowMod = "U";
                    SetData.ReplyOption = payload.ReplyOption;
                    SetData.ReqActionID = payload.ReqActionID;
                    SetData.ReqActionIDReqActionDesc = payload.ReqActionIDReqActionDesc;
                    SetData.CurrDispatcherID = payload.CurrDispatcherID;
                    SetData.CurrDispatcherName = payload.CurrDispatcherName;

                    SetData.NextNote = payload.ApproveMsg;
                    SetData.NextActionID = ReplyOption == "R" ? SetData.CreatedBy : payload.NextActionID;
                    SetData.NextDispatcherID = ReplyOption == "R" ? " " : payload.NextDispatcherID;
                    SetData.NextActionDesc = ReplyOption == "R" ? "Reject Requisition" : "";
                    SetData.ReqUserID = user.nik;

                    #region Approve PR
                    var poData = new
                    {
                        ds = new
                        {
                            ReqHead = new[] { SetData2, SetData }
                        },
                    };
                    string preview = "";
                    var boOrderInfo = EpicorRest.BoPost("Erp.BO.ReqSvc", "Update", poData);
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
                    PRResponse newResponse = JsonConvert.DeserializeObject<PRResponse>(preview);
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
            catch (Exception Ex)
			{

				throw;
			}
        }

        public dynamic GetData(string reqNum)
        {
            List<ReqHead> list = new List<ReqHead>();
            try
            {
                bool test = epicRest.PortalBeearer(user);
                dynamic result;
                if (test)
                {
                    string rtn = "";
                    MultiMap<string, string> dic = new MultiMap<string, string>();
                    dic.Add("reqNum", reqNum); //Value harus string
                    var boGet = EpicorRest.BoGet("Erp.BO.ReqSvc", "GetByID", dic);
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
                        JArray jsonArray = (JArray)jsonObject["returnObj"]["ReqHead"];
                        rtn = jsonArray == null ? "" : jsonArray.ToString();
                        list = JsonConvert.DeserializeObject<List<ReqHead>>(rtn);
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
                    desc = ex.Message.ToString() + "FromGetData"
                };
                return result;
            }

        }
    }
}
