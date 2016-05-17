using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using System.Data.SqlClient;

namespace DSS
{
    class DBHelper
    {
        public static void AddMapImageToDB(String name, byte[] image, byte[] coords)
        {
            try
            {
                DBDataSet.FieldImagesDataTable dt = new DBDataSet.FieldImagesDataTable();
                DBDataSetTableAdapters.FieldImagesTableAdapter adapter = new DBDataSetTableAdapters.FieldImagesTableAdapter();
                adapter.Fill(dt);
                DBDataSet.FieldImagesRow row = (DBDataSet.FieldImagesRow)dt.NewRow();
                row.image_name = name;
                row.data = image;
                row.map_coordinates = coords;

                dt.Rows.Add(row);
                adapter.Update(dt);
                adapter.Dispose();
                dt.Dispose();
            }
            catch (Exception e )
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
