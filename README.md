# RealEstateProject 
* SDK Net Core 6
* Database MSFT SQL Server
* Entityframework Core - Code First
* NUnit

## When you start the app:
Run intialization process:
 * The database will be create
 * Migrations will be applied
 * And seed data will be inserted
	
> Note: if you don't want this approach, go to program.cs in RealEstate.Api project and comment this line:
```csharp
   await app.InitializeDatabaseAsync();
```

## Endpoints basic flow:
 1. Create a user app from POST /User endpoint
 2. Do login in POST /User/Login endpoint
 3. In this moment, you can use the others endpoints

## Endpoints authorization:
* User endpoints:

| Endpoint                      |  Description                         | Authorization        |
|:------------------------------|:-------------------------------------|:---------------------|
| `POST` User                   | Create user                          | Anonymous            |
| `POST` User/AddUserAdminRole  | Add admin role to specified user     | JWT token basic user |
| `POST` User/Login             | Generate JWT token for login         | Anonymous            |
| `DELETE` User                 | Delete user                          | JWT token admin user |

* Property Building endpoints:   
 
| Endpoint                      |  Description                         | Authorization        |
|:------------------------------|:-------------------------------------|:---------------------|
| `POST` Property               | Create property                      | JWT token basic user |
| `POST` Property/AddImage      | Add image to property                | JWT token basic user |
| `PUT` Property/ChangePrice    | Change property price                | JWT token admin user |
| `PUT` Property                | Update other property fields         | JWT token basic user |
| `GET` Property                | Get property list with filters       | Anonymous            |


