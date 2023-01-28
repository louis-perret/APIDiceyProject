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
        public async Task<IActionResult> GetProfileByPage(int numPage, int nbByPage)
        {
            var modelProfiles = await _profileService.GetProfilesByPage(numPage, nbByPage);
            
            return Ok(modelProfiles.ToDTO());
        }

        [HttpGet("{id}")]
        async public Task<IActionResult> GetProfileById(Guid id)
        {
            try
            {
                var profile = await _profileService.GetProfileById(id);
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
        async public Task<IActionResult> RemoveAllProfiles()
        {
            if (await _profileService.RemoveAllProfiles()) return Ok();

            // logger
            return StatusCode(500);
        }

        [HttpDelete("{id}")]
        async public Task<IActionResult> RemoveProfileById(Guid id)
        {
            try
            {
                if (await _profileService.RemoveProfileById(id))
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
        async public Task<IActionResult> AddProfile(Api.DTOs.Profile profile)
        {
            try
            {
                var proAdded = await _profileService.AddProfile(profile.ToModel());
                if (proAdded != null) return CreatedAtAction(nameof(GetProfileById), profile.Id, profile);
                return BadRequest("A profile with this Id already exists");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        async public Task<IActionResult> UpdateProfile(Api.DTOs.Profile profile)
        {
            try
            {
                if(await _profileService.UpdateProfile(profile.ToModel())) return StatusCode(204);
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
