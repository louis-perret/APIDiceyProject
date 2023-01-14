using Api.Services;
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
            var modelDices = _diceService.GetDices();
            return Ok((await modelDices).ToDTO());
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiceById(int id)
        {
            try
            {
                var dice = await _diceService.GetDiceById(id);
                if(dice == null)
                {
                    return NotFound("There is already a dice with this number of faces");
                }
                return Ok(_diceService.GetDiceById(id));
            }
            #region exceptions
            catch (Exception e)
            {
                return StatusCode(500,"An error has occured");
            }
            #endregion
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAllDices()
        {
            if (await _diceService.RemoveAllDices()) return Ok();

            // logger
            return StatusCode(500);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDiceById(int id)
        {
            try
            {
                if (await _diceService.RemoveDiceById(id))
                    return Ok();
                else
                    return BadRequest("No dice with this number of faces exists");
            }
            catch (Exception)
            {
                return StatusCode(500);
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
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion
    }
}
