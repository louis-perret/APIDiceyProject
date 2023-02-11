using Api.Model;
using Api.Model.Throw;
using Api.Services.DiceFolder;
using Api.Services.ThrowService;
using ApiGRPCDiceyProject;
using ApiGRPCDiceyProject.Services;
using Grpc.Core;
using Grpc.Core.Testing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Throw = Api.Model.Throw.Throw;

namespace Api.UnitTests
{
    [TestClass]
    public class GRPCServiceThrow_UnitTest
    {

        /// <summary>
        /// Contrôleur à tester.
        /// </summary>
        private static GRPCServiceThrow _throwController;

        /// <summary>
        /// Initialise notre contrôleur avant chaque test.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            var loggerApi = new NullLogger<GRPCServiceThrow>();
            var service = new Mock<IThrowService>();
            service.Setup(service => service.GetThrowById(It.IsAny<Guid>()))
                .Returns(new Func<Guid, Task<Throw?>>((id) => Task.FromResult(CreateDatasetThrow().Where(t => t.Id == id).FirstOrDefault())));
            service.Setup(service => service.GetThrowByProfileId(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new Func<Guid, int, int, Task<List<Model.Throw.Throw>>>((id, numPage, nbByPage) => Task.FromResult(SimulatedGetThrowByProfileId(id, numPage, nbByPage))));
            service.Setup(service => service.AddThrow(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>()))
                .Returns(new Func<int, int, Guid, Task<Guid>>((result, idDice, idProfile) => Task.FromResult(SimulatedAddThrow(result, idDice, idProfile))));
            service.Setup(service => service.RemoveThrow(It.IsAny<Guid>()))
                .Returns(new Func<Guid, Task<bool>>(id => Task.FromResult(SimulatedRemoveThrow(id))));
            _throwController = new GRPCServiceThrow(loggerApi, service.Object);
        }

        /// <summary>
        /// Jeu de données pour notre test sur la méthode GetThrowById
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<object[]> Test_GetData_GetThrowById()
        {
            yield return new object[]
            {
                "aa6f9111-b174-4064-814b-ce7eb4169e80", 1, 2, "cc6f9111-b174-4064-814b-ce7eb4169e80", false
            };
            yield return new object[]
            {
                "ab6f9111-b174-4064-814b-ce7eb4169e80", 1, 2, "cc6f9111-b174-4064-814b-ce7eb4169e80", true
            };
            yield return new object[]
            {
                "ba6f964-814b-ce7eb4169e80", 1, 2, "cc6f9111-b174-4064-814b-ce7eb4169e80", true
            };
        }

        [TestMethod]
        [DynamicData(nameof(Test_GetData_GetThrowById), DynamicDataSourceType.Method)]
        public void UT_GetThrowById(string id, int expectedResult, int expectedIdDice, string expectedIdProfile, bool isThrowRpcException)
        {
            var context = TestServerCallContext.Create(method: nameof(_throwController.GetThrowById)
                                        , host: "localhost"
                                        , deadline: DateTime.Now.AddMinutes(30)
                                        , requestHeaders: new Metadata()
                                        , cancellationToken: CancellationToken.None
                                        , peer: null
                                        , authContext: null
                                        , contextPropagationToken: null
                                        , writeHeadersFunc: (metadata) => Task.CompletedTask
                                        , writeOptionsGetter: () => new WriteOptions()
                                        , writeOptionsSetter: (writeOptions) => { });

            if (isThrowRpcException)
            {
                Assert.ThrowsException<AggregateException>(() => _throwController.GetThrowById(new ApiGRPCDiceyProject.RequestGetThrowById() { SearchedId = id }, context).Result);
            }
            else
            {
                var result = _throwController.GetThrowById(new ApiGRPCDiceyProject.RequestGetThrowById() { SearchedId = id }, context).Result;
                Assert.IsNotNull(result);
                Assert.AreEqual(id, result.ThrowId);
                Assert.AreEqual(expectedResult, result.Result);
                Assert.AreEqual(expectedIdDice, result.IdDice);
                Assert.AreEqual(expectedIdProfile, result.ProfileId);
            }
        }

