# User Management Console App

A clean and layered **C# console application** for managing users using a command-based interface.

## Features

- Register a new user
- Login and logout
- Change user status (available / not available)
- Change user password
- Search users by username
- File-based storage (no database)
- Fully layered architecture (Services, Repositories, Interfaces, Core, etc.)
- Proper error handling for all commands

## Commands Syntax

All commands follow a structured format:

Command --key1 value1 --key2 value2

### Register

Register --username sara --password 1234

- True Output: `register successful.`
- False Output: `register failed! username already exists.`

### Login


- True Output: `login successful. welcome sara.`
- False Output: `login failed! invalid username or password.`

### Change Status

Change --status available
Change --status not available


### Change Password

ChangePassword --old 1234 --new 4321


### Search Users

Search --username sa


Output example:

sara | status: available
sam | status: not available


### Logout

Logout


## Folder Structure

UserManagementApp/
├── Core/
│ ├── CommandHandler.cs
│ └── SessionManager.cs
├── Interfaces/
│ └── IUserRepository.cs
├── Models/
│ └── User.cs
├── Repositories/
│ └── FileUserRepository.cs
├── Services/
│ └── UserService.cs
├── Utils/
│ └── FileHandler.cs
├── Data/
│ └── users.txt
├── Program.cs
└── README.md

## How to Run

1. Clone the repository:
git clone https://github.com/goat-debug/UserManagementApp.git

2. Open the project in Visual Studio.

3. Build the project and run it:

4. Use commands in the console as shown above.

## Author

Made with ❤️ by [goat-debug](https://github.com/goat-debug)

> If you found this project helpful, feel free to ⭐ star the repository!
