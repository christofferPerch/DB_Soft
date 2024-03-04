using DB_Soft.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DB_Soft.ViewModels;

namespace DB_Soft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _organizationService;
        private readonly IConfiguration _configuration;

        public OrganizationController(OrganizationService organizationService, IConfiguration configuration)
        {
            _organizationService = organizationService;
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetOrganizations()
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand("SP_OrganizationWithCountries", myConn))
                {
                    myCommand.CommandType = CommandType.StoredProcedure; // If you are using a stored procedure
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
                myConn.Close();
            }

            return new JsonResult(table);
        }

        // New method to get organization emissions for a specific year
        // Modify this method in your OrganizationController
        [HttpGet("Emissions")]
        public JsonResult GetEmissions(string reportingYear, string organizationName)
        {
            if (string.IsNullOrWhiteSpace(organizationName))
            {
                return new JsonResult(new { error = "Organization name is required." }) { StatusCode = 400 }; // Bad Request
            }

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("SP_OrgEmissionForYear", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@year", reportingYear);
                    sqlCommand.Parameters.AddWithValue("@organizationName", organizationName);

                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        table.Load(sqlReader);
                    }
                }
            }

            return new JsonResult(table);
        }
    }
}

