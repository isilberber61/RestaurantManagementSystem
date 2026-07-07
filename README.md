# 🍽️ Restaurant Management System

A desktop-based Restaurant Management System developed using C# Windows Forms and SQLite.

The application provides a centralized interface for managing restaurant operations such as products, categories, staff, and tables. It includes user authentication, database operations, reusable form components, dynamic navigation, search functionality, and product price visualization.

## 📌 Project Overview

The Restaurant Management System is a Windows desktop application designed to manage essential restaurant data through a simple and centralized user interface.

The application uses SQLite as its local database management system and C# Windows Forms for the graphical user interface.

Different management screens are dynamically loaded into the main application panel, allowing users to navigate between modules without opening separate application windows.

## ✨ Features

* User authentication
* SQLite database integration
* Product management
* Category management
* Staff management
* Table management
* Search functionality
* Database CRUD operations
* Dynamic form navigation
* Reusable view and data entry forms
* Product price visualization
* Parameterized SQL queries
* Database error handling

## 🔐 User Authentication

The application starts with a login screen.

Users must enter a valid username and password before accessing the main management interface.

The authentication process:

* Retrieves username and password input
* Validates empty fields
* Connects to the SQLite database
* Checks user credentials in the `Users` table
* Uses parameterized SQL queries
* Displays appropriate success or error messages
* Opens the main application interface after successful authentication

The application entry point is configured to launch the `LoginForm` when the program starts.

## 🏠 Main Dashboard

After successful authentication, users are redirected to the main application interface.

The dashboard provides access to the following modules:

* Home
* Staff
* Categories
* Products
* Tables
* Charts

The application uses a central panel structure for navigation.

Instead of opening each form in a separate window, the selected form is dynamically loaded into the `CenterPanel`.

This approach provides a more consistent desktop application experience.

## 🏡 Home Page

The Home section provides a simple welcome screen for the Restaurant Management System.

It displays general restaurant information such as:

* Available payment methods
* Minimum order amount
* Estimated delivery time

## 🗂️ Category Management

The category management module is designed to organize restaurant product categories.

It provides a database-connected structure for managing category records.

## 🍔 Product Management

The product management module is responsible for managing restaurant products stored in the SQLite database.

Product data can also be used by the chart module to visualize product prices.

## 👨‍🍳 Staff Management

The staff management section provides an interface for managing restaurant employee data.

It follows the reusable view and data entry structures implemented in the project.

## 🪑 Table Management

The table management module provides an interface for managing restaurant table records.

Like the other management modules, it is dynamically loaded into the main application panel.

## 🔎 Search Functionality

The project includes a reusable `SampleView` form structure.

This form provides common components used by management screens, including:

* Search input
* Search event
* Add button
* Header area

The search and add methods are implemented as virtual methods, allowing derived forms to provide their own functionality.

This structure helps reduce duplicated interface code between different management modules.

## ➕ Reusable Data Entry Forms

The project includes a reusable `SampleAdd` form structure for data entry operations.

The form contains common components such as:

* Header panel
* Header label
* Save button
* Close button
* Bottom action panel

The save and close events are implemented as virtual methods.

This allows other data entry forms to extend the base form and implement module-specific database operations.

## 📊 Product Price Visualization

The application includes a chart module for visualizing product prices.

The chart system connects to the SQLite database and executes a query to retrieve:

* Product names
* Product prices

The retrieved data is displayed using a column chart.

The visualization process includes:

1. Opening the SQLite database connection
2. Retrieving product names and prices from the `Products` table
3. Clearing existing chart data
4. Creating a new chart series
5. Reading database records
6. Adding product names and prices to the chart
7. Displaying the results as a column chart

## 🗄️ Database Integration

The application uses SQLite as its local database management system.

The database connection is configured using:

```text
RestoranYönetimi.db
```

The project contains reusable database methods for common database operations.

These operations include:

* Executing SQL queries
* Adding SQL parameters
* Loading database records
* Binding data to DataGridView controls
* Filling ComboBox controls
* Managing SQLite connections
* Handling database exceptions

## 🔄 CRUD Operations

The application infrastructure supports common CRUD operations:

* Create
* Read
* Update
* Delete

Reusable SQL execution methods are used to perform database operations.

Parameters are passed to SQL commands using `SQLiteParameter`-based operations to provide a structured approach to database queries.

## 🏗️ Application Architecture

The application uses multiple Windows Forms components and reusable base forms.

### Program

`Program.cs` contains the main entry point of the application.

The application starts by launching:

```text
LoginForm
```

### LoginForm

Responsible for:

* User authentication
* SQLite database connections
* SQL query execution
* Loading database records
* DataGridView data binding
* ComboBox data loading

### MainForm

Acts as the main application container.

It is responsible for:

* Application navigation
* Loading management forms
* Managing the central content panel
* Opening the Home screen
* Opening management modules
* Opening the Chart screen
* Closing the application

### frmHome

Provides the application's welcome and restaurant information screen.

### frmChart

Retrieves product information from the SQLite database and visualizes product prices using a column chart.

### SampleView

Provides a reusable base structure for management and data listing forms.

### SampleAdd

Provides a reusable base structure for data entry forms.

## 🛠️ Technologies Used

* C#
* .NET Framework
* Windows Forms
* SQLite
* ADO.NET
* System.Data.SQLite
* Windows Forms DataVisualization
* Visual Studio

## 📂 Project Structure

```text
Restaurant-Management-System/
│
├── Program.cs
│
├── App.config
├── packages.config
├── WindowsFormsApp12.csproj
│
├── Form1.cs
├── Form1.Designer.cs
├── Form1.resx
│
├── MainForm.cs
├── MainForm.Designer.cs
├── MainForm.resx
│
├── frmHome.cs
├── frmHome.Designer.cs
├── frmHome.resx
│
├── frmChart.cs
├── frmChart.Designer.cs
├── frmChart.resx
│
├── SampleView.cs
├── SampleView.Designer.cs
├── SampleView.resx
│
├── SampleAdd.cs
├── SampleAdd.Designer.cs
├── SampleAdd.resx
│
├── ChartValues.cs
│
├── RestoranYönetimi.db
│
└── README.md
```

## ⚙️ Installation and Setup

### 1. Clone the repository

```bash
git clone <repository-url>
```

### 2. Open the project

Open the project file using Visual Studio:

```text
WindowsFormsApp12.csproj
```

### 3. Restore NuGet packages

Restore the required dependencies defined in:

```text
packages.config
```

Make sure the SQLite dependencies required by the application are installed.

### 4. Add the database

Make sure the SQLite database file exists in the application's working directory:

```text
RestoranYönetimi.db
```

### 5. Build the project

Build the project using Visual Studio.

### 6. Run the application

Start the application.

The application will automatically open the Login screen.

After successful authentication, the main Restaurant Management System interface will be displayed.

## 🚀 Future Improvements

Possible improvements for the project include:

* Password hashing
* Role-based authorization
* Order management
* Reservation management
* Inventory management
* Sales tracking
* Advanced reporting
* Dashboard statistics
* Database backup and recovery
* Improved exception handling
* Improved UI/UX design
* Separation of database logic into a dedicated data access layer
* Migration to a layered architecture

## 🎯 Project Purpose

This project was developed to practice and demonstrate:

* C# programming
* Windows desktop application development
* Windows Forms
* Event-driven programming
* SQLite database integration
* SQL queries
* CRUD operations
* User authentication
* Data binding
* Reusable UI components
* Dynamic form navigation
* Data visualization
* Database error handling

## 👩‍💻 Author

**Işıl Berber**

Computer Engineering Graduate

Interested in Software Development, Backend Development, Data Engineering, Machine Learning, and Artificial Intelligence.

## 📄 License

This project was developed for educational and portfolio purposes.
