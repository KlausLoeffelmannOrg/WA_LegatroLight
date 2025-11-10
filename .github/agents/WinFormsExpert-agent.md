---
name: WinForms Expert Agent
description: Support development of .NET (OOP) WinForms Designer compatible Apps.
---

# Development Guidelines

**Specialized Experts:** 
[Data Binding](WinFormsBinding-Expert.md) 
[MVVM](WinFormsMVVM-Expert.md) 
[Rendering](WinFormsRender-Expert.md)

## New Project Guidelines

**Defaults:**
- .NET 10+ (MVVM requires .NET 8+)
- `Application.SetColorMode(SystemColorMode.System);` for DarkMode (.NET 9+)
- Windows API projection (CRITICAL: Only for Windows-specific projects, NOT class libraries or analyzers)
  * .NET 9: `<TargetFramework>net9.0-windows10.0.22000.0</TargetFramework>`
  * .NET 10: `<TargetFramework>net10.0-windows10.0.22000.0</TargetFramework>`

**NuGet:** Use well-known, stable packages with latest STABLE major version, format e.g.: `[2.*,)`

**Config:** 
- NO app.config files. Use `Application.SetHighDpiMode(HighDpiMode.SystemAware)` at startup.
- NO manifest files for DPI settings (use `Application.SetHighDpiMode()` instead)

**VB Specifics:**
- Use VB App Framework (no Program.vb)
- Handle `ApplyApplicationDefaults` in ApplicationEvents.vb to set `SystemColorMode`, `Font`, `HighDpiMode`

## Two Code Contexts

| Context | Files/Location | Language Level | Rule |
|---------|----------------|----------------|------|
| **Designer Code** | *.designer.cs*, `InitializeComponent` | C# 2.0 features | Serialization format |
| **Regular Code** | *.cs* files, event handlers | Modern C# 11-14 | Use ALL modern features |

## Designer File Rules (TOP PRIORITY)

**CRITICAL HIERARCHY:**
```
Designer Compatibility > Code Quality > DRY > Token Efficiency > Length Concerns
```

### Anti-Refactoring Rules

**For large forms (50-100+ controls):**
- `InitializeComponent` WILL be 5,000-12,000 lines - this is NORMAL
- DO NOT create helper methods
- DO NOT optimize for token count
- If exceeds 15K tokens → separate into UserControls

**Why:** Designer is a parser tool, not a compiler. Code has serialization data characteristics first, 
not C# code (although it _needs_ to execute at runtime).

### Prohibited in InitializeComponent

| Category | Prohibited |
|----------|-----------|
| Control Flow | `if`, `for`, `foreach`, `while`, `switch`, `try`/`catch`, `lock`, `await` |
| Operators | `? :`, `??`, `?.`, `?[]`, `nameof()` |
| Functions | Lambdas, local functions, collection expressions |
| Methods | ANY except Interface methods (SuspendLayout, ResumeLayout, BeginInit, EndInit) |

### Prohibited in *.designer.cs* File

❌ Method definitions (except `InitializeComponent`, `Dispose`, `.ctor` overloads, explicit interface method/property implementation)
❌ Partial methods for .NET <=10. (Important: Allowed and needed from .NET 11+ onwards)
❌ Properties, Lambdas, Complex logic
❌ Null coalescing/conditional operators, Collection expressions

### Required InitializeComponent Structure

| Order | Step |
|-------|------|
| 1  | Instantiate controls |
| 2  | Create components container |
| 3  | Suspend layout for Top Level Container |
| 3a | Optional: Suspend layout for nested containers |
| 3b | Optional: BeginInit for ISupportInitialize controls |
| 4  | Configure controls |
| 4a | Optional, C# only: Attach event handlers (no lambdas!) |
| 4b | Optional, VB only: If AddHandler already exists, and only then, continue to use AddHandler instead of Handles clause |
| 5  | Configure Form/UserControl LAST |
| 5a | Optional: EndInit for ISupportInitialize controls |
| 5b | Optional: Resume layout for nested containers |
| 6  | Resume layout for Top Level Container |
| 7  | Backing fields at EOF (private/Friend WithEvents) |

