using Api.Model;
using Api.Services;
using APIDiceyProject.Controllers.V1;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using ModelDTOExtensions;
using Moq;

namespace Api.UnitTests;

[TestClass]
public class SimpleDiceController_UnitTest
{

    private static AbstractDiceController _diceController;

    [TestInitialize]
    public void Init()
    {
        var loggerApi = new NullLogger<AbstractDiceController>();
        var service = new Mock<IDiceService>();
        service.Setup(service => service.GetDices())
            .Returns(Task.FromResult(CreateDatasetDice()));
        service.Setup(service => service.GetDiceById(It.IsAny<int>()))
        .Returns(new Func<int, Task<Dice?>>((id) => Task.FromResult(CreateDatasetDice().Where(dice => dice.NbFaces == id).FirstOrDefault())));
        service.Setup(service => service.RemoveAllDices())
            .Returns(new Func<Task<bool>>(() => Task.FromResult(true)));
        service.Setup(service => service.RemoveDiceById(It.IsAny<int>()))
            .Returns(new Func<int, Task<bool>>(id => Task.FromResult(SimulatedRemoveDiceById(id))));
        _diceController = new SimpleDiceController(loggerApi, service.Object);
    }

    [TestMethod]
    public async Task UT_GetDices()
    {
        var result = (await _diceController.GetDices()) as ObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        var actualListDice = result.Value as List<DTOs.Dice>;
        var expectedListDice = CreateDatasetDice().ToDTO();
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

    private static IEnumerable<object[]> Test_GetData_GetDiceById()
    {
        yield return new object[]
        {
            2, 200, string.Empty, new DTOs.Dice(2)
        };
        yield return new object[]
        {
            1, 404, "There is already a dice with this number of faces", null
        };
    }

    [TestMethod]
    [DynamicData(nameof(Test_GetData_GetDiceById), DynamicDataSourceType.Method)]
    public async Task UT_GetDiceById(int id, int expectedStatusCode, string expectedMessageError, DTOs.Dice expectedDice)
    {
        var result = (await _diceController.GetDiceById(id)) as ObjectResult;
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);

        if(result.StatusCode == 404)
        {
            Assert.AreEqual(expectedMessageError, result.Value as string);
        }
        else
        {
            Assert.AreEqual(expectedDice, result.Value as DTOs.Dice);
        }
    }

    private static IEnumerable<object[]> Test_GetData_RemoveAllDices()
    {
        /*yield return new object[]
        {
            500, "Could not delete dices."
        };*/

        yield return new object[]
        {
            200, string.Empty
        };
    }

    [TestMethod]
    [DynamicData(nameof(Test_GetData_RemoveAllDices), DynamicDataSourceType.Method)]
    public async Task UT_RemoveAllDices(int expectedStatusCode, string expectedMessageError)
    {
        var result = (await _diceController.RemoveAllDices()) as OkResult; // replace with ObjectResult later
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);

        /*if(result.StatusCode == 500)
        {
            Assert.AreEqual(expectedMessageError, result.Value as string);
        }*/
    }

    private static IEnumerable<object[]> Test_GetData_RemoveDiceById()
    {
        yield return new object[]
        {
            2, 200, string.Empty, true, false
        };

        yield return new object[]
        {
            1, 400, "No dice with this number of faces exists", false, false
        };
        
        yield return new object[]
        {
            -1, 500, "Could not remove the dice with the given id from the database", false, true
        };
    }

    [TestMethod]
    [DynamicData(nameof(Test_GetData_RemoveDiceById), DynamicDataSourceType.Method)]
    public async Task UT_RemoveDiceById(int id, int expectedStatusCode, string expectedMessageError, bool isOkResult, bool isProblemResult)
    {
        var result = await _diceController.RemoveDiceById(id);
        
        if (isOkResult)
        {
            var res = result as OkResult;
            Assert.IsNotNull(res);
            Assert.AreEqual(expectedStatusCode, res.StatusCode);
        }
        else
        {
            var res = result as ObjectResult;
            Assert.IsNotNull(res);
            Assert.AreEqual(expectedStatusCode, res.StatusCode);
            if (isProblemResult)
            {
                Assert.AreEqual(expectedMessageError, (res.Value as ProblemDetails).Detail);
            }
            else
            {
                Assert.AreEqual(expectedMessageError, res.Value as string);
            }
        }

    }
    private static List<Model.Dice> CreateDatasetDice()
    {
        return new List<Model.Dice>()
            {
                new SimpleDice(2),
                new SimpleDice(3),
                new SimpleDice(4),
                new SimpleDice(5),
                new SimpleDice(6),
            };
    }

    private static bool SimulatedRemoveDiceById(int id)
    {
        if(id > 0)
        {
            var dice = CreateDatasetDice().Where(dice => dice.NbFaces == id).FirstOrDefault();
            if(dice == null)
            {
                return false;
            }

            return true;
        }
        else
        {
            throw new EntityFrameworkException("");
        }
    }
}
