using Api.Services.ProfileFolder;
using APIDiceyProject.Controllers.DiceFolder;

namespace APIDiceyProject.Controllers.ProfileFolder
{
    public class SimpleProfileController : AbstractProfileController
    {
        public SimpleProfileController(ILogger<AbstractProfileController> logger, IProfileService profileService) : base(logger, profileService)
        {
        }
    }
}
