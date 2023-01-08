using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIDiceyProject.Controllers
{
    /// <summary>
    /// Controlleur abstrait pour les dés.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
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
        /// Constructeur à un argument.
        /// </summary>
        /// <param name="diceService"> Service contenant la logique CRUD des dés. </param>
        protected AbstractDiceController(IDiceService diceService)
        {
            _diceService = diceService;
        }

        /// <summary>
        /// Constructeur complet.
        /// Utilise le constructeru à un argument.
        /// </summary>
        /// <param name="logger"> Logger de cette classe. </param>
        /// <param name="diceServce"> Service contenant la logique CRUD des dés. </param>
        protected AbstractDiceController(ILogger<AbstractDiceController> logger, IDiceService diceServce) : this(diceServce)
        {
            _logger = logger;
        }
        #endregion

        #region routes

        /// <summary>
        /// Récupère la liste complète des dés.
        /// </summary>
        /// <returns> La liste complète des dés. </returns>
        [HttpGet]
        [Route("dices")]
        public IActionResult GetDices()
        {
            List<Api.DTOs.Dice> dtoDices = new List<Api.DTOs.Dice>();

            var modelDices = _diceService.GetDices();
            
            foreach(Api.Model.Dice modelDice in modelDices)
            {
                dtoDices.Add(new Api.DTOs.Dice(modelDice.NbFaces));
            }


            return Ok(dtoDices);
        }
        #endregion
    }
}
