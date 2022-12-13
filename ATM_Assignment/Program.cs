using System;
using System.IO;
using System.Runtime.CompilerServices;
 

namespace ATM_Assignment
{

  public  class Person {
        private string firstName;
        private string lastName;
        public Person()
        {
            firstName = "Null";
            lastName = "Null";
        }
        public Person(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }


        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
    }
    public class BankAccount 
    {
        private Person person;
        private string email;
        private string cardNumber;
        private string pinCode;
        private int accountBalance;
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
           
            BankAccount p = (BankAccount)obj;
            if (this.person == p.Person && this.email == p.email && this.cardNumber == p.cardNumber && this.pinCode == p.pinCode && this.accountBalance == p.accountBalance)
                return true;
            else
                return false;

        }
        public Person Person { get => person; set => person = value; }
        public string Email { get => email; set => email = value; }
        public string CardNumber { get => cardNumber; set => cardNumber = value; }
        public string PinCode { get => pinCode; set => pinCode = value; }
        public int AccountBalance { get => accountBalance; set => accountBalance = value; }
        
        public BankAccount(Person person, string email, string cardNumber, string pinCode, int accountBalance)
        {
            this.Person = person;
            this.Email = email;

            //cardNumber
            if (cardNumber.Length == 9)
            {
                this.CardNumber = cardNumber;
            }
            else 
            {
                Console.WriteLine("error: cardNumber is not valid");
            }


            // pinCode
            if (pinCode.Length == 4)
            {
                this.PinCode = pinCode;
            }
            else
            {
                Console.WriteLine("error: pinCode is not valid");
            }

            this.AccountBalance = accountBalance;

        }

    }

  public class Bank
    {
        private int bankCapacity;
        public  int NumberOfCustomers = 0;
        private BankAccount[] bankAccounts;



        public Bank()
        {
            this.bankCapacity = 0;
        }
        public Bank(int bankCapacity)
        {
            this.bankCapacity = bankCapacity;
            bankAccounts = new BankAccount[bankCapacity];

        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            if (base.Equals(obj))
                return true;
            else
                return false;
            

            

        }
        public void AddNewAccount(BankAccount tmpAccount) 
        {
            if (NumberOfCustomers < bankCapacity)
            {
                bankAccounts[NumberOfCustomers] = tmpAccount;
                NumberOfCustomers++;
            }
            else
            {
                Console.WriteLine("ERROR!");
            }
        }
        
        // is A bank user
        public bool IsBankUser(string cardNumber, string pinCode) {

            // CHECK IF CARDNUMBER VALID 
            for (int i = 0; i < NumberOfCustomers; i++) {
                if (bankAccounts[i].CardNumber.Equals(cardNumber) && bankAccounts[i].PinCode.Equals(pinCode)) {
                    return true;
                }
                
            }

            Console.WriteLine("Account not found!");
            return false;
        }

        public int CheckBalance(string cardNumber, string pinCode) 
        {
            
            for (int i = 0; i < NumberOfCustomers; i++)
            {
                if (bankAccounts[i].CardNumber.Equals(cardNumber) && bankAccounts[i].PinCode.Equals(pinCode)) {
                    return bankAccounts[i].AccountBalance;
                }
            }

            Console.WriteLine("Account not found!");
            return 0;
        }

        public void Withdraw(BankAccount tmpAccount, int withdrawAmount)
        {
            for (int i = 0; i < NumberOfCustomers; i++)
            {
                if (bankAccounts[i].CardNumber.Equals(tmpAccount.CardNumber) && 
                    bankAccounts[i].PinCode.Equals(tmpAccount.PinCode) &&
                    bankAccounts[i].Email.Equals(tmpAccount.Email))
                {
                    if (bankAccounts[i].AccountBalance >= withdrawAmount)
                    {
                        bankAccounts[i].AccountBalance = bankAccounts[i].AccountBalance - withdrawAmount;
                    }
                    else 
                    {
                        bankAccounts[i].AccountBalance = 0;
                        Console.WriteLine(" You dont have enought money to draw!");
                    }
                }
            }
        }
        public void Deposit(BankAccount tmpAccount, int withdrawAmount)
        {
            for (int i = 0; i < NumberOfCustomers; i++)
            {
                if (bankAccounts[i].CardNumber.Equals(tmpAccount.CardNumber) &&
                    bankAccounts[i].PinCode.Equals(tmpAccount.PinCode) &&
                    bankAccounts[i].Email.Equals(tmpAccount.Email))
                {
                    bankAccounts[i].AccountBalance = bankAccounts[i].AccountBalance + withdrawAmount;
                }
            }
        }

        public async void Save() 
        {
            // (Person person, string email, string cardNumber, string pinCode, int accountBalance)
            string[] lines = new string[NumberOfCustomers];
            for (int i = 0; i < NumberOfCustomers; i++) 
            {
                lines[i] = bankAccounts[i].Person.FirstName + "," + bankAccounts[i].Person.LastName + ","
                    + bankAccounts[i].Email + ","
                    + bankAccounts[i].CardNumber + ","
                    + bankAccounts[i].PinCode + ","
                    + bankAccounts[i].AccountBalance.ToString();
            }
            
            File.WriteAllLines("bank.txt", lines);
        }

        // jamal, mutawe, jamal@gmail.com, 123456789, 1234, 1000 
        public void Load()
        {
            bankAccounts = new BankAccount[bankCapacity];
            
            NumberOfCustomers = 0;

            var lines = File.ReadLines("bank.txt");
            foreach (string line in lines) { 
                string[] strs = line.Split(',');
                Person person = new Person(strs[0], strs[1]);

                string email = strs[2];
                string cardNumber = strs[3];
                string pinCode = strs[4];
                string accountBalance = strs[5];

                // (Person person, string email, string cardNumber, string pinCode, int accountBalance)
                BankAccount bankAccount = new BankAccount(person, email, cardNumber, pinCode, Convert.ToInt32(accountBalance));
                bankAccounts[NumberOfCustomers] = bankAccount;
                NumberOfCustomers++;
            }

        }
    }
    
     class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
