// The port number must match the port of the gRPC server.
using ApiGRPCDiceyProject;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Win32.SafeHandles;

var httpHandler = new HttpClientHandler();
httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
using var channel = GrpcChannel.ForAddress("https://localhost:7088", new GrpcChannelOptions { HttpHandler = httpHandler });
var client = new ThrowService.ThrowServiceClient(channel);

var KEYWORDTOEXIT = "No";
int choice;
bool loop = true;

// Main loop for the console app
while(loop)
{
    choice = ManageMainMenu();
    try
    {


        switch (choice)
        {
            case 1:
                    choice = ManageGetThrowByIdMenu();
                    if (choice == 1)
                    {
                        await ExecuteRequestToGetThrowById("aa6f9111-b174-4064-814b-ce7eb4169e80");
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Entrez l'id du lancer en question : ");
                        var searchedIdThrow = ReadGuid();
                        if (!searchedIdThrow.Equals(KEYWORDTOEXIT))
                        {
                            await ExecuteRequestToGetThrowById(searchedIdThrow);
                        }
                    }
                break;
            case 2:
                    choice = ManageGetThrowsByProfileIdMenu();
                    if (choice == 1)
                    {
                        await ExecuteRequestToGetThrowsByProfileId("cc6f9111-b174-4064-814b-ce7eb4169e80", 1, 2);
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Entrez l'id du lancer en question : ");
                        var profileId = ReadGuid();
                        if (profileId.Equals(KEYWORDTOEXIT)) break;
                        Console.WriteLine("Entrez le numéro de page voulue : ");
                        var numPage = ReadInt();
                        Console.WriteLine("Entrez le nombre de résultats souhaités : ");
                        var nbByPage = ReadInt();
                        await ExecuteRequestToGetThrowsByProfileId(profileId, numPage, nbByPage);
                    }
                break;

            case 3:
                ManageAddThrowMenu();
                Console.WriteLine("Veuillez maitenant entrer le dé voulu : ");
                var dice = ReadInt();
                Console.WriteLine("Veuillez maitenant entrer votre résultat après lancer : ");
                var result = ReadInt();
                Console.WriteLine("Veuillez maitenant entrer l'id du lancer : ");
                var profile = ReadGuid();
                if (profile.Equals(KEYWORDTOEXIT)) break;
                await ExecuteRequestAddThrow(dice, result, profile);
                break;

            default:
                loop = false;
                break;
        }
    }
    catch (RpcException e)
    {
        Console.WriteLine(e.Message);
    }
}

/// Manage the main menu of the app.
int ManageMainMenu()
{
    int max = 3;
    Console.WriteLine("\n\nBienvenue sur cette application console permettant de tester notre API GRPC sur les lancers de dés. " +
    "\n Deux choix s'offrent à vous : " +
        "\n\t 1. Rechercher un lancer avec son id " +
        "\n\t 2. Rechercher les lancers d'un joueur avec un système de pagination" +
        "\n\t 3. Ajouter un throw" + 
        "\n\t 0. Quitter l'application");
    return ReadChoice(0, max);
}

/// Manage the menu to execute the endpoint "GetThrowById" of the api.
int ManageGetThrowByIdMenu()
{
    int max = 2;
    Console.WriteLine("\n\nVous pouvez récupérer un lancer par son id avec un système de pagination." +
    "\n Deux choix s'offrent à vous : " +
        "\n\t 1. Utiliser un id de test (on utilise un id qui existe dans la base pour vous montrer le résultat)" +
        "\n\t 2. Utiliser votre propre id" +
        "\n\t 0. Revenir au menu principal");
    return ReadChoice(0, max);
}

/// Manage the menu to execute the endpoint "GetThrowByProfileId" of the api.
int ManageGetThrowsByProfileIdMenu()
{
    int max = 2;
    Console.WriteLine("\n\nVous pouvez récupérer les différents lancers d'un joueur." +
    "\n Deux choix s'offrent à vous : " +
        "\n\t 1. Utiliser un id d'un joueur de test (on utilise un id qui existe dans la base pour vous montrer le résultat)" +
        "\n\t 2. Utiliser votre propre id" +
        "\n\t 0. Revenir au menu principal");
    return ReadChoice(0, max);
}

void ManageAddThrowMenu()
{
    Console.WriteLine("\n\nVous pouvez ajouter un lancer." +
    "\n Pour vous aidez voici de l'aide aux niveaux des différents paramètres à mettre : " +
        "\n\t Tous les dés qui existent déjà en base : 2 faces, 3 faces, 4 faces, 5 faces, 6 faces" +
        "\n\t Et l'id un joueur déjà existant dans la base : cc6f9111-b174-4064-814b-ce7eb4169e80");
}

/// Read an int from the console.
int ReadInt()
{
    int res = 0;
    bool loop = true;
    while (loop)
    {
        if (int.TryParse(Console.ReadLine(), out res))
        {
            loop = false;
        }
        else
        {
            Console.WriteLine("Veuillez entrez un chiffre correcte.");
        }
    }
    return res;
}

/// Read the user's choice in menu.
int ReadChoice(int min, int max)
{
    int res = 0;
    bool loop = true;
    while(loop)
    {
        res = ReadInt();
        if(res >= min && res <= max)
        {
            loop = false;
        }
        else
        {
            Console.WriteLine("Veuillez entrez un chiffre correcte.");
        }
    }
    return res;
}

/// Read the choosen id as Guid.
string ReadGuid()
{
    Console.WriteLine("Nos id sont de types Guid, de la forme suivante : XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX");
    Console.WriteLine($"Tapez : {KEYWORDTOEXIT} pour revenir en arrière.");
    return Console.ReadLine();
}

/// Execute the endpoint GetThrowById of the api.
async Task ExecuteRequestToGetThrowById(string searchedId)
{
    var reply = await client.GetThrowByIdAsync(
                      new RequestGetThrowById() { SearchedId = searchedId });
    DisplayThrow(reply);
}

/// Execute the endpoint GetThrowByProfileId of the api.
async Task ExecuteRequestToGetThrowsByProfileId(string profileId, int numPage, int nbByPages)
{
    var reply = await client.GetThrowByProfileIdAsync(new RequestGetThrowByProfileId() { ProfileId = profileId, NumPages = numPage, NbElements = nbByPages });
    Console.WriteLine($"Retrieved throw with {profileId} as profile id : ");
    foreach (var t in reply.Throws)
    {
        DisplayThrow(t);
    }
}

/// Execute the endpoint AddThrow of the api.
async Task ExecuteRequestAddThrow(int dice, int result, string profile)
{
    var reply = await client.AddThrowAsync(
                      new RequestAddThrow() { IdDice = dice, Result = result, IdProfile = profile});
    DisplayThrow(reply);
}

/// Affiche le lancer renvoyer par l'API.
void DisplayThrow(Throw t)
{
    Console.WriteLine($"Lancer créé : id : {t.ThrowId}, face obtenue : {t.Result} avec un dé à {t.IdDice} faces et un id de profile de {t.ProfileId}");
}