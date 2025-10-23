# Legatro Experimental - Time Tracking Application

A modern WinForms application for time tracking, built with .NET 9 and Entity Framework Core with SQLite.

## Overview

Legatro Experimental is a comprehensive time tracking application designed for managing projects, tasks, and time entries. It features automatic Windows authentication, flexible task grouping, and a modern UI with dark mode support.

## Features

### Core Functionality
- **Windows Authentication**: Automatic login using Windows credentials
- **First-Run Setup**: User Setup Assistant for initial configuration
- **Project Management**: Organize work into projects with time budgets
- **Task Management**: Create and track tasks within projects
- **Time Tracking**: Record time entries for tasks with detailed descriptions
- **Flexible Grouping**: Organize tasks using automatic date-based groups or manual assignment

### User Interface
- **Modern Design**: 11pt Segoe UI base font optimized for high-DPI displays
- **Dark Mode Support**: Automatic detection and adjustment for system dark mode
- **Persistent Layout**: Window positions and splitter settings saved between sessions
- **TreeView Navigation**: Hierarchical view of groups and tasks
- **Status Bar**: Real-time display of database, session time, and current time

### Data Management
- **SQLite Database**: Lightweight, file-based database (`.legatro` extension)
- **Automatic Backups**: Optional backup creation on application shutdown
- **Soft Deletes**: Records are marked as deleted rather than removed
- **Sync Support**: Built-in sync GUID for future synchronization features

## System Groups

The application includes predefined system groups:

1. **My Day** - Tasks created today (1-day range)
2. **Sliding Week** - Tasks from the last 7 days
3. **My Month** - Tasks from the last 30 days
4. **Roaming Links** - Manual group for reference links
5. **Roaming Notes** - Manual group for notes
6. **Done** - Completed tasks (Order: 65534)
7. **Clutter** - Miscellaneous items (Order: 65535)

## System Categories

10 predefined color categories are created on first run:
- Red, Orange, Yellow, Green, Blue, Indigo, Violet, Pink, Brown, Gray

## Database Schema

### Tables
- **Users** - Windows authenticated user information
- **Categories** - Color-coded categories for projects
- **Groups** - Task grouping with automatic or manual assignment
- **Projects** - Top-level work containers with time budgets
- **Tasks** - Individual work items with time tracking
- **TimeEntries** - Detailed time tracking records
- **TasksGroupsRelations** - Many-to-many relationships between tasks and groups

All tables include:
- Primary key (ID field specific to table)
- DateCreated, DateLastEdited, DateDeleted (soft delete)
- SyncGuidChanged (for synchronization)

## Configuration

### Settings Location
Settings are stored in: `%LOCALAPPDATA%\LegatroExp\settings.json`

### Configurable Options
- Database path
- Window position and size (stored as percentages)
- Splitter positions
- Default sort order for tasks
- Auto-backup enabled/disabled
- Base font
- UI language (English, German, Spanish, Japanese, Chinese Simplified)

## Requirements

### Build Requirements
- .NET 9 SDK
- Windows OS (for Windows Forms)

### Runtime Requirements
- .NET 9 Runtime
- Windows 10 or later

## Building the Application

```bash
# Clone the repository
git clone https://github.com/KlausLoeffelmannOrg/LegatroLight.git
cd LegatroLight

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the application
dotnet run --project LegatroExp/LegatroExp.csproj
```

## Project Structure

```
LegatroExp/
├── Controls/          # Custom controls (DarkModeDataGridView)
├── Data/             # EF Core DbContext
├── Forms/            # WinForms forms
│   ├── MainForm      # Main application window
│   └── UserSetupForm # First-run user configuration
├── Helpers/          # Utility classes
│   ├── AppSettings   # Settings management
│   ├── DatabaseInitializer # Database seeding
│   └── WindowsAuthHelper   # Windows authentication
├── Models/           # EF Core entity models
│   ├── User
│   ├── Category
│   ├── Group
│   ├── Project
│   ├── Task
│   ├── TasksGroupsRelation
│   └── TimeEntry
└── Program.cs        # Application entry point
```

## Technologies

- **.NET 9**: Latest .NET framework
- **Windows Forms**: Desktop UI framework
- **Entity Framework Core 9**: ORM for database access
- **SQLite**: Embedded database engine
- **System.DirectoryServices**: Windows authentication
- **CommunityToolkit.Mvvm**: MVVM infrastructure (future use)

## Database File

- **Extension**: `.legatro`
- **Default Location**: `%USERPROFILE%\Documents\LegatroExp\default.legatro`
- **Backup**: `{database}.bak` (created on clean shutdown if auto-backup enabled)

## First Run Experience

1. Application starts and detects no existing database
2. User Setup Assistant form appears with Windows user information pre-populated
3. User can optionally modify display name, nickname, and contact information
4. Upon confirmation:
   - Database is created
   - System groups and categories are seeded
   - Default project is created
   - User information is saved
5. Main application window opens

## Menu Structure

### File Menu
- **New Solution**: Create a new database
- **Open Solution**: Load a different database
- **Quit**: Close application with optional backup

### Edit Menu
- **Groups...**: Manage task groups
- **Projects...**: Manage projects
- **Categories...**: Manage color categories
- **Tasks...**: Manage tasks

### Tools Menu
- **Options...**: Configure application settings

## Future Enhancements

The application is designed with extensibility in mind:

- **Synchronization**: SyncGuidChanged fields support future multi-device sync
- **External Integration**: IDForeignID and IDForeignSyncID support external system integration
- **MVVM Pattern**: CommunityToolkit.Mvvm package included for future refactoring
- **Localization**: Language support infrastructure in place
- **Advanced Reporting**: Time budget tracking and reporting capabilities

## License

This project is part of the LegatroLight repository.

## Contributing

This application was created as part of an experimental time tracking solution. Contributions and suggestions are welcome through the GitHub repository.
