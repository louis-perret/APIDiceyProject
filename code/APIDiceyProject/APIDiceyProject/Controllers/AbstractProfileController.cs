using Api.Services;
using Microsoft.AspNetCore.Mvc;
using ModelDTOExtensions;

namespace APIDiceyProject.Controllers
{
    /// <summary>
    /// Controller abstrait pour les Profile
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AbstractProfileController : ControllerBase
    {
        #region attributs
        /// <summary>
        /// Service contenant la logique CRUD des Profile.
        /// </summary>
        private IProfileService _profileService;

        /// <summary>
        /// Logger de la classe.
        /// </summary>
        protected ILogger<AbstractDiceController>? _logger;
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur complet.
        /// Utilise le constructeur à un argument.
        /// </summary>
        /// <param name="logger"> Logger de cette classe. </param>
        /// <param name="profileService"> Service contenant la logique CRUD des Profile. </param>
        protected AbstractProfileController(ILogger<AbstractDiceController> logger, IProfileService profileService)
        {
            _profileService = profileService;
            _logger = logger;
        }
        #endregion

        #region routes
        [HttpGet]
        public IActionResult GetProfileByPage(int numPage, int nbByPage)
        {
            var modelProfiles = _profileService.GetProfilesByPage(numPage, nbByPage);
            return Ok(modelProfiles.ToDTO());
        }

        [HttpGet("{id}")]
        public IActionResult GetProfileById(Guid id)
        {
            try
            {
                var profile = _profileService.GetProfileById(id);
                if (profile == null)
                {
                    return NotFound("There is no profile with this ID");
                }
                return Ok(profile.ToDto());
            }
            #region exceptions
            catch (Exception e)
            {
                return StatusCode(500, "An error has occured");
            }
            #endregion
        }

        [HttpDelete]
        public IActionResult RemoveAllProfiles()
        {
            if (_profileService.RemoveAllProfiles()) return Ok();

            // logger
            return StatusCode(500);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveProfileById(Guid id)
        {
            try
            {
                if (_profileService.RemoveProfileById(id))
                    return Ok();
                else
                    return BadRequest("No profile with this ID exists");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult AddProfile(Api.DTOs.Profile profile)
        {
            try
            {
                var proAdded = _profileService.AddProfile(profile.ToModel());
                if (proAdded != null) return CreatedAtAction(nameof(GetProfileById), profile.Id, profile);
                return BadRequest("A profile with this Id already exists");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult UpdateProfile(Api.DTOs.Profile profile)
        {
            try
            {
                if(_profileService.UpdateProfile(profile.ToModel())) return StatusCode(204);
                return BadRequest("No profile found with this Id");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    #endregion
    }
}
