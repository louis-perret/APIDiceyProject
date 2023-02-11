using Api.Model;
using Api.Services.DiceFolder;
using APIDiceyProject.Controllers.DiceFolder;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using ModelDTOExtensions;
using Moq;

namespace Api.UnitTests;

/// <summary>
/// Classes de tests pour notre contrôleur sur les dés.
/// </summary>
[TestClass]
public class SimpleDiceController_UnitTest
{

    /// <summary>
    /// Contrôleur à tester.
    /// </summary>
    private static AbstractDiceController _diceController;

    /// <summary>
    /// Initialise notre contrôleur avant chaque test.
    /// </summary>
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
        service.Setup(service => service.AddDice(It.IsAny<Model.Dice>()))
            .Returns(new Func<Model.Dice, Task<bool>>(dice => Task.FromResult(SimulatedAddDice(dice))));
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

    /// <summary>
    /// Jeu de données pour notre test sur la méthode GetDiceById.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Jeu de données pour notre test sur la méthode RemoveAllDices.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Jeu de données pour notre test sur la méthode RemoveDiceById.
    /// </summary>
    /// <returns></returns>
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
                Assert.AreEqual(expectedMessageError, (res.Value as ProblemDetails)?.Detail);
            }
            else
            {
                Assert.AreEqual(expectedMessageError, res.Value as string);
            }
        }

    }

    /// <summary>
    /// Jeu de données pour notre test sur la méthode AddDice.
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<object[]> Test_GetData_AddDice()
    {
        yield return new object[]
        {
            new DTOs.Dice(1), 201, string.Empty, true, false
        };

        yield return new object[]
        {
            new DTOs.Dice(2), 400, "No dice with this number of faces exists", false, false
        };

        yield return new object[]
        {
            new DTOs.Dice(-1), 500, "Could not insert given object in database.", false, true
        };
    }

    [TestMethod]
    [DynamicData(nameof(Test_GetData_AddDice), DynamicDataSourceType.Method)]
    public async Task UT_AddDice(DTOs.Dice dice, int expectedStatusCode, string expectedMessageError, bool isOkResult, bool isProblemResult)
    {
        var result = await _diceController.AddDice(dice) as ObjectResult;
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedStatusCode, result.StatusCode);

        if (isOkResult)
        {
            var res = result as CreatedAtActionResult;
            Assert.IsNotNull(res);
            Assert.AreEqual(dice, res.Value as DTOs.Dice);
            Assert.AreEqual(nameof(AbstractDiceController.AddDice), res.ActionName);
        }
        else
        {
            if (isProblemResult)
            {
                Assert.AreEqual(expectedMessageError, (result.Value as ProblemDetails)?.Detail);
            }
            else
            {
                Assert.AreEqual(expectedMessageError, result.Value as string);
            }
        }

    }

    /// <summary>
    /// Créer une liste de données pour venir simuler notre base de données.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Simule la méthode RemoveDiceById de notre DiceService.
    /// </summary>
    /// <param name="id">Nombre de faces du dé à supprimer.</param>
    /// <returns>True si bien supprimé, false autrement.</returns>
    /// <exception cref="EntityFrameworkException">Si nombre de faces négatif (simule une erreur côté bd).</exception>
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

    /// <summary>
    /// Simule la méthode AddDice de notre DiceService.
    /// </summary>
    /// <param name="id">Nombre de faces du dé à ajouter.</param>
    /// <returns>True si bien ajouté, false autrement.</returns>
    /// <exception cref="EntityFrameworkException">Si nombre de faces négatif (simule une erreur côté bd).</exception>
    private static bool SimulatedAddDice(Model.Dice diceToAdd)
    {
        if (diceToAdd.NbFaces > 0)
        {
            var dice = CreateDatasetDice().Where(dice => dice.NbFaces == diceToAdd.NbFaces).FirstOrDefault();
            if (dice == null)
            {
                return true;
            }

            return false;
        }
        else
        {
            throw new EntityFrameworkException("");
        }
    }
}
