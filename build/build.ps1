. .\functions.ps1

$version = Get-PackageVersion
$packageId = "ClashOfClans"
$basePath = "..\src\ClashOfClans\"
$project = $basePath + $packageId + ".csproj"
$nuspec = $basePath + $packageId + ".nuspec"
$nupkg = "$packageId.$version.nupkg"
$configuration = "Release"
$commit = Get-CommitHash

dotnet clean -c $configuration $project
dotnet build -c $configuration $project -p:Version=$version -p:ContinuousIntegrationBuild=true

nuget pack $nuspec -Properties "Version=$version;Commit=$commit" -Symbols -SymbolPackageFormat snupkg
nuget push $nupkg -ApiKey ${env:NUGET_API_KEY} -Source https://api.nuget.org/v3/index.json
