using System;
using Grpc.Core;
using ApiGRPCDiceyProject;
using Api.Services.ThrowService;
using ApiGRPCDiceyProject.ExtensionsMethods;
using Microsoft.AspNetCore.Components;
using Api.Model.Throw;

namespace ApiGRPCDiceyProject.Services
{
    /// <summary>
    /// Service GRPC permettant de gérer les requêtes en lien avec les lancers (Throw).
    /// </summary>
	public class GRPCServiceThrow : ThrowService.ThrowServiceBase
    {
        #region attributs
        /// <summary>
        /// Service contenant la logique CRUD des lancers.
        /// </summary>
        public IThrowService ThrowService { get; set; }

        /// <summary>
        /// Logger de la classe.
        /// </summary>
        public ILogger<GRPCServiceThrow>? Logger { get; set; }

        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur complet.
        /// Utilise le constructeru à un argument.
        /// </summary>
        /// <param name="logger"> Logger de cette classe. </param>
        /// <param name="throwService"> Service contenant la logique CRUD des dés. </param>
        public GRPCServiceThrow(ILogger<GRPCServiceThrow> logger, IThrowService throwService)
        {
            ThrowService = throwService;
            Logger = logger;
        }
        #endregion

        #region routes
        /// <summary>
        /// Récupère un lancer suivant son id.
        /// </summary>
        /// <param name="request">Message provenant du client.</param>
        /// <param name="context"></param>
        /// <returns>Un objet Throw correpondant à l'id.</returns>
        public override async Task<Throw> GetThrowById(RequestGetThrowById request, ServerCallContext context)
        {
            try
            {
                var t = await ThrowService.GetThrowById(new Guid(request.SearchedId));
                if (t == null)
                {
                    Logger?.LogInformation("GetThrowById : requête effectuée avec succès. Throw d'ID " + request.SearchedId + " demandé par l'utilisateur n'existe pas en base.");
                    throw new RpcException(new Status(StatusCode.NotFound, "No Throw with this exist in our database. Try wwith another."));
                }

                return t.ToDTO();
            }
            catch(FormatException e)
            {
                Logger?.LogInformation("GetThrowById : Problème de format de l'id passé en paramètre, id = {0}", request.SearchedId);
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The id of the wanted throw was incorrect"));
            }
        }

        /// <summary>
        /// Récupère les lancers d'un joueur avec un système de pagination.
        /// </summary>
        /// <param name="request">Message provenant du client.</param>
        /// <param name="context"></param>
        /// <returns>Une liste de lancers.</returns>
        public override async Task<ListThrows> GetThrowByProfileId(RequestGetThrowByProfileId request, ServerCallContext context)
        {
            try
            {
                if (request.NumPages <= 0 || request.NbElements <= 0)
                {
                    Logger?.LogInformation("GetThrowsByProfilId : id du profile = {0}, numéro de page = {1}, nombre d'éléments = {2}. Un de ces paramètres est inférieur où égale à zéro. Requête annulée.", request.ProfileId, request.NumPages, request.NbElements);
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid argument, page number and number of elements must be superior to 0."));
                }

                var throws = await ThrowService.GetThrowByProfileId(Guid.Parse(request.ProfileId), request.NumPages, request.NbElements);
                if (throws == null)
                {
                    Logger?.LogInformation("GetThrowsByProfilId : id du profile = {0}, numéro de page = {1}, nombre d'éléments = {2}.Aucun throw retournée -> Id du profil incorrecte.", request.ProfileId, request.NumPages, request.NbElements);
                    return new ListThrows();
                }
                else
                {
                    Logger?.LogInformation("GetThrowsByProfilId : id du profile = {0}, numéro de page = {1}, nombre d'éléments = {2}. Méthode exécutée correctement et qui a retourné des éléments.", request.ProfileId, request.NumPages, request.NbElements);
                }

                var response = new ListThrows();
                response.Throws.AddRange(throws.ToDTO());
                return response;
            }
            catch (FormatException e)
            {
                Logger?.LogInformation("GetThrowByProfileId : Problème de format de l'id passé en paramètre, id = {0}", request.ProfileId);
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The id of the profile was incorrect"));
            }
        }


        /// <summary>
        /// Ajoute un lancer dans la base de données.
        /// </summary>
        /// <param name="request">Message provenant du client.</param>
        /// <param name="context"></param>
        /// <returns>Le lancer ajouté.</returns>
        public override async Task<Throw> AddThrow(RequestAddThrow request, ServerCallContext context)
        {
            try
            {
                if (request.IdDice <= 0) throw new RpcException(new Status(StatusCode.InvalidArgument, "The number of faces of the dice must be superior to 0."));
                if (request.Result > 0 || request.Result <= request.IdDice) throw new RpcException(new Status(StatusCode.InvalidArgument, "The result of the dice must be superior to 0 and inferior to the number of faces of the dice."));
                var res = await ThrowService.AddThrow(request.Result, request.IdDice, Guid.Parse(request.IdProfile));
                Logger?.LogInformation("AddThrow : Effectué avec succès : id = {0}, result = {1}, id dé = {2}, id profil = {3}", res, request.Result, request.IdDice, request.IdProfile);
                return new Throw() { ThrowId = res.ToString(), IdDice = request.IdDice, Result = request.Result };
            }
            catch (FormatException e)
            {
                Logger?.LogInformation("AddThrow : Problème de format de l'id passé en paramètre, id = {0}", request.IdProfile);
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The id of the profile was incorrect"));
            }

        }
        #endregion
    }
}

