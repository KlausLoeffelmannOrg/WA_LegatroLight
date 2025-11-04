GitHub Copilot CLI Command Options

   Great! I just installed the extension. Here's what's available:

   Main Commands

     gh copilot [command]

   Available Commands:

     - suggest - Suggest a command based on natural language
     - explain - Explain what a command does
     - config - Configure options (analytics/telemetry)
     - alias - Generate shell-specific aliases

   Command Details

   1. gh copilot suggest

   Purpose: Get command suggestions from natural language descriptions

   Options:

     gh copilot suggest [prompt] [flags]

     Flags:
       -h, --help               Help for suggest
       -s, --shell-out string   Path to file for outputting command to execute
       -t, --target target      Target type: shell, gh, or git

     Global Flags:
           --hostname string    GitHub host for authentication

   Examples:

     # Interactive mode
     gh copilot suggest

     # Git commands
     gh copilot suggest -t git "Undo the most recent local commits"
     gh copilot suggest -t git "Clean up local branches"

     # Shell commands
     gh copilot suggest -t shell "Kill processes holding deleted files"
     gh copilot suggest -t shell "Convert SVG to PNG"

     # GitHub CLI commands
     gh copilot suggest -t gh "Create pull request"
     gh copilot suggest -t gh "List PRs waiting for review"

   2. gh copilot explain

   Purpose: Explain what a command does in natural language

   Options:

     gh copilot explain [command] [flags]

     Flags:
       -h, --help   Help for explain

     Global Flags:
           --hostname string    GitHub host for authentication

   Examples:

     gh copilot explain 'du -sh | sort -h'
     gh copilot explain 'git log --oneline --graph --decorate --all'
     gh copilot explain 'bfg --strip-blobs-bigger-than 50M'

   3. gh copilot config

   Purpose: Configure optional settings (currently just analytics/telemetry)

   Options:

     gh copilot config [flags]

     Flags:
       -h, --help   Help for config

     Global Flags:
           --hostname string    GitHub host for authentication

   Interactive Options:

     - Allow/disallow optional usage data collection

   4. gh copilot alias

   Purpose: Generate shell-specific aliases for convenience

     gh copilot alias [flags]

   Global Flags

   Available on all commands:

     - --hostname string - Specify GitHub host (useful for GitHub Enterprise)
     - -v, --version - Show version
     - -h, --help - Show help

   Model Selection?

   Unfortunately, the gh copilot CLI extension does not have model selection
   options. The model used (like Claude Sonnet 3.5) is determined by:

     - GitHub's backend based on your subscription
     - Request type (suggest vs explain)
     - Availability of models at the time

   Where Model Selection IS Available

   Model selection is typically available in:

     - GitHub Copilot settings on GitHub.com (for organizations)
     - VS Code - Settings → GitHub Copilot
     - Visual Studio - Tools → Options → GitHub Copilot
     - Copilot Chat interface - Sometimes has model picker in UI

   Summary

   The gh copilot CLI is focused on command suggestions and explanations, not model
   configuration. It's a productivity tool for terminal work, not for managing
   Copilot settings.

   For model selection, you'll need to use your IDE settings or GitHub web
   interface!

 D:\CPGit\LegatroLight[⎇ main]                                                                  claude-sonnet-4.5 (1x)
 ──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
 >  Enter @ to mention files or / for commands
 ──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
 Ctrl+c Exit · Ctrl+r Expand recent          