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


## Contraintes techniques

Afin de pouvoir build le projet, il faut utiliser le framework .NET et Asp.Net en version 6.0.

Les NuGet que nous utilisons pour ce projet sont :

* EntityFramework afin de gérer la persistance dans les bases de données

* MSTest pour faire les tests unitaires



## Premier pas

Voici la liste des étapes pour tester notre API dans son intégralité : 

### Lien postman pour l'API RESTful
https://documenter.getpostman.com/view/24331780/2s935oKiHW#c68e5320-c4b9-4ae9-aa0e-2864b2804601



## Où en sommes-nous ?
---

### API REST

- ✅ Contrôleur dé

- ✅ Contrôleur profil

- 🔜 Contrôleur session

### API GRPC

- ✅ Contrôleur lancers de profil

- 🔜 Contrôleur lancers de session


✅ Les **Tests unitaires** sur les contrôleurs

✅ Versionnage des API

✅ **Logs** de l'application

✅ API Gateway

✅ **Application console** pour l'API GRPC

✅ Collection Postman pour l'API gateway

## Objectifs

* Court terme : Implémentation du contrôleur REST sur les Sessions et du service gRPC sur les lancers des sessions

* Long terme : Réaliser des clients mobiles pour consommers nos API

## Exécuter les tests

Les tests sont situés dans **Api.UnitTests**. Ainsi, ouvrez le projet **Api.UnitTests** puis exécutez les. 

## Tester l'API gRPC

Pour ce faire, il vous suffira de lancer à la fois notre api gRPC **ApiGRPCDiceyProject.csproj** mais également notre client console ****ApiGRPCDiceyProject.Client.csproj**. Vous serez guidez au travers de l'application via des menus et des essais avec des données déjà rentrées en dure vous serons proposés pour pouvoir tester directement les différentes routes possibles.

Ainsi, au travers de notre client console, vous pourrez faire appel à notre 4 routes contenues dans l'Api gRPC :
- GetThrowById -> Récupérer un lancer suivant son id
- GetThrowByProfileId -> Récupérer avec un système de pagination les lancers d'un joueur
- AddThrow -> Pour ajouter un lancer dans la base de données
- RemoveThrow -> Pour supprimer un lancer dans la base de données.

### Contrainte technique 

Si jamais l'accès à la bd ne se fait pas automatiquement, vous devrez la créer à la main. Vous aurez besoin d'avoir *EntityFramework* d'installé sur votre machine puis exécuté les étapes suivantes :
- Déplacez vous dans APIDiceyProject/ApiGRPCDiceyProject (Le projet contenant notre service gRPC).

- ``dotnet ef migrations add test --project ../Api.EF --context ApiDbContextStubbed`` -> pour effectuer une migration

- ``dotnet ef database update test --project ../Api.EF --context ApiDbContextStubbed`` -> pour effectuer la création

Ensuite, exécutez les deux projets sous un terminal Windows et profitez !
## Documentation

La documentation du projet avec les différents diagrammes UML est disponible sur notre **[wiki](https://codefirst.iut.uca.fr/git/louis.perret/APIDiceyProject/wiki)**.


## Structure du projet

Le projet est séparé en plusieurs assemblies afin de pouvoir gérer au mieux les dépendances, et éviter que par exemple, le modèle ait besoin des tests. Ce qui donne la structure suivante :


| Nom de l'assembly | Description | Dépendances |
| -------- | -------- | -------- |
| Api.Modele | Ensemble des classes du modèle | / |
| Api.DTOs | Ensemble des classes représentant les données renvoyées par notre API | / |
| Api.EF | Assembly contenant la liaison avec la BD via EntityFramework | Api.Entities, EntityFramework |
| Api.Entities | Assembly contenant les entités stockées dans la BD | EntityFramework |
| Api.Repositories | Assembly contenant nos classes permettant d'effectuer des requêtes sur notre BD | Api.Model, Api.EF, Api.Entities, Exceptions, ModelEntityExtensions |
| Api.Services | Assembly contenant nos classes permettant de faire la liaison entre nos API et nos repositories | Api.Model, Api.Repositories, Exceptions |
| ApiRESTDiceyProject | Assembly contenant les contrôleurs de notre API REST | Api.DTOs, Api.Model, Api.Services, Api.Repositories, Exceptions, Api.ModelDTOExtensions |
| ApiGRPCDiceyProject | Assembly contenant notre service gRPC | Api.Model, Api.Services, Api.Repositories, Api.ModelDTOExtensions |
| ApiGRPCDiceyProject | Assembly contenant notre client console gRPC | ApiGRPCDiceyProject |
| Api.UnitTest | Assembly contenant les tests de notre API REST | ApiRESTDiceyProject |
| ApiGRPC.UnitTest | Assembly contenant les tests de notre API GRPC | ApiGRPCDiceyProject |
| Exceptions | Ensemble des exceptions lancées par nos différents assembly | / |
| ModelDTOExtensions | Assembly contenant nos méthodes d'extensions entre nos objets Model et DTO | Api.DTOs, Api.Model |
| ModelEntitiesExtensions | Assembly contenant nos méthodes d'extensions entre nos objets Model et Entitues | Api.Model, Api.Entities |

## Contributeurs

Grienenberger Come, groupe PM B3

Malvezin Neitah, groupe PM B3

Perret Louis, groupe PM A1