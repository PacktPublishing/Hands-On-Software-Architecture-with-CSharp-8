1) Create a .NetCore.App library of the last dotnetcore version you have installed
called WWTravelClubDB. 

2) Then add the following nuget packages:
Microsoft.EntityFrameworkCore.Design"
Microsoft.EntityFrameworkCore.SqlServer"
Microsoft.EntityFrameworkCore.Tools"
Version must be the same of the chosent dotet core version!

3) Delete also the initial class scaffolded by Visual Studio

4) Then add to the solution, a dotnetcore consolle application called WWTravelClubDBTest
with the same dotnet core version of the WWTravelClubDB library.

5) Add the previous library as a reference of this consolle project.

6) Finally copy the whole content of the ch06->WWTravelClubDB folder onto
your solution.

7) Follows instructions in section "Entity Framework Core migrations" to create migrations and database

8) Run the consolle application and follow comments in "Querying and updating data with Entity Framework Core"
section

