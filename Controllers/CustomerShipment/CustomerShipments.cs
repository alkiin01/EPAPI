﻿using Microsoft.AspNetCore.Mvc;
using static EPAPI.Repository.ShipmentRepositories;
using EpicorRestAPI;
using EpicorRestSharedClasses;
using EPAPI.Repository;
using EpicorRestAPI.BAQDesigner;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;
using Swashbuckle.Swagger;
using System.Reflection.Emit;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Microsoft.VisualBasic;
namespace EPAPI.Controllers
{
    public class CustomerShipments : Controller
    {

        public EpicRest epicRest = new EpicRest();
        User user = new User();
        ShipmentRepositories shipmentRepository = new ShipmentRepositories();
        OrderInfo orderInfo = new OrderInfo();


        [Route("Shipment/GetNew")]
        [HttpPost]
        public dynamic GetNew([FromBody] User users)
        {
          user.nik = users.nik;
            user.password = users.password;

            bool test = epicRest.PortalBeearer(user);
            List<ShipHead> list = new List<ShipHead>();
            string rtn = "";
            if (test)
            {
                //SqlDBHelper sh = new SqlDBHelper(Startup.ConnectionStringBuildRes());
                try
                {
                    var pData = new
                    {
                        ds = new
                        {
                            Shiphead = new[] { new { list } }
                        },
                      
                    };
                    var bo = EpicorRest.BoPost("Erp.BO.CustShipSvc", "GetNewShipHead", pData);
                    dynamic result;
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 401,
                            desc = bo.ResponseError.ToString()
                        };
                    }
                    else
                    {
                        rtn = bo.ResponseBody.ToString();
                       JObject jsonObject = JObject.Parse(rtn);
                        ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rtn);

                        // Mengambil data ShipHead
                        List<ShipHead> shipHeads = apiResponse.Parameters.Ds.ShipHead;

                        var number = GetLegalNumber(shipHeads);
                        var SetShip = shipHeads.Last();
                        var newShip = new
                        {
                            doValidateCreditHold = false,
                            doCheckShipDtl = false,
                            doLotValidation = false,
                            doCheckOrderComplete = false,
                            doPostUpdate = false,
                            doCheckCompliance = false,
                            ipShippedFlagChanged = false,
                            ipPackNum = 0,
                            ipBTCustNum = 0,
                            ds = new
                            {
                                
                                ShipHead = new[] { SetShip },
                                LegalNumGenOpts = new[] { number }
                            }
                        };
                        string preview = "";
                        var boPost = EpicorRest.BoPost("Erp.BO.CustShipSvc", "UpdateMaster", newShip);
                                if (boPost.ResponseStatus != System.Net.HttpStatusCode.OK)
                                {
                                    result = new
                                    {
                                        code = 401,
                                        desc = bo.ResponseError.ToString()
                                    };
                                }
                                else {
                                preview = boPost.ResponseBody.ToString(); 
                                }
                        ApiResponse newResponse = JsonConvert.DeserializeObject<ApiResponse>(preview);
                        list = newResponse.Parameters.Ds.ShipHead.ToList();
                        var ShipList = list.Last();
                        result = new
                        {
                            PackNum = ShipList.PackNum,
                            LegalNumber = ShipList.LegalNumber,
                            ShipDate = ShipList.ShipDate,
                            OrderNum_c = ShipList.OrderNum_C,
                            code = 200,
                            desc = "Ok"
                        };
                    }
                    return result;
                }
                catch (Exception Ex)
                {
                    dynamic result = new
                    {
                        code = 401,
                        desc = "Internal Error.",
                        msg = Ex
                    };
                    return result;
                }
            }
            else
            {
                dynamic result = new
                {
                    code = 1000,
                    desc = "Not Authorized"
                };
                return result;
            }
        }

        public dynamic GetLegalNumber(List<ShipHead> ship)
        {
            List<ShipHead> list = new List<ShipHead>();
            var newShip = new
            {

                ds = new
                {
                    ShipHead = new[] { ship[1] },

                },
                ipPackNum = 0
            };
            try
            {
                dynamic result;
                string rtn="";


                var boPost = EpicorRest.BoPost("Erp.BO.CustShipSvc", "GetLegalNumGenOpts", newShip);
                if (boPost.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        code = 401,
                        desc = boPost.ResponseError.ToString()
                    };
                }
                else
                {
                    rtn = boPost.ResponseBody.ToString();
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rtn);

                    // Mengambil data ShipHead
                    List<LegalNumGenOpts> shipHeads = apiResponse.Parameters.Ds.LegalNumGenOpts;
                    var legalNumber = shipHeads.FirstOrDefault();
                    result = legalNumber;
                }
               
                return result;
            }
            catch (Exception ex)
            {

                dynamic result = new
                {
                    code = 1000,
                    desc = ex.Message.ToString()
                };
                return result;
            }
        }

        [Route("GetByID")]
        [HttpPost]
        public dynamic GetByID([FromBody] List<Var> param)
        {
            var pack = param.FirstOrDefault();
            var packNum = pack.packNum;
         bool test = epicRest.PortalBeearer(user);
            MultiMap<string, string> dic = new MultiMap<string, string>();
            dic.Add("packNum", packNum); //Value harus string
            List<ShipHead> list = new List<ShipHead>();
            string rtn = "";
            if (test)
            {
                //SqlDBHelper sh = new SqlDBHelper(Startup.ConnectionStringBuildRes());
                try
                {
                   
                    var bo = EpicorRest.BoGet("Erp.BO.CustShipSvc", "GetByID", dic);
                    dynamic result;
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 401,
                            desc = bo.ResponseError.ToString()
                        };
                    }
                    else
                    {
                        rtn = bo.ResponseBody.ToString();
                        JObject jsonObject = JObject.Parse(rtn);
                        JArray jsonArray = (JArray)jsonObject["returnObj"]["ShipHead"];
                        rtn = jsonArray == null ? "" : jsonArray.ToString();
                        // Mengambil data ShipHead

                        if (rtn.StartsWith("[")) //jadi table
                        {
                            list = JsonConvert.DeserializeObject<List<ShipHead>>(rtn);
                        }
                        else
                        {
                            result = new
                            {
                                code = 401,
                                desc = "Can't Generate value ShipHead"
                            };
                        }
                        result = list;
                    }
                    return result;
                }
                catch (Exception Ex)
                {
                    dynamic result = new
                    {
                        code = 401,
                        desc = "Internal Error.",
                        msg = Ex
                    };
                    return result;
                }
            }
            else
            {
                dynamic result = new
                {
                    code = 1000,
                    desc = "Not Authorized"
                };
                return result;
            }
        }
        [Route("Shipment/SetOrderNum")]
        [HttpPost]
        public dynamic SetOrderNum([FromBody] OrderNumInfo param)
        {
            user.nik = param.nik;
            user.password = param.password;

         bool test = epicRest.PortalBeearer(user);
            List<ShipHead> list = new List<ShipHead>();
            string rtn = "";
            if (test)
            {
                //SqlDBHelper sh = new SqlDBHelper(Startup.ConnectionStringBuildRes());
                try
                {
                    var packNum = param.PackNum;
                    MultiMap<string, string> dic = new MultiMap<string, string>();
                    dic.Add("packNum", packNum.ToString()); //Value harus string
                    var boGet = EpicorRest.BoGet("Erp.BO.CustShipSvc", "GetByID", dic);
                    if (boGet.IsErrorResponse)
                    {
                        rtn = boGet.ResponseError; //Returns the Error Message

                        return new
                        {
                            code = 400,
                            desc = rtn
                        };
                    }
                    else
                    {
                        rtn = boGet.ResponseBody.ToString();
                        JObject jsonObject = JObject.Parse(rtn);
                        JArray jsonArray = (JArray)jsonObject["returnObj"]["ShipHead"];
                        rtn = jsonArray == null ? "" : jsonArray.ToString();
                        list = JsonConvert.DeserializeObject<List<ShipHead>>(rtn);
                    }
                   var sjorder = list.FirstOrDefault();
                    sjorder.OrderNum = param.OrderNum;
                    sjorder.RowMod = "U";

                    var pData = new
                    {
                        ds = new {
                            Shiphead = new[]{ sjorder }              
                        },
                           orderNum = param.OrderNum
                           
                    };
                    var bo = EpicorRest.BoPost("Erp.BO.CustShipSvc", "GetHeadOrderInfo", pData);
                    dynamic result;
                    List<ShipHead> newList = new List<ShipHead>();
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                        {
                             result = new
                            {
                                code = 401,
                                desc = bo.ResponseError.ToString()
                             };
                        }
                        else
                        {
                            rtn = bo.ResponseBody.ToString();
                        ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rtn);

                        // Mengambil data ShipHead
                        List<ShipHead> shipHeads = apiResponse.Parameters.Ds.ShipHead;
                        //List<OrderDetails> shipDetails = apiResponse.Parameters.Ds.ShipDtl;

                        var headList2 = shipHeads.Last();
                        var headList = shipHeads.Last();

                        var buildShip = new
                        {
                            orderNum = param.OrderNum,
                            iShipToCustNum = headList.CustNum
                        };
                        var buildshiplist = EpicorRest.BoPost("Erp.BO.CustShipSvc", "BuildShipToList", buildShip);
                        string shipToList = "";
                        if (buildshiplist.ResponseStatus != System.Net.HttpStatusCode.OK)
                        {
                            result = new
                            {
                                code = 401,
                                desc = buildshiplist.ResponseData.Description
                            };
                            return result;
                        }
                        else
                        {
                            rtn = buildshiplist.ResponseBody.ToString();
                            JObject parsedJson = JObject.Parse(rtn);
                             shipToList = (string)parsedJson["parameters"]["shipToList"];
                        }

                        var shipToNum = shipToList.Split("`");
                        //var dtlList = shipDetails.FirstOrDefault();
                        DateTime dateTime = DateTime.Now;
                        int timeMsSinceMidnight = (int)dateTime.TimeOfDay.TotalSeconds;
                        headList.RowMod = "U";
                        headList.ChangeDateTime = DateTime.Now;
                        headList.ChangeTime = timeMsSinceMidnight;
                        headList.ShipToNum = shipToNum[0];
                        headList.PackNum = packNum;
                        headList.OrderNum_C = Convert.ToInt32(param.OrderNum);

                        sjorder.RowMod = "";
                        sjorder.ChangeDateTime = DateTime.Now;
                        sjorder.OrderNum_C = Convert.ToInt32(param.OrderNum);
                        sjorder.ChangeTime = timeMsSinceMidnight;


                        //dtlList.RowMod = "U";

                        var newShip = new
                        {
                            doValidateCreditHold = false,
                            doCheckShipDtl = false,
                            doLotValidation = false,
                            doCheckOrderComplete = false,
                            doPostUpdate = false,
                            doCheckCompliance = false,
                            ipShippedFlagChanged = false,
                            ipPackNum = headList.PackNum,
                            ipBTCustNum = headList.CustNum,
                            ds = new
                            {
                                ShipHead = new[] { sjorder, headList },
                            }
                        };
                        string preview = "";
                        var boPost = EpicorRest.BoPost("Erp.BO.CustShipSvc", "UpdateMaster", newShip);
                        if (boPost.ResponseStatus != System.Net.HttpStatusCode.OK)
                        {
                            result = new
                            {
                                code = 401,
                                desc = boPost.ResponseData.Description
                            };
                            return result;
                        }
                        else
                        {
                            preview = boPost.ResponseBody.ToString();
                            ApiResponse preResult = JsonConvert.DeserializeObject<ApiResponse>(preview);
                            list = preResult.Parameters.Ds.ShipHead.ToList();
                            var ShipList = list.Last();
                            result = new
                            {
                                code = 200,
                                desc = "Ok"
                            };
                            return result;
                        }
                    }
                    return result;
                }
                catch (Exception Ex)
                {
                    dynamic result = new
                    {
                        code = 401,
                        desc = "Internal Error.",
                        msg = Ex
                    };
                    return result;
                }
            }
            else
            {
                dynamic result = new
                {
                    code = 401,
                    desc = "Not Authorized"
                };
                return result;
            }
        }
        [Route("Shipment/AddPackLine")]
        [HttpPost]
        public dynamic UpdateDetail([FromBody] OrderNumInfo param)
        {
            user.nik = param.nik;
            user.password = param.password;

         bool test = epicRest.PortalBeearer(user);
            List<ShipHead> listHead = new List<ShipHead>();
            ShipDtl listDtls = new ShipDtl();
            List<ShipDtl> listDtl = new List<ShipDtl>();
            dynamic result;
            string rtn = "";
            var packNum = param.PackNum;
            var PONum = param.PONum;

            if (test)
            {
                #region ShipHead
                listHead = GetByID(param.PackNum);
                listDtls.RowMod = "A";
                #endregion
                #region ShipDtl
                var pData = new
                {
                    ds = new
                    {
                     
                    },
                    orderNum = param.OrderNum.ToString(),
                    packNum = packNum
                };
                var bo = EpicorRest.BoPost("Erp.BO.CustShipSvc", "GetNewOrdrShipDtl", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        code = 401,
                        desc = bo.ResponseError.ToString()
                    };
                    return result;
                }
                else
                {
                    rtn = bo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rtn);
                    // Mengambil data ShipHead
                     listDtl = apiResponse.Parameters.Ds.ShipDtl;
                }
                #endregion
                var SetShipHead = listHead.Last();
                var SetShipDtl = listDtl.Last();
                SetShipHead.RowMod = "U";
                SetShipDtl.RowMod = "A";
                SetShipDtl.PONum = param.PONum;
                SetShipDtl.OrderNum = Convert.ToInt32(param.OrderNum);
                var ord = param.OrderNum.ToString();
                SetShipDtl.PONum = param.PONum;
                SetShipDtl.LineDesc = param.PartDesc;
                SetShipDtl.OrderLine = param.OrderLine;

                #region GetOrderInfo
                var orderData = new
                {
                    ds = new
                    {
                        ShipDtl = new[] { SetShipDtl },
                        ShipHead = new[] { new { } },
                    },
                    packLine = 0,
                    subsPart = param.PartNum.ToString(),
                    orderLine = param.OrderLine.ToString(),
                };

                var boOrderInfo = EpicorRest.BoPost("Erp.BO.CustShipSvc", "GetOrderLineInfo", orderData);
                if (boOrderInfo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        code = 401,
                        desc = boOrderInfo.ResponseError.ToString()
                    };
                    return result;
                }
                else
                {
                    rtn = boOrderInfo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rtn);
                    // Mengambil data ShipHead
                    listDtl = apiResponse.Parameters.Ds.ShipDtl;
                }
                #endregion

                SetShipDtl = listDtl.Last();
                SetShipDtl.OrderNum = Convert.ToInt32(param.OrderNum);
                SetShipDtl.OrderLine = param.OrderLine;
                SetShipDtl.OrderRelNum = param.OrderRel;
                SetShipDtl.PartNum = param.PartNum;
                SetShipDtl.RowMod = "A";
                SetShipDtl.PackLine = param.PackLine;
                SetShipDtl.CustNumCustID = SetShipHead.CustomerCustID;
                SetShipDtl.CustNumName = SetShipHead.ShipToCustName;
                SetShipDtl.OurInventoryShipQty = param.DisplayInvQty;

                #region QtyInfo
                var qtyData = new
                        {
                            ds = new
                            {
                                ShipDtl = new[] { SetShipDtl }
                            },
                            displayInvQty = param.DisplayInvQty,
                            ourJobShipQty = 0,
                            packLine = 0
                };
                var boQtyInfo = EpicorRest.BoPost("Erp.BO.CustShipSvc", "GetQtyInfo", qtyData);
                if (boQtyInfo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        code = 401,
                        desc = boQtyInfo.ResponseError.ToString()
                    };
                    return result;
                }
                else
                {
                    rtn = boQtyInfo.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rtn);
            // Mengambil data ShipHead
                    listDtl = apiResponse.Parameters.Ds.ShipDtl;
                }
                #endregion
                var SetShipHead2 = listHead.Last();
                SetShipHead2.RowMod = "";

                SetShipHead = listHead.Last();
                SetShipHead.RowMod = "U";

                var newSetShipDtl = listDtl.Last();
                newSetShipDtl.WarehouseCode = param.WarehouseCode;
                newSetShipDtl.WarehouseCodeDescription = param.WarehouseCodeDescription;
                newSetShipDtl.BinNum = param.BinNum;

                newSetShipDtl.LotNum = param.LotNum;
                newSetShipDtl.RowMod = "A";
                var newShipment = new
                {
                    
                    ds = new
                    {
                        ShipHead = new[] { SetShipHead },
                        ShipDtl = new[] { newSetShipDtl }
                    }
                };
                List<ShipHead> newHead = new List<ShipHead>();
                string preview = "";
                var boPost = EpicorRest.BoPost("Erp.BO.CustShipSvc", "CheckPCBinOutLocation", newShipment);
                if (boPost.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        code = 401,
                        desc = boPost.ResponseData.Messages.ToString()
                    };
                    return result;
                }
                else
                {
                    rtn = boPost.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rtn);
                    // Mengambil data ShipHead
                    listDtl = apiResponse.Parameters.Ds.ShipDtl;
                    newHead = apiResponse.Parameters.Ds.ShipHead;

                }
                var SetShipHead3 = listHead.Last();
                SetShipHead3.RowMod = "";

                SetShipHead = newHead.Last();
                SetShipHead.RowMod = "U";

                newSetShipDtl = listDtl.Last();
                newSetShipDtl.WarehouseCode = param.WarehouseCode;
                newSetShipDtl.WarehouseCodeDescription = param.WarehouseCodeDescription;
                newSetShipDtl.BinNum = param.BinNum;

                newSetShipDtl.LotNum = param.LotNum;
                newSetShipDtl.RowMod = "A";

                var newShip = new
                {
                    doValidateCreditHold = false,
                    doCheckShipDtl = true,
                    doLotValidation = true,
                    doCheckOrderComplete = false,
                    doPostUpdate = false,
                    doCheckCompliance = false,
                    ipShippedFlagChanged = false,
                    ipPackNum = packNum,
                    ipBTCustNum = SetShipHead.CustNum,
                    ds = new
                    {
                        ShipHead = new[] { SetShipHead3, SetShipHead },
                        ShipDtl = new[] { newSetShipDtl }
                    }
                };
                var boPosts = EpicorRest.BoPost("Erp.BO.CustShipSvc", "UpdateMaster", newShip);
                if (boPosts.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        code = 401,
                        desc = boPosts.ResponseError.ToString()
                    };
                    return result;
                }
                else
                {
                    preview = boPost.ResponseBody.ToString();
                }
                ApiResponse newResponse = JsonConvert.DeserializeObject<ApiResponse>(preview);
                listDtl = newResponse.Parameters.Ds.ShipDtl.ToList();
                var ShipList = listDtl.Last();
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
                    code = 500,
                    desc = "Not Authorzied, Server full"
                };
            }
            return result;
        }
        public dynamic GetPackLine(int PackNum, int PackLine)
        {
            List<ShipDtl> listData = new List<ShipDtl>();
            ShipDtl data = new ShipDtl();
            try
            {
                dynamic result;
                string Method = "ShipDtls('SAI'," + PackNum + "," + PackLine + ")";
                var bo = EpicorRest.BoGet("Erp.BO.CustShipSvc", Method);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        code = 401,
                        desc = bo.ResponseError.ToString()
                    };
                    return result;
                }
                else
                {
                    string rtn = "";
                    rtn = bo.ResponseBody.ToString();
                    var ds = new
                    {
                        ShipDtl = new[] { rtn }
                    };
                    //rtn = ds.ToString();
                    //JObject jsonObject = JObject.Parse(rtn);
                    //JArray jsonArray = (JArray)jsonObject["LaborDtl"];
                    data = JsonConvert.DeserializeObject<ShipDtl>(rtn);
                    listData = new List<ShipDtl> { data };
                    return listData;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Route("Shipment/UpdatePackLine")]
        [HttpPost]
        public dynamic UpdateChangeDetail([FromBody] OrderNumLine param)
        {
            user.nik = param.nik;
            user.password = param.password;
            try
            {

            
         bool test = epicRest.PortalBeearer(user);

            List<ShipHead> listHead = new List<ShipHead>();
            List<ShipHead> listHead2 = new List<ShipHead>();

            List<ShipDtl> listDtl = new List<ShipDtl>();
            List<ShipDtl> listDtl2 = new List<ShipDtl>();
            if (test)
            {
                    
             dynamic result;
            string rtn = "";
            var packNum = param.PackNum;
            var PONum = param.PONum;
            listHead = GetByID(param.PackNum);
            listHead2 = GetByID(param.PackNum);

            listDtl = GetPackLine(param.PackNum, param.PackLine);
            listDtl2 = GetPackLine(param.PackNum,param.PackLine);

            var SetShipDtl = listDtl.Last();
            var SetShipDtl2 = listDtl2.Last();

            var SetShipHead = listHead.Last();
            var SetShipHead2 = listHead2.Last();

            SetShipDtl.OrderNum = Convert.ToInt32(param.OrderNum);
            SetShipDtl.OrderLine = param.OrderLine;
            SetShipDtl.OrderRelNum = param.OrderRel;
            SetShipDtl.PartNum = param.PartNum;
            SetShipDtl.RowMod = "U";
            SetShipDtl.PackLine = param.PackLine;
            SetShipDtl.CustNumCustID = SetShipHead.CustomerCustID;
            SetShipDtl.CustNumName = SetShipHead.ShipToCustName;
            SetShipDtl.OurInventoryShipQty = param.DisplayInvQty;

            #region QtyInfo
            var qtyData = new
            {
                ds = new
                {
                    ShipDtl = new[] { SetShipDtl2, SetShipDtl }
                },
                displayInvQty = param.DisplayInvQty,
                ourJobShipQty = 0,
                packLine = SetShipDtl.PackLine,
            };
            var boQtyInfo = EpicorRest.BoPost("Erp.BO.CustShipSvc", "GetQtyInfo", qtyData);
            if (boQtyInfo.ResponseStatus != System.Net.HttpStatusCode.OK)
            {
                result = new
                {
                    code = 401,
                    desc = boQtyInfo.ResponseError.ToString()
                };
                return result;
            }
            else
            {
                rtn = boQtyInfo.ResponseBody.ToString();
                JObject jsonObject = JObject.Parse(rtn);
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rtn);
                // Mengambil data ShipHead
                listDtl = apiResponse.Parameters.Ds.ShipDtl;
            }
            #endregion
            SetShipHead2.RowMod = "";

            SetShipHead = listHead.Last();
            SetShipHead.RowMod = "U";

            var newSetShipDtl = listDtl.Last();
            newSetShipDtl.WarehouseCode = param.WarehouseCode;
            newSetShipDtl.WarehouseCodeDescription = param.WarehouseCodeDescription;
            newSetShipDtl.BinNum = param.BinNum;

            newSetShipDtl.LotNum = param.LotNum;
            newSetShipDtl.RowMod = "U";
            var newShipment = new
            {

                ds = new
                {
                    ShipHead = new[] { SetShipHead2,SetShipHead },
                    ShipDtl = new[] { SetShipDtl2, newSetShipDtl }
                }
            };
            List<ShipHead> newHead = new List<ShipHead>();
            string preview = "";
            var boPost = EpicorRest.BoPost("Erp.BO.CustShipSvc", "CheckPCBinOutLocation", newShipment);
            if (boPost.ResponseStatus != System.Net.HttpStatusCode.OK)
            {
                result = new
                {
                    code = 401,
                    desc = boPost.ResponseData.Messages.ToString()
                };
                return result;
            }
            else
            {
                rtn = boPost.ResponseBody.ToString();
                JObject jsonObject = JObject.Parse(rtn);
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(rtn);
                // Mengambil data ShipHead
                listDtl = apiResponse.Parameters.Ds.ShipDtl;
                newHead = apiResponse.Parameters.Ds.ShipHead;
            }
            var SetShipHead3 = listHead.Last();
            SetShipHead3.RowMod = "";

            SetShipHead = newHead.Last();
            SetShipHead.RowMod = "U";

            newSetShipDtl = listDtl.Last();
            newSetShipDtl.WarehouseCode = param.WarehouseCode;
            newSetShipDtl.WarehouseCodeDescription = param.WarehouseCodeDescription;
            newSetShipDtl.BinNum = param.BinNum;

            newSetShipDtl.LotNum = param.LotNum;
            newSetShipDtl.RowMod = "U";

            var newShip = new
            {
                doValidateCreditHold = false,
                doCheckShipDtl = true,
                doLotValidation = true,
                doCheckOrderComplete = false,
                doPostUpdate = false,
                doCheckCompliance = false,
                ipShippedFlagChanged = false,
                ipPackNum = packNum,
                ipBTCustNum = SetShipHead.CustNum,
                ds = new
                {
                    ShipHead = new[] { SetShipHead3, SetShipHead },
                    ShipDtl = new[] { SetShipDtl2, newSetShipDtl }
                }
            };
            var boPosts = EpicorRest.BoPost("Erp.BO.CustShipSvc", "UpdateMaster", newShip);
            if (boPosts.ResponseStatus != System.Net.HttpStatusCode.OK)
            {
                result = new
                {
                    code = 401,
                    desc = boPosts.ResponseError.ToString()
                };
                return result;
            }
            else
            {
                preview = boPost.ResponseBody.ToString();
            }
            ApiResponse newResponse = JsonConvert.DeserializeObject<ApiResponse>(preview);
            listDtl = newResponse.Parameters.Ds.ShipDtl.ToList();
            var ShipList = listDtl.Last();
            result = new
            {
                code = 200,
                desc = "Ok"
            };
            return result;
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
            catch (Exception ex)
            {
                dynamic result;
                result = new
                {
                    code = 401,
                    desc = "Internal Error."
                };
                return result;
            }
        }
        public dynamic GetByID(int packNum)
        {
            dynamic result;
            List<ShipHead> list = new List<ShipHead>();
            try
            {
                string rtn = "";
                MultiMap<string, string> dic = new MultiMap<string, string>();
                dic.Add("packNum", packNum.ToString()); //Value harus string
                var boGet = EpicorRest.BoGet("Erp.BO.CustShipSvc", "GetByID", dic);
                if (boGet.IsErrorResponse)
                {
                    result = new
                    {
                        code = 401,
                        desc = "Internal Error."
                    };
                    return result;
                }
                else
                {
                    rtn = boGet.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    JArray jsonArray = (JArray)jsonObject["returnObj"]["ShipHead"];
                    rtn = jsonArray == null ? "" : jsonArray.ToString();
                    list = JsonConvert.DeserializeObject<List<ShipHead>>(rtn);
                }

                return list;
            }
            catch (Exception)
            {

                result = new
                {
                    code = 401,
                    desc = "Internal Error."
                };
                return result;
            }
        }

        public dynamic GetByIDDetail(int packNum)
        {
            dynamic result;
            List<ShipDtl> list = new List<ShipDtl>();
            try
            {
                string rtn = "";
                MultiMap<string, string> dic = new MultiMap<string, string>();
                dic.Add("packNum", packNum.ToString()); //Value harus string
                var boGet = EpicorRest.BoGet("Erp.BO.CustShipSvc", "GetByID", dic);
                if (boGet.IsErrorResponse)
                {
                    result = new
                    {
                        code = 401,
                        desc = "Internal Error. GetByID"
                    };
                    return result;
                }
                else
                {
                    rtn = boGet.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    JArray jsonArray = (JArray)jsonObject["returnObj"]["ShipDtl"];
                    rtn = jsonArray == null ? "" : jsonArray.ToString();
                    list = JsonConvert.DeserializeObject<List<ShipDtl>>(rtn);
                }

                return list;
            }
            catch (Exception)
            {

                result = new
                {
                    code = 401,
                    desc = "Internal Error."
                };
                return result;
            }
        }
        [Route("Shipment/DeleteHead")]
        [HttpDelete]
        public dynamic DeleteHead([FromBody] PackNumLine param)
        {
            string rtn = "";
            user.nik = param.nik;
            user.password = param.password;
            bool test = epicRest.PortalBeearer(user);
            if (test)
            {
                dynamic result;
                try
                {
                    string Method = "CustShips('SAI'," + param.PackNum + ")";
                    var bo = EpicorRest.BoDelete("Erp.BO.CustShipSvc", Method);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 401,
                            desc = bo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        result = new
                        {
                            code = 200,
                            desc = "OK"
                        };
                        return result;
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                dynamic result = new
                {
                    code = 1000,
                    desc = "Not Authorized or Server Full"
                };
                return result;
            }

        }

        [Route("Shipment/DeleteLine")]
        [HttpDelete]
        public dynamic DeleteLine([FromBody] PackNumLine param)
        {
            user.nik = param.nik;
            user.password = param.password;

            string rtn = "";
            bool test = epicRest.PortalBeearer(user);
            if (test)
            {
                try
                {
                    dynamic result;
                    string Method = "ShipDtls('SAI',"+param.PackNum+","+param.PackLine+")";
                    var bo = EpicorRest.BoDelete("Erp.BO.CustShipSvc", Method);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 401,
                            desc = bo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        result = new
                        {
                            code = 200,
                            desc = "OK"
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
            }else{
                dynamic result = new
                {
                    code = 1000,
                    desc = "Not Authorized or Server Full"
                };
                return result;
            }

        }

        [Route("Shipment/ReadyToPrint")]
        [HttpPost]
        public dynamic ReadyToPrint([FromBody] ReadyPrint param)
        {
            user.nik = param.nik;
            user.password = param.password;
            string rtn = "";
         bool test = epicRest.PortalBeearer(user);
            if (test)
            {
                try
                {
                    var data = new
                    {
                        ReadyToPrint_c = param.ReadyToPrint_c
                    };
                    dynamic result;
                    string Method = "CustShips('SAI'," + param.PackNum + ")";
                    var bo = EpicorRest.BoPatch("Erp.BO.CustShipSvc", Method,data);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.NoContent)
                    {
                        result = new
                        {
                            code = 401,
                            desc = bo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        result = new
                        {
                            code = 200,
                            desc = "OK"
                        };
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    dynamic result;
                    result = new
                    {
                        code = 1000,
                        desc = ex.Message.ToString()
                    };
                    return result;
                }
            }
            else
            {
                dynamic result = new
                {
                    code = 1000,
                    desc = "Not Authorized or Server Full"
                };
                return result;
            }

        }

        [Route("Shipment/SetUpdateMaster")]
        [HttpPost]
        public dynamic SetUpdateMaster([FromBody] List<SetUpdateMaster> update)
        {
         bool test = epicRest.PortalBeearer(user);
            if (test)
            {
                dynamic result;

                var masters = update.FirstOrDefault();
                int ShipHeadNum = masters.PackNum;
                List<ShipHead> shipHead =  GetByID(ShipHeadNum);
                PrePartInfo prePart = new PrePartInfo();
                prePart.partNum = masters.PartNum.ToString();
                prePart.orderNum = masters.orderNum.ToString();
                prePart.orderLine = masters.orderLine.ToString();
                var CheckPart = PrePartInfo(prePart);
                if( CheckPart == false)
                {
                    return result = new
                    {
                        code = 400,
                        desc = "PartNumber tidak sama"
                    };
                }

                var orderNum = masters.orderNum;
                var shiprow = shipHead.FirstOrDefault();
                shiprow.RowMod = "U";
                masters.RowMod = "A";
                var pData = new
                {
                    doValidateCreditHold = false,
                    doCheckShipDtl = true,
                    doLotValidation = true,
                    doCheckOrderComplete = false,
                    doPostUpdate = false,
                    doCheckCompliance = false,
                    ipShippedFlagChanged = false,
                    ipPackNum = masters.PackNum,
                    //ipBTCustNum = masters.CustNum,
                    ds = new
                    {
                        Shiphead = new []{ shipHead },
                        ShipDtl = new[] { update },
                    },
                };

                string preview = "";
                var boPost = EpicorRest.BoPost("Erp.BO.CustShipSvc", "UpdateMaster", pData);
                if (boPost.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        code = boPost.ResponseStatus,
                        desc = boPost.ResponseData.ToString(),
                    };
                }
                else
                {
                    result = new
                    {
                        code = boPost.ResponseStatus,
                        desc = "Ok"
                    };
                }
                return result;
            }
            else
            {
                dynamic result = new
                {
                    code = 1000,
                    desc = "Not Authorized"
                };
                return result;
            }
        }
        public dynamic PrePartInfo(PrePartInfo prePartInfo)
        {

            try
            {
             bool test = epicRest.PortalBeearer(user);
                string rtn = "";
                if (test)
                {
                    var pData = new
                    {
                        partNum = "",
                        orderNum = prePartInfo.orderNum,
                        orderLine = prePartInfo.orderLine,
                    };
                    string preview = "";
                    var boPost = EpicorRest.BoPost("Erp.BO.CustShipSvc", "CheckPrePartInfo", pData);
                    rtn = boPost.ResponseBody.ToString();
                    JObject jsonObject = JObject.Parse(rtn);
                    string PartNum = (string)jsonObject["parameters"]["partNum"];

                    if(PartNum == prePartInfo.partNum)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}
    