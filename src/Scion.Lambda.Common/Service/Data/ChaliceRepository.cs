using Microsoft.Extensions.Configuration;
using Scion.Lambda.Common.Interface.Service.Data;
using Scion.Lambda.Common.Service.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using Scion.Lambda.Common.Interface.Models;
using Dapper;
using System.Text.Json;
using Scion.Lambda.Common.Extensions;
using Scion.Lambda.Common.Service.Data.Parameters;
using System.IO;

namespace Scion.Lambda.Common.Service.Data
{
    public sealed class ChaliceRepository : IChaliceRepository
    {
        private ChaliceConfiguration Configuration { get; }
        private Lazy<IDbConnection> Connection { get; }

        public ChaliceRepository(ChaliceConfiguration configuration)
        {
            Configuration = configuration;
            Connection = new Lazy<IDbConnection>(() =>
            {
                var connection = new NpgsqlConnection(Configuration.ConnectionString);
                connection.Open();
                return connection;
            });
        }

        public ChaliceRepository(IConfiguration configuration) : this(configuration.GetChaliceConfiguration()) { }

        public async Task SaveSetCardsAsync(string setCode, Stream stream)
        {
            using var reader = new StreamReader(stream, Encoding.UTF8);
           
            using var transaction = Connection.Value.BeginTransaction();

            await transaction.Connection.ExecuteAsync("DELETE FROM external_data.sets WHERE code = @SetCode",
                new
                {
                    setCode
                });
            await transaction.Connection.ExecuteAsync("INSERT INTO external_data.sets (code, data) VALUES (@SetCode, @Data);",
                new
                {
                    setCode,
                    data = new JsonBytesParameter(Encoding.UTF8.GetBytes(await reader.ReadToEndAsync()))
                });

            transaction.Commit();
        }

        public async Task SaveCardsAsync(IEnumerable<Card> cards)
        {
            using var transaction = Connection.Value.BeginTransaction();

            foreach (var card in cards)
            {
                await Connection.Value.ExecuteAsync(
                    @"INSERT INTO public.cards (id, name, identity) VALUES (@Id, @Name, @ColorIdentity);",
                    new
                    {
                        card.Id,
                        card.Name,
                        card.ColorIdentity,
                    }
                );
            }

            transaction.Commit();
        }

        public void Dispose()
        {
            if (Connection.IsValueCreated)
            {
                Connection.Value.Dispose();
            }
        }
    }
}
