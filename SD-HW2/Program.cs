using SD_HW2;
using SD_HW2.BankAccount;
using SD_HW2.Category;
using SD_HW2.FileWork;
using SD_HW2.Operation;
using Type = SD_HW2.Category.Type;

var bankFactory = new BankAccountFactory();
var categoryFactory = new CategoryFactory();
var operationFactory = new OperationFactory();

var categoryRepo = new CategoryRepository(categoryFactory);
var bankRepo = new BankAccountRepository(bankFactory);
var opRepo = new OperationRepository(operationFactory);

ConsoleService consoleService = new ConsoleService(categoryRepo, bankRepo, opRepo);
consoleService.ShowMainMenu();