using System;
using System.Collections.Generic;
using System.Text;

using MMABooksTools;
using DBDataReader = MySql.Data.MySqlClient.MySqlDataReader;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MMABooksProps
{
    public class CustomerProps : IBaseProps
    {
        #region Auto-implemented Properties
        public string Code { get; set; } = "";

        public string Name { get; set; } = "";

        /// <summary>
        /// ConcurrencyID. Don't manipulate directly.
        /// </summary>
        public int ConcurrencyID { get; set; } = 0;
        #endregion

        public object Clone()
        {
            CustomerProps p = new CustomerProps();
            p.Code = this.Code;
            p.Name = this.Name;
            p.ConcurrencyID = this.ConcurrencyID;
            return p;
        }

        public string GetState()
        {
            string jsonString;
            jsonString = JsonSerializer.Serialize(this);
            return jsonString;
        }

        public void SetState(string jsonString)
        {
            CustomerProps p = JsonSerializer.Deserialize<CustomerProps>(jsonString);
            this.Code = p.Code;
            this.Name = p.Name;
            this.ConcurrencyID = p.ConcurrencyID;
        }

        public void SetState(DBDataReader dr)
        {
            this.Code = ((string)dr["StateCode"]).Trim();
            this.Name = (string)dr["StateName"];
            this.ConcurrencyID = (Int32)dr["ConcurrencyID"];
        }
    }
}
