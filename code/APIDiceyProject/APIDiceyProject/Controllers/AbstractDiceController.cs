using Api.Services;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using ModelDTOExtensions;

namespace APIDiceyProject.Controllers.V1
{
    /// <summary>
    /// Controlleur abstrait pour les dés.
    /// </summary>
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/Dice")]
    [ApiController]
    public abstract class AbstractDiceController : ControllerBase
    {
        #region attributs
        /// <summary>
        /// Service contenant la logique CRUD des dés.
        /// </summary>
        private IDiceService _diceService;

        /// <summary>
        /// Logger de la classe.
        /// </summary>
        protected ILogger<AbstractDiceController>? _logger;
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur complet.
        /// Utilise le constructeru à un argument.
        /// </summary>
        /// <param name="logger"> Logger de cette classe. </param>
        /// <param name="diceService"> Service contenant la logique CRUD des dés. </param>
        protected AbstractDiceController(ILogger<AbstractDiceController> logger, IDiceService diceService)
        {
            _diceService = diceService;
            _logger = logger;
        }

        #endregion

        #region routes

        /// <summary>
        /// Récupère la liste complète des dés.
        /// </summary>
        /// <returns> La liste complète des dés. </returns>
        [HttpGet]
        public async Task<IActionResult> GetDices()
        {
            var dices = await _diceService.GetDices();

            _logger?.LogInformation("GetDices : requête effectuée avec succès. List de dés de taille " + dices.Count() + " retournée.");
            return Ok(dices.ToDTO());
        }
        
        /// <summary>
        /// Récupère un dé en fonction de son nombre de faces.
        /// </summary>
        /// <param name="id">Nombre de faces du dé à récupérer.</param>
        /// <returns>Le dé si trouvé, sinon une BadRequest.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiceById(int id)
        {
            var dice = await _diceService.GetDiceById(id);
            if(dice == null)
            {
                _logger?.LogInformation("GetDicesById : requête effectuée avec succès. Dé d'ID " + id + " demandé par l'utilisateur n'existe pas en base.");
                return NotFound("There is already a dice with this number of faces");
                //Redirect? => question sur comment faire.
            }

            _logger?.LogInformation("GetDicesById : requête effectée avec succès. Dé d'ID " + id + "est retourné à l'utilisateur.");
            return Ok(dice.ToDTO());
        }

        /// <summary>
        /// Supprime tous les dés.
        /// </summary>
        /// <returns>Un code de retour de 200 si effectué, 500 sinon.</returns>
        [HttpDelete]
        public async Task<IActionResult> RemoveAllDices()
        {
            if (await _diceService.RemoveAllDices())
            {
                _logger?.LogInformation("RemoveAllDices : Requête effectuée avec succès. Tous les dés ont été supprimés.");

                return Ok();
            }
            _logger?.LogError("RemoveAllDices : Les dés n'ont pas pu être supprimé par la base de données. ");
            return Problem("Could not delete dices.", statusCode: 500);
        }

        /// <summary>
        /// Supprime un dé.
        /// </summary>
        /// <param name="id">Nombre de faces du dé à supprimer.</param>
        /// <returns>Code de retour de 200 si effectué, 400 ou 500 autrement.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDiceById(int id)
        {
            try
            {
                if (await _diceService.RemoveDiceById(id))
                {
                    _logger?.LogInformation("RemoveDiceById : Requête effectuée avec succès. Le dé d'identifiant " + id + "a bien été suprrimé");
                    return Ok();
                }
                else
                {
                    _logger?.LogInformation("RemoveDiceById : Requête effectuée avec succès. Le dé d'identifiant " + id + " n'existe pas en base. Suppression impossible.");
                    return BadRequest("No dice with this number of faces exists");
                }
            }
            catch (EntityFrameworkException)
            {
                _logger?.LogError("RemoveDiceById : Erreur EntityFramework. Le dé d'identifiant " + id + " n'a pas pu être supprimé.");
                return Problem("Could not remove the dice with the given id from the database", statusCode:500);
            }
        }

        /// <summary>
        /// Ajoute un dé.
        /// </summary>
        /// <param name="dice">Dé à ajouter.</param>
        /// <returns>Code de retour de 201 si ajouté, 400 ou 500 autrement.</returns>
        [HttpPost]
        public async Task<IActionResult> AddDice(Api.DTOs.Dice dice)
        {
            try
            {
                if (await _diceService.AddDice(dice.ToModel()))
                {
                    _logger?.LogInformation("AddDice : requête effectuée avec succès. Le dé d'id " + dice.NbFaces + " a bien été ajouté.");
                    return CreatedAtAction(nameof(AddDice), dice.NbFaces, dice);
                }

                _logger?.LogInformation("AddDice : requête effectuée avec succès. Le dé d'id " + dice.NbFaces + " existe déjà en base. Le dé n'a pu être ajouté.");

                return BadRequest("No dice with this number of faces exists");
            }
            catch(EntityFrameworkException)
            {
                _logger?.LogError("AddDice : Erreur EntityFramework. Le dé d'identifiant " + dice.NbFaces + " n'a pas pu être ajouté.");
                return Problem("Could not insert given object in database.", statusCode: 500);   
            }

        }
        #endregion
    }
}
