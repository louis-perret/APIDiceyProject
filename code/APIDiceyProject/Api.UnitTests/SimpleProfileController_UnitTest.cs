using Api.Model;
using Api.Services.ProfileFolder;
using APIDiceyProject.Controllers;
using APIDiceyProject.Controllers.ProfileFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging.Abstractions;
using ModelDTOExtensions;
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
                .Returns(new Func<int,int,string, Task<List<Profile>>>((int numPage, int NbByPage, string subStr) => Task.FromResult(CreateDatasetProfile().Where(profile => (profile.Name+profile.Surname).Contains(subStr) || (profile.Surname + profile.Name).Contains(subStr)).ToList())));

            service.Setup(service => service.GetProfileById(It.IsAny<Guid>()))
            .Returns(new Func<Guid, Task<Model.Profile?>>((id) => Task.FromResult(CreateDatasetProfile().Where(profile => profile.Id == id).FirstOrDefault())));
            
            service.Setup(service => service.RemoveAllProfiles())
                .Returns(new Func<Task<bool>>(() => Task.FromResult(true)));
            
            service.Setup(service => service.RemoveProfileById(It.IsAny<Guid>()))
                .Returns(new Func<Guid, Task<bool>>(id => Task.FromResult(SimulatedRemoveProfileById(id))));
            
            service.Setup(service => service.AddProfile(It.IsAny<Model.Profile>()))
                .Returns(new Func<Model.Profile, Task<Profile?>>(profile => Task.FromResult(SimulatedAddProfile(profile))));
            
            service.Setup(service => service.UpdateProfile(It.IsAny<Model.Profile>()))
                .Returns(new Func<Model.Profile, Task<bool>>(profile => Task.FromResult(SimulatedUpdateProfile(profile))));
            
            service.Setup(service => service.getNbProfiles())
                .Returns(new Func<Task<int>>(() => Task.FromResult(3)));
            _profileController = new SimpleProfileController(loggerApi, service.Object);
        }

        /// <summary>
        /// Simule la méthode UpdateProfile de notre service Profile
        /// </summary>
        /// <param name="profile">Le Profile a update</param>
        /// <returns>true si bien updaté, false sinon </returns>
        private bool SimulatedUpdateProfile(Profile profile)
        {
            var profileUp = CreateDatasetProfile().Where(profileWhere => profileWhere.Id == profile.Id).FirstOrDefault();
            if (profileUp == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Simule la méthode AddProfile de notre service profil
        /// </summary>
        /// <param name="profile">Le profil à ajouter</param>
        /// <returns>true si bien ajouté, false sinon</returns>
        private Profile? SimulatedAddProfile(Profile profile)
        {
            var myProfile = CreateDatasetProfile().Where(innerProfile => innerProfile.Id == profile.Id).FirstOrDefault();
            if (profile == null)
            {
                return profile;
            }

            return null;
        }

        /// <summary>
        /// Simule la méthode RemoveProfile de notre service profil
        /// </summary>
        /// <param name="id">L'ID du profil à supprimer</param>
        /// <returns>true si bien supprimé, false sinon</returns>
        private bool SimulatedRemoveProfileById(Guid id)
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


        /// <summary>
        /// Jeu de données pour notre test sur la méthode GetProfileByPage.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<object[]> Test_GetData_GetProfileByPage()
        {
            yield return new object[]
            {
                1, 3, 200, string.Empty, new
                    {
                        Profiles = CreateDatasetProfile().ToDTO(),
                        PageNumber = 1,
                        NbElementsByPage= 3,
                        numberOfElements = 3
                    }, ""
            };
            yield return new object[]
            {
                -1, 3, 400, "Please give a page number and a number of elements by page both superior to 0", null, ""
            };
            yield return new object[]
            {
                1, -3, 400, "Please give a page number and a number of elements by page both superior to 0", null, ""
            };
            yield return new object[]
            {
                2, 3, 200, string.Empty, new
                    {
                        Profiles = new List<Api.DTOs.Profile>(),
                        PageNumber = 2,
                        NbElementsByPage= 3,
                        numberOfElements = 3
                    }, ""
            };
            yield return new object[]
            {
                1, 3, 200, string.Empty, new
                    {
                        Profiles = CreateDatasetProfile().ToDTO().Where(profile => profile.Surname.Contains("Nei")).ToList(),
                        PageNumber = 1,
                        NbElementsByPage= 3,
                        numberOfElements = 3
                    }, "Nei"
            };
            yield return new object[]
            {
                1, 3, 200, string.Empty, new
                    {
                        Profiles = CreateDatasetProfile().ToDTO().Where(profile => profile.Surname.Contains("AUYFEUYAFDYUAFE")).ToList(),
                        PageNumber = 1,
                        NbElementsByPage= 3,
                        numberOfElements = 3
                    }, "AUYFEUYAFDYUAFE"
            };
        }

        
        [TestMethod]
        [DynamicData(nameof(Test_GetData_GetProfileByPage), DynamicDataSourceType.Method)]
        public async Task UT_GetProfileByPage(int numPage, int nbByPage, int expectedStatusCode, string expectedMessageError, dynamic answer,string substr) 
        {

            var result = (await _profileController.GetProfileByPage(numPage, nbByPage, substr)) as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStatusCode, result.StatusCode);

            if (result.StatusCode == 400)
            {
                Assert.AreEqual(expectedMessageError, result.Value as string);
            }
            else
            {
                Assert.AreEqual(result.Value?.ToString(), answer.ToString());
            }
        }


        private static IEnumerable<object[]> Test_GetData_GetProfileById()
        {
            yield return new object[]
            {
                Guid.Parse("6af0ae0f-fb3a-4604-810a-9517d8f6a741"), 
                200,
                string.Empty,
                new Model.SimpleProfile(Guid.Parse("6af0ae0f-fb3a-4604-810a-9517d8f6a741"),"Louis","Perret")
            };
            yield return new object[]
            {
                Guid.Parse("6af0ae0f-ab3a-4604-810a-9517d8f6a741"),
                404,
                "There is no profile with this ID",
                null
            };
        }

        [TestMethod]
        [DynamicData(nameof(Test_GetData_GetProfileById), DynamicDataSourceType.Method)]
        public async Task UT_GetProfileById(Guid id, int expectedStatusCode, string expectedMessageError, Model.Profile expected)
        {
            var result = (await _profileController.GetProfileById(id)) as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStatusCode, result.StatusCode);

            if (result.StatusCode == 404)
            {
                Assert.AreEqual(expectedMessageError, result.Value as string);
            }
            else
            {
                Assert.AreEqual((result.Value as DTOs.Profile)?.ToModel(), expected);
            }
        }

        [TestMethod]
        public async Task UT_RemoveAllProfiles()
        {
            var result = (await _profileController.RemoveAllProfiles()) as OkResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        private static IEnumerable<object[]> Test_GetData_RemoveProfileById()
        {
            yield return new object[]
            {
                Guid.Parse("6af0ae0f-fb3a-4604-810a-9517d8f6a741"),
                200,
                string.Empty
            };
            yield return new object[]
            {
                Guid.Parse("6af0ae0f-ab3a-4604-810a-9517d8f6a741"),
                400,
                "No profile with this ID exists",
            };
        }
        [TestMethod]
        [DynamicData(nameof(Test_GetData_RemoveProfileById), DynamicDataSourceType.Method)]
        public async Task UT_RemoveProfileById(Guid id, int expectedStatusCode, string expectedMessageError)
        {
            if (expectedStatusCode == 200)
            {
                var result = (await _profileController.RemoveProfileById(id)) as OkResult;
                Assert.IsNotNull(result);
                Assert.AreEqual(expectedStatusCode, result.StatusCode);
            }
            else { 
                var result = (await _profileController.RemoveProfileById(id)) as ObjectResult;
                Assert.IsNotNull(result);
                Assert.AreEqual(expectedStatusCode, result.StatusCode);
                Assert.AreEqual(expectedMessageError, result.Value as string);
            }
        }
    }
}
