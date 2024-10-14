
namespace TDDBank
{
    public class BankAccount
    {
        public decimal Balance { get; private set; }

        public void Deposit(decimal v)
        {
            if(v <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive");
            }

            Balance += v;
        }

        public void Withdraw(decimal v)
        {
            if(v <= 0)
            {
                throw new ArgumentException("Withdraw amount must be positive");
            }
            if (v > Balance)
            {
                throw new InvalidOperationException("Withdraw amount must be less than balance");
            }

            Balance -= v;
        }
    }
}
