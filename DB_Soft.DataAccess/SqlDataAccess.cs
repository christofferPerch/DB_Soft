using DB_Soft.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DB_Soft.DataAccess
{
    public class SqlDataAccess : IDataAccess
    {
        private readonly string _connectionString;

        public SqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        private async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await getData(connection);
            }
            catch (Exception ex)
            {
                throw new Exception("Error with connection to database: " + ex.Message, ex);
            }
        }

        public async Task<T?> GetById<T>(string sql, object parameters)
        {
            try
            {
                return await WithConnection(async db =>
                {
                    var result = await db.QueryFirstOrDefaultAsync<T>(sql, parameters);
                    return result;
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing GetById: {ex.Message}", ex);
            }
        }

        public async Task<List<T>> GetAll<T>(string sql, object? parameters = null)
        {
            try
            {
                return await WithConnection(async db =>
                {
                    var result = await db.QueryAsync<T>(sql, parameters);
                    return result.ToList();
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing GetAll: {ex.Message}", ex);
            }
        }

        public async Task<int> Insert(string sql, object parameters)
        {
            try
            {
                return await WithConnection(async db =>
                {
                    var result = await db.ExecuteAsync(sql, parameters);
                    return result;
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing Insert: {ex.Message}", ex);
            }
        }

        public async Task<int> Update(string sql, object parameters)
        {
            try
            {
                return await WithConnection(async db =>
                {
                    var result = await db.ExecuteAsync(sql, parameters);
                    return result;
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing Update: {ex.Message}", ex);
            }
        }

        public async Task<int> Delete(string sql, object parameters)
        {
            try
            {
                return await WithConnection(async db =>
                {
                    var result = await db.ExecuteAsync(sql, parameters);
                    return result;
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing Delete: {ex.Message}", ex);
            }
        }

        public async Task<T?> ExecuteStoredProcedure<T>(string procedureName, object parameters)
        {
            try
            {
                return await WithConnection(async db =>
                {
                    var result = await db.QueryFirstOrDefaultAsync<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing ExecuteStoredProcedure: {ex.Message}", ex);
            }
        }
    }
}
