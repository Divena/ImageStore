using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImageStore.Models
{
    public class EXIFHelper
    {
        private BitmapMetadata bitmapMetadata;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public string Comment { get; set; }
        public EXIFHelper(Stream pic)
        {
            BitmapDecoder decoder = JpegBitmapDecoder.Create(pic, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);
            bitmapMetadata = (BitmapMetadata)decoder.Frames[0].Metadata.Clone();
            Getlatitude();
            Getlongitude();
            
            GetComment();
        }
        private void Getlatitude()
        {
            byte[] gpsVersionNumbers = bitmapMetadata.GetQuery(GPS_VERSION_QUERY) as byte[];
            bool strangeVersion = (gpsVersionNumbers != null && gpsVersionNumbers[0] == 2);
            ulong[] latitudes = bitmapMetadata.GetQuery(LATITUDE_QUERY) as ulong[];
            if (latitudes != null)
            {
                double latitude = ConvertCoordinate(latitudes, strangeVersion);
                // N or S                         
                string northOrSouth = (string)bitmapMetadata.GetQuery(NORTH_OR_SOUTH_QUERY);
                if (northOrSouth == "S")
                {
                    // South means negative latitude.
                    latitude = -latitude;
                }
                this.Latitude = latitude;
            }
        }
        private void Getlongitude()
        {
            byte[] gpsVersionNumbers = bitmapMetadata.GetQuery(GPS_VERSION_QUERY) as byte[];
            bool strangeVersion = (gpsVersionNumbers != null && gpsVersionNumbers[0] == 2);
            ulong[] longitudes = bitmapMetadata.GetQuery(LONGITUDE_QUERY) as ulong[];
            if (longitudes != null)
            {
                double longitude = ConvertCoordinate(longitudes, strangeVersion);
                // E or W                          
                string eastOrWest = (string)bitmapMetadata.GetQuery(EAST_OR_WEST_QUERY);
                if (eastOrWest == "W")
                {
                    // West means negative longitude. 
                    longitude = -longitude;
                }
                this.Longitude = longitude;
            }
        }
        private double ConvertCoordinate(ulong[] coordinates, bool strangeVersion)
        {
            int degrees;
            int minutes;
            double seconds;
            if (strangeVersion)
            {
                degrees = (int)splitLongAndDivide(coordinates[0]);
                minutes = (int)splitLongAndDivide(coordinates[1]);
                seconds = splitLongAndDivide(coordinates[2]);
            }
            else
            {
                degrees = (int)(coordinates[0] - DEGREES_OFFSET);
                minutes = (int)(coordinates[1] - MINUTES_OFFSET);
                seconds = (double)(coordinates[2] - SECONDS_OFFSET) / 100.0;
            }
            double coordinate = degrees + (minutes / 60.0) + (seconds / 3600);
            double roundedCoordinate = Math.Floor(coordinate * COORDINATE_ROUNDING_FACTOR) / COORDINATE_ROUNDING_FACTOR;
            return roundedCoordinate;
        }

        private static double splitLongAndDivide(ulong number)
        {
            byte[] bytes = BitConverter.GetBytes(number);
            int int1 = BitConverter.ToInt32(bytes, 0);
            int int2 = BitConverter.ToInt32(bytes, 4);
            return ((double)int1 / (double)int2);
        }

        private void GetComment()
        {
            this.Comment = this.bitmapMetadata.Comment;
        }

        #region Private Constants

        private const string DATE_TAKEN_QUERY = "/app1/ifd/{ushort=306}";
        private const string LATITUDE_QUERY = "/app1/ifd/gps/subifd:{ulong=2}";
        private const string LONGITUDE_QUERY = "/app1/ifd/gps/subifd:{ulong=4}";
        private const string NORTH_OR_SOUTH_QUERY = "/app1/ifd/gps/subifd:{char=1}";
        private const string EAST_OR_WEST_QUERY = "/app1/ifd/gps/subifd:{char=3}";
        private const string GPS_VERSION_QUERY = "/app1/ifd/gps/";
        private const long DEGREES_OFFSET = 0x100000000;
        private const long MINUTES_OFFSET = 0x100000000;
        private const long SECONDS_OFFSET = 0x6400000000;
        private const double COORDINATE_ROUNDING_FACTOR = 1000000.0;
        #endregion
    }
}