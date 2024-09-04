using Microsoft.AspNetCore.Mvc;
using EpicorRestAPI;
using EpicorRestSharedClasses;
using Newtonsoft.Json;
using EPAPI.Repository;
using Newtonsoft.Json.Linq;
using System;
using EPAPI.Repository.Purchase;
using System.Collections.Generic;

namespace EPAPI.Controllers
{
    public class AuthController : Controller
    {
        public EpicRest epicRest = new EpicRest();
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
        
    }
}
