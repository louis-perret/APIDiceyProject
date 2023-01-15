using Api.Model;
using Api.Services;
using APIDiceyProject.Controllers.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Api.UnitTests;

[TestClass]
public class SimpleDiceController_UnitTest
{

    private static AbstractDiceController _diceController;

    [TestInitialize]
    private static void Init()
    {
        var logger = new NullLogger<AbstractDiceController>();
        var service = new Mock<AbstractDiceService>();
        service.Setup(service => service.GetDices())
            .Returns(Task.FromResult<List<Model.Dice>>(CreateDatasetDice()
            ));

        _diceController = new SimpleDiceController(logger, service.Object);
    }

    [TestMethod]
    public async void UT_GetDices()
    {
        var result = (await _diceController.GetDices()) as ObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        var actualListDice = result.Value as List<Model.Dice>;
        var expectedListDice = CreateDatasetDice();
        Assert.IsNotNull(actualListDice);
        Assert.AreEqual(expectedListDice.Count, actualListDice.Count);
        bool areListDiceEqual = true;
        foreach(var dice in expectedListDice)
        {
            if (!actualListDice.Contains(dice))
            {
                areListDiceEqual = false;
            }
        }
        Assert.IsTrue(areListDiceEqual);
    }

    private static List<Dice> CreateDatasetDice()
    {
        return new List<Model.Dice>()
            {
                new SimpleDice(2),
                new SimpleDice(3),
                new SimpleDice(4),
                new SimpleDice(5),
            };
    }
}
