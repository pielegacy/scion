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

        public async Task PurgeCardsAsync()
        {
            await Connection.Value.ExecuteAsync("TRUNCATE cards");
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
