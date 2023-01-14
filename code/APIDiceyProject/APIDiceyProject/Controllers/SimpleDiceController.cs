using Api.Services;

namespace APIDiceyProject.Controllers
{
    /// <summary>
    /// Implémentation simple de AbstractDiceController.
    /// </summary>
    public class SimpleDiceController : AbstractDiceController
    {
        #region constructeurs
        public SimpleDiceController(ILogger<AbstractDiceController> logger, IDiceService diceServce) : base(logger, diceServce)
        {
        }
        #endregion
    }
}
