# 🎬 MovieDapper Admin Dashboard

[![.NET Version](https://img.shields.io/badge/.NET-10.0-blue.svg?style=for-the-badge&logo=.net)](https://dotnet.microsoft.com/)
[![ORM](https://img.shields.io/badge/ORM-Dapper-orange.svg?style=for-the-badge)](https://github.com/DapperLib/Dapper)
[![Database](https://img.shields.io/badge/Database-SQL_Server-red.svg?style=for-the-badge&logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)
[![UI Template](https://img.shields.io/badge/UI-AdminKit-teal.svg?style=for-the-badge)](https://adminkit.io/)
[![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)](LICENSE)

A robust, enterprise-ready administration panel for movie catalog and review management. Built with **ASP.NET Core MVC 10.0**, utilizing **Dapper** as the micro-ORM to achieve lightweight, high-performance database communication through clean, stored procedure-driven interactions. The front-end leverages the **AdminKit** dashboard template (Bootstrap 5) for a responsive, modern admin interface.

---

## 🛠️ Technology Stack

| Component | Technology | Version | Description |
| :--- | :--- | :--- | :--- |
| **Core Language** | C# | 10.0 | High-performance type-safe programming language |
| **Framework** | ASP.NET Core MVC | 10.0 (net10.0) | Model-View-Controller framework for web applications |
| **Data Access** | Dapper | 2.1.79 | Lightweight micro-ORM for mapping SQL queries directly to objects |
| **Database Engine** | MS SQL Server | LocalDB / Express / Standard | Relational database to store catalogs, users, and review data |
| **ADO.NET Provider**| Microsoft.Data.SqlClient | 7.0.1 | Official database client driver for SQL Server |
| **Styling & UI** | AdminKit (Bootstrap 5) | v3.x | Sleek, modern admin UI template with custom layouts and cards |
| **PDF Generation** | QuestPDF | 2026.6.1 | Open-source library for creating well-structured PDF reports |
| **Excel Exporting** | EPPlus | 8.6.1 | Library to programmatically generate xlsx spreadsheets |

---

## ✨ Key Features

*   📊 **Executive Analytics Dashboard**: Instant lookup of platform-wide statistics (total movies, reviews, genres, most profitable movie, best director, and top genres).
*   🔑 **Security & Session Management**: Self-contained user registration, login, logout, and change password workflows protected by session-based authentication guards.
*   🎥 **Movie Catalog & Director Directory**: Manage movies, directors, and genres including metadata such as release year, duration, budget, and revenue.
*   💬 **Moderated Review Platform**: Add, view, edit, or delete movie reviews with comment logs and rating systems.
*   📄 **Data Exporter Engine**:
    *   **PDF Rapor**: Custom page-designed PDF documents with tables, custom margins, and headers using `QuestPDF`.
    *   **Excel Spreadsheets**: Auto-fit columns, styled headers, and clean structures using `EPPlus`.

### User Interface Previews

#### 🖥️ Admin Dashboard
![Dashboard (Live)](https://github-production-user-asset-62135c.s3.amazonaws.com/placeholder-dashboard.png)
*Instruction: Upload a screenshot of the main analytics dashboard here showing movie metrics*

#### 📂 Movie Catalog Management
![Movie Management](https://github-production-user-asset-62135c.s3.amazonaws.com/placeholder-movies.png)
*Instruction: Upload a screenshot of the movie listings page showing grid view and action buttons*

---

## 🏗️ Architecture & Folder Structure

The project implements a classic **Model-View-Controller (MVC)** architectural pattern. Data access is centralized through a static `Context` helper class, separating database connections from core controller operations. 

```
MovieDapperProject/
├── .github/
│   └── workflows/
├── MovieDapperProject/
│   ├── Controllers/             # MVC Controllers handling requests and business logic
│   │   ├── DashboardController.cs
│   │   ├── DirectorsController.cs
│   │   ├── GenresController.cs
│   │   ├── HomeController.cs
│   │   ├── MoviesController.cs
│   │   ├── ReviewsController.cs
│   │   └── UsersController.cs
│   ├── Models/                  # C# DTOs, ViewModels and the Database Context
│   │   ├── Context.cs           # Dapper connection utility (Execute & Query mappings)
│   │   ├── DashboardModel.cs
│   │   ├── DirectorsModel.cs
│   │   ├── ErrorViewModel.cs
│   │   ├── GenresModel.cs
│   │   ├── MoviesModel.cs
│   │   ├── Reviews.cs
│   │   └── UsersModel.cs
│   ├── Views/                   # Razor Views (.cshtml templates)
│   │   ├── Dashboard/
│   │   ├── Directors/
│   │   ├── Genres/
│   │   ├── Home/
│   │   ├── Movies/
│   │   ├── Reviews/
│   │   ├── Shared/
│   │   └── Users/
│   ├── wwwroot/                 # Static assets (JS, CSS, AdminKit template)
│   │   ├── adminkit/
│   │   ├── css/
│   │   └── js/
│   ├── appsettings.json
│   ├── Program.cs               # App startup, DI configuration, and routes
│   └── MovieDapperProject.csproj
├── MovieDapperProject.slnx      # Solution file
└── README.md
```

---

## ⚙️ Setup & Installation Guide

### Prerequisites
Before running the application, make sure you have the following installed:
*   [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
*   [MS SQL Server](https://www.microsoft.com/sql-server/) (LocalDB, Developer, or Express Edition)
*   An IDE like Visual Studio 2022, Rider, or VS Code (with C# Dev Kit extension)

---

### 🗄️ Database Setup (Dapper Stored Procedures)

Since this project utilizes **Dapper with Stored Procedures** for all DB queries instead of EF Core Migrations, you must create the database, tables, and stored procedures manually in your SQL Server instance before running the app.

Follow the instructions below to set up your database:

1. Connect to your SQL Server instance using **SQL Server Management Studio (SSMS)** or Azure Data Studio.
2. Execute the following script to create the database and tables:

<details>
<summary><b>1. Click to view Table Schema Scripts</b></summary>

```sql
CREATE DATABASE MovieMvcDb;
GO

USE MovieMvcDb;
GO

-- 1. Genres Table
CREATE TABLE Genres (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

-- 2. Directors Table
CREATE TABLE Directors (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    BirthDate DATETIME NULL
);

-- 3. Movies Table
CREATE TABLE Movies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    ReleaseYear INT NOT NULL,
    Duration INT NOT NULL,
    Budget DECIMAL(18,2) NOT NULL,
    Revenue DECIMAL(18,2) NOT NULL,
    GenreId INT NOT NULL,
    DirectorId INT NOT NULL,
    CONSTRAINT FK_Movies_Genres FOREIGN KEY (GenreId) REFERENCES Genres(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Movies_Directors FOREIGN KEY (DirectorId) REFERENCES Directors(Id) ON DELETE CASCADE
);

-- 4. Users Table
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL UNIQUE,
    Email NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- 5. Reviews Table
CREATE TABLE Reviews (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MovieId INT NOT NULL,
    UserName NVARCHAR(100) NOT NULL,
    Rating INT NOT NULL,
    Comment NVARCHAR(MAX) NULL,
    ViewCount INT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    IsApproved BIT DEFAULT 0,
    CONSTRAINT FK_Reviews_Movies FOREIGN KEY (MovieId) REFERENCES Movies(Id) ON DELETE CASCADE
);
GO
```
</details>

3. Run the following script to register the **Stored Procedures** mapped by the controllers:

<details>
<summary><b>2. Click to view Stored Procedures Scripts</b></summary>

```sql
USE MovieMvcDb;
GO

-- =============================================
-- USER STORED PROCEDURES
-- =============================================
CREATE PROCEDURE sp_UserRegister
    @UserName NVARCHAR(100),
    @Email NVARCHAR(255),
    @PasswordHash NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Users (UserName, Email, PasswordHash, CreatedDate, IsActive)
    VALUES (@UserName, @Email, @PasswordHash, GETDATE(), 1);
END;
GO

CREATE PROCEDURE sp_UserLogin
    @UserName NVARCHAR(100),
    @PasswordHash NVARCHAR(MAX)
AS
BEGIN
    SELECT Id, UserName, Email, PasswordHash, CreatedDate, IsActive 
    FROM Users 
    WHERE UserName = @UserName AND PasswordHash = @PasswordHash AND IsActive = 1;
END;
GO

CREATE PROCEDURE sp_UserChangePassword
    @Id INT,
    @OldPasswordHash NVARCHAR(MAX),
    @NewPasswordHash NVARCHAR(MAX)
AS
BEGIN
    UPDATE Users 
    SET PasswordHash = @NewPasswordHash 
    WHERE Id = @Id AND PasswordHash = @OldPasswordHash;
END;
GO

-- =============================================
-- GENRE STORED PROCEDURES
-- =============================================
CREATE PROCEDURE sp_GenreViewAll
AS
BEGIN
    SELECT Id, Name FROM Genres;
END;
GO

CREATE PROCEDURE sp_GenreViewById
    @GenreId INT
AS
BEGIN
    SELECT Id, Name FROM Genres WHERE Id = @GenreId;
END;
GO

CREATE PROCEDURE sp_GenreEY
    @GenreId INT,
    @Name NVARCHAR(100)
AS
BEGIN
    IF @GenreId = 0 OR NOT EXISTS(SELECT 1 FROM Genres WHERE Id = @GenreId)
        INSERT INTO Genres (Name) VALUES (@Name);
    ELSE
        UPDATE Genres SET Name = @Name WHERE Id = @GenreId;
END;
GO

CREATE PROCEDURE sp_GenreSil
    @GenreId INT
AS
BEGIN
    DELETE FROM Genres WHERE Id = @GenreId;
END;
GO

-- =============================================
-- DIRECTOR STORED PROCEDURES
-- =============================================
CREATE PROCEDURE sp_DirectorViewAll
AS
BEGIN
    SELECT Id, FirstName, LastName, BirthDate FROM Directors;
END;
GO

CREATE PROCEDURE sp_DirectorViewById
    @DirectorId INT
AS
BEGIN
    SELECT Id, FirstName, LastName, BirthDate FROM Directors WHERE Id = @DirectorId;
END;
GO

CREATE PROCEDURE sp_DirectorEY
    @DirectorId INT,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @BirthDate DATETIME = NULL
AS
BEGIN
    IF @DirectorId = 0 OR NOT EXISTS(SELECT 1 FROM Directors WHERE Id = @DirectorId)
        INSERT INTO Directors (FirstName, LastName, BirthDate) VALUES (@FirstName, @LastName, @BirthDate);
    ELSE
        UPDATE Directors SET FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate WHERE Id = @DirectorId;
END;
GO

CREATE PROCEDURE sp_DirectorSil
    @DirectorId INT
AS
BEGIN
    DELETE FROM Directors WHERE Id = @DirectorId;
END;
GO

-- =============================================
-- MOVIE STORED PROCEDURES
-- =============================================
CREATE PROCEDURE sp_MovieViewAll
AS
BEGIN
    SELECT m.Id, m.Title, m.Description, m.ReleaseYear, m.Duration, m.Budget, m.Revenue, m.GenreId, m.DirectorId,
           g.Name AS GenreName, (d.FirstName + ' ' + d.LastName) AS DirectorName
    FROM Movies m
    INNER JOIN Genres g ON m.GenreId = g.Id
    INNER JOIN Directors d ON m.DirectorId = d.Id;
END;
GO

CREATE PROCEDURE sp_MovieViewById
    @MovieId INT
AS
BEGIN
    SELECT m.Id, m.Title, m.Description, m.ReleaseYear, m.Duration, m.Budget, m.Revenue, m.GenreId, m.DirectorId,
           g.Name AS GenreName, (d.FirstName + ' ' + d.LastName) AS DirectorName
    FROM Movies m
    INNER JOIN Genres g ON m.GenreId = g.Id
    INNER JOIN Directors d ON m.DirectorId = d.Id
    WHERE m.Id = @MovieId;
END;
GO

CREATE PROCEDURE sp_MovieEY
    @MovieId INT,
    @Title NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @ReleaseYear INT,
    @Duration INT,
    @Budget DECIMAL(18,2),
    @Revenue DECIMAL(18,2),
    @GenreId INT,
    @DirectorId INT
AS
BEGIN
    IF @MovieId = 0 OR NOT EXISTS(SELECT 1 FROM Movies WHERE Id = @MovieId)
        INSERT INTO Movies (Title, Description, ReleaseYear, Duration, Budget, Revenue, GenreId, DirectorId)
        VALUES (@Title, @Description, @ReleaseYear, @Duration, @Budget, @Revenue, @GenreId, @DirectorId);
    ELSE
        UPDATE Movies
        SET Title = @Title, Description = @Description, ReleaseYear = @ReleaseYear, Duration = @Duration,
            Budget = @Budget, Revenue = @Revenue, GenreId = @GenreId, DirectorId = @DirectorId
        WHERE Id = @MovieId;
END;
GO

CREATE PROCEDURE sp_MovieSil
    @MovieId INT
AS
BEGIN
    DELETE FROM Movies WHERE Id = @MovieId;
END;
GO

-- =============================================
-- REVIEW STORED PROCEDURES
-- =============================================
CREATE PROCEDURE sp_ReviewViewAll
AS
BEGIN
    SELECT Id, MovieId, UserName, Rating, Comment, ViewCount, CreatedDate, IsApproved
    FROM Reviews;
END;
GO

CREATE PROCEDURE sp_ReviewViewByMovieId
    @MovieId INT
AS
BEGIN
    SELECT Id, MovieId, UserName, Rating, Comment, ViewCount, CreatedDate, IsApproved
    FROM Reviews
    WHERE MovieId = @MovieId;
END;
GO

CREATE PROCEDURE sp_ReviewById
    @ReviewId INT
AS
BEGIN
    SELECT Id, MovieId, UserName, Rating, Comment, ViewCount, CreatedDate, IsApproved
    FROM Reviews
    WHERE Id = @ReviewId;
END;
GO

CREATE PROCEDURE sp_ReviewEY
    @ReviewId INT,
    @MovieId INT,
    @UserName NVARCHAR(100),
    @Rating INT,
    @Comment NVARCHAR(MAX)
AS
BEGIN
    IF @ReviewId = 0 OR NOT EXISTS(SELECT 1 FROM Reviews WHERE Id = @ReviewId)
        INSERT INTO Reviews (MovieId, UserName, Rating, Comment, ViewCount, CreatedDate, IsApproved)
        VALUES (@MovieId, @UserName, @Rating, @Comment, 0, GETDATE(), 1);
    ELSE
        UPDATE Reviews
        SET MovieId = @MovieId, UserName = @UserName, Rating = @Rating, Comment = @Comment
        WHERE Id = @ReviewId;
END;
GO

CREATE PROCEDURE sp_ReviewSil
    @ReviewId INT
AS
BEGIN
    DELETE FROM Reviews WHERE Id = @ReviewId;
END;
GO

-- =============================================
-- DASHBOARD STORED PROCEDURES
-- =============================================
CREATE PROCEDURE sp_Dash_TotalMovies
AS
BEGIN
    SELECT COUNT(*) FROM Movies;
END;
GO

CREATE PROCEDURE sp_Dash_TotalReviews
AS
BEGIN
    SELECT COUNT(*) FROM Reviews;
END;
GO

CREATE PROCEDURE sp_Dash_TotalGenres
AS
BEGIN
    SELECT COUNT(*) FROM Genres;
END;
GO

CREATE PROCEDURE sp_Dash_BestProfitMovie
AS
BEGIN
    SELECT TOP 1 Title, (Revenue - Budget) AS Profit
    FROM Movies
    ORDER BY Profit DESC;
END;
GO

CREATE PROCEDURE sp_Dash_BestDirector
AS
BEGIN
    SELECT TOP 1 (d.FirstName + ' ' + d.LastName) AS DirectorName, COUNT(m.Id) AS MovieCount
    FROM Movies m
    INNER JOIN Directors d ON m.DirectorId = d.Id
    GROUP BY d.FirstName, d.LastName
    ORDER BY MovieCount DESC;
END;
GO

CREATE PROCEDURE sp_Dash_BestGenre
AS
BEGIN
    SELECT TOP 1 g.Name, COUNT(m.Id) AS MovieCount
    FROM Movies m
    INNER JOIN Genres g ON m.GenreId = g.Id
    GROUP BY g.Name
    ORDER BY MovieCount DESC;
END;
GO
```
</details>

---

### ⚙️ Application Configuration

Database connection settings are configured directly inside `MovieDapperProject/Models/Context.cs`. 

Modify line 9 of [Context.cs](file:///c:/Users/Bora/Desktop/MovieDapperProject/MovieDapperProject/Models/Context.cs#L9-L10) if you need to point the application to a different server instance or modify connection parameters:

```csharp
public static string connectionString = 
    @"Server=DESKTOP-74G97I3\SQLEXPRESS;Database=MovieMvcDb;Trusted_Connection=True;TrustServerCertificate=True;";
```

> [!NOTE]
> Make sure your database server is running, and that the credential details mapped in the connection string match your server settings.

---

### 🚀 Running the Application Locally

1. Open your terminal in the root of the project directory.
2. Navigate to the web project folder:
   ```powershell
   cd MovieDapperProject
   ```
3. Restore the project packages:
   ```powershell
   dotnet restore
   ```
4. Build and run the project:
   ```powershell
   dotnet run
   ```
5. By default, the application runs on:
   - HTTPS: `https://localhost:7198` (or similar depending on system allocation)
   - HTTP: `http://localhost:5246`

---

### 👤 Testing Credentials
There are **no default admin credentials** pre-seeded into the database. 
To start testing:
1. Navigate to the application. It will automatically redirect you to the Login screen.
2. Click **Create Account** to register a new admin user (which populates the `Users` table via stored procedures).
3. Log in with your new credentials to access the main management panels.

---

## 📄 License & Attribution

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.

*Special thanks to the open-source creators of EPPlus, QuestPDF, Dapper, and the AdminKit theme.*
