using System;
using GeoCoordinatePortable;    // Library

namespace LoggingKata
{
    
    /// <summary>
    /// Provides the functionality to parse the Tacobell data. CENTER-POINT OF THE WHOLE APPLICATION.
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();    

        public int parsingCounter = 0;

        /// <summary>
        /// This function parses the supplied string and returns the 'ITrackable' object with 'longitude', 'latitude' and 'Name' members
        /// </summary>
        /// <param name="line">Contains string comma seperated values.</param>
        /// <returns>Returns 'TacoBell' Object with properties value assigned or 'null' if values are not parsed correctly.</returns>
        public ITrackable Parse(string line)
        {
            parsingCounter++;

            var cells = line.Split(',');

            // If your array's Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                // Log error message and return null
                logger.LogError($"Insufficient information for parsing for record no: {parsingCounter}!!!\n{line}", new Exception("Incorrect input data format!!!"));
                return null; 
            }

            double latitude = 1000;     // Max value 180
            double longitude = -1000;   // Max value -180
            string name = "";

            try
            {
                latitude = double.Parse(cells[0].Trim());

                longitude = double.Parse(cells[1].Trim());

                name = cells[2].Trim();
            }
            catch (FormatException ex)
            {
                string errorSource = latitude == 1000 ? "latitude" : "longitude";
                logger.LogError($"Error occured for '{errorSource}' value conversion for the record no: {parsingCounter}!!!\n{line}", ex);
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured for the record no: {parsingCounter}!!!\n{line}!!!", ex);
                return null;
            }

            Point locationCoOrdinates = new Point();
            locationCoOrdinates.Latitude = latitude;
            locationCoOrdinates.Longitude = longitude;

            TacoBell tacoBell = new TacoBell(name, locationCoOrdinates);

            return tacoBell;
        }

        /// <summary>
        /// Calculates and returns the distance between two geographic locations by utilizing "Haversine formula" with the precision loss of < 0.1% for long distances
        /// </summary>
        /// <param name="corA"></param>
        /// <param name="corB"></param>
        /// <returns></returns>
        public double ParseDistance(Point corA, Point corB)
        {
            
            GeoCoordinate geoLocA = new GeoCoordinate(corA.Latitude, corA.Longitude);
            GeoCoordinate geoLocB = new GeoCoordinate(corB.Latitude, corB.Longitude);

            // 'divide by 1609.344': Gets the distance in Miles
            return Math.Round(geoLocA.GetDistanceTo(geoLocB) / 1609.344, 2);
        }

    }
}
