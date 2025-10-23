# Legatro Experimental - Development Guide

## Architecture Overview

Legatro Experimental follows a layered architecture pattern optimized for WinForms applications:

### Layer Structure

```
┌─────────────────────────────────────────┐
│           Presentation Layer            │
│  (Forms, Controls, User Interaction)    │
├─────────────────────────────────────────┤
│          Business Logic Layer           │
│     (Helpers, Data Processing)          │
├─────────────────────────────────────────┤
│            Data Access Layer            │
│    (EF Core DbContext, Repositories)    │
├─────────────────────────────────────────┤
│              Model Layer                │
│       (Entity Models, DTOs)             │
└─────────────────────────────────────────┘
```

## Project Structure

### `/Controls`
Custom controls extending standard WinForms controls:
- **DarkModeDataGridView**: DataGridView with automatic dark mode theming
- Future: Custom TreeView, Calendar controls

### `/Data`
Entity Framework Core database context:
- **LegatroDbContext**: Main database context with all entity configurations
- Defines relationships, indexes, and constraints
- Manages database migrations and schema

### `/Forms`
WinForms UI components:
- **MainForm**: Primary application window
- **UserSetupForm**: First-run user configuration
- **OptionsForm**: Application settings dialog
- Future: List and detail forms for base data editing

### `/Helpers`
Utility and helper classes:
- **AppSettings**: Application configuration management
- **DatabaseInitializer**: Seed data and first-run setup
- **WindowsAuthHelper**: Windows authentication integration

### `/Models`
Entity models representing database tables:
- All inherit from **BaseEntity**
- Follow EF Core conventions
- Include navigation properties for relationships

## Design Patterns

### 1. Repository Pattern (Future)
Currently using DbContext directly, but structured for easy refactoring to repository pattern:
```csharp
public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(string id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
}
```

### 2. Settings Management
Centralized settings through **AppSettings** class:
- JSON serialization for persistence
- Type-safe property access
- Automatic directory creation

### 3. Soft Delete Pattern
All entities support soft deletion:
```csharp
entity.DateDeleted = DateTime.UtcNow;
// Never: context.Remove(entity);
```

### 4. Sync GUID Pattern
Every entity tracks changes with **SyncGuidChanged**:
- Updated on every modification
- Enables conflict resolution
- Supports future synchronization features

## Database Design Principles

### Primary Keys
- All tables use TEXT (GUID) primary keys
- Format: `ID{TableName}` (e.g., IDTask, IDProject)
- Generated via `Guid.NewGuid().ToString()`

### Timestamps
- **DateCreated**: Set once on creation (UTC)
- **DateLastEdited**: Updated on every change (UTC)
- **DateDeleted**: Soft delete timestamp (UTC, nullable)

### Foreign Keys
- Cascade delete for junction tables (TasksGroupsRelations)
- Restrict delete for primary relationships
- SetNull for optional relationships (Category in Project)

### Indexes
Strategy:
- Primary keys (automatic)
- Foreign keys (for join performance)
- Frequently queried fields (DueDate, DateFinished, DisplayName)
- Unique constraints where appropriate

## UI Design Guidelines

### Layout Strategy
1. **Use TableLayoutPanel** for structured layouts
2. **Nested containers** for complex forms
3. **FlowLayoutPanel** for button rows
4. **Dock and Anchor** for responsive design

### Font Sizing
- **Base font**: 11pt Segoe UI (configurable)
- **Headers**: 14pt Bold
- **Status**: 9pt for secondary information

### Color Scheme
Support both light and dark modes:
- Use **SystemColors** where possible
- Override in dark mode with custom colors
- DataGridView requires manual theming

### Control Naming
Convention: `_{controlType}{Purpose}`
- Examples: `_btnOK`, `_txtTaskName`, `_dgvTimeEntries`
- Consistent with Hungarian-style notation

## Data Flow

### Application Startup
```
Program.Main()
  ├─> Load AppSettings
  ├─> Check database exists
  │   ├─> No: Show UserSetupForm
  │   │   └─> Initialize database
  │   └─> Yes: Auto-login current user
  └─> Show MainForm
```

### Task Loading
```
MainForm.LoadData()
  └─> LoadGroupsAndTasks()
      ├─> Query Groups (active, not hidden, ordered)
      └─> For each Group:
          ├─> Auto-range groups: Query by DateCreated
          └─> Manual groups: Query via TasksGroupsRelations
```

### Settings Persistence
```
On application start:
  └─> AppSettings.Load() from JSON

On form closing:
  ├─> SaveWindowPosition()
  ├─> AppSettings.Save() to JSON
  └─> Optional: CreateBackup()
```

