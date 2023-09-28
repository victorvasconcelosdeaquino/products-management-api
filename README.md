# products-management-api

### Requirements
- [.Net Framework 5.0](https://dotnet.microsoft.com/pt-br/download/dotnet/5.0)
- [MySQL](https://www.mysql.com/downloads/)
- [EF Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### Migration
1. Clone the repository to your computer
2. Open the Command/Power Shell and navigate to the .csproj file
3. Type "dotnet ef" to verify if EF Core Tools are installed correctly
4. Type "dotnet ef migrations add -name your migration-". The project will be built.
5. Type "dotnet ef database update". The project will be built.
