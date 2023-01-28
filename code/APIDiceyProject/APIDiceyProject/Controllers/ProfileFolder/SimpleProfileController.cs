using Api.Services.ProfileFolder;
using APIDiceyProject.Controllers.DiceFolder;

namespace APIDiceyProject.Controllers.ProfileFolder
{
    public class SimpleProfileController : AbstractProfileController
    {
        protected SimpleProfileController(ILogger<AbstractProfileController> logger, IProfileService profileService) : base(logger, profileService)
        {
        }
    }
}