## Extension Points

### Adding New Entity Types
1. Create model inheriting **BaseEntity**
2. Add **DbSet<T>** to **LegatroDbContext**
3. Configure in **OnModelCreating**
4. Create migration (future)
5. Add seed data if system entity

### Adding New Forms
1. Create partial class in `/Forms`
2. Separate `.Designer.cs` for InitializeComponent
3. Follow TableLayoutPanel pattern
4. Implement IDisposable if managing resources
5. Center on parent: `StartPosition = CenterParent`

### Custom Controls
1. Inherit from appropriate base control
2. Add to `/Controls` directory
3. Implement theme awareness if visual
4. Double-buffer for custom painting:
   ```csharp
   DoubleBuffered = true;
   ```

## Testing Strategy (Future)

### Unit Tests
- Test business logic in Helpers
- Mock DbContext for data layer tests
- Test entity validation rules

### Integration Tests
- Test EF Core queries and relationships
- Database initialization and seeding
- Settings persistence

### UI Tests (Manual)
- Form layouts at different DPI settings
- Dark mode switching
- Keyboard navigation and accelerators

## Performance Considerations

### Database Queries
- Use **AsNoTracking()** for read-only queries
- Avoid N+1 queries with **Include()**
- Index frequently filtered/sorted columns

### UI Responsiveness
- Load data asynchronously where appropriate
- Use **BeginInvoke** for cross-thread UI updates
- Batch database operations

### Memory Management
- Dispose Forms and DbContext properly
- Use **using** statements for IDisposable
- Avoid creating Brush/Pen in loops

## Security Considerations

### Windows Authentication
- Never store passwords
- Use Windows SID for user identification
- Respect domain security policies

### Database
- SQLite file permissions follow OS security
- No encryption by default (can be added)
- Backup files need same protection as database

### Settings
- Store in user-specific AppData
- Don't store sensitive data in settings
- Validate all user input

## Future Enhancements

### Priority 1: Core Functionality
- [ ] Complete task editing panel
- [ ] TimeEntries DataGridView with editing
- [ ] File operations (New/Open database)
- [ ] Base data editing dialogs

### Priority 2: User Experience
- [ ] Keyboard shortcuts
- [ ] Context menus
- [ ] Drag-and-drop for task grouping
- [ ] Quick task entry

### Priority 3: Advanced Features
- [ ] Reporting and charts
- [ ] Data export (CSV, Excel)
- [ ] Time entry timer
- [ ] Recurring tasks

### Priority 4: Synchronization
- [ ] Conflict resolution
- [ ] Multi-device sync
- [ ] Cloud storage integration

## Contributing Guidelines

### Code Style
- Follow existing patterns
- Use modern C# features (C# 11+)
- Document public APIs
- Keep methods focused and small

### Commit Messages
- Use imperative mood: "Add feature" not "Added feature"
- Reference issues when applicable
- Keep first line under 72 characters

### Pull Requests
- One feature per PR
- Update documentation
- Add tests for new functionality
- Ensure clean build with no warnings

## Tools and IDEs

### Recommended
- **Visual Studio 2022** (17.8+) - Full WinForms designer support
- **Visual Studio Code** - Lightweight editing
- **JetBrains Rider** - Alternative IDE

### Extensions
- WinForms Designer (VS 2022)
- EF Core Power Tools
- CodeMaid (code cleanup)

## Build Configuration

### Debug
- Full symbols included
- Detailed logging
- No optimizations

### Release
- Optimized compilation
- Trimmed output
- Ready for distribution

## Deployment

### Requirements
- .NET 9 Runtime (Windows)
- Windows 10 1809 or later
- No additional dependencies

### Distribution Options
1. **Framework-dependent**: Requires .NET 9 installed
2. **Self-contained**: Includes runtime (~70MB)
3. **Single-file**: All in one executable

### Example Publish Command
```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

## Troubleshooting

### Common Issues

**Database locked error**
- Ensure only one instance running
- Check for crashed processes holding file

**DPI scaling issues**
- Verify app.manifest DPI settings
- Test at 100%, 125%, 150% scaling

**Dark mode not applying**
- API is experimental in .NET 9
- Restart application after OS theme change

## Resources

- [WinForms Documentation](https://docs.microsoft.com/dotnet/desktop/winforms/)
- [EF Core Documentation](https://docs.microsoft.com/ef/core/)
- [SQLite Documentation](https://www.sqlite.org/docs.html)
- [.NET 9 What's New](https://docs.microsoft.com/dotnet/core/whats-new/dotnet-9)

## License

See LICENSE file in repository root.
