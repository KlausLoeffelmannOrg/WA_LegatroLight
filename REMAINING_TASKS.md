# Remaining Tasks for LegatroExp

## Overview

This document tracks the remaining features and components that need to be implemented to complete the LegatroExp WinForms time tracking application. The foundational infrastructure (database schema, EF Core setup, basic UI layout, and services) has been completed.

---

## UI Components

### Task Details Panel
- [ ] Implement editable Task Display Name TextBox
- [ ] Implement editable multiline Task Description TextBox
- [ ] Add Groups assignment CheckedListBox with owner-drawn items showing group symbols and colors
- [ ] Add Project selection ComboBox (editable)
- [ ] Implement date row with nested TableLayoutPanel containing:
  - [ ] Date Created (info label, read-only)
  - [ ] Due Date (editable DateTimePicker)
  - [ ] Estimated Effort (editable)
  - [ ] Percent Done (info label, calculated from TimeSpent/EstimatedEffort)
- [ ] Add "New Task" GroupBox with:
  - [ ] Multiline TextBox for task entry
  - [ ] Add Button (disabled by default, enabled when TextBox is dirty)
- [ ] Implement edit mode with Apply/Cancel ToolStripButtons (right-aligned, prominently visible)
- [ ] Add yellow background on TextBox focus
- [ ] Add light red background on validation failure
- [ ] Integrate ErrorProvider for validation feedback
- [ ] Block access to other controls during edit mode

### Time Tracking DataGridView
- [ ] Create DataGridView for TimeEntry records
- [ ] Enable direct cell editing
- [ ] Implement Duration auto-calculation (EndTime - StartTime)
- [ ] Add dark mode color adjustments for DataGridView
- [ ] Display duration in proper format (hh:mm:ss)
- [ ] Update Task and Project TimeSpent when entries are modified

### TreeView Task Loading
- [ ] Implement automatic task assignment based on AutoRangeSpan/AutoRangeOffset
- [ ] Implement manual task assignment via TasksGroupsRelations
- [ ] Add task sorting by user preference (DisplayName, DueDate, PercentDone, EstimatedEffort, DateCreated)
- [ ] Add task status indicators (overdue, completed, in-progress)
- [ ] Implement task selection handler to display details in right panel

---

## Menu Functionality

### File Menu
- [ ] Implement "New Solution..." command
  - [ ] Show SaveFileDialog for .legatro file
  - [ ] Create new SQLite database
  - [ ] Copy default project, categories, and user data
  - [ ] Update settings with new database path
- [ ] Implement "Open Solution..." command
  - [ ] Show OpenFileDialog for .legatro file
  - [ ] Load selected database
  - [ ] Update settings and refresh UI
  - [ ] Store path in settings file

### Edit Menu
- [ ] Implement "Groups..." command
  - [ ] Create GroupsListDialog with DataGridView
  - [ ] Enable double-click to open GroupEditDialog
  - [ ] Protect system groups from editing/deletion
- [ ] Implement "Projects..." command
  - [ ] Create ProjectsListDialog with DataGridView
  - [ ] Enable double-click to open ProjectEditDialog
  - [ ] Protect system projects from editing/deletion
- [ ] Implement "Categories..." command
  - [ ] Create CategoriesListDialog with DataGridView
  - [ ] Enable double-click to open CategoryEditDialog
  - [ ] Protect system categories from editing/deletion
- [ ] Implement "Tasks..." command
  - [ ] Create TasksListDialog with DataGridView
  - [ ] Enable double-click to open TaskEditDialog

### Tools Menu
- [ ] Implement "Options..." command
  - [ ] Create OptionsDialog with TabControl or TableLayoutPanel
  - [ ] Add Default Sort Order ComboBox
  - [ ] Add Auto-backup CheckBox
  - [ ] Add Base Font ComboBox with installed fonts
  - [ ] Add UI Language ComboBox (English, German, Spanish, Japanese, Chinese Simplified)
  - [ ] Show restart warning for language changes

---

## Modal Dialogs

### Base Data List/Edit Dialogs
- [ ] Create base class or pattern for list dialogs
  - [ ] DataGridView (read-only, sortable by column click)
  - [ ] Row selection (select entire row)
  - [ ] Double-click handler to open edit dialog
