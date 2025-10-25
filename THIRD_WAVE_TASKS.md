# Third Wave Implementation Tasks for LegatroExp

This document outlines the remaining features from issue #4 that were not completed in the 2nd wave PR. These tasks represent medium to low priority features that will enhance the application but are not critical for core functionality.

## Summary of Completed Work (2nd Wave)

The following high-priority features have been successfully implemented:
- ✅ Complete task loading with automatic and manual group assignment
- ✅ Task selection and details display (read-only)
- ✅ Time tracking DataGridView showing time entries
- ✅ File menu operations (New Solution, Open Solution)
- ✅ Quick task creation with "New Task" panel
- ✅ Task status indicators (completed, overdue, in-progress)
- ✅ Automatic backup on clean shutdown
- ✅ Task sorting by user preference
- ✅ Due date display in TreeView

## Remaining Tasks for 3rd Wave

### High Priority - Task Editing

#### Task Details Panel Editing
- [ ] Convert task details display to editable controls
  - [ ] Task Display Name TextBox (editable)
  - [ ] Task Description TextBox (multiline, editable)
  - [ ] Project selection ComboBox (editable, populated from Projects table)
  - [ ] Due Date DateTimePicker (editable)
  - [ ] Estimated Effort TextBox (TimeSpan format, editable)
  - [ ] Groups assignment CheckedListBox (owner-drawn, showing symbols and colors)
- [ ] Add Apply/Cancel ToolStripButtons (right-aligned)
- [ ] Implement edit mode state management
  - [ ] Enable/disable editing controls based on mode
  - [ ] Show Apply/Cancel buttons only in edit mode
  - [ ] Block access to TreeView and other controls during edit
- [ ] Implement Apply button handler
  - [ ] Validate all fields
  - [ ] Save changes to database
  - [ ] Update Task.TimeSpent if needed
  - [ ] Refresh TreeView to show updated task
  - [ ] Exit edit mode
- [ ] Implement Cancel button handler
  - [ ] Discard changes
  - [ ] Restore original values
  - [ ] Exit edit mode
- [ ] Add Edit button to enter edit mode from read-only view

#### Task Details Validation
- [ ] Integrate ErrorProvider for validation feedback
- [ ] Implement validation rules:
  - [ ] DisplayName: Required, max length 200
  - [ ] Description: Max length 2000
  - [ ] Due Date: Must be in future or null
  - [ ] Estimated Effort: Must be positive or null
  - [ ] Project: Must be selected
- [ ] Add yellow background on TextBox focus (#FFFFCC)
- [ ] Add light red background on validation failure (#FFE0E0)
- [ ] Display validation error messages via ErrorProvider

### Medium Priority - Time Entry Management

#### Time Entry Editing
- [ ] Make DataGridView cells fully editable
- [ ] Add new time entry row functionality
- [ ] Implement cell editing validation:
  - [ ] Description: Optional, max length 500
  - [ ] Start Time: Required, must be valid DateTime
  - [ ] End Time: Required, must be after Start Time
  - [ ] Duration: Auto-calculated, read-only
- [ ] Implement row deletion
- [ ] Auto-update Task.TimeSpent when entries change
- [ ] Auto-update Project.TimeSpent when entries change
- [ ] Save changes to database immediately or on Apply button

#### Time Entry Features
- [ ] Add TimeZone tracking for each entry
- [ ] Display times in local timezone with UTC indicator
- [ ] Add context menu for time entries
  - [ ] Copy entry
  - [ ] Delete entry
  - [ ] Edit in separate dialog (optional)

### Medium Priority - Management Dialogs

All management dialogs should follow the base pattern defined in the requirements:
- DataGridView (read-only, sortable by column click)
- Row selection (select entire row)
- Double-click handler to open edit dialog
- Add, Edit, Delete buttons
- Protection for system records (IsSystem=true)

#### Groups Management Dialog
- [ ] Create GroupsListDialog form
  - [ ] DataGridView showing all non-deleted groups
  - [ ] Columns: Display Name, Description, Order No, Auto Range, Is System
  - [ ] Sort by OrderNo by default
  - [ ] Add button (opens GroupEditDialog)
  - [ ] Edit button (double-click or button, opens GroupEditDialog)
  - [ ] Delete button (soft delete, confirms first, disabled for system groups)
- [ ] Create GroupEditDialog form
  - [ ] TableLayoutPanel layout with proper sizing
  - [ ] Group Display Name TextBox (required)
  - [ ] Group Description TextBox (multiline)
  - [ ] Group Symbol TextBox (single char, optional)
  - [ ] Order No NumericUpDown
  - [ ] Auto Range Span TextBox (TimeSpan format)
  - [ ] Auto Range Offset NumericUpDown
  - [ ] BackColor ColorPicker
  - [ ] ForeColor ColorPicker
  - [ ] Is Hidden CheckBox
  - [ ] Validation with ErrorProvider
  - [ ] OK and Cancel buttons
  - [ ] Center on parent window
  - [ ] Disable editing for system groups

#### Projects Management Dialog
- [ ] Create ProjectsListDialog form
  - [ ] DataGridView showing all non-deleted projects
  - [ ] Columns: Name, Description, Budget, Time Spent, Is System
  - [ ] Sort by Name by default
  - [ ] Add, Edit, Delete buttons (Delete disabled for system projects)
- [ ] Create ProjectEditDialog form
  - [ ] Project Name TextBox (required)
  - [ ] Description TextBox (multiline)
  - [ ] Budget TimeSpan TextBox
  - [ ] Category ComboBox
  - [ ] Validation with ErrorProvider
  - [ ] OK and Cancel buttons
  - [ ] Disable editing for system projects

#### Categories Management Dialog
- [ ] Create CategoriesListDialog form
  - [ ] DataGridView showing all non-deleted categories
  - [ ] Columns: Name, Back Color, Fore Color, Is System
  - [ ] Sort by Name by default
  - [ ] Color preview in cells
  - [ ] Add, Edit, Delete buttons (Delete disabled for system categories)
- [ ] Create CategoryEditDialog form
  - [ ] Category Name TextBox (required)
  - [ ] BackColor ColorPicker
  - [ ] ForeColor ColorPicker
  - [ ] Preview panel showing colors
  - [ ] Validation with ErrorProvider
  - [ ] OK and Cancel buttons
  - [ ] Disable editing for system categories

#### Tasks Management Dialog (Optional - already have quick create)
- [ ] Create TasksListDialog form
  - [ ] DataGridView showing all non-deleted tasks
  - [ ] Columns: Display Name, Project, Due Date, Status, % Done
  - [ ] Sort by multiple columns
  - [ ] Filter by Project, Status
  - [ ] Add, Edit, Delete buttons
- [ ] Create TaskEditDialog form (full editor, not just quick create)
  - [ ] All fields from task details panel
  - [ ] Groups assignment CheckedListBox
  - [ ] Validation with ErrorProvider
  - [ ] OK and Cancel buttons

### Medium Priority - Options Dialog

- [ ] Create OptionsDialog form
  - [ ] Use TabControl or TableLayoutPanel for organization
  - [ ] General Tab:
    - [ ] Default Task Sort Order ComboBox (DisplayName, DueDate, etc.)
    - [ ] Auto-backup CheckBox
    - [ ] Confirm on delete CheckBox
  - [ ] Appearance Tab:
    - [ ] Base Font ComboBox (list installed fonts)
    - [ ] Font Size NumericUpDown
    - [ ] Preview label showing current font
  - [ ] Language Tab:
    - [ ] UI Language ComboBox (English, German, Spanish, Japanese, Chinese Simplified)
    - [ ] Show restart warning when language changes
  - [ ] OK, Cancel, Apply buttons
  - [ ] Center on parent window
  - [ ] Validate all settings before applying

### Low Priority - UI Polish

#### Visual Feedback
- [ ] Implement yellow background on TextBox focus (#FFFFCC)
- [ ] Implement light red background on validation failure (#FFE0E0)
- [ ] Add hover effects on buttons
- [ ] Add progress indicators for long operations (database create, open, backup)
- [ ] Improve status bar messages with color coding (green=success, red=error, blue=info)

#### User Experience
- [ ] Add context menus throughout the application
  - [ ] TreeView context menu (Add Task, Edit Task, Delete Task, Refresh)
  - [ ] DataGridView context menu (Copy, Paste, Delete)
- [ ] Add keyboard shortcuts
  - [ ] Ctrl+N: New Task
  - [ ] Ctrl+E: Edit Selected Task
  - [ ] Del: Delete Selected Task
  - [ ] F5: Refresh
  - [ ] Ctrl+S: Save/Apply changes
- [ ] Add tooltips to all controls with helpful text
- [ ] Add confirmation dialogs for destructive operations

#### Dark Mode Support
- [ ] Detect dark mode changes at runtime
- [ ] Apply dark theme colors to DataGridView
  - [ ] Background: #1E1E1E
  - [ ] Foreground: #E0E0E0
  - [ ] Grid lines: #3F3F3F
  - [ ] Selection: #264F78
- [ ] Apply dark theme to custom controls if needed
- [ ] Test contrast and readability in both modes

### Low Priority - Accessibility

#### Keyboard Navigation
- [ ] Set TabIndex for all controls in logical order
- [ ] Verify keyboard-only navigation works throughout
- [ ] Add unambiguous mnemonics for all labels and menu items
- [ ] Ensure Enter/Escape work appropriately in dialogs

#### Screen Reader Support
- [ ] Set AccessibleName for all controls
- [ ] Set AccessibleDescription for complex controls
- [ ] Set AccessibleRole where appropriate
- [ ] Test with Windows Narrator and NVDA

#### Visual Accessibility
- [ ] Verify color contrast meets WCAG AA standards
- [ ] Support high contrast mode
- [ ] Ensure minimum control sizes (44x44 pixels for touch targets)
- [ ] Add visual focus indicators

### Low Priority - Advanced Features

#### Reporting and Export
- [ ] Add reports menu with options:
  - [ ] Time by Project report
  - [ ] Time by Task report
  - [ ] Time by Date Range report
- [ ] Implement CSV export for time entries
- [ ] Implement Excel export (optional, requires library)
- [ ] Add print preview for reports

#### Search and Filter
- [ ] Add search box in TreeView panel
- [ ] Filter tasks by text search
- [ ] Filter tasks by date range
- [ ] Filter tasks by project
- [ ] Filter tasks by status
- [ ] Save filter preferences

#### Data Management
- [ ] Add database compaction/vacuum option
- [ ] Add database integrity check
- [ ] Add import/export for data migration
- [ ] Add merge capability for multiple databases

### Documentation

- [ ] Update README.md with new features
- [ ] Create user guide with screenshots
- [ ] Document all keyboard shortcuts
- [ ] Add inline help or help menu
- [ ] Create developer documentation for extending the application

## Implementation Recommendations

### Priority Order for 3rd Wave
1. **Task Editing** (High Priority) - Makes the app fully functional
2. **Management Dialogs** (Medium Priority) - Essential for data management
3. **Options Dialog** (Medium Priority) - User preferences
4. **UI Polish** (Low Priority) - Improves user experience
5. **Accessibility** (Low Priority) - Ensures broad usability
6. **Advanced Features** (Low Priority) - Nice to have

### Estimated Effort
- Task Editing: 15-20 hours
- Management Dialogs: 20-25 hours (5 hours per dialog × 4 dialogs)
- Options Dialog: 5-8 hours
- UI Polish: 10-15 hours
- Accessibility: 8-10 hours
- Advanced Features: 15-20 hours
- Documentation: 5-8 hours

**Total Estimated Effort: 78-106 hours**

### Breaking It Down Further
For more manageable PRs, the 3rd wave could be split into:
- **3rd Wave Part A**: Task Editing (15-20 hours)
- **3rd Wave Part B**: Groups and Projects Management Dialogs (10-15 hours)
- **3rd Wave Part C**: Categories, Tasks, and Options Dialogs (15-20 hours)
- **3rd Wave Part D**: UI Polish and Accessibility (18-25 hours)
- **3rd Wave Part E**: Advanced Features and Documentation (20-28 hours)

Each part represents a complete, testable increment of functionality that adds value to the application.
