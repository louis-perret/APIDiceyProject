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
        

        [HttpGet("{id}")]
        public IActionResult GetDiceById(int id)
        {
            var modelDice = _diceService.GetDiceById(id);
            
            if(modelDice == null)
            {
                return NotFound("There is no dice with this number of faces");
            }

            return Ok(new Api.DTOs.Dice(modelDice.NbFaces));
        }

        [HttpDelete]
        public IActionResult RemoveAllDices()
        {
            if (_diceService.RemoveAllDices())
            {
                return Ok();
            }

            // logguer
            return StatusCode(500);
        }

        [HttpPost]
        public IActionResult AddDice(Api.DTOs.Dice dice)
        {
            if (_diceService.AddDice(dice.ToModel()))
            {
                return Ok();
            }

            return StatusCode(500);
        }
        #endregion
    }
}
