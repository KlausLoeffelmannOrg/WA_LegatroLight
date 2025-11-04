# LegatroLight - Complete Specification

**Project:** LegatroLight (Legatro Exp)  
**Purpose:** Task Management and Time Tracking for Projects  
**Technologies:** SQLite, EF Core, .NET 10, WinForms  
**Database File Extension:** .legatro  
**Version:** 2025-10-22  
**Author:** Product Management

---

## Table of Contents

1. [Application Overview](#application-overview)
2. [First Start Experience](#first-start-experience)
3. [Database Schema](#database-schema)
4. [Base Data Management](#base-data-management)
5. [Symbol and Color Management](#symbol-and-color-management)
6. [Task and Time Tracking](#task-and-time-tracking)
7. [User Interface Specification](#user-interface-specification)
8. [General Requirements](#general-requirements)
9. [Terminology Reference](#terminology-reference)

---

## Application Overview

LegatroLight (Legatro Exp) is a WinForms-based time tracking and task management application designed for individual users to organize their work across multiple projects and tasks. The application emphasizes simplicity, accessibility, and seamless Windows integration through automatic authentication.

### Key Features

- **Windows Authentication Integration:** Automatic user login using Windows credentials
- **Hierarchical Task Organization:** Projects contain tasks; tasks are organized into groups
- **Visual Organization:** Color-coded categories and symbolic icons for groups
- **Time Tracking:** Built-in timer with manual time entry capabilities
- **SQLite-Based:** Portable database files with .legatro extension
- **Cross-Platform Potential:** Built on .NET 10 for future expansion

### Design Philosophy

The application prioritizes:
- **Minimal friction:** Quick task entry and time tracking
- **Accessibility:** Full keyboard navigation, screen reader support, high-contrast mode compatibility
- **Data integrity:** Soft deletion, comprehensive sync support, validation at commit time
- **User experience:** Consistent 11pt Segoe UI font, smart defaults, context-aware behavior

### Implementation Note

**IMPORTANT:** Implement as much of these requirements as possible in a single implementation pass. If complete implementation is not feasible in one iteration, create detailed GitHub issues describing how to implement each missing feature, including acceptance criteria and implementation guidance.

---

## First Start Experience

### User Setup Assistant

When the application starts for the first time, a dedicated **User Setup Assistant** form appears, centered on screen. This form automatically retrieves user information from Windows authentication.

**Authentication Process:**
- Uses `WindowsIdentity` to retrieve the current Windows user
- Where applicable (domain controller scenarios), uses `WindowsPrincipal` for extended information
- The currently logged-on Windows user is automatically authenticated into LegatroLight
- No login dialog appears on subsequent starts—the Windows user is automatically logged in

**User Information Collection:**
During initial setup, users may optionally provide:
- Nickname or preferred display name
- Email address
- Additional personal information

All fields except those auto-populated from Windows are optional.

### Initial Data Creation

On first start, the application automatically creates:

#### Default Project
- **Name:** "Default"
- **IsSystem:** `true` (cannot be deleted or edited)
- **Purpose:** Fallback project for uncategorized tasks

#### System Groups
The following groups are created with specific `OrderNo` values:

| OrderNo | Group Name | Purpose |
|---------|------------|---------|
| 1 | My Day | Tasks for the current day |
| 2 | Sliding Week | Tasks for the current week |
| 3 | My Month | Tasks for the current month |
| 4 | Roaming Links | Quick access to reference materials |
| 5 | Roaming Notes | Free-form notes and ideas |
| 65534 | Done | Completed tasks |
| 65535 | Clutter | Low-priority or deferred tasks |

**Properties:**
- All groups have `IsSystem = true` (cannot be edited or deleted)
- `OrderNo` determines display order in TreeView
- Groups support `AutoRangeSpan` and `AutoRangeOffset` for automatic task assignment

#### System Categories
Ten color categories are created, named after their colors:
- Red
- Orange
- Yellow
- Green
- Blue
- Purple
- Pink
- Brown
- Gray
- Black

**Properties:**
- Each category uses its namesake color for `BackColor`
- `ForeColor` is calculated to ensure WCAG-compliant contrast
- `IsSystem = true` (cannot be edited or deleted)
- Categories provide visual distinction for projects and tasks

---

## Database Schema

### Schema Design Principles

All tables follow a consistent schema design with mandatory common fields, GUID-based primary keys, and soft deletion support. The schema is optimized for:
- **Sync scenarios:** GUIDs enable distributed database merging
- **Audit trails:** Comprehensive timestamp tracking
- **Data recovery:** Soft deletion with `DateDeleted` field
- **Performance:** Strategic indexing on commonly queried fields

### Common Fields for All Tables

Every table in the database includes these mandatory fields:

| Field Name | Data Type | Attributes | Description |
|------------|-----------|------------|-------------|
| `ID[TableName]` | `TEXT` (GUID) | PRIMARY KEY, NOT NULL | Unique identifier; field name is table-specific (e.g., `IDCategory`, `IDProject`, `IDTask`) |
| `DateCreated` | `TEXT` (ISO 8601) | NOT NULL, INDEX | UTC timestamp when record was created |
| `DateLastEdited` | `TEXT` (ISO 8601) | NOT NULL | UTC timestamp when record was last modified |
| `DateDeleted` | `TEXT` (ISO 8601) | NULL, INDEX | UTC timestamp when record was soft-deleted; NULL indicates active record |
| `SyncGuidChanged` | `TEXT` (GUID) | NOT NULL | Unique identifier updated on every change; used for sync conflict resolution |

**Notes:**
- All GUIDs are stored as TEXT in RFC 4122 format (e.g., `"550e8400-e29b-41d4-a716-446655440000"`)
- All timestamps use ISO 8601 format (e.g., `"2025-10-22T14:30:00Z"`)
- TimeSpan values are stored as TEXT in standard .NET format (e.g., `"1.02:30:45"` for 1 day, 2 hours, 30 minutes, 45 seconds)
- Boolean values are stored as INTEGER (0 = false, 1 = true)

---

### Users Table

Stores user information retrieved from Windows authentication. This table is the foundation for all user-specific data in the application.

| Field Name | Data Type | Attributes | Description |
|------------|-----------|------------|-------------|
| `IDUser` | `TEXT` (GUID) | PRIMARY KEY, NOT NULL | Unique user identifier |
| `UserDisplayId` | `TEXT` | NOT NULL, UNIQUE | User-friendly display ID (e.g., "klaus", "john.smith") |
| `UserAuthID` | `TEXT` | NOT NULL, UNIQUE | Windows authentication ID (e.g., "DOMAIN\\username") |
| `UserSid` | `TEXT` | NOT NULL, UNIQUE | Windows Security Identifier (SID) |
| `DomainOrMachine` | `TEXT` | NULL | Domain name or machine name for the user |
| `UserName` | `TEXT` | NOT NULL | Windows username without domain |
| `DisplayName` | `TEXT` | NOT NULL | Full display name (e.g., "Klaus Loeffelmann") |
| `FirstName` | `TEXT` | NULL | User's first name |
| `MiddleName` | `TEXT` | NULL | User's middle name or initial |
| `LastName` | `TEXT` | NULL | User's last name |
| `Email` | `TEXT` | NULL | Email address |
| *(Common fields)* | | | DateCreated, DateLastEdited, DateDeleted, SyncGuidChanged |

**Indexes:**
- `IX_Users_UserAuthID` on `UserAuthID` (UNIQUE)
- `IX_Users_UserSid` on `UserSid` (UNIQUE)
- `IX_Users_UserDisplayId` on `UserDisplayId` (UNIQUE)

**Business Rules:**
- `UserAuthID` and `UserSid` are populated automatically from Windows authentication
- `UserDisplayId` may be set by the user during initial setup; defaults to `UserName`
- `DisplayName` is populated from Windows; may be overridden by user
- `Email` is optional and may be set by user or retrieved from Active Directory

---

### SymbolConfiguration Table

Defines available symbols for visual representation of Groups and Categories throughout the UI. Symbols are single-character icons from the **Segoe Fluent Icons** font.

| Field Name | Data Type | Attributes | Description |
|------------|-----------|------------|-------------|
| `IDSymbol` | `TEXT` (GUID) | PRIMARY KEY, NOT NULL | Unique symbol identifier |
| `SymbolChar` | `TEXT` (1 char) | NOT NULL, UNIQUE | The Unicode character from Segoe Fluent Icons font |
| `SymbolName` | `TEXT` | NOT NULL, UNIQUE | Human-readable name (e.g., "Calendar", "Star", "Folder", "Checkmark") |
| `DefaultBackColor` | `TEXT` | NOT NULL | Default background color in hex format (e.g., "#FFFFFF") |
| `DefaultForeColor` | `TEXT` | NOT NULL | Default foreground/text color in hex format (e.g., "#000000") |
| `ContrastRatio` | `REAL` | NOT NULL | Calculated WCAG contrast ratio between foreground and background |
| `IsSystem` | `INTEGER` (Boolean) | NOT NULL | Indicates if the symbol is system-defined (cannot be edited or deleted) |
| *(Common fields)* | | | DateCreated, DateLastEdited, DateDeleted, SyncGuidChanged |

**Indexes:**
- `IX_SymbolConfiguration_SymbolName` on `SymbolName` (UNIQUE)
- `IX_SymbolConfiguration_SymbolChar` on `SymbolChar` (UNIQUE)
- `IX_SymbolConfiguration_IsSystem` on `IsSystem`

**Business Rules:**
- System symbols (`IsSystem = true`) provide default options and cannot be deleted
- Users may create custom symbols by adding new SymbolConfiguration records
- `ContrastRatio` must meet WCAG AA standards (minimum 4.5:1) for accessibility
- When a Group or Category references a symbol, it may override the default colors

**Typical System Symbols:**
- Calendar (📅)
- Star (⭐)
- Folder (📁)
- Checkmark (✓)
- Flag (🚩)
- Heart (❤)
- Lightning (⚡)
- Clock (🕐)

---

### Categories Table

Defines color categories for visual organization of projects and tasks. Categories provide an additional layer of visual distinction beyond groups.

| Field Name | Data Type | Attributes | Description |
|------------|-----------|------------|-------------|
| `IDCategory` | `TEXT` (GUID) | PRIMARY KEY, NOT NULL | Unique category identifier |
| `CategoryName` | `TEXT` | NOT NULL, UNIQUE | Name of the category (e.g., "Red", "Marketing", "Personal") |
| `IDUser` | `TEXT` (GUID) | NOT NULL, FOREIGN KEY → Users(IDUser), INDEX | User who created the category |
| `IDSymbol` | `TEXT` (GUID) | NULL, FOREIGN KEY → SymbolConfiguration(IDSymbol), INDEX | Optional symbol for the category |
| `BackColor` | `TEXT` | NOT NULL | Background color in hex format (e.g., "#FF5733") |
| `ForeColor` | `TEXT` | NOT NULL | Foreground/text color in hex format (e.g., "#FFFFFF") |
| `IsSystem` | `INTEGER` (Boolean) | NOT NULL | Indicates if the category is system-defined (cannot be edited or deleted) |
| *(Common fields)* | | | DateCreated, DateLastEdited, DateDeleted, SyncGuidChanged |

**Indexes:**
- `IX_Categories_IDUser` on `IDUser`
- `IX_Categories_CategoryName` on `CategoryName`
- `IX_Categories_IDSymbol` on `IDSymbol`

**Business Rules:**
- System categories (created on first start) have `IsSystem = true`
- `CategoryName` must be unique per user
- If `IDSymbol` is set, the category may override the symbol's default colors
- `BackColor` and `ForeColor` must provide sufficient contrast (WCAG AA: 4.5:1 minimum)

---

### Groups Table

Defines groups for organizing tasks. Groups provide a primary organizational structure, displayed in a TreeView on the main screen. Tasks can be assigned to groups either automatically (based on date ranges) or manually.

| Field Name | Data Type | Attributes | Description |
|------------|-----------|------------|-------------|
| `IDGroup` | `TEXT` (GUID) | PRIMARY KEY, NOT NULL | Unique group identifier |
| `GroupDisplayName` | `TEXT` | NOT NULL | Display name shown in TreeView and dialogs |
| `GroupDescription` | `TEXT` | NULL | Optional longer description of the group's purpose |
| `CreatedByIDUser` | `TEXT` (GUID) | NOT NULL, FOREIGN KEY → Users(IDUser), INDEX | User who created the group |
| `IDSymbol` | `TEXT` (GUID) | NULL, FOREIGN KEY → SymbolConfiguration(IDSymbol), INDEX | Optional symbol displayed next to group name |
| `OrderNo` | `INTEGER` | NULL | Defines display order in TreeView (lower numbers appear first) |
| `BackColor` | `TEXT` | NOT NULL | Background color in hex format |
| `ForeColor` | `TEXT` | NOT NULL | Foreground/text color in hex format |
| `IsHidden` | `INTEGER` (Boolean) | NOT NULL | If true, group is not shown in TreeView |
| `IsSystem` | `INTEGER` (Boolean) | NOT NULL | If true, group is system-defined and cannot be edited or deleted |
| `AutoRangeSpan` | `TEXT` (TimeSpan) | NULL | Duration for automatic task assignment (e.g., "7.00:00:00" for 7 days) |
| `AutoRangeOffset` | `INTEGER` | NULL | Number of days to offset the AutoRangeSpan (positive = future, negative = past) |
| *(Common fields)* | | | DateCreated, DateLastEdited, DateDeleted, SyncGuidChanged |

**Indexes:**
- `IX_Groups_CreatedByIDUser` on `CreatedByIDUser`
- `IX_Groups_GroupDisplayName` on `GroupDisplayName`
- `IX_Groups_OrderNo` on `OrderNo`
- `IX_Groups_IDSymbol` on `IDSymbol`

**Automatic Task Assignment Logic:**
If `AutoRangeSpan` is not NULL:
1. Calculate range start: `DateTime.Today + TimeSpan.FromDays(AutoRangeOffset ?? 0)`
2. Calculate range end: `RangeStart + AutoRangeSpan`
3. Tasks with `DateCreated` OR `DueDate` within this range are automatically assigned to the group

**Example Configurations:**
- **My Day:** `AutoRangeSpan = "1.00:00:00"`, `AutoRangeOffset = 0` (tasks for today)
- **Sliding Week:** `AutoRangeSpan = "7.00:00:00"`, `AutoRangeOffset = 0` (tasks for this week)
- **Next Week:** `AutoRangeSpan = "7.00:00:00"`, `AutoRangeOffset = 7` (tasks for next week)

**Business Rules:**
- System groups (`IsSystem = true`) cannot be edited or deleted
- `OrderNo` controls TreeView display order; NULL values appear after numbered groups, sorted alphabetically
- If `IDSymbol` is set, the group may override the symbol's default colors
- Hidden groups (`IsHidden = true`) do not appear in TreeView but remain queryable

---

### Projects Table

Defines projects that contain tasks and track time budgets. Projects are the primary organizational unit for work activities.

| Field Name | Data Type | Attributes | Description |
|------------|-----------|------------|-------------|
| `IDProject` | `TEXT` (GUID) | PRIMARY KEY, NOT NULL | Unique project identifier |
| `IDUser` | `TEXT` (GUID) | NOT NULL, FOREIGN KEY → Users(IDUser), INDEX | User who owns the project |
| `IDCategory` | `TEXT` (GUID) | NULL, FOREIGN KEY → Categories(IDCategory), INDEX | Optional category for visual organization |
| `ProjectName` | `TEXT` | NOT NULL | Name of the project (e.g., "WinForms Designer Refactor") |
| `Description` | `TEXT` | NULL | Detailed project description or notes |
| `DueDate` | `TEXT` (ISO 8601) | NULL, INDEX | Project deadline (UTC); NULL indicates no deadline |
| `IsSystem` | `INTEGER` (Boolean) | NOT NULL | If true, project is system-defined (e.g., "Default") and cannot be deleted |
| `TimeBudget` | `TEXT` (TimeSpan) | NULL | Allocated time budget (e.g., "40:00:00" for 40 hours) |
| `TimeSpent` | `TEXT` (TimeSpan) | NOT NULL, DEFAULT '0:00:00' | Total time logged on project (calculated from TimeEntries) |
| *(Common fields)* | | | DateCreated, DateLastEdited, DateDeleted, SyncGuidChanged |

**Indexes:**
- `IX_Projects_IDUser` on `IDUser`
- `IX_Projects_IDCategory` on `IDCategory`
- `IX_Projects_ProjectName` on `ProjectName`
- `IX_Projects_DueDate` on `DueDate`

**Time Tracking:**
- `TimeSpent` is automatically calculated as the sum of `Duration` from all associated TimeEntries
- When a TimeEntry is created, updated, or deleted, `TimeSpent` is recalculated
- `TimeBudget` provides a target for project planning; exceeding budget triggers visual warnings

**Business Rules:**
- System projects (`IsSystem = true`) cannot be deleted but may have time logged against them
- `ProjectName` should be unique per user (soft constraint, not enforced at database level)
- Deleting a project (soft delete) does not delete associated tasks or time entries

---

### TodoTasks Table

Defines individual tasks within projects. Tasks are the atomic unit of work and can be assigned to multiple groups.

| Field Name | Data Type | Attributes | Description |
|------------|-----------|------------|-------------|
| `IDTodoTask` | `TEXT` (GUID) | PRIMARY KEY, NOT NULL | Unique task identifier |
| `IDUser` | `TEXT` (GUID) | NOT NULL, FOREIGN KEY → Users(IDUser), INDEX | User who created the task |
| `IDProject` | `TEXT` (GUID) | NOT NULL, FOREIGN KEY → Projects(IDProject), INDEX | Parent project |
| `DisplayName` | `TEXT` | NOT NULL | Short task name displayed in TreeView and grids |
| `Description` | `TEXT` | NULL | Detailed task description or notes |
| `DueDate` | `TEXT` (ISO 8601) | NULL, INDEX | Task deadline (UTC); NULL indicates no specific deadline |
| `EstimatedEffort` | `TEXT` (TimeSpan) | NULL | Estimated time required to complete the task |
| `TimeSpent` | `TEXT` (TimeSpan) | NOT NULL, DEFAULT '0:00:00' | Actual time logged on task (calculated from TimeEntries) |
| `PercentDone` | `INTEGER` | NOT NULL, DEFAULT 0 | Completion percentage (0-100) |
| `DateFinished` | `TEXT` (ISO 8601) | NULL, INDEX | UTC timestamp when task was marked complete; NULL if incomplete |
| `Priority` | `INTEGER` | NOT NULL, DEFAULT 3 | Priority level (1=Highest, 5=Lowest); used for sorting |
| *(Common fields)* | | | DateCreated, DateLastEdited, DateDeleted, SyncGuidChanged |

**Indexes:**
- `IX_TodoTasks_IDUser` on `IDUser`
- `IX_TodoTasks_IDProject` on `IDProject`
- `IX_TodoTasks_DisplayName` on `DisplayName`
- `IX_TodoTasks_DueDate` on `DueDate`
- `IX_TodoTasks_DateFinished` on `DateFinished`
- `IX_TodoTasks_DateCreated` on `DateCreated`
- `IX_TodoTasks_Priority` on `Priority`

**Calculated Fields:**
- `TimeSpent` is automatically calculated as the sum of `Duration` from all associated TimeEntries
- `PercentDone` is calculated automatically based on `TimeSpent` / `EstimatedEffort` ratio
  - If `EstimatedEffort` is NULL, `PercentDone` remains 0 or is set manually
  - When `TimeSpent` >= `EstimatedEffort`, `PercentDone` is capped at 100%

**Business Rules:**
- When `PercentDone` reaches 100%, `DateFinished` is automatically set to current UTC time
- Tasks can be reassigned to different projects
- Tasks appear in TreeView under their assigned groups (via TasksGroupsRelations)
- Tasks with no group assignments appear in a default "Ungrouped" node

---

### TodoTasksGroupsRelations Table

Many-to-many relationship table enabling manual assignment of tasks to groups. This supplements the automatic assignment based on `AutoRangeSpan`.

| Field Name | Data Type | Attributes | Description |
|------------|-----------|------------|-------------|
| `IDTodoTasksGroups` | `TEXT` (GUID) | PRIMARY KEY, NOT NULL | Unique relation identifier |
| `IDTodoTask` | `TEXT` (GUID) | NOT NULL, FOREIGN KEY → Tasks(IDTask), INDEX | Associated task |
| `IDGroup` | `TEXT` (GUID) | NOT NULL, FOREIGN KEY → Groups(IDGroup), INDEX | Associated group |
| `IDUser` | `TEXT` (GUID) | NOT NULL, FOREIGN KEY → Users(IDUser), INDEX | User who created the relation |
| *(Common fields)* | | | DateCreated, DateLastEdited, DateDeleted, SyncGuidChanged |

**Indexes:**
- `IX_TodoTasksGroupsRelations_IDTodoTask` on `IDTodoTask`
- `IX_TodoTasksGroupsRelations_IDGroup` on `IDGroup`
- `IX_TodoTasksGroupsRelations_IDUser` on `IDUser`
- `IX_TodoTasksGroupsRelations_Composite` on `(IDTodoTask, IDGroup)` (UNIQUE)

**Business Rules:**
- A task can be manually assigned to multiple groups
- A task cannot be assigned to the same group more than once (enforced by composite unique index)
- When displaying tasks in TreeView, both automatic (based on `AutoRangeSpan`) and manual (via this table) assignments are shown
- Deleting a group (soft delete) does not delete relation records; they are filtered via `DateDeleted`

---

### TimeEntries Table

Records individual time tracking entries for tasks. This is the core table for time tracking functionality.

| Field Name | Data Type | Attributes | Description |
|------------|-----------|------------|-------------|
| `IDTimeEntry` | `TEXT` (GUID) | PRIMARY KEY, NOT NULL | Unique time entry identifier |
| `IDUser` | `TEXT` (GUID) | NOT NULL, FOREIGN KEY → Users(IDUser), INDEX | User who logged the time |
| `IDProject` | `TEXT` (GUID) | NOT NULL, FOREIGN KEY → Projects(IDProject), INDEX | Associated project |
| `IDTodoTask` | `TEXT` (GUID) | NOT NULL, FOREIGN KEY → Tasks(IDTask), INDEX | Associated task |
| `DescriptionDoneWork` | `TEXT` | NULL | Description of work completed during this time entry |
| `StartTime` | `TEXT` (ISO 8601) | NOT NULL, INDEX | UTC start time of work session |
| `EndTime` | `TEXT` (ISO 8601) | NOT NULL, INDEX | UTC end time of work session |
| `Duration` | `TEXT` (TimeSpan) | NOT NULL | Calculated duration (EndTime - StartTime) |
| `TimeZone` | `TEXT` | NOT NULL | IANA time zone identifier where entry was recorded (e.g., "America/Los_Angeles") |
| *(Common fields)* | | | DateCreated, DateLastEdited, DateDeleted, SyncGuidChanged |

**Indexes:**
- `IX_TimeEntries_IDUser` on `IDUser`
- `IX_TimeEntries_IDProject` on `IDProject`
- `IX_TimeEntries_IDTodoTask` on `IDTodoTask`
- `IX_TimeEntries_StartTime` on `StartTime`
- `IX_TimeEntries_EndTime` on `EndTime`

**Calculation Rules:**
- `Duration` is always calculated as `EndTime - StartTime`
- Both `StartTime` and `EndTime` are stored in UTC
- `TimeZone` preserves the local time zone for display purposes (e.g., "Entry created at 2:00 PM Pacific Time")

**Validation Rules (enforced on commit):**
- `StartTime` must be less than `EndTime`
- `Duration` must be greater than 0
- `IDProject` and `IDTodoTask` must reference valid, non-deleted records
- Optional: Warn (but do not block) if time entries overlap for the same user

**Business Rules:**
- When a TimeEntry is created, updated, or deleted:
  - Recalculate `TodoTask.TimeSpent` for the associated task
  - Recalculate `Project.TimeSpent` for the associated project
- Time entries can be edited after creation (inline in DataGridView)
- Soft-deleted time entries are excluded from `TimeSpent` calculations

---

## Base Data Management

### Overview

Base data management provides a consistent interface for creating, viewing, and maintaining foundational elements: **Projects**, **Groups**, and **Categories**. Each entity type follows the same interaction pattern, ensuring user familiarity and reducing cognitive load.

### Access Points

The **Edit** menu in the main MenuStrip contains entries for:
- **Groups…** (Alt+E, then G)
- **Projects…** (Alt+E, then P)
- **Categories…** (Alt+E, then C)

Selecting any menu item opens a modal **List Dialog** specific to that entity type.

---

### List Dialogs

Each list dialog presents a comprehensive view of all records for the selected entity type.

#### Layout and Behavior

- **Main Control:** `DataGridView` with:
  - **Selection Mode:** Full-row selection
  - **Read-Only:** Data cannot be edited directly in the grid
  - **Sortable Columns:** Click any column header to sort
  - **Multi-Select:** Disabled; only single-row selection allowed
  - **Double-Click Action:** Opens the edit dialog for the selected record

- **Toolbar/Button Bar:** Contains the following buttons (with icons and tooltips):
  - **New…** (Ctrl+N): Opens edit dialog with empty record
  - **Edit…** (Enter or Double-Click): Opens edit dialog with selected record
  - **Delete** (Del): Soft-deletes selected record (confirmation dialog appears)
  - **Close** (Esc): Closes the list dialog

#### Column Configuration

**Groups List:**
| Column Header | Field | Width | Sortable |
|---------------|-------|-------|----------|
| Symbol | GroupSymbol (rendered) | 50px | Yes |
| Group Name | GroupDisplayName | 200px | Yes |
| Description | GroupDescription | 300px | Yes |
| Order | OrderNo | 80px | Yes |
| Hidden | IsHidden (checkbox) | 60px | Yes |
| Created | DateCreated (local time) | 150px | Yes |

**Projects List:**
| Column Header | Field | Width | Sortable |
|---------------|-------|-------|----------|
| Project Name | ProjectName | 200px | Yes |
| Category | Category.CategoryName | 120px | Yes |
| Time Budget | TimeBudget (formatted) | 100px | Yes |
| Time Spent | TimeSpent (formatted) | 100px | Yes |
| Due Date | DueDate (local date) | 120px | Yes |
| Created | DateCreated (local time) | 150px | Yes |

**Categories List:**
| Column Header | Field | Width | Sortable |
|---------------|-------|-------|----------|
| Symbol | Symbol (rendered) | 50px | Yes |
| Category Name | CategoryName | 200px | Yes |
| Color | BackColor (color swatch) | 80px | No |
| Created | DateCreated (local time) | 150px | Yes |

#### Visual Design

- **Grid Row Height:** 28 pixels (accommodates 11pt Segoe UI + padding)
- **Alternating Row Colors:** Light gray for even rows in light mode; adjusted for dark mode
- **Selection Color:** System highlight color
- **Grid Lines:** Vertical and horizontal, subtle gray (#E0E0E0 in light mode)

---

### Edit Dialogs

Double-clicking a record in the list or pressing **New** opens a modal **Edit Dialog** for that entity type.

#### General Behavior

- **Modality:** True (blocks interaction with parent window)
- **Position:** Centered on parent list dialog
- **Size:** Fixed width (600px), auto-height based on content
- **Resizable:** No
- **Title Bar:**
  - New record: "New [Entity Type]"
  - Editing: "Edit [Entity Type]: [RecordName]"

#### Layout Structure

All edit dialogs use a **TableLayoutPanel** with two columns:
- **Column 1 (Labels):** Right-aligned, 150px fixed width
- **Column 2 (Controls):** Left-aligned, auto-width (fills remaining space)

**Row Spacing:**
- 8px vertical padding between rows
- 12px horizontal padding from dialog edges

#### Field Presentation

**Label Requirements:**
- Format: `"[Field Name]:"`
- Font: 11pt Segoe UI
- Accelerator Key: Underlined letter (e.g., "&Project Name:")
- Accessible Name: Set to field purpose
- Associated Control: Linked via `TabIndex` and `UseMnemonic`

**Control Requirements:**
- **Tab Order:** Sequential, top-to-bottom
- **Focus Indicator:** Yellow background when focused (`BackColor = #FFFFCC`)
- **Validation Error:** Light red background (`BackColor = #FFE6E6`)
- **Error Provider:** Red icon with tooltip appears next to invalid fields (only after OK click)
- **Accessible Name:** Describes control purpose
- **Accessible Description:** Provides additional context or format requirements

#### Field Types by Category

**Non-Editable Fields (Display Only):**
- IDs (all `ID*` fields)
- `DateCreated`
- `DateLastEdited`
- `DateDeleted`
- `SyncGuidChanged`

**Presentation:** ReadOnly TextBoxes with gray background (#F0F0F0)

**Text Fields:**
- `DisplayName`, `ProjectName`, `GroupDisplayName`, `CategoryName`: Single-line TextBox
- `Description`, `GroupDescription`: Multi-line TextBox (5 rows, vertical scrollbar)

**Foreign Key Fields:**
- Presented as ComboBoxes
- Display the related entity's display name (e.g., `User.DisplayName`, `Category.CategoryName`)
- Store the related entity's ID
- Include a "(None)" option for nullable foreign keys

**Date Fields:**
- `DueDate`: DateTimePicker with custom format "ddd, MMM dd, yyyy"
- Includes checkbox to enable/disable (sets NULL when disabled)

**TimeSpan Fields:**
- `TimeBudget`, `EstimatedEffort`: Custom MaskedTextBox with format "HH:MM:SS"
- Includes quick-select buttons: "+15m", "+30m", "+1h", "+4h"

**Boolean Fields:**
- `IsHidden`: CheckBox with label "Hidden from TreeView"
- `IsSystem`: ReadOnly CheckBox (grayed out, non-editable)

**Color Fields:**
- `BackColor`, `ForeColor`: Button that opens ColorDialog on click
- Button face shows current color with contrasting text displaying hex value
- Includes "Reset to Default" link below button

**Symbol Fields:**
- `GroupSymbol`, `IDSymbol`: ComboBox with custom owner-draw
- Each item displays: Symbol character (in Segoe Fluent Icons) + Symbol name
- Includes preview of symbol with current colors

#### Validation

**Timing:**
- Validation occurs **ONLY** when user clicks **OK**
- No validation occurs during field editing (allows progressive entry)

**Validation Rules by Field:**
- `DisplayName`, `ProjectName`, `GroupDisplayName`, `CategoryName`: Must not be empty, must not exceed 255 characters
- `TimeBudget`, `EstimatedEffort`: Must be valid TimeSpan format, must be >= 0
- `DueDate`: Must be valid date
- `BackColor`, `ForeColor`: Must be valid hex color, must meet WCAG AA contrast ratio (4.5:1)

**Error Feedback:**
- **Visual:** Field background turns light red
- **Icon:** ErrorProvider icon appears to right of field
- **Tooltip:** ErrorProvider displays specific error message on hover
- **Focus:** Focus does **NOT** automatically move to first invalid field
- **OK Button:** Remains disabled until all errors are resolved

**Multi-Error Handling:**
- All validation errors are displayed simultaneously
- User can fix errors in any order
- OK button becomes enabled only when all fields are valid

#### Button Bar

Bottom of dialog, right-aligned:
- **OK** (Enter): Commits changes and closes dialog
- **Cancel** (Esc): Discards changes and closes dialog

**OK Button Behavior:**
1. Trigger validation for all fields
2. If validation fails: Display errors, remain open
3. If validation succeeds: Return updated entity to parent dialog, close

**Cancel Button Behavior:**
- No validation occurs
- All changes are discarded
- Returns NULL to parent dialog

---

### Data Exchange Pattern

Each dialog operates with a clean data handover pattern:

**Opening Edit Dialog:**
1. Parent list dialog creates new entity instance or clones selected entity
2. Passes entity instance to edit dialog constructor
3. Edit dialog populates fields from entity properties

**Returning from Edit Dialog:**
1. On OK: Edit dialog updates entity instance with field values
2. Edit dialog returns entity instance to parent
3. Parent dialog calls EF Core `Update()` or `Add()` method
4. Parent dialog calls `SaveChanges()`
5. Parent dialog refreshes DataGridView

**Error Handling:**
- If `SaveChanges()` fails (e.g., constraint violation), display error MessageBox
- Reopen edit dialog with same entity instance
- User can correct errors or cancel

---

### Keyboard Navigation

All dialogs support comprehensive keyboard navigation:

**In List Dialog:**
- **Arrow Keys:** Navigate between rows
- **Enter:** Open edit dialog for selected row
- **Ctrl+N:** Create new record
- **Del:** Delete selected record (with confirmation)
- **Esc:** Close dialog
- **Alt+[Letter]:** Jump to button (e.g., Alt+N for New)

**In Edit Dialog:**
- **Tab / Shift+Tab:** Navigate between fields
- **Alt+[Letter]:** Jump to field (e.g., Alt+P for Project Name)
- **Enter:** Click OK (if focus is on button or last field)
- **Esc:** Click Cancel
- **Space:** Toggle CheckBoxes, open ComboBoxes

---

### Accessibility Features

**Screen Reader Support:**
- All fields have meaningful `AccessibleName` properties
- All fields have descriptive `AccessibleDescription` properties
- Tab order follows logical reading order
- Form title is announced when dialog opens

**High-Contrast Mode:**
- Symbolic icons render in monochrome
- Color swatches show as labeled buttons
- Focus indicators use high-contrast borders
- Validation errors use icons, not just color

**Keyboard-Only Operation:**
- Every action is accessible via keyboard
- No mouse-only functionality
- Visual focus indicators on all controls
- Logical tab order throughout

---

### Dialog State Persistence

Dialog size and position are persisted per user in `%LOCALAPPDATA%\LegatroExp\settings.json`:

```json
{
  "dialogs": {
    "GroupsList": {
      "width": 800,
      "height": 600,
      "left": 100,
      "top": 100
    },
    "GroupsEdit": {
      "width": 600,
      "height": 500
    }
  }
}
```

**Persistence Rules:**
- List dialogs: Remember size and position
- Edit dialogs: Remember size only (always centered)
- Multi-monitor support: Validate saved position is on visible screen
- First run: Use default sizes and center on screen

---

## Symbol and Color Management

### Overview

Symbols provide visual distinction for **Groups** and **Categories** throughout the application UI. Each symbol is a single Unicode character rendered in the **Segoe Fluent Icons** font, paired with customizable foreground and background colors.

**Visual Locations:**
- TreeView: Next to group and task names
- ComboBoxes: In project and category selectors
- DataGridViews: In group and category columns
- Edit Dialogs: In symbol selection controls

---

### Symbol Configuration

Symbols are defined in the **SymbolConfiguration** table, which provides a library of available symbols for users to choose from.

#### Default System Symbols

On first start, the application creates system symbols for common use cases:

| SymbolChar | SymbolName | DefaultBackColor | DefaultForeColor | Usage Context |
|------------|------------|------------------|------------------|---------------|
| 📅 (U+1F4C5) | Calendar | #4A90E2 | #FFFFFF | Date-based groups |
| ⭐ (U+2B50) | Star | #F5A623 | #FFFFFF | Important items |
| 📁 (U+1F4C1) | Folder | #7ED321 | #000000 | General organization |
| ✓ (U+2713) | Checkmark | #50E3C2 | #000000 | Completed items |
| 🚩 (U+1F6A9) | Flag | #D0021B | #FFFFFF | Priority items |
| ❤ (U+2764) | Heart | #BD10E0 | #FFFFFF | Personal projects |
| ⚡ (U+26A1) | Lightning | #F8E71C | #000000 | Quick tasks |
| 🕐 (U+1F550) | Clock | #9013FE | #FFFFFF | Time-sensitive |
| 📝 (U+1F4DD) | Note | #B8E986 | #000000 | Notes and ideas |
| 🔗 (U+1F517) | Link | #50E3C2 | #000000 | Reference links |

**Properties:**
- `IsSystem = true` for all default symbols
- Cannot be edited or deleted
- Provide consistent baseline for all users

#### Custom Symbols

Users may create custom symbols:
1. Open Symbol Manager (Tools → Manage Symbols…)
2. Click "Add Custom Symbol…"
3. Enter Unicode character or select from character map
4. Set default colors
5. Provide meaningful name

**Validation Rules:**
- `SymbolChar` must be exactly 1 Unicode character
- `SymbolChar` must be unique (no duplicate symbols)
- `SymbolName` must be unique and between 3-50 characters
- Contrast ratio between `DefaultForeColor` and `DefaultBackColor` must be >= 4.5:1 (WCAG AA)

---

### Symbol Selection Logic

When editing or creating a Group or Category, the application provides intelligent symbol selection.

#### Preselection Algorithm

**For New Records:**
1. Query all symbols used by user's existing Groups or Categories
2. Filter SymbolConfiguration to exclude already-used symbols
3. If unused symbols exist: Select first unused symbol (ordered by `SymbolName`)
4. If all symbols are used: Select least recently used symbol

**For Existing Records:**
- Preselect current symbol
- Allow user to change to any available symbol

#### Symbol Selection Control

Custom ComboBox with owner-draw rendering:

**Item Template:**
```
[Symbol] SymbolName
[Color Rectangle] [ForeColor hex] on [BackColor hex]
```

**Example Rendering:**
```
⭐ Star
[█████] #FFFFFF on #F5A623
```

**Interaction:**
- Drop-down displays all available symbols
- Clicking an item selects that symbol
- Selected symbol is highlighted with system selection color

---

### Color Customization

When assigning a symbol to a Group or Category, users may override the default colors.

#### Color Picker Dialog

Appears when user clicks "Customize Colors…" button in edit dialog.

**Layout:**
- Symbol preview (large, 48px character)
- Foreground Color picker (button + color swatch)
- Background Color picker (button + color swatch)
- Contrast Ratio indicator (live calculation)
- "Reset to Defaults" button

**Contrast Checker:**
- Calculates WCAG contrast ratio in real-time
- Displays ratio (e.g., "7.2:1")
- Shows pass/fail status:
  - ✓ **Pass (AA):** Ratio >= 4.5:1 (green indicator)
  - ✗ **Fail:** Ratio < 4.5:1 (red indicator with warning)

**Warning Behavior:**
- If contrast ratio fails WCAG AA, display warning message:
  > "Contrast ratio is below recommended minimum (4.5:1). This may make the symbol difficult to read for users with visual impairments. Continue anyway?"
- Warning is non-blocking; user may proceed with low-contrast colors
- Recommendation: Adjust colors until warning clears

#### Color Persistence

Color overrides are stored per Group/Category:
- If user customizes colors: Store in `BackColor` and `ForeColor` fields
- If user does not customize: Fields may be NULL, falling back to symbol defaults
- Color changes do not affect the SymbolConfiguration table (only local overrides)

---

### Rendering Controls

Custom lightweight renderers ensure consistent symbol presentation across all controls.

#### ComboBox Renderer

**Purpose:** Display symbols in project/task selection ComboBoxes

**Rendering Logic:**
```csharp
void OnDrawItem(DrawItemEventArgs e)
{
    Symbol symbol = GetItemSymbol(e.Index);
    string displayText = GetItemDisplayText(e.Index);
    
    // Draw background
    e.DrawBackground();
    
    // Draw symbol with colors
    using (Font symbolFont = new Font("Segoe Fluent Icons", 14))
    using (SolidBrush symbolBrush = new SolidBrush(symbol.ForeColor))
    using (SolidBrush backBrush = new SolidBrush(symbol.BackColor))
    {
        Rectangle symbolRect = new Rectangle(e.Bounds.Left + 4, e.Bounds.Top + 2, 24, 24);
        e.Graphics.FillRectangle(backBrush, symbolRect);
        e.Graphics.DrawString(symbol.Char, symbolFont, symbolBrush, symbolRect);
    }
    
    // Draw display text
    using (SolidBrush textBrush = new SolidBrush(e.ForeColor))
    {
        Rectangle textRect = new Rectangle(e.Bounds.Left + 32, e.Bounds.Top, e.Bounds.Width - 32, e.Bounds.Height);
        e.Graphics.DrawString(displayText, e.Font, textBrush, textRect);
    }
    
    e.DrawFocusRectangle();
}
```

**Accessibility:**
- `AccessibleName` includes symbol name and display text
- `AccessibleDescription` includes color information
- High-contrast mode: Render symbols in system foreground color

---

#### DataGridView Cell Renderer

**Purpose:** Display symbols in DataGridView columns (Groups, Tasks, TimeEntries)

**Column Configuration:**
```csharp
DataGridViewImageColumn symbolColumn = new DataGridViewImageColumn
{
    Name = "SymbolColumn",
    HeaderText = "Symbol",
    Width = 50,
    ImageLayout = DataGridViewImageCellLayout.Zoom,
    ReadOnly = true
};
```

**Cell Painting Event:**
```csharp
void OnCellPainting(DataGridViewCellPaintingEventArgs e)
{
    if (e.ColumnIndex == symbolColumnIndex && e.RowIndex >= 0)
    {
        e.Paint(e.CellBounds, DataGridViewPaintParts.Background);
        
        Symbol symbol = GetRowSymbol(e.RowIndex);
        
        using (Font symbolFont = new Font("Segoe Fluent Icons", 12))
        using (SolidBrush symbolBrush = new SolidBrush(symbol.ForeColor))
        using (SolidBrush backBrush = new SolidBrush(symbol.BackColor))
        {
            Rectangle symbolRect = GetCenteredSymbolRect(e.CellBounds);
            e.Graphics.FillRectangle(backBrush, symbolRect);
            e.Graphics.DrawString(symbol.Char, symbolFont, symbolBrush, symbolRect, StringFormat.GenericDefault);
        }
        
        e.Handled = true;
    }
}
```

**Cell Accessibility:**
- Cell's `AccessibleName` property includes symbol name
- Cell's `ToolTipText` includes full symbol information

---

#### TreeView Renderer

**Purpose:** Display symbols next to group and task nodes in TreeView

**Custom Draw Mode:**
```csharp
treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;

void OnDrawNode(DrawTreeNodeEventArgs e)
{
    Symbol symbol = GetNodeSymbol(e.Node);
    
    // Draw node background
    e.Graphics.FillRectangle(
        e.Node.IsSelected ? SystemBrushes.Highlight : Brushes.White, 
        e.Bounds);
    
    // Calculate symbol position
    int symbolX = e.Bounds.Left;
    int symbolY = e.Bounds.Top + (e.Bounds.Height - 20) / 2;
    Rectangle symbolRect = new Rectangle(symbolX, symbolY, 20, 20);
    
    // Draw symbol
    using (Font symbolFont = new Font("Segoe Fluent Icons", 11))
    using (SolidBrush symbolBrush = new SolidBrush(symbol.ForeColor))
    using (SolidBrush backBrush = new SolidBrush(symbol.BackColor))
    {
        e.Graphics.FillRectangle(backBrush, symbolRect);
        e.Graphics.DrawString(symbol.Char, symbolFont, symbolBrush, symbolRect);
    }
    
    // Draw node text
    int textX = symbolX + 24;
    Rectangle textRect = new Rectangle(textX, e.Bounds.Top, e.Bounds.Width - 24, e.Bounds.Height);
    using (SolidBrush textBrush = new SolidBrush(e.Node.IsSelected ? SystemColors.HighlightText : SystemColors.WindowText))
    {
        e.Graphics.DrawString(e.Node.Text, e.Node.TreeView.Font, textBrush, textRect);
    }
}
```

**Accessibility:**
- Node's `AccessibleName` includes symbol name + node text
- Node's `ToolTipText` includes full group/task information
- Symbol colors are ignored in high-contrast mode (symbols render in tree foreground color)

---

### Accessibility Considerations

#### High-Contrast Mode Detection

```csharp
bool isHighContrastMode = SystemInformation.HighContrast;

if (isHighContrastMode)
{
    // Override symbol colors with system colors
    symbolForeColor = SystemColors.WindowText;
    symbolBackColor = SystemColors.Window;
}
```

**High-Contrast Behavior:**
- Symbols render in system foreground color
- Background rectangles are omitted
- Symbol character becomes the only visual indicator
- Contrast warnings are suppressed (system handles contrast)

#### Screen Reader Support

All symbol renderers must expose:
- **AccessibleName:** Describes the symbol (e.g., "Calendar symbol", "Star symbol")
- **AccessibleDescription:** Describes context (e.g., "Group 'My Day' uses Calendar symbol")
- **AccessibleRole:** Appropriate role for control type

**Announcement Pattern:**
```
Group node: "Calendar. My Day. Group with 5 tasks."
Task node: "Checkmark. Implement login form. Task 80% complete."
```

---

### Symbol Management Dialog

Advanced users may access the Symbol Manager to view and customize symbols.

**Access:** Tools → Manage Symbols…

**Layout:**
- List of all symbols (system and custom)
- Details pane showing selected symbol's properties
- "Add Custom Symbol…" button
- "Edit" button (disabled for system symbols)
- "Delete" button (disabled for system symbols)

**Custom Symbol Creation:**
1. User clicks "Add Custom Symbol…"
2. Dialog appears:
   - Character Picker (Unicode selector or paste character)
   - Symbol Name field
   - Default Foreground Color picker
   - Default Background Color picker
   - Contrast Ratio indicator
3. User sets values and clicks OK
4. New symbol is saved to SymbolConfiguration table

**Usage Tracking:**
- Symbol Manager shows "Used by X groups, Y categories"
- Attempting to delete in-use symbol displays warning:
  > "This symbol is used by 3 groups and 2 categories. If you delete it, those items will revert to no symbol. Continue?"
- Deletion is soft delete (`DateDeleted` is set)

---

## Task and Time Tracking

### Overview

The time tracking system enables users to log work time against tasks with minimal friction while maintaining flexibility for manual adjustments. The system supports both active timer-based tracking and manual time entry.

**Design Goals:**
- **Speed:** Start/stop timer in one click (F9)
- **Accuracy:** Store times in UTC, display in user's time zone
- **Flexibility:** Edit any entry inline in DataGridView
- **Intelligence:** Smart defaults based on recent activity
- **Validation:** Errors detected but don't block entry; fixed on commit

---

### Main Screen Integration

Time tracking UI resides in the **lower-right panel** of the main SplitContainer.

**Layout Hierarchy:**
```
MainForm
└─ SplitContainer (vertical)
   ├─ Left Panel: TreeView (Tasks)
   └─ Right Panel: SplitContainer (horizontal)
      ├─ Upper Panel: Task Details
      └─ Lower Panel: GroupBox "Time Tracking"
         ├─ ToolStrip (timer controls + quick selectors)
         └─ DataGridView (time entries)
```

**GroupBox Properties:**
- Title: "Time Tracking"
- Dock: Fill
- Padding: 8px all sides
- Font: 11pt Segoe UI (bold for title)

---

### ToolStrip Controls

The ToolStrip contains timer controls and quick selectors for rapid time entry.

#### Timer Control Section

| Button | Icon | Label | Shortcut | Function |
|--------|------|-------|----------|----------|
| Start/Stop | ▶/⏸ | "Start" or "Stop" | F9 | Toggles active timer |
| Add Entry | ➕ | "Add Entry…" | Ctrl+N | Opens manual time entry dialog |

**Timer Behavior:**
- **Start State (Default):**
  - Button shows play icon (▶) and "Start" label
  - Click starts timer for currently selected task
  - Button changes to stop state
- **Stop State (Timer Running):**
  - Button shows pause icon (⏸) and "Stop" label
  - Label shows current duration (e.g., "Stop (00:15:23)")
  - Click stops timer, creates TimeEntry, refreshes grid
- **No Task Selected:**
  - Start/Stop button is disabled
  - Tooltip: "Select a task to start tracking time"

**Timer Display:**
- Updates every second while running
- Format: "HH:MM:SS"
- Prominently displayed next to Start/Stop button

#### Quick Selector Section

| Control | Type | Purpose | Behavior |
|---------|------|---------|----------|
| Project | ComboBox | Select project | Filtered to user's active projects |
| Task | ComboBox | Select task | Auto-filtered based on selected project |

**Interaction Flow:**
1. User selects project from dropdown
2. Task dropdown automatically populates with project's tasks
3. Selecting task updates "Current Task" label
4. Start/Stop button uses selected project/task

**Smart Preselection:**
- On form load: Preselect project/task from last time entry
- On TreeView selection: Sync project/task with selected tree node
- On project change: If current task belongs to new project, keep it; otherwise, select first task

#### Action Buttons Section

| Button | Icon | Label | Shortcut | Function |
|--------|------|-------|----------|----------|
| Split | ✂ | "Split" | Ctrl+S | Splits selected entry at specified time |
| Duplicate | ⧉ | "Duplicate" | Ctrl+D | Duplicates selected entry with new times |
| Delete | 🗑 | "Delete" | Del | Soft-deletes selected entry (with confirmation) |
| Undo | ↶ | "Undo" | Ctrl+Z | Reverts last change (limited undo stack) |

**Button States:**
- Split, Duplicate, Delete: Enabled only when an entry is selected
- Undo: Enabled only when undo stack is not empty

---

### DataGridView Configuration

The DataGridView displays all time entries for the selected task (or all tasks if no task is selected).

#### Column Specification

| Column | Header | Field | Type | Width | Editable | Format |
|--------|--------|-------|------|-------|----------|--------|
| 0 | Start | StartTime | DateTime | 140px | Yes | "ddd, MMM dd, yyyy HH:mm" |
| 1 | End | EndTime | DateTime | 140px | Yes | "ddd, MMM dd, yyyy HH:mm" |
| 2 | Duration | Duration | TimeSpan | 80px | No | "H:mm:ss" |
| 3 | Project | IDProject | ComboBox | 150px | Yes | Project.ProjectName |
| 4 | Task | IDTodoTask | ComboBox | 150px | Yes | Task.DisplayName |
| 5 | Description | DescriptionDoneWork | Text | 300px | Yes | Plain text |
| 6 | Time Zone | TimeZone | Text | 100px | No | IANA identifier |
| 7 | Created | DateCreated | DateTime | 140px | No | "ddd, MMM dd, yyyy HH:mm" |

**Column Configuration Details:**

**Start and End Columns:**
- Type: `DataGridViewTextBoxColumn` with custom DateTime editor
- Parsing: Accepts multiple date/time formats:
  - "10/22/2025 14:30"
  - "Oct 22 2:30 PM"
  - "2:30 PM" (assumes today's date)
  - "14:30" (assumes today's date, 24-hour format)
- Validation: Deferred until commit; invalid entries highlighted in red

**Duration Column:**
- Type: `DataGridViewTextBoxColumn` (read-only)
- Auto-calculated on row leave event
- Format: Hours:minutes:seconds (e.g., "1:23:45" for 1 hour 23 minutes 45 seconds)
- Background: Light gray to indicate read-only

**Project and Task Columns:**
- Type: `DataGridViewComboBoxColumn`
- DisplayMember: Entity's display name property
- ValueMember: Entity's ID property
- DataSource: Dynamically filtered collections
  - Project: All user's active projects
  - Task: Tasks belonging to selected project
- Behavior: Task dropdown updates when project changes

**Description Column:**
- Type: `DataGridViewTextBoxColumn`
- Multiline: False (single-line entry)
- MaxLength: 1000 characters
- Autocomplete: Context menu with last 10 used descriptions

**Time Zone Column:**
- Type: `DataGridViewTextBoxColumn` (read-only)
- Auto-populated from system time zone
- Background: Light gray to indicate read-only

**Created Column:**
- Type: `DataGridViewTextBoxColumn` (read-only)
- Shows when entry was originally created (for audit purposes)
- Background: Light gray to indicate read-only

#### Grid Behavior

**Row Selection:**
- Single-row selection mode
- Clicking any cell selects the entire row
- Selected row is highlighted with system highlight color

**Inline Editing:**
- Double-click cell to enter edit mode
- Edit mode indicated by cell border change
- Press Enter to commit, Esc to cancel
- Moving to another cell commits current cell

**Auto-Calculation:**
- When Start or End is edited, Duration recalculates on cell exit
- When Project is changed, Task dropdown refreshes
- When Duration exceeds 24 hours, row is highlighted (warning, not error)

**Sorting:**
- Click column header to sort
- Default sort: Descending by StartTime (most recent first)
- Sort indicator (▲/▼) appears in header

**Context Menu (Right-Click):**
- **Copy**: Copies selected cell value to clipboard
- **Paste**: Pastes clipboard text into selected cell (if editable)
- **Split Entry…**: Opens split dialog
- **Duplicate Entry…**: Opens duplicate dialog
- **Delete Entry**: Soft-deletes entry (with confirmation)
- **---** (separator)
- **Last Used Descriptions**: Submenu with 10 most recent descriptions (click to apply to selected row)

---

### Smart Defaults

The application uses intelligent defaults to minimize user input.

#### Default Start Time

**Rule Set (in priority order):**
1. If entries exist for today:
   - Start = Last entry's EndTime (continuing from last session)
2. If no entries today, but entries exist in past 7 days:
   - Start = Today at same time as yesterday's first entry
3. If no recent entries:
   - Start = Current time, rounded to nearest 5-minute interval

#### Default End Time

**Rule Set:**
1. End = Start + Last entry's Duration
2. If no last entry, End = Start + 30 minutes

#### Default Duration

**Rule Set:**
1. Duration = Last entry's Duration
2. If no last entry, Duration = 30 minutes

#### Auto-Snap to Intervals

**User Preference (Tools → Options):**
- Enable/Disable: "Snap start/end times to 5-minute intervals"
- Default: Enabled

**Behavior When Enabled:**
- On timer stop: End time rounds to nearest 5 minutes
- On manual entry: Start and End round to nearest 5 minutes after blur
- Rounding logic:
  - 0-2 minutes: Round down
  - 3-7 minutes: Round to 5
  - 8-12 minutes: Round to 10
  - Etc.

#### Default Project and Task

**Rule Set:**
1. If task selected in TreeView: Use that task and its project
2. Else if last entry exists: Use its project and task
3. Else: Use Default project and first task in that project

---

### Manual Time Entry Dialog

Opened via **Add Entry…** button (Ctrl+N) or double-clicking a grid row.

**Dialog Layout:**

```
┌─ Add Time Entry ────────────────────────┐
│  Project:   [ComboBox              ▼]   │
│  Task:      [ComboBox              ▼]   │
│  Start:     [DateTimePicker            ] │
│  End:       [DateTimePicker            ] │
│  Duration:  [1:30:00       ] (read-only)│
│                                          │
│  Description:                            │
│  [Multi-line TextBox                  ]  │
│  [                                    ]  │
│  [                                    ]  │
│                                          │
│               [OK]  [Cancel]             │
└──────────────────────────────────────────┘
```

**Field Details:**

- **Project:** ComboBox, required, populated from user's projects
- **Task:** ComboBox, required, filtered by selected project
- **Start:** DateTimePicker, custom format "ddd, MMM dd, yyyy hh:mm tt"
- **End:** DateTimePicker, custom format "ddd, MMM dd, yyyy hh:mm tt"
- **Duration:** Read-only TextBox, auto-calculated
- **Description:** Multi-line TextBox, optional, 5 rows

**Smart Behavior:**

- On project change: Update task dropdown
- On start/end change: Recalculate duration
- Description: Autocomplete from recent entries (press Ctrl+Space to show)

**Validation (on OK):**

- Start must be <= End
- Duration must be > 0
- Project and Task must be selected
- If overlaps detected with existing entries: Display warning (non-blocking)

**Overlap Warning:**
> "This entry overlaps with an existing entry from [Start] to [End]. Continue anyway?"

User may proceed or cancel to adjust times.

---

### Validation Rules

Validation is **deferred until commit** to avoid interrupting user flow.

#### Field-Level Validation

**StartTime:**
- Must be valid DateTime
- Warning (not error) if > 7 days in past
- Warning (not error) if in future

**EndTime:**
- Must be valid DateTime
- Must be >= StartTime
- Warning (not error) if Duration > 24 hours

**Duration:**
- Must be > 0 (enforced by StartTime/EndTime validation)

**Project:**
- Must not be NULL
- Must reference a valid, non-deleted project

**Task:**
- Must not be NULL
- Must reference a valid, non-deleted task
- Must belong to selected project (enforced at UI level)

**Description:**
- Optional
- Max 1000 characters

#### Row-Level Validation

**Overlap Detection:**
- Query for other entries by same user with overlapping times
- If found: Display warning icon in row header
- Click icon to show details of overlapping entries
- **Non-blocking:** User may save entry with overlaps

**Cross-Date Entry Warning:**
- If entry spans midnight (Start and End are on different dates): Display warning
- Warning text: "This entry spans multiple days. Consider splitting into separate entries for accurate daily totals."
- **Non-blocking:** User may proceed

#### Commit-Time Validation

**When User Clicks Save or Navigates Away:**
1. Collect all invalid rows (red background)
2. If any invalid: Display MessageBox listing errors
3. Focus first invalid row
4. Remain in edit mode

**Error Message Format:**
```
The following errors must be corrected before saving:

Row 3: End time must be after start time
Row 5: Project is required
Row 7: Task is required

Please correct these errors and try again.
```

---

### Totals and Status Display

#### StatusStrip (Bottom of Main Form)

Displays aggregated time data and system status.

**Layout (left to right):**
```
[Database: mydatabase.legatro] | [Today: 5:23:45] | [Week: 23:15:30] | [Session: 1:45:20] | [2025-10-22 14:32:15]
```

**Field Specifications:**

| Field | Label | Source | Update Frequency |
|-------|-------|--------|------------------|
| Database | "Database: [filename]" | Current database file name | On file open |
| Today | "Today: [duration]" | Sum of today's entries | On entry create/update/delete |
| Week | "Week: [duration]" | Sum of this week's entries | On entry create/update/delete |
| Session | "Session: [duration]" | Time since app start | Every second |
| DateTime | Current date/time | System clock | Every second |

**Color Coding:**
- Normal: Black text on default background
- Exception: Red text on yellow background (if any errors occurred)

**Exception Display:**
- If database operation fails, StatusStrip shows error in red
- Error persists until user acknowledges (click to clear)
- Example: "Error: Unable to save changes. Database is read-only."

#### Grid Footer (Below DataGridView)

Displays sum of visible rows.

**Format:**
```
Total: 12 entries | 8:45:30 hours
```

**Behavior:**
- Recalculates when:
  - Grid is filtered
  - Rows are added/edited/deleted
  - Grid is sorted (total unchanged, but updates to confirm calculation)
- Displays sum of Duration column for all visible (non-filtered) rows

---

### Storage and Sync

#### Automatic Calculations

When a TimeEntry is created, updated, or deleted:

**Task Update:**
```csharp
void RecalculateTaskTimeSpent(Guid taskId)
{
    Task task = _context.Tasks.Find(taskId);
    TimeSpan totalTime = _context.TimeEntries
        .Where(e => e.IDTodoTask == taskId && e.DateDeleted == null)
        .Sum(e => e.Duration);
    
    task.TimeSpent = totalTime;
    task.DateLastEdited = DateTime.UtcNow;
    task.SyncGuidChanged = Guid.NewGuid();
}
```

**Project Update:**
```csharp
void RecalculateProjectTimeSpent(Guid projectId)
{
    Project project = _context.Projects.Find(projectId);
    TimeSpan totalTime = _context.TimeEntries
        .Where(e => e.IDProject == projectId && e.DateDeleted == null)
        .Sum(e => e.Duration);
    
    project.TimeSpent = totalTime;
    project.DateLastEdited = DateTime.UtcNow;
    project.SyncGuidChanged = Guid.NewGuid();
}
```

**Trigger Points:**
- After TimeEntry inserted
- After TimeEntry updated (if Duration changed)
- After TimeEntry soft-deleted
- After TimeEntry restored (DateDeleted set back to NULL)

#### Time Zone Handling

**Storage:**
- `StartTime` and `EndTime` are stored in UTC
- `TimeZone` stores the IANA time zone identifier (e.g., "America/Los_Angeles")

**Display:**
- Convert UTC times to user's current time zone for display
- Use `TimeZoneInfo.ConvertTimeFromUtc()` for conversion
- Display format: "ddd, MMM dd, yyyy hh:mm tt" (e.g., "Mon, Oct 22, 2025 02:30 PM")

**Benefits:**
- Accurate time tracking across time zone changes (e.g., travel)
- Sync-friendly (all systems use UTC internally)
- Historical accuracy (can reconstruct original local time)

**Example:**
- User in PST creates entry: Start = "2025-10-22 14:30 PST"
- Stored as: StartTime = "2025-10-22T21:30:00Z", TimeZone = "America/Los_Angeles"
- User travels to EST: Same entry displays as "2025-10-22 04:30 PM EST"

---

### Keyboard Shortcuts Summary

| Action | Shortcut | Context |
|--------|----------|---------|
| Start/Stop Timer | F9 | Main form |
| Add Entry | Ctrl+N | Main form |
| Split Entry | Ctrl+S | DataGridView row selected |
| Duplicate Entry | Ctrl+D | DataGridView row selected |
| Delete Entry | Del | DataGridView row selected |
| Undo | Ctrl+Z | Main form |
| Save All | Ctrl+S | Main form (when edits pending) |
| Cancel Edits | Esc | Main form (when edits pending) |
| Show Description History | Ctrl+Space | Description field focused |

---

## User Interface Specification

### Menu Structure

The main form includes a MenuStrip with four top-level menus.

#### File Menu

| Menu Item | Shortcut | Action |
|-----------|----------|--------|
| **New Solution…** | Ctrl+N | Creates new SQLite database |
| **Open Solution…** | Ctrl+O | Opens existing database file |
| **Close Solution** | - | Closes current database |
| --- | | (separator) |
| **Backup Now** | - | Creates immediate backup |
| --- | | (separator) |
| **Exit** | Alt+F4 | Closes application |

**New Solution Behavior:**
1. Display Save File Dialog with .legatro filter
2. Create new SQLite database at selected path
3. Initialize with schema (run migrations)
4. Create default system data (projects, groups, categories)
5. Create user record from Windows authentication
6. Store database path in settings
7. Load new database into UI

**Open Solution Behavior:**
1. Display Open File Dialog with .legatro filter
2. Validate selected file is valid SQLite database
3. Run migrations if schema version is outdated
4. Verify current Windows user exists in Users table; create if not
5. Store database path in settings
6. Load database into UI

**Close Solution Behavior:**
1. Check for unsaved changes; prompt to save if any
2. Create backup if auto-backup enabled
3. Close database connection
4. Clear all UI controls
5. Display "No database open" message

**Exit Behavior:**
1. Trigger Close Solution logic
2. Save window position and splitter positions to settings
3. Dispose all resources
4. Exit application

---

#### Edit Menu

| Menu Item | Shortcut | Action |
|-----------|----------|--------|
| **Groups…** | - | Opens Groups list dialog |
| **Projects…** | - | Opens Projects list dialog |
| **Categories…** | - | Opens Categories list dialog |

All menu items open modal dialogs as specified in [Base Data Management](#base-data-management).

---

#### View Menu

| Menu Item | Shortcut | Checked | Action |
|-----------|----------|---------|--------|
| **Show Completed Tasks** | - | Default: Yes | Toggles visibility of completed tasks in TreeView |
| **Show Hidden Groups** | - | Default: No | Toggles visibility of groups with IsHidden=true |
| --- | | | (separator) |
| **Expand All Groups** | Ctrl+E | - | Expands all group nodes in TreeView |
| **Collapse All Groups** | Ctrl+W | - | Collapses all group nodes in TreeView |
| --- | | | (separator) |
| **Refresh** | F5 | - | Reloads all data from database |

**Show Completed Tasks:**
- When checked: Tasks with PercentDone=100 are visible
- When unchecked: Completed tasks are hidden but remain in database

**Show Hidden Groups:**
- When checked: Groups with IsHidden=true appear in TreeView
- When unchecked: Hidden groups are not displayed

---

#### Tools Menu

| Menu Item | Shortcut | Action |
|-----------|----------|--------|
| **Manage Symbols…** | - | Opens Symbol Manager dialog |
| **Options…** | - | Opens Options dialog |

**Manage Symbols Dialog:**
- Described in [Symbol and Color Management](#symbol-and-color-management)

**Options Dialog:**
- Described in [Tools/Options Dialog](#toolsoptions-dialog)

---

### Main Form Layout

#### General Layout Guidelines

**Spacing:**
- All SplitContainers: Minimum 3px distance to splitter bars
- Panel padding: 8px on all sides
- Control spacing: 6px vertical, 4px horizontal

**Fonts:**
- Base font: 11pt Segoe UI
- Group headers: 11pt Segoe UI Bold
- Selected task name: 14pt Segoe UI (in task details panel)
- TreeView nodes: 11pt Segoe UI
- StatusStrip: 10pt Segoe UI

#### Main Container Structure

```
MainForm (1024x768 default)
│
├─ MenuStrip
│  ├─ File
│  ├─ Edit
│  ├─ View
│  └─ Tools
│
├─ Main SplitContainer (Vertical, 25% / 75% default)
│  │
│  ├─ Left Panel: TreeView Container
│  │  └─ TreeView (_tvwTasks)
│  │
│  └─ Right Panel: SplitContainer (Horizontal, 60% / 40% default)
│     │
│     ├─ Upper Panel: Task Details Container
│     │  ├─ Selected Group Label (large font)
│     │  ├─ GroupBox: "Task Details"
│     │  │  ├─ TableLayoutPanel
│     │  │  │  ├─ Task Display Name (TextBox, editable)
│     │  │  │  ├─ Task Description (TextBox, multiline, editable)
│     │  │  │  ├─ Groups (CheckedListBox, owner-drawn)
│     │  │  │  ├─ Project (ComboBox, editable)
│     │  │  │  └─ Date Row (Nested TableLayoutPanel)
│     │  │  │     ├─ Date Created (Label, info only)
│     │  │  │     ├─ Due Date (DateTimePicker, editable)
│     │  │  │     ├─ Estimated Effort (TextBox, editable)
│     │  │  │     └─ Percent Done (Label, calculated)
│     │  │  └─ ToolStrip (Apply / Cancel buttons, right-aligned)
│     │  │
│     │  └─ GroupBox: "New Task"
│     │     ├─ Label: "Quick Add:"
│     │     ├─ TextBox (multiline, 2 rows)
│     │     └─ Button: "Add" (enabled when TextBox is dirty)
│     │
│     └─ Lower Panel: Time Tracking Container
│        └─ GroupBox: "Time Tracking"
│           ├─ ToolStrip (timer controls + action buttons)
│           └─ DataGridView (time entries)
│
└─ StatusStrip
   ├─ Database Label (spring=false)
   ├─ Separator
   ├─ Today Total (spring=false)
   ├─ Separator
   ├─ Week Total (spring=false)
   ├─ Separator
   ├─ Status Label (spring=true, red on error)
   ├─ Separator
   ├─ Current Date Label (spring=false)
   └─ Current Time Label (spring=false)
```

---

### TreeView (_tvwTasks)

Displays hierarchical view of groups and their assigned tasks.

#### Node Structure

```
📦 All Groups (root node, not editable)
│
├─ 📅 My Day (group node)
│  ├─ ✓ Task 1 (task node)
│  ├─ ✓ Task 2 (task node)
│  └─ ✓ Task 3 (task node)
│
├─ ⭐ Important (group node)
│  ├─ ✓ Task 4 (task node)
│  └─ ✓ Task 5 (task node)
│
└─ 🗑️ Clutter (group node)
   └─ ✓ Task 6 (task node)
```

#### Node Properties

**Root Node:**
- Text: "All Groups"
- Icon: Folder icon (static)
- Expanded by default
- Not selectable (no highlighting on click)

**Group Nodes:**
- Text: Group.GroupDisplayName
- Icon: Rendered symbol from Group.IDSymbol with colors
- Selectable: Yes
- Expanded: Persisted per group in settings
- Font: 11pt Segoe UI
- On click: Updates task details panel to show group information

**Task Nodes:**
- Text: Task.DisplayName
- Icon: Checkmark if PercentDone=100, empty otherwise
- Selectable: Yes
- Font: 11pt Segoe UI
- Strikethrough: Applied if PercentDone=100
- On click: Loads task details into right panel
- Color: Grayed out if PercentDone=100 (optional preference)

#### Filtering and Sorting

**Filtering:**
- Controlled by View menu options (Show Completed Tasks, Show Hidden Groups)
- Applied dynamically on menu item change
- Tree refreshes to show/hide nodes

**Sorting:**
- Groups: Always sorted by OrderNo (ascending), then GroupDisplayName (alphabetical)
- Tasks: Sorted by user preference (set in Tools → Options):
  - By Display Name (alphabetical)
  - By Due Date (ascending, NULL last)
  - By Percent Done (ascending, incomplete first)
  - By Estimated Effort (descending, largest first)
  - By Date Created (descending, newest first)

**Default Sort:** By Due Date

---

### Task Details Panel

Displays and allows editing of the selected task.

#### Selected Group Label

**Position:** Top of panel, above GroupBox  
**Font:** 14pt Segoe UI Bold  
**Text:** Selected group's GroupDisplayName  
**Behavior:** Updates when group node is clicked in TreeView

#### GroupBox: "Task Details"

**Visibility:** Shown only when a task node is selected

##### Field Layout (TableLayoutPanel)

**Column Configuration:**
- Column 1 (Labels): 120px fixed width, right-aligned
- Column 2 (Controls): Auto-width, fills remaining space

**Row Configuration:**
```
Row 0: Task Display Name [TextBox, single-line, editable]
Row 1: Task Description [TextBox, multiline, 5 rows, editable]
Row 2: Groups [CheckedListBox, 3 rows visible, scrollable] | Project [ComboBox, editable]
Row 3: Date Row (nested TableLayoutPanel, 4 columns)
```

##### Field Specifications

**Task Display Name:**
- Control: TextBox
- MaxLength: 255
- On change: Marks task as dirty, enables Apply/Cancel buttons
- Validation: Cannot be empty

**Task Description:**
- Control: TextBox
- Multiline: True
- ScrollBars: Vertical
- MaxLength: 4000
- On change: Marks task as dirty

**Groups:**
- Control: CheckedListBox
- ItemHeight: 24px (allows for symbol + text)
- CheckOnClick: True
- Items: All user's groups (except hidden groups if preference is set)
- Item Format: [Symbol] GroupDisplayName
- Item Rendering: Owner-drawn with symbol and colors
- On check/uncheck: Adds/removes TodoTasksGroupsRelations record
- Note: Items can be checked or unchecked; multiple selections allowed

**Project:**
- Control: ComboBox
- DropDownStyle: DropDownList (no manual entry)
- Items: All user's projects (active only, IsSystem=true projects included)
- DisplayMember: ProjectName
- ValueMember: IDProject
- On change: Updates Task.IDProject, marks task as dirty

**Date Row (Nested TableLayoutPanel):**

| Label | Control | Width | Format/Behavior |
|-------|---------|-------|-----------------|
| Date Created: | Label (read-only) | 150px | "ddd, MMM dd, yyyy" (local time) |
| Due Date: | DateTimePicker | 200px | Custom format, editable, can be NULL |
| Estimated Effort: | TextBox | 100px | TimeSpan format "HH:MM", editable |
| Percent Done: | Label (calculated) | 80px | "X%" format, read-only |

**Due Date:**
- Includes checkbox to enable/disable
- When unchecked: DueDate is NULL
- When checked: DateTimePicker is enabled

**Estimated Effort:**
- MaskedTextBox with mask "00:00"
- Accepts hours and minutes (e.g., "03:30" for 3 hours 30 minutes)
- On change: Recalculates Percent Done

**Percent Done:**
- Calculated as: (TimeSpent / EstimatedEffort) * 100
- If EstimatedEffort is NULL or 0: Shows "N/A"
- Capped at 100%
- Color coding:
  - 0-33%: Red
  - 34-66%: Orange
  - 67-99%: Yellow
  - 100%: Green

##### ToolStrip (Apply / Cancel)

**Position:** Bottom of GroupBox, right-aligned  
**Visibility:** Hidden by default; shown when task becomes dirty

**Buttons:**
- **Apply Changes** (✓ icon): Commits changes to database, hides buttons
- **Cancel** (✗ icon): Reverts changes, reloads task data, hides buttons

**Keyboard Shortcuts:**
- Apply: Ctrl+Enter (when focus is in task details)
- Cancel: Esc (when focus is in task details)

**Modal Editing Behavior:**
- When task becomes dirty, all other controls in main form are disabled except:
  - Task detail fields
  - Apply/Cancel buttons
  - Menu (File → Exit only)
- Attempting to navigate away (e.g., clicking TreeView) displays warning:
  > "You have unsaved changes. Apply or cancel before navigating away."

---

#### GroupBox: "New Task"

**Purpose:** Quick entry of new tasks without opening dialogs

**Position:** Below Task Details GroupBox  
**Height:** Auto-size based on content (approximately 100px)

**Layout:**
```
┌─ New Task ─────────────────────────────────────┐
│ Quick Add:                                     │
│ [Multi-line TextBox (2 rows)                 ] │
│ [                                            ] │
│                                   [Add Button] │
└────────────────────────────────────────────────┘
```

**Field Specifications:**

**Quick Add TextBox:**
- Control: TextBox
- Multiline: True
- Rows: 2
- ScrollBars: Vertical
- MaxLength: 255
- Placeholder: "Type task name and press Ctrl+Enter or click Add..."
- On change: Enables Add button

**Add Button:**
- Enabled: Only when TextBox contains text
- Shortcut: Ctrl+Enter (when TextBox is focused)
- Action:
  1. Create new Task with DisplayName from TextBox
  2. Set IDProject to currently selected project (from TreeView or last used)
  3. Set IDUser to current user
  4. Add to currently selected group (if any)
  5. Set DateCreated to now
  6. Save to database
  7. Add to TreeView under appropriate group
  8. Clear TextBox
  9. Select newly created task in TreeView

---

### Dark Mode Support

#### Detection

```csharp
bool isDarkMode = Microsoft.Win32.Registry.GetValue(
    @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
    "AppsUseLightTheme",
    1) is int value && value == 0;
```

#### Color Adjustments

**DataGridView:**
- Light Mode:
  - Background: White (#FFFFFF)
  - Foreground: Black (#000000)
  - Alternate Row: Light Gray (#F5F5F5)
  - Selection Back: System Highlight
  - Selection Fore: System Highlight Text
  - Grid Lines: Light Gray (#E0E0E0)
- Dark Mode:
  - Background: Dark Gray (#1E1E1E)
  - Foreground: Light Gray (#E0E0E0)
  - Alternate Row: Slightly Lighter Gray (#252526)
  - Selection Back: Dark Blue (#094771)
  - Selection Fore: White (#FFFFFF)
  - Grid Lines: Medium Gray (#3E3E42)

**TreeView:**
- Light Mode:
  - Background: White (#FFFFFF)
  - Foreground: Black (#000000)
  - Line Color: Light Gray (#CCCCCC)
- Dark Mode:
  - Background: Dark Gray (#1E1E1E)
  - Foreground: Light Gray (#E0E0E0)
  - Line Color: Medium Gray (#3E3E42)

**GroupBoxes and Panels:**
- Light Mode:
  - Background: Control (#F0F0F0)
  - Foreground: Black (#000000)
- Dark Mode:
  - Background: Dark Gray (#1E1E1E)
  - Foreground: Light Gray (#E0E0E0)

**Buttons and ToolStrips:**
- Use system colors for automatic dark mode adaptation
- ButtonFace, ButtonText, ControlDark, ControlLight

#### Symbol Rendering in Dark Mode

- Symbol colors remain as configured by user
- If symbol's BackColor is light and app is in dark mode: Add subtle border for visibility
- In high-contrast mode: Ignore custom colors, use system colors

---

### Window Position Persistence

#### Saved Settings

Settings are stored in `%LOCALAPPDATA%\LegatroExp\settings.json`:

```json
{
  "window": {
    "width": 1024,
    "height": 768,
    "left": 100,
    "top": 100,
    "maximized": false
  },
  "splitters": {
    "mainVertical": 0.25,
    "mainHorizontal": 0.60
  },
  "treeView": {
    "expandedGroups": [
      "550e8400-e29b-41d4-a716-446655440000",
      "6ba7b810-9dad-11d1-80b4-00c04fd430c8"
    ]
  },
  "lastDatabase": "C:\\Users\\Klaus\\Documents\\LegatroExp\\mywork.legatro",
  "preferences": {
    "taskSortOrder": "DueDate",
    "showCompletedTasks": true,
    "showHiddenGroups": false,
    "autoBackupOnExit": true,
    "snapToIntervals": true,
    "baseFont": "Segoe UI",
    "uiLanguage": "en"
  }
}
```

#### Persistence Timing

**On Application Exit:**
1. Save current window size and position (unless maximized)
2. Save splitter positions as percentages (e.g., 0.25 for 25%)
3. Save TreeView expansion state (list of expanded group IDs)
4. Save user preferences

**On Application Start:**
1. Load settings file
2. Validate window position is on visible screen (multi-monitor support)
3. Apply window size and position
4. Apply splitter positions (percentages scale to window size)
5. Expand TreeView groups based on saved state
6. Apply user preferences

#### Multi-Monitor Handling

```csharp
bool IsPositionOnAnyScreen(int left, int top)
{
    return Screen.AllScreens.Any(screen => 
        screen.WorkingArea.Contains(left, top));
}

if (!IsPositionOnAnyScreen(savedLeft, savedTop))
{
    // Reset to primary screen, centered
    mainForm.StartPosition = FormStartPosition.CenterScreen;
}
```

---

## General Requirements

### Modal Dialog Positioning

**Rule:** All modal dialogs must be centered on their parent window.

**Implementation:**
```csharp
public void ShowCenteredDialog(Form parentForm, Form dialogForm)
{
    dialogForm.StartPosition = FormStartPosition.Manual;
    
    int x = parentForm.Left + (parentForm.Width - dialogForm.Width) / 2;
    int y = parentForm.Top + (parentForm.Height - dialogForm.Height) / 2;
    
    dialogForm.Location = new Point(x, y);
    dialogForm.ShowDialog(parentForm);
}
```

**Validation:**
- Ensure dialog is fully visible on screen
- If dialog extends beyond screen bounds: Adjust position to fit

---

### Settings File Management

**Location:** `%LOCALAPPDATA%\LegatroExp\settings.json`

**Example Path:**
```
C:\Users\Klaus\AppData\Local\LegatroExp\settings.json
```

**File Format:** JSON with UTF-8 encoding

**Persistence:**
- Settings are saved on application exit (normal close only)
- Settings are not saved on crash or forced termination
- Settings are validated on load; invalid values use defaults

**Security:**
- File permissions: Current user only (no admin rights required)
- No sensitive data stored (database path is relative where possible)
- File is human-readable and editable

---

### Accessibility Requirements

#### Keyboard Navigation

**All controls must be keyboard-accessible:**
- Tab order follows logical reading order (left to right, top to bottom)
- Accelerator keys defined for all menu items and form labels
- Ctrl+Tab navigates between panels in SplitContainer
- F6 cycles between TreeView and task details

**Focus Indicators:**
- Yellow background on TextBox focus
- Dotted outline on Button focus
- Highlight on TreeView node focus

#### Screen Reader Support

**All controls must have:**
- Meaningful AccessibleName property
- Descriptive AccessibleDescription property
- Correct AccessibleRole property
- Logical tab order

**Announcements:**
- Form title is announced when dialog opens
- Field labels are announced when field receives focus
- Validation errors are announced via ErrorProvider
- TreeView nodes announce: Symbol name + Node text + Item count (for groups)

#### High-Contrast Mode

**Visual adjustments:**
- Use system colors (SystemColors class) for all UI elements
- Disable custom symbol colors; use SystemColors.WindowText instead
- Increase border width for focus indicators
- Ensure all text has sufficient contrast (automatic with system colors)

---

### Auto-Backup

**User Preference:** Tools → Options → "Create backup on exit"

**Backup Timing:**
- Triggered on normal application exit (File → Exit or window close)
- Not triggered on crash or forced termination

**Backup Process:**
1. Close all database connections
2. Copy database file to backup location
3. Backup filename: `[original_name].bak`
4. Backup location: Same folder as original database
5. Overwrite existing .bak file (keep only latest backup)

**Example:**
- Original: `C:\Users\Klaus\Documents\mywork.legatro`
- Backup: `C:\Users\Klaus\Documents\mywork.legatro.bak`

**User Notification:**
- Backup occurs silently (no progress dialog)
- If backup fails: Display error MessageBox but still exit application

---

### Multi-Language Support

**Supported Languages:**
- English (en)
- German (de)
- Spanish (es)
- Japanese (ja)
- Chinese Simplified (zh-CN)

**Language Selection:** Tools → Options → UI Language

**Resource Files:**
```
Resources/
├─ Strings.resx (English, default)
├─ Strings.de.resx (German)
├─ Strings.es.resx (Spanish)
├─ Strings.ja.resx (Japanese)
└─ Strings.zh-CN.resx (Chinese Simplified)
```

**Scope:**
- All UI text (menus, labels, buttons, tooltips)
- Validation error messages
- MessageBox prompts

**Date and Time Formatting:**
- Use user's Windows locale settings
- DateTimePicker controls respect locale
- TimeSpan displays in culture-neutral format (H:mm:ss)

**Restart Requirement:**
- Language changes require application restart to take effect
- Display prompt: "Language will change on next application start. Restart now?"

---

### Base Font Customization

**User Preference:** Tools → Options → Base Font

**Scope:**
- Applies to all UI text except:
  - Segoe Fluent Icons font (for symbols)
  - Fixed-width areas (if any)

**Available Fonts:**
- ComboBox populated from installed system fonts
- Default: 11pt Segoe UI

**Preview:**
- Options dialog includes sample text showing selected font
- Preview updates in real-time as user changes selection

**Persistence:**
- Saved in settings.json
- Applied on next application start (or immediately if user chooses)

---

### Exception Handling

**User-Facing Errors:**
- Display in MessageBox with clear, non-technical language
- Include "Details" button to show technical error (collapsible)
- Log to application log file for diagnostics

**StatusStrip Error Display:**
- Database connection errors
- Save failures
- Validation errors (summary)

**Application Log:**
- Location: `%LOCALAPPDATA%\LegatroExp\logs\`
- Filename: `legatro_[date].log` (e.g., `legatro_2025-10-22.log`)
- Format: `[timestamp] [level] [message]`
- Levels: DEBUG, INFO, WARNING, ERROR, FATAL

**Example Log Entry:**
```
[2025-10-22 14:32:15] ERROR: Failed to save TimeEntry. Database is locked.
System.Data.SQLite.SQLiteException: database is locked
   at System.Data.SQLite.SQLite3.Prepare(...)
   at LegatroExp.Services.TimeEntryService.Save(...)
```

---

## Tools/Options Dialog

Modal dialog for configuring application preferences.

### Layout

**Form Properties:**
- Title: "Options"
- Size: 600x500
- Resizable: No
- Start Position: CenterParent
- Modal: True

**Control Layout (TableLayoutPanel):**
```
┌─ Options ───────────────────────────────────────┐
│ Category List            Settings Panel         │
│ ┌──────────────┐        ┌─────────────────────┐ │
│ │ General      │◄──────►│ [Settings controls] │ │
│ │ TreeView     │        │                     │ │
│ │ Time Tracking│        │                     │ │
│ │ Appearance   │        │                     │ │
│ └──────────────┘        └─────────────────────┘ │
│                                                  │
│                            [OK]  [Cancel]  [Apply] │
└──────────────────────────────────────────────────┘
```

**Left Panel (Category List):**
- Control: ListBox
- Width: 150px
- Items: General, TreeView, Time Tracking, Appearance
- Selection Mode: One

**Right Panel (Settings):**
- Control: Panel with dynamic content based on selected category
- Width: Fills remaining space

---

### General Category

**Settings:**

**Default Sort Order for Tasks:**
- Control: ComboBox
- Items:
  - "Display Name (Alphabetical)"
  - "Due Date"
  - "Percent Done"
  - "Estimated Effort"
  - "Date Created"
- Default: "Due Date"

**Auto-Backup on Exit:**
- Control: CheckBox
- Label: "Create backup copy of database on exit"
- Default: Checked

**UI Language:**
- Control: ComboBox
- Items: English, German, Spanish, Japanese, Chinese (Simplified)
- Default: English
- Note: "Restart required for changes to take effect" (displayed below)

**Base Font:**
- Control: ComboBox (populated from system fonts)
- Default: Segoe UI
- Font Size: NumericUpDown, range 8-16, default 11

---

### TreeView Category

**Settings:**

**Show Completed Tasks:**
- Control: CheckBox
- Label: "Show tasks marked as 100% complete"
- Default: Checked

**Show Hidden Groups:**
- Control: CheckBox
- Label: "Show groups marked as hidden"
- Default: Unchecked

**Auto-Expand Groups:**
- Control: CheckBox
- Label: "Automatically expand groups when selecting them"
- Default: Checked

---

### Time Tracking Category

**Settings:**

**Snap Times to Intervals:**
- Control: CheckBox
- Label: "Round start/end times to nearest 5 minutes"
- Default: Checked

**Warn on Overlapping Entries:**
- Control: CheckBox
- Label: "Display warning when time entries overlap"
- Default: Checked

**Default Duration:**
- Control: ComboBox
- Items: 15 min, 30 min, 45 min, 1 hour, 2 hours, 4 hours, 8 hours
- Default: 30 min

---

### Appearance Category

**Settings:**

**Dark Mode:**
- Control: ComboBox
- Items: "Auto (follow system)", "Light", "Dark"
- Default: "Auto (follow system)"

**Symbol Size in TreeView:**
- Control: ComboBox
- Items: Small (16px), Medium (20px), Large (24px)
- Default: Medium

**Grid Row Height:**
- Control: NumericUpDown
- Range: 20-40
- Default: 28

---

### Button Bar

**Buttons:**
- **OK:** Applies settings and closes dialog
- **Cancel:** Discards changes and closes dialog
- **Apply:** Applies settings without closing dialog (remains enabled while unsaved changes exist)

**Validation:**
- Occurs on OK or Apply
- Invalid settings are highlighted with ErrorProvider
- Dialog remains open until all errors are corrected

---
