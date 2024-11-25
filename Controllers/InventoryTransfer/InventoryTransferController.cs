using EPAPI.Repository;
using EPAPI.Repository.InventoryTransfer;
using EpicorRestAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace EPAPI.Controllers.InventoryTransfer
{
    public class InventoryTransferController : Controller
    {
        public EpicRest epicRest = new EpicRest();
        User user = new User();

        [Route("Mit/ScanProcess")]
        [HttpPost]
        public dynamic ScanProcess([FromBody] MIT mit)
        {
            dynamic result;
			try
			{
                user.nik = mit.nik;
                user.password = mit.password;
                bool test = epicRest.PortalBeearer(user);
                if (test)
                {
                    var pData = new
                    {
                         MIT = mit.Mit,
                         PartNum = mit.PartNumber,
                         Qty = mit.Qty,
                         Line = mit.Line,
                         isTransferred = mit.IsTransferred
                    };
                    var rsp = EpicorRest.EfxPost("SAIInvTransfer", "PartScan", pData);
                    if (rsp.ResponseStatus != System.Net.HttpStatusCode.OK)
                    {
                        result = new
                        {
                            code = 400,
                            status = rsp.ResponseError.ToString() ,
                        };
                        return result;
                    }
                    else
                    {
                       
                        result = new
                        {
                           code = 200,
                           status = "ok",
                           data = new
                           {
                               result = ""
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
                        status = "Not Authorized or Server Full",
                    };
                    return result;
                }

            }
            catch (Exception ex)
			{

                result = new
                {
                    code = 401,
                    status = ex.Message.ToString(),
                    data = new { }
                };
                return result;
            }
        }
       
    }
}
