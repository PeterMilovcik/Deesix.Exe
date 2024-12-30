# Deesix.Exe

Deesix.Exe is a cross-platform, .NET 8 powered text-based RPG console game. It features rich, intricate storytelling, and deep gameplay mechanics. Explore, survive, and unravel mysteries in a procedurally generated world. Perfect for fans of classic RPGs and modern console interfaces.

## Available Functionality

- Create new game
- Load game
- Choose a world genre
   - Select specific world genre
- Generate world settings
   - Accept generated world settings
   - Regenerate world settings
- Generate world description
   - Accept generated world description
   - Select specific world description (in work)
- Exit game

## Roadmap

### Implemented Features

- Create new game
- Load game
- Choose a world genre
  - Select specific world genre
- Generate world settings
  - Accept generated world settings
  - Regenerate world settings
- Generate world description
  - Accept generated world description
- Exit game

### Planned Features

- Generate world description
  - Select specific world description (in work)
- Additional world genres
- Create character
  - Choose character race
  - Customize character appearance
  - Choose character background story
  - Choose character name
- Crafting and resource management
  - Gather resources
  - Craft items and equipment
  - Upgrade equipment
  - Discover new crafting recipes
- In-game economy and trading system
  - Buy and sell items
  - Manage inventory
  - Barter with NPCs
  - Establish trade routes
- Random events and encounters
  - Encounter unique NPCs
  - Discover hidden locations
  - Trigger random story events
- Voice acting and sound effects
- Localization and multi-language support
- Exploration actions
  - Search for hidden items
  - Interact with NPCs
  - Map exploration and discovery
  - Set waypoints on the map
- Quest system
  - Accept and complete quests
  - Track quest progress
  - Branching quest outcomes
  - Replayable quests
- Health and stamina management
  - Rest to recover health and stamina
  - Use items to restore health and stamina
  - Manage hunger and thirst
  - Treat injuries and illnesses
- Skill development
  - Train skills to improve proficiency
  - Unlock new abilities and spells
- Combat actions
  - Attack enemies
  - Defend against attacks
  - Use items during combat
  - Flee from combat
  - Use environment for tactical advantage
  - Perform combo attacks
- Social interactions
  - Form alliances with NPCs
  - Engage in dialogue with multiple choices
  - Influence NPCs' attitudes and decisions
  - Recruit companions
  - Build relationships with NPCs
- Environmental interactions
  - Set up camp
  - Build and upgrade shelters
  - Forage for food and water
  - Navigate through different terrains
  - Use tools to interact with the environment

## Implementation details

### Using EF Core with a Centralized Build Output (SQLite) in .NET 8

#### Prerequisites

- **.NET 8 SDK**: Ensure the .NET 8 SDK is installed on your system.
- **Entity Framework Core CLI**: Install or update the EF Core CLI tools to ensure compatibility with .NET 8:

```bash
dotnet tool install --global dotnet-ef --version 8.0.8
```

#### Setting Up the Database

1. **Ensure You're in the Repository Root**:
   - Start your terminal session in the root of your repository where your solution file (.sln) is located.

2. **Create Migrations**:
   - Generate a new migration using the following command. Specify the path to the project file correctly relative to the repository root:

```bash
dotnet ef migrations add <YourMigrationName> --project src/Deesix.Infrastructure/Deesix.Infrastructure.csproj
```

Important: Don't forget to update `<YourMigrationName>` to your migration name, e.g., `Initial`


3. **Update the Database**:
   - Apply the migrations to update the database. This command also references the project file from the repository root:

```bash
dotnet ef database update --project src/Deesix.Infrastructure/Deesix.Infrastructure.csproj
```

#### Additional Useful Commands

- **Remove a Migration**:
  - If you need to reverse a migration, you can execute:

```bash
dotnet ef migrations remove --project src/Deesix.Infrastructure/Deesix.Infrastructure.csproj
```

#### Final Notes

- **Maintain CLI Tool Updates**: Regularly update your EF Core CLI tools to match the SDK version used in development:

```bash
dotnet tool update --global dotnet-ef
```

This guide is designed to facilitate the use of EF Core commands from the root of your repository, making it more convenient and ensuring a smooth workflow with .NET 8. Adjust the guide as needed based on the specifics of your environment and project configurations.

---

### How to run test coverage

To run test coverage, use the following command:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Report Generator

To install the report generator tool, use the following command:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

To generate a report, use the following command:

```bash
reportgenerator -reports:"TestResults/**/*.cobertura.xml" -targetdir:"coverage-report" -reporttypes:Cobertura,Json,Lcov,Html,TextSummary
```

Replace `reporttypes` with your desired formats, separated by commas.
