using Chatbot.DataAccess;
using Chatbot.Models;
using Dapper;
using System.Data;

namespace DB_Soft.Services
{
    public class GasService
    {
        private readonly IDataAccess _dataAccess;

        public GasService(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }


    }
}