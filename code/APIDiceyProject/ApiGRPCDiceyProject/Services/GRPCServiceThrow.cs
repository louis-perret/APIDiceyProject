using System;
using Grpc.Core;
using ApiGRPCDiceyProject;
using Api.Services.ThrowService;
using ApiGRPCDiceyProject.ExtensionsMethods;
using Microsoft.AspNetCore.Components;

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
        public override async Task<Throw> GetThrowById(Request request, ServerCallContext context)
        {
            var t = await ThrowService.GetThrowById(new Guid(request.SearchId));
            if(t == null)
            {
                Logger?.LogInformation("GetThrowsById : requête effectuée avec succès. Dé d'ID " + request.SearchId + " demandé par l'utilisateur n'existe pas en base.");
                throw new RpcException(new Status(StatusCode.NotFound, "No Throw with this exist in our database. Try wwith another."));
            }

            return t.ToDTO();
        }
        #endregion
    }
}

