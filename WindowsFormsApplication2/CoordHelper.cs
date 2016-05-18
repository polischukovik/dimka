using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS
{
    class CoordHelper
    {
        public class MapCoordinates
        {
            private double latitude;
            private double longetude;

            public MapCoordinates(double lat, double lon)
            {
                latitude = lat;
                longetude = lon;
            }

            public MapCoordinates getCoords()
            {
                return new MapCoordinates(latitude, longetude);
            }

            public double getLatitude()
            {
                return latitude;
            }

            public double getLongetude()
            {
                return longetude;
            }

            public byte[] getBytes()
            {
                String res = latitude.ToString() + ";" + longetude.ToString();
                byte[] bytes = new byte[res.Length * sizeof(char)];
                System.Buffer.BlockCopy(res.ToCharArray(), 0, bytes, 0, bytes.Length);
                return bytes;
            }

            public static byte[] coordToBytes(MapCoordinates coord)
            {
                String res = ((Double)coord.getLatitude()).ToString() + ";" + ((Double)coord.getLongetude()).ToString();
                byte[] bytes = new byte[res.Length * sizeof(char)];
                System.Buffer.BlockCopy(res.ToCharArray(), 0, bytes, 0, bytes.Length);
                return bytes;
            }

            public static MapCoordinates bytesToCoord(byte[] bytearr)
            {
                String src = System.Text.Encoding.UTF8.GetString(bytearr);
                double lat = 0, lon = 0;
                try 
	            {
                    String[] strArr = src.Split(";".ToCharArray(),System.StringSplitOptions.RemoveEmptyEntries);
                    if(strArr.Length != 2){
                        throw new Exception("Cannot parse coordinates");
                    }
                    lat = Double.Parse(strArr[0]);
                    lon = Double.Parse(strArr[1]);
	            }
                catch (System.FormatException e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
	            catch (Exception e)
	            {
                    System.Windows.Forms.MessageBox.Show(e.Message);
	            }
                return new MapCoordinates(lat, lon);
            }
        }


        public class MapPointer
        {
            int id, name;
            CoordHelper.MapCoordinates coordinates;
        }
    }
}
