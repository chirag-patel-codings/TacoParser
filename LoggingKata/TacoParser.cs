using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file's records one-by-one (supplied as a Single line) to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {

            Console.WriteLine();
            logger.LogInfo($"Begin parsing: {line}");

            var cells = line.Split(',');

            // If your array's Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                // Log error message and return null
                logger.LogError($"Insufficient information for parsing!!!", new Exception("Incorrect input data format!!!"));
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

                logger.LogInfo($"Successfully parsed details for: {name}.");

            }
            catch (FormatException ex)
            {
                string errorSource = latitude == 1000 ? "latitude" : "longitude";
                logger.LogError($"Error occured for '{errorSource}' conversion!!!", ex);
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured!!!", ex);
                return null;
            }

            Point locationCoOrdinates = new Point();
            locationCoOrdinates.Latitude = latitude;
            locationCoOrdinates.Longitude = longitude;

            TacoBell tacoBell = new TacoBell(name, locationCoOrdinates);

            return tacoBell;
        }
    }
}
