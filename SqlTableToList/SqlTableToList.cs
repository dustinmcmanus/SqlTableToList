using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqlTableToList
{
    public class SqlTableToList<T>
    {
        IDbConnection Database;
        IDbTransaction Transaction;

        public SqlTableToList(IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            this.Database = dbConnection;
            this.Transaction = dbTransaction;
        }
        
        public virtual IEnumerable<T> Select(string sql)
        {
            IEnumerable<T> records;
            List<T> recordsList;
            List<Dictionary<string, object>> dictList = new List<Dictionary<string, object>>();
            PropertyInfo[] classProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

            using (IDbCommand cmd = Database.CreateCommand())
            {
                cmd.Transaction = Transaction;
                cmd.CommandText = sql;
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        foreach (PropertyInfo p in classProps)
                        {
                            ColumnAttribute c = (ColumnAttribute)p.GetCustomAttribute(typeof(ColumnAttribute), true);
                            if (c != null)
                            {
                                if ((!dr.IsDBNull(dr.GetOrdinal(c.Name))))
                                    dict.Add(p.Name, dr.GetValue(dr.GetOrdinal(c.Name)));
                            }

                            //NotMappedColumnAttribute c2 = p.GetCustomAttribute(typeof(Schema.NotMappedColumnAttribute), false);
                            //if (c2 != null)
                            //{
                            //    if ((!dr.IsDBNull(dr.GetOrdinal(c2.ColumnName))))
                            //        dict.Add(p.Name, dr.GetValue(dr.GetOrdinal(c2.ColumnName)));
                            //}
                        }
                        dictList.Add(dict);
                    }
                }

                string dictListJson = JsonConvert.SerializeObject(dictList);
                recordsList = JsonConvert.DeserializeObject<List<T>>(dictListJson);
                records = recordsList;
            }

            return records;
        }
    }
}
