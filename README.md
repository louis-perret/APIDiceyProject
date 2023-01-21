![DiceyProject Banner](./images/Banner.jpg)

# APIDiceyProject

| [Introduction](#introduction) | [Requirements](#requirements) | [Getting started](#getting-started) | [Where are we now ?](#where-are-we-now) | [Roadmap](#roadmap) | [Running the tests](#running-the-tests) | [Documentation](#documentation) | [Project structure](#project-structure) | [Contributors](#contributors) | 

---

## Introduction

APIDiceyProject est une API permettant à l'utilisateur de de récupérer des données en lien avec notre application DiceyProject. Ainsi, il peut récupérer les dés, les profils et les sessions créés ainsi que les différents lancés exécutés suivant différent paramètres permettant de filtrer les résultats.

Nous avons deux API :

- API REST permettant de récupérer les dés, profils et sessions crées
- API GRPC permettant de récupérer les différents lancers d'un profil et d'une session

Nous avons effectué ce choix car nous risquons de posséder plusieurs dizaines de lancers que l'API pourra retourner. Ainsi, l'utilisation d'une API GRPC permettra de répondre à ce besoin en améliorant les performances de l'utilisation de cette dernière.


## Requirements

Afin de pouvoir build le projet, il faut utiliser le framework .NET et Asp.Net en version 6.0.

Les NuGet que nous utilisons pour ce projet sont :

* EntityFramework afin de gérer la persistance dans les bases de données

* MSTest pour faire les tests unitaires



## Getting started

Afin de pouvoir tester notre API (pour l'instant uniquement disponible en mode console), il vous suffit de lancer la solution APIDiceyProject.sln, puis de générer et lancer l'assembly `MyConsoleApp.csproj`



## Where are we now?
---

### API REST

- ✅ Contrôleur dé

- 🔜 Contrôleur profil

- 🔜 Contrôleur session

### API GRPC

- 🔜 Contrôleur lancers de profil

- 🔜 Contrôleur lancers de session


🔜 Les **Tests unitaires** sur les contrôleurs

🔜 Versionnage des API

🔜 **Logs** de l'application

🔜 API Gateway

🔜 **Application console**

## Roadmap

* Court terme : Implémentation des contrôleurs sur les profils et les lancers

* Long terme : Implémentation de l'API GRPC puis d'une API Gateway

## Running the tests

Les tests sont situés dans **Api.UnitTests**. Ainsi, ouvrez le projet **Api.UnitTests** puis exécutez les. 

## Documentation

La documentation du projet avec les différents diagrammes UML est disponible sur notre **[wiki](https://codefirst.iut.uca.fr/git/louis.perret/APIDiceyProject/wiki)**.


## Project Structure

Le projet est séparé en plusieurs assemblies afin de pouvoir gérer au mieux les dépendances, et éviter que par exemple, le modèle ait besoin des tests. Ce qui donne la structure suivante :


| Nom de l'assembly | Description | Dépendances |
| -------- | -------- | -------- |
| Api.Modele | Ensemble des classes du modèle | / |
| Api.DTOs | Ensemble des classes représentant les données renvoyées par notre API | / |
| Api.EF | Assembly contenant la liaison avec la BD via EntityFramework | Api.Entities, EntityFramework |
| Api.Entities | Assembly contenant les entités stockées dans la BD | EntityFramework |
| Api.Repositories | Assembly contenant nos classes permettant d'effectuer des requêtes sur notre BD | Api.Model, Api.EF, Api.Entities, Exceptions, ModelEntityExtensions |
| Api.Services | Assembly contenant nos classes permettant de faire la liaison entre nos API et nos repositories | Api.Model, Api.Repositories, Exceptions |
| ApiDiceyProject | Assembly contenant les contrôleurs de notre API REST | Api.DTOs, Api.Model, Api.Services, Exceptions, Api.ModelDTOExtensions |
| Api.UnitTest | Assembly contenant les tests de notre API REST | / |
| Exceptions | Ensemble des exceptions lancées par nos différents assembly | / |
| ModelDTOExtensions | Assembly contenant nos méthodes d'extensions entre nos objets Model et DTO | Api.DTOs, Api.Model |
| ModelEntitiesExtensions | Assembly contenant nos méthodes d'extensions entre nos objets Model et Entitues | Api.Model, Api.Entities |

## Contributors 

Grienenberger Come, groupe PM B3

Malvezin Neitah, groupe PM B3

Perret Louis, groupe PM A1

# A supprimer plus tard

# APIDiceyProject

Pour nous et à supprimer : 
https://github.com/nicolas63/coursservicesmobiles

Pour faire les migrations (à l'IUT, donc en local parce que c'est de la m*rde) :

Installation de EF CLI (Nuget Package Manager)

dotnet new tool-manifest 

dotnet tool install dotnet-ef

dotnet ef (pour vérifier si c'est bien installé)

Création de la base de données (Reset à chaque foi pour l'instant => pas de migrations).

Se déplacer dans APIDiceyProject/APIDiceyProject (Le project contenant les controllers de l'API RESTful).

dotnet ef migrations add test --project ../Api.EF --context Api_DbContext_Stubbed

dotnet ef database update test --project ../Api.EF --context Api_DbContext_Stubbed

