using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlRestApi.Models
{
    public sealed class Top100RowsResource : Resource
    {
        private ServerContext _context;
        private string _dbName;
        private string _schemaName;
        private string _tableName;
        private List<Dictionary<string, string>> _top100Rows;
        public List<Dictionary<string, string>> Top100Rows { get { return this._top100Rows; } }
        
        public Top100RowsResource(ServerContext context, string dbName, string schemaName, string tableName, IUrlHelper urlHelper)
        {
            this._context = context;
            this._dbName = dbName;
            this._schemaName = schemaName;
            this._tableName = tableName;
            this.UpdateLinks(urlHelper);
            this.GetTop100Rows();
        }

        private void GetTop100Rows()
        {
            int rowCount = 100;
            string sqlQuery = string.Empty;
            try
            {
                this._top100Rows = new List<Dictionary<string, string>>();

                SMO.Database smoDb = _context.SmoServer.Databases[this._dbName];
                if (smoDb == null)
                {
                    Log.Warning("Database '{0}' not found. No Rows to display.", this._dbName);
                    return;
                }

                SMO.Table smoTable = smoDb.Tables[this._tableName, this._schemaName];
                if(smoTable == null)
                {
                    Log.Warning("Table '{0}' not found in Schema '{1}' in Database '{2}'. No Rows to display.",
                        this._tableName, this._schemaName, this._dbName);
                    return;
                }

                // fetch top 100 rows from table
                sqlQuery = String.Format("SELECT TOP {0} * FROM [{1}].[{2}] WITH(NOLOCK)",
                        rowCount, smoTable.Schema, smoTable.Name);
                Log.Information("Database: {0}, Schema: {1}, Table: {2}. Running SMO ExecuteWithResults: {3}",
                    smoDb.Name, smoTable.Schema, smoTable.Name, sqlQuery);

                using(DataSet dataset = smoDb.ExecuteWithResults(sqlQuery))
                {
                    if ((dataset != null) && (dataset.Tables.Count > 0) && (dataset.Tables[0].Rows.Count > 0))
                    {
                        // Loop through all rows in the table
                        foreach (DataRow datarow in dataset.Tables[0].Rows)
                        {
                            // Loop through all cells in row
                            int columIndex = 0;
                            Dictionary<string, string> rowToAdd = new Dictionary<string, string>();
                            foreach (object dataObj in datarow.ItemArray)
                            {
                                rowToAdd[smoTable.Columns[columIndex].Name] = dataObj.ToString();
                                columIndex ++;
                            }
                            _top100Rows.Add(rowToAdd);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Log.Error("Error running query: {0}\n\n{1}", sqlQuery, e.ToString());
            }
        }

        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.TableTop100Rows,
                new
                {
                    dbName = this._dbName,
                    schemaName = this._schemaName,
                    tableName = this._tableName
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Table,
                new
                {
                    dbName = this._dbName,
                    schemaName = this._schemaName,
                    tableName = this._tableName
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));
        }
    }
}