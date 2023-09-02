using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STAGGI_Budget_API.Migrations
{
    public partial class SPSubscriptionUpdater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE SubscriptionUpdater
	                    @IsActive bit = 0, 
	                    @EndDate DateTime = null
                        AS
                        BEGIN
	                    SET NOCOUNT ON;

	                    UPDATE Subscriptions
	                    SET IsActive = 0
	                    WHERE GETDATE() >= EndDate

                        END";
            migrationBuilder.Sql(sp);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
