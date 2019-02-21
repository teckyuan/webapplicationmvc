using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            var userid =User.Identity.GetUserId();

            object result = null;
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[uspInsertSLA]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DESCRIPTION", model.desc);
                cmd.Parameters.AddWithValue("@SLADAYS", model.sladays);
                cmd.Parameters.AddWithValue("@createdby", userid);

                conn.Open();
                result = cmd.ExecuteScalarAsync().Result;

            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }
            finally
            {
                conn.Close();
            }

            return Json(result);
        }
        // If we got this far, something failed, redisplay form
    }

    }
