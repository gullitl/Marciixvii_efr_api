using FluentAssertions;
using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.Helpers.Enumerations;
using Marciixvii.EFR.App.Models.DTOs;
using Marciixvii.EFR.App.Models.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1 {
    public class UtilisateurControllerTest : IntegrationTest {
        private readonly string route = "utilisateur/";

        [Fact]
        public async Task TestCreate() {
            Utilisateur utilisateur = new Utilisateur {
                Nom = "Luzolo",
                Postnom = "Lusembo",
                Prenom = "Plamedi",
                Sexe = Sexe.Masculin,
                Photosrc = "",
                Email = "plam.l@live.fr",
                Username = "pllusembo",
                Password = "12345",
                NiveauAcces = NiveauAcces.Administrateur
            };

            HttpResponseMessage response = await TestClient.PostAsJsonAsync(route + "create", utilisateur);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage response1 = await TestClient.PostAsJsonAsync(route + "create", utilisateur);
            response1.StatusCode.Should().Be(HttpStatusCode.NoContent);
            Utilisateur u = await response1.Content.ReadAsAsync<Utilisateur>(); 
            u.Should().BeNull();

            HttpResponseMessage response2 = await TestClient.PostAsJsonAsync<Utilisateur>(route + "create", null);
            response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task TestGetall() {
            Utilisateur utilisateur = new Utilisateur {
                Nom = "Luzolo",
                Postnom = "Nzuzi",
                Prenom = "Zouzou",
                Sexe = Sexe.Feminin,
                Photosrc = "",
                Email = "elsa.l@live.fr",
                Username = "elsa",
                Password = "12345",
                NiveauAcces = NiveauAcces.Utilisateur
            };

            HttpResponseMessage response = await TestClient.PostAsJsonAsync(route + "create", utilisateur);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u = await response.Content.ReadAsAsync<Utilisateur>();
            u.Should().NotBeNull();

            HttpResponseMessage response1 = await TestClient.GetAsync(route + "getall");
            response1.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response1.Content.ReadAsAsync<List<Utilisateur>>()).Should().NotBeEmpty();
        }

        [Fact]
        public async Task TestGetById() {
            Utilisateur utilisateur = new Utilisateur {
                Nom = "Luzolo",
                Postnom = "Matanu",
                Prenom = "Hervé",
                Sexe = Sexe.Masculin,
                Photosrc = "",
                Email = "herve.l@live.fr",
                Username = "hlm",
                Password = "12345",
                NiveauAcces = NiveauAcces.Utilisateur
            };

            HttpResponseMessage response = await TestClient.PostAsJsonAsync(route + "create", utilisateur);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur utilisateur1 = await response.Content.ReadAsAsync<Utilisateur>();

            HttpResponseMessage response1 = await TestClient.GetAsync(route + "getbyid?id=" + utilisateur1.Id);
            response1.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u = await response1.Content.ReadAsAsync<Utilisateur>();
            u.Equals(utilisateur1).Should().BeTrue();

            HttpResponseMessage response2 = await TestClient.DeleteAsync(route + "delete?id=" + utilisateur1.Id);
            response2.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage response3 = await TestClient.GetAsync(route + "getbyid?id=" + utilisateur1.Id);
            response3.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task TestDelete() {
            Utilisateur utilisateur = new Utilisateur {
                Nom = "Luzolo",
                Postnom = "Nsambu",
                Prenom = "Nadine",
                Sexe = Sexe.Feminin,
                Photosrc = "",
                Email = "nadine.l@live.fr",
                Username = "nana",
                Password = "12345",
                NiveauAcces = NiveauAcces.Utilisateur
            };

            HttpResponseMessage response = await TestClient.PostAsJsonAsync(route + "create", utilisateur);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur utilisateur1 = await response.Content.ReadAsAsync<Utilisateur>();

            HttpResponseMessage response1 = await TestClient.DeleteAsync(route + "delete?id=" + utilisateur1.Id);
            response1.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage response2 = await TestClient.GetAsync(route + "getbyid?id=" + utilisateur1.Id);
            response2.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task TestLogin() {
            Utilisateur utilisateur = new Utilisateur {
                Nom = "Luzolo",
                Postnom = "Makiese",
                Prenom = "Patricia",
                Sexe = Sexe.Feminin,
                Photosrc = "",
                Email = "patou.l@live.fr",
                Username = "ptcia",
                Password = "12345",
                NiveauAcces = NiveauAcces.Utilisateur
            };

            HttpResponseMessage response = await TestClient.PostAsJsonAsync(route + "create", utilisateur);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur utilisateur1 = await response.Content.ReadAsAsync<Utilisateur>();

            HttpResponseMessage response1 = await TestClient.GetAsync(route + "getbyid?id=" + utilisateur1.Id);
            response1.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u = await response1.Content.ReadAsAsync<Utilisateur>();
            u.Equals(utilisateur1).Should().BeTrue();

            HttpResponseMessage response2 = await TestClient.PostAsJsonAsync(route + "login", utilisateur1);
            response2.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u1 = await response2.Content.ReadAsAsync<Utilisateur>();
            u1.Equals(utilisateur1).Should().BeTrue();

            Utilisateur utilisateur2 = new Utilisateur {
                Email = "patou.l@live.fr",
                Password = "12345"
            };

            HttpResponseMessage response3 = await TestClient.PostAsJsonAsync(route + "login", utilisateur2);
            response3.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u2 = await response3.Content.ReadAsAsync<Utilisateur>();
            u2.Equals(utilisateur1).Should().BeTrue();

            Utilisateur utilisateur3 = new Utilisateur {
                Username = "ptcia",
                Password = "12345"
            };

            HttpResponseMessage response4 = await TestClient.PostAsJsonAsync(route + "login", utilisateur3);
            response4.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u3 = await response4.Content.ReadAsAsync<Utilisateur>(); 
            u3.Equals(utilisateur1).Should().BeTrue();

            utilisateur2.Email = "pato.l@live.fr";

            HttpResponseMessage response5 = await TestClient.PostAsJsonAsync(route + "login", utilisateur2);
            response5.StatusCode.Should().Be(HttpStatusCode.NoContent);
            Utilisateur u4 = await response4.Content.ReadAsAsync<Utilisateur>(); 
            u4.Should().BeNull();

            utilisateur3.Username = "ptci";

            HttpResponseMessage response6 = await TestClient.PostAsJsonAsync(route + "login", utilisateur3);
            response6.StatusCode.Should().Be(HttpStatusCode.NoContent);
            Utilisateur u5 = await response6.Content.ReadAsAsync<Utilisateur>();
            u5.Should().BeNull();

            utilisateur2.Email = "patou.l@live.fr";
            utilisateur2.Password = "1234";

            HttpResponseMessage response7 = await TestClient.PostAsJsonAsync(route + "login", utilisateur2);
            response7.StatusCode.Should().Be(HttpStatusCode.NoContent);
            Utilisateur u6 = await response7.Content.ReadAsAsync<Utilisateur>();
            u6.Should().BeNull();

            HttpResponseMessage response8 = await TestClient.DeleteAsync(route + "delete?id=" + utilisateur1.Id);
            response8.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage response9 = await TestClient.PostAsJsonAsync(route + "login", utilisateur1);
            response9.StatusCode.Should().Be(HttpStatusCode.NoContent);
            (await response9.Content.ReadAsAsync<Utilisateur>()).Should().BeNull();
        }

        //Not implementated yet
        [Fact]
        public async Task TestUpdate() {
            Utilisateur utilisateur = new Utilisateur {
                Nom = "Lusembo",
                Postnom = "Kekese",
                Prenom = "Antoinette",
                Sexe = Sexe.Masculin,
                Photosrc = "",
                Email = "kemi.l@live.fr",
                Username = "kemi",
                Password = "12345",
                NiveauAcces = NiveauAcces.Utilisateur
            };

            var response = await TestClient.PostAsJsonAsync(route + "create", utilisateur);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur utilisateur1 = await response.Content.ReadAsAsync<Utilisateur>();

            var response1 = await TestClient.GetAsync(route + "getbyid?id=" + utilisateur1.Id);
            response1.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u = await response1.Content.ReadAsAsync<Utilisateur>();
            u.Equals(utilisateur1).Should().BeTrue();

            Utilisateur utilisateur2 = new Utilisateur {
                Id = utilisateur1.Id,
                Nom = "Lusembo",
                Postnom = "Kekese Kemi",
                Prenom = "Antoinette",
                Sexe = Sexe.Feminin,
                Photosrc = "",
                Email = "anto.l@live.fr",
                Username = "nenette",
                Password = "12345",
                NiveauAcces = NiveauAcces.Utilisateur
            };

            var response2 = await TestClient.PutAsJsonAsync(route + "update", utilisateur2);
            response2.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u1 = await response2.Content.ReadAsAsync<Utilisateur>();
            u1.Equals(utilisateur2).Should().BeTrue();
            u1.Equals(utilisateur1).Should().BeFalse();

        }

        [Fact]
        public async Task TestChangeProfil() {
            Utilisateur utilisateur = new Utilisateur {
                Nom = "Sangi",
                Postnom = "Mohumbu",
                Prenom = "Naomi",
                Sexe = Sexe.Masculin,
                Photosrc = "",
                Email = "nao.l@live.fr",
                Username = "nao",
                Password = "12345",
                NiveauAcces = NiveauAcces.Utilisateur
            };

            var response = await TestClient.PostAsJsonAsync(route + "create", utilisateur);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur utilisateur1 = await response.Content.ReadAsAsync<Utilisateur>();

            var response1 = await TestClient.GetAsync(route + "getbyid?id=" + utilisateur1.Id);
            response1.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u = await response1.Content.ReadAsAsync<Utilisateur>();
            u.Equals(utilisateur1).Should().BeTrue();

            Utilisateur utilisateur2 = new Utilisateur {
                Id = utilisateur1.Id,
                Nom = "Sangi",
                Postnom = "Mohumbu",
                Prenom = "Naomie",
                Sexe = Sexe.Feminin,
                Photosrc = "",
                Email = "naomi.l@live.fr",
                Username = "naomi",
                NiveauAcces = NiveauAcces.Utilisateur
            };

            var response2 = await TestClient.PutAsJsonAsync(route + "changeprofil", utilisateur2);
            response2.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u1 = await response2.Content.ReadAsAsync<Utilisateur>();
            u1.Equals(utilisateur2).Should().BeTrue();
            u1.Equals(utilisateur1).Should().BeFalse();
        }

        private string GetEncrypt(string username) {
            char separetor = '#';
            string changePasswordToken = username;
            double timeout = 60;
            string datestr = DateTime.Now.ToString();

            string plain = $"{changePasswordToken}{separetor}{timeout}{separetor}{datestr}";
            return DesCryptography.Encrypt(plain);
        }

        [Fact]
        public async Task TestChangePassword() {
            Utilisateur utilisateur = new Utilisateur {
                Nom = "Kahuma",
                Postnom = "Luzolo",
                Prenom = "Precieu",
                Sexe = Sexe.Masculin,
                Photosrc = "",
                Email = "precieu.l@live.fr",
                Username = "precieu",
                Password = "12345",
                NiveauAcces = NiveauAcces.Utilisateur
            };

            var response = await TestClient.PostAsJsonAsync(route + "create", utilisateur);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur utilisateur1 = await response.Content.ReadAsAsync<Utilisateur>();

            var response1 = await TestClient.GetAsync(route + "getbyid?id=" + utilisateur1.Id);
            response1.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u = await response1.Content.ReadAsAsync<Utilisateur>();
            u.Equals(utilisateur1).Should().BeTrue();

            string token = GetEncrypt(u.Username);

            UtilisateurNewPassword utilisateurNewPassword = new UtilisateurNewPassword {
                Token = token,
                Username = u.Username,
                Password = "UtlzNewPsw123"
            };

            var response2 = await TestClient.PutAsJsonAsync(route + "changepassword", utilisateurNewPassword);
            response2.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u1 = await response2.Content.ReadAsAsync<Utilisateur>();
            u1.Equals(u).Should().BeFalse();
            u1.Equals(utilisateur1).Should().BeFalse();
            u1.Password.Equals(u.Password).Should().BeFalse();
            u1.Password.Equals(utilisateur1.Password).Should().BeFalse();

            var response3 = await TestClient.GetAsync(route + "getbyid?id=" + utilisateur1.Id);
            response3.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u2 = await response3.Content.ReadAsAsync<Utilisateur>();
            u2.Equals(u).Should().BeFalse();
            u2.Equals(utilisateur1).Should().BeFalse();
            u2.Password.Equals(u.Password).Should().BeFalse();
            u2.Password.Equals(utilisateur1.Password).Should().BeFalse();

        }

        [Fact]
        public async Task TestChangePasswordWithToken() {
            Utilisateur utilisateur = new Utilisateur {
                Nom = "Nsimba",
                Postnom = "Nsimba",
                Prenom = "Dina",
                Sexe = Sexe.Masculin,
                Photosrc = "",
                Email = "dina.l@live.fr",
                Username = "dina",
                Password = "dina123",
                NiveauAcces = NiveauAcces.Utilisateur
            };

            var response = await TestClient.PostAsJsonAsync(route + "create", utilisateur);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur utilisateur1 = await response.Content.ReadAsAsync<Utilisateur>();

            var response1 = await TestClient.GetAsync(route + "getbyid?id=" + utilisateur1.Id);
            response1.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u = await response1.Content.ReadAsAsync<Utilisateur>();
            u.Equals(utilisateur1).Should().BeTrue();

            UtilisateurNewPassword utilisateurNewPassword = new UtilisateurNewPassword {
                Username = u.Username,
                Password = "UtlzNewPsw123"
            };

            var response2 = await TestClient.PutAsJsonAsync(route + "changepassword", utilisateurNewPassword);
            response2.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u1 = await response2.Content.ReadAsAsync<Utilisateur>();
            u1.Equals(u).Should().BeFalse();
            u1.Equals(utilisateur1).Should().BeFalse();
            u1.Password.Equals(u.Password).Should().BeFalse();
            u1.Password.Equals(utilisateur1.Password).Should().BeFalse();

            var response3 = await TestClient.GetAsync(route + "getbyid?id=" + utilisateur1.Id);
            response3.StatusCode.Should().Be(HttpStatusCode.OK);
            Utilisateur u2 = await response3.Content.ReadAsAsync<Utilisateur>();
            u2.Equals(u).Should().BeFalse();
            u2.Equals(utilisateur1).Should().BeFalse();
            u2.Password.Equals(u.Password).Should().BeFalse();
            u2.Password.Equals(utilisateur1.Password).Should().BeFalse();
        }

    }
}
