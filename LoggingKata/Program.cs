using System;

using System.Linq;
using System.IO;
using GeoCoordinatePortable;


namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            
            logger.LogInfo("Log initialized");

            // Use File.ReadAllLines(path) to grab all the lines from your csv file. 
            // Optional: Log an error if you get 0 lines and a warning if you get 1 line
            var lines = new string[] { };
            try
            {

                lines = File.ReadAllLines(csvPath);

                if (lines.Length == 0)
                {
                    logger.LogError("There are no records to process!!!", new Exception("Empty Source!!!"));
                    return;
                }

                if (lines.Length == 1)
                {
                    string message = lines[0].Length > 75 
                                        ? "It may be possible that supplied source data not in correct format."
                                        : "You are checking two Taco Bells at the same location!!!";
                    
                    logger.LogWarning(message);

                    string userInput = "";
                    do
                    {
                        Console.Write("Do you want to proceed? Please answer 'Y' or 'N': ");
                        userInput = Console.ReadLine().Trim().ToLower();
                    } while (!(userInput == "y" || userInput == "n"));

                    if (userInput == "n")
                    {
                        return;
                    }
                }

            }
            catch (FileNotFoundException ex)
            {
                logger.LogFatal("File not found at specified path.", ex);
                return;
            }
            catch (UnauthorizedAccessException ex)
            {
                logger.LogFatal("Access Denied:", ex);
                return;
            }
            catch (IOException ex)
            {
                logger.LogFatal("File is locked.", ex);
                return;
            }
            catch (Exception ex)
            {
                logger.LogFatal("Exception occured.", ex);
                return;
            }

            // This will display the first item in your lines array
            // logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Use the Select LINQ method to parse every line in lines collection
            // var tacoBells = lines.Select(line => parser.Parse(line)).Where(t => t != null).ToArray();
            var tacoBells = lines.Select(parser.Parse).Where(t => t != null).ToArray();

            if (tacoBells.Length > 0)
            {
                Console.Write("\n\n");
                logger.LogInfo($"SUPPLIED NO OF RECORDS: {lines.Length}\n      SUCCESSFULLY EXTRACTED RECORDS: {tacoBells.Length}\n\n");
            }
            else
            {
                logger.LogFatal("No records in correct format.", new Exception("Operation aborted!!!"));
                return;
            }

            // Complete the Parse method in TacoParser class first and then START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. 
            // These will be used to store your two Taco Bells that are the farthest from each other.
            ITrackable tacobellOne = null;
            ITrackable tacobellTwo = null;

            // TODO: Create a `double` variable to store the distance
            double farthestDistanceBetweenTacoBells = 0;

            // TODO: Add the Geolocation library to enable location comparisons: using GeoCoordinatePortable;
            // Look up what methods you have access to within this library.

            // NESTED LOOPS SECTION----------------------------

            // FIRST FOR LOOP -
            // TODO: Create a loop to go through each item in your collection of tacoBells.
            // This loop will let you select one location at a time to act as the "starting point" or "origin" location.
            // Naming suggestion for variable: `locA`

            for (int i = 0; i < tacoBells.Length; i++)
            {
                ITrackable locA = tacoBells[i];

                // TODO: Once you have locA, create a new Coordinate object called `corA` with your locA's latitude and longitude.
                var corA = locA.Location;
                GeoCoordinate geoLocA = new GeoCoordinate(corA.Latitude, corA.Longitude);

                // TODO: Now, Inside the scope of your first loop, create another loop to iterate through tacoBells again.
                // This allows you to pick a "destination" location for each "origin" location from the first loop. 
                // Naming suggestion for variable: `locB`

                for (int j = i; j < tacoBells.Length; j++)
                {

                    ITrackable locB = tacoBells[j];

                    // TODO: Once you have locB, create a new Coordinate object called `corB` with your locB's latitude and longitude.
                    var corB = locB.Location;
                    GeoCoordinate geoLocB = new GeoCoordinate(corB.Latitude, corB.Longitude);

                    // TODO: Now, still being inside the scope of the second for loop, compare the two tacoBells using `.GetDistanceTo()` method, which returns a double.
                    // If the distance is greater than the currently saved distance, update the distance variable and the two `ITrackable` variables you set above.

                     var newDistance = Math.Round(geoLocA.GetDistanceTo(geoLocB) / 1609.344, 2);    // 'divide by 1609.344': Gets the distance in Miles

                    if (newDistance >= farthestDistanceBetweenTacoBells)
                    {
                        tacobellOne = locA;
                        tacobellTwo = locB;
                        farthestDistanceBetweenTacoBells = newDistance;
                        logger.LogInfo($"New farthest distance of {newDistance} miles found between Taco Bells: {locA.Name} --> {locB.Name}.");
                    }

                }
            }

            // NESTED LOOPS SECTION COMPLETE ---------------------

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
            // Display these two Taco Bell locations to the console.
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("\n\n--------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{tacobellOne.Name} and {tacobellTwo.Name} are located farthest to each other at {farthestDistanceBetweenTacoBells} miles distance.");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
            Console.ReadLine();
        }
    }
}
