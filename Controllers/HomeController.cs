using System.Data.SqlClient;
using System.Diagnostics;
using csv_to_db.Models;
using Microsoft.AspNetCore.Mvc;
using Dapper;


namespace csv_to_db.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConnectDb(ConnectionStringView cnnString)
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();

            connectionString.DataSource = cnnString.Server;
            connectionString.UserID = cnnString.User;
            connectionString.Password = cnnString.Password;
            connectionString.InitialCatalog = cnnString.Database;

            try
            {
                var cnn = new SqlConnection(connectionString.ConnectionString);
                cnn.Open();
                var eventos = cnn.Query("select * from Evento");
                return View(eventos);
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}