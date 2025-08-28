using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillsFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBillTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var triggerFunctionSql = @"
            CREATE OR REPLACE FUNCTION update_bill_total_price()
            RETURNS TRIGGER AS $$
            DECLARE
                v_bill_id UUID;
            BEGIN
                -- Analyze the operation type and get the BillId accordingly
                IF (TG_OP = 'INSERT' OR TG_OP = 'UPDATE') THEN
                    v_bill_id := NEW.""BillId"";
                ELSE
                    v_bill_id := OLD.""BillId"";
                END IF;

                --  Analyze the operation type and update the TotalPrice accordingly
                UPDATE bill
                SET ""TotalPrice"" = (
                    SELECT COALESCE(SUM(""Quantity"" * ""UnitPrice""), 0)
                    FROM bill_detail
                    WHERE ""BillId"" = v_bill_id
                )
                WHERE ""Id"" = v_bill_id;

                -- Return the appropriate record based on the operation type
                RETURN COALESCE(NEW, OLD);
            END;
            $$ LANGUAGE plpgsql;
        ";

            var createTriggerSql = @"
            CREATE TRIGGER trg_update_bill_total_price
            AFTER INSERT OR UPDATE OR DELETE ON bill_detail
            FOR EACH ROW EXECUTE FUNCTION update_bill_total_price();
        ";

            migrationBuilder.Sql(triggerFunctionSql);
            migrationBuilder.Sql(createTriggerSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert 
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_update_bill_total_price ON bill_detail;");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS update_bill_total_price();");
        }
    }
}
