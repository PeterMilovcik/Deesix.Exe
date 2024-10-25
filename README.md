# Deesix.Exe

Deesix.Exe is a cross-platform, .NET 8 powered text-based RPG console game. It features rich, dynamic UIs, intricate storytelling, and deep gameplay mechanics. Explore, survive, and unravel mysteries in a procedurally generated world. Perfect for fans of classic RPGs and modern console interfaces.

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

## Implementation details

### Using EF Core with a Centralized Build Output (SQLite) in .NET 8

#### Prerequisites

- **.NET 8 SDK**: Ensure the .NET 8 SDK is installed on your system.
- **Entity Framework Core CLI**: Install or update the EF Core CLI tools to ensure compatibility with .NET 8:

```bash
dotnet tool install --global dotnet-ef --version 8.0.6
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
