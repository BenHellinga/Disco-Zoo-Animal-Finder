# Disco Zoo Animal Finder

**This project is no longer under active development.**

## About

Disco Zoo Animal Finder is a solver for a mobile game called [Disco Zoo](https://en.wikipedia.org/wiki/Disco_Zoo). In the game, each stage hides 1 to 3 animals on a 5x5 grid, and each animal has its own fixed shape. You dig one tile at a time, and each dig either reveals part of an animal or comes up empty, narrowing down where the animals could be. This project takes the animal names for a stage and works out the best tile to dig next. This was a 2023 side project I put together over a weekend.

## Implementation

- The board is a 5x5 grid of tiles, each either unknown, known empty, or known to belong to a specific animal.
- Every animal is a value of the `AnimalType` enum. `AnimalCatalog` maps each `AnimalType` to its display name and its fixed 4x4 pattern, and parses user-typed names back into an `AnimalType`.
- The solver brute forces every valid placement of every animal on the board, recursively trying each animal in turn and skipping any placement that conflicts with what's already known.
- Each full valid arrangement increments a per tile counter for every tile it covers.
- Once every arrangement has been tried, the tile with the highest count is the best guess, since it's covered by an animal in the most possible solutions.
- The counts are also rendered as a heatmap, alongside the odds that the suggested tile actually contains an animal.
- After digging, you tell the solver what was found (an animal, nothing, or `done`/`reset`), it updates the board, and searches again. Input is validated, so unrecognized text just re-prompts instead of crashing.

## Usage

1. Build with the .NET SDK, eg `dotnet build`, or run directly with `dotnet run`.
2. Enter the animals present in the stage, comma separated, matching the names in `AnimalCatalog.cs`.
3. The solver prints the board, the tile counts, a heatmap, and its best guess.
4. Enter what was found at the suggested tile: `x` or `nothing` for empty, an animal's name (or its number in the list you entered) if it was found, or `done`/`reset` to end or restart.
5. Repeat until every animal has been fully located.

## Example

<img width="424" height="956" alt="image" src="https://github.com/user-attachments/assets/ecc5628f-690d-4de6-9731-6dfb7e02dd3a" />
