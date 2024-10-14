namespace Calculator.Tests
{
    public class CalcTests : IDisposable
    {

        [Fact]
        [Trait("Category", "Unit")]
        public void Sum_2_and_3_results_5()
        {
            //Arrage
            var calc = new Calc();

            //Act
            var result = calc.Sum(2, 3);

            //Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Sum_0_and_0_results_0()
        {
            //Arrage
            var calc = new Calc();

            //Act
            var result = calc.Sum(0, 0);

            //Assert
            Assert.Equal(0, result);
        }


        [Fact]
        public void Sum_MAX_and_1_throws_OverflowException()
        {
            var calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(1, -2, -1)]
        [InlineData(0, 0, 0)]
        [InlineData(5000, 2000, 7000)]
        public void Sum_with_result(int a, int b, int exp)
        {
            var calc = new Calc();

            var result = calc.Sum(a, b);

            Assert.Equal(exp, result);
        }

        [Theory]
        [InlineData(int.MaxValue, 1)]
        [InlineData(int.MinValue, -1)]
        public void Sum_throws_OverflowException(int a, int b)
        {
            var calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(a, b));
        }

        public void Dispose()
        {

        }
    }
}
