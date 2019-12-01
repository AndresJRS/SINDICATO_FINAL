
using Sindicato_v1.Hubs;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Sindicato_v1.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult Notifications()
        {
            return View();
        }

        public JsonResult Get()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StrConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [cedula],[nombre],[primer_Apellido] FROM [dbo].[Tbl_Persona] WHERE [Estado] = 2", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    var listNoti = reader.Cast<IDataRecord>()
                            .Select(x => new
                            {
                                Cedula = (int)x["cedula"],
                                Nombre = (string)x["nombre"],
                                Apellido = (string)x["primer_Apellido"],
                            }).ToList();

                    return Json(new { listNoti = listNoti }, JsonRequestBehavior.AllowGet);

                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            NotificationsHub.Show();
        }
    }
}