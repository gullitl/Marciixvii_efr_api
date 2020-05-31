using FluentAssertions;
using Marciixvii.EFR.App.Helpers;
using Marciixvii.EFR.App.Helpers.Enumerations;
using Marciixvii.EFR.App.Models.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1 {
    public class UtilisateurControllerTest : IntegrationTest {
        [Fact]
        public async Task Test1Async() {
            // Arrange
            string infix = "utilisateur/";
            Utilisateur utilisateur = new Utilisateur {
                Nom = "Luzolo",
                Postnom = "Lusembo",
                Prenom = "Plamedi",
                Sexe = Sexe.Masculin,
                Photosrc = "",
                Email = "plam.l@live.fr",
                Username = "pllusembo",
                Password = "12345",
                NiveauAcces = NiveauAcces.Administrateur,
            };

            var resp = await _testClient.PostAsJsonAsync(string.Concat(_testClient.BaseAddress.AbsoluteUri, infix, "/create"), utilisateur);
            resp.StatusCode.Should().Be(HttpStatusCode.Created);
            // Act
            var response = await _testClient.GetAsync(string.Concat(_testClient.BaseAddress.AbsoluteUri, infix, "/getall"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            //(await response.Content.ReadAsAsync<List<Utilisateur>>()).Should().BeEmpty();
        }
    }
}
