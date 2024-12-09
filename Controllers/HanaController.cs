using Microsoft.AspNetCore.Mvc;
using HanaWebApp.Models;
using System.Data.Odbc;

namespace HanaWebApp.Controllers
{
    public class HanaController : Controller
    {
        public IActionResult Index()
        {
            var records = new List<HanaRecord>();

            string dsn = "DSN=HANA_ODBC_64;"; // Substitua pelo seu DSN configurado
            string user = "******";
            string password = "********";
            string connectionString = $"{dsn}UID={user};PWD={password};";

            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    var command = new OdbcCommand("SELECT SECUSERNAME FROM SEUESQUEMAAQUI.SECUSER", connection);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new HanaRecord
                            {
                                SecUsername = reader.GetString(0)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Erro ao conectar: {ex.Message}";
            }

            return View(records);
        }
    }
}
