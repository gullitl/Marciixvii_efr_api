using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marciixvii.EFR.App.Helpers {
    public static class ApiRoute {
        public static class CrudUrl {
            public const string GetAll = "getall";
            public const string GetById = "getbyid/{id}";
            public const string Create = "create";
            public const string Update = "update";
            public const string Delete = "delete/{id}";
        }
        public static class UtilisateurUrl {
            public const string Login = "login/{username}/{password}";
        }

    }
}
