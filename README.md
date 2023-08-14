# ToDoList

## Frontend
### Requierments
- Angular CLI: 16.1.4
- Node: 18.14.2
- Package Manager: npm 9.8.0
### How to run
- Navigate to `~/Frontend` folder
- Run `npm install`
- Run `ng serve --configuration=development`
## Backend 
### Requierments
- Dotnet SDK 6+
### How to run
- Navigate to `~/Backend` folder
- Run `dotnet restore`
- Provide database connection string in `appsettings.json` if needed
- Run `dotnet ef database update --project ToDoList.Infrastructure`
- Run `dotnet run`
### NuGet packages used
- FluentValidation
- BCrypt.Net-Next
