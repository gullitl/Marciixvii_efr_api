
using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.DataAccess.Contexts;
using Marciixvii.EFR.App.Models.Entities;
using Microsoft.Extensions.Logging;

namespace Marciixvii.EFR.App.Services {
    public class FicheService : CrudService<Fiche>, IFicheService {
        public FicheService(ILogger<CrudService<Fiche>> logger, 
                             AppDbContext context) : base(logger, context) {
        }

    }
}
