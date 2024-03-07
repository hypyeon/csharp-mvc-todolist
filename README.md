# Friend Letter
by Hayeong Pyeon

## Description
- This app is created based on a class material and as a review of **Database Basics** chapter of **C#** course provided by Epicodus.
- This application is a continuing work from **Basic Web Application** course - recorded in previous commits.  
- `.cs` files have notes and practices that are commented out for author's personal learning purpose. 

## Setup Instructions
*learning purpose*
1. Clone this repo.
2. Open your shell (e.g., Terminal or GitBash) and navigate to this project's production directory named **ToDoList**. 
3. Within the production directory **ToDoList**, create a new file called `appsettings.json`.
4. Within `appsettings.json`, put in the following code, replacing the `uid` and `pwd` values with your own username and password for MySQL. *For the LearnHowToProgram.com lessons, we always assume the `uid` is `root` and the `pwd` is `epicodus`.*
```
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=to_do_list_with_ef_core;uid=root;pwd=epicodus;"
  }
}
```
5. Within **ToDoList**, the production directory, run `dotnet watch run` in the command line to start the project in development mode with a watcher.
6. Open the browser to _https://localhost:5001_. 
- Make sure to have configured a .NET developer security certificate for HTTPS.
- If the access in the browser is denied for certification authorization reasons, run `dotnet dev-certs https --trust` in the command line. 
- To see all possible commands and tolls for `dev-certs`, run `dotnet dev-certs https --help` in the command line. 