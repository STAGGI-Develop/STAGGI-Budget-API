![Banner](https://github.com/STAGGI-Develop/STAGGI-Budget-Backend/assets/129558962/41d5b5ae-6261-429c-bbb2-8803fe940bca)

# STAGGI Budget 

**STAGGI Budget** is a web application designed to help you keep a detailed track of your expenses, budget appropriately, and plan your savings in a categorized manner.

![Main_Page](https://github.com/STAGGI-Develop/STAGGI-Budget-Backend/assets/129558962/b6cfbdc4-34c6-4fe1-8673-5851d20e6425)

## Features 

- **Expense Tracking**: Easily add and categorize your monthly expenses.
- **Budgeting**: Set up a monthly and weekly budget and stay on top of your finances.
- **Visualizations**: View your expenses for the month and the previous week through charts.
- **Savings Project**: Plan and categorize your savings projects for future goals.

## Technologies Used 

- **Backend**: .NET 6.0.
- **Security** : ASP.Net Core Identity.
- **Authentication**: JWT.
- **Containerization**: Docker.
- **Deployment**: Azure.
- **Frontend**: React with Chakra UI.
- **Database**: SQL Server.
- **ORM**: Entity Framework Core.
- **Queries**: LINQ.

## Layers

STAGGI Budget layers and components help to structure the code in a clean and maintainable manner, promoting the separation of concerns and making it easier to test and scale the application as it grows. 
STAGGI Budget contains the following layers:

**Controllers** 

Controllers are responsible for handling HTTP requests, executing the necessary business logic, and returning appropriate responses, typically in the form of views or data. 

**Data**

Enums are a distinct value type which defines a set of named constants representing underlying integral values. In software projects, they are used to give meaningful names to a set of related values, making the code more readable and maintainable.

**DTOs**

 DTOs are plain objects that are used to transport data between processes or layers. In ASP.NET projects, they are often used to shape data that's sent or received through an API, ensuring that unnecessary data isn't exposed or that data is structured in a certain way for clients.

**Enums**

Enums are a distinct value type which defines a set of named constants representing underlying integral values. In software projects, they are used to give meaningful names to a set of related values, making the code more readable and maintainable.

**Helpers**

Helpers typically consist of static methods or extension methods that provide common functionalities which can be reused throughout the application. In ASP.NET, helper methods might assist with tasks like data formatting, input validation, or other utility functions.

**Models**

Models represent the data structure, business logic, and the rules for how data can be modified or processed. They can map directly to database tables, but they can also represent more complex structures or processes. They are the backbone of the application, containing the main logic and data.

**Repositories**

Repositories provide an abstraction layer between the data access and the business logic layers of an application. They encapsulate the logic required to access data sources, ensuring that the methods to manage the data are consistent and maintainable. In many ASP.NET projects, repositories work closely with Entity Framework or other ORMs.

**Services**

Services typically encapsulate specific business logic or application functions. They can be used to organize and share code across controllers, or between different parts of an application. In many ASP.NET projects, a service layer stands between the controllers and the repositories, ensuring that any business rules or additional logic are applied before data is saved or retrieved.

## Advantages of Repository Pattern

The Repository Pattern promotes a cleaner, more maintainable, and more scalable codebase by organizing and centralizing data operations. However, as with any design pattern, it's essential to understand the specific needs of your project to determine if it's the right fit. Some of the advantage are:

**Separation of Concerns:**

By separating the data access logic from the rest of the application, there's a clear boundary which makes the system more modular. This allows developers to work on the data layer independently of the business logic layer.
Abstraction over Data Access Logic:

The pattern hides the details of data retrieval and storage. Thus, the application code can remain agnostic to the underlying data source or the intricacies of database interactions.

**Testability:**

With a well-implemented Repository Pattern, it's easier to mock the data layer, enabling unit tests to run against the business logic without involving actual data operations.
Centralized Data Logic:

Having a centralized place for data access logic can lead to better code maintainability. This means you won't find scattered SQL queries or data access codes throughout your application.

**Flexibility:**

If in the future you decide to change your data source, be it moving from one type of database to another or even switching to a completely different data storage method, the impact on the rest of your application would be minimal. You would only need to modify the repository layer.

**Consistent Data Access Rules:**

By centralizing data operations in repositories, you ensure that all data access rules and business validations are consistently enforced whenever data is accessed.

**Caching:**

Repositories can serve as a layer where caching strategies are implemented. This can improve performance by reducing unnecessary database calls.

**Improved Collaboration:**

In bigger teams, while one group of developers works on data access logic, another group can work on business logic or user interfaces without stepping on each other's toes.

**Code Reusability:**

Common data operations that are used frequently throughout the application can be encapsulated within the repository, promoting code reuse.

**Unified API:**

Repository provides a consistent API to the data store. This ensures that developers have a unified method of accessing data, regardless of the underlying data source or storage mechanism.


## Work Methodology 
![Trello Board](https://github.com/STAGGI-Develop/STAGGI-Budget-Backend/assets/129558962/04c4891d-55de-4f0e-8dc4-6aad90c045af)


For the development of this project, we adopted the **Agile** methodology using Trello as our task management tool.

## Building and Deploying 

Staggi Budget is available as a prebuilt **Docker** image [here](https://hub.docker.com/r/gastonr1/budget-api)

