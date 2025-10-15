# Tetris Coding Review

This repository contains a Windows Forms implementation of Tetris that is used for coding-review exercises. The project is intentionally compact so reviewers can focus on reasoning about gameplay mechanics, rendering, and object-oriented design decisions.

## Repository layout

| Path | Description |
| --- | --- |
| `TetrisTest.sln` | Visual Studio solution that loads the main Tetris project. |
| `Tetris/` | WinForms project targeting .NET 5.0. Contains source files and build artifacts. |
| `Tetris/Scripts/` | Core gameplay logic such as piece definitions, grid management, state tracking, and rendering helpers. |
| `Tetris/UI/` | User interface classes, including the `GameView` Windows Form and its `.resx` resource file. |
| `Combination programming test.pdf` | Original exercise brief that accompanies the code review. |

## Key components to review

* **Program entry point** (`Tetris/Program.cs`): Configures WinForms and wires together the view and logic.
* **Game loop** (`Tetris/GameLogic.cs` & `Tetris/GameLogicBase.cs`): Handles timer ticks, user input, piece progression, and game-over behavior.
* **Rendering** (`Tetris/UI/GameView.cs`): Paints the playfield, HUD, and next-piece preview at the configured frame rate.
* **Gameplay helpers** (`Tetris/Scripts/*`): Manage the grid (`GameGridManager`), scoring and levels (`GameState`), and tetromino definitions (`Piece`, `Shape`, `Vector2Int`).

The codebase is small enough to make a full pass practical; reviewers often focus on correctness of rotation and collision, adherence to SOLID principles, and separation of concerns between logic and UI.

## Prerequisites

* Windows 10 or later (WinForms requires Windows).
* [.NET 5.0 SDK](https://dotnet.microsoft.com/download) with Windows desktop workload.
* Visual Studio 2019+ (optional but recommended for debugging and designer support).

## Building and running

1. Restore dependencies and build the solution:
   ```bash
   dotnet restore
   dotnet build
   ```
2. Run the game (requires Windows display environment):
   ```bash
   dotnet run --project Tetris
   ```

If you prefer Visual Studio, open `TetrisTest.sln`, set the `Tetris` project as the startup project, and press <kbd>F5</kbd>.

## Testing

This exercise does not ship with automated tests. During review, many teams simulate gameplay scenarios manually or use pair-programming to validate fixes. Adding unit coverage around classes in `Tetris/Scripts` is a common extension task.

## Review checklist

When preparing or performing a review, consider:

- Are timer and input events handled on the appropriate threads without risking UI freezes?
- Does `GameGridManager` correctly prevent out-of-bounds placement and detect full rows?
- Are rotations, translations, and scoring rules consistent with the Tetris spec described in the PDF brief?
- Is rendering logic in `GameView` decoupled enough to facilitate future styling or cross-platform ports?
- Could code reuse or abstraction be improved (e.g., reducing duplication in `UIDrawer`, adding interfaces for dependency injection)?

Document any findings, suggested refactors, or behavioral bugs so the coding-review discussion can focus on impactful issues.