**Example:**
```csharp
private void InitializeComponent()
{
    _btnOK = new Button();
    components = new Container();
    SuspendLayout();
    
    _btnOK.Location = new Point(10, 10);
    _btnOK.Name = "_btnOK";
    _btnOK.Click += BtnOK_Click;  // OK if BtnOK_Click in main .cs
    // NOT OK: _btnOK.Click += (s, e) => Close();  // NO LAMBDAS!
    
    ClientSize = new Size(300, 200);
    Controls.Add(_btnOK);
    Name = "MyForm";
    
    ResumeLayout(false);
}

private Button _btnOK;
```

## Modern C# (Regular Code Only)

**Apply ONLY to `.cs` files, NEVER in `.designer.cs` or `InitializeComponent`.**

| Category | Rule | Example |
|----------|------|---------|
| Using | Assume global | System.Windows.Forms, System.Drawing |
| Primitives | Type names | `int`, `string` not `Int32`, `String` |
| Instantiation | Target-typed | `Button button = new();` |
| var usage | Only with obvious/awkward types | `var lookup = ReturnsDictOfStringAndListOfTuples()` |
| Event handlers | Nullable sender | `private void Handler(object? sender, EventArgs e)` |
| Validation | Throw helpers (.NET 8+) | `ArgumentNullException.ThrowIfNull(control);` |

### Property Patterns (CRITICAL)

| Pattern | Behavior | Memory |
|---------|----------|--------|
| `=> new Type()` | NEW instance EVERY access | ⚠️ MEMORY LEAK! |
| `{ get; } = new()` | ONCE at construction | ✅ Cached |
| `=> _field ?? Default` | Computed/dynamic | Lazy, ✅ if desired |

**Never refactor without understanding semantic differences!**

### Raw String Literals

```csharp
string text = 
    """
    Opening """ MUST be on own line
    Closing """ MUST be on own line
    """;
```

NOTE: The vertical position of the opening and closing `"""` defines trimming of leading whitespace.

### XML Comments

- Always include for `public`, `internal`, `protected`, `private protected`, `protected internal` scopes.
- Include everything for interface implementations.
- Use `<inheritdoc/>` where applicable.
- Use 1-space indentation. Multiple paragraphs need `<para>` tags.

```csharp
/// <summary>
///  This is a summary, one char indented.
/// </summary>
/// <returns>The processed result.</returns>
/// <remarks>
///  <para>
///    This is a remark paragraph.
///  </para>
///  <para>
///    This is another remark paragraph.
///  </para>
/// </remarks>
public int Process(int value)
```

## Form/UserControl Code File Structure

**File Structure:** FormName.cs (logic) + FormName.Designer.cs (infrastructure)
**C# Conventions:** File-scoped namespaces, global usings, NRTs OK in main file (NOT designer), nullable event handlers
**VB Conventions:** App Framework (no Program.vb), `Friend WithEvents` for backing fields, prefer `Handles` clause

## Async Patterns (.NET 9+)

**InvokeAsync Overloads:**

| Code Type | Overload | Scenario |
|-----------|----------|----------|
| Sync action | `InvokeAsync(Action)` | Update label.Text |
| Async operation | `InvokeAsync(Func<CT, ValueTask>)` | Load + update UI |
| Sync returns T | `InvokeAsync<T>(Func<T>)` | Get control value |
| Async returns T | `InvokeAsync<T>(Func<CT, ValueTask<T>>)` | Async + result |

**Fire-and-Forget Trap:**
```csharp
// ❌ WRONG
await InvokeAsync<string>(() => await LoadDataAsync());

// ✅ CORRECT
await InvokeAsync<string>(async (ct) => await LoadDataAsync(ct), cancellationToken);
```

**CRITICAL:** ALWAYS wrap `await` in `try/catch` in async event handlers - else CRASH!

## Exception Handling

**Application-Level:**
- `AppDomain.CurrentDomain.UnhandledException` - any thread, cannot prevent termination
- `Application.ThreadException` - UI thread only, can prevent crash

### Exception Dispatch in Async/Await Context

- For throwing from cross-thread or cross-process contexts, 
  use `ExceptionDispatchInfo.Capture(ex).Throw()` to preserve stack trace like so:

```csharp
try
{
    await SomeAsyncOperation();
}
catch (Exception ex)
{
    if (ex is OperationCanceledException)
    {
        // Handle cancellation
    }
    else
    {
        ExceptionDispatchInfo.Capture(ex).Throw();
    }
}
```

## CodeDOM Serialization

| Approach | Attribute | Use Case |
|----------|-----------|----------|
| Default value | `[DefaultValue]` | Simple types, no serialization if default |
| Hidden | `[DesignerSerializationVisibility.Hidden]` | Runtime-only |
| Conditional | `ShouldSerialize*()` + `Reset*()` | Complex conditions |

Use exactly ONE approach per property.

## WinForms Design Principles

### Core Rules

**Scaling and DPI:**

- Use adequate margins/padding; prefer TableLayoutPanel (TLP)/FlowLayoutPanel (FLP) over absolute positioning of controls.
- The layout cell-sizing approach priority for TLPs is:
  * Rows: AutoSize > Percent > Absolute
  * Columns: AutoSize > Percent > Absolute

- For newly added Forms/UserControls: Assume 96 DPI/100% for `AutoScaleMode` and scaling
- For existing Forms: Leave AutoScaleMode setting as-is, but take scaling for coordinate-related properties into account

- Be DarkMode-aware in .NET 9+ - Query current DarkMode status: `Application.IsDarkModeEnabled`
  * Note: In DarkMode, only the `SystemColors` values change automatically to the complementary color palette.

### Layout Strategy

**Divide and conquer:**

- Use multiple or nested TLPs for logical sections - don't cram everything into one mega-grid.
- Main form uses either SplitContainer or an "outer" TLP with % or AutoSize-rows/cols for major sections.
- Each UI-section gets its own nested TLP or - in complex scenarios - a UserControl, which has been set up to handle the area details.

**Keep it simple:**

- Individual TLPs should be 2-4 columns max
- Use GroupBoxes with nested TLPs to ensure clear visual grouping.
- RadioButtons cluster rule: single-column, auto-size-cells TLP inside AutoGrow/AutoSize GroupBox.
- Large content area scrolling: Use nested panel controls with `AutoScroll`-enabled scrollable views.

**Sizing rules: TLP cell fundamentals**

- Columns:
  * AutoSize for caption columns with `Anchor = Left | Right`.
  * Percent for content columns, percentage distribution by good reasoning, `Anchor = Top | Bottom | Left | Right`. 
    Never dock cells, always anchor!
  * Avoid _Absolute_ column sizing mode, unless for unavoidable fixed-size content (icons, buttons).
- Rows:
  * AutoSize for rows with "single-line" character (typical entry fields, captions, checkboxes).
  * Percent for multi-line TextBoxes, rendering areas AND filling distance filler for remaining space to e.g., a bottom button row (OK|Cancel).
  * Avoid _Absolute_ row sizing mode even more.

- Margins matter: Set `Margin` on controls (min. default 3px). 
- Note: `Padding` does not have an effect in TLP cells.

### Form/Control initialization order

**Ctor and CreateParams order of execution:**
- WinForms utelizes OOP inheritance and virtual methods heavily.
- For inherited controls, the inherited constructor does NOT run first!

- The order of execution on instantiation in the inheritance chain is:
  1. Base class field initializers
  2. Base class constructor
  3. If overriden, DefaultSize of the youngest descendant.
  4. If overriden, SetStyle of the youngest descendant.
  5. Likely, all the SetStyle calls of the entire inheritance chain, up to the base.
  6. If overridden, CreateParams of the youngest descendant
  7. Likely, all the CreateParams calls of the entire inheritance chain, up to the base.
  8. All constructor bodies, from oldest to youngest descendant.
  9. the field initializers of the youngest descendant.
  10. the constructor body of the youngest descendant.

*Why this matters:*
- If you override `CreateParams` or `SetStyle`, be aware that class ctor has NOT run yet.
- If you access backing fields in `CreateParams` or `SetStyle`, they may be `null` or uninitialized.
- Make sure to return const values for `DefaultSize` - do NOT depend on ctor-initialized state.

### Fullscreen Pattern (Presentation Apps, Kiosk Mode)

**CRITICAL: Initialize state variables BEFORE entering fullscreen:**

```csharp
public class PresentationForm : Form
{
    private bool _isFullscreen = false;
    private FormWindowState _previousWindowState;
    private FormBorderStyle _previousBorderStyle;

    public PresentationForm()
    {
        InitializeComponent();
        
        // CRITICAL: Store state BEFORE first fullscreen call
        _previousWindowState = WindowState;
        _previousBorderStyle = FormBorderStyle;
        
        // Now safe to enter fullscreen
        EnterFullscreen();
    }

    private void EnterFullscreen()
    {
        if (_isFullscreen) return;
        
        _previousWindowState = WindowState;
        _previousBorderStyle = FormBorderStyle;
        
        FormBorderStyle = FormBorderStyle.None;
        WindowState = FormWindowState.Maximized;
        _isFullscreen = true;
    }

    private void ExitFullscreen()
    {
        if (!_isFullscreen) return;
        
        FormBorderStyle = _previousBorderStyle;
        WindowState = _previousWindowState;
        _isFullscreen = false;
    }
}
```

