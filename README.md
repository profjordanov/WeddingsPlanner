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
- [x] Thin Controllers <br>
- [x] Robust service layer using the [Either](http://optional-github.com) monad. <br>
