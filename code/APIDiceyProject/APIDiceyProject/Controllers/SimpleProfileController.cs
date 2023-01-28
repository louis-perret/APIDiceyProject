using Api.Services;

namespace APIDiceyProject.Controllers
{
    public class SimpleProfileController : AbstractProfileController
    {
        protected SimpleProfileController(ILogger<AbstractDiceController> logger, IProfileService profileService) : base(logger, profileService)
        {
        }
    }
}
