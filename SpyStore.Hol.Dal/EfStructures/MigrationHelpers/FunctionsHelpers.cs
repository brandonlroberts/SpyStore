﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SpyStore.Hol.Dal.EfStructures.MigrationHelpers
{
    public static class FunctionsHelpers
    {
        public static void CreateOrderTotalFunction(MigrationBuilder migrationBuilder)
        {
            string sql = @"
                CREATE FUNCTION Store.GetOrderTotal ( @OrderId INT )
                RETURNS MONEY WITH SCHEMABINDING
                BEGIN
                DECLARE @Result MONEY;
                SELECT @Result = SUM([Quantity]*[UnitCost]) FROM Store.OrderDetails
                WHERE OrderId = @OrderId;
                RETURN @Result
                END";
            migrationBuilder.Sql(sql);
        }

        public static void DropOrderTotalFunction(MigrationBuilder builder)
        {
            builder.Sql("drop function [Store].[GetOrderTotal]");
        }
    }
}
