# Getting Started with Legatro Light

## What is Legatro Light?

Legatro Light is a Windows desktop application for task management and time tracking. It uses SQLite for data storage, Entity Framework Core for data access, and Windows Forms for the user interface.

## Current Status (v0.1 - MVP)

This is a **functional MVP** (Minimum Viable Product) that demonstrates core functionality:
- ✅ Task management (create, view)
- ✅ Time tracking (view entries)
- ✅ Automatic task grouping
- ✅ Windows authentication
- ✅ Settings persistence

## Prerequisites

- **Operating System**: Windows 10 or later
- **.NET SDK**: .NET 9 SDK or later
- **Development**: Visual Studio 2022 or VS Code with C# extension

## Installation

### Option 1: Build from Source

```bash
# Clone the repository
git clone https://github.com/KlausLoeffelmannOrg/WA_LegatroLight.git
cd WA_LegatroLight

# Restore NuGet packages
dotnet restore

# Build the solution
dotnet build

# Run the application
dotnet run --project LegatroExp/LegatroExp.csproj
```

### Option 2: Open in Visual Studio

1. Open `LegatroLight.sln` in Visual Studio 2022
2. Press F5 to build and run

## First Launch

On first launch, you'll see the **User Setup Assistant**:

1. The form will show your Windows user information (automatically detected)
2. You can edit:
   - Display Name
   - First Name, Middle Name, Last Name
   - Email Address
3. Click **OK** to continue or **Cancel** to exit
4. The application will:
   - Create a database at `%USERPROFILE%\Documents\LegatroExp\default.legatro`
   - Seed system data (groups, categories, default project)
   - Save your user information
   - Open the main window

## Main Window Overview

The main window is divided into three sections:

### Left Panel: Task Tree
- Shows all groups (expandable)
- Tasks appear under their groups
- Click to select a task or group

### Right Upper Panel: Task Details
- **Task Information**: Displays selected task details
- **New Task**: Quick task creation box

### Right Lower Panel: Time Tracking
- Shows time entries for selected task
- Columns: Description, Start Time, End Time, Duration

### Status Bar
- Database name (left)
- Status messages (center)
- Current date (right)
- Current time (right, updates every second)

## Quick Start Guide

### Creating Your First Task

1. Select any group in the tree (e.g., "My Day")
2. Type a task name in the "New Task" text box
3. Click the "Add" button
4. Your task appears in the selected group!

### Viewing Task Details

1. Click on any task in the tree
2. Details appear in the "Task Information" box:
   - Display Name
   - Description
   - Creation date
   - Due date
   - Estimated effort
   - Time spent
   - Percent done (calculated)
   - Completion status

### Understanding Groups

**Automatic Groups** (date-based):
- **My Day**: Tasks created today
- **Sliding Week**: Tasks from the last 7 days
- **My Month**: Tasks from the last 30 days

**Manual Groups**:
- **Roaming Links**: For reference links
- **Roaming Notes**: For notes
- **Done**: For completed tasks
- **Clutter**: For miscellaneous items

Tasks automatically appear in date-based groups based on their creation date. Manual groups require explicit assignment (feature coming soon).

## System Data

On first run, the application creates:

### Groups (7 total)
1. My Day (Order: 1)
2. Sliding Week (Order: 2)
3. My Month (Order: 3)
4. Roaming Links (Order: 4)
5. Roaming Notes (Order: 5)
6. Done (Order: 65534)
7. Clutter (Order: 65535)

All marked as "System Groups" (cannot be deleted).

### Categories (10 total)
Red, Orange, Yellow, Green, Blue, Indigo, Violet, Pink, Brown, Gray

Each with appropriate background and foreground colors.

### Projects (1 total)
**Default** - System project for general tasks

## File Locations

### Database
- **Default**: `%USERPROFILE%\Documents\LegatroExp\default.legatro`
- **Example**: `C:\Users\JohnDoe\Documents\LegatroExp\default.legatro`

### Backup
- **Location**: Same as database with `.bak` extension
- **Example**: `C:\Users\JohnDoe\Documents\LegatroExp\default.legatro.bak`
- **When**: Created on normal application exit

### Settings
- **Location**: `%LOCALAPPDATA%\LegatroExp\settings.json`
- **Example**: `C:\Users\JohnDoe\AppData\Local\LegatroExp\settings.json`
- **Contents**: Window position, splitter positions, preferences

## Settings File

The `settings.json` file stores:

```json
{
  "DatabasePath": "C:\\Users\\...\\default.legatro",
  "WindowLeftPercent": 10.0,
  "WindowTopPercent": 10.0,
  "WindowWidthPercent": 80.0,
  "WindowHeightPercent": 80.0,
  "IsMaximized": false,
  "MainSplitterPercent": 30.0,
  "RightSplitterPercent": 60.0,
  "DefaultSortOrder": 0,
  "AutoBackupEnabled": true,
  "BaseFontName": "Segoe UI",
  "BaseFontSize": 11.0,
  "Language": "English"
}
```

You can edit this file manually if needed (close the application first).

## Menu Commands

### File Menu
- **New Solution...** (not yet implemented)
- **Open Solution...** (not yet implemented)
- **Quit**: Close the application (creates backup if enabled)

### Edit Menu
- **Groups...** (not yet implemented)
- **Projects...** (not yet implemented)
- **Categories...** (not yet implemented)

### Tools Menu
- **Options...** (not yet implemented)

## What You Can Do Now

✅ **Task Management:**
- Create tasks
- View task details
- See tasks grouped by date
- See calculated progress (percent done)

✅ **Navigation:**
- Browse groups and tasks
- Select items to view details
- Expand/collapse groups

✅ **Time Tracking:**
- View time entries for tasks
- See formatted times and durations

✅ **Application:**
- Automatic Windows authentication
- Persistent window positions
- Auto-backup on exit
- Real-time clock

## What's Coming Next

See `FUTURE_ISSUES.md` for the complete roadmap. Key upcoming features:

🚧 **High Priority:**
- Task editing (modify existing tasks)
- Time entry creation and editing
- Apply/Cancel buttons during editing
- UI blocking during edits

🔜 **Medium Priority:**
- Groups management dialog
- Projects management dialog
- Categories management dialog
- Options dialog
- New/Open solution commands

⏳ **Low Priority:**
- Dark mode support
- Custom owner-drawn controls
- Context menus and shortcuts
- Multi-language support

## Tips & Tricks

### Window Layout
- Drag the vertical splitter to resize the tree vs. details panels
- Drag the horizontal splitter to resize task details vs. time tracking
- Positions are automatically saved when you close the application

### Backup Behavior
- Backup is created only on normal exit (File > Quit or X button)
- If the application crashes, no backup is created
- Only one backup is kept (overwrites previous backup)
- To disable: Edit `settings.json` and set `"AutoBackupEnabled": false`

### Database Location
- You can move the database file anywhere
- Update the path in `settings.json`
- Or use File > Open Solution when implemented

### Multiple Databases
- Currently, the app works with one database at a time
- To use multiple databases: Create them in different locations and update settings.json
- Or wait for File > New/Open Solution implementation

## Troubleshooting

### Application Won't Start
- Check that you have .NET 9 runtime installed
- Check Windows Event Viewer for error details
- Try deleting `settings.json` to reset settings

### Database Errors
- Check that the database file exists
- Check file permissions
- Try creating a new database (delete old one first)

### User Setup Fails
- Check that you have permissions to create files in Documents folder
- Try running as administrator (one time)
- Check that Windows user account is valid

### TreeView is Empty
- Check that database was created successfully
- Check that system data was seeded
- Restart the application

## Development

### Project Structure

```
LegatroExp/
├── Models/          - Entity models (User, Task, etc.)
├── Data/            - DbContext and EF Core configuration
├── Forms/           - WinForms forms (.cs and .Designer.cs)
├── Helpers/         - Utility classes
├── Controls/        - Custom controls (to be added)
└── Properties/      - Application properties
```

### Key Files

- `Program.cs`: Application entry point
- `MainForm.cs`: Main application window
- `UserSetupForm.cs`: First-run wizard
- `LegatroDbContext.cs`: EF Core database context
- `DatabaseInitializer.cs`: System data seeding
- `WindowsAuthHelper.cs`: Windows authentication
- `AppSettings.cs`: Settings management

### Building

```bash
# Debug build
dotnet build

# Release build
dotnet build -c Release

# Clean build
dotnet clean
dotnet build
```

### Testing

Currently, there are no automated tests. Testing is done manually:
1. Run the application
2. Test each feature
3. Verify database contents

Automated testing is planned for future releases.

## Contributing

To contribute to this project:

1. Check `FUTURE_ISSUES.md` for open tasks
2. Pick an issue to work on
3. Follow the coding guidelines in the WinForms Agent instructions
4. Submit a pull request

### Coding Guidelines

- Follow C# conventions
- Use modern C# 11 features in regular code
- Keep `.Designer.cs` files simple (no complex logic)
- Add XML comments to public members
- Test your changes thoroughly

## Support

For issues, questions, or suggestions:
- Check `IMPLEMENTATION_STATUS.md` for feature status
- Check `FUTURE_ISSUES.md` for planned features
- File an issue on GitHub (coming soon)

## License

See the repository LICENSE file for details.

## Version History

### v0.1 - MVP (Current)
- Initial release
- Basic task management
- Time tracking display
- Windows authentication
- Auto-backup
- Settings persistence

### Planned Versions

**v0.2** - Editing
- Task editing
- Time entry editing
- Validation

**v0.3** - Management
- Groups dialog
- Projects dialog
- Categories dialog
- Options dialog

**v0.4** - Polish
- Dark mode
- Localization
- Advanced features

**v1.0** - Full Release
- All features complete
- Full documentation
- Automated tests

## Resources

- **README.md**: Project overview
- **IMPLEMENTATION_STATUS.md**: Detailed feature checklist
- **FUTURE_ISSUES.md**: Planned features and issues
- **GETTING_STARTED.md**: This file (user guide)

## Feedback

We welcome feedback on:
- User experience
- Feature priorities
- Bug reports
- Performance issues
- Documentation clarity

Thank you for trying Legatro Light!
