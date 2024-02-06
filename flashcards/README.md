# Flashcard Stack Manager
Flashcard Stack Manager is a C# application designed to help users create, manage, and study flashcard stacks efficiently. It utilizes SQL Server for data storage and incorporates Data Transfer Objects (DTOs) to streamline data presentation.

## Features
- ### Stack Management:
Users can create and manage stacks of flashcards, each with a unique name.
- ### Flashcard Management:
Users can add, edit, delete flashcards within a stack. Flashcards are numbered sequentially, starting from 1, without any gaps.
- ### Study Sessions:
Users can conduct study sessions to review flashcard stacks. Each session is recorded with a date and score.
- ### Data Integrity:
Deleting a stack also deletes associated flashcards and study sessions.

## Technologies Used
- ### C# for application logic
- ### SQL Server for database management
- ### Entity Framework Core for ORM (Object-Relational Mapping)
