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
            Create.Table("Cards")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("data").AsCustom("json").NotNullable();
        }

        public override void Down()
        {
            
        }
    }
}
