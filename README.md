# PawList Back-end
This is a back-end application developed in .NET Core 8. It consumes data from a public API called "The Dog API" and processes that data into a PostgreSQL database, enabling users to perform CRUD operations along with authentication and security features. The front-end application can be accessed at: Front-end URL.

### Overview
PawList is an application made to consume Data from a public API called "The Dog API", take that data, process it into a PostgreSQL Database to let the user do CRUD operations. Also implements authentication and security functionalities.

### Project Features

#### Namespace Division
The project is organized in folders for better class management and maintainability.

#### Entity Framework
Entity Framework was used for data persistence operations to streamline processes and also for the creation of the DataBase using Migrations. Automatic mapping is handled in the `AppDbContext` class.

#### JWT
For security, JSON Web Token (JWT) is used to manage user authentication.

#### Data Encryption
Sensitive data such as user passwords are encrypted to ensure data security in the database.

#### Testing
An xUnit testing project is included to verify the correct functionality of individual classes, independently evaluating potential system responses.

### Integration of AI
The integration of AI (ChatGPT and Canva) was crucial for implementing project features and optimizing the development process. ChatGPT helped in a very significant way to make the developing process a lot faster than usual, to implement functionalities like data encryption and the use of JWT. Canva's on the other hand, helped with its AI features to create the Logo of PawList.