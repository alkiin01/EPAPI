using EPAPI.Repository;
using EPAPI.Repository.Purchase;
using EPAPI.Repository.ReceiptEntry;
using EpicorRestAPI;
using EpicorRestSharedClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
namespace EPAPI.Controllers.ReceiptEntry
{

    public class ReceiptEntryController : Controller
    {
        public EpicRest epicRest = new EpicRest();
        User user = new User();

        [Route("Receipt/GetNew")]
        [HttpPost]
        public dynamic GetNew([FromBody] RcvParam rcv)
        {
            user.nik = rcv.nik;
            user.password = rcv.password;
            bool test = epicRest.PortalBeearer(user);
            List<RcvHead> list = new List<RcvHead>();
            if (test)
            {
                var PackSlip = rcv.PackSlip;
                string rtn = "";
                try
                {
                    dynamic result;
                    list = GetnewList();
                    var validatePO = ValidatePONum(rcv.poNum);
                    if (validatePO.epi_code != 200)
                    {
                        result = new
                        {
                            code = 200,
                            status = "Ok",
                            data = validatePO
                        };
                        return result;
                    }
                    else
                    {
                        string preview;
                        var rcvHead = list.Last();
                        rcvHead.RowMod = "A";
                        rcvHead.ArrivedDate = rcv.ArrivalDate;
                        rcvHead.ReceiptComment = rcv.receiptComment;

                        var pData = new
                        {
                            ds = new
                            {
                                RcvHead = new[] { rcvHead },
                            },
                            fromReceiptEntryNewRcpt = true,
                            poNum = rcv.poNum,
                        };
                        var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "GetPOInfo", pData);
                        if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                        {
                            result = new
                            {
                                code = 200,
                                status = "OK",
                                data = new
                                {
                                    epi_code = 400,
                                    epi_status = bo.ResponseError.ToString()
                                }
                            };
                            return result;
                        }
                        else
                        {
                            preview = bo.ResponseBody.ToString();
                            RcvResponse newResponse = JsonConvert.DeserializeObject<RcvResponse>(preview);
                            list = newResponse.parameters.ds.RcvHead.ToList();
                            dynamic update = UpdateNewHeader(list, rcv);

                            if (update.epi_code != 200)
                            {
                                result = new
                                {
                                    code = 200,
                                    status = "OK",
                                    data = update
                                };
                                return result;
                            }
                            else
                            {
                                result = new
                                {
                                    code = 200,
                                    status = "OK",
                                    data = update
                                };
                                return result;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    dynamic result = new
                    {
                        code = 500,
                        status = ex.Message.ToString(),
                    };
                    return result;
                }
            }
            else
            {
                dynamic result = new
                {
                    code = 401,
                    status = "Not Authorized or Server Full"
                };
                return result;
            }
        }
        [Route("Receipt/GetPOInfo")]
        [HttpGet]
        public dynamic GetPOInfo([FromBody] RcvParam rcv)
        {
            try
            {
                List<RcvHead> list = new List<RcvHead>();
                user.nik = rcv.nik;
                user.password = rcv.password;
                if (!epicRest.PortalBeearer(user))
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = "Not Authorized or Server Full"
                        }
                    };
                }
                dynamic result;
                list = GetnewList();
                string preview;
                var rcvHead = list.Last();
                rcvHead.RowMod = "A";
                var pData = new
                {
                    ds = new
                    {
                        RcvHead = new[] { rcvHead },
                    },
                    fromReceiptEntryNewRcpt = true,
                    poNum = rcv.poNum,
                };
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "GetPOInfo", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 400,
                            epi_status = bo.ResponseError.ToString()
                        }
                    };
                    return result;
                }
                else
                {
                    preview = bo.ResponseBody.ToString();
                    RcvResponse newResponse = JsonConvert.DeserializeObject<RcvResponse>(preview);
                    list = newResponse.parameters.ds.RcvHead.ToList();
                    result = new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 200,
                            epi_status = bo.ResponseError.ToString(),
                            RcvHead = list.FirstOrDefault()
                        }
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {

                dynamic result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        [Route("Receipt/UpdateHeader")]
        [HttpPost]
        public dynamic UpdateHeader([FromBody] RcvParam rcv)
        {
            try
            {
                List<RcvHead> dt = new List<RcvHead>();
                user.nik = rcv.nik;
                user.password = rcv.password;
                if (!epicRest.PortalBeearer(user))
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = "Not Authorized or Server Full"
                        }
                    };
                }
                var list = GetByID(rcv);
                var list2 = GetByID(rcv);

                if (list.epi_code != 200)
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = list.epi_status.ToString()
                        }
                    };
                }
                List<RcvHead> listResult = list.list;
                List<RcvHead> listResult2 = list2.list;

                var rcvHead = listResult.FirstOrDefault();
                if (rcvHead == null)
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = "PackSlip Not Found"
                        }
                    };
                }
                var rcvHead2 = listResult2.FirstOrDefault();
                rcvHead.PackSlip = rcv.PackSlip;
                rcvHead.ReceiptComment = rcv.receiptComment;
                rcvHead.ArrivedDate = rcv.ArrivalDate;

                dynamic pData;
                if (rcv.RowMod == "U")
                {
                    rcvHead.RowMod = "";
                    rcvHead2.RowMod = rcv.RowMod;
                    rcvHead2.PackSlip = rcv.PackSlip;
                    rcvHead2.ReceiptComment = rcv.receiptComment;
                    rcvHead2.ArrivedDate = rcv.ArrivalDate;

                }
                else if (rcv.RowMod == "D")
                {
                    rcvHead.RowMod = rcv.RowMod;
                    rcvHead2.RowMod = "";
                    rcvHead2.PackSlip = rcv.PackSlip;
                    rcvHead2.ReceiptComment = rcv.receiptComment;
                    rcvHead2.ArrivedDate = rcv.ArrivalDate;

                }
                else
                {
                    pData = new { };
                }
                pData = new
                {
                    RunChkHdrBeforeUpdate = true,
                    RunChkLCAmtBeforeUpdate = true,
                    ds = new
                    {
                        RcvHead = new[] { rcvHead, rcvHead2 },
                    },
                    ipPackLine = "0",
                    ipPackSlip = rcv.PackSlip,
                    ipPurPoint = "",
                    ipVendorNum = rcvHead.VendorNum,
                    lOkToUpdate = true,
                    lRunCheckCompliance = false,
                    lRunChkDtl = true,
                    lRunChkDtlCompliance = true,
                    lRunCreatePartLot = false,
                    lRunPreUpdate = true,
                    lotNum = "",
                    partNum = ""
                };
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "UpdateMaster", pData);

                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = bo.ResponseError.ToString()
                        }
                    };
                }

                RcvResponse newResponse = JsonConvert.DeserializeObject<RcvResponse>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.RcvHead.ToList();

                return new
                {
                    code = 200,
                    status = "OK",
                    data = new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        RcvHead = dt.FirstOrDefault()
                    }
                };
            }
            catch (Exception ex)
            {
                var result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        [Route("Receipt/GetPOLine")]
        [HttpGet]
        public dynamic PoLine([FromBody] RcvDtlParam rcv) {
            try
            {
                user.nik = rcv.nik;
                user.password = rcv.password;
                List<RcvDtl> list = new List<RcvDtl>();
                List<RcvDtl> list2 = new List<RcvDtl>();
                if (!epicRest.PortalBeearer(user))
                {
                    return new
                    {
                        code = 401,
                        status = "Not Authorized or Server Full"
                    };
                }
                var dataNew = GetNewDetail(rcv);
                if (dataNew.epi_code != 200)
                {
                    return dataNew;
                }
                list = dataNew.data as List<RcvDtl>;
                var rcvDtl = list.FirstOrDefault();

                var GetPO = GetPOLine(rcv, list);
                if (GetPO.epi_code != 200)
                {
                    return new
                    {
                        code = 200,
                        status = "Ok",
                        data = GetPO
                    };
                }
                else
                {
                    return new
                    {
                        code = 200,
                        status = "Ok",
                        data = GetPO
                    };
                }
                
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        [Route("Receipt/RcvDtlPo")]
        [HttpGet]
        public dynamic RcvDtlPo([FromBody] RcvDtlParam rcv)
        {
            try
            {
                user.nik = rcv.nik;
                user.password = rcv.password;
                List<RcvDtl> list = new List<RcvDtl>();
                List<RcvDtl> list2 = new List<RcvDtl>();
                if (!epicRest.PortalBeearer(user))
                {
                    return new
                    {
                        code = 401,
                        status = "Not Authorized or Server Full"
                    };
                }
                var dataNew = GetNewDetail(rcv);
                if (dataNew.epi_code != 200)
                {
                    return dataNew;
                }
                list = dataNew.data as List<RcvDtl>;
                var rcvDtl = list.FirstOrDefault();

                var GetPO = GetRcvDtlPO(rcv, list);
                if (GetPO.epi_code != 200)
                {
                    return new
                    {
                        code = 200,
                        status = "Ok",
                        data = GetPO
                    };
                }
                list2 = GetPO.rcvDtls as List<RcvDtl>;
                var polinelist = list.FirstOrDefault();

                var POLine = GetPOLine(rcv, list2);
                if (POLine.epi_code != 200)
                {
                    return new
                    {
                        code = 200,
                        status = "Ok",
                        data = POLine
                    };
                }
                else
                {
                    return new
                    {
                        code = 200,
                        status = "Ok",
                        data = POLine
                    };
                }


            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [Route("Receipt/GetQtyInfo")]
        [HttpGet]
        public dynamic GetQtyInfo([FromBody] RcvDtlParam rcv) {
                user.nik = rcv.nik;
                user.password = rcv.password;
                List<RcvDtl> list = new List<RcvDtl>();
                List<RcvDtl> list2 = new List<RcvDtl>();

                if (!epicRest.PortalBeearer(user))
                {
                    return new
                    {
                        code = 401,
                        status = "Not Authorized or Server Full"
                    };
                }
                try
                {

                    if (rcv.RowMod == "A")
                    {
                        var dataNew = GetNewDetail(rcv);
                        if (dataNew.epi_code != 200)
                        {
                            return dataNew;
                        }
                        list = dataNew.data as List<RcvDtl>;
                        var rcvDtl = list.Last();

                        var GetPO = GetRcvDtlPOUpdate(rcv, list);
                        if (GetPO.epi_code != 200)
                        {
                            return new
                            {
                                code = 200,
                                status = "Ok",
                                data = GetPO
                            };
                        }
                        list2 = GetPO.data as List<RcvDtl>;
                        var polinelist = list.Last();

                        var POLine = GetPOLineUpdate(rcv, list);
                        if (POLine.epi_code != 200)
                        {
                            return new
                            {
                                code = 200,
                                status = "Ok",
                                data = POLine
                            };
                        }
                        list = POLine.data;
                        list2 = POLine.data;

                    }
                    else
                    {
                        var getDtlData = GetDtlByID(rcv.VendorNum, rcv.PackSlip, rcv.PackLine);
                        var getDtlData2 = GetDtlByID(rcv.VendorNum, rcv.PackSlip, rcv.PackLine);
                        if (getDtlData.epi_code != 200)
                        {
                            return new
                            {
                                code = 200,
                                status = "Ok",
                                data = getDtlData
                            };
                        }
                        var pohead = Convert.ToInt32(rcv.PoNum);
                        var podtl = Convert.ToInt32(rcv.PoLine);

                        list = getDtlData.data as List<RcvDtl>;
                        list2 = getDtlData2.data as List<RcvDtl>;
                        var getDtlResult = list.Last();
                        if (pohead != getDtlResult.PONum)
                        {
                            var dtData = list.Last();
                            var dtData2 = list2.Last();
                            dtData2.RowMod = rcv.RowMod;
                            List<RcvDtl> send = [dtData, dtData2];
                            var GetPO = GetRcvDtlPOUpdate(rcv, send);
                            if (GetPO.epi_code != 200)
                            {
                                return new
                                {
                                    code = 200,
                                    status = "Ok",
                                    data = GetPO
                                };
                            }
                            list2 = GetPO.data as List<RcvDtl>;
                        }
                        if (podtl != getDtlResult.POLine)
                        {
                            var dtData = list.First();
                            var dtData2 = list2.Last();
                            dtData2.RowMod = rcv.RowMod;
                            List<RcvDtl> send = [dtData, dtData2];
                            var POLine = GetPOLineUpdate(rcv, send);
                            if (POLine.epi_code != 200)
                            {
                                return new
                                {
                                    code = 200,
                                    status = "Ok",
                                    data = POLine
                                };
                            }
                            list = POLine.data;
                            list2 = POLine.data;
                        }
                        if (podtl == getDtlResult.POLine && pohead == getDtlResult.PONum)
                        {
                            list = getDtlData.data as List<RcvDtl>;
                            list2 = getDtlData2.data as List<RcvDtl>;
                            var newList2 = list2.FirstOrDefault();
                            var newList = list.FirstOrDefault();
                            newList2.RowMod = rcv.RowMod;
                            list = [newList, newList2];
                        }

                    }

                    var QtyInfo = GetDtlQtyInfo(rcv, list, list2);
                    var QtyInfo2 = GetDtlQtyInfo(rcv, list, list2);
                    if (QtyInfo.epi_code != 200)
                    {
                        return new
                        {
                            code = 200,
                            status = "Ok",
                            data = QtyInfo
                        };
                    }
                    list = QtyInfo.data as List<RcvDtl>;
                    list2 = QtyInfo2.data as List<RcvDtl>;
                return new
                {
                    code = 200,
                    status = "Ok",
                    data = new
                    {
                        epi_code = 200,
                        epi_status = "Ok",
                        rcvDtls = list2.Last()
                    }
                };
            } 
            catch (Exception ex)
            {
                var result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
            }

        [Route("Receipt/UpdateDetail")]
        [HttpPost]
        public dynamic CreateNewDtl([FromBody] RcvDtlParam rcv)
        {
            user.nik = rcv.nik;
            user.password = rcv.password;
            List<RcvDtl> list = new List<RcvDtl>();
            List<RcvDtl> list2 = new List<RcvDtl>();

            if (!epicRest.PortalBeearer(user))
            {
                return new
                {
                    code = 401,
                    status = "Not Authorized or Server Full"
                };
            }
            try
            {

                if (rcv.RowMod == "A")
                {
                    var dataNew = GetNewDetail(rcv);
                    if (dataNew.epi_code != 200)
                    {
                        return dataNew;
                    }
                    list = dataNew.data as List<RcvDtl>;
                    var rcvDtl = list.Last();

                    var GetPO = GetRcvDtlPOUpdate(rcv, list);
                    if (GetPO.epi_code != 200)
                    {
                        return new
                        {
                            code = 200,
                            status = "Ok",
                            data = GetPO
                        };
                    }
                    list2 = GetPO.data as List<RcvDtl>;
                    var polinelist = list.Last();

                    var POLine = GetPOLineUpdate(rcv, list);
                    if (POLine.epi_code != 200)
                    {
                        return new
                        {
                            code = 200,
                            status = "Ok",
                            data = POLine
                        };
                    }
                    list = POLine.data;
                    list2 = POLine.data;

                }
                else
                {
                    var getDtlData = GetDtlByID(rcv.VendorNum, rcv.PackSlip, rcv.PackLine);
                    var getDtlData2 = GetDtlByID(rcv.VendorNum, rcv.PackSlip, rcv.PackLine);

                    if (getDtlData.epi_code != 200)
                    {
                        return new
                        {
                            code = 200,
                            status = "Ok",
                            data = getDtlData
                        };
                    }
                    var pohead = Convert.ToInt32(rcv.PoNum);
                    var podtl = Convert.ToInt32(rcv.PoLine);

                    list = getDtlData.data as List<RcvDtl>;
                    list2 = getDtlData2.data as List<RcvDtl>;
                    var getDtlResult = list.Last();
                    if (pohead != getDtlResult.PONum) {
                        var dtData = list.Last();
                        var dtData2 = list2.Last();
                        dtData2.RowMod = rcv.RowMod;
                        List<RcvDtl> send = [dtData,dtData2];
                        var GetPO = GetRcvDtlPOUpdate(rcv, send);
                        if (GetPO.epi_code != 200)
                        {
                            return new
                            {
                                code = 200,
                                status = "Ok",
                                data = GetPO
                            };
                        }
                        list2 = GetPO.data as List<RcvDtl>;
                    }
                    if (podtl != getDtlResult.POLine)
                    {
                        var dtData = list.First();
                        var dtData2 = list2.Last();
                        dtData2.RowMod = rcv.RowMod;
                        List<RcvDtl> send = [dtData, dtData2];
                        var POLine = GetPOLineUpdate(rcv, send);
                        if (POLine.epi_code != 200)
                        {
                            return new
                            {
                                code = 200,
                                status = "Ok",
                                data = POLine
                            };
                        }
                        list = POLine.data;
                        list2 = POLine.data;
                    }
                    if (podtl == getDtlResult.POLine && pohead == getDtlResult.PONum) {
                        list = getDtlData.data as List<RcvDtl>;
                        list2 = getDtlData2.data as List<RcvDtl>;
                        var newList2 = list2.FirstOrDefault();
                        var newList = list.FirstOrDefault();
                        newList2.RowMod = rcv.RowMod;
                        list = [newList, newList2];
                    }
                        
                }

                var QtyInfo = GetDtlQtyInfo(rcv, list, list2);
                var QtyInfo2 = GetDtlQtyInfo(rcv, list, list2);
                if (QtyInfo.epi_code != 200)
                {
                    return new
                    {
                        code = 200,
                        status = "Ok",
                        data = QtyInfo
                    };
                }
                list = QtyInfo.data as List<RcvDtl>;
                list2 = QtyInfo2.data as List<RcvDtl>;

                var dtlData = list.Last();
                var dtlData2 = list2.Last();
                var validate = ValidateBeforeSave(dtlData.PackSlip, dtlData.PartNum, dtlData.PONum, dtlData.VendorNum);
                if (validate.epi_code != 200)
                {
                    return new
                    {
                        code = 200,
                        status = "Ok",
                        data = validate
                    };
                }

                dtlData.RowMod = rcv.RowMod;
                dtlData.QtyOption = rcv.QtyOption;
                dtlData.InputOurQty = rcv.InputOurQty;
                dtlData.ThisTranQty = rcv.InputOurQty;
                dtlData.OurQty = rcv.InputOurQty;
                dtlData.VendorQty = rcv.VendorQty;
                dtlData.IUM = rcv.IUM;
                dtlData.PUM = rcv.PUM;
                dtlData.WareHouseCode = rcv.WarehouseCode;
                dtlData.BinNum = rcv.BinNum;
                dtlData.TranReference = rcv.TranReference;
                dtlData.ConvOverride = rcv.ConvOverride;
                dtlData.PORelNum = 1;
                dtlData.ReceivedComplete = rcv.ReceivedComplete;
                dtlData.Received = rcv.Received;
                dtlData.LegalNumber = rcv.LegalNumber;
                dtlData.LotNum = rcv.LotNum;
                dtlData.IssuedComplete = rcv.IssuedComplete;


                if (rcv.RowMod == "U")
                {
                    dtlData2.RowMod = "";
                    dtlData2.QtyOption = rcv.QtyOption;
                    dtlData2.InputOurQty = rcv.InputOurQty;
                    dtlData2.OurQty = rcv.InputOurQty;
                    dtlData2.IUM = rcv.IUM;
                    dtlData2.VendorQty = rcv.VendorQty;
                    dtlData2.PUM = rcv.PUM;
                    dtlData2.WareHouseCode = rcv.WarehouseCode;
                    dtlData2.BinNum = rcv.BinNum;
                    dtlData2.TranReference = rcv.TranReference;
                    dtlData2.ConvOverride = rcv.ConvOverride;
                    dtlData2.ReceivedComplete = rcv.ReceivedComplete;
                    dtlData2.ThisTranQty = rcv.InputOurQty;
                    dtlData2.PORelNum = 1;
                    dtlData2.Received = rcv.Received;
                    dtlData2.LotNum = rcv.LotNum;
                    dtlData2.IssuedComplete = rcv.IssuedComplete;

                }
                else if (rcv.RowMod == "D")
                {
                    dtlData.RowMod = "D";
                    dtlData2.RowMod = "";
                    dtlData2.QtyOption = rcv.QtyOption;
                    dtlData2.InputOurQty = rcv.InputOurQty;
                    dtlData2.IUM = rcv.IUM;
                    dtlData2.VendorQty = rcv.VendorQty;
                    dtlData2.PUM = rcv.PUM;
                    dtlData2.WareHouseCode = rcv.WarehouseCode;
                    dtlData2.BinNum = rcv.BinNum;
                    dtlData2.TranReference = rcv.TranReference;
                    dtlData2.ConvOverride = rcv.ConvOverride;
                    dtlData.ReceivedComplete = rcv.ReceivedComplete;
                    dtlData2.PORelNum = 1;
                    dtlData2.Received = rcv.Received;
                    dtlData.LotNum = rcv.LotNum;

                }
                dynamic pData;
                if (rcv.RowMod == "U")
                {
                    pData = new
                    {
                        RunChkHdrBeforeUpdate = true,
                        RunChkLCAmtBeforeUpdate = true,
                        ds = new
                        {
                            RcvDtl = new[] { dtlData2, dtlData },
                        },
                        ipPackLine = 0,
                        ipPackSlip = rcv.PackSlip,
                        ipPurPoint = "",
                        ipVendorNum = rcv.VendorNum,
                        lOkToUpdate = true,
                        lRunCheckCompliance = false,
                        lRunChkDtl = true,
                        lRunChkDtlCompliance = true,
                        lRunCreatePartLot = false,
                        lotNum = "",
                        lRunPreUpdate = true,
                        partNum = ""
                    };
                }
                else if (rcv.RowMod == "D")
                {
                    pData = new
                    {
                        RunChkHdrBeforeUpdate = true,
                        RunChkLCAmtBeforeUpdate = true,
                        ds = new
                        {
                            RcvDtl = new[] { dtlData2, dtlData },
                        },
                        ipPackLine = 0,
                        ipPackSlip = rcv.PackSlip,
                        ipPurPoint = "",
                        ipVendorNum = rcv.VendorNum,
                        lOkToUpdate = true,
                        lRunCheckCompliance = false,
                        lRunChkDtl = true,
                        lRunChkDtlCompliance = true,
                        lRunCreatePartLot = false,
                        lotNum = "",
                        lRunPreUpdate = true,
                        partNum = ""
                    };
                }
                else
                {
                    pData = new
                    {
                        RunChkHdrBeforeUpdate = true,
                        RunChkLCAmtBeforeUpdate = true,
                        ds = new
                        {
                            RcvDtl = new[] { dtlData },
                        },
                        ipPackLine = rcv.PackLine,
                        ipPackSlip = rcv.PackSlip,
                        ipPurPoint = "",
                        ipVendorNum = rcv.VendorNum,
                        lOkToUpdate = true,
                        lRunCheckCompliance = false,
                        lRunChkDtl = true,
                        lRunChkDtlCompliance = true,
                        lRunCreatePartLot = false,
                        lotNum = "",
                        lRunPreUpdate = true,
                        partNum = ""
                    };
                }
                string rtn;
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "UpdateMaster", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        code = 200,
                        status = "Ok",
                        data = new
                        {
                            epi_code = 400,
                            epi_status = bo.ResponseError.ToString(),
                        }
                    };
                    return result;
                }
                else
                {
                    rtn = bo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    Responder apiResponse = JsonConvert.DeserializeObject<Responder>(rtn);
                    List<RcvDtl> rcvDtls = apiResponse.Parameters.ds.RcvDtl;
                    result = new
                    {
                        code = 200,
                        status = "Ok",
                        data = new
                        {
                            epi_code = 200,
                            epi_status = "Ok",
                            RcvDtl = rcvDtls.FirstOrDefault()
                        }
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                var result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        [Route("Receipt/AttachFile")]
        [HttpPost]
        public dynamic AttachFile([FromBody] RcvAttachment attachment)
        {
            try
            {
                List<RcvHeadAttch> dt = new List<RcvHeadAttch>();

                user.nik = attachment.nik;
                user.password = attachment.password;
                if (!epicRest.PortalBeearer(user))
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = "Not Authorized or Server Full"
                        }
                    };
                }

                var uploadFile = UploadFile(attachment);
                if (uploadFile.epi_code != 200)
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = uploadFile.epi_status
                        }
                    };
                }
                string filePath = uploadFile.data.preview;
                List<RcvHeadAttch> list = new List<RcvHeadAttch>();
                var dataNew = GetAttachmentDs(attachment.PackSlip, attachment.VendorNum);
                if (dataNew.epi_code != 200)
                {
                    return dataNew;
                }
                list = dataNew.data as List<RcvHeadAttch>;
                var rcvAttachment = list.FirstOrDefault();
                rcvAttachment.FileName = filePath;
                rcvAttachment.DrawDesc = attachment.DrawDesc;
                rcvAttachment.RowMod = "A";
                var pData = new
                {
                    RunChkHdrBeforeUpdate = true,
                    RunChkLCAmtBeforeUpdate = true,
                    ds = new
                    {
                        RcvHeadAttch = new[] { rcvAttachment },
                    },
                    ipPackLine = "0",
                    ipPackSlip = attachment.PackSlip,
                    ipPurPoint = "",
                    ipVendorNum = attachment.VendorNum,
                    lOkToUpdate = true,
                    lRunCheckCompliance = false,
                    lRunChkDtl = true,
                    lRunChkDtlCompliance = true,
                    lRunCreatePartLot = false,
                    lRunPreUpdate = true,
                    lotNum = "",
                    partNum = ""
                };
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "UpdateMaster", pData);
                RcvResponse newResponse = JsonConvert.DeserializeObject<RcvResponse>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.RcvHeadAttch.ToList();

                return new
                {
                    code = 200,
                    status = "OK",
                    data = new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        RcvHeadAttch = dt.FirstOrDefault()
                    }
                };

            }
            catch (Exception ex)
            {
                dynamic result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        [Route("Receipt/DettachFile")]
        [HttpPost]
        public dynamic DettachFile([FromBody] RcvParam rcv)
        {
            try
            {
                List<RcvHeadAttch> dt = new List<RcvHeadAttch>();

                user.nik = rcv.nik;
                user.password = rcv.password;
                if (!epicRest.PortalBeearer(user))
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = "Not Authorized or Server Full"
                        }
                    };
                }
                int xFileRefNum = rcv.DrawSeq;
                var deleteFile = DeleteFile(xFileRefNum);
                if (deleteFile.epi_code != 200)
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = deleteFile.epi_status
                        }
                    };
                }
                var list = GetByIDAttachment(rcv);
                var list2 = GetByIDAttachment(rcv);

                List<RcvHeadAttch> listResult = list.list;
                List<RcvHeadAttch> listResult2 = list2.list;

                var rcvHead = listResult.Where(x => x.XFileRefNum == xFileRefNum).FirstOrDefault();
                if (rcvHead == null)
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = "Attachment Not Found"
                        }
                    };
                }
                var rcvHead2 = listResult2.Where(x => x.XFileRefNum == xFileRefNum).FirstOrDefault();
                rcvHead.PackSlip = rcv.PackSlip;

                dynamic pData;
                rcvHead.RowMod = rcv.RowMod;
                rcvHead2.RowMod = "";
                rcvHead2.PackSlip = rcv.PackSlip;


                pData = new
                {
                    RunChkHdrBeforeUpdate = true,
                    RunChkLCAmtBeforeUpdate = true,
                    ds = new
                    {
                        RcvHeadAttch = new[] { rcvHead, rcvHead2 },
                    },
                    ipPackLine = "0",
                    ipPackSlip = rcv.PackSlip,
                    ipPurPoint = "",
                    ipVendorNum = rcvHead.VendorNum,
                    lOkToUpdate = true,
                    lRunCheckCompliance = false,
                    lRunChkDtl = true,
                    lRunChkDtlCompliance = true,
                    lRunCreatePartLot = false,
                    lRunPreUpdate = true,
                    lotNum = "",
                    partNum = ""
                };
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "UpdateMaster", pData);

                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = bo.ResponseError.ToString()
                        }
                    };
                }

                return new
                {
                    code = 200,
                    status = "OK",
                    data = new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        RcvHeadAttch = Array.Empty<string>()
                    }
                };


            }
            catch (Exception)
            {

                throw;
            }


        }
        public dynamic GetPOLineUpdate(RcvDtlParam rcv, List<RcvDtl> rcvDtl)
        {
            try
            {
                List<RcvDtl> list = new List<RcvDtl>();
                var pData = new
                {
                    ds = new
                    {
                        RcvDtl = rcvDtl,
                    },
                    packLine = rcv.PackLine,
                    packSlip = rcv.PackSlip,
                    poLine = rcv.PoLine,
                    purPoint = "",
                    vendorNum = rcv.VendorNum,
                };
                string rtn;
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "GetDtlPOLineInfo", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 401,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                else
                {
                    rtn = bo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    Responder apiResponse = JsonConvert.DeserializeObject<Responder>(rtn);
                    List<RcvDtl> rcvDtls = apiResponse.Parameters.ds.RcvDtl;
                    result = new
                    {
                        epi_code = 200,
                        epi_status = "Ok",
                        data = rcvDtls
                    };
                    return result;

                }
            }
            catch (Exception ex)
            {
                dynamic result = new
                {
                    code = 500,
                    status = ex.Message.ToString()
                };
                return result;
            }
        }
        public dynamic GetRcvDtlPOUpdate(RcvDtlParam rcv, List<RcvDtl> rcvDtl)
        {
            try
            {
                List<RcvDtl> list = new List<RcvDtl>();
                    var pData = new
                    {
                        ds = new
                        {
                            RcvDtl =rcvDtl,
                        },
                        packLine = rcv.PackLine,
                        packSlip = rcv.PackSlip,
                        poNum = rcv.PoNum,
                        purPoint = "",
                        requiresUserInput = true,
                        vendorNum = rcv.VendorNum,
                    };
               
                string rtn;
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "GetDtlPOInfo", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 401,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                else
                {
                    rtn = bo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    Responder apiResponse = JsonConvert.DeserializeObject<Responder>(rtn);
                    List<RcvDtl> rcvDtls = apiResponse.Parameters.ds.RcvDtl;
                    result = new
                    {
                        epi_code = 200,
                        epi_status = "Ok",
                        data = rcvDtls
                    };
                    return result;

                }
            }
            catch (Exception ex)
            {
                dynamic result = new
                {
                    code = 500,
                    status = ex.Message.ToString()
                };
                return result;
            }
        }
        public dynamic GetDtlQtyInfo(RcvDtlParam rcv, List<RcvDtl> rcvDtl, List<RcvDtl> rcvDtl2) {
            try
            {
                List<RcvDtl> list = new List<RcvDtl>();
                List<RcvDtl> list2 = new List<RcvDtl>();
                list = rcvDtl;
                list2 = rcvDtl2;
                var dt = list.FirstOrDefault();
                var dt2 = list2.FirstOrDefault();
                dt.RowMod = "";
                dt2.RowMod = rcv.RowMod;
                var pData = new
                {
                    ds = new
                    {
                        RcvDtl = rcvDtl,
                    },
                    inputIUM = rcv.IUM,
                    inputOurQty = rcv.InputOurQty,
                    packLine = rcv.PackLine,
                    packSlip = rcv.PackSlip,
                    purPoint = "",
                    vendorNum = rcv.VendorNum,
                    whichField = "QTY",
                };
                string rtn;
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "GetDtlQtyInfo", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 401,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                else
                {
                    rtn = bo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    Responder apiResponse = JsonConvert.DeserializeObject<Responder>(rtn);
                    List<RcvDtl> rcvDtls = apiResponse.Parameters.ds.RcvDtl;
                    result = new
                    {
                        epi_code = 200,
                        epi_status = "Ok",
                        data = rcvDtls
                    };
                    return result;

                }
            }
            catch (Exception ex)
            {
                dynamic result = new
                {
                    code = 500,
                    status = ex.Message.ToString()
                };
                return result;
            }
        }
        public dynamic GetByID(RcvParam rcv)
        {
            user.nik = rcv.nik;
            user.password = rcv.password;
            bool test = epicRest.PortalBeearer(user);
            List<RcvHead> list = new List<RcvHead>();
            if (test)
            {
                try
                {
                    dynamic result;
                    MultiMap<string, string> dic = new MultiMap<string, string>();
                    var VendorNum = rcv.VendorNum;
                    var PackSlip = rcv.PackSlip;
                    var PurPoint = "";

                    dic.Add("packSlip", PackSlip);
                    dic.Add("vendorNum", VendorNum.ToString());
                    dic.Add("purPoint", PurPoint);

                    var bo = EpicorRest.BoGet("Erp.BO.ReceiptSvc", "GetByID", dic);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            epi_code = 401,
                            epi_status = bo.ResponseError.ToString()
                        };
                    }
                    else
                    {
                        string rtn;
                        rtn = bo.ResponseBody.ToString();
                        JObject jsonObject = JObject.Parse(rtn);
                        JArray jsonArray = (JArray)jsonObject["returnObj"]["RcvHead"];
                        rtn = jsonArray == null ? "" : jsonArray.ToString();
                        // Mengambil data ShipHead

                        if (rtn.StartsWith("[")) //jadi table
                        {
                            result = new
                            {
                                epi_code = 200,
                                epi_status = "OK",
                                list = JsonConvert.DeserializeObject<List<RcvHead>>(rtn)
                            };
                        }
                        else
                        {
                            result = new
                            {
                                epi_code = 401,
                                epi_status = "Can't Generate value ShipHead"
                            };
                        }
                    }
                    return result;

                }
                catch (Exception ex)
                {
                    var result = new
                    {
                        code = 500,
                        status = ex.Message.ToString(),
                    };
                    return result;
                }
            }
            else
            {

                dynamic result = new
                {
                    code = 401,
                    status = "Not Authorized or Server Full"
                };
                return result;
            }
        }
        public dynamic GetByIDAttachment(RcvParam rcv)
        {
            user.nik = rcv.nik;
            user.password = rcv.password;
            bool test = epicRest.PortalBeearer(user);
            List<RcvHeadAttch> list = new List<RcvHeadAttch>();
            if (test)
            {
                try
                {
                    dynamic result;
                    MultiMap<string, string> dic = new MultiMap<string, string>();
                    var VendorNum = rcv.VendorNum;
                    var PackSlip = rcv.PackSlip;
                    var PurPoint = "";

                    dic.Add("packSlip", PackSlip);
                    dic.Add("vendorNum", VendorNum.ToString());
                    dic.Add("purPoint", PurPoint);

                    var bo = EpicorRest.BoGet("Erp.BO.ReceiptSvc", "GetByID", dic);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            epi_code = 401,
                            epi_status = bo.ResponseError.ToString()
                        };
                    }
                    else
                    {
                        string rtn;
                        rtn = bo.ResponseBody.ToString();
                        JObject jsonObject = JObject.Parse(rtn);
                        JArray jsonArray = (JArray)jsonObject["returnObj"]["RcvHeadAttch"];
                        rtn = jsonArray == null ? "" : jsonArray.ToString();
                        // Mengambil data ShipHead

                        if (rtn.StartsWith("[")) //jadi table
                        {
                            result = new
                            {
                                epi_code = 200,
                                epi_status = "OK",
                                list = JsonConvert.DeserializeObject<List<RcvHeadAttch>>(rtn)
                            };
                        }
                        else
                        {
                            result = new
                            {
                                epi_code = 401,
                                epi_status = "Can't Generate value ShipHead"
                            };
                        }
                    }
                    return result;

                }
                catch (Exception ex)
                {
                    var result = new
                    {
                        code = 500,
                        status = ex.Message.ToString(),
                    };
                    return result;
                }
            }
            else
            {

                dynamic result = new
                {
                    code = 401,
                    status = "Not Authorized or Server Full"
                };
                return result;
            }
        }
        public dynamic GetDtlByID(int VendorNum, string PackSlip, int PackLine)
        {
            try
            {
                dynamic result;
                List<RcvHead> listHead = new List<RcvHead>();
                List<RcvDtl> listData = new List<RcvDtl>();
                RcvDtl data = new RcvDtl();

                string rtn = "";
                string method = "RcvDtls('SAI'," + VendorNum + ",'','" + PackSlip + "'," + PackLine + ")";
                var boGet = EpicorRest.BoGet("Erp.BO.ReceiptSvc", method);
                if (boGet.IsErrorResponse)
                {
                    result = new
                    {
                        epi_code = 400,
                        epi_status = boGet.ResponseError.ToString(),
                        func = "GetDtlByID"
                    };

                    return result;
                }
                else
                {
                    rtn = boGet.ResponseBody.ToString();
                    var ds = new
                    {
                        RcvDtl = new[] { rtn }
                    };
                    data = JsonConvert.DeserializeObject<RcvDtl>(rtn);
                    listData = new List<RcvDtl> { data };
                    result = new
                    {
                        epi_code = 200,
                        epi_status = "Ok",
                        data = listData

                    };
                    return result;
                }

            }
            catch (Exception ex)
            {

                var result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        public dynamic GetNewDetail(RcvDtlParam rcv)
        {
            List<RcvHead> list = new List<RcvHead>();
            try
            {
                string rtn;
                var pData = new
                {
                    ds = new
                    {
                        RcvDtl = Array.Empty<object>(),
                    },
                    packSlip = rcv.PackSlip,
                    purPoint = "",
                    vendorNum = rcv.VendorNum
                };
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "GetNewRcvDtl", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 401,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return list;
                }
                else
                {
                    rtn = bo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    Responder apiResponse = JsonConvert.DeserializeObject<Responder>(rtn);
                    List<RcvDtl> rcvDtl = apiResponse.Parameters.ds.RcvDtl;
                    result = new
                    {
                        epi_code = 200,
                        epi_status = "Ok",
                        data = rcvDtl
                    };
                    return result;

                }
            }
            catch (Exception ex)
            {

                var result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        public List<RcvHead> GetnewList()
        {
            List<RcvHead> list = new List<RcvHead>();
            try
            {
                string rtn;
                var pData = new
                {
                    ds = new
                    {
                        RcvHead = Array.Empty<object>(),
                    },
                    purPoint = "",
                    vendorNum = 0
                };
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "GetNewRcvHead", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        code = 401,
                        status = bo.ResponseError.ToString()
                    };
                    return list;
                }
                else
                {
                    rtn = bo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    Responder apiResponse = JsonConvert.DeserializeObject<Responder>(rtn);
                    List<RcvHead> rcvHead = apiResponse.Parameters.ds.RcvHead;
                    return rcvHead;

                }
            }
            catch (Exception ex)
            {

                dynamic result = new
                {
                    code = 500,
                    status =ex.Message.ToString(),
                };
                return result;
            }
        }
        public dynamic GetRcvDtlPO(RcvDtlParam rcv, List<RcvDtl> rcvDtl)
        {
            try
            {
                List<RcvDtl> list = new List<RcvDtl>();
                var pData = new
                {
                    ds = new
                    {
                        RcvDtl = new[] { rcvDtl.FirstOrDefault() },
                    },
                    packLine = rcv.PackLine,
                    packSlip = rcv.PackSlip,
                    poNum = rcv.PoNum,
                    purPoint = "",
                    requiresUserInput=true,
                    vendorNum = rcv.VendorNum,
                };
                string rtn;
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "GetDtlPOInfo", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 401,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                else
                {
                    rtn = bo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    Responder apiResponse = JsonConvert.DeserializeObject<Responder>(rtn);
                    List<RcvDtl> rcvDtls = apiResponse.Parameters.ds.RcvDtl;
                    result = new
                    {
                        epi_code = 200,
                        epi_status = "Ok",
                        rcvDtls = rcvDtls
                    };
                    return result;

                }
            }
            catch (Exception ex)
            {
                dynamic result = new
                {
                    code = 500,
                    status = ex.Message.ToString()
                };
                return result;
            }
        }
        public dynamic GetPOLine(RcvDtlParam rcv, List<RcvDtl> rcvDtl)
        {
            try
            {
                List<RcvDtl> list = new List<RcvDtl>();
                var pData = new
                {
                    ds = new
                    {
                        RcvDtl = new[] { rcvDtl.FirstOrDefault() },
                    },
                    packLine = rcv.PackLine,
                    packSlip = rcv.PackSlip,
                    poLine = rcv.PoLine,
                    purPoint = "",
                    vendorNum = rcv.VendorNum,
                };
                string rtn;
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "GetDtlPOLineInfo", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 401,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                else
                {
                    rtn = bo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    Responder apiResponse = JsonConvert.DeserializeObject<Responder>(rtn);
                    List<RcvDtl> rcvDtls = apiResponse.Parameters.ds.RcvDtl;
                    result = new
                    {
                        epi_code = 200,
                        epi_status = "Ok",
                        rcvDtls = rcvDtls.FirstOrDefault()
                    };
                    return result;

                }
            }
            catch (Exception ex)
            {
                dynamic result = new
                {
                    code = 500,
                    status = ex.Message.ToString()
                };
                return result;
            }
        }
        public dynamic UpdateNewHeader(List<RcvHead> list, RcvParam rcv)
        {
            try
            {
                dynamic result;
                string preview;
                var rcvHead = list.Last();
                rcvHead.RowMod = "A";
                rcvHead.PackSlip = rcv.PackSlip;
                var pData = new
                {
                    RunChkHdrBeforeUpdate = true,
                    RunChkLCAmtBeforeUpdate = true,
                    ds = new
                    {
                        RcvHead = new[] { rcvHead },
                    },
                    ipPackLine = "0",
                    ipPackSlip = rcv.PackSlip,
                    ipPurPoint = "",
                    ipVendorNum = rcvHead.VendorNum,
                    lOkToUpdate = true,
                    lRunCheckCompliance = false,
                    lRunChkDtl = true,
                    lRunChkDtlCompliance = true,
                    lRunCreatePartLot = false,
                    lRunPreUpdate = true,
                    lotNum = "",
                    partNum = ""
                };
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "UpdateMaster", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 400,
                        epi_status = bo.ResponseError.ToString(),
                    };
                    return result;
                }
                else
                {
                    preview = bo.ResponseBody.ToString();
                    RcvResponse newResponse = JsonConvert.DeserializeObject<RcvResponse>(preview);
                    list = newResponse.parameters.ds.RcvHead.ToList();
                    result = new
                    {
                        epi_code = 200,
                        epi_status = "OK",
                        RcvHead = list.FirstOrDefault()
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                var result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        public dynamic ValidateBeforeSave(string PackSlip, string PartNum, int? PONum, int? VendorNum)
        {
            try
            {
                string rtn;
                var pData = new
                {
                    packSlip = PackSlip,
                    partNum = PartNum,
                    vendorNum = VendorNum,
                    poNum = PONum,
                    purPoint = ""
                };
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "ValidateSMIReceiptAttrPart", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 401,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                else
                {
                    result = new
                    {
                        epi_code = 200,
                        epi_status = "Ok",
                        data = new { }
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                dynamic result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        public dynamic ValidatePONum(string PONum)
        {
            try
            {
                var pData = new
                {
                    poNum = PONum
                };
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "ValidatePONum", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 404,
                        epi_status = bo.ResponseError.ToString()
                    };

                    return result;
                }
                else
                {
                    result = new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {

                dynamic result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        public dynamic CheckFileExist(RcvAttachment attachment) {
            try
            {
                user.nik = attachment.nik;
                user.password = attachment.password;
                if (!epicRest.PortalBeearer(user))
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = "Not Authorized or Server Full"
                        }
                    };
                }
                var pData = new
                {
                    data = attachment.Data,
                    docTypeID = "",
                    fileName = attachment.FileName,
                    parentTable = attachment.ParentTable
                };
                var bo = EpicorRest.BoPost("Ice.BO.AttachmentSvc", "FileExists", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 404,
                        epi_status = bo.ResponseError.ToString()
                    };

                    return result;
                }
                else
                {
                    result = new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        data = new
                        {
                            preview = bo.ResponseBody.ToString()
                        }
            };
                    return result;
                }

            }
            catch (Exception ex)
            {
                dynamic result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        public dynamic UploadFile(RcvAttachment attachment)
        {
            try
            {
                user.nik = attachment.nik;
                user.password = attachment.password;
                if (!epicRest.PortalBeearer(user))
                {
                    return new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 404,
                            epi_status = "Not Authorized or Server Full"
                        }
                    };
                }
                var pData = new
                {
                    data = attachment.Data,
                    docTypeID = "",
                    fileName = attachment.FileName,
                    parentTable = attachment.ParentTable
                };
                var bo = EpicorRest.BoPost("Ice.BO.AttachmentSvc", "UploadFile", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 404,
                        epi_status = bo.ResponseError.ToString()
                    };

                    return result;
                }
                else
                {
                    string json = bo.ResponseBody.ToString();
                    ResponseObj response = JsonConvert.DeserializeObject<ResponseObj>(json);

                    result = new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        data = new
                        {
                            preview = response.ReturnObj
                        }
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {

                dynamic result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        public dynamic GetAttachmentDs(string packSlip, int VendorNum) {
            List<RcvHeadAttch> list = new List<RcvHeadAttch>();
            try
            {
                string rtn;
                var pData = new
                {
                    ds = new
                    {
                        RcvHeadAttch = Array.Empty<object>(),
                    },
                    packSlip = packSlip,
                    purPoint = "",
                    vendorNum = VendorNum
                };
                var bo = EpicorRest.BoPost("Erp.BO.ReceiptSvc", "GetNewRcvHeadAttch", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 401,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return list;
                }
                else
                {
                    rtn = bo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    Responder apiResponse = JsonConvert.DeserializeObject<Responder>(rtn);
                    List<RcvHeadAttch> rcvAttach = apiResponse.Parameters.ds.RcvHeadAttch;
                    result = new
                    {
                        epi_code = 200,
                        epi_status = "Ok",
                        data = rcvAttach
                    };
                    return result;

                }
            }
            catch (Exception ex)
            {

                var result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        public dynamic DeleteFile(int xFileRefNum)
        {
            try
            {
                List<RcvHeadAttch> dt = new List<RcvHeadAttch>();

                var pData = new
                {
                    xFileRefNum = xFileRefNum,
                    makeBackup = false
                };
                var bo = EpicorRest.BoPost("Ice.BO.AttachmentSvc", "DeleteFile", pData);
                dynamic result;
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 404,
                        epi_status = bo.ResponseError.ToString()
                    };

                    return result;
                }
                else
                {
                    string json = bo.ResponseBody.ToString();
                    ResponseObj response = JsonConvert.DeserializeObject<ResponseObj>(json);

                    result = new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        data = new
                        {
                        }
                    };
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}