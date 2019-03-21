1) Create a .NetCore.App library of the last dotnetcore version you have installed
called WWTravelClubDB. 

2) Then add the following nuget packages:
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Cosmos
Microsoft.EntityFrameworkCore.Tools
Version must be the same of the chosent dotet core version! 
Microsoft.EntityFrameworkCore.Cosmos might have some "preview..." postfix after the dothet core version.

3) Delete also the initial class scaffolded by Visual Studio

4) Then add to the solution, a dotnetcore consolle application called WWTravelClubDBTest
with the same dotnet core version of the WWTravelClubDB library.

5) Add the previous library as a reference of this consolle project.

6) Finally copy the whole content of the ch07->WWTravelClubDB folder onto
your solution folder.

7) PAY ATTENTION COSMOS DOES NOT NEED MIGRATIONS. Run the consolle application.




