using UserManagementApp.Core;
using UserManagementApp.Repositories;
using UserManagementApp.Services;

Console.WriteLine("Welcome to User Management System");
Console.WriteLine("Type your commands below (type 'exit' to quit):");

var userRepository = new FileUserRepository();
var userService = new UserService(userRepository);
var commandHandler = new CommandHandler(userService);

while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
        continue;

    if (input.Trim().ToLower() == "exit")
    {
        Console.WriteLine("Exiting the program...");
        break;
    }

    commandHandler.Handle(input);
}
