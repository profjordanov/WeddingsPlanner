# WeddingsPlanner
Application for monitoring marriages around Bulgaria.


## Technology Stack:
- [x] C# 7.1
- [x] .NET Core Web API v2.1
- [x] EntityFramework Core with SQL Server and ASP.NET Identity
- [x] Apache Cordova
- [x] jQuery Mobile

### Test Suite
- [x] SQL Database Integration Testing
- [x] Arrange Act Assert Pattern
- [x] xUnit
- [x] Autofixture
- [x] Moq
- [x] Shouldly


## Features:

### Web API
- [x] AutoMapper
- [x] File logging with Serilog
- [x] JWT authentication/authorization
- [x] Stylecop
- [x] Neat folder structure

```
├───src
|   |___clients
|       ├───jQuery.Client
|       ├───WeddingsPlanner.Mobile
│   ├───configuration
│   └───server
│       ├───WeddingsPlanner.Api
│       ├───WeddingsPlanner.Business
│       ├───WeddingsPlanner.Core
│       ├───WeddingsPlanner.Data
│       └───WeddingsPlanner.Data.EntityFramework
└───tests
    └───WeddingsPlanner.Business.Tests

```
- [x] Global Model Errors Handler <br>
- [x] Global Environment-Dependent Exception Handler <br>
- [x] Neatly organized solution structure <br>
- [x] Thin Controllers

```csharp
/// POST: /Account/Login
/// <summary>
/// Login.
/// </summary>
[HttpPost]
public async Task<IActionResult> Login(CredentialsModel model)
    => (await _usersService.LoginAsync(model))
        .Match(RedirectToLocal, ErrorLogin);
```

- [x] Robust service layer using the [Either](http://optional-github.com) monad. <br>

## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites
1. You'll need version `2.1.4` of the [`.NET Core SDK`](https://dotnet.microsoft.com/download).

2. If not, you'll need to have [SQLServer](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) either installed locally or at least have some instance available to set up the connection strings.

3. For the mobile application, you will need [`NodeJS`](https://nodejs.org/en/).

### Running Using Visual Studio

1. Download it via `CLONE OR DOWNLOAD BUTTON`
2. Unzip the project
3. Open the `.sln` file using Visual Studio
4. Set up the connection strings inside `WeddingsPlanner.Api/appsettings.Development.json` (or leave the set)
5. Execute `Update-Database` inside the `Package Manager Console`
6. Run the FamousQuoteQuiz.Api
7. Go to src/client/WeddingsPlanner.Mobile
8. Open it via Visual Studio

