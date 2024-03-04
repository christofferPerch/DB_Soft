using DB_Soft.DataAccess;
using DB_Soft.Models;
using DB_Soft.ViewModels;
using Dapper;
using System.Data;
using System.Text.Json; // Include the System.Text.Json namespace

namespace DB_Soft.Services
{
    public class OrganizationService
    {
        private readonly IDataAccess _dataAccess;

        public OrganizationService(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<string> GetOrganizationWithCountriesAsJson()
        {
            var organizations = await _dataAccess.ExecuteStoredProcedureNoParameters<List<OrganizationViewModel>>("SP_OrganizationWithCountries");
            return JsonSerializer.Serialize(organizations);
        }
    }
}
