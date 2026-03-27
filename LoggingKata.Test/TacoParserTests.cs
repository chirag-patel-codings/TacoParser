using System;
using Xunit;

namespace LoggingKata.Test
{
    /// <summary>
    /// This class (Blue-Print) has the implementation of all the tests required for 'Test Driven Development (TDD)' of Taco-Parser project.
    /// </summary>
    /// <remarks>
    /// Note: It is NOT possible to implement all the tests for TDD for all the parts like GUI tests are more complex to write and maintain as GUIs 
    ///       are changed frequenty and incresing the complexities and time to write tests. For the parts where test cannot be implemented or 
    ///       contains complexity for testing, can be tested manually but their (test) volume must be very low than compared to test written using TDD.
    /// </remarks>
    public class TacoParserTests
    {
        [Fact]      // No Test data to supply
        public void ShouldReturnNonNullObject()
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638,-84.677017,Taco Bell Acwort...");

            //Assert
            Assert.NotNull(actual);

        }

        [Theory]    // Tested with supplied test data
        [InlineData("34.073638,-84.677017,Taco Bell Acwort...",-84.677017)]     // Test data
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
    }
}
