using EPAPI.Repository.JobReceipt;
using EPAPI.Repository.ReceiptEntry;
using EpicorRestAPI;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace EPAPI.Repository.IssueMtl
{

    public class IssueMtlRepository
    {
        private EpicRest epicRest = new EpicRest();
        User user = new User();
        public dynamic GetJobMtl(List<IssueReturnJobAsmbl> rcv,bool isReturn)
        {
            try
            {
                List<IssueReturn> dt = new List<IssueReturn>();
                var jobAsmbl = rcv.FirstOrDefault();
                jobAsmbl.RowMod = "A";
                jobAsmbl.SysRowID = Guid.Empty;
                dynamic pData;
                dynamic result;
                if (!isReturn)
                {
                     pData = new
                    {
                        ds = new
                        {
                            SelectedJobAsmbl = rcv,
                        },
                        pCallProcess = "IssueMaterial",
                        pcMtlQueueRowID = "00000000-0000-0000-0000-000000000000",
                        pcTranType = "STK-MTL",
                    };
                }
                else {
                     pData = new
                    {
                        ds = new
                        {
                            SelectedJobAsmbl = rcv,
                        },
                        pCallProcess = "ReturnMaterial",
                        pcMtlQueueRowID = "00000000-0000-0000-0000-000000000000",
                        pcTranType = "MTL-STK",
                    };
                }
                
                var bo = EpicorRest.BoPost("Erp.BO.IssueReturnSvc", "GetNewJobAsmblMultiple", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 400,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                dt = newResponse.returnObj.IssueReturn.ToList();

                return new
                {
                    epi_code = 200,
                    epi_status = bo.ResponseError.ToString(),
                    IssueReturn = dt
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
        #region IssueMaterial
        public dynamic OnChangingToJobSeq(List<IssueReturn> issue, int JobMtlSeq)
        {
            try
            {
                List<IssueReturn> dt = new List<IssueReturn>();
                List<IssueReturn> dt2 = new List<IssueReturn>();
                
                dynamic result;
                var pData = new
                {
                    ds = new
                    {
                        IssueReturn = issue,
                    },
                    piToJobSeq = JobMtlSeq.ToString()
                };
                var bo = EpicorRest.BoPost("Erp.BO.IssueReturnSvc", "OnChangingToJobSeq", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 400,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.IssueReturn;

                return new
                {
                    epi_code = 200,
                    epi_status = bo.ResponseError.ToString(),
                    IssueReturn = dt
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
        public dynamic OnChangeToJobSeq(SelectedJobAsmbl rcv, List<IssueReturnJobAsmbl> mtl)
        {
            List<IssueReturn> dt = new List<IssueReturn>();
            List<IssueReturn> dt2 = new List<IssueReturn>();
            try
            {
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
                bool rtn = rcv.IsReturn;
                var GetJob = GetJobMtl(mtl, rtn);                                                                                    
                var GetJob2 = GetJobMtl(mtl, rtn);

                if (GetJob.epi_code != 200)
                {
                    return new
                    {
                        code = 200,
                        status = "Ok",
                        data = GetJob
                    };
                }
                
                dt = GetJob.IssueReturn as List<IssueReturn>;
                dt2 = GetJob2.IssueReturn as List<IssueReturn>;
                var listdt = dt.Last();
                var listdt2 = dt2.Last();
                listdt2.RowMod = "U";

                List<IssueReturn> send = [listdt, listdt2];

                var ChangingJobSeq = OnChangingToJobSeq(send, rcv.JobMtlSeq);
                if (ChangingJobSeq.epi_code != 200) {
                    return new
                    {
                        code = 200,
                        status = "Ok",
                        data = ChangingJobSeq
                    };
                }
                dt2 = ChangingJobSeq.IssueReturn as List<IssueReturn>;
                var updateJobSeqMt = dt2.Where(x => x.RowMod == "U").FirstOrDefault();
                updateJobSeqMt.ToJobSeq = rcv.JobMtlSeq;
                dynamic result;
                var pData = new
                {
                    ds = new
                    {
                        IssueReturn = dt2,
                    },
                    pCallProcess = "IssueMaterial"
                };
                var bo = EpicorRest.BoPost("Erp.BO.IssueReturnSvc", "OnChangeToJobSeq", pData);
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
                IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.IssueReturn.ToList();
                return new
                {
                    code = 200,
                    status = "OK",
                    data = new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        IssueReturn = dt.FirstOrDefault( x=> x.RowMod == "U")
                    }
                };
            }
            catch (Exception ex )
            {
                dynamic result = new
                {
                    code = 500,
                    status = ex.Message.ToString(),
                };
                return result;
            }
        }
        #endregion
        #region ReturnMaterial
        public dynamic OnChangingJobSeq(List<IssueReturn> issue, int JobMtlSeq)
        {
            try
            {
                List<IssueReturn> dt = new List<IssueReturn>();
                List<IssueReturn> dt2 = new List<IssueReturn>();

                dynamic result;
                var pData = new
                {
                    ds = new
                    {
                        IssueReturn = issue,
                    },
                    pCallProcess = "ReturnMaterial",
                    pcDirection = "From",
                    piJobSeq = JobMtlSeq.ToString()
                };
                var bo = EpicorRest.BoPost("Erp.BO.IssueReturnSvc", "OnChangingJobSeq", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 400,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.IssueReturn;

                return new
                {
                    epi_code = 200,
                    epi_status = bo.ResponseError.ToString(),
                    IssueReturn = dt
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
        public dynamic OnChangeFromJobSeq(SelectedJobAsmbl rcv, List<IssueReturnJobAsmbl> mtl)
        {
            List<IssueReturn> dt = new List<IssueReturn>();
            List<IssueReturn> dt2 = new List<IssueReturn>();
            try
            {
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
                bool rtn = rcv.IsReturn;

                var GetJob = GetJobMtl(mtl,rtn);
                var GetJob2 = GetJobMtl(mtl,rtn);

                if (GetJob.epi_code != 200)
                {
                    return new
                    {
                        code = 200,
                        status = "Ok",
                        data = GetJob
                    };
                }

                dt = GetJob.IssueReturn as List<IssueReturn>;
                dt2 = GetJob2.IssueReturn as List<IssueReturn>;
                var listdt = dt.Last();
                var listdt2 = dt2.Last();
                listdt2.RowMod = "U";

                List<IssueReturn> send = [listdt, listdt2];

                var ChangingJobSeq = OnChangingJobSeq(send, rcv.JobMtlSeq);
                if (ChangingJobSeq.epi_code != 200)
                {
                    return new
                    {
                        code = 200,
                        status = "Ok",
                        data = ChangingJobSeq
                    };
                }
                dt2 = ChangingJobSeq.IssueReturn as List<IssueReturn>;
                var updateJobSeqMt = dt2.Where(x => x.RowMod == "U").FirstOrDefault();
                updateJobSeqMt.ToJobSeq = rcv.JobMtlSeq;
                dynamic result;
                var pData = new
                {
                    ds = new
                    {
                        IssueReturn = dt2,
                    },
                    pCallProcess = "ReturnMaterial"
                };
                var bo = EpicorRest.BoPost("Erp.BO.IssueReturnSvc", "OnChangeFromJobSeq", pData);
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
                IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.IssueReturn.ToList();
                return new
                {
                    code = 200,
                    status = "OK",
                    data = new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        IssueReturn = dt.FirstOrDefault(x => x.RowMod == "U")
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
        #endregion
        public dynamic OnChangeTranQty(List<IssueReturn> issue, SelectedJobAsmbl rcv)
        {
            List<IssueReturn> dt = new List<IssueReturn>();
            try
            {

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
                var pData = new
                {
                    ds = new
                    {
                        IssueReturn = issue
                    },
                    pdTranQty = rcv.Qty
                };
                var bo = EpicorRest.BoPost("Erp.BO.IssueReturnSvc", "OnChangeTranQty", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {                        
                        epi_code = 400,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.IssueReturn.ToList();

                return new
                {
                    epi_code = 200,
                    epi_status = bo.ResponseError.ToString(),
                    IssueReturn = dt
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

        #region IssueMaterial Change Warehouse From
        public dynamic OnChangeFromWarehouse(List<IssueReturn> issue, SelectedJobAsmbl rcv)
        {
            List<IssueReturn> dt = new List<IssueReturn>();
            try
            {
                
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
                var whseDsU = issue.FirstOrDefault(x => x.RowMod == "U");
                var whseDs = issue.FirstOrDefault(x => x.RowMod != "U");
                whseDs.FromWarehouseCode = rcv.WarehouseCode;
                whseDs.FromWarehouseCodeDescription = rcv.WarehouseDescription;
                whseDs.FromBinNum = rcv.BinNum;
                whseDs.FromBinNumDescription = rcv.BinNumDescription;

                whseDsU.FromWarehouseCode = rcv.WarehouseCode;
                whseDsU.FromWarehouseCodeDescription = rcv.WarehouseDescription;
                whseDsU.FromBinNum = rcv.BinNum;
                whseDsU.FromBinNumDescription = rcv.BinNumDescription;
                whseDsU.FromWarehouseCode = rcv.WarehouseCode;
                var pData = new
                {
                    ds = new
                    {
                        IssueReturn = issue
                    },
                    pCallProcess = "IssueMaterial"
                };
                var bo = EpicorRest.BoPost("Erp.BO.IssueReturnSvc", "OnChangeFromWarehouse", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 400,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.IssueReturn.ToList();
                return new
                {
                    epi_code = 200,
                    epi_status = bo.ResponseError.ToString(),
                    IssueReturn = dt
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
        #endregion

        #region ReturnMaterial Change Warehouse To
        public dynamic OnChangeToWarehouse(List<IssueReturn> issue, SelectedJobAsmbl rcv)
        {
            List<IssueReturn> dt = new List<IssueReturn>();
            try
            {

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
                var whseDsU = issue.FirstOrDefault(x => x.RowMod == "U");
                var whseDs = issue.FirstOrDefault(x => x.RowMod != "U");
                whseDs.FromWarehouseCode = rcv.WarehouseCode;
                whseDs.FromWarehouseCodeDescription = rcv.WarehouseDescription;
                whseDs.FromBinNum = rcv.BinNum;
                whseDs.FromBinNumDescription = rcv.BinNumDescription;

                whseDsU.FromWarehouseCode = rcv.WarehouseCode;
                whseDsU.FromWarehouseCodeDescription = rcv.WarehouseDescription;
                whseDsU.FromBinNum = rcv.BinNum;
                whseDsU.FromBinNumDescription = rcv.BinNumDescription;
                whseDsU.FromWarehouseCode = rcv.WarehouseCode;
                var pData = new
                {
                    ds = new
                    {
                        IssueReturn = issue
                    },
                    pCallProcess = "ReturnMaterial"
                };
                var bo = EpicorRest.BoPost("Erp.BO.IssueReturnSvc", "OnChangeToWarehouse", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 400,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.IssueReturn.ToList();
                return new
                {
                    epi_code = 200,
                    epi_status = bo.ResponseError.ToString(),
                    IssueReturn = dt
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

        #endregion
        public dynamic PrePerformMaterialMovement(List<IssueReturn> issue, SelectedJobAsmbl rcv)
        {
            List<IssueReturn> dt = new List<IssueReturn>();
            List<LegalNumGenOpts> legal = [];
            try
            {
                bool IsReturn = rcv.IsReturn;
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
                var ChangeQtyTran = OnChangeTranQty(issue, rcv);
                if (ChangeQtyTran.epi_code != 200) {
                    return new
                    {
                       
                            epi_code = 404,
                            epi_status = ChangeQtyTran.epi_status
                    };
                }
                List<IssueReturn> issues = ChangeQtyTran.IssueReturn;
                dynamic ChangeWhse;
                if (!IsReturn) {
                     ChangeWhse = OnChangeFromWarehouse(issues, rcv);
                    if (ChangeWhse.epi_code != 200)
                    {
                        return new
                        {

                            epi_code = 404,
                            epi_status = ChangeWhse.epi_status
                        };
                    }
                }
                else
                {
                     ChangeWhse = OnChangeToWarehouse(issues, rcv);
                    if (ChangeWhse.epi_code != 200)
                    {
                        return new
                        {

                            epi_code = 404,
                            epi_status = ChangeWhse.epi_status
                        };
                    }
                }
                
                issues = ChangeWhse.IssueReturn;
                var whseDsU = issues.FirstOrDefault(x => x.RowMod == "U");
                //var whseDs = issues.FirstOrDefault(x => x.RowMod != "U");
                //whseDs.FromWarehouseCode = rcv.WarehouseCode;
                //whseDs.FromWarehouseCodeDescription = rcv.WarehouseDescription;
                //whseDs.FromBinNum = rcv.BinNum;
                //whseDs.FromBinNumDescription = rcv.BinNumDescription;
                whseDsU.FromWarehouseCode = rcv.WarehouseCode;
                whseDsU.FromWarehouseCodeDescription = rcv.WarehouseDescription;
                whseDsU.FromBinNum = rcv.BinNum;
                whseDsU.FromBinNumDescription = rcv.BinNumDescription;
                whseDsU.FromWarehouseCode = rcv.WarehouseCode;
                dynamic result;
                var pData = new
                {
                    ds = new
                    {
                        IssueReturn = issues,
                    },
                };
                var bo = EpicorRest.BoPost("Erp.BO.IssueReturnSvc", "PrePerformMaterialMovement", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                            epi_code = 400,
                            epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                IssueMtl newResponse =  JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.IssueReturn.ToList();
                legal = newResponse.parameters.ds.LegalNumGenOpts.ToList();
                var BinTests = MasterInventoryBinTests(dt, legal,rcv);
                if (BinTests.epi_code != 200)
                {
                    return new
                    {
                        
                            epi_code = 404,
                            epi_status = BinTests.epi_status
                    };
                }
                dt = BinTests.IssueReturn;
                legal = BinTests.LegalNumGenOpts;
                if (!rcv.IsReturn)
                {
                    var MaterialMovement = PerformMaterialMovement(dt, legal, rcv);
                    if (MaterialMovement.epi_code != 200)
                    {
                        return new
                        {
                            epi_code = 404,
                            epi_status = MaterialMovement.epi_status
                        };
                    }
                    dt = BinTests.IssueReturn;
                    legal = BinTests.LegalNumGenOpts;
                    return new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        IssueReturn = dt.FirstOrDefault(),
                        LegalNumGenOpts = legal
                    };
                }
                else {
                    var MaterialMovement = PerformMaterialMovement2(dt, legal, rcv);
                    if (MaterialMovement.epi_code != 200)
                    {
                        return new
                        {
                            epi_code = 404,
                            epi_status = MaterialMovement.epi_status
                        };
                    }
                    dt = BinTests.IssueReturn;
                    legal = BinTests.LegalNumGenOpts;
                    return new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        IssueReturn = dt.FirstOrDefault(),
                        LegalNumGenOpts = legal
                    };
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

        public dynamic MasterInventoryBinTests(List<IssueReturn> issue,List<LegalNumGenOpts> legal, SelectedJobAsmbl rcv)
        {
            List<IssueReturn> dt = new List<IssueReturn>();
            try
            {

                user.nik = rcv.nik;
                user.password = rcv.password;
                if (!epicRest.PortalBeearer(user))
                {
                    return new
                    {
                        epi_code = 404,
                        epi_status = "Not Authorized or Server Full"
                    };
                }
                dynamic result;
                var pData = new
                {
                    ds = new
                    {
                        IssueReturn = issue,
                        LegalNumGenOpts = legal
                    },
                };
                var bo = EpicorRest.BoPost("Erp.BO.IssueReturnSvc", "MasterInventoryBinTests", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 400,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.IssueReturn.ToList();
                legal = newResponse.parameters.ds.LegalNumGenOpts.ToList();
                return new
                {
                    epi_code = 200,
                    epi_status = bo.ResponseError.ToString(),
                    IssueReturn = dt,
                    LegalNumGenOpts = legal
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
        public dynamic PerformMaterialMovement(List<IssueReturn> issue, List<LegalNumGenOpts> legal, SelectedJobAsmbl rcv)
        {
            List<IssueReturn> dt = new List<IssueReturn>();
            try
            {

                user.nik = rcv.nik;
                user.password = rcv.password;
                if (!epicRest.PortalBeearer(user))
                {
                    return new
                    {
                        epi_code = 404,
                        epi_status = "Not Authorized or Server Full"
                    };
                }
                dynamic result;
                var pData = new
                {
                    ds = new
                    {
                        IssueReturn = issue,
                        LegalNumGenOpts = legal
                    },
                    plNegQtyAction = false
                };
                var bo = EpicorRest.BoPost("Erp.BO.IssueReturnSvc", "PerformMaterialMovement", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 400,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.IssueReturn.ToList();
                legal = newResponse.parameters.ds.LegalNumGenOpts.ToList();
                return new
                {
                    epi_code = 200,
                    epi_status = bo.ResponseError.ToString(),
                    IssueReturn = dt,
                    LegalNumGenOpts = legal
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
        public dynamic PerformMaterialMovement2(List<IssueReturn> issue, List<LegalNumGenOpts> legal, SelectedJobAsmbl rcv)
        {
            List<IssueReturn> dt = new List<IssueReturn>();
            try
            {

                user.nik = rcv.nik;
                user.password = rcv.password;
                if (!epicRest.PortalBeearer(user))
                {
                    return new
                    {
                        epi_code = 404,
                        epi_status = "Not Authorized or Server Full"
                    };
                }
                dynamic result;
                var pData = new
                {
                    ds = new
                    {
                        IssueReturn = issue,
                        LegalNumGenOpts = legal
                    },
                    plNegQtyAction = false
                };
                var bo = EpicorRest.BoPost("Erp.BO.IssueReturnSvc", "PerformMaterialMovement2", pData);
                if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                {
                    result = new
                    {
                        epi_code = 400,
                        epi_status = bo.ResponseError.ToString()
                    };
                    return result;
                }
                IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                dt = newResponse.parameters.ds.IssueReturn.ToList();
                legal = newResponse.parameters.ds.LegalNumGenOpts.ToList();
                return new
                {
                    epi_code = 200,
                    epi_status = bo.ResponseError.ToString(),
                    IssueReturn = dt,
                    LegalNumGenOpts = legal
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
    }
}
