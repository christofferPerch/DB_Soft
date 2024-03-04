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

        [HttpGet("OrgGases")]
        public JsonResult GetGasesUsedByOrg(string gas)
        {
            if (string.IsNullOrWhiteSpace(gas))
            {
                return new JsonResult(new { error = "Gas name is required." }) { StatusCode = 400 }; // Bad Request
            }

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("SP_OrgSpecificGas", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@gas", gas);
       
                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        table.Load(sqlReader);
                    }
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("PopulationDetailsYear")]
        public JsonResult GetPopulationDetailsByYear(string year)
        {
            if (string.IsNullOrWhiteSpace(year))
            {
                return new JsonResult(new { error = "Year is required." }) { StatusCode = 400 }; // Bad Request
            }

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("SP_PopulationDetailsYear", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@year", year);

                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        table.Load(sqlReader);
                    }
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("C40")]
        public JsonResult GetOrganizationsC40()
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand("SP_OrganizationC40", myConn))
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
        [HttpGet("Gases")]
        public JsonResult GetGases(string organizationName)
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
                using (SqlCommand sqlCommand = new SqlCommand("SP_OrganizationGases", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@organizationName", organizationName);

                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        table.Load(sqlReader);
                    }
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("Method")]
        public JsonResult GetOrganizationMethodologies(string organizationName)
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_GetOrganizationMethodologies", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@organizationName", organizationName);

                    sqlConnection.Open();
                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        table.Load(sqlReader);
                    }
                }
            }

            return new JsonResult(table);
        }


        [HttpGet("CountriesLandAreaAndTemp")]
        public JsonResult GetCountriesLandAreaAndTemperature()
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand("SP_GetCountriesLandAreaAndTemperature", myConn))
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

        [HttpGet("OrgGDP")]
        public JsonResult GetOrganizationsWithGdp(string reportingYear)
        {
            if (string.IsNullOrWhiteSpace(reportingYear))
            {
                return new JsonResult(new { error = "reporting year is required." }) { StatusCode = 400 }; // Bad Request
            }

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("SP_OrganizationWithGDP", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@year", reportingYear);

                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        table.Load(sqlReader);
                    }
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("OrganizationEmissions")]
        public JsonResult GetOrganizationsWithEmissions()
        {

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("SP_OrganizationsWithEmissions", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

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

