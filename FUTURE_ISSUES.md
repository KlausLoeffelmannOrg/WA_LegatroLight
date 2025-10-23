# Future Implementation Issues for Legatro Light

This document outlines the features that need to be implemented to complete the Legatro Light application. Each section represents a potential GitHub issue.

## Issue 1: Complete Main Form Task Details Panel

**Priority: HIGH**

### Description
Implement the task details editing panel in the main form with all required fields and controls.

### Requirements
1. Create GroupBox "Task Details" in the upper right panel
2. Add the following controls in a TableLayoutPanel:
   - Task Display Name: TextBox (single-line, editable)
   - Task Description: TextBox (multiline, editable)
   - Groups: CheckedListBox (custom owner-drawn with symbols and colors)
   - Project: ComboBox (editable, populated from Projects table)
   - Date Created: Label (info only, formatted)
   - Due Date: DateTimePicker (editable)
   - Estimated Effort: TextBox with TimeSpan validation (editable)
   - Percent Done: Label (calculated from TimeSpent / EstimatedEffort)

3. Layout guidelines:
   - Use nested TableLayoutPanels for proper spacing
   - Labels in AutoSize column, controls in 100% Percent column
   - Minimum 3px margins on all controls
   - MultiDue TextBox should use Percent row height

### Acceptance Criteria
- [ ] All fields display correctly when a task is selected in TreeView
- [ ] Fields are properly bound to the selected task
- [ ] Validation works for all editable fields
- [ ] Percent Done updates automatically when TimeSpent or EstimatedEffort changes
- [ ] Groups CheckedListBox shows group symbols in their respective colors

---

## Issue 2: Implement "New Task" Entry Controls

**Priority: HIGH**

### Description
Add controls for quickly creating new tasks in the currently selected group.

### Requirements
1. Create GroupBox "New Task" below the Task Details panel
2. Add controls:
   - Multiline TextBox for task entry (3-4 rows)
   - "Add" Button (initially disabled)
3. Enable Add button when TextBox becomes dirty (has text)
4. On Add button click:
   - Create new task in the database
   - Assign to current user
   - Assign to default project (or currently selected project)
   - Add to currently selected group if applicable
   - Clear TextBox
   - Disable Add button
   - Refresh TreeView
   - Select the newly created task

### Acceptance Criteria
- [ ] Add button enables/disables based on TextBox content
- [ ] New tasks are created in the correct group
- [ ] TreeView refreshes and shows the new task
- [ ] New task is automatically selected after creation
- [ ] TextBox clears after successful creation

---

## Issue 3: Implement Edit Mode with Apply/Cancel Buttons

**Priority: HIGH**

### Description
Add edit mode functionality that blocks the UI and shows Apply/Cancel buttons during editing.

### Requirements
1. Add ToolStrip below the MenuStrip (if not present)
2. Add ToolStripButtons:
   - "Apply Changes" button (checkmark icon)
   - "Cancel" button (X icon)
   - Both right-aligned
   - Hidden by default

3. Enter edit mode when:
   - Any editable field in Task Details is modified
   - Track dirty state

4. In edit mode:
   - Show Apply and Cancel buttons
   - Disable menu items
   - Disable TreeView selection change
   - Only allow interaction with task detail fields and Apply/Cancel buttons

5. Apply button:
   - Validate all fields
   - Save changes to database
   - Update TimeSpent if needed
   - Exit edit mode
   - Refresh TreeView if task name changed
   - Show success message in status bar

6. Cancel button:
   - Revert all field changes
   - Exit edit mode
   - Show cancellation message in status bar

### Acceptance Criteria
- [ ] Edit mode activates when fields are modified
- [ ] UI is properly blocked during edit mode
- [ ] Apply saves changes correctly
- [ ] Cancel reverts changes correctly
- [ ] Status bar shows appropriate messages
- [ ] TreeView cannot be changed during edit

---

## Issue 4: Implement Time Tracking DataGridView

**Priority: HIGH**

### Description
Add a DataGridView in the lower right panel for managing time entries.

### Requirements
1. Create DataGridView in the Time Tracking panel
2. Columns:
   - Description (DataGridViewTextBoxColumn, editable)
   - Start Time (DataGridViewTextBoxColumn, editable, format: yyyy-MM-dd HH:mm)
   - End Time (DataGridViewTextBoxColumn, editable, format: yyyy-MM-dd HH:mm)
   - Duration (DataGridViewTextBoxColumn, read-only, calculated)
   - Time Zone (DataGridViewTextBoxColumn, auto-filled)

3. Functionality:
   - Load time entries for currently selected task
   - Allow adding new rows
   - Allow editing existing rows
   - Allow deleting rows (soft delete)
   - Auto-calculate Duration when Start/End Time changes
   - Auto-fill Time Zone from system
   - Update task TimeSpent when entries change
   - Update project TimeSpent when entries change

4. Validation:
   - End Time must be after Start Time
   - Times must be valid DateTime
   - Show validation errors in status bar

5. Dark Mode:
   - Detect system dark mode
   - Adjust colors appropriately for dark backgrounds

### Acceptance Criteria
- [ ] DataGridView shows time entries for selected task
- [ ] New entries can be added
- [ ] Entries can be edited directly in grid
- [ ] Entries can be deleted
- [ ] Duration calculates automatically
- [ ] Time Zone fills automatically
- [ ] Task and Project TimeSpent update correctly
- [ ] Dark mode colors work properly
- [ ] Validation prevents invalid data entry

---

## Issue 5: Create Groups Management Dialog

**Priority: MEDIUM**

### Description
Implement the Groups management dialog accessible from Edit > Groups menu.

### Requirements
1. Create GroupsListForm:
   - DataGridView showing all groups
   - Columns: Display Name, Description, Symbol, Order, Auto Range, Hidden
   - Sort by clicking column headers
   - Double-click row to open edit dialog
   - "New" button to create new group
   - "Edit" button for selected group
   - "Delete" button for selected group (soft delete, prevent for system groups)
   - "Close" button

2. Create GroupEditForm:
   - TableLayoutPanel layout with all group fields
   - Fields:
     - Display Name (TextBox, required)
     - Description (Multiline TextBox, optional)
     - Symbol (TextBox, single character, optional)
     - Order No (NumericUpDown, optional)
     - Background Color (ColorDialog button + preview)
     - Foreground Color (ColorDialog button + preview)
     - Hidden (CheckBox)
     - Auto Range Span (TextBox with TimeSpan, optional)
     - Auto Range Offset (NumericUpDown, optional)
   - ErrorProvider for validation
   - Focus indication: Yellow background on focus, red on validation error
   - OK and Cancel buttons
   - Prevent editing IsSystem field for system groups

3. Validation rules:
   - Display Name is required
   - Symbol must be 1 character or empty
   - Order No must be positive if specified
   - Colors must be valid hex format
   - Auto Range Span must be valid TimeSpan if specified

### Acceptance Criteria
- [ ] List form shows all groups
- [ ] Sorting works on all columns
- [ ] Double-click opens edit dialog
- [ ] New group can be created
- [ ] Existing group can be edited
- [ ] System groups cannot be deleted
- [ ] System groups' critical fields cannot be edited
- [ ] Validation works with ErrorProvider
- [ ] Focus indication works (yellow/red backgrounds)
- [ ] Changes save to database correctly
- [ ] Modal dialogs are centered on parent

---

## Issue 6: Create Projects Management Dialog

**Priority: MEDIUM**

### Description
Implement the Projects management dialog accessible from Edit > Projects menu.

### Requirements
1. Create ProjectsListForm:
   - DataGridView showing all projects
   - Columns: Name, Description, Category, Due Date, Time Budget, Time Spent
   - Sort by clicking column headers
   - Double-click row to open edit dialog
   - "New", "Edit", "Delete", "Close" buttons
   - Prevent deleting system projects

2. Create ProjectEditForm:
   - TableLayoutPanel layout with all project fields
   - Fields:
     - Project Name (TextBox, required)
     - Description (Multiline TextBox, optional)
     - Category (ComboBox from Categories table)
     - Due Date (DateTimePicker, optional)
     - Time Budget (TextBox with TimeSpan validation, optional)
     - Time Spent (Label, read-only, calculated)
   - ErrorProvider for validation
   - Focus indication (yellow/red backgrounds)
   - OK and Cancel buttons
   - Prevent editing IsSystem field for system projects

3. Validation rules:
   - Project Name is required
   - Due Date must be in future if specified
   - Time Budget must be valid TimeSpan if specified

### Acceptance Criteria
- [ ] List form shows all projects
- [ ] Sorting works correctly
- [ ] New project can be created
- [ ] Existing project can be edited
- [ ] System projects cannot be deleted
- [ ] Category ComboBox shows all categories
- [ ] Time Spent displays correctly
- [ ] Validation works properly
- [ ] Changes save correctly

---

## Issue 7: Create Categories Management Dialog

**Priority: MEDIUM**

### Description
Implement the Categories management dialog accessible from Edit > Categories menu.

### Requirements
1. Create CategoriesListForm:
   - DataGridView showing all categories
   - Columns: Name, Background Color (with color preview), Foreground Color (with color preview)
   - Color preview cells custom-painted
   - Sort by clicking column headers
   - Double-click row to open edit dialog
   - "New", "Edit", "Delete", "Close" buttons
   - Prevent deleting system categories

2. Create CategoryEditForm:
   - TableLayoutPanel layout with category fields
   - Fields:
     - Category Name (TextBox, required)
     - Background Color (ColorDialog button + preview panel)
     - Foreground Color (ColorDialog button + preview panel)
     - Color preview panel showing text "Sample" in selected colors
   - ErrorProvider for validation
   - Focus indication
   - OK and Cancel buttons
   - Prevent editing IsSystem field for system categories

3. Validation rules:
   - Category Name is required
   - Background Color is required (valid hex)
   - Foreground Color is required (valid hex)

### Acceptance Criteria
- [ ] List form shows all categories with color previews
- [ ] Color preview cells render correctly
- [ ] New category can be created
- [ ] Existing category can be edited
- [ ] System categories cannot be deleted
- [ ] ColorDialog works for color selection
- [ ] Preview panel shows colors correctly
- [ ] Validation works
- [ ] Changes save correctly

---

## Issue 8: Create Options Dialog

**Priority: MEDIUM**

### Description
Implement the Options dialog accessible from Tools > Options menu.

### Requirements
1. Create OptionsForm:
   - TabControl with tabs (if more options added later) or simple GroupBox layout
   - Settings:
     - Default Sort Order (ComboBox): Display Name, Due Date, Percent Done, Estimated Effort, Date Created
     - Auto-Backup on Shutdown (CheckBox)
     - Base Font Name (ComboBox with installed fonts)
     - Base Font Size (NumericUpDown, 8-16)
     - UI Language (ComboBox): English, German, Spanish, Japanese, Chinese (Simplified)
   - Font preview label showing "Sample Text 123"
   - Language change warning label (red text): "Application restart required"
   - OK and Cancel buttons
   - Apply button to save without closing

2. Functionality:
   - Load current settings from AppSettings
   - Update preview as settings change
   - Save to AppSettings on OK/Apply
   - Show message box about restart for language change
   - Validate font size range

### Acceptance Criteria
- [ ] All settings load correctly
- [ ] Font preview updates in real-time
- [ ] Settings save to settings.json
- [ ] Language change warning shows
- [ ] Restart requirement is communicated
- [ ] Font list shows installed fonts
- [ ] Validation works for font size

---

## Issue 9: Implement File Operations (New/Open Solution)

**Priority: MEDIUM**

### Description
Implement File > New Solution and File > Open Solution menu commands.

### Requirements
1. File > New Solution:
   - Show SaveFileDialog with .legatro filter
   - Validate file path
   - Close current database
   - Create new database file
   - Initialize schema with EnsureCreated
   - Copy system data (groups, categories) from current DB or recreate
   - Add current user to new database
   - Create default project
   - Update settings with new database path
   - Reload UI with new database

2. File > Open Solution:
   - Show OpenFileDialog with .legatro filter
   - Validate file exists
   - Close current database
   - Open selected database
   - Verify schema (ensure it's a valid Legatro database)
   - Load user (find or create)
   - Update settings with new database path
   - Reload UI with new database

3. Common functionality:
   - Prompt to save changes if current database is dirty
   - Handle errors gracefully
   - Update window title with database name
   - Update status bar with database name

### Acceptance Criteria
- [ ] New Solution creates a valid database
- [ ] System data seeds correctly in new database
- [ ] Open Solution loads existing database
- [ ] User is found or created in opened database
- [ ] UI refreshes correctly with new database
- [ ] Settings update with new path
- [ ] Window title shows database name
- [ ] Status bar shows database name
- [ ] Errors are handled gracefully

---

## Issue 10: Implement Custom Owner-Drawn CheckedListBox for Groups

**Priority: LOW**

### Description
Create a custom CheckedListBox that draws group symbols in their respective colors.

### Requirements
1. Create CustomGroupCheckedListBox : CheckedListBox
2. Override OnDrawItem:
   - Draw checkbox
   - Draw group symbol in group foreground color
   - Draw group name
   - Handle selected state background
   - Handle dark mode colors

3. Data binding:
   - Bind to list of groups
   - Store group object in item tag
   - Extract symbol and colors from group

4. Integration:
   - Replace standard CheckedListBox in Task Details panel
   - Populate with all groups
   - Check boxes for groups the task belongs to
   - Handle check changes to update TasksGroupsRelations

### Acceptance Criteria
- [ ] Control draws symbols in correct colors
- [ ] Checkboxes work correctly
- [ ] Selection highlighting works
- [ ] Dark mode colors apply
- [ ] Task group assignments update correctly
- [ ] Performance is acceptable with many groups

---

## Issue 11: Implement Dark Mode Support

**Priority: LOW**

### Description
Add comprehensive dark mode support throughout the application.

### Requirements
1. Create DarkModeHelper utility class:
   - Detect dark mode: Application.IsDarkModeEnabled
   - Provide color schemes for light and dark modes
   - Helper methods for color adjustment

2. Create DarkModeDataGridView : DataGridView:
   - Automatically adjust colors based on dark mode
   - Background, foreground, grid lines, selection colors
   - Header colors

3. Apply dark mode to forms:
   - MainForm
   - All dialog forms
   - Custom controls

4. Update colors on mode change:
   - Listen for system setting changes (if possible)
   - Refresh UI colors

5. Test with Windows dark mode on/off

### Acceptance Criteria
- [ ] Dark mode detection works
- [ ] DataGridView colors adjust automatically
- [ ] All forms look good in both modes
- [ ] Text is readable in both modes
- [ ] Selection highlights are visible in both modes
- [ ] Custom controls respect dark mode

---

## Issue 12: Implement Localization/Multi-Language Support

**Priority: LOW**

### Description
Add support for multiple UI languages: English, German, Spanish, Japanese, Chinese (Simplified).

### Requirements
1. Create resource files (.resx) for each language:
   - Resources.resx (English, default)
   - Resources.de.resx (German)
   - Resources.es.resx (Spanish)
   - Resources.ja.resx (Japanese)
   - Resources.zh-CN.resx (Chinese Simplified)

2. Move all UI strings to resources:
   - Form titles
   - Labels
   - Button text
   - Menu items
   - Status bar messages
   - Error messages
   - Validation messages

3. Implement language switching:
   - Load language from settings
   - Set Thread.CurrentThread.CurrentUICulture
   - Set Thread.CurrentThread.CurrentCulture for date/time
   - Require application restart for changes

4. Test with all supported languages

### Acceptance Criteria
- [ ] All UI strings are in resource files
- [ ] English resources complete
- [ ] German resources complete
- [ ] Spanish resources complete
- [ ] Japanese resources complete
- [ ] Chinese resources complete
- [ ] Language setting persists
- [ ] Culture-aware date/time formatting works
- [ ] Right-to-left languages handled (if applicable)

---

## Issue 13: Add Task Context Menu and Keyboard Shortcuts

**Priority: LOW**

### Description
Add productivity features like context menus and keyboard shortcuts.

### Requirements
1. TreeView Context Menu:
   - Right-click on task
   - Menu items:
     - Edit Task (also double-click)
     - Mark as Done
     - Delete Task
     - Move to Group >
     - Copy Task
     - Paste Task (if copied)

2. Keyboard Shortcuts:
   - Ctrl+N: New Task
   - Ctrl+S: Save/Apply Changes
   - Escape: Cancel Edit
   - Delete: Delete Selected Task
   - F2: Rename Task
   - Ctrl+F: Find Task (if search implemented)

3. Main Menu Accelerators:
   - &File, &Edit, &Tools (already in place)
   - All menu items have accelerators

### Acceptance Criteria
- [ ] Context menu appears on right-click
- [ ] All context menu items work
- [ ] Keyboard shortcuts work correctly
- [ ] Shortcuts don't conflict with system shortcuts
- [ ] Accelerators work in all dialogs

---

## Issue 14: Add Validation and Enhanced Error Handling

**Priority: MEDIUM**

### Description
Implement comprehensive validation and user-friendly error handling.

### Requirements
1. Field Validations:
   - Required fields (Display Name, Project Name, etc.)
   - Format validations (Email, TimeSpan, Colors)
   - Range validations (Dates, Numeric values)
   - Business rule validations (End Time > Start Time)

2. ErrorProvider Integration:
   - Show error icon next to invalid fields
   - Error tooltips on hover
   - Red background on validation failure
   - Clear errors when fixed

3. Focus Behaviors:
   - Yellow background on focus for text fields
   - Auto-focus first error field on validation failure
   - Tab order set correctly in all forms

4. Error Handling:
   - Try-catch blocks around all operations
   - User-friendly error messages
   - Log errors to file (optional)
   - Show errors in status bar (red text)
   - Detailed error dialog for exceptions

5. Confirmation Dialogs:
   - Confirm before deleting
   - Confirm before overwriting
   - Warn about unsaved changes

### Acceptance Criteria
- [ ] All fields have appropriate validation
- [ ] ErrorProvider shows errors correctly
- [ ] Focus indication works (yellow/red backgrounds)
- [ ] Errors display in status bar
- [ ] Exception dialogs are user-friendly
- [ ] Confirmation dialogs prevent data loss
- [ ] Tab order is logical in all forms

---

## Issue 15: Implement Advanced Features (Search, Filtering, Reporting)

**Priority: LOW**

### Description
Add advanced features to enhance usability.

### Requirements
1. Search/Filter:
   - Search box in toolbar
   - Search by task name, description
   - Filter tasks by project
   - Filter tasks by date range
   - Filter tasks by completion status

2. Time Budget Warnings:
   - Visual indicator when project over budget
   - Warning in status bar
   - Color coding in project list

3. Percent Done Calculation:
   - Calculate from TimeSpent / EstimatedEffort
   - Show in task list
   - Visual progress bar (optional)

4. Task Statistics:
   - Total tasks
   - Completed tasks
   - Total time tracked
   - Display in status bar or dashboard

5. Export:
   - Export tasks to CSV
   - Export time entries to CSV
   - Export for reporting

### Acceptance Criteria
- [ ] Search works correctly
- [ ] Filtering works correctly
- [ ] Time budget warnings show
- [ ] Percent done calculates correctly
- [ ] Statistics display accurately
- [ ] Export produces valid CSV files

---

## Implementation Priority Summary

### Phase 1 (Critical - MVP)
- Issue 1: Task Details Panel
- Issue 2: New Task Entry
- Issue 3: Edit Mode with Apply/Cancel
- Issue 4: Time Tracking DataGridView

### Phase 2 (Important - Core Features)
- Issue 5: Groups Management
- Issue 6: Projects Management
- Issue 7: Categories Management
- Issue 8: Options Dialog
- Issue 9: File Operations
- Issue 14: Validation and Error Handling

### Phase 3 (Nice to Have - Polish)
- Issue 10: Custom CheckedListBox
- Issue 11: Dark Mode
- Issue 13: Context Menu and Shortcuts

### Phase 4 (Future - Advanced)
- Issue 12: Localization
- Issue 15: Advanced Features

## Development Guidelines

1. **Follow WinForms Agent Guidelines**: Adhere to all rules in the agent instructions
2. **Designer Files**: Keep InitializeComponent simple and serializable
3. **Modern C#**: Use latest C# features in regular code files
4. **Testing**: Test each feature thoroughly before marking as complete
5. **Commit Frequently**: Small, focused commits with clear messages
6. **Documentation**: Update README and implementation status as features complete

## Notes

- Each issue should be created as a separate GitHub issue with appropriate labels
- Issues can be tackled in parallel by multiple developers
- Cross-reference related issues
- Update this document as issues are completed or priorities change
