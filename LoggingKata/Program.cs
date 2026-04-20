using System;
using System.Linq;
using System.IO;

namespace LoggingKata
{
    class Program
    {
        
        static readonly ILog logger = new TacoLogger();     
        const string csvPath = "TacoBell-US-AL.csv";        

        /// <summary>
        /// Reads Taco Bell location data from a supplied datafile and finds the maximum geographical spread between two Taco Bells among the data
        /// using 'TacoParser', 'GeoCoordinatePortable.GeoCoordinate' and 'System.IO.File' classes.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            
            logger.LogInfo("Log initialized");

            var lines = new string[] { };
            try
            {
                // Reading the supplied file using 'System.IO.File' class that returns the 'string[]'
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
                Console.ReadLine();
                return;
            }
            catch (UnauthorizedAccessException ex)
            {
                logger.LogFatal("Access Denied:", ex);
                Console.ReadLine();
                return;
            }
            catch (IOException ex)
            {
                logger.LogFatal("File is locked.", ex);
                Console.ReadLine();
                return;
            }
            catch (Exception ex)
            {
                logger.LogFatal("Exception occured.", ex);
                Console.ReadLine();
                return;
            }

            var parser = new TacoParser();
            parser.parsingCounter = 0;  // Reset the Data Parsing Counter

            // Form the recordset and eliminate incorrect records
            var tacoBells = lines.Select(parser.Parse).Where(t => t != null).ToArray();

            if (tacoBells.Length > 0)
            {
                Console.Write("\n\n");
                logger.LogInfo($"SUPPLIED NO OF RECORDS: {lines.Length}\n      SUCCESSFULLY EXTRACTED RECORDS: {tacoBells.Length}\n\n");
            }
            else
            {
                logger.LogFatal("No records in correct format.", new Exception("Operation aborted!!!"));
                Console.ReadLine();
                return;
            }


            ITrackable tacobellOne = null;
            ITrackable tacobellTwo = null;

            double farthestDistanceBetweenTacoBells = 0;

            // Iterating to find the maximum distance between two Taco Bells
            for (int i = 0; i < tacoBells.Length; i++)
            {
                ITrackable locA = tacoBells[i];

                var corA = locA.Location;

                for (int j = i; j < tacoBells.Length; j++)
                {

                    ITrackable locB = tacoBells[j];

                    var corB = locB.Location;

                    var newDistance = parser.ParseDistance(corA, corB);

                    if (newDistance >= farthestDistanceBetweenTacoBells)
                    {
                        tacobellOne = locA;
                        tacobellTwo = locB;
                        farthestDistanceBetweenTacoBells = newDistance;
                        logger.LogInfo($"New farthest distance of {newDistance} miles: {locA.Name} ({locA.Location.Latitude}, {locA.Location.Longitude}) --> {locB.Name} ({locB.Location.Latitude}, {locB.Location.Longitude})");
                    }
                }
            }

            // Displaying the final outcome:
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("\n\n--------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{tacobellOne.Name} and {tacobellTwo.Name} are located farthest to each other at {farthestDistanceBetweenTacoBells} miles distance.");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
            Console.ReadLine();
        }
    }
}