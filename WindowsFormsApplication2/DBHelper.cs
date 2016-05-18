using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace DSS
{
    class DBHelper
    {
        public class SQLiteDBProvider
        {
            string baseName = DSS.Properties.Settings.Default.DBName;
            public SQLiteDBProvider()
            {
                SQLiteConnection.CreateFile(baseName);
                SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + baseName;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = DSS.Properties.Settings.Default.SQLiteCreateStatement;
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
            }
            
        }

        public static void AddMapImageToDB(String name, byte[] image, byte[] coords)
        {
            try
            {
                SQLiteDBProvider db = new SQLiteDBProvider();


            }
            catch (Exception e )
            {
                throw new Exception(e.Message);
            }
            
        }

        public static HashSet<CoordHelper.MapPointer> LoadMapPointers()
        {
            throw new NotImplementedException();
        }
    }
}
