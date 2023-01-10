# APIDiceyProject

Pour nous et à supprimer : 
https://github.com/nicolas63/coursservicesmobiles

Pour faire les migrations (à l'IUT, donc en local parce que c'est de la m*rde) :
dotnet new tool-manifest 
dotnet tool install dotnet-ef
dotnet ef (pour vérifier si c'est bien installé)
dotnet ef migrations add test --project ../Api.EF --context Api_DbContext_Stubbed
dotnet ef database update test --project ../Api.EF --context Api_DbContext_Stubbed
