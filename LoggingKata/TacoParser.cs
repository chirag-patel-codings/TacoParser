using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file's records one-by-one (supplied as a Single line) to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        // Generates a new TacoBell object by extracting details from the supplied string line.
        public ITrackable Parse(string line)
        {

            Console.WriteLine();
            logger.LogInfo($"Begin parsing: {line}");

            // Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
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
                // TODO: Grab the latitude from your array at index 0
                // You're going to need to parse your string as a `double`
                // which is similar to parsing a string as an `int`
                latitude = double.Parse(cells[0].Trim());

                // TODO: Grab the longitude from your array at index 1
                // You're going to need to parse your string as a `double`
                // which is similar to parsing a string as an `int`
                longitude = double.Parse(cells[1].Trim());

                // TODO: Grab the name from your array at index 2
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

            // TODO: Create an instance of the Point Struct
            // TODO: Set the values of the point correctly (Latitude and Longitude) 
            Point locationCoOrdinates = new Point(latitude, longitude);

            // TODO: Create an instance of the TacoBell class
            // TODO: Set the values of the class correctly (Name and Location)
            TacoBell tacoBell = new TacoBell(name, locationCoOrdinates);

            // TODO: Then, return the instance of your TacoBell class,
            // since it conforms to ITrackable

            return tacoBell;
        }
    }
}
