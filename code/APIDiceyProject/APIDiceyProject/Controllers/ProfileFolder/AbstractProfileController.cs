using Api.Services.ProfileFolder;
using Microsoft.AspNetCore.Mvc;
using ModelDTOExtensions;

namespace APIDiceyProject.Controllers
{
    /// <summary>
    /// Controller abstrait pour les Profile
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Profile")]
    public abstract class AbstractProfileController : ControllerBase
    {
        #region attributs
        /// <summary>
        /// Service contenant la logique CRUD des Profile.
        /// </summary>
        private IProfileService _profileService;

        /// <summary>
        /// Logger de la classe.
        /// </summary>
        protected ILogger<AbstractProfileController>? _logger;
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur complet.
        /// Utilise le constructeur à un argument.
        /// </summary>
        /// <param name="logger"> Logger de cette classe. </param>
        /// <param name="profileService"> Service contenant la logique CRUD des Profile. </param>
        protected AbstractProfileController(ILogger<AbstractProfileController> logger, IProfileService profileService)
        {
            _profileService = profileService;
            _logger = logger;
        }
        #endregion

        #region routes
        [HttpGet]
        public async Task<IActionResult> GetProfileByPage(int numPage = 1, int nbByPage = 10, string subString = "")
        {
            try
            {
                _logger?.Log(LogLevel.Information, "GetProfileByPage : Entrée dans la méthode avec numPage = {0}, nbByPage = {1}, subString = {2}", numPage, nbByPage, subString);
                if (numPage > 0 && nbByPage > 0)
                {
                    var modelProfiles = await _profileService.GetProfilesByPage(numPage, nbByPage, subString);

                    return Ok(modelProfiles.ToDTO());
                }

                return BadRequest("Please give a page number and a number of elements by page both superior to 0");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.StackTrace);
                return StatusCode(500,ex.Message);
            }
        }


        [HttpGet("{id}")]
        async public Task<IActionResult> GetProfileById(Guid id)
        {
            _logger?.Log(LogLevel.Information, "GetProfileById : Entrée dans la méthode avec id = {}", id);
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
                _logger?.LogError(e.StackTrace);
                return StatusCode(500, e.Message);
            }
            #endregion
        }

        [HttpDelete]
        async public Task<IActionResult> RemoveAllProfiles()
        {
            try
            {
                _logger?.Log(LogLevel.Information, "RemoveAllProfiles : Entrée dans la méthode");
                await _profileService.RemoveAllProfiles(); 
                return Ok();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.StackTrace);
                return StatusCode(500,ex.Message);
            }
        }

        [HttpDelete("{id}")]
        async public Task<IActionResult> RemoveProfileById(Guid id)
        {
            _logger?.Log(LogLevel.Information, "RemoveProfileById : Entrée dans la méthode avec id = {}",id);
            try
            {
                if (await _profileService.RemoveProfileById(id))
                    return Ok();
                else
                    return BadRequest("No profile with this ID exists");
            }
            catch (Exception e)
            {
                _logger?.LogError(e.StackTrace);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        async public Task<IActionResult> AddProfile(Api.DTOs.Profile profile)
        {
            _logger?.Log(LogLevel.Information,"AddProfile : Entrée dans la méthode avec profile = ",profile.ToString());
            try
            {
                var proAdded = await _profileService.AddProfile(profile.ToModel());
                if (proAdded != null) return CreatedAtAction(nameof(GetProfileById), profile.Id, profile);
                return BadRequest("A profile with this Id already exists");
            }
            catch (Exception e)
            {
                _logger?.LogError(e.StackTrace);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        async public Task<IActionResult> UpdateProfile(Api.DTOs.Profile profile)
        {
            _logger?.Log(LogLevel.Information, "UpdateProfile : Entrée dans la méthode avec profile = ", profile.ToString());
            try
            {
                if(await _profileService.UpdateProfile(profile.ToModel())) return StatusCode(204);
                return BadRequest("No profile found with this Id");
            }
            catch (Exception e)
            {
                _logger?.LogError(e.StackTrace);
                return StatusCode(500, e.Message);
            }
        }
    #endregion
    }
}
