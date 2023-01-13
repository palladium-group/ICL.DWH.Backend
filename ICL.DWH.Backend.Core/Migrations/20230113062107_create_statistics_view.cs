using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    public partial class create_statistics_view : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR REPLACE VIEW public.statistics as
                select a.createdate, sum(pending) as pending, sum(delivered) as delivered, sum(failed) as failed 
                from
                (
                SELECT ""CreateDate""::TIMESTAMP::DATE as createdate
                , case when ""DeliveryStatus"" = 0 then 1 else 0 end as pending
                , case when ""DeliveryStatus"" = 1 then 1 else 0 end as delivered
                , case when ""DeliveryStatus"" = 2 then 1 else 0 end as failed
                FROM public.ft_purchase_orders
                ) a
                group by a.createdate;");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW public.statistics;");
        }
    }
}
