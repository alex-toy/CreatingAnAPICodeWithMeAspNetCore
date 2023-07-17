# Creating an API - Code with me Asp.Net Core

## Create project
```
dotnet new webapi -n SohatNotebook.Api
```

## In the Package Manager Console :
```
Add-Migration InitialCreate
Update-Database
```

## Adding JWT Authentication

### Add Packages
```
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.Identity.UI
```

### Register a user

<img src="/pictures/register.png" title="register a user"  width="900">
<img src="/pictures/register1.png" title="register a user"  width="900">

### Login

<img src="/pictures/login.png" title="login"  width="900">

### Add Authorize decorator

<img src="/pictures/authorize.png" title="authorize"  width="900">

You need to get a token to be authorized to retrieve data from the user controller

<img src="/pictures/authorize2.png" title="authorize"  width="900">