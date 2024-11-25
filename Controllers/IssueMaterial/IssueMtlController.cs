using EPAPI.Repository;
using EPAPI.Repository.IssueMtl;
using EPAPI.Repository.ReceiptEntry;
using EpicorRestAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using EpicorRestSharedClasses;
using Microsoft.AspNetCore.Http.HttpResults;
namespace EPAPI.Controllers.IssueMaterial
{
    [Route("[controller]")]
    public class IssueMtlController : Controller
    {
        private EpicRest epicRest = new();
        User user = new User();
        private IssueMtlRepository issueMtl = new();

        [Route("GetJobMtl")]
        [HttpGet]
        public dynamic GetJobMtl([FromBody]SelectedJobAsmbl mtl) {
            try
            {
                dynamic result;
                user.nik = mtl.nik;
                user.password = mtl.password;
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
                List<IssueReturnJobAsmbl> dt = new List<IssueReturnJobAsmbl>();
                MultiMap<string, string> dic = new MultiMap<string, string>();
                dic.Add("whereClauseJobHead", "JobNum = '" + mtl.JobNum + "'");
                dic.Add("whereClauseJobAsmbl", "");
                dic.Add("pageSize", "0");
                dic.Add("absolutePage", "0");

                var bo = EpicorRest.BoGet("Erp.BO.IssueReturnSvc", "GetList", dic);
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
                    IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                    dt = newResponse.returnObj.IssueReturnJobAsmbl.ToList();
                    result = new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        data = new
                        {
                            preview = dt.ToList()
                        }
                    };
                }
                var _getJobIssue = issueMtl.OnChangeToJobSeq(mtl, dt);
                return _getJobIssue;
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

        [Route("MtlMovement")]
        [HttpPost]
        public dynamic MtlMovement([FromBody] SelectedJobAsmbl mtl)
        {
            try
            {
                dynamic result;
                user.nik = mtl.nik;
                user.password = mtl.password;
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
                bool IsReturn = mtl.IsReturn;

                List<IssueReturnJobAsmbl> dt = new List<IssueReturnJobAsmbl>();
                MultiMap<string, string> dic = new MultiMap<string, string>();
                dic.Add("whereClauseJobHead", "JobNum = '" + mtl.JobNum + "'");
                dic.Add("whereClauseJobAsmbl", "");
                dic.Add("pageSize", "0");
                dic.Add("absolutePage", "0");
                var bo = EpicorRest.BoGet("Erp.BO.IssueReturnSvc", "GetList", dic);
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
                    IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                    dt = newResponse.returnObj.IssueReturnJobAsmbl.ToList();
                    result = new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        data = new
                        {
                            preview = dt.ToList()
                        }
                    };
                }
                dynamic _getJobIssue;
                if (!IsReturn)
                {
                    _getJobIssue = issueMtl.OnChangeToJobSeq(mtl, dt);
                    if (_getJobIssue.data.epi_code != 200)
                    {
                        result = new
                        {
                            code = 200,
                            status = "OK",
                            data = new
                            {
                                epi_code = 400,
                                epi_status = _getJobIssue.data.epi_status
                            }
                        };
                        return result;
                    }
                }
                else
                {
                    _getJobIssue = issueMtl.OnChangeFromJobSeq(mtl, dt);
                    if (_getJobIssue.data.epi_code != 200)
                    {
                        result = new
                        {
                            code = 200,
                            status = "OK",
                            data = new
                            {
                                epi_code = 400,
                                epi_status = _getJobIssue.data.epi_status
                            }
                        };
                        return result;
                    }
                }

                IssueReturn issues = _getJobIssue.data.IssueReturn;
                issues.RowMod = "";
                IssueReturn issues2 = _getJobIssue.data.IssueReturn;
                IssueReturn newIssue = JsonConvert.DeserializeObject<IssueReturn>(JsonConvert.SerializeObject(issues));
                newIssue.RowMod = "U";
                List<IssueReturn> send = [issues, newIssue];

                var _premovement = issueMtl.PrePerformMaterialMovement(send, mtl);
                if (_premovement.epi_code != 200)
                {
                    result = new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 400,
                            epi_status = _premovement.epi_status
                        }
                    };
                    return result;
                }

                return new
                {
                    code = 200,
                    status = "OK",
                    data = _premovement
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

        [Route("ReturnIssue")]
        [HttpPost]
        public dynamic ReturnIssue([FromBody] SelectedJobAsmbl mtl)
        {
            try
            {
                dynamic result;
                user.nik = mtl.nik;
                user.password = mtl.password;
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
                bool IsReturn = mtl.IsReturn;

                List<IssueReturnJobAsmbl> dt = new List<IssueReturnJobAsmbl>();
                MultiMap<string, string> dic = new MultiMap<string, string>();
                dic.Add("whereClauseJobHead", "JobNum = '" + mtl.JobNum + "'");
                dic.Add("whereClauseJobAsmbl", "");
                dic.Add("pageSize", "0");
                dic.Add("absolutePage", "0");
                var bo = EpicorRest.BoGet("Erp.BO.IssueReturnSvc", "GetList", dic);
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
                    IssueMtl newResponse = JsonConvert.DeserializeObject<IssueMtl>(bo.ResponseBody.ToString());
                    dt = newResponse.returnObj.IssueReturnJobAsmbl.ToList();
                    result = new
                    {
                        epi_code = 200,
                        epi_status = bo.ResponseError.ToString(),
                        data = new
                        {
                            preview = dt.ToList()
                        }
                    };
                }
                dynamic _getJobIssue;
                if (!IsReturn) { 
                     _getJobIssue = issueMtl.OnChangeToJobSeq(mtl, dt);
                    if (_getJobIssue.data.epi_code != 200)
                    {
                        result = new
                        {
                            code = 200,
                            status = "OK",
                            data = new
                            {
                                epi_code = 400,
                                epi_status = _getJobIssue.data.epi_status
                            }
                        };
                        return result;
                    }
                }
                else
                {
                    _getJobIssue = issueMtl.OnChangeFromJobSeq(mtl, dt);
                    if (_getJobIssue.data.epi_code != 200)
                    {
                        result = new
                        {
                            code = 200,
                            status = "OK",
                            data = new
                            {
                                epi_code = 400,
                                epi_status = _getJobIssue.data.epi_status
                            }
                        };
                        return result;
                    }
                }

                IssueReturn issues = _getJobIssue.data.IssueReturn;
                issues.RowMod = "";
                IssueReturn issues2 = _getJobIssue.data.IssueReturn;
                IssueReturn newIssue = JsonConvert.DeserializeObject<IssueReturn>(JsonConvert.SerializeObject(issues));
                newIssue.RowMod = "U";
                List<IssueReturn> send = [issues, newIssue];

                var _premovement = issueMtl.PrePerformMaterialMovement(send, mtl);
                if (_premovement.epi_code != 200)
                {
                    result = new
                    {
                        code = 200,
                        status = "OK",
                        data = new
                        {
                            epi_code = 400,
                            epi_status = _premovement.epi_status
                        }
                    };
                    return result;
                }

                return new
                {
                    code = 200,
                    status = "OK",
                    data = _premovement
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
