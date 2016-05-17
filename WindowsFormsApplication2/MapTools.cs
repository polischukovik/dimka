using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace DSS
{
    class MapTools
    {
        public static String LoadMapPhotosFromFolder()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            DialogResult result = fbd.ShowDialog();
            string[] img_files = new string[0];
            int i = 0, failed = 0, saved = 0;

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                var allowedExtensions = Properties.Settings.Default.MapImagesFileTypes.Split(','); 
                var files = Directory
                    .GetFiles(fbd.SelectedPath)
                    .Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
                    .ToList();
                img_files = files.ToArray<String>();

                Image[] imgArray = new Image[img_files.Length];
                
                foreach (var file in img_files)
                {
                    try
                    {
                        DBHelper.AddMapImageToDB(file, MapTools.imageToByteArr(file), MapTools.getImageCoords(file).getBytes());
                        saved++;
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                        failed++;
                    }                    
                    i++;
                }
            }

            Console.WriteLine();
            return "Files found: " + img_files.Length.ToString() +
                    " Loaded: " + i + " Saved: " + saved + " Failed: " + failed;
        }

        public static byte[] imageToByteArr(String pathToFile)
        {
            byte[] xByte = (byte[])(new ImageConverter().ConvertTo(Image.FromFile(pathToFile), typeof(byte[])));
            return xByte;
        }

        public static CoordHelper.MapCoordinates getImageCoords(String pathToFile)
        {
            Image image = Image.FromFile(pathToFile); 
            return new CoordHelper.MapCoordinates(GetLatitude(image), GetLongitude(image));            
        }

        private static double ExifGpsToDouble(PropertyItem propItemRef, PropertyItem propItem) //to non static
        {
            double degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
            double degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
            double degrees = degreesNumerator / (double)degreesDenominator;

            double minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
            double minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);
            double minutes = minutesNumerator / (double)minutesDenominator;

            double secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
            double secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);
            double seconds = secondsNumerator / (double)secondsDenominator;


            double coorditate = degrees + (minutes / 60d) + (seconds / 3600d);
            string gpsRef = System.Text.Encoding.ASCII.GetString(new byte[1] { propItemRef.Value[0] }); //N, S, E, or W
            if (gpsRef == "S" || gpsRef == "W")
                coorditate = coorditate * -1;
            return coorditate;
        }

        public static double GetLatitude(Image targetImg)
        {
            try
            {
                //Property Item 0x0001 - PropertyTagGpsLatitudeRef
                PropertyItem propItemRef = targetImg.GetPropertyItem(1);
                //Property Item 0x0002 - PropertyTagGpsLatitude
                PropertyItem propItemLat = targetImg.GetPropertyItem(2);
                return ExifGpsToDouble(propItemRef, propItemLat);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Cannot get property Latitude");
            }
        }
        public static double GetLongitude(Image targetImg)
        {
            try
            {
                ///Property Item 0x0003 - PropertyTagGpsLongitudeRef
                PropertyItem propItemRef = targetImg.GetPropertyItem(3);
                //Property Item 0x0004 - PropertyTagGpsLongitude
                PropertyItem propItemLong = targetImg.GetPropertyItem(4);
                return ExifGpsToDouble(propItemRef, propItemLong);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Cannot get property Longitude");
            }
        }
    }
}
