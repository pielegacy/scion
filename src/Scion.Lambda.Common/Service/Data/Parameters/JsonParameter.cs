using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Scion.Lambda.Common.Service.Data.Parameters
{
    internal sealed class JsonParameter : ICustomQueryParameter
    {
        private readonly string _value;

        internal JsonParameter(string value)
        {
            _value = value;
        }

        public static JsonParameter Create<TData>(TData value) => new(JsonSerializer.Serialize(value));

        public void AddParameter(IDbCommand command, string name)
        {
            var parameter = new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Json)
            {
                Value = _value
            };

            command.Parameters.Add(parameter);
        }
    }
}
