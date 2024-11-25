using EPAPI.Repository;
using EpicorRestAPI;
using EpicorRestSharedClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace EPAPI.Controllers.TimeEntry
{
    public class TimeEntryController : Controller
    {
        public EpicRest epicRest = new();

        TimeEntries timeEntries = new();
        User user = new();

        public dynamic GetNewLaborHead(TimeEntries entries)
        {
            try
            {
                user.nik = entries.nik;
                user.password = entries.password;
                dynamic result;
                List<LaborHed> listData = new List<LaborHed>();
                List<LaborHed> listData2 = new List<LaborHed>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    #region GetData
                    //listData = GetData(payload.ReqNum.ToString());
                    //listData2 = GetData(payload.ReqNum.ToString());
                    #endregion

                    var pData = new
                    {
                        ds = new
                        {

                        },
                        EmployeeNum = entries.EmployeeNum,
                        ShopFloor = false,
                        payrollDate = entries.StartDate,
                    };
                    string preview = "";

                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "GetNewLaborHed1", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                    }
                    LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                    listData = newResponse.parameters.ds.LaborHed.ToList();

                    return listData;
                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + "GetLaborHead"
                };
                return result;
            }
        }
        

        [Route("Labor/CreateNew")]
        [HttpPost]
        public dynamic UpdateLaborHead([FromBody] TimeEntries entries)
        {
            try
            {
                user.nik = entries.nik;
                user.password = entries.password;
                dynamic result;
                List<LaborHed> listData = new List<LaborHed>();
                List<LaborHed> listData2 = new List<LaborHed>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    listData = GetNewLaborHead(entries);
                    var dt = listData.Last();
                    var pData = new
                    {
                        ds = new
                        {
                            LaborHed = new[] { dt }
                        },

                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "Update", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                    }
                    LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                    listData = newResponse.parameters.ds.LaborHed.ToList();
                    var SqNum = listData.FirstOrDefault();
                    result = new
                    {
                        code = 200,
                       status = "Ok",
                        data = new
                        {
                                LaborHedSeq = SqNum.LaborHedSeq
                        }
                    };
                    return result;

                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
                    };
                    return result;

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public dynamic GetHeadByID(int? LaborSeq)
        {
            try
            {
                dynamic result;
                List<LaborHed> listData = new List<LaborHed>();
                List<LaborHed> listData2 = new List<LaborHed>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    string rtn = "";
                    MultiMap<string, string> dic = new MultiMap<string, string>();
                    var seq = LaborSeq.ToString();
                    dic.Add("laborHedSeq", seq); //Value harus string
                    var boGet = EpicorRest.BoGet("Erp.BO.LaborSvc", "GetByID", dic);
                    if (boGet.IsErrorResponse)
                    {
                        result = new
                        {
                            code = 400,
                           status = boGet.ResponseError.ToString(),
                        };

                        return result;
                    }
                    else
                    {
                        rtn = boGet.ResponseBody.ToString();
                        JObject jsonObject = JObject.Parse(rtn);
                        JArray jsonArray = (JArray)jsonObject["returnObj"]["LaborHed"];
                        rtn = jsonArray == null ? "" : jsonArray.ToString();
                        listData = JsonConvert.DeserializeObject<List<LaborHed>>(rtn);
                        return listData;
                    }
                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + "GetLaborHead"
                };
                return result;
            }
        }
        [Route("Labor/ChangeShift")]
        [HttpPost]
        public dynamic ChangeShift([FromBody] LaborHedDs entries)
        {
            try
            {
                user.nik = entries.nik;
                user.password = entries.password;
                dynamic result;
                List<LaborHed> listData = new List<LaborHed>();
                List<LaborHed> listData2 = new List<LaborHed>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    #region GetData
                    listData = GetHeadByID(entries.LaborHedSeq);
                    listData2 = GetHeadByID(entries.LaborHedSeq);
                    #endregion
                    var SetLabor = listData.Last();
                    SetLabor.RowMod = "";
                    var SetLabor2 = listData2.Last();
                    SetLabor2.RowMod = "U";
                    var pData = new
                    {
                        ds = new
                        {
                            LaborHed = new[] { SetLabor, SetLabor2 }
                        },
                        shift = entries.Shift.ToString()
                    };
                    string preview = "";

                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "DefaultShift", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        
                        };
                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborHed.ToList();
                        DirectUpdate(listData.Last());
                        var resultData = listData.Last();
                        result = new
                        {
                            code = 200,
                            status = "Ok",
                            data = new
                            {
                                employeeNum = resultData.EmployeeNum,
                                workDate = resultData.PayrollDate,
                                payHour = resultData.PayHours,
                                actualClockinDate = resultData.ActualClockinDate,
                                clockInDate = resultData.ClockInDate,
                                actualClockInTime = resultData.ActualClockInTime,
                                clockInTime = resultData.ClockInTime,
                                actualClockOutTime = resultData.ActualClockOutTime,
                                clockOutTime = resultData.ClockOutTime,
                                actLunchOutTime = resultData.ActLunchOutTime,
                                lunchOutTime = resultData.LunchOutTime,
                                actLunchInTime = resultData.ActLunchInTime,
                                lunchInTime = resultData.LunchInTime
                            }
                        };

                        return result;
                    }
                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + "ChangeShift"
                };
                return result;
            }
        }
        public dynamic DirectUpdate(LaborHed ds)
        {
            try
            {
                List<LaborHed> listData = new List<LaborHed>();

                dynamic result;
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    ds.RowMod = "U";
                    var pData = new
                    {
                        ds = new
                        {
                            LaborHed = new[] { ds }
                        },
                    };
                    string preview = "";

                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "Update", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborHed.ToList();

                        return listData;
                    }

                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + "DirectUpdate"
                };
                return result;
            }
        }

        [Route("Labor/UpdateHeader")]
        [HttpPost]
        public dynamic UpdateHed([FromBody] LaborHedDs entries)
        {
            try
            {
                user.nik = entries.nik;
                user.password = entries.password;
                dynamic result;
                List<LaborHed> listData = new List<LaborHed>();
                List<LaborHed> listData2 = new List<LaborHed>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    listData = GetHeadByID(entries.LaborHedSeq);
                    var dt = listData.Last();
                    dt.Shift = entries.Shift;
                    dt.ActualClockInTime = ConvertTimeToDecimal(entries.ActualClockInTime);
                    dt.ClockInTime = ConvertTimeToDecimal(entries.ClockInTime);
                    dt.ActualClockOutTime = ConvertTimeToDecimal(entries.ActualClockOutTime);
                    dt.ClockOutTime = ConvertTimeToDecimal(entries.ClockOutTime);
                    dt.ActLunchOutTime = ConvertTimeToDecimal(entries.ActLunchOutTime);
                    dt.LunchOutTime = ConvertTimeToDecimal(entries.LunchOutTime);
                    dt.ActLunchInTime = ConvertTimeToDecimal(entries.ActLunchInTime);
                    dt.LunchInTime = ConvertTimeToDecimal(entries.LunchInTime);
                    dt.PayHours = entries.PayHours;
                    dt.RowMod = "U";

                    var pData = new
                    {
                        ds = new
                        {
                            LaborHed = new[] { dt }
                        },

                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "Update", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                    }
                    LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                    listData = newResponse.parameters.ds.LaborHed.ToList();
                    var SqNum = listData.FirstOrDefault();
                    result = new
                    {
                        code = 200,
                       status = "Ok",
                        data = new
                        { 
                                LaborHedSeq = SqNum.LaborHedSeq
                        }
                    };
                    return result;

                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
                    };
                    return result;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("Labor/GeNewtLaborDtl")]
        [HttpPost]
        public dynamic GeNewtLaborDtl([FromBody] GetLabor entries)
        {
            try
            {
                user.nik = entries.nik;
                user.password = entries.password;
                dynamic result;
                List<LaborDtl> listData = new List<LaborDtl>();
                List<LaborDtl> listData2 = new List<LaborDtl>();

                List<LaborHed> list = new List<LaborHed>();
                var LaborTypePseudo = entries.LaborTypePseudo;
                bool test = epicRest.PortalBeearer(user);
                List<LaborDtl> listing = new List<LaborDtl>();
                if (test)
                {
                    var pData = new
                    {
                        ds = new
                        {
                            LaborHed = new[] { new { } },
                        },
                        ipClockInDate = entries.date,
                        ipClockInTime = 0,
                        ipClockOutDate = entries.date,
                        ipClockOutTime = 0,
                        ipLaborHedSeq = entries.LaborHedSeq
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "GetNewLaborDtlWithHdr", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();
                        list = newResponse.parameters.ds.LaborHed.ToList();

                        if (list != null)
                        {
                            var laborHed = list.Last();
                        }
                        if(LaborTypePseudo != "I")
                        {
                            listData = DefaultJobNum(listData, entries.JobNum, "");
                            listData = LaborDtlOprSeq(listData, entries.OpSeq, "");
                            listing = DirectUpdateDtl(listData, "");
                        }
                        else
                        {
                            listing = DirectUpdateDtl(listData, "");
                        }
                    }
                    if(listing != null)
                    {
                        var detectCopart = listing.Last();
                        if(detectCopart.EnableLaborQty == false)
                        {
                            result = new
                            {
                                code = 200,
                                status = "Ok",
                                data = new{
                                LaborHeadSeq = detectCopart.LaborHedSeq,
                                LaborDtlSeq = detectCopart.LaborDtlSeq,
                                IsCopart = true
                                }
                            };
                            return result;
                        }
                        else
                        {
                            result = new
                            {
                                code = 200,
                                status = "Ok",
                                data = new
                                {
                                    LaborHeadSeq = detectCopart.LaborHedSeq,
                                    LaborDtlSeq = detectCopart.LaborDtlSeq,
                                    IsCopart = false
                                }
                            };
                            return result;
                        }
                    }
                    else
                    {
                        result = new
                        {
                            code = 400,
                            status = "Error, Failed to Update Detail",
                            data = Array.Empty<object>()
                        };
                    }
                    result = new
                    {
                        code = 200,
                       status = "Ok",
                        data = Array.Empty<object>()
                    };
                    return result;
                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + " GeNewtLaborDtl"
                };
                return result;
            }
        }
        public dynamic DefaultJobNum(List<LaborDtl> labor, string JobNum, string RowMod)
        {
            try
            {
                dynamic result;
                List<LaborDtl> listData = new List<LaborDtl>();
                List<LaborDtl> listData2 = new List<LaborDtl>();
                bool test = epicRest.PortalBeearer(user);
                RowMod = RowMod == "" ? "A" : RowMod;
                if (test)
                {
                    var lab = labor.FirstOrDefault();
                    lab.RowMod = RowMod;
                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { lab }
                        },
                        jobNum = JobNum
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "DefaultJobNum", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();
                        var laborDtl = listData.Last();
                        LaborRateCalc(listData, "");
                        return listData;
                    }
                }
                return listData;
            }
            catch (Exception ex)
            {

                dynamic result;
                result = new
                {
                    code = 400,
                   status = ex.Message.ToString() + "DefaultJobNum"
                };
                return result;
            }
        }
        public dynamic LaborDtlOprSeq(List<LaborDtl> labor, string op, string RowMod)
        {
            try
            {
                dynamic result;
                List<LaborDtl> listData = new List<LaborDtl>();
                List<LaborDtl> listData2 = new List<LaborDtl>();
                bool test = epicRest.PortalBeearer(user);
                RowMod = RowMod == "" ? "A" : RowMod;
                if (test)
                {
                    var lab = labor.FirstOrDefault();
                    lab.RowMod = RowMod;
                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { lab }
                        },
                        oprSeq = op
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "ChangeLaborDtlOprSeq", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();
                        var laborDtl = listData.Last();
                        LaborRateCalc(listData, "");
                        return listData;
                    }
                }
                return listData;
            }
            catch (Exception ex)
            {

                dynamic result;
                result = new
                {
                    code = 400,
                   status = ex.Message.ToString() + "LaborDtlOprSeq"
                };
                return result;
            }
        }
        public dynamic CheckWarnings(List<LaborDtl> labor, string RowMod)
        {
            try
            {
                dynamic result;
                List<LaborDtl> listData = new List<LaborDtl>();
                List<LaborDtl> listData2 = new List<LaborDtl>();
                bool test = epicRest.PortalBeearer(user);
                RowMod = RowMod == "" ? "A" : RowMod;
                if (test)
                {
                    var lab = labor.FirstOrDefault();
                    lab.RowMod = RowMod;
                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { lab }
                        }
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "CheckWarnings", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();
                        var laborDtl = listData.Last();
                        LaborRateCalc(listData, "");
                        return listData;
                    }
                }
                return listData;
            }
            catch (Exception ex)
            {

                dynamic result;
                result = new
                {
                    code = 400,
                   status = ex.Message.ToString() + "LaborDtlOprSeq"
                };
                return result;
            }
        }
        public dynamic DirectUpdateDtl(List<LaborDtl> ds, string RowMod)
        {
            try
            {
                List<LaborDtl> listData = new List<LaborDtl>();

                dynamic result;
                bool test = epicRest.PortalBeearer(user);
                RowMod = RowMod == "" ? "A" : RowMod;
                if (test)
                {
                    var param = ds.Last();
                    param.RowMod = RowMod;
                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { param }
                        },
                    };
                    string preview = "";
                    CheckWarnings(ds, RowMod);
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "Update", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();
                        
                        return listData;
                    }

                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + " DirectUpdate"
                };
                return result;
            }
        }
        public dynamic DefaultLaborType(List<LaborDtl> labor, string laborType, string RowMod)
        {
            try
            {
                dynamic result;
                List<LaborDtl> listData = new List<LaborDtl>();
                List<LaborDtl> listData2 = new List<LaborDtl>();
                bool test = epicRest.PortalBeearer(user);
                RowMod = RowMod == "" ? "A" : RowMod;
                if (test)
                {
                    var lab = labor.FirstOrDefault();
                    lab.RowMod = RowMod;
                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { lab }
                        },
                        ipLaborType = laborType
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "DefaultLaborType", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();
                        var laborDtl = listData.Last();
                        ChangeLaborType(listData, "");
                        return listData;
                    }
                }
                return listData;
            }catch(Exception ex)
            {
                dynamic result;
                result = new
                {
                    code = 400,
                   status = ex.Message.ToString() + " DefaultLaborType"
                };
                return result;
            }
            }
        public dynamic ChangeLaborType(List<LaborDtl> labor,string RowMod)
        {
            try
            {
                dynamic result;
                List<LaborDtl> listData = new List<LaborDtl>();
                List<LaborDtl> listData2 = new List<LaborDtl>();
                bool test = epicRest.PortalBeearer(user);
                RowMod = RowMod == "" ? "A" : RowMod;
                if (test)
                {
                    var lab = labor.FirstOrDefault();
                    lab.RowMod = RowMod;
                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { lab }
                        },
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "ChangeLaborType", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();
                        var laborDtl = listData.Last();
                        LaborRateCalc(listData, "");
                        return listData;
                    }
                }
                return listData;
            }
            catch (Exception ex)
            {
                dynamic result;
                result = new
                {
                    code = 400,
                   status = ex.Message.ToString() + " DefaultLaborType"
                };
                return result;
            }
        }

        public dynamic InDirectUpdateDtl(List<LaborDtl> ds, string RowMod, GetLabor entries)
        {
            try
            {
                List<LaborDtl> listData = new List<LaborDtl>();

                dynamic result;
                bool test = epicRest.PortalBeearer(user);
                RowMod = RowMod == "" ? "A" : RowMod;
                if (test)
                {
                    var param = ds.Last();
                    param.RowMod = RowMod;
                    param.LaborTypePseudo = entries.LaborTypePseudo;

                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { param }
                        },
                    };
                    string preview = "";
                    CheckWarnings(ds, RowMod);
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "Update", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();

                        return listData;
                    }

                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + "DirectUpdate"
                };
                return result;
            }
        }
        public dynamic LaborRateCalc(List<LaborDtl> labors, string RowMod)
        {
            try
            {
                dynamic result;
                List<LaborDtl> listData = new List<LaborDtl>();
                List<LaborDtl> listData2 = new List<LaborDtl>();
                RowMod = RowMod == "" ? "A" : RowMod;

                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var lab = labors.FirstOrDefault();
                    lab.RowMod = "A";

                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { lab }
                        },
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "LaborRateCalc", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();
                        var laborDtl = listData.Last();
                        return listData;
                    }
                }
                return listData;
            }
            catch (Exception ex)
            {

                dynamic result;
                result = new
                {
                    code = 400,
                   status = ex.Message.ToString() + "DefaultJobNum"
                };
                return result;
            }
        }
        [Route("Labor/GetByID")]
        [HttpPost]
        public dynamic GetByID([FromBody] GetLabor labor)
        {
            try
            {
                user.nik = labor.nik;
                user.password = labor.password;
                dynamic result;
                List<LaborHed> listHead = new List<LaborHed>();
                List<LaborDtl> listData = new List<LaborDtl>();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    string rtn = "";
                    MultiMap<string, string> dic = new MultiMap<string, string>();
                    dic.Add("laborHedSeq", labor.LaborHedSeq.ToString()); //Value harus string
                    var boGet = EpicorRest.BoGet("Erp.BO.LaborSvc", "GetByID", dic);
                    if (boGet.IsErrorResponse)
                    {
                        result = new
                        {
                            code = 400,
                           status = boGet.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        rtn = boGet.ResponseBody.ToString();
                        JObject jsonObject = JObject.Parse(rtn);
                        JArray jsonArray = (JArray)jsonObject["returnObj"]["LaborHed"];
                        JArray jsonDtl = (JArray)jsonObject["returnObj"]["LaborDtl"];
                        rtn = jsonArray == null ? "" : jsonArray.ToString();
                        string dtl = jsonDtl == null ? "" : jsonDtl.ToString();
                        listHead = JsonConvert.DeserializeObject<List<LaborHed>>(rtn);
                        listData = JsonConvert.DeserializeObject<List<LaborDtl>>(dtl);
                        result = new{ 
                            code = 200,
                            status = "OK",
                            data = new
                            {
                              
                                LaborHead = new [] {listHead },
                                LaborDtl = new [] { listData }
                            }
                         };
                    }
                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
                    };
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {

                dynamic result;
                result = new
                {
                    code = 400,
                   status = ex.Message.ToString() + "GetLaborDtl"
                };
                return result;
            }
        }
        public dynamic GetDetailID(int LaborSeq, int LaborSeqDtl)
        {
            try
            {
              
                dynamic result;
                List<LaborHed> listHead = new List<LaborHed>();
                List<LaborDtl> listData = new List<LaborDtl>();
                LaborDtl data = new LaborDtl();
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    string rtn = "";
                    string method = "LaborDtls('SAI'," + LaborSeq + "," + LaborSeqDtl + ")";
                    var boGet = EpicorRest.BoGet("Erp.BO.LaborSvc", method);
                    if (boGet.IsErrorResponse)
                    {
                        result = new
                        {
                            code = 400,
                           status = boGet.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        rtn = boGet.ResponseBody.ToString();
                        var ds = new
                        {
                            LaborDtl = new[] { rtn }  
                        };
                        //rtn = ds.ToString();
                        //JObject jsonObject = JObject.Parse(rtn);
                        //JArray jsonArray = (JArray)jsonObject["LaborDtl"];
                        data = JsonConvert.DeserializeObject<LaborDtl>(rtn);
                        listData = new List<LaborDtl> { data }; 
                        return listData;
                    }
                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + "GetLaborDtl"
                };
                return result;
            }
        }

        [Route("Labor/ChangeLaborTime")]
        [HttpPost]
        public dynamic ChangeLaborTime([FromBody] GetLabor labor)
        {
            try
            {
                List<LaborDtl> listData = new List<LaborDtl>();
                List<LaborDtl> listData2 = new List<LaborDtl>();

                int HeadNum = labor.LaborHedSeq;
                int DtlNum = labor.LaborDtlSeq;
                string RowMod = labor.RowMod;
                dynamic result;
                var ClockinTime = ConvertTimeToDecimal(labor.ClockinTime);
                var ClockOutTime = ConvertTimeToDecimal(labor.ClockOutTime);

                user.nik = labor.nik;
                user.password = labor.password;
                bool test = epicRest.PortalBeearer(user);
                RowMod = RowMod == "" ? "A" : RowMod;
                if (test)
                {
                    listData = GetDetailID(HeadNum, DtlNum);
                    listData2 = GetDetailID(HeadNum, DtlNum);

                    var param = listData.FirstOrDefault();
                    var param2 = listData2.FirstOrDefault();

                    param.RowMod = "U";
                    param.ClockinTime = ClockinTime;
                    param.ClockOutTime = ClockOutTime;
                    param.Shift = labor.Shift;
                    param.ShiftDescription = labor.ShiftDescription;
                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { param2,param }
                        },
                        fieldName = "ClockOutTime",
                        timeValue = ClockOutTime
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "ChangeLaborDtlTimeField", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            status = bo.ResponseError.ToString(),

                        };
                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();
                        var listResult = listData.Last();
                        result = new
                        {
                            code = 200,
                            status = "OK",
                            data = new
                            {
                                LaborHrs = listResult.LaborHrs,
                                BurdenHrs = listResult.BurdenHrs
                            }
                        };
                        return result;
                    }
                }
                else
                {
                    result = new
                    {
                        code = 401,
                        status = "Not Authorized or Server Full"
                    };
                    return result;
                }
            }
            catch (Exception ex )
            {
                dynamic result;
               
                result = new
                {
                    code = 400,
                   status = ex.Message.ToString() + " ChangeLaborDtlTimeField"
                };
                return result;
            }
        }
        [Route("Labor/UpdateDtl")]
        [HttpPost]
        public dynamic UpdateDtl([FromBody]GetLabor entries)
        {
            try
            {
                List<LaborDtl> listData = new List<LaborDtl>();
                int HeadNum = entries.LaborHedSeq;
                int DtlNum = entries.LaborDtlSeq;
                string RowMod = entries.RowMod;
                dynamic result;

                user.nik = entries.nik;
                user.password = entries.password;
                bool test = epicRest.PortalBeearer(user);
                RowMod = RowMod == "" ? "A" : RowMod;
                if (test)
                {
                    listData = GetDetailID(HeadNum, DtlNum);
                    var param = listData.FirstOrDefault();
                    param.ClockInDate = entries.ClockInDate;
                    param.ClockinTime = ConvertTimeToDecimal(entries.ClockinTime);
                    param.ClockOutTime = ConvertTimeToDecimal(entries.ClockOutTime); ;
                    param.LaborHrs = entries.LaborHrs;
                    param.BurdenHrs = entries.BurdenHrs;
                    param.LaborQty = entries.LaborQty;
                    param.ScrapQty = entries.ScrapQty;
                    param.DiscrepQty = entries.DiscrepQty;
                    param.DiscrpRsnCode = entries.DiscrpRsnCode;
                    param.RowMod = RowMod;
                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { param }
                        },
                    };
                    string preview = "";
                    CheckWarnings(listData, RowMod);
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "Update", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();
                        result = new
                        {
                            code = 200,
                           status = "OK",
                            data = new {
                                listData
                            }
                        };
                        return result;
                    }

                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + " UpdateDtl"
                };
                return result;
            }
        }
        [Route("Labor/Submit")]
        [HttpPost]
        public dynamic SubmitForApproval([FromBody] GetLabor entries)
        {
            try
            {
                List<LaborDtl> listData = new List<LaborDtl>();
                List<LaborDtl> listData2 = new List<LaborDtl>();

                int HeadNum = entries.LaborHedSeq;
                int DtlNum = entries.LaborDtlSeq;
                string RowMod = "U";
                dynamic result;

                user.nik = entries.nik;
                user.password = entries.password;
                bool test = epicRest.PortalBeearer(user);
                RowMod = RowMod == "" ? "A" : RowMod;

                if (test)
                {
                    listData = GetDetailID(HeadNum, DtlNum);
                    
                    listData2 = ValidateChargeRateForTimeType(listData);
                    listData = ValidateChargeRateForTimeType(listData);

                    var param2 = listData.FirstOrDefault();
                    param2.TimeStatus = "";
                    var param = listData2.FirstOrDefault();
                    param.TimeStatus = "A";
                    param.NotSubmitted = false;
                    param.RowSelected = true;
                    param.NewDifDateFlag = 2;
                    param.RowMod = "U";
                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { param2, param }
                        },
                        weeklyView = false
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "SubmitForApprovalBySelected", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();
                        listData = ValidateChargeRateForTimeType(listData);
                         param2 = listData.FirstOrDefault();
                        param2.TimeStatus = "";
                         param = listData2.FirstOrDefault();
                        param.TimeStatus = "A";
                        param.NotSubmitted = false;
                        param.RowSelected = true;
                        param.NewDifDateFlag = 2;
                        param.RowMod = "U";
                         pData = new
                        {
                            ds = new
                            {
                                LaborDtl = new[] { param2, param }
                            },
                            weeklyView = false
                        };
                         preview = "";
                         bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "SubmitForApprovalBySelected", pData);
                        if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                        {
                            result = new
                            {
                                code = 400,
                               status = bo.ResponseError.ToString(),
                            
                            };

                            return result;
                        }
                        else
                        {
                            preview = bo.ResponseBody.ToString();
                             newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                            listData = newResponse.parameters.ds.LaborDtl.ToList();
                            listData = ValidateChargeRateForTimeType(listData);
                            result = new{ 
                             code = 200,
                             status = "OK",
                             
                            };
                            return result;
                        }
                    }

                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + " SubmitForApproval"
                };
                return result;
            }
        }

        [Route("Labor/Recall")]
        [HttpPost]
        public dynamic Recall([FromBody] GetLabor entries)
        {
            try
            {
                List<LaborDtl> listData = new List<LaborDtl>();
                List<LaborDtl> listData2 = new List<LaborDtl>();

                int HeadNum = entries.LaborHedSeq;
                int DtlNum = entries.LaborDtlSeq;
                string RowMod = "U";
                dynamic result;

                user.nik = entries.nik;
                user.password = entries.password;
                bool test = epicRest.PortalBeearer(user);
                RowMod = RowMod == "" ? "A" : RowMod;

                if (test)
                {
                    listData = GetDetailID(HeadNum, DtlNum);
                    listData2 = GetDetailID(HeadNum, DtlNum);


                    var param2 = listData.FirstOrDefault();
                    param2.TimeStatus = "";
                    var dt = RecallFromApprovalBySelected(param2.JobNum, param2.LaborTypePseudo, "");
                    var param = listData2.FirstOrDefault();
                    param.RowSelected = true;
                    param.NewDifDateFlag = 2;
                    param.RowMod = "U";
                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { param2, param }
                        },
                        weeklyView = false
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "RecallFromApprovalBySelected", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();
                         dt = RecallFromApprovalBySelected(param2.JobNum, param2.LaborTypePseudo, "");
                         param = listData2.FirstOrDefault();
                        param.RowSelected = true;
                        param.NewDifDateFlag = 2;
                        param.RowMod = "U";
                        pData = new
                        {
                            ds = new
                            {
                                LaborDtl = new[] { param2, param }
                            },
                            weeklyView = false
                        };
                        preview = "";
                        bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "RecallFromApprovalBySelected", pData);
                        if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                        {
                            result = new
                            {
                                code = 400,
                                status = bo.ResponseError.ToString(),
                                data = new { }
                            };

                            return result;
                        }
                        else
                        {
                            preview = bo.ResponseBody.ToString();
                            newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                            listData = newResponse.parameters.ds.LaborDtl.ToList();

                            result = new
                            {
                                code = 200,
                               status = "Ok"
                            };
                            return result;
                        }
                    }

                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + " SubmitForApproval"
                };
                return result;
            }
        }

        [Route("Labor/UpdateCoPart")]
        [HttpPatch]
        public dynamic UpdateCoPart([FromBody] GetLabor entries) {
            try
            {
                dynamic result;
                List<LaborHed> listHead = new List<LaborHed>();
                List<LaborDtl> listData = new List<LaborDtl>();
                LaborDtl data = new LaborDtl();
                user.nik = entries.nik;
                user.password = entries.password;
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var laborHeadSeq = entries.LaborHedSeq;
                    var LaborSeqDtl = entries.LaborDtlSeq;
                    var PartNum = entries.PartNum;
                    var Qty = entries.LaborQty;
                    var DiscrepQty = entries.DiscrepQty;


                    string rtn = "";
                    string method = "LaborParts('SAI'," + laborHeadSeq + "," + LaborSeqDtl + ",'"+PartNum+"')";
                    var patchData = new
                    {
                        Company = "SAI",
                        PartQty = Qty,

                    };
                    var boGet = EpicorRest.BoPatch("Erp.BO.LaborSvc", method,patchData);
                    if (boGet.IsErrorResponse)
                    {
                        result = new
                        {
                            code = 400,
                           status = boGet.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        rtn = boGet.ResponseBody.ToString();
                        var ds = new
                        {
                            LaborDtl = new[] { rtn }
                        };
                        //rtn = ds.ToString();
                        //JObject jsonObject = JObject.Parse(rtn);
                        //JArray jsonArray = (JArray)jsonObject["LaborDtl"];
                        data = JsonConvert.DeserializeObject<LaborDtl>(rtn);
                        listData = new List<LaborDtl> { data };
                        result = new
                        {
                            code = 200,
                           status = "Ok",
                            
                        };
                        return result;
                    }
                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + "GetLaborDtl"
                };
                return result;
            }
        }

        public dynamic ValidateChargeRateForTimeType(List<LaborDtl> labors) {
            try
            {
                List<LaborDtl> listData = new List<LaborDtl>();

                dynamic result;
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var param = labors.Last();
                    param.RowMod = "U";
                    var pData = new
                    {
                        ds = new
                        {
                            LaborDtl = new[] { param }
                        },
                    };
                    string preview = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "ValidateChargeRateForTimeType", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                          data = new { }
                        };
                        return result;
                    }
                    else
                    {
                        preview = bo.ResponseBody.ToString();
                        LaborResponse newResponse = JsonConvert.DeserializeObject<LaborResponse>(preview);
                        listData = newResponse.parameters.ds.LaborDtl.ToList();

                        return listData;
                    }

                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + " ValidateChargeRateForTimeType"
                };
                return result;
            }
        }

        public dynamic RecallFromApprovalBySelected(string jobNum, string laborType, string prjectID)
        {
            try
            {
                List<LaborDtl> listData = new List<LaborDtl>();

                dynamic result;
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var pData = new
                    {
                       jobNum = jobNum,
                        laborTypePseudo = laborType,
                        projectID = prjectID
                    };
                    string rtn = "";
                    var bo = EpicorRest.BoPost("Erp.BO.LaborSvc", "ValidateProjectClosed", pData);
                    if (bo.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                           status = bo.ResponseError.ToString(),
                        
                        };

                        return result;
                    }
                    else
                    {
                        rtn = bo.ResponseBody.ToString();
                        //JObject jsonObject = JObject.Parse(rtn);
                        //JArray jsonArray = (JArray)jsonObject["returnObj"];
                        //rtn = jsonArray == null ? "" : jsonArray.ToString();
                        return rtn;
                    }

                }
                else
                {
                    result = new
                    {
                        code = 401,
                       status = "Not Authorized or Server Full"
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
                   status = ex.Message.ToString() + " ValidateChargeRateForTimeType"
                };
                return result;
            }
        }

        [Route("Labor/DeleteHead")]
        [HttpDelete]
        public dynamic DeleteHed([FromBody] GetLabor entries)
        {
            try
            {
                dynamic result;
                List<LaborHed> listHead = new List<LaborHed>();
                List<LaborDtl> listData = new List<LaborDtl>();
                LaborDtl data = new LaborDtl();
                user.nik = entries.nik;
                user.password = entries.password;
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var laborHeadSeq = entries.LaborHedSeq;
                    var LaborSeqDtl = entries.LaborDtlSeq;
                    string rtn = "";
                    string method = "Labors('SAI'," + laborHeadSeq + ")";
                  
                    var boGet = EpicorRest.BoDelete("Erp.BO.LaborSvc", method);
                    if (boGet.IsErrorResponse)
                    {
                        result = new
                        {
                            code = 400,
                            status = boGet.ResponseError.ToString(),

                        };
                        return result;
                    }
                    else
                    {
                        result = new
                        {
                            code = 200,
                            status = "Ok",

                        };
                        return result;
                    }
                }
                else
                {
                    result = new
                    {
                        code = 401,
                        status = "Not Authorized or Server Full"
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
                    status = ex.Message.ToString() + "GetLaborDtl"
                };
                return result;
            }
        }
        [Route("Labor/DeleteDtl")]
        [HttpDelete]
        public dynamic DeleteDtl([FromBody] GetLabor entries)
        {
            try
            {
                dynamic result;
                List<LaborHed> listHead = new List<LaborHed>();
                List<LaborDtl> listData = new List<LaborDtl>();
                LaborDtl data = new LaborDtl();
                user.nik = entries.nik;
                user.password = entries.password;
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var laborHeadSeq = entries.LaborHedSeq;
                    var LaborSeqDtl = entries.LaborDtlSeq;

                    string rtn = "";
                    string method = "LaborDtls('SAI'," + laborHeadSeq + ","+ LaborSeqDtl + ")";
                   
                    var boGet = EpicorRest.BoDelete("Erp.BO.LaborSvc", method);
                    if (boGet.IsErrorResponse)
                    {
                        result = new
                        {
                            code = 400,
                            status = boGet.ResponseError.ToString(),

                        };
                        return result;
                    }
                    else
                    {
                        result = new
                        {
                            code = 200,
                            status = "Ok",

                        };
                        return result;
                    }
                }
                else
                {
                    result = new
                    {
                        code = 401,
                        status = "Not Authorized or Server Full"
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
                    status = ex.Message.ToString() + "GetLaborDtl"
                };
                return result;
            }
        }
        static double ConvertTimeToDecimal(string time)
        {
            // Pisahkan jam dan menit
            string[] parts = time.Split(':');
            int hours = int.Parse(parts[0]);
            int minutes = int.Parse(parts[1]);

            // Ubah menit menjadi nilai desimal
            double timeInDecimal = hours + (minutes / 60.0);

            return timeInDecimal;
        }
    }
}
// SUDAH END KAH MANIS ?