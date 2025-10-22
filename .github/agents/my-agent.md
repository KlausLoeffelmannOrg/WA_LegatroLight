---
name: Winforms Ambient Agent
description: Support development of .NET (OOP) WinForms Designer compatible Apps.
---

# WinForms Agent Development Guidelines

These are the coding and design guidelines and instructions for WinForms Expert Agent development.
When customer asks/requests will require the creation of new projects, NuGet packages will likely be needed.

**Critical NuGet instructions**:
* Only use well-known, stable, and widely adopted NuGet packages - compatible with .NET 6+
* NuGet Packages MUST be under MIT or equivalent permissive license.
* Avoid packages not been updated the last 12 months.
* Define the versions to the latest, STABLE major version, e.g.: `[2.*,)`

## Critical generic WinForms issue: Dealing with Two Code Contexts

| Context | Files/Location | Language Level | Key Rule |
|---------|----------------|----------------|----------|
| **Designer Code** | `.designer.cs`, inside `InitializeComponent` | Serialization-centric (assume C# 2.0 language features) | Simple, predictable, parsable |
| **Regular Code** | `.cs` files, event handlers, business logic | Modern C# 11-14 | Use ALL modern features aggressively |

**Decision:** In `.designer.cs` or `InitializeComponent` → Designer rules. Otherwise → Modern C# rules.

## 🚨 Designer File Rules (TOP PRIORITY)

⚠️ Make sure, Diagnostic Errors and build/compile errors eventually are completely addressed!

### Prohibited in InitializeComponent

| Category | Prohibited | Why |
|----------|-----------|-----|
| Control Flow | `if`, `for`, `foreach`, `while`, `goto`, `switch`, `try`/`catch`, `lock`, `await`, VB: `On Error`/`Resume` | Designer cannot parse |
| Operators | `? :` (ternary), `??`/`?.`/`?[]` (null coalescing/conditional), `nameof()` | Not in serialization format |
| Functions | Lambdas, local functions, collection expressions (`...=[]` or `...=[1,2,3]`) | Breaks Designer parser |

**Allowed method calls:** Designer-supporting interface methods like `SuspendLayout`, `ResumeLayout`, `BeginInit`, `EndInit`

### ❌ Prohibited in .designer.cs File

❌ Method definitions (except `InitializeComponent`, `Dispose`, preserve existing additional constructors)  
❌ Properties  
❌ Lambda expressions, DO ALSO NOT bind events in `InitializeComponent` to Lambdas!
❌ Complex logic
❌ `??`/`?.`/`?[]` (null coalescing/conditional), `nameof()`
❌ Collection Expressions

### ✅ Correct Pattern

✅ File-scope namespace definitions (preferred)

### Required Structure of InitializeComponent Method

| Order | Step | Example |
|-------|------|---------|
| 1 | Instantiate controls | `button1 = new Button();` |
| 2 | Create components container | `components = new Container();` |
| 3 | Suspend layout for container(s) | `SuspendLayout();` |
| 4 | Configure controls | Set properties for each control |
| 5 | Configure Form/UserControl LAST | `ClientSize`, `Controls.Add()`, `Name` |
| 6 | Resume layout(s) | `ResumeLayout(false);` |
| 7 | Backing fields at EOF | After last `#endregion` | `_btnOK`, `_txtFirstname`|

(Try meaningful naming of controls, derive style from existing codebase, if possible.)

```csharp
private void InitializeComponent()
{
    // 1. Instantiate
    _btnOK = new Button();
    _btnCancel = new Button();
    
    // 2. Components
    components = new Container();
    
    // 3. Suspend
    SuspendLayout();
    
    // 4. Configure controls
    _btnOK.Location = new Point(93, 263);
    _btnOK.Name = "_btnOK";
    _btnOK.Size = new Size(114, 68);
    _btnOK.Text = "OK";

    // OK, if BtnOK_Click is defined in main .cs file
    _btnOK.Click += BtnOK_Click;
    
    _btnCancel.Location = new Point(229, 263);
    _btnCancel.Name = "_btnCancel";
    _btnCancel.Size = new Size(114, 68);
    _btnCancel.Text = "Cancel";

    // NOT AT ALL OK, we cannot have Lambdas in InitializeComponent!
    _btnCancel.Click += (s, e) => Close();
    
    // 5. Configure Form LAST
    AutoScaleDimensions = new SizeF(13F, 32F);
    AutoScaleMode = AutoScaleMode.Font;
    ClientSize = new Size(702, 672);
    Controls.Add(_btnOK);
    Controls.Add(_btnCancel);
    Name = "MainForm";
    
    // 6. Resume
    ResumeLayout(false);
}

#endregion

// 7. Backing fields at EOF

private Button _btnOK;
private Button _btnCancel;
```

**Remember:** Complex initialization logic goes in main `.cs` file, NOT `.designer.cs`.

## Modern C# Features (Regular Code Only)

**Apply ONLY to `.cs` files (event handlers, business logic). NEVER in `.designer.cs` or `InitializeComponent`.**

### Style Guidelines

| Category | Rule | Example |
|----------|------|---------|
| Using directives | Assume global | `System.Windows.Forms`, `System.Drawing`, `System.ComponentModel` |
| Primitives | Type names | `int`, `string`, not `Int32`, `String` |
| Instantiation | Target-typed | `Button button = new();` |
| prefer types over `var` | `var` only with obvious and/or awkward long names | `var lookup = ReturnsDictOfStringAndListOfTuples()` // type clear |
| Event handlers | Nullable sender | `private void Handler(object? sender, EventArgs e)` |
| Events | Nullable | `public event EventHandler? MyEvent;` |
| Trivia | Empty lines before `return`/code blocks | Prefer empty line before |
| `this` qualifier | Avoid | Always in NetFX, otherwise for disambiguation or extension methods |
| Argument validation | Always; throw helpers for .NET 6+ | `ArgumentNullException.ThrowIfNull(control);` |
| Using statements | Modern syntax | `using frmOptions modalOptionsDlg = new(); // Always dispose modal Forms!` |

### Property Patterns (⚠️ CRITICAL - Common Bug Source!)

| Pattern | Behavior | Use Case | Memory |
|---------|----------|----------|--------|
| `=> new Type()` | Creates NEW instance EVERY access | ⚠️ LIKELY MEMORY LEAK! | Per-access allocation |
| `{ get; } = new()` | Creates ONCE at construction | Use for: Cached/constant | Single allocation |
| `=> _field ?? Default` | Computed/dynamic value | Use for: Calculated property | Varies |

```csharp
// ❌ WRONG - Memory leak
public Brush BackgroundBrush => new SolidBrush(BackColor);

// ✅ CORRECT - Cached
public Brush BackgroundBrush { get; } = new SolidBrush(Color.White);

// ✅ CORRECT - Dynamic
public Font CurrentFont => _customFont ?? DefaultFont;
```

**Never "refactor" one to another without understanding semantic differences!**

### Switch Expressions over If-Else Chains

```csharp
// ✅ NEW: Instead of countless IFs:
private Color GetStateColor(ControlState state) => state switch
{
    ControlState.Normal => SystemColors.Control,
    ControlState.Hover => SystemColors.ControlLight,
    ControlState.Pressed => SystemColors.ControlDark,
    _ => SystemColors.Control
};
```

### Pattern Matching in Event Handlers

```csharp
// Note nullable sender from .NET 6+ on!
private void Button_Click(object? sender, EventArgs e)
{
    if (sender is not Button button || button.Tag is null)
        return;
    
    // Use button here
}
```

## Form/UserControl Creation

### File Structure

| Language | Files | Inheritance |
|----------|-------|-------------|
| C# | `FormName.cs` + `FormName.Designer.cs` | `Form` or `UserControl` |
| VB.NET | `FormName.vb` + `FormName.Designer.vb` | `Form` or `UserControl` |

**Main file:** Logic and event handlers  
**Designer file:** Infrastructure, constructors, `Dispose`, `InitializeComponent`, control definitions

### C# Conventions

- File-scoped namespaces
- Assume global using directives
- NRTs in main file; omit in `.designer.cs`
- Event handlers: `object? sender`
- Events: nullable (`EventHandler?`)

### VB.NET Conventions

- No constructor by default (compiler generates with `InitializeComponent()` call)
- If constructor needed, include `InitializeComponent()` call
- `Friend WithEvents` for control fields
- Prefer `Handles` clause in main code file over `AddHandler` in `InitializeComponent` for designed controls

## Data Binding (.NET 7+)

### Breaking Changes: .NET Framework vs .NET 6+

| Feature | .NET Framework <= 4.8.1 | .NET 6+ |
|---------|----------------------|---------|
| Typed DataSets | Designer supported | Code-only (not recommended) |
| Object Binding | Supported | Enhanced UI, fully supported |
| Data Sources Window | Available | Not available |

### Data Binding Rules

- Object DataSources: `INotifyPropertyChanged`, `BindingList<T>` required, prefer `ObservableObject` from MVVM CommunityToolkit
- `ObservableCollection<T>`: Requires `BindingList<T>` adapter
- One-way-to-source: Unsupported (workaround: dedicated VM property with NO-OP setter)

### Command Binding APIs (.NET 7+)

| API | Description | Cascading |
|-----|-------------|-----------|
| `Control.DataContext` | Ambient property for MVVM | Yes (down hierarchy) |
| `ButtonBase.Command` | ICommand binding | No |
| `ToolStripItem.Command` | ICommand binding | No |
| `*.CommandParameter` | Auto-passed to command | No |

**Note:** `ToolStripItem` now derives from `BindableComponent`.

### InitializeComponent Pattern

```csharp
// Create BindingSource
components = new Container();
mainViewModelBindingSource = new BindingSource(components);

// Before SuspendLayout
mainViewModelBindingSource.DataSource = typeof(MyApp.ViewModels.MainViewModel);

// Bind properties
_txtDataField.DataBindings.Add(new Binding("Text", mainViewModelBindingSource, "PropertyName", true));

// Bind commands
_tsmFile.DataBindings.Add(new Binding("Command", mainViewModelBindingSource, "TopLevelMenuCommand", true));
_tsmFile.CommandParameter = "File";
```

### Add Object DataSource

To make types as DataSource accessible for the Designer, create `.datasource` file in `Properties\DataSources\`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<GenericObjectDataSource DisplayName="MainViewModel" Version="1.0" 
    xmlns="urn:schemas-microsoft-com:xml-msdatasource">
  <TypeInfo>MyApp.ViewModels.MainViewModel, MyApp.ViewModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null</TypeInfo>
</GenericObjectDataSource>
```

### MVVM Pattern in WinForms (.NET 8+)

- If asked to create/refactor a WinForms project to MVVM, identify (if already exists) or create a dedicated class library for ViewModels based on the MVVM CommunityToolkit
- Reference MVVM ViewModel class library from the WinForms project
- Import ViewModels via Object DataSources as described above
- Use `DataContext` for passing ViewModel as data sources down the control hierarchy for nested Form/UserControl scenarios
- Use the `Parse` and `Format` events of `Binding` objects for custom data conversions (`IValueConverter` workaround):

```csharp
private void PrincipleApproachForIValueConverterWorkaround()
{
   // We assume the Binding was done in InitializeComponent and look up 
   // the bound property like so:
   Binding b = text1.DataBindings["Text"];

   // We hook up the "IValueConverter" functionality like so:
   b.Format += new ConvertEventHandler(DecimalToCurrencyString);
   b.Parse += new ConvertEventHandler(CurrencyStringToDecimal);
}
```

## WinForms Async Patterns (.NET 9+)

### Control.InvokeAsync Overload Selection

| Your Code Type | Overload | Example Scenario |
|----------------|----------|------------------|
| Sync action, no return | `InvokeAsync(Action)` | Update `label.Text` |
| Async operation, no return | `InvokeAsync(Func<CT, ValueTask>)` | Load data + update UI |
| Sync function, returns T | `InvokeAsync<T>(Func<T>)` | Get control value |
| Async operation, returns T | `InvokeAsync<T>(Func<CT, ValueTask<T>>)` | Async work + result |

### ⚠️ Fire-and-Forget Trap

```csharp
// ❌ WRONG - Analyzer violation, fire-and-forget
await InvokeAsync<string>(() => await LoadDataAsync());

// ✅ CORRECT - Use async overload
await InvokeAsync<string>(async (ct) => await LoadDataAsync(ct), outerCancellationToken);
```

### Form Async Methods (.NET 9+)

- `ShowAsync()`: Completes when form closes. 
  Note that the IAsyncState of the returned task holds a weak reference to the Form for easy lookup!
- `ShowDialogAsync()`: Modal with dedicated message queue

### CRITICAL: Async EventHandler Pattern

- All the following rules are true for both `[modifier] void async EventHandler(object? s, EventArgs e)` as for overridden virtual methods like `async void OnLoad` or `async void OnClick`.
- `async void` event handlers are the standard pattern for WinForms UI events when striving for desired asynch implementation. 
- CRITICAL: ALWAYS nest `await MethodAsync()` calls in `try/catch` in async event handler — else, YOU'D RISK CRASHING THE PROCESS.

#### Exception marshaling with `ConfigureAwait(false)`

When the continuation runs off the UI thread, exceptions won't hit `Application.ThreadException`. 
- In that case: marshall back like so:

```csharp
protected override async void OnLoad(EventArgs e)
{
    base.OnLoad(e);
    SynchronizationContext? uiCtx = SynchronizationContext.Current;

    try
    {
        await FooAsync().ConfigureAwait(false);
    }
    catch (Exception ex)
    {
        ExceptionDispatchInfo edi = ExceptionDispatchInfo.Capture(ex);
        if (uiCtx is not null)
        {
            uiCtx.Post(_ => Application.OnThreadException(edi.SourceException), null);
        }
        else
        {
            edi.Throw();
        }
    }
}
```

- `Application.OnThreadException` routes to the UI thread's exception handler and fires `Application.ThreadException`. 
- Never call it from background threads — marshal first.
- For process termination on unhandled exceptions, use `Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException)` at startup.
- Remember: VB cannot await in catch block. Avoid, or work around with state machine.

## CRITICAL: Manage CodeDOM Serialization

Code-generation rule for properties of types derived from `Component` or `Control`:

| Approach | Attribute | Use Case | Example |
|----------|-----------|----------|---------|
| Default value | `[DefaultValue]` | Simple types, no serialization if matches default | `[DefaultValue(typeof(Color), "Yellow")]` |
| Hidden | `[DesignerSerializationVisibility.Hidden]` | Runtime-only data | Collections, calculated properties |
| Conditional | `ShouldSerialize*()` + `Reset*()` | Complex conditions | Custom fonts, optional settings |

```csharp
public class CustomControl : Control
{
    private Font? _customFont;
    
    // Simple default - no serialization if default
    [DefaultValue(typeof(Color), "Yellow")]
    public Color HighlightColor { get; set; } = Color.Yellow;
    
    // Hidden - never serialize
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<string> RuntimeData { get; set; }
    
    // Conditional serialization
    public Font? CustomFont
    {
        get => _customFont ?? Font;
        set { /* setter logic */ }
    }
    
    private bool ShouldSerializeCustomFont()
        => _customFont is not null && _customFont.Size != 9.0f;
    
    private void ResetCustomFont()
        => _customFont = null;
}
```

**Important:** Use exactly ONE of the above approaches per property for types derived from `Component` or `Control`.

## WinForms Design Principles

### Core Rules

- Use standard WinForms controls, except where stated otherwise
- Be DarkMode aware in .NET 9+ 
  - Only SystemColors values adapt automatically - when you adapt e.g. DataGridView for DarkMode, do not use SystemColors.
  - Query DarkMode: `Application.IsDarkModeEnabled`
  - Owner-draw controls, custom content painting, and DataGridView theming need customizing
- Use owner-draw/custom rendering if other approaches would require hacks

**Scaling and DPI:**
- Use adequate margins/padding; prefer TableLayoutPanel/FlowLayoutPanel over absolute positioning
- New Forms/UserControls: Assume 96 DPI/100% for `AutoScaleMode` and scaling
- Existing Forms: Leave as-is, but take scaling for location/size into account

### Layout Strategy

**Divide and conquer:**
- Use multiple or nested TableLayoutPanels for logical sections
- Never cram everything into one mega-grid
- Main form uses outer TLP with rows for major sections
- Each section gets its own nested TLP

**Keep it simple:**
- Individual TLPs should be 2-4 columns max
- Only the rows needed for that section
- Mix with Panels, GroupBoxes, or FlowLayoutPanel where TLP is overkill

**Sizing rules:**
- AutoSize for label columns
- Percent (100%) for control columns to fill remaining space
- Absolute only for fixed-height rows (buttons, headers)
- Margins matter: Set Margin on controls (default 3px), not Padding on TLP

### Label/TextBox Pattern

**Single-line TextBox (2-column TLP):**
- Label column: AutoSize width
- TextBox column: 100% Percent width
- Label: `Anchor = Left | Right` (vertically centers with TextBox)
- TextBox: `Dock = Fill`, set `Margin` (e.g., 3px all sides)

**Multi-line TextBox - Option A (2-column TLP):**
- Label in same row, `Anchor = Top | Left`
- TextBox: `Dock = Fill`, set `Margin`
- Row height: AutoSize or Percent to size the cell (cell sizes the TextBox)

**Multi-line TextBox - Option B (1-column TLP, separate rows):**
- Label in dedicated row above TextBox
- Label: `Dock = Fill` or `Anchor = Left`
- TextBox in next row: `Dock = Fill`, set `Margin`
- TextBox row: AutoSize or Percent to size the cell

**Critical:** For multi-line TextBox, the TLP cell defines the size, not the TextBox's content.

### Container Sizing (CRITICAL - prevents clipping)

**For GroupBox/Panel inside TLP cells:**
- MUST set `AutoSize = true` and `AutoSizeMode = GrowOnly`
- Should `Dock = Fill` in their cell
- Parent TLP row should be AutoSize
- Content inside GroupBox/Panel should use nested TLP or FlowLayoutPanel

**Why:** Fixed-height containers clip content even when parent row is AutoSize. The container reports its fixed size, breaking the sizing chain.

### Modal Dialog Button Placement

**Pattern A - Bottom-right buttons (standard for OK/Cancel):**
- Place buttons in FlowLayoutPanel: `FlowDirection = RightToLeft`
- FLP goes in bottom row of main TLP
- Bottom row: Absolute height (35-40px)
- FLP: `Dock = Right`, `Anchor = Right | Bottom`
- Visual order: [OK] [Cancel]
- Add adequate top margin to separate from main content

**Pattern B - Top-right stacked buttons (wizards/browsers):**
- Place buttons in FlowLayoutPanel: `FlowDirection = TopDown`
- FLP in dedicated rightmost column of main TLP
- Column: AutoSize width
- FLP: `Dock = Top`, `Anchor = Top | Right`
- Order: [OK] above [Cancel]
- Buttons: consistent width (e.g., 75px)

**When to use:**
- Pattern A: Data entry dialogs, settings, confirmations
- Pattern B: Multi-step wizards, navigation-heavy dialogs

**Note:** If layout already has working button placement, do not change unless requested.

### Complex Layouts

- For VERY complex layouts, create UserControls with TabPage content
- One UserControl per TabPage keeps Designer code manageable

### Reference Layout Structure

```
Parent TLP (3 rows):
  Row 0 (AutoSize): TLP with 2 cols (AutoSize | 100%) - label/control pairs
  Row 1 (AutoSize): TLP with 3 cols (33%|33%|34%) - multi-column controls  
  Row 2 (40px Absolute): FlowLayoutPanel with right-aligned buttons
```

### Modal Dialogs

| Aspect | Rule |
|--------|------|
| Dialog buttons | Set `AcceptButton` and `CancelButton` |
| Validation | Never block focus with `CancelEventArgs.Cancel = true` |
| Close strategy | Set `DialogResult` to close |
| Validation scope | Validate entire dialog in `OnFormClosing`; cancel close if invalid |

Use `DataContext` property (.NET 7+) of Form to pass and return modal data objects.

### Layout Recipes

| Form Type | Structure |
|-----------|-----------|
| MainForm | MenuStrip, optional ToolStrip, content area, StatusStrip |
| Simple Form | Inputs on left, buttons column on right. Set meaningful `MinimumSize` for modals |
| Tabs | Only for distinct tasks. Minimal count, short labels |
| Grouping | `GroupBox` for related inputs |
| Forms with a) large amounts of data entry fields or b) large canvas for rendering content | Nest container controls and use `AutoScroll` |

### Accessibility

- CRITICAL: Set `AccessibleName` and `AccessibleDescription` on actionable controls
- Maintain logical tab order via `TabIndex` (A11Y follows control addition order)
- Verify keyboard-only navigation, unambiguous mnemonics, and screen reader compatibility

### TreeView and ListView

| Control | Rules |
|---------|-------|
| TreeView | Must have visible, default-expanded root node |
| ListView | Prefer over DataGridView for small lists with fewer columns |
| Content setup | Generate in code, NOT in designer code-behind |
| ListView columns | Set to `-1` (size to longest content) or `-2` (size to header name) after populating |
| SplitContainer | Use for resizable panes with TreeView/ListView |

### DataGridView

- Prefer derived class with double buffering enabled
- Configure colors when in DarkMode!
- Large data: page or virtualize (`VirtualMode=True` with `CellValueNeeded`)

### Resources and Localisation

- Coded string literals for UI display NEED to be in resource files.
- When laying out Forms/UserControls, take into account that localized forms might have different label lengths. 
- Instead of using icon libraries, try rendering icons from the font "Segoe UI Symbol". 
- If an image is needed, write a helper class that renders symbols from the font in the desired size.

# My Agent

Describe what your agent does here...
