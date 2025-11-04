using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegatroLight.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SymbolConfigurations",
                columns: table => new
                {
                    IDSymbol = table.Column<string>(type: "TEXT", nullable: false),
                    SymbolChar = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    SymbolName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    DefaultBackColor = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    DefaultForeColor = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    ContrastRatio = table.Column<double>(type: "REAL", nullable: false),
                    IsSystem = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<string>(type: "TEXT", nullable: false),
                    DateLastEdited = table.Column<string>(type: "TEXT", nullable: false),
                    DateDeleted = table.Column<string>(type: "TEXT", nullable: true),
                    SyncGuidChanged = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymbolConfigurations", x => x.IDSymbol);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IDUser = table.Column<string>(type: "TEXT", nullable: false),
                    UserDisplayId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    UserAuthID = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    UserSid = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    DomainOrMachine = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    MiddleName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    DateCreated = table.Column<string>(type: "TEXT", nullable: false),
                    DateLastEdited = table.Column<string>(type: "TEXT", nullable: false),
                    DateDeleted = table.Column<string>(type: "TEXT", nullable: true),
                    SyncGuidChanged = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IDUser);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IDCategory = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    IDUser = table.Column<string>(type: "TEXT", nullable: false),
                    IDSymbol = table.Column<string>(type: "TEXT", nullable: true),
                    BackColor = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    ForeColor = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IsSystem = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<string>(type: "TEXT", nullable: false),
                    DateLastEdited = table.Column<string>(type: "TEXT", nullable: false),
                    DateDeleted = table.Column<string>(type: "TEXT", nullable: true),
                    SyncGuidChanged = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IDCategory);
                    table.ForeignKey(
                        name: "FK_Categories_SymbolConfigurations_IDSymbol",
                        column: x => x.IDSymbol,
                        principalTable: "SymbolConfigurations",
                        principalColumn: "IDSymbol",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Categories_Users_IDUser",
                        column: x => x.IDUser,
                        principalTable: "Users",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    IDGroup = table.Column<string>(type: "TEXT", nullable: false),
                    GroupDisplayName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    GroupDescription = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    CreatedByIDUser = table.Column<string>(type: "TEXT", nullable: false),
                    IDSymbol = table.Column<string>(type: "TEXT", nullable: true),
                    OrderNo = table.Column<int>(type: "INTEGER", nullable: true),
                    BackColor = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    ForeColor = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IsHidden = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSystem = table.Column<bool>(type: "INTEGER", nullable: false),
                    AutoRangeSpan = table.Column<string>(type: "TEXT", nullable: true),
                    AutoRangeOffset = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<string>(type: "TEXT", nullable: false),
                    DateLastEdited = table.Column<string>(type: "TEXT", nullable: false),
                    DateDeleted = table.Column<string>(type: "TEXT", nullable: true),
                    SyncGuidChanged = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.IDGroup);
                    table.ForeignKey(
                        name: "FK_Groups_SymbolConfigurations_IDSymbol",
                        column: x => x.IDSymbol,
                        principalTable: "SymbolConfigurations",
                        principalColumn: "IDSymbol",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Groups_Users_CreatedByIDUser",
                        column: x => x.CreatedByIDUser,
                        principalTable: "Users",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    IDProject = table.Column<string>(type: "TEXT", nullable: false),
                    IDUser = table.Column<string>(type: "TEXT", nullable: false),
                    IDCategory = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    DueDate = table.Column<string>(type: "TEXT", nullable: true),
                    IsSystem = table.Column<bool>(type: "INTEGER", nullable: false),
                    TimeBudget = table.Column<string>(type: "TEXT", nullable: true),
                    TimeSpent = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "00:00:00"),
                    DateCreated = table.Column<string>(type: "TEXT", nullable: false),
                    DateLastEdited = table.Column<string>(type: "TEXT", nullable: false),
                    DateDeleted = table.Column<string>(type: "TEXT", nullable: true),
                    SyncGuidChanged = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.IDProject);
                    table.ForeignKey(
                        name: "FK_Projects_Categories_IDCategory",
                        column: x => x.IDCategory,
                        principalTable: "Categories",
                        principalColumn: "IDCategory",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Projects_Users_IDUser",
                        column: x => x.IDUser,
                        principalTable: "Users",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TodoTasks",
                columns: table => new
                {
                    IDTodoTask = table.Column<string>(type: "TEXT", nullable: false),
                    IDUser = table.Column<string>(type: "TEXT", nullable: false),
                    IDProject = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    DueDate = table.Column<string>(type: "TEXT", nullable: true),
                    EstimatedEffort = table.Column<string>(type: "TEXT", nullable: true),
                    TimeSpent = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "00:00:00"),
                    PercentDone = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    DateFinished = table.Column<string>(type: "TEXT", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 3),
                    DateCreated = table.Column<string>(type: "TEXT", nullable: false),
                    DateLastEdited = table.Column<string>(type: "TEXT", nullable: false),
                    DateDeleted = table.Column<string>(type: "TEXT", nullable: true),
                    SyncGuidChanged = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoTasks", x => x.IDTodoTask);
                    table.ForeignKey(
                        name: "FK_TodoTasks_Projects_IDProject",
                        column: x => x.IDProject,
                        principalTable: "Projects",
                        principalColumn: "IDProject",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TodoTasks_Users_IDUser",
                        column: x => x.IDUser,
                        principalTable: "Users",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeEntries",
                columns: table => new
                {
                    IDTimeEntry = table.Column<string>(type: "TEXT", nullable: false),
                    IDUser = table.Column<string>(type: "TEXT", nullable: false),
                    IDProject = table.Column<string>(type: "TEXT", nullable: false),
                    IDTodoTask = table.Column<string>(type: "TEXT", nullable: false),
                    DescriptionDoneWork = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    StartTime = table.Column<string>(type: "TEXT", nullable: false),
                    EndTime = table.Column<string>(type: "TEXT", nullable: false),
                    Duration = table.Column<string>(type: "TEXT", nullable: false),
                    TimeZone = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DateCreated = table.Column<string>(type: "TEXT", nullable: false),
                    DateLastEdited = table.Column<string>(type: "TEXT", nullable: false),
                    DateDeleted = table.Column<string>(type: "TEXT", nullable: true),
                    SyncGuidChanged = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntries", x => x.IDTimeEntry);
                    table.ForeignKey(
                        name: "FK_TimeEntries_Projects_IDProject",
                        column: x => x.IDProject,
                        principalTable: "Projects",
                        principalColumn: "IDProject",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeEntries_TodoTasks_IDTodoTask",
                        column: x => x.IDTodoTask,
                        principalTable: "TodoTasks",
                        principalColumn: "IDTodoTask",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeEntries_Users_IDUser",
                        column: x => x.IDUser,
                        principalTable: "Users",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TodoTasksGroupsRelations",
                columns: table => new
                {
                    IDTodoTasksGroups = table.Column<string>(type: "TEXT", nullable: false),
                    IDTodoTask = table.Column<string>(type: "TEXT", nullable: false),
                    IDGroup = table.Column<string>(type: "TEXT", nullable: false),
                    IDUser = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<string>(type: "TEXT", nullable: false),
                    DateLastEdited = table.Column<string>(type: "TEXT", nullable: false),
                    DateDeleted = table.Column<string>(type: "TEXT", nullable: true),
                    SyncGuidChanged = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoTasksGroupsRelations", x => x.IDTodoTasksGroups);
                    table.ForeignKey(
                        name: "FK_TodoTasksGroupsRelations_Groups_IDGroup",
                        column: x => x.IDGroup,
                        principalTable: "Groups",
                        principalColumn: "IDGroup",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TodoTasksGroupsRelations_TodoTasks_IDTodoTask",
                        column: x => x.IDTodoTask,
                        principalTable: "TodoTasks",
                        principalColumn: "IDTodoTask",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TodoTasksGroupsRelations_Users_IDUser",
                        column: x => x.IDUser,
                        principalTable: "Users",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DateCreated",
                table: "Categories",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DateDeleted",
                table: "Categories",
                column: "DateDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IDSymbol",
                table: "Categories",
                column: "IDSymbol");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IDUser",
                table: "Categories",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatedByIDUser",
                table: "Groups",
                column: "CreatedByIDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_DateCreated",
                table: "Groups",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_DateDeleted",
                table: "Groups",
                column: "DateDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupDisplayName",
                table: "Groups",
                column: "GroupDisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_IDSymbol",
                table: "Groups",
                column: "IDSymbol");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OrderNo",
                table: "Groups",
                column: "OrderNo");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DateCreated",
                table: "Projects",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DateDeleted",
                table: "Projects",
                column: "DateDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DueDate",
                table: "Projects",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_IDCategory",
                table: "Projects",
                column: "IDCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_IDUser",
                table: "Projects",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectName",
                table: "Projects",
                column: "ProjectName");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolConfiguration_DateCreated",
                table: "SymbolConfigurations",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolConfiguration_DateDeleted",
                table: "SymbolConfigurations",
                column: "DateDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolConfiguration_IsSystem",
                table: "SymbolConfigurations",
                column: "IsSystem");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolConfiguration_SymbolChar",
                table: "SymbolConfigurations",
                column: "SymbolChar",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SymbolConfiguration_SymbolName",
                table: "SymbolConfigurations",
                column: "SymbolName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_DateDeleted",
                table: "TimeEntries",
                column: "DateDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_EndTime",
                table: "TimeEntries",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_IDProject",
                table: "TimeEntries",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_IDTodoTask",
                table: "TimeEntries",
                column: "IDTodoTask");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_IDUser",
                table: "TimeEntries",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_StartTime",
                table: "TimeEntries",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_DateCreated",
                table: "TodoTasks",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_DateDeleted",
                table: "TodoTasks",
                column: "DateDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_DateFinished",
                table: "TodoTasks",
                column: "DateFinished");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_DisplayName",
                table: "TodoTasks",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_DueDate",
                table: "TodoTasks",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_IDProject",
                table: "TodoTasks",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_IDUser",
                table: "TodoTasks",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_Priority",
                table: "TodoTasks",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasksGroupsRelations_Composite",
                table: "TodoTasksGroupsRelations",
                columns: new[] { "IDTodoTask", "IDGroup" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasksGroupsRelations_DateDeleted",
                table: "TodoTasksGroupsRelations",
                column: "DateDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasksGroupsRelations_IDGroup",
                table: "TodoTasksGroupsRelations",
                column: "IDGroup");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasksGroupsRelations_IDTodoTask",
                table: "TodoTasksGroupsRelations",
                column: "IDTodoTask");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasksGroupsRelations_IDUser",
                table: "TodoTasksGroupsRelations",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DateCreated",
                table: "Users",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DateDeleted",
                table: "Users",
                column: "DateDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserAuthID",
                table: "Users",
                column: "UserAuthID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserDisplayId",
                table: "Users",
                column: "UserDisplayId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserSid",
                table: "Users",
                column: "UserSid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeEntries");

            migrationBuilder.DropTable(
                name: "TodoTasksGroupsRelations");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "TodoTasks");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "SymbolConfigurations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
