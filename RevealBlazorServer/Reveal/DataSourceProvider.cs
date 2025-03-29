﻿using Reveal.Sdk;
using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Microsoft.SqlServer;

namespace RevealSdk.Server.Reveal
{
    internal class DataSourceProvider : IRVDataSourceProvider
    { 
        public Task<RVDataSourceItem>? ChangeDataSourceItemAsync(IRVUserContext userContext, 
                string dashboardId, RVDataSourceItem dataSourceItem)
        {
            if (dataSourceItem is RVSqlServerDataSourceItem sqlDsi)
            {
                ChangeDataSourceAsync(userContext, sqlDsi.DataSource);

                if (sqlDsi.Id == "CustOrderHist")
                {
                    sqlDsi.Procedure = "CustOrderHist";
                    sqlDsi.ProcedureParameters = new Dictionary<string, object> { { "@CustomerID", userContext.UserId } };
                }

                else if (sqlDsi.Id == "CustOrdersOrders")
                {
                    sqlDsi.Procedure = "CustOrdersOrders";
                    sqlDsi.ProcedureParameters = new Dictionary<string, object> { { "@CustomerID", userContext.UserId } };
                }

                else if (sqlDsi.Id == "TenMostExpensiveProducts")
                {
                    sqlDsi.Procedure = "Ten Most Expensive Products";
                }


                else if (sqlDsi.Id == "300KRows")
                {
                    sqlDsi.Procedure = "spGetLoad1m";
                }

                else if (sqlDsi.Id == "CustomerOrders")
                {
                    //sqlDsi.CustomQuery = "Select CustomerID, EmployeeID, Freight from Orders Where OrderId = " + userContext.Properties["OrderId"];

                    sqlDsi.CustomQuery = "SELECT      CAST(YEAR(O.OrderOrderDate) AS INT) " +
                        "AS OrderYear,     COUNT(O.OrderID) AS TotalOrders FROM      " +
                        "OrderLineItems AS O  WHERE O.CustomerID = '" + userContext.UserId + 
                        "' GROUP BY      YEAR(O.OrderOrderDate)";


                }

                else if (sqlDsi.Id == "TenMostExpensiveProducts")
                {
                    sqlDsi.Title = "Ten Most Expensive Products";
                    sqlDsi.Subtitle = "Whatever";
                    sqlDsi.Procedure = "Ten Most Expensive Products";
                }
                else if (sqlDsi.Table == "OrdersQry")
                {
                    //sqlDsi.Database = "devtest";
                }

                //else return null;
            }
            return Task.FromResult(dataSourceItem);
        }

        public Task<RVDashboardDataSource> ChangeDataSourceAsync(IRVUserContext userContext, RVDashboardDataSource dataSource)
        {
            if (dataSource is RVSqlServerDataSource sqlDs)
            {


                sqlDs.Host = "jberes.database.windows.net";
                sqlDs.Database = "northwindcloud";
            }
            return Task.FromResult(dataSource);
        }
    }
}