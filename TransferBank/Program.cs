using TransferBank;

var bank = new Bank();

var account1 = bank.CreateAccount();
var account2 = bank.CreateAccount();


await account1.TransferAsync(account2, 100, new TransactionProvider());

Console.ReadLine();