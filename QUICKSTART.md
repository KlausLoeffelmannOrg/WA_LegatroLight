# Legatro Experimental - Quick Start Guide

## What is Legatro Experimental?

Legatro Experimental is a Windows desktop application for tracking time spent on projects and tasks. It automatically organizes your tasks into groups and provides a clean interface for time management.

## Prerequisites

Before you start, ensure you have:

- **Windows 10** (version 1809 or later) or **Windows 11**
- **.NET 9 SDK** - Download from https://dotnet.microsoft.com/download/dotnet/9.0
- A **text editor** or IDE (Visual Studio 2022 recommended)

## Installation

### Option 1: Build from Source

1. **Clone the repository**
   ```bash
   git clone https://github.com/KlausLoeffelmannOrg/LegatroLight.git
   cd LegatroLight
   ```

2. **Build the application**
   ```bash
   dotnet build
   ```

3. **Run the application**
   ```bash
   dotnet run --project LegatroExp/LegatroExp.csproj
   ```

### Option 2: Visual Studio

1. Open `LegatroLight.sln` in Visual Studio 2022
2. Press `F5` to build and run
3. The application will start automatically

## First Run

When you run Legatro Experimental for the first time:

1. **User Setup Assistant** appears
   - Your Windows username and information are pre-filled
   - You can modify your display name, nickname, or add contact details
   - Click **OK** to continue

2. **Database Creation**
   - A new SQLite database is created at:
     `%USERPROFILE%\Documents\LegatroExp\default.legatro`
   - System groups and categories are initialized
   - A default project is created

3. **Main Window Opens**
   - You'll see the main application with an empty task list
   - The status bar shows database name, session start time, and current time

## Basic Usage

### Main Window Layout

```
┌─────────────────────────────────────────────────────────┐
│  File   Edit   Tools                                    │ ← Menu
├──────────────┬──────────────────────────────────────────┤
│              │                                          │
│  Tasks       │  Task Details                           │
│  ├─ My Day   │  (Task editing panel)                   │
│  ├─ Week     │                                          │
│  └─ Month    │                                          │
│              ├──────────────────────────────────────────┤
│              │  Time Tracking                           │
│              │  (Time entries)                          │
├──────────────┴──────────────────────────────────────────┤
│ Database: default.legatro | Session: 10:30:15 | ...    │ ← Status
└─────────────────────────────────────────────────────────┘
```

### Understanding Groups

Groups organize your tasks automatically or manually:

- **My Day** - Tasks created today
- **Sliding Week** - Tasks from the last 7 days
- **My Month** - Tasks from the last 30 days
- **Roaming Links** - For bookmarks and references
- **Roaming Notes** - For notes and ideas
- **Done** - Completed tasks
- **Clutter** - Everything else

### Categories

10 color-coded categories help organize projects:
- Red, Orange, Yellow, Green, Blue
- Indigo, Violet, Pink, Brown, Gray

## Common Tasks

### View Tasks by Group

1. In the left panel, expand "Tasks"
2. Click on a group name
3. Tasks in that group appear underneath

### Change Settings

1. Go to **Tools → Options**
2. Configure:
   - Default sort order for tasks
   - Auto-backup on shutdown
   - Base font
   - Language
3. Click **OK** to save

### Close the Application

- **File → Quit**, or
- Click the **X** button, or
- Press **Alt+F4**

If auto-backup is enabled, a `.bak` file is created automatically.

## Tips and Tricks

### Window Layout

- **Drag splitters** to resize panels
- Your layout is saved automatically
- Window position and size are preserved between sessions

### Keyboard Navigation

- Use **Alt + letter** for menu accelerators
  - `Alt+F` for File menu
  - `Alt+E` for Edit menu
  - `Alt+T` for Tools menu

### Dark Mode

The application automatically detects Windows dark mode:
- Changes take effect on restart
- DataGridView colors adapt automatically

## Troubleshooting

### Application won't start

**Problem**: Error about missing .NET runtime

**Solution**: Install .NET 9 Desktop Runtime from:
https://dotnet.microsoft.com/download/dotnet/9.0

---

**Problem**: Database locked error

**Solution**: 
- Close any other instances of the application
- Check Task Manager for hung processes

### Display Issues

**Problem**: Text is too small or too large

**Solution**:
- Go to **Tools → Options**
- Increase or decrease the base font size
- Restart the application

---

**Problem**: Window doesn't remember position

**Solution**:
- Ensure you have write permissions to:
  `%LOCALAPPDATA%\LegatroExp\`
- Delete `settings.json` to reset to defaults

### Data Issues

**Problem**: Can't see my tasks

**Solution**:
- Check that you're looking at the right database
- Status bar shows the current database name
- Use **File → Open** to switch databases (when implemented)

---

**Problem**: Lost data after crash

**Solution**:
- Check for `.bak` backup file next to your database
- Rename it from `.legatro.bak` to `.legatro`
- Restart the application

## Advanced Usage

### Multiple Databases

You can create separate databases for different purposes:
- Personal tasks
- Work projects
- Client work

Each database is a standalone `.legatro` file.

### Database Location

Default: `%USERPROFILE%\Documents\LegatroExp\default.legatro`

Settings: `%LOCALAPPDATA%\LegatroExp\settings.json`

### Backup Strategy

**Automatic**:
- Enable in Tools → Options
- Creates `.bak` file on every clean shutdown

**Manual**:
- Copy the `.legatro` file to another location
- Consider cloud storage for safety

## Getting Help

### Documentation

- **README.md** - Overview and features
- **DEVELOPMENT.md** - Architecture and development guide
- This file - Quick start guide

### Common Questions

**Q: Can I use this on Mac or Linux?**
A: No, this is a Windows-only application using Windows Forms.

**Q: Can I sync between computers?**
A: Not yet, but the database structure supports future sync features.

**Q: Is my data private?**
A: Yes, all data is stored locally on your computer in a SQLite file.

**Q: Can I export my data?**
A: Data export features are planned for future releases.

## What's Next?

Now that you have the basics:

1. **Explore the menus** - See what options are available
2. **Customize settings** - Set your preferred font and language
3. **Check the status bar** - Monitor database and session info

## Version Information

This quick start guide is for:
- Legatro Experimental (Initial Release)
- .NET 9.0
- Windows 10/11

## Feedback

If you encounter issues or have suggestions:
- Check the repository's Issues page
- Review existing documentation
- Consider contributing improvements

---

**Remember**: The application automatically saves your window layout and settings. You can start where you left off every time you launch Legatro Experimental!
