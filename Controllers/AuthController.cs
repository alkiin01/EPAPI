using Microsoft.AspNetCore.Mvc;
using EpicorRestAPI;
using EpicorRestSharedClasses;
using Newtonsoft.Json;
using EPAPI.Repository;
using Newtonsoft.Json.Linq;
using System;
using EPAPI.Repository.Purchase;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace EPAPI.Controllers
{
    public class AuthController : Controller
    {
        public EpicRest epicRest = new();
        Mail mail = new Mail();
        User user = new User();

        [Route("Auth/Login")]
        [HttpPost]
        public dynamic EpicorAuth([FromBody] User user)
        {
            try
            {
                List<UserFile> list = new List<UserFile> ();
                dynamic result;
                var login = epicRest.CreateLogin(user);
                if (login)
                {
                    string rtn = "";
                    MultiMap<string, string> dic = new MultiMap<string, string>();
                    dic.Add("dcdUserID", user.nik); //Value harus string
                    dic.Add("userID", user.nik); //Value harus string

                    var boGet = EpicorRest.BoGet("Ice.BO.UserFileSvc", "GetByID", dic);
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
                        JArray jsonArray = (JArray)jsonObject["returnObj"]["UserFile"];
                        rtn = jsonArray == null ? "" : jsonArray.ToString();
                        list = JsonConvert.DeserializeObject<List<UserFile>>(rtn);
                        var res = list.FirstOrDefault();

                        result = new
                        {
                            code=200,
                            desc="ok",
                            name= res.Name,
                            email = res.EMailAddress,
                            SecurityMgr = res.SecurityMgr,
                            GroupList = res.GroupList
                        };

                    }
                    return result;
                }else{
                    result = new
                    {
                        code = 401,
                        desc = "Worng username or password",
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
                    msg = ex.Message.ToString(),
                    desc = "Worng username or password"
                };
                return result;
            }
        }

        [Route("SendEmail")]
        public dynamic SendEmail([FromBody] Mail mail)
        {
            try
            {
                user.nik = mail.nik;
                user.password = mail.password;
                bool test = epicRest.PortalBeearer(user);
                dynamic result;
                if (test)
                {
                    var mailSend = new
                    {
                        emailFrom = mail.EmailFrom,
                        emailTo = mail.EmailTo,
                        emailSubject = mail.EmailSubject,
                        emailBody = mail.EmailBody,
                        bodyIsHtml = mail.BodyIsHtml,
                        sendAsync = mail.SendAsync,
                        emailCC= "",
                        emailBCC= "",
                        emailReplyTo= "",
                        emailMessageID= "",
                        emailPriority= ""
                    };
                    
                    var rsp = EpicorRest.EfxPost("SAIMail", "Email", mailSend);
                    if (rsp.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            desc = rsp.ResponseError.ToString()+" From BO"
                        };
                        return result;
                    }
                    else
                    {
                        var nama = mail.EmailTo;
                        var reslt = nama.Replace(";", ",");
                        result = new
                        {
                            code = 200,
                            desc = "Email Sends to :"+ reslt
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
                dynamic result;
                result = new
                {
                    code = 401,
                    desc = ex.Message.ToString()
                };
                return result;
                throw;
            }
            
            

        }
    }
}
