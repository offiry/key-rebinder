using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Persistance.Repositories
{
    public class ValidationRepository : IValidationRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ValidationRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public bool IsGameExists(int gameId)
        {
            return _applicationDbContext
                .GameEntities
                .Where(g => g.Id == gameId)
                .FirstOrDefault() is null ? false : true;
        }
    }
}
