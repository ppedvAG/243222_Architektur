namespace TDDBank.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void New_BankAccount_should_have_zero_as_blanace()
        {
            var ba = new BankAccount();

            Assert.Equal(0, ba.Balance);
        }

        [Fact]
        public void Deposit_should_increase_balance()
        {
            var ba = new BankAccount();

            ba.Deposit(100m);

            Assert.Equal(100, ba.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Deposit_a_negative_or_zero_amount_should_throw_ArgumentEx(decimal amount)
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Deposit(amount));
        }

        [Fact]
        public void Withdraw_should_decrease_balance()
        {
            var ba = new BankAccount();
            ba.Deposit(100m);
            ba.Withdraw(50m);
            Assert.Equal(50, ba.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Withdraw_a_negative_or_zero_amount_should_throw_ArgumentEx(decimal amount)
        {
            var ba = new BankAccount();
            Assert.Throws<ArgumentException>(() => ba.Withdraw(amount));
        }

        [Fact]
        public void Withdraw_more_than_balance_should_throw_InvalidOperationEx()
        {
            var ba = new BankAccount();
            ba.Deposit(100m);
            Assert.Throws<InvalidOperationException>(() => ba.Withdraw(101m));
        }
    }
}
