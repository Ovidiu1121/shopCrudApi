using FluentMigrator;

namespace ShopCrudApi.Data.Migrations
{
    [Migration(4220224)]
    public class CreateSchema : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Table("shop")
                  .WithColumn("id").AsInt32().PrimaryKey().Identity()
                  .WithColumn("name").AsString(128).NotNullable()
                  .WithColumn("location").AsString(128).NotNullable()
                  .WithColumn("employees").AsInt32().NotNullable();
        }
    }
}
