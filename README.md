# eFruit
eFruit is the new e-commerce platform which aims to modernize and digitalize the whole fruit industry.
Implemented using:-
  - VS 2017.
  - SQL Server 2017
  - .NET Core 2.0

## Architecture
- eFruit is implemented in Onion architecture applying SOLID principles.
- Using StyleCop for static code analysis.

### Grocery.API.eFruitService
- Main Apis for eFruit, implemented in Web Api .NET Core 2.0.
#### Frameworks
- AutoMapper for mapping between DAO and DTO.

### Grocery.Code.Tasks.ProductRefresher
- Scheduled task to refresh products list from External REST service.
#### Frameworks
- PeterKottas.DotNetCore.WindowsService as .NET Core doesn't have implementation for Windows Service.

### Grocery.Core.Data.Model
- Data Models for eFruit.

### Grocery.Core.Data
- Data layer to deal with SQL Database implemented in EF Core Code First with integration with Asp.NET Identity Database applying patterns like:-
  - Unit Of Work.
  - Repository.
#### Frameworks
- EntityFramewWorkCore.

### Grocery.Core.Extension
Shared functionality across eFruit
#### Frameworks
- Newtonsoft.Json.

### Grocery.Core.Service
- Services layer between Database and UI/API.
- Post orders to External WCF Service.

### Grocery.Web.eFruit
- eCommerce Application for eFruit implemented in Asp.NET Mvc Core.
- Has functionalties:-
  - Register/Login using Microsoft Identity.
  - External Auth using Google.
  - View paged order list.
  - Add product to cart.
  - View cart items.
  - Place orders.
  - View order history.

## Testing
- Sample of unit tests for projects
#### Frameworks
- xunit.
- Moq

## Deployment
1- Download solution.

2- Update database connection in:-
  - Grocery.Web.eFruit\appsettings.json
  - Grocery.API.eFruitService\appsettings.json
  - Grocery.Core.Data\eFruitEntities.cs

3- Run Update-Database in Package Manager for project Console Grocery.Core.Data.

4- Publish Grocery.Web.eFruit.

5- Publish Grocery.API.eFruitService.

6- Install Grocery.Core.Tasks.ProductRefresher as a Windows Service.
  - Run the service with action:install and it will install the service.
  - Run the service without arguments and it runs like console app.
  - Refer to installation guide for more details [a link](https://github.com/PeterKottas/DotNetCore.WindowsService)

7- Enjoy :)
