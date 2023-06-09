using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scion.Lambda.Artificer.Migrations
{
    [Migration(20231824050415)]
    public sealed class Initial : Migration
    {
        public override void Up()
        {
            Create.Schema("external_data");

            Create.Table("sets")
                .InSchema("external_data")
                .WithColumn("code").AsString().PrimaryKey()
                .WithColumn("data").AsCustom("jsonb").NotNullable();
        }

        public override void Down()
        {
            
        }
    }
}
