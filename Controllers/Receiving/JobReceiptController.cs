using EPAPI.Repository.JobReceipt;
using EPAPI.Repository;

using Microsoft.AspNetCore.Mvc;
using EpicorRestAPI;
using Newtonsoft.Json;

namespace EPAPI.Controllers.Receiving
{
    public class JobReceiptController : Controller
    {
        public EpicRest epicRest = new EpicRest();
        User user = new User();

        public dynamic GetNew([FromBody] JobReceipt job)
        {
            try
            {
                user.nik = job.nik;
                user.password = job.password;
                dynamic result;
                List<PartTran> listData = new List<PartTran>();
                List<PartTran> listData2 = new List<PartTran>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var jo = new
                    {
                        pcJobNum = job.JobNum,
                    };
                    var bojo = EpicorRest.BoPost("Erp.BO.ReceiptsFromMfgSvc", "ValidateFromJob", jo);
                    if (bojo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            desc = bojo.ResponseError.ToString()
                        };
                        return result;
                    }
                    var pData = new
                    {
                        ds = new
                        {
                            PartTran = Array.Empty<object>()
                        },
                        pcJobNum = job.JobNum,
                        pcProcessID = "RcptToJobEntry",
                        pcTranType = "MFG-WIP",
                        piAssemblySeq = 0,
                    };
                    string preview = "";

                    var bo = EpicorRest.BoPost("Erp.BO.ReceiptsFromMfgSvc", "GetNewReceiptsFromMfgJobAsm", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            desc = bo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                    }
                    JobResponse newResponse = JsonConvert.DeserializeObject<JobResponse>(preview);
                    listData = newResponse.parameters.ds.PartTran.ToList();

                    return listData;
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
                    code = 400,
                    desc = ex.Message.ToString() + "on GetNew"
                };
                return result;
            }
        }
        

        [Route("JobRec/CreateNew")]
        [HttpPost]
        public dynamic GetNewJoBTransfer([FromBody] JobReceipt job)
        {
            try
            {
                user.nik = job.nik;
                user.password = job.password;
                dynamic result;
                List<PartTran> listData = new List<PartTran>();
                List<PartTran> listData2 = new List<PartTran>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    listData = GetNew(job);
                    listData2 = GetNew(job);

                    var dt = listData.Last();
                    var dt2 = listData2.Last();
                    dt2.ActTranQty = job.Qty;
                    dt2.RowMod = "U";
                    var pData = new
                    {
                        ds = new
                        {
                            PartTran = new[] { dt,dt2 }
                        },
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.ReceiptsFromMfgSvc", "OnChangeActTranQty", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            desc = bo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                    }
                    JobResponse newResponse = JsonConvert.DeserializeObject<JobResponse>(preview);
                    listData = newResponse.parameters.ds.PartTran.ToList();
                    var message = newResponse.parameters.pcMessage;
                    if (message != "") 
                    {
                        result = new
                        {
                            code = 201,
                            desc = "Ok",
                            message = message
                        };
                        return result;
                    }
                    else
                    {
                        listData2 = ChangeLotNum(listData, job);
                        var data = listData2.Last();
                        data.WareHouseCode = job.WarehouseCode;
                        data.WarehouseDescription = job.WarehouseDescription;

                        listData2 = new List<PartTran> { data };
                        listData2 = ChangeWareHouse(listData2, job);
                        listData2 = PreUpdate(listData2, job);
                        result = new
                        {
                            code = 200,
                            desc = "Ok",
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
        public dynamic ChangeLotNum(List<PartTran> tran, JobReceipt job)
        {
            try
            {
                user.nik = job.nik;
                user.password = job.password;
                dynamic result;
                List<PartTran> listData = tran;
                List<PartTran> listData2 = new List<PartTran>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var list = listData.Last();
                    var partran = tran.Last();
                    partran.RowMod = "U";
                    var pData = new
                    {
                        ds = new
                        {
                            PartTran = new[] { list, partran }
                        },
                        ProposedLotNumber = job.LotNum,
                        messageasked = false
                     
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.ReceiptsFromMfgSvc", "OnChangeLotNum", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            desc = bo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        JobResponse newResponse = JsonConvert.DeserializeObject<JobResponse>(preview);
                        listData2 = newResponse.parameters.ds.PartTran.ToList();
                        return listData2;
                    }
                }
                return listData2;

            }
            catch (Exception ex)
            {


                throw;
            }
        }
        public dynamic ChangeWareHouse(List<PartTran> tran, JobReceipt job)
        {
            try
            {
                user.nik = job.nik;
                user.password = job.password;
                dynamic result;
                List<PartTran> listData = tran;
                List<PartTran> listData2 = new List<PartTran>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var list = listData.FirstOrDefault();
                    var partran = tran.FirstOrDefault();
                    partran.RowMod = "U";
                    var pData = new
                    {
                        ds = new
                        {
                            PartTran = new[] { list, partran }
                        },
                        pcProcessID = "RcptToInvEntry",
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.ReceiptsFromMfgSvc", "OnChangeWareHouseCode", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            desc = bo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        JobResponse newResponse = JsonConvert.DeserializeObject<JobResponse>(preview);
                        listData2 = newResponse.parameters.ds.PartTran.ToList();
                        return listData2;
                    }
                }
                return listData2;

            }
            catch (Exception ex)
            {


                throw;
            }

        }

        public dynamic PreUpdate(List<PartTran> tran, JobReceipt job)
        {
            try
            {
                user.nik = job.nik;
                user.password = job.password;
                dynamic result;
                List<PartTran> listData = tran;
                List<PartTran> listData2 = new List<PartTran>();
                List<LegalNumGenOpts> listLegal = new List<LegalNumGenOpts>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var list = listData.Last();
                    var partran = tran.Last();
                    partran.RowMod = "U";
                    var pData = new
                    {
                        ds = new
                        {
                            PartTran = new[] { list, partran }
                        },
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.ReceiptsFromMfgSvc", "PreUpdate", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            desc = bo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        JobResponse newResponse = JsonConvert.DeserializeObject<JobResponse>(preview);
                        listData2 = newResponse.parameters.ds.PartTran.ToList();
                        listLegal = newResponse.parameters.ds.LegalNumGenOpts.ToList();
                        VerifySerialMatchAndPlanContract(listData2, listLegal, job);
                        return listData2;
                    }
                }
                        return listData2;


            }
            catch (Exception ex)
            {


                throw;
            }
        }
        public dynamic VerifySerialMatchAndPlanContract(List<PartTran> tran, List<LegalNumGenOpts> legal ,JobReceipt job)
        {
            try
            {
                user.nik = job.nik;
                user.password = job.password;
                dynamic result;
                List<PartTran> listData = tran;
                List<PartTran> listData2 = new List<PartTran>();
                List<LegalNumGenOpts> listLegal = new List<LegalNumGenOpts>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var list = listData.Last();
                    var partran = tran.Last();
                    var legalopt = legal.Last();
                    partran.RowMod = "U";
                    partran.Plant2 = "MfgSys";
                    partran.BinNum = job.BinNum;
                    legalopt.RowMod = "A";
                    var pData = new
                    {
                        ds = new
                        {
                            LegalNumGenOpts = new[] {legalopt},
                            PartTran = new[] { list, partran }
                        },
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.ReceiptsFromMfgSvc", "VerifySerialMatchAndPlanContract", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            desc = bo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        JobResponse newResponse = JsonConvert.DeserializeObject<JobResponse>(preview);
                        listData2 = newResponse.parameters.ds.PartTran.ToList();
                        listLegal = newResponse.parameters.ds.LegalNumGenOpts.ToList();
                        listData2 = ReceiveMfgPartToInventory(listData2, listLegal, job);
                        return listData2;
                    }
                }
                return listData2;

            }
            catch (Exception ex)
            {


                throw;
            }
        }

        public dynamic ReceiveMfgPartToInventory(List<PartTran> tran, List<LegalNumGenOpts> legal, JobReceipt job)
        {
            try
            {
                user.nik = job.nik;
                user.password = job.password;
                dynamic result;
                List<PartTran> listData = tran;
                List<PartTran> listData2 = new List<PartTran>();
                List<LegalNumGenOpts> listLegal = new List<LegalNumGenOpts>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var list = listData.Last();
                    var partran = tran.Last();
                    var legalopt = legal.Last();
                    partran.RowMod = "U";
                    legalopt.RowMod = "A";
                    var pData = new
                    {
                        ds = new
                        {
                            LegalNumGenOpts = new[] { legalopt },
                            PartTran = new[] { list, partran }
                        },
                        pcProcessID = "RcptToInvEntry",
                        pdSerialNoQty ="0",
                        plNegQtyAction = true   
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.ReceiptsFromMfgSvc", "ReceiveMfgPartToInventory", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            desc = bo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        JobResponse newResponse = JsonConvert.DeserializeObject<JobResponse>(preview);
                        listData2 = newResponse.parameters.ds.PartTran.ToList();
                        listLegal = newResponse.parameters.ds.LegalNumGenOpts.ToList();
                        return listData2;
                    }
                }
                return listData2;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Route("JobRec/BypassUpdate")]
        [HttpPost]
        public dynamic BypassUpdate ([FromBody] JobReceipt job)
        {
            try
            {
                user.nik = job.nik;
                user.password = job.password;
                dynamic result;
                List<PartTran> listData = new List<PartTran>();
                List<PartTran> listData2 = new List<PartTran>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    listData = GetNew(job);
                    listData2 = GetNew(job);

                    var dt = listData.Last();
                    var dt2 = listData2.Last();
                    dt2.ActTranQty = job.Qty;
                    dt2.RowMod = "U";
                    var pData = new
                    {
                        ds = new
                        {
                            PartTran = new[] { dt,dt2 }
                        },
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.ReceiptsFromMfgSvc", "OnChangeActTranQty", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            desc = bo.ResponseError.ToString()
                        };
                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                    }
                    JobResponse newResponse = JsonConvert.DeserializeObject<JobResponse>(preview);
                    listData = newResponse.parameters.ds.PartTran.ToList();
                    var message = newResponse.parameters.pcMessage;
                    
                        listData2 = ChangeLotNum(listData, job);
                        var data = listData2.Last();
                        data.WareHouseCode = job.WarehouseCode;
                        data.WarehouseDescription = job.WarehouseDescription;

                        listData2 = new List<PartTran> { data };
                        listData2 = ChangeWareHouse(listData2, job);
                        listData2 = PreUpdate(listData2, job);
                        result = new
                        {
                            code = 200,
                            desc = "Ok",
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

                throw;
            }
        }
    }

}
