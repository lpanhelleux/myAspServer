Remove-Item –path .\TestResults\* -recurse
dotnet test --collect:"XPlat Code Coverage"  --results-directory ".\TestResults\Test1"
reportgenerator -reports:".\TestResults\Test1\*\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html