using Api.Services;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using ModelDTOExtensions;

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
        public async Task<IActionResult> GetDices()
        {
            return Ok((await _diceService.GetDices()).ToDTO());
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiceById(int id)
        {
            var dice = await _diceService.GetDiceById(id);
            if(dice == null)
            {
                return NotFound("There is already a dice with this number of faces");
                //Redirect? => question sur comment faire.
            }
            return Ok(_diceService.GetDiceById(id));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAllDices()
        {
            if (await _diceService.RemoveAllDices()) return Ok();
            
            // logger
            return Problem("Could not delete dices.", statusCode: 500);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDiceById(int id)
        {
            try
            {
                if (await _diceService.RemoveDiceById(id)) return Ok();
                else return BadRequest("No dice with this number of faces exists");
            }
            catch (EntityFrameworkException)
            {
                return Problem("Could not remove the dice with the given id from the database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDice(Api.DTOs.Dice dice)
        {
            try
            {
                if (await _diceService.AddDice(dice.ToModel())) return CreatedAtAction(nameof(GetDices), dice.NbFaces, dice);
                return BadRequest("No dice with this number of faces exists");
            }
            catch(EntityFrameworkException)
            {
                return Problem("Could not insert given object in database.", statusCode: 500);   
            }

        }
        #endregion
    }
}
