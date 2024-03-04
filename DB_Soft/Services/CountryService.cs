using DB_Soft.DataAccess;
using DB_Soft.Models;
using Dapper;
using System.Data;

namespace DB_Soft.Services
{
	public class CountryService
	{
		private readonly IDataAccess _dataAccess;

		public CountryService(IDataAccess dataAccess)
		{
			_dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
		}

	


	}
}