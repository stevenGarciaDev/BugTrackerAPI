# BugTracks API

This is a portfolio application used to showcase my skill in C#, ASP.NET Core, Entity Framework Core, and SQL Server.

BugTracks allows users to create an account, create projects, and create tickets for managing
their project tasks.

The deployed application can be found at the following URL, [https://bugtracks.azurewebsites.net/](https://bugtracks.azurewebsites.net/).

The API has been deployed to Azure and setup to use CI/CD with Azure Pipelines.

The GitHub for the React UI can be found at the following [link](https://github.com/stevenGarciaDev/BugTrackerUI) which was developed using React, Styled Components, and Redux.

## Description of API

The features implemented accomplish the goals of making the Minimum Viable Product of a Bug Tracking app.

The API allows authenticated clients to create an account, create a project, and create task tickets.

The controllers utilized include an AccountsController, ProjectsController, TicketsController, and UsersController.

## Skills Learned

The skills I demonstrate in this project include the following.

* Implement ASP.NET Core API endpoints for a React client to make request to
* Use interfaces to implement the Repository pattern and the Unit of Work pattern to achieve decoupling
* Use Fluent API to specify the database relationships between resources
* Use LINQ and Entity Framework Core as the Object Relational Mapper to a SQL Server database
* Deploy to Azure and setup CI/CD with Azure Pipelines through the Classic Editor

## Structure of Code

NOTE: This is a minimum viable product, and I will be implementing more features to the API soon.

I implemented the project using the Repository and Unit of Work pattern through the use of interfaces to help abstract the Entity Framework Core object relational mapper.

This helps decouple the controllers from Entity Framework Core.

The implemented interface for the repository can be found [here](https://github.com/stevenGarciaDev/BugTrackerAPI/blob/main/BugTrackerAPI/Data/Repository.cs).

The class that implements the Unit of Work pattern interface can be found [here](https://github.com/stevenGarciaDev/BugTrackerAPI/blob/main/BugTrackerAPI/Data/UnitOfWork.cs).

An example of an ASP.NET Core controller can be found here for the ProjectsController. [Here](https://github.com/stevenGarciaDev/BugTrackerAPI/blob/main/BugTrackerAPI/Controllers/ProjectsController.cs)

<hr >

Thank you for checking out my project.

Steven Garcia

* Website: [https://stevengarciadev.github.io/](https://stevengarciadev.github.io/)
* LinkedIn: [https://www.linkedin.com/in/stevengarciadev/](https://www.linkedin.com/in/stevengarciadev/)
