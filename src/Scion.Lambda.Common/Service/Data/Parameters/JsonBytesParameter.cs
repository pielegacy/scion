using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Scion.Lambda.Common.Service.Data.Parameters
{
    internal sealed class JsonBytesParameter : ICustomQueryParameter
    {
        private readonly Byte[] _value;

        public JsonBytesParameter(Byte[] value)
        {
            _value = value;
        }

        public void AddParameter(IDbCommand command, string name)
        {
            var parameter = new NpgsqlParameter(name, NpgsqlTypes.NpgsqlDbType.Jsonb)
            {
                Value = _value
            };

            command.Parameters.Add(parameter);
        }
    }
}
