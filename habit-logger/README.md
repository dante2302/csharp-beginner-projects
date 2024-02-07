
# Habit Tracker Application
## Introduction
Console based CRUD application designed to track habits and routines effectively. Developed using C# and SQLite

## Features
- ### Habit Management
Register One Habit: Users can register one habit to track. This habit is quantifiable and cannot be tracked by time (e.g., hours of sleep), only by quantity (e.g., number of water glasses consumed per day).
Database Interaction
- ### SQLite Database: The application stores and retrieves data from a SQLite database. Upon application start, it creates a SQLite database if one isn’t present.

- ### Table Creation: The application creates a table in the database to log the habit.

## User Interface
- ### Menu Options: The app provides users with a menu of options to interact with their habit.
- ### CRUD Operations
- Insert
- Delete
- Update
-  View 
## Error Handling
All possible errors are handled to ensure the application never crashes, providing a smooth user experience.
## Termination
The application terminates only when the user inserts 0, ensuring a user-friendly experience.
## Database Interaction
 - ### Raw SQL:
   Interaction with the database is done using raw SQL. Mappers such as Entity Framework are not used.






