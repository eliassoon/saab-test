# The Saab test application - a MVVM based WPF application 
This application demonstrates simple functionality of creating and removing bookings in a system.

## Prerequisites
You'll need both of these to run the application.
- Visual Studio (latest)
- Microsoft SQL Server (latest)

## Getting started
The application requires a database to store the data. Follow these steps to set up the database.

1.
Run the script 'SaabRentals.sql' to create and populate the database. The database does however no populate as it should. Cannot figure out why right now.

2.
Set the connection string

&ensp;&ensp;  i.        
  Open CarRental.sln (Visual Studio is required)

&ensp;&ensp;  ii.      
  Go to Properties in Solution Explorer

&ensp;&ensp;  iii.     
  Go to Settings.settings

&ensp;&ensp;  iv.    
  Insert Name as 'connString', Type as (Connection String), Scope as Application and Value as Connection String of Database.
  
  ## **Author**

[Marcus Eliasson](https://www.linkedin.com/in/marcus-eliasson-sj%C3%B6stedt-03113b152/)
