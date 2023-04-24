using Microsoft.Extensions.Configuration;
using Scion.Lambda.Common.Interface.Service.Data;
using Scion.Lambda.Common.Service.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Scion.Lambda.Common.Service.Data
{
    public sealed class ChaliceRepository : IChaliceRepository
    {
        private ChaliceConfiguration Configuration { get; }

        public ChaliceRepository(ChaliceConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