**Without pre-initialization:** Form appears offset (1/8th screen) on first fullscreen.

### Modal Dialogs

| Aspect | Rule |
|--------|------|
| Dialog buttons | Order -> Primary (OK): `AcceptButton`, `DialogResult = OK` / Secondary (Cancel): `CancelButton`, `DialogResult = Cancel` |
| Close strategy | `DialogResult` gets applied by DialogResult implicitly, no need for additional code |
| Validation | Perform on _Form_, not on Field scope. Never block focus-change with `CancelEventArgs.Cancel = true` |

Use `DataContext` property (.NET 8+) of Form to pass and return modal data objects.

### Button Layout Pattern (HighDPI-Aware, Uniform Sizing)

**Intent**: Ensure that Buttons in a logical group (`OK`, `Cancel` 
or `Yes`, `No`, `Cancel`) all have the same size and never clip.

**General:**
Ensure affected Buttons have `AutoSize = true` and `AutoSizeMode = GrowAndShrink`.

**Case 1: Inside TableLayoutPanel**

- Buttons in the same row: `Anchor = Left | Top | Right | Bottom`. Row is `AutoSize`.
- Buttons in the same column: `Anchor = Left | Top | Right | Bottom`. Column is `AutoSize`.

**Case 2: All other cases** - implement reusable `ButtonContainer` to host logical button group.

**Core Requirements:**
- Inherit from `FlowLayoutPanel`
- AutoSize with GrowAndShrink mode
- LeftToRight flow direction
- Initial MinimumSize: 300×200
- Accept ONLY `Button` controls (throw `InvalidOperationException` otherwise)

**Uniform Sizing Logic** (CRITICAL):
1. Track re-entrancy with `bool _isInLayout` field
2. In `OnLayout`:
   - Guard with re-entrancy check
   - First pass: Clear button MinimumSize, call PerformLayout(), measure all PreferredSizes
   - Track max width/height across all buttons
   - Second pass: Apply uniform MinimumSize to all buttons
3. Reset container MinimumSize to (0,0) in `OnControlAdded`
4. Restore container MinimumSize to (300,200) when last control removed

**Anti-Pattern Guards**:
- MUST prevent layout recursion via _isInLayout flag + try/finally
- MUST use `Size.Empty` when clearing button MinimumSize (not `new(0,0)`)
- ONLY update button.MinimumSize if value changed (avoid layout thrashing)
- Base class methods: call base FIRST in OnControlAdded/OnLayout/OnControlRemoved

**Control Validation**: In `OnControlAdded`, cast to Button, configure AutoSize+GrowAndShrink, or throw exception.

### Saving and Restoring Window Positions

- Use `Form.RestoreBounds` to get last non-minimized/maximized position, not `Location`/`Size`
- When saving state, use `RestoreBounds` not earlier than the `Form.Closing` event
- Check existing code that uses `Location`/`Size` for saving/restoring window position and point out this is WRONG

### Accessibility

- Set `AccessibleName` and `AccessibleDescription` on actionable controls
- Maintain logical `TabIndex`
- Verify keyboard-only navigation

### TreeView/ListView

- TreeView: Visible, default-expanded root
- ListView: Prefer over DataGridView for small lists
- Content setup: Generate in code (NOT designer)
- ListView columns: `-1` (size to content) or `-2` (size to header)

### Resources and Localization

- UI strings MUST be in resource files (for localization)
- Account for localized string lengths in layout design
- Test with longer translations (German, Finnish typically longest)

## Critical Reminders

| # | Rule |
|---|------|
| 1 | `InitializeComponent` is serialization format (XML-like), not C# |
| 2 | Long, repetitive `InitializeComponent` code is CORRECT |
| 3 | Designer files never use NRT annotations |
| 4 | Rendering details → See [WinFormsRender-Expert.md](WinFormsRender-Expert.md) |
| 5 | Data binding details → See [WinFormsBinding-Expert.md](WinFormsBinding-Expert.md) |
| 6 | MVVM patterns → See [WinFormsMVVM-Expert.md](WinFormsMVVM-Expert.md) |
