using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    public partial class update_statistics_view : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS public.statistics;
                CREATE OR REPLACE VIEW public.statistics as
                select a.createdate, processtype, sum(pending) as pending, sum(delivered) as delivered, sum(failed) as failed 
                from
                (
                SELECT ""CreateDate""::TIMESTAMP::DATE as createdate
                , case when ""DeliveryStatus"" = 0 then 1 else 0 end as pending
                , case when ""DeliveryStatus"" = 1 then 1 else 0 end as delivered
                , case when ""DeliveryStatus"" = 2 then 1 else 0 end as failed
                , ""ProcessType"" as processtype
                FROM public.ft_purchase_orders
                ) a
                group by a.createdate, processType;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW public.statistics;");
        }
    }
}
