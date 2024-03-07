# To Do List
by Hayeong Pyeon

## Description
- This app is created based on a class material and as a review of **Basic Web Applications** chapter of **C#** course provided by Epicodus. 
- `.cs` files have notes and practices that are commented out for author's personal learning purpose. 
- Initial setup done via git cloning from [here](https://github.com/epicodus-lessons/section-2-to-do-list-csharp-net6/tree/1_mvc_setup) (`1_mvc_setup` branch). 

## Setup Instructions
*learning purpose*
1. Clone this repo.
2. Open your shell (e.g., Terminal or GitBash) and navigate to this project's production directory named **ToDoList**. 
3. Within **ToDoList**, the production directory, create a new file named `appsettings.json`. 
4. Within `appsettings.json` file, put in the following code, replacing `uid` and `pwd` values with your own username and password for MySQL.
```
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=to_do_list_with_mysqlconnector;uid=[YOUR-USERNAME];pwd=[YOUR-PASSWORD];",
      "TestConnection": "Server=localhost;Port=3306;database=to_do_list_with_mysqlconnector_test;uid=[YOUR-USERNAME];pwd=[YOUR-PASSWORD];"
  }
}
```
5. Run `dotnet watch run` in the command line to start the project in development mode with a watcher.
6. Open the browser to _https://localhost:5001_. 
- Make sure to have configured a .NET developer security certificate for HTTPS.
- Run `dotnet dev-certs https --trust` in the command line *if* the access in the browser is denied for certification authorization reasons. 
- To see all possible commands and tolls for `dev-certs`, run `dotnet dev-certs https --help` in the command line. 