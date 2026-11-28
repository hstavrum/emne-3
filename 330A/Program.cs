namespace _330A
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank myBank = new Bank();

            // Add Customers
            Customer c1 = new Customer("C001", "Ola Nordmann");
            Customer c2 = new Customer("C002", "Kari Nordmann");
            myBank.AddCustomer(c1);
            myBank.AddCustomer(c2);

            // Add Accounts
            myBank.CreateAccount("C001", "ACC_101", 1000m);
            myBank.CreateAccount("C002", "ACC_202", 500m);

            myBank.DisplayBalances();

            // Transfer money, Kari  sends 200 to Ola
            myBank.Transfer("ACC_101", "ACC_202", 200m);

            myBank.DisplayBalances();
        }
    }

    public class Customer
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }

        // Ensure that as soon as a Customer object is created, it automatically starts with an
        // empty list ready to use.
        public List<BankAccount> Accounts { get; set; } = new List<BankAccount>();

        public Customer(string id, string name)
        {
            CustomerId = id;
            Name = name;
        }
    }

    public class BankAccount
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; private set; } // Private setter to ensure balance is only changed via methods
        public Customer Owner { get; set; }

        public BankAccount(string accountNumber, decimal initialBalance, Customer owner)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
            Owner = owner;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Innskuddsbeløp må være positivt.");
            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Beløp for uttak må være positivt.");
            if (Balance < amount)
            {
                Console.WriteLine($"Ikke nok på konto {AccountNumber}.");
                return false;
            }
            Balance -= amount;
            return true;
        }
    }

    public class Bank
    {
        private List<Customer> _customers = new List<Customer>();
        private List<BankAccount> _allAccounts = new List<BankAccount>();

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
            Console.WriteLine($"Kundeen {customer.Name} ble opprettet.");
        }

        public BankAccount CreateAccount(string customerId, string accountNumber, decimal initialDeposit)
        {
            var customer = _customers.Find(c => c.CustomerId == customerId);
            if (customer == null) throw new Exception("Kunde ikke funnet.");

            var newAccount = new BankAccount(accountNumber, initialDeposit, customer);
            customer.Accounts.Add(newAccount);
            _allAccounts.Add(newAccount);

            Console.WriteLine($"Konto {accountNumber} opprettet for {customer.Name}.");
            return newAccount;
        }

        public bool Transfer(string sourceAccNum, string destAccNum, decimal amount)
        {
            var source = _allAccounts.Find(a => a.AccountNumber == sourceAccNum);
            var destination = _allAccounts.Find(a => a.AccountNumber == destAccNum);

            if (source == null || destination == null)
            {
                Console.WriteLine("En eller begge kontoer er ugyldige.");
                return false;
            }

            if (source.Withdraw(amount))
            {
                destination.Deposit(amount);
                Console.WriteLine($"Overførte {amount:C} fra {sourceAccNum} til {destAccNum}.");
                return true;
            }

            return false;
        }

        public void DisplayBalances()
        {
            Console.WriteLine("\n--- Saldoer ---");
            foreach (var acc in _allAccounts)
            {
                Console.WriteLine($"Konto: {acc.AccountNumber} | Eier: {acc.Owner.Name} | Saldo: {acc.Balance:C}");
            }
        }
    }
}
