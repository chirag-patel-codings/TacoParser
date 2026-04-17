using System;
using System.Net.NetworkInformation;
using Xunit;

namespace LoggingKata.Test
{
    /// <summary>
    /// This class (Blue-Print) has the implementation of all the tests required for 'Test Driven Development (TDD)' of Taco-Parser project.
    /// </summary>
    public class TacoParserTests
    {
        [Fact]
        public void ShouldReturnNonNullObject()
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638,-84.677017,Taco Bell Acwort...");

            //Assert
            Assert.NotNull(actual);

        }

        [Theory]
        [InlineData("34.073638,-84.677017,Taco Bell Acwort...",-84.677017)]
        [InlineData("33.671982,-85.826674,Taco Bell Annisto...", -85.826674)]
        [InlineData("33.587217,-85.057114,Taco Bell Carrollto...", -85.057114)]
        [InlineData("30.22841,-85.649286,Taco Bell Lynn Have...", -85.649286)]
        [InlineData("31.777472,-85.936073,Taco Bell Tro...", -85.936073)]
        [InlineData("32.524115,-86.20775,Taco Bell Wetumpk...", -86.20775)]
        public void ShouldParseLongitude(string line, double expected)
        {
        
            //Arrange
            TacoParser tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse(line).Location.Longitude;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("34.073638,-84.677017,Taco Bell Acwort...", 34.073638)]
        [InlineData("33.671982,-85.826674,Taco Bell Annisto...", 33.671982)]
        [InlineData("33.587217,-85.057114,Taco Bell Carrollto...", 33.587217)]
        [InlineData("30.22841,-85.649286,Taco Bell Lynn Have...", 30.22841)]
        [InlineData("31.777472,-85.936073,Taco Bell Tro...", 31.777472)]
        [InlineData("32.524115,-86.20775,Taco Bell Wetumpk...", 32.524115)]
        public void ShouldParseLatitude(string line, double expected)
        {

            //Arrange
            TacoParser tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse(line).Location.Latitude;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new double[] {34.376395,-84.913185}, new double[] {34.035985, -84.683302}, 26.96)]
        [InlineData(new double[] {33.22997, -86.805275}, new double[] {34.035985, -84.683302}, 134.29)]
        [InlineData(new double[] {34.035985, -84.683302}, new double[] {30.903723, -84.556037}, 216.73)]
        [InlineData(new double[] {30.906033, -87.79328}, new double[] {34.035985, -84.683302}, 282.4)]
        [InlineData(new double[] {34.376395, -84.913185}, new double[] {30.654113, -87.912144}, 311.15)]
        [InlineData(new double[] {30.39371, -87.68332}, new double[] {34.376395, -84.913185}, 319.37)]
        public void ShouldConfirmDistanceCalculation(double[] coOrdinateA, double[] coOrdinateB, double expected)
        {
            //Arrange
            TacoParser tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.ParseDistance(new Point(coOrdinateA[0], coOrdinateA[1]), new Point(coOrdinateB[0], coOrdinateB[1]));
            
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
