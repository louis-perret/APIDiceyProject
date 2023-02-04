using Api.Services.ProfileFolder;
using APIDiceyProject.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.UnitTests
{
    [TestClass]
    public class SimpleProfileController_UnitTest
    {
        [TestInitialize]
        public void Init()
        {
            /*var loggerApi = new NullLogger<AbstractProfileController>();
            var service = new Mock<IProfileService>();
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
            _diceController = new SimpleDiceController(loggerApi, service.Object);*/
        }
    }
}
