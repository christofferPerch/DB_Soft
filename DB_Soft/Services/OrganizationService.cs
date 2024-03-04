using DB_Soft.DataAccess;
using DB_Soft.Models;
using DB_Soft.ViewModels;
using Dapper;
using System.Data;

namespace DB_Soft.Services
{
    public class OrganizationService
    {
        private readonly IDataAccess _dataAccess;

        public OrganizationService(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<List<OrganizationViewModel>> GetAllOrganizationsWithNames()
        {
            var SqlStr = = @"SELECT *
                             FROM ChatBot AS cb                           
                             WHERE cb.UserId = @UserId";

            return await _dataAccess.GetAll<OrganizationViewModel>(SqlStr);

        }

    }
}