        private static IEnumerable<object[]> Test_GetData_GetThrowByProfileId()
        {
            yield return new object[]
            {
                "cc6f9111-b174-4064-814b-ce7eb4169e80", 1, 2, false, 2
            };

            yield return new object[]
           {
                "ac6f9111-b174-4064-814b-ce7eb4169e80", 1, 2, false, 0
           };

            yield return new object[]
            {
                "fa6f9111-b174-4064-814b-ce7eb4169e80", 1, 2, true, 0
            };

            yield return new object[]
            {
                "cc6f9111-b174-4064-814b-ce7eb4169e80", -1, 2, true, 0
            };

            yield return new object[]
            {
                "cc6f9111-b174-4064-814b-ce7eb4169e80", 1, -2, true, 0
            };

            yield return new object[]
            {
                "6f9111-b174-4064-814b-ce7eb4169e80", 1, 2, true, 0
            };
        }

        [TestMethod]
        [DynamicData(nameof(Test_GetData_GetThrowByProfileId), DynamicDataSourceType.Method)]
        public void GetThrowByProfileId(string idProfile, int numPage, int nbByPage, bool isThrowRpcException, int expectedCount)
        {
            var context = TestServerCallContext.Create(method: nameof(_throwController.GetThrowById)
                                        , host: "localhost"
                                        , deadline: DateTime.Now.AddMinutes(30)
                                        , requestHeaders: new Metadata()
                                        , cancellationToken: CancellationToken.None
                                        , peer: null
                                        , authContext: null
                                        , contextPropagationToken: null
                                        , writeHeadersFunc: (metadata) => Task.CompletedTask
                                        , writeOptionsGetter: () => new WriteOptions()
                                        , writeOptionsSetter: (writeOptions) => { });

            if (isThrowRpcException)
            {
                Assert.ThrowsException<AggregateException>(() => _throwController.GetThrowByProfileId(new RequestGetThrowByProfileId() { ProfileId = idProfile, NbElements = nbByPage, NumPages = numPage }, context).Result);
            }
            else
            {
                var result = _throwController.GetThrowByProfileId(new RequestGetThrowByProfileId() { ProfileId = idProfile, NbElements = nbByPage, NumPages = numPage }, context).Result;
                Assert.IsNotNull(result);
                Assert.AreEqual(expectedCount, result.Throws.Count);
            }
        }

        private static IEnumerable<object[]> Test_GetData_AddThrow()
        {
            yield return new object[]
            {
                1, 2, "cc6f9111-b174-4064-814b-ce7eb4169e80", false
            };

            yield return new object[]
            {
                0, 2, "cc6f9111-b174-4064-814b-ce7eb4169e80", true
            };

            yield return new object[]
            {
                3, 2, "cc6f9111-b174-4064-814b-ce7eb4169e80", true
            };

            yield return new object[]
            {
                1, 0, "cc6f9111-b174-4064-814b-ce7eb4169e80", true
            };

            yield return new object[]
            {
                1, 2, "cc6f9111-b174-4064-814e7eb4169e80", true
            };
        }

        [TestMethod]
        [DynamicData(nameof(Test_GetData_AddThrow), DynamicDataSourceType.Method)]
        public void UT_AddThrow(int result, int nbFacesDe, string profileId, bool isThrowRpcException)
        {
            var context = TestServerCallContext.Create(method: nameof(_throwController.GetThrowById)
                                       , host: "localhost"
                                       , deadline: DateTime.Now.AddMinutes(30)
                                       , requestHeaders: new Metadata()
                                       , cancellationToken: CancellationToken.None
                                       , peer: null
                                       , authContext: null
                                       , contextPropagationToken: null
                                       , writeHeadersFunc: (metadata) => Task.CompletedTask
                                       , writeOptionsGetter: () => new WriteOptions()
                                       , writeOptionsSetter: (writeOptions) => { });

            if (isThrowRpcException)
            {
                Assert.ThrowsException<AggregateException>(() => _throwController.AddThrow(new RequestAddThrow() { IdDice = nbFacesDe, Result = result, IdProfile = profileId }, context).Result);
            }
            else
            {
                var res = _throwController.AddThrow(new RequestAddThrow() { IdDice = nbFacesDe, Result = result, IdProfile = profileId }, context).Result;
                Assert.IsNotNull(result);
                Assert.IsNotNull(res.ThrowId);
                Assert.AreEqual(res.Result, result);
                Assert.AreEqual(res.IdDice, nbFacesDe);
                Assert.AreEqual(res.ProfileId, profileId);
            }
        }

