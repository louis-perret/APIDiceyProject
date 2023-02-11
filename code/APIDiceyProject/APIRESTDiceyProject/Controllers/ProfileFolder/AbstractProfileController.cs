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
        private readonly IProfileService _profileService;

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
                    _logger?.Log(LogLevel.Information, "GetProfileByPage : Requête en base faite avec numPage = {0}, nbByPage = {1}, subString = {2}", numPage, nbByPage, subString);

                    return Ok(new
                    {
                        Profiles = modelProfiles.ToDTO(),
                        PageNumber = numPage,
                        NbElementsByPage=nbByPage,
                        numberOfElements = _profileService.getNbProfiles().Result
                    });
                }
                _logger?.Log(LogLevel.Information, "GetProfileByPage : Problème dans les arguments avec numPage = {0}, nbByPage = {1}, subString = {2}", numPage, nbByPage, subString);

                return BadRequest("Please give a page number and a number of elements by page both superior to 0");
            }
            #region Exception 
            catch (Exception ex)
            {
                _logger?.LogError("GetProfileByPage avec numPage = {0}, nbByPage = {1}, subString = {2} : " + ex.StackTrace, numPage, nbByPage, subString);
                return Problem(ex.Message, statusCode:500);
            }
            #endregion
        }


        [HttpGet("{id}")]
        async public Task<IActionResult> GetProfileById(Guid id)
        {
            _logger?.Log(LogLevel.Information, "GetProfileById : Entrée dans la méthode avec id = {0}", id);
            try
            {
                var profile = await _profileService.GetProfileById(id);
                if (profile == null)
                {
                    _logger?.Log(LogLevel.Information, "GetProfileById : Aucun profil n'a été trouvé avec id = {0}", id);

                    return NotFound("There is no profile with this ID");
                }
                _logger?.Log(LogLevel.Information, "GetProfileById Retour du profil avec id = {0} : ", id);

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
                if (await _profileService.RemoveAllProfiles())
                {
                    _logger?.Log(LogLevel.Information, "RemoveAllProfiles : Tous les profils ont été supprimés");
                    return Ok();
                }
                _logger?.Log(LogLevel.Information, "RemoveAllProfiles : Un problème a empêché de supprimer tous les profils");
                return Problem("The profiles couldn't be removed");
            }
            catch (Exception ex)
            {
                _logger?.LogError("RemoveAllProfiles : " + ex.StackTrace);
                return Problem(ex.Message,statusCode:500) ;
            }
        }

        [HttpDelete("{id}")]
        async public Task<IActionResult> RemoveProfileById(Guid id)
        {
            _logger?.Log(LogLevel.Information, "RemoveProfileById : Entrée dans la méthode avec id = {0}",id);
            try
            {
                if (await _profileService.RemoveProfileById(id))
                {
                    _logger?.Log(LogLevel.Information, "RemoveProfileById : Profil supprimé avec id = {0}", id);
                    return Ok();
                }
                else
                {
                    _logger?.Log(LogLevel.Information, "RemoveProfileById : Aucun profil supprimé car aucun avec id = {0}", id);
                    return BadRequest("No profile with this ID exists");
                }
            }
            catch (Exception e)
            {
                _logger?.LogError("RemoveProfileById avec id = {0} : " + e.StackTrace,id);
                return Problem(e.Message, statusCode:500) ;
            }
        }

        [HttpPost]
        async public Task<IActionResult> AddProfile(Api.DTOs.Profile profile)
        {
            _logger?.Log(LogLevel.Information,"AddProfile : Entrée dans la méthode avec profile = {0}",profile.ToString());
            try
            {
                var proAdded = await _profileService.AddProfile(profile.ToModel());
                if (proAdded != null) 
                {
                    _logger?.Log(LogLevel.Information, "AddProfile : Profile ajouté avec profile = {0}", profile.ToString());

                    return CreatedAtAction(nameof(GetProfileByPage), proAdded.Id, proAdded);
                }
                _logger?.Log(LogLevel.Information, "AddProfile : Profile non ajouté car existe déjà avec id = {0}", profile.Id);

                return BadRequest("A profile with this Id already exists");
            }
            catch (Exception e)
            {
                _logger?.LogError("AddProfile avec profile = {0} : " + e.StackTrace, profile.ToString());
                return Problem(e.Message, statusCode: 500);
            }
        }

        [HttpPut]
        async public Task<IActionResult> UpdateProfile(Api.DTOs.Profile profile)
        {
            _logger?.Log(LogLevel.Information, "UpdateProfile : Entrée dans la méthode avec profile = {0}", profile.ToString());
            try
            {
                if (await _profileService.UpdateProfile(profile.ToModel()))
                {
                    _logger?.Log(LogLevel.Information, "UpdateProfile : Update du Profile avec profile = {0}", profile.ToString());
                    return StatusCode(204);
                }
                _logger?.Log(LogLevel.Information, "UpdateProfile : Profile pas ajouté car aucun profile en base avec id = {0}", profile.Id);

                return BadRequest("No profile found with this Id");
            }
            catch (Exception e)
            {
                _logger?.LogError("UpdateProfile avec profile = {0} : " + e.StackTrace, profile.ToString());
                return Problem(e.Message, statusCode: 500);
            }
        }
    #endregion
    }
}
