﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SpyStore.Hol.Dal.EfStructures.MigrationHelpers
{
    public static class ViewsHelper
    {
        public static void CreateOrderDetailWithProductInfoView(MigrationBuilder builder)
        {
            builder.Sql(@"
                CREATE VIEW [Store].[OrderDetailWithProductInfo]
                AS
                SELECT od.Id, od.TimeStamp, od.OrderId, od.ProductId, od.Quantity, od.UnitCost,
                od.Quantity * od.UnitCost as LineItemTotal,
                p.ModelName, p.Description as Description,
                p.ModelNumber, p.ProductImage,
                p.ProductImageLarge, p.ProductImageThumb,
                p.CategoryId, p.UnitsInStock, p.CurrentPrice, c.CategoryName
                FROM Store.Categories c
                INNER JOIN Store.Products p ON c.Id = p.Id
                INNER JOIN Store.OrderDetails od ON c.Id = od.Id");
        }

        public static void CreateCartRecordWithProductInfoView(MigrationBuilder builder)
        {
            builder.Sql(@"
                CREATE VIEW [Store].[CartRecordWithProductInfo]
                AS
                SELECT scr.Id, scr.TimeStamp, scr.DateCreated, scr.CustomerId, scr.Quantity,
                scr.LineItemTotal,
                scr.ProductId, p.ModelName, p.Description,
                p.ModelNumber, p.ProductImage,
                p.ProductImageLarge, p.ProductImageThumb,
                p.CategoryId, p.UnitsInStock, p.CurrentPrice, c.CategoryName
                FROM Store.ShoppingCartRecords scr
                INNER JOIN Store.Products p ON p.Id = scr.ProductId
                INNER JOIN Store.Categories c ON c.Id = p.CategoryId");
        }

        public static void DropOrderDetailWithProductInfoView(MigrationBuilder builder)
        {
            builder.Sql("drop view [Store].[OrderDetailWithProductInfo]");
        }
        public static void DropCartRecordWithProductInfoView(MigrationBuilder builder)
        {
            builder.Sql("drop view [Store].[CartRecordWithProductInfo]");
        }
    }
}
