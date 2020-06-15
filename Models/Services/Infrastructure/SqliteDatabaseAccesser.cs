using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using MyCourse.Models.Options;


namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccesser : IDatabaseAccesser
    {
        private readonly IOptionsMonitor<ConnectionStringsOptions> connectionStringOptions;
        public SqliteDatabaseAccesser(IOptionsMonitor<ConnectionStringsOptions> connectionStringOptions)
        {
            this.connectionStringOptions = connectionStringOptions;
        }


        public DataSet Query(FormattableString formattableQuery)
        {   
            //Creiamo dei SqliteParameter a partire dalla FormattableString
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqliteParameter>();
            for (var i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = formattableQuery.ToString();

            string connectionString = connectionStringOptions.CurrentValue.Default;

            //Colleghiamoci al database Sqlite, inviamo la query e leggiamo i risultati
            using(var conn = new SqliteConnection(connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddRange(sqliteParameters);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var dataSet = new DataSet();
                        
                        //TODO: La riga qui sotto va rimossa quando la issue sarà risolta
                        //https://github.com/aspnet/EntityFrameworkCore/issues/14963
                        dataSet.EnforceConstraints = false;

                        do 
                        {
                            var dataTable = new DataTable();
                            dataSet.Tables.Add(dataTable);
                            dataTable.Load(reader);
                        } while (!reader.IsClosed);

                        return dataSet;
                    }
                }
            }
        }
    }
}