# LegatroLight GitHub Issues - Creation Summary

**Date:** November 3, 2025  
**Repository:** KlausLoeffelmannOrg/LegatroLight  
**Specification:** specs/LegatroLight_Complete_Specification.md

## Overview

Successfully created a comprehensive set of 8 epic issues that break down the entire LegatroLight specification into implementable units. Each epic includes detailed scope, child tasks, acceptance criteria, technical notes, and dependencies.

## Labels Created

### Type Labels
- **epic** - Umbrella issue for feature area (#3E4B9E)

### Area Labels
- **area:database** - Database schema and EF Core (#FFA500)
- **area:ui** - User interface and forms (#D4C5F9)
- **area:time-tracking** - Time tracking features (#F9D71C)
- **area:auth** - Authentication and user management (#FF6B6B)
- **area:accessibility** - Accessibility features (#50E3C2)
- **area:data-management** - Base data management dialogs (#B8E986)

### Priority Labels
- **p0-critical** - Critical priority (#FF0000)
- **p1-high** - High priority (#FF8C00)
- **p2-medium** - Medium priority (#FFD700)
- **p3-low** - Low priority (#90EE90)

### Complexity Labels
- **complexity:low** - Simple task, good for copilot (#90EE90)
- **complexity:medium** - Moderate complexity (#FFD700)
- **complexity:high** - Complex, needs human review (#FF8C00)

## Created Epics

### Epic #9: Database Schema and Infrastructure
**Priority:** p1-high  
**Labels:** epic, area:database, p1-high  
**Link:** https://github.com/KlausLoeffelmannOrg/WA_LegatroLight/issues/9

**Scope:**
- SQLite database with .legatro extension
- Entity Framework Core 10 integration
- All 8 core tables with common fields pattern
- GUID-based primary keys for sync scenarios
- Soft deletion support throughout
- Initial data seeding

**Key Child Tasks (11):**
1. DbContext and common entity base classes
2. Users table and Windows authentication integration
3. SymbolConfiguration table
4. Categories, Groups, Projects, Tasks tables
5. TasksGroupsRelations many-to-many table
6. TimeEntries table with time zone handling
7. Database migrations and seeding
8. Automatic TimeSpent calculations

**Dependencies:** None (foundation for all others)

---

### Epic #10: First Start Experience and Windows Authentication
**Priority:** p1-high  
**Labels:** epic, area:auth, area:ui, p1-high  
**Link:** https://github.com/KlausLoeffelmannOrg/WA_LegatroLight/issues/10

**Scope:**
- User Setup Assistant dialog
- Windows authentication (WindowsIdentity, WindowsPrincipal)
- Automatic user information retrieval
- Default system data creation on first start
- Automatic login on subsequent starts

**Key Child Tasks (8):**
1. Windows authentication service integration
2. User Setup Assistant dialog UI
3. Default project creation (IsSystem=true)
4. System groups initialization (My Day, Sliding Week, etc.)
5. System categories creation (10 colors with WCAG compliance)
6. System symbols seeding (Segoe Fluent Icons)
7. First-start detection logic

**Dependencies:** Epic #9 (Database Schema)

---

### Epic #11: Base Data Management Dialogs
**Priority:** p1-high  
**Labels:** epic, area:data-management, area:ui, p1-high  
**Link:** https://github.com/KlausLoeffelmannOrg/WA_LegatroLight/issues/11

**Scope:**
- Consistent list and edit dialogs for Projects, Groups, Categories
- DataGridView with sorting and filtering
- TableLayoutPanel-based edit dialogs
- Validation framework (commit-time)
- Dialog state persistence
- Full keyboard navigation

**Key Child Tasks (12):**
1. Base list dialog framework (reusable)
2. Base edit dialog framework with validation
3. Groups/Projects/Categories list and edit dialogs
4. Symbol selection control (custom ComboBox)
5. Color picker with WCAG contrast checker
6. ErrorProvider integration
7. Dialog state persistence service
8. Keyboard navigation and accessibility

**Dependencies:** Epic #9, Epic #10 (for system data)

---

### Epic #12: Symbol and Color Management System
**Priority:** p2-medium  
**Labels:** epic, area:ui, p2-medium  
**Link:** https://github.com/KlausLoeffelmannOrg/WA_LegatroLight/issues/12

**Scope:**
- Segoe Fluent Icons integration
- Custom symbol renderers (TreeView, DataGridView, ComboBox)
- Color customization with contrast validation
- Symbol Manager dialog
- High-contrast mode support

**Key Child Tasks (12):**
1. Segoe Fluent Icons font integration
2. ComboBox symbol renderer (owner-draw)
3. DataGridView cell symbol renderer
4. TreeView node symbol renderer
5. Color picker dialog with contrast checker
6. WCAG contrast ratio calculator
7. Symbol Manager dialog
8. Custom symbol creation
9. High-contrast mode adaptation
10. Symbol accessibility features

**Dependencies:** Epic #9 (SymbolConfiguration table), Epic #11 (integration)

---

### Epic #13: Task and Time Tracking System
**Priority:** p0-critical  
**Labels:** epic, area:time-tracking, area:ui, p0-critical  
**Link:** https://github.com/KlausLoeffelmannOrg/WA_LegatroLight/issues/13

**Scope:**
- Active timer with Start/Stop (F9)
- Manual time entry dialog
- Time entries DataGridView with inline editing
- Smart defaults based on recent activity
- UTC storage, local display
- Automatic TimeSpent calculations
- Validation and overlap detection

**Key Child Tasks (20):**
1. Timer service (start/stop, duration tracking)
2. Timer UI controls
3. Time entry DataGridView configuration
4. Inline editing for time entries
5. Manual time entry dialog
6. Smart defaults engine
7. Time zone handling (UTC ↔ local conversion)
8. Automatic TimeSpent rollup
9. Validation framework (deferred)
10. Overlap detection
11. Split and duplicate entry operations
12. Quick selectors (project/task filtering)
13. Totals calculation (Today/Week/Session)
14. StatusStrip time display
15. Context menu and keyboard shortcuts

**Dependencies:** Epic #9 (TimeEntries table), main form UI structure

---

### Epic #14: Main User Interface and Form Layout
**Priority:** p0-critical  
**Labels:** epic, area:ui, p0-critical  
**Link:** https://github.com/KlausLoeffelmannOrg/WA_LegatroLight/issues/14

**Scope:**
- Main form with MenuStrip (File, Edit, View, Tools)
- TreeView for hierarchical task display
- Task details panel with inline editing
- Quick task entry box
- StatusStrip with database info and totals
- Window/splitter position persistence
- Dark mode support

**Key Child Tasks (24):**
1. Main form skeleton with SplitContainers
2. MenuStrip implementation (all menus)
3. File menu operations (New/Open/Close/Backup/Exit)
4. Edit/View/Tools menus
5. TreeView configuration and data binding
6. TreeView node rendering with symbols
7. TreeView filtering and sorting
8. Task details panel layout
9. Task details inline editing with Apply/Cancel
10. Quick task entry ("New Task" with Ctrl+Enter)
11. Groups CheckedListBox (multi-assignment)
12. StatusStrip layout
13. Window/splitter position persistence
14. Dark mode detection and color adaptation
15. Keyboard navigation (F6, Ctrl+Tab)

**Dependencies:** Epic #9, integrates Epic #13 (time tracking panel), Epic #12 (symbol rendering)

---

### Epic #15: Accessibility and Localization Features
**Priority:** p2-medium  
**Labels:** epic, area:accessibility, p2-medium  
**Link:** https://github.com/KlausLoeffelmannOrg/WA_LegatroLight/issues/15

**Scope:**
- Full keyboard navigation
- Screen reader support (AccessibleName/Description)
- High-contrast mode support
- Focus indicators
- Multi-language support (EN, DE, ES, JA, ZH-CN)
- Resource files for all UI text
- Culture-aware date/time formatting

**Key Child Tasks (20):**
1. Keyboard navigation implementation
2. AccessibleName/Description for all controls
3. AccessibleRole properties
4. Focus indicators (yellow background)
5. High-contrast mode detection and adaptation
6. Screen reader announcements
7. Resource file structure (Strings.resx)
8. 5 language resource files (EN, DE, ES, JA, ZH-CN)
9. Language selection in Options
10. Culture-aware formatting
11. Accelerator keys throughout
12. WCAG compliance testing

**Dependencies:** Applies to all UI epics (should be implemented alongside)

---

### Epic #16: Settings and Configuration Management
**Priority:** p3-low  
**Labels:** epic, area:ui, p3-low  
**Link:** https://github.com/KlausLoeffelmannOrg/WA_LegatroLight/issues/16

**Scope:**
- Tools → Options dialog with categorized settings
- Settings persistence (settings.json)
- Auto-backup on exit
- Application logging
- Exception handling
- Base font customization
- User preferences for sorting, display, time tracking

**Key Child Tasks (19):**
1. Settings service (read/write JSON)
2. Settings.json schema and defaults
3. Options dialog framework
4. General/TreeView/Time Tracking/Appearance settings panels
5. Font preview
6. Apply button (non-closing save)
7. Auto-backup service (.bak files)
8. Application logging service (daily files)
9. Log file rotation
10. Exception handler with user-friendly messages
11. StatusStrip error display
12. Settings validation and migration

**Dependencies:** Settings used by all epics

---

## Implementation Strategy

### Phase 1: Foundation (Critical Path)
**Weeks 1-2:**
1. **Epic #9: Database Schema** - Complete all tables, migrations, seeding
2. **Epic #10: First Start Experience** - Windows auth, user setup, system data

### Phase 2: Core UI (Critical Path)
**Weeks 3-4:**
3. **Epic #14: Main User Interface** - Main form, TreeView, basic layout
4. **Epic #16: Settings Management** - Early for testing configuration

### Phase 3: Primary Features (Critical Path)
**Weeks 5-7:**
5. **Epic #11: Base Data Management** - Projects/Groups/Categories dialogs
6. **Epic #13: Task and Time Tracking** - Timer, entries, validation (MOST CRITICAL)

### Phase 4: Polish and Accessibility
**Weeks 8-9:**
7. **Epic #12: Symbol Management** - Visual enhancements
8. **Epic #15: Accessibility and Localization** - Make it usable by everyone

### Suggested for @copilot Assignment
Based on complexity assessment, the following child tasks are good candidates:

**Low Complexity (Good for @copilot):**
- Database table entity classes (Epic #9)
- Resource file creation for translations (Epic #15)
- Settings.json schema and serialization (Epic #16)
- Basic control layout (TableLayoutPanel setup) (Epic #11)
- StatusStrip layout and data binding (Epic #14)

**Medium Complexity (Review before assignment):**
- Timer service implementation (Epic #13)
- TreeView data binding (Epic #14)
- Symbol rendering in controls (Epic #12)
- Validation framework (Epic #11, #13)

**High Complexity (Human required):**
- Windows authentication integration (Epic #10)
- Time zone handling and UTC conversions (Epic #13)
- Owner-draw symbol rendering (Epic #12)
- Accessibility compliance (Epic #15)
- Dark mode system-wide implementation (Epic #14)

## Key Design Principles Applied

1. **Minimal Friction:** Timer starts with F9, quick task entry with Ctrl+Enter
2. **Accessibility First:** Full keyboard navigation, screen reader support, high-contrast mode
3. **Soft Deletion:** DateDeleted field throughout, no hard deletes
4. **GUID-Based PKs:** Enables future sync scenarios
5. **Windows Integration:** Automatic authentication, no login dialogs
6. **Validation at Commit:** Errors highlighted but don't block entry, fixed on save
7. **Consistent 11pt Segoe UI:** Base font throughout (customizable)
8. **WCAG AA Compliance:** 4.5:1 minimum contrast ratio enforced

## Next Steps

1. **Start with Epic #9** (Database Schema) - foundation for everything
2. **Implement Epic #10** (First Start Experience) - user onboarding
3. **Build Epic #14** (Main UI) - establish visual structure
4. **Deliver Epic #13** (Time Tracking) - core business value
5. **Polish with Epic #15** (Accessibility) - ensure inclusivity

## Files Modified

- `create-labels.ps1` - Label creation script (can be deleted after execution)
- `GITHUB_ISSUES_SUMMARY.md` - This summary document

## Validation Checklist

- [x] All epics created with proper structure
- [x] Labels applied correctly (epic, area:*, priority)
- [x] Dependencies documented
- [x] Acceptance criteria defined for each epic
- [x] Technical notes included
- [x] Child tasks outlined (11-24 tasks per epic)
- [x] Specification sections referenced
- [x] Complexity assessment completed
- [x] @copilot assignment guidance provided

## Issue Statistics

- **Total Epics Created:** 8
- **Total Labels Created:** 14
- **Estimated Total Child Tasks:** ~120-140 (when broken down)
- **Priority Breakdown:**
  - P0 Critical: 2 epics (Main UI, Time Tracking)
  - P1 High: 3 epics (Database, Auth, Base Data)
  - P2 Medium: 2 epics (Symbol Management, Accessibility)
  - P3 Low: 1 epic (Settings)

---

**Repository:** https://github.com/KlausLoeffelmannOrg/WA_LegatroLight  
**Issues:** https://github.com/KlausLoeffelmannOrg/WA_LegatroLight/issues

**Status:** ✅ All epic issues created successfully
