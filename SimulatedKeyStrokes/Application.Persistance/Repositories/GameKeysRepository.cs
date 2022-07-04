using Application.Contracts;
using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application.Persistance.Repositories
{
    public class GameKeysRepository : IGameKeysRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GameKeysRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }
    }
}
