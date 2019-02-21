using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplicationmvc.Entity;

namespace WebApplicationmvc.Controllers
{
    public class SlaController : Controller
    {
        private Logger logger;

        public SlaController()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        // GET: Sla
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> sla(SLABE model)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("www.yahoo.com");
                    return Json(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }

            return Json("Error Occured!");
        }
        // If we got this far, something failed, redisplay form
    }

    }
