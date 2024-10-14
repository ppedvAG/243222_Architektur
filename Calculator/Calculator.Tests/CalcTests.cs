namespace Calculator.Tests
{
    public class CalcTests
    {
        [Fact]
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
    }
}
