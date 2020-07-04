
using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.DataAccess.Contexts;
using Marciixvii.EFR.App.Models.Entities;
using Microsoft.Extensions.Logging;

namespace Marciixvii.EFR.App.Services {
    public class ClientService : CrudService<Client>, IClientService {
        public ClientService(ILogger<CrudService<Client>> logger, 
                             AppDbContext context) : base(logger, context) {
        }

    }
}
