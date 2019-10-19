# https://docs.microsoft.com/en-us/nuget/concepts/package-versioning : Major.Minor.Patch[-Suffix]
$packageVersion = "5.0.0-rc1"
$project = "..\src\ClashOfClans\ClashOfClans.csproj"
$configuration = "Release"

dotnet clean -c $configuration $project
dotnet pack -p:PackageVersion=$packageVersion -p:Version=$packageVersion -c $configuration $project
