using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  
using Serilog;
using Serilog.Events;
using System.Data;

namespace MSSqlWebapi.Models
{
    public sealed class Top100RowsResource : Resource
    {
        private ServerContext _context;
        private string _dbName;
        private string _tableName;
        private List<Dictionary<string, string>> _top100Rows;
        public List<Dictionary<string, string>> Top100Rows { get { return this._top100Rows; } }
        
        public Top100RowsResource(ServerContext context, string dbName, string tableName, IUrlHelper urlHelper)
        {
            this._context = context;
            this._dbName = dbName;
            this._tableName = tableName;
            this.UpdateLinks(urlHelper);
            this.GetTop100Rows();
        }

        private void GetTop100Rows()
        {
            try
            {
                this._top100Rows = new List<Dictionary<string, string>>();

                SMO.Database smoDb = _context.SmoServer.Databases[this._dbName];
                if (smoDb == null)
                {
                    Log.Warning("Database {0} not found. No Rows to display.", this._dbName);
                    return;
                }

                SMO.Table smoTable = smoDb.Tables[this._tableName];
                if(smoTable == null)
                {
                    Log.Warning("Table {0} not found in Database {1}. No Rows to display.",
                        this._tableName, this._dbName);
                    return;
                }

                // fetch top 100 rows from table
                string sqlQuery = String.Format("SELECT TOP 100 * FROM {0} WITH(NOLOCK)", smoTable.Name);
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
                Log.Error("Error while generating script for table {0} in database {1}\n\n{2}",
                    this._tableName, this._dbName, e.ToString());
            }
        }

        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameTableTop100Rows,
                new
                {
                    dbName = this._dbName,
                    tableName = this._tableName
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameTable,
                new
                {
                    dbName = this._dbName,
                    tableName = this._tableName
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));
        }
    }
}