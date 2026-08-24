
using System.Numerics;

namespace Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        private Money(decimal amount, string currency)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency can not be empty");
            Amount = amount;
            Currency = currency;
        }
        public static Money Create(decimal amount, string currency)
        {
            return new Money(amount, currency);
        }
        public static Money operator +(Money left, Money right)
        {
            if (left.Currency != right.Currency)
                throw new ArgumentException("cannot add money with different currencies");
            return new Money(left.Amount + right.Amount, right.Currency);
        }
        public static Money operator -(Money left, Money right)
        {
            if (left.Currency != right.Currency)
                throw new ArgumentException("cannot add money with different currencies");
            return new Money(left.Amount - right.Amount, right.Currency);
        }
        public static Money operator *(Money money, decimal mul)
        {
            return Money.Create(money.Amount * mul, money.Currency);
        }
        public override string ToString()
        {
            return $"{Amount}:{Currency}";
        }
    }
}
