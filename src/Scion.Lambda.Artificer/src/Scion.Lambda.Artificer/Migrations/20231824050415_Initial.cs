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
            Create.Table("cards")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("identity").AsCustom("text[]").NotNullable();
        }

        public override void Down()
        {
            
        }
    }
}
