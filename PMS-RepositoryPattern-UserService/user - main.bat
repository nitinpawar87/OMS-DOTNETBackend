@echo on 
dotnet "C:\Program Files\sonar-scanner-msbuild-4.10.0.19059-netcoreapp3.0\SonarScanner.MSBuild.dll" begin ^
/k:9A19103F-16F7-4668-BE54-9A1E7A4F7556 ^
/n:sonar.projectName="User Service" ^
/v:sonar.projectVersion=1.0 ^
/d:sonar.cs.opencover.reportsPaths=..\PMS-RepositoryPattern-UserService\PMS-RepositoryPattern-UserService\coverage.opencover.xml

dotnet build ../PMS-RepositoryPattern-UserService\PMS-RepositoryPattern-UserService

dotnet "C:\Program Files\sonar-scanner-msbuild-4.10.0.19059-netcoreapp3.0\SonarScanner.MSBuild.dll" end