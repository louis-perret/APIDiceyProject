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

        #region init tests
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
            if (myProfile == null)
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
        #endregion

        #region tests get
        /// <summary>
        /// Jeu de données pour notre test sur la méthode GetProfileByPage.
        /// </summary>
        /// <returns>la liste des params du test</returns>
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

        /// <summary>
        /// Test de la méthode GetProfileByPage
        /// </summary>
        /// <param name="numPage">numéro de la page</param>
        /// <param name="nbByPage">Nombre d'éléments de chaque page</param>
        /// <param name="expectedStatusCode">Code de retour de la requête attendu</param>
        /// <param name="expectedMessageError">Message d'erreur de la requête attendu (string.empty si pas d'erreur)</param>
        /// <param name="answer">Résulatat attendu si résultat attendu</param>
        /// <param name="substr">substring de filtrage</param>
        /// <returns></returns>
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

        /// <summary>
        /// Jeu de données pour notre test sur la méthode GetProfileById.
        /// </summary>
        /// <returns>la liste des params du test</returns>
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

        /// <summary>
        /// Test de la méthode GetProfileById
        /// </summary>
        /// <param name="id">Id du profil recherché</param>
        /// <param name="expectedStatusCode">code de retour attendu de la requête</param>
        /// <param name="expectedMessageError">message d'erreur attendu s'il y en a un, string.empty sinon</param>
        /// <param name="expected">Résultat attendu</param>
        /// <returns></returns>
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
        #endregion

        #region tests remove

        /// <summary>
        /// test de la méthodeRemoveAllProfiles
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task UT_RemoveAllProfiles()
        {
            var result = (await _profileController.RemoveAllProfiles()) as OkResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        /// <summary>
        /// Jeu de données pour notre test sur la méthode RemoveProfileById.
        /// </summary>
        /// <returns>la liste des params du test</returns>
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
                404,
                "No profile with this ID exists",
            };
        }
        /// <summary>
        /// Test de la méthode RemoveProfileById
        /// </summary>
        /// <param name="id">Id du Profile à supprimer</param>
        /// <param name="expectedStatusCode">Code de retour de la méthode attendu</param>
        /// <param name="expectedMessageError">Message d'erreur attendu s'il y en a un, string.empty sinon</param>
        /// <returns></returns>
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
        #endregion

        #region test add
        /// <summary>
        /// Jeu de données pour notre test sur la méthode AddProfile.
        /// </summary>
        /// <returns>la liste des params du test</returns>
        private static IEnumerable<object[]> Test_GetData_AddProfile()
        {
            yield return new object[]
            {
                new DTOs.Profile(Guid.Parse("6af0ae0f-fb3a-4604-810a-9517d8f6a741"),"Loulou","Perret"),
                400,
                "A profile with this Id already exists"
            };
            yield return new object[]
            {
                new DTOs.Profile(Guid.Parse("6af8ae0a-fb3a-4604-810a-9517d8f6a741"),"Mwa","Sémwa"),
                201,
                string.Empty,
            };
        }

        /// <summary>
        /// Test de la méthode AddProfile
        /// </summary>
        /// <param name="prof">le Profile à ajouter</param>
        /// <param name="expectedStatusCode">Code de retour attendu</param>
        /// <param name="expectedMessageError">Message d'erreur attendu s'il y en a un, string.empty sinon</param>
        /// <returns></returns>
        [TestMethod]
        [DynamicData(nameof(Test_GetData_AddProfile), DynamicDataSourceType.Method)]
        public async Task UT_AddProfile(DTOs.Profile prof, int expectedStatusCode, string expectedMessageError)
        {
            if (expectedStatusCode == 201)
            {
                var result = (await _profileController.AddProfile(prof)) as CreatedAtActionResult;
                Assert.IsNotNull(result);
                Assert.AreEqual(expectedStatusCode, result.StatusCode);
                Assert.AreEqual("GetProfileByPage", result.ActionName);
            }
            else
            {
                var result = (await _profileController.AddProfile(prof)) as ObjectResult;
                Assert.IsNotNull(result);
                Assert.AreEqual(expectedStatusCode, result.StatusCode);
                Assert.AreEqual(expectedMessageError, result.Value as string);
            }
        }
        #endregion

        #region tests update
        /// <summary>
        /// Jeu de données pour notre test sur la méthode UpdateProfile.
        /// </summary>
        /// <returns>la liste des params du test</returns>
        private static IEnumerable<object[]> Test_GetData_UpdateProfile()
        {
            yield return new object[]
            {
                new DTOs.Profile(Guid.Parse("6af0ae0f-fb3a-4604-810a-9517d8f6a741"),"Loulou","Perret"),
                204,
                string.Empty
            };
            yield return new object[]
            {
                new DTOs.Profile(Guid.Parse("6af0ae0f-fb3a-4604-810a-9517d8f6a741"),"Louis","Perret"),
                204,
                string.Empty
            };
            yield return new object[]
            {
                new DTOs.Profile(Guid.Parse("6af8ae0f-fb3a-4604-810a-9517d8f6a741"),"Louis","Perret"),
                404,
                "No profile found with this Id",
            };
        }

        /// <summary>
        /// Test de la méthode UpdateProfile
        /// </summary>
        /// <param name="prof">le Profile à mettre à jour</param>
        /// <param name="expectedStatusCode">code de retour attendu de la méthode</param>
        /// <param name="expectedMessageError">message d'erreur attendu s'il y en a un, string.empty sinon</param>
        /// <returns></returns>
        [TestMethod]
        [DynamicData(nameof(Test_GetData_UpdateProfile), DynamicDataSourceType.Method)]
        public async Task UT_UpdateProfile(DTOs.Profile prof, int expectedStatusCode, string expectedMessageError)
        {
            if (expectedStatusCode == 204)
            {
                var result = (await _profileController.UpdateProfile(prof)) as StatusCodeResult;
                Assert.IsNotNull(result);
                Assert.AreEqual(expectedStatusCode, result.StatusCode);
            }
            else
            {
                var result = (await _profileController.UpdateProfile(prof)) as ObjectResult;
                Assert.IsNotNull(result);
                Assert.AreEqual(expectedStatusCode, result.StatusCode);
                Assert.AreEqual(expectedMessageError, result.Value as string);
            }
        }
        #endregion
    }
}
