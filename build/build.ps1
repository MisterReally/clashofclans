$packageVersion = "3.0.1"
$project = "..\src\ClashOfClans\ClashOfClans.csproj"

dotnet build --configuration Release $project
dotnet pack -p:PackageVersion=$packageVersion --configuration Release $project
