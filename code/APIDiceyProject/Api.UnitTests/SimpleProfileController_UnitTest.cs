using Api.Model;
using Api.Services.ProfileFolder;
using APIDiceyProject.Controllers;
using APIDiceyProject.Controllers.ProfileFolder;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.UnitTests
{
    /// <summary>
    /// Classes de tests pour notre contrôleur sur les profils.
    /// </summary>
    [TestClass]
    public class SimpleProfileController_UnitTest
    {
        /// <summary>
        /// Contrôleur à tester.
        /// </summary>
        private static AbstractProfileController _profileController;

        /// <summary>
        /// Initialise notre contrôleur avant chaque test.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            var loggerApi = new NullLogger<AbstractProfileController>();
            var service = new Mock<IProfileService>();
            service.Setup(service => service.GetProfilesByPage(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(CreateDatasetProfile()));
            service.Setup(service => service.GetProfileById(It.IsAny<Guid>()))
            .Returns(new Func<Guid, Task<Model.Profile?>>((id) => Task.FromResult(CreateDatasetProfile().Where(profile => profile.Id == id).FirstOrDefault())));
            service.Setup(service => service.RemoveAllProfiles())
                .Returns(new Func<Task<bool>>(() => Task.FromResult(true)));
            service.Setup(service => service.RemoveProfileById(It.IsAny<Guid>()))
                .Returns(new Func<Guid, Task<bool>>(id => Task.FromResult(SimulatedRemoveDiceById(id))));
            service.Setup(service => service.AddProfile(It.IsAny<Model.Profile>()))
                .Returns(new Func<Model.Profile, Task<bool>>(profile => Task.FromResult(SimulatedAddProfile(profile))));
            service.Setup(service => service.UpdateProfile(It.IsAny<Model.Profile>()))
                .Returns(new Func<Model.Profile, Task<bool>>(profile => Task.FromResult(SimulatedAddProfile(profile))));
            service.Setup(service => service.getNbProfiles())
                .Returns(new Func<Task<int>>(() => Task.FromResult(3)));
            _profileController = new SimpleProfileController(loggerApi, service.Object);
        }

        /// <summary>
        /// Simule la méthode AddProfile de notre service profil
        /// </summary>
        /// <param name="profile">Le profil à ajouter</param>
        /// <returns>true si bien ajouté, false sinon</returns>
        private bool SimulatedAddProfile(Profile profile)
        {
            var myProfile = CreateDatasetProfile().Where(innerProfile => innerProfile.Id == profile.Id).FirstOrDefault();
            if (profile == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Simule la méthode RemoveProfile de notre service profil
        /// </summary>
        /// <param name="id">L'ID du profil à supprimer</param>
        /// <returns>true si bien supprimé, false sinon</returns>
        private bool SimulatedRemoveDiceById(Guid id)
        {
                var profile= CreateDatasetProfile().Where(profile => profile.Id == id).FirstOrDefault();
                if (profile == null)
                {
                    return false;
                }

                return true;
        }

        /// <summary>
        /// Méthode qui crée une dataset pour les tests sur le contrôleur de profils
        /// </summary>
        /// <returns>Une Liste de profils</returns>
        private static List<Model.Profile> CreateDatasetProfile()
        {
            return new List<Model.Profile>()
            {
                new SimpleProfile(Guid.Parse("6af0ae0f-fb3a-4604-810a-9517d8f6a741"),"Perret","Loulou"),
                new SimpleProfile(Guid.Parse("76a6268b-cc5f-4971-90d9-079f3720c0b9"),"Grienenberger","Cocome"),
                new SimpleProfile(Guid.Parse("dcce4943-25ea-4929-a23a-427fd0a62cb7"),"Malvezin","Neitah"),
            };
        }
    }
}
