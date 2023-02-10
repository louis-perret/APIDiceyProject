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
    switch (choice)
    {
        case 1:
            try
            {


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
            }
            catch(RpcException e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case 2:
            try
            {


                choice = ManageGetThrowsByProfileIdMenu();
                if (choice == 1)
                {
                    await ExecuteRequestToGetThrowsByProfileId("cc6f9111-b174-4064-814b-ce7eb4169e80", 1, 2);
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Entrez l'id du lancer en question : ");
                    var profileId = ReadGuid();
                    Console.WriteLine("Entrez le numéro de page voulue : ");
                    var numPage = ReadInt();
                    Console.WriteLine("Entrez le nombre de résultats souhaités : ");
                    var nbByPage = ReadInt();
                    await ExecuteRequestToGetThrowsByProfileId(profileId, numPage, nbByPage);
                }
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Message);
            }
            break;

        default:
            loop = false;
            break;
    }
}

/// Manage the main menu of the app.
int ManageMainMenu()
{
    Console.WriteLine("Bienvenue sur cette application console permettant de tester notre API GRPC sur les lancers de dés. " +
    "\n Deux choix s'offrent à vous : " +
        "\n\t 1. Rechercher un lancer avec son id " +
        "\n\t 2. Rechercher les lancers d'un joueur avec un système de pagination" +
        "\n\t 0. Quitter l'application");
    return ReadChoice(0, 2);
}

/// Manage the menu to execute the endpoint "GetThrowById" of the api.
int ManageGetThrowByIdMenu()
{
    Console.WriteLine("Vous pouvez récupérer un lancer par son id avec un système de pagination." +
    "\n Deux choix s'offrent à vous : " +
        "\n\t 1. Utiliser un id de test (on utilise un id qui existe dans la base pour vous montrer le résultat)" +
        "\n\t 2. Utiliser votre propre id" +
        "\n\t 0. Revenir au menu principal");
    return ReadChoice(0, 2);
}

/// Manage the menu to execute the endpoint "GetThrowByProfileId" of the api.
int ManageGetThrowsByProfileIdMenu()
{
    Console.WriteLine("Vous pouvez récupérer les différents lancers d'un joueur." +
    "\n Deux choix s'offrent à vous : " +
        "\n\t 1. Utiliser un id d'un joueur de test (on utilise un id qui existe dans la base pour vous montrer le résultat)" +
        "\n\t 2. Utiliser votre propre id" +
        "\n\t 0. Revenir au menu principal");
    return ReadChoice(0, 2);
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
    Console.WriteLine($"Lancé récupéré: {reply.ThrowId}, face obtenue : {reply.Result} avec un dé à {reply.IdDice} faces");
}

/// Execute the endpoint GetThrowByProfileId of the api.
async Task ExecuteRequestToGetThrowsByProfileId(string profileId, int numPage, int nbByPages)
{
    var reply2 = await client.GetThrowByProfileIdAsync(new RequestGetThrowByProfileId() { ProfileId = profileId, NumPages = numPage, NbElements = nbByPages });
    Console.WriteLine($"Retrieved throw with {profileId} as profile id : ");
    foreach (var t in reply2.Throws)
    {
        Console.WriteLine($"\tLancé récupéré : {t.ThrowId}, face obtenue : {t.Result} avec un dé à {t.IdDice} faces");
    }
}