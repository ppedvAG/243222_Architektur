using Microsoft.QualityTools.Testing.Fakes;

namespace TDDBank.Tests
{
    public class OpeningHoursTests
    {
        [Theory]
        [InlineData(2024, 10, 14, 10, 30, true)]  // Mo, 10:30
        [InlineData(2024, 10, 14, 10, 29, false)] // Mo, 10:29
        [InlineData(2024, 10, 14, 10, 31, true)]  // Mo, 10:31
        [InlineData(2024, 10, 14, 18, 59, true)]  // Mo, 18:59
        [InlineData(2024, 10, 14, 19, 00, false)] // Mo, 19:00 (außerhalb)
        [InlineData(2024, 10, 15, 12, 00, true)]  // Di, 12:00
        [InlineData(2024, 10, 18, 12, 00, true)]  // Di, 12:00
        [InlineData(2024, 10, 19, 10, 30, true)] // Sa, 10:30
        [InlineData(2024, 10, 19, 14, 0, false)] // Sa, 14:00 (außerhalb)
        [InlineData(2024, 10, 19, 12, 0, true)]  // Sa, 12:00
        [InlineData(2024, 10, 19, 9, 0, false)]  // Sa, 9:00 (vor Öffnung)
        [InlineData(2024, 10, 20, 12, 0, false)] // So, 12:00 (geschlossen)
        public void OpeningHours_IsOpen(int year, int month, int day, int hour, int minute, bool expected)
        {
            // Arrange
            var dt = new DateTime(year, month, day, hour, minute, 0);
            var openingHours = new OpeningHours();

            // Act
            var result = openingHours.IsOpen(dt);

            // Assert
            Assert.Equal(expected, result);
        }


        //ChatGPT 
        [Theory]
        [InlineData("2024-10-14 11:00", true)] // Montag, 11:00 Uhr (innerhalb der Öffnungszeiten)
        [InlineData("2024-10-14 20:00", false)] // Montag, 20:00 Uhr (außerhalb der Öffnungszeiten)
        [InlineData("2024-10-12 11:00", true)] // Samstag, 11:00 Uhr (innerhalb der Öffnungszeiten)
        [InlineData("2024-10-12 15:00", false)] // Samstag, 15:00 Uhr (außerhalb der Öffnungszeiten)
        [InlineData("2024-10-13 12:00", false)] // Sonntag, geschlossen
        public void IsOpen_ValidatesCorrectly(string dateTimeString, bool expected)
        {
            // Arrange
            var openingHours = new OpeningHours();
            var zeit = DateTime.Parse(dateTimeString);

            // Act
            var result = openingHours.IsOpen(zeit);

            // Assert
            Assert.Equal(expected, result);
        }


        [Fact]
        public void IsWeekend()
        {
            using var context = ShimsContext.Create();

            var oh = new OpeningHours();

            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 10, 7); 
            Assert.False(oh.IsWeekend());//Mo
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 10, 8); 
            Assert.False(oh.IsWeekend());//Di
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 10, 9); 
            Assert.False(oh.IsWeekend());//Mi
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 10, 10); 
            Assert.False(oh.IsWeekend());//Do
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 10, 11); 
            Assert.False(oh.IsWeekend());//Fr
            
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 10, 12); 
            Assert.True(oh.IsWeekend()); //Sa
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 10, 13); 
            Assert.True(oh.IsWeekend()); //So

        }
    }
}