- [ ] Create base class or pattern for edit dialogs
  - [ ] TableLayoutPanel layout with proper column/row sizing
  - [ ] Accelerator keys for all entry fields
  - [ ] Accessibility properties (AccessibleName, AccessibleDescription)
  - [ ] ErrorProvider for validation
  - [ ] Yellow background on TextBox focus
  - [ ] Light red background on validation failure
  - [ ] Validation on OK click only (not at field level)
  - [ ] OK and Cancel buttons
  - [ ] ComboBoxes for foreign key fields
  - [ ] Center on parent window

### Specific Edit Dialogs
- [ ] Create GroupEditDialog
- [ ] Create ProjectEditDialog  
- [ ] Create CategoryEditDialog
- [ ] Create TaskEditDialog

---

## Feature Implementation

### Task Management
- [ ] Implement create new task functionality
- [ ] Implement edit existing task functionality
- [ ] Implement task-to-group assignment (automatic based on date ranges)
- [ ] Implement task-to-group assignment (manual via CheckedListBox)
- [ ] Implement mark task complete functionality
- [ ] Calculate and display percent done (TimeSpent / EstimatedEffort * 100)
- [ ] Update UI when tasks are created/modified/deleted

### Time Tracking
- [ ] Implement add time entry functionality
- [ ] Implement edit time entry in DataGridView
- [ ] Auto-calculate and update Task.TimeSpent when entries change
- [ ] Auto-calculate and update Project.TimeSpent when entries change
- [ ] Display duration in hh:mm:ss format
- [ ] Store TimeZone information with each entry

### Data Validation
- [ ] Define validation rules for all entity fields
- [ ] Integrate ErrorProvider in all edit dialogs
- [ ] Implement validation logic (runs on OK button click)
- [ ] Prevent editing/deletion of system records (IsSystem=true)
- [ ] Show appropriate error messages for validation failures

### Dark Mode Support
- [ ] Detect system dark mode (Application.IsDarkModeEnabled)
- [ ] Apply dark theme to DataGridView controls
- [ ] Apply dark theme to custom controls if needed
- [ ] Test color contrast and readability in dark mode

### Backup Functionality
- [ ] Implement auto-backup on clean shutdown (when enabled in settings)
- [ ] Create .bak file in same directory as database
- [ ] Add error handling for backup failures
- [ ] Consider adding manual backup option to File menu

### Database Operations
- [ ] Complete soft-delete implementation (set DateDeleted instead of hard delete)
- [ ] Add database migration support if schema changes
- [ ] Implement data integrity checks
- [ ] Add transaction support for multi-step operations

---

## Accessibility & UX Improvements
- [ ] Set TabIndex for logical control navigation
- [ ] Verify keyboard-only navigation works throughout app
- [ ] Add unambiguous mnemonics for all actionable controls
- [ ] Test with screen reader compatibility
- [ ] Ensure minimum form sizes are set appropriately
- [ ] Add tooltips to controls where helpful

---

## Localization (Future)
- [ ] Create resource files for each supported language
- [ ] Implement language switching mechanism
- [ ] Handle text length differences in layouts
- [ ] Test all UI elements with different languages

---

## Documentation
- [ ] Add XML documentation to public APIs
- [ ] Create user guide/help documentation
- [ ] Document database schema in detail
- [ ] Add inline code comments for complex logic

---

## Testing
- [ ] Add unit tests for services layer
- [ ] Add integration tests for database operations
- [ ] Add UI tests for critical workflows
- [ ] Test on different Windows versions (10, 11)
- [ ] Test with different DPI settings (96, 120, 144 DPI)
- [ ] Test dark mode functionality

---

## Implementation Priority

### High Priority (Core functionality)
1. Task Details Panel with full editing
2. Time Tracking DataGridView
3. Task-to-group assignment logic
4. File menu (New/Open Solution)

### Medium Priority (Essential features)
1. Edit menu dialogs (Groups, Projects, Categories)
2. Options dialog
3. Dark mode support
4. Backup functionality

### Low Priority (Polish and extras)
1. Localization
2. Advanced accessibility features
3. Comprehensive documentation
4. Automated testing

---

## How to Use This Document

To create a GitHub issue from this document:

1. Go to the repository on GitHub
2. Click on "Issues" tab
3. Click "New issue"
4. Copy the content from this file (starting from "## Overview")
5. Set the title: "Complete remaining features for LegatroExp time tracking application"
6. Add labels: `enhancement`, `feature-request`
7. Click "Submit new issue"

Alternatively, if you have GitHub CLI installed and authenticated:

```bash
gh issue create \
  --title "Complete remaining features for LegatroExp time tracking application" \
  --body-file REMAINING_TASKS.md \
  --label enhancement \
  --label feature-request
```
