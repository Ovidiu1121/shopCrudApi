using FluentMigrator;

namespace ShopCrudApi.Data.Migrations
{
    [Migration(422202410)]
    public class TestMigrate : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Execute.Script(@"./Data/Scripts/data.sql");
        }
    }
}