        private static IEnumerable<object[]> Test_GetData_RemoveThrow()
        {
            yield return new object[]
            {
                "aa6f9111-b174-4064-814b-ce7eb4169e80", false
            };

            yield return new object[]
            {
                "ba6f9111-b174-4064-814b-ce7eb4169e80", true
            };

            yield return new object[]
            {
                "aa6f9111--814b-ce7eb4169e80", true
            };
        }

        [TestMethod]
        [DynamicData(nameof(Test_GetData_RemoveThrow), DynamicDataSourceType.Method)]
        public void UT_RemoveThrow(string id, bool isThrowRpcException)
        {
            var context = TestServerCallContext.Create(method: nameof(_throwController.GetThrowById)
                                       , host: "localhost"
                                       , deadline: DateTime.Now.AddMinutes(30)
                                       , requestHeaders: new Metadata()
                                       , cancellationToken: CancellationToken.None
                                       , peer: null
                                       , authContext: null
                                       , contextPropagationToken: null
                                       , writeHeadersFunc: (metadata) => Task.CompletedTask
                                       , writeOptionsGetter: () => new WriteOptions()
                                       , writeOptionsSetter: (writeOptions) => { });

            if (isThrowRpcException)
            {
                Assert.ThrowsException<AggregateException>(() => _throwController.RemoveThrow(new RequestRemoveThrow() { Id = id }, context).Result);
            }
            else
            {
                var result = _throwController.RemoveThrow(new RequestRemoveThrow() { Id = id }, context).Result;
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Res);
            }
        }

        /// <summary>
        /// Créer une liste de données pour venir simuler notre base de données.
        /// </summary>
        /// <returns></returns>
        private static List<Model.Throw.Throw> CreateDatasetThrow()
        {
            return new List<Model.Throw.Throw>()
            {
                new Throw(1, new SimpleDice(2), Guid.Parse("aa6f9111-b174-4064-814b-ce7eb4169e80"), Guid.Parse("cc6f9111-b174-4064-814b-ce7eb4169e80")),
                new Throw(2, new SimpleDice(2), Guid.Parse("bb6f9111-b174-4064-814b-ce7eb4169e80"), Guid.Parse("cc6f9111-b174-4064-814b-ce7eb4169e80")),
                new Throw(1, new SimpleDice(3), Guid.Parse("dd6f9111-b174-4064-814b-ce7eb4169e80"), Guid.Parse("fe6f9111-b174-4064-814b-ce7eb4169e80")),
                new Throw(3, new SimpleDice(3), Guid.Parse("ee6f9111-b174-4064-814b-ce7eb4169e80"), Guid.Parse("fe6f9111-b174-4064-814b-ce7eb4169e80")),
                new Throw(4, new SimpleDice(5), Guid.Parse("ff6f9111-b174-4064-814b-ce7eb4169e80"), Guid.Parse("aeff9111-b174-4064-814b-ce7eb4169e80")),
                new Throw(3, new SimpleDice(5), Guid.Parse("ef6f9111-b174-4064-814b-ce7eb4169e80"), Guid.Parse("af6f9111-b174-4064-814b-ce7eb4169e80"))
            };
        }

        private static List<Model.Throw.Throw>? SimulatedGetThrowByProfileId(Guid id, int numPage, int nbByPage)
        {
            var profile = new List<Model.Profile>() {
                new SimpleProfile(Guid.Parse("cc6f9111-b174-4064-814b-ce7eb4169e80"), "Louis", "Perret"),
                new SimpleProfile(Guid.Parse("ac6f9111-b174-4064-814b-ce7eb4169e80"), "Louis2", "Perret2")
            };

            if (profile.Where(p => p.Id.Equals(id)).FirstOrDefault() == null) return null;
            var throws = CreateDatasetThrow();
            var result = throws.Where(t => t.ProfileId == id).Skip((numPage - 1) * nbByPage).Take(nbByPage).ToList();
            return result;
        }

        private static Guid SimulatedAddThrow(int result, int idDice, Guid idProfile)
        {
            var throws = CreateDatasetThrow();
            var guid = Guid.NewGuid();
            throws.Add(new Throw(result, new SimpleDice(idDice), guid, idProfile));
            return guid;
        }

        private static bool SimulatedRemoveThrow(Guid id)
        {
            var throws = CreateDatasetThrow();
            var t = throws.Find(t => t.Id == id);
            if (t == null) return false;
            throws.Remove(t);
            return true;
        }

    }
}
