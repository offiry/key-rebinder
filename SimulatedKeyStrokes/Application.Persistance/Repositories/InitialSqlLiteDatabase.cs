using Application.Contracts;
using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application.Persistance.Repositories
{
    public class InitialSqlLiteDatabase : IInitialSqlLiteDatabase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public InitialSqlLiteDatabase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));

            InitSqlLiteDatabase().GetAwaiter().GetResult();
        }

        private async Task InitSqlLiteDatabase()
        {
            await
            _applicationDbContext
                .GameEntities
                .AddAsync(new GameEntity
                {
                    WindowGameName = "Company Of Heroes",
                    DisplayUIGameName = "Company Of Heroes Relaunch"
                });

            await
            _applicationDbContext
                .KeysEntities
                .AddRangeAsync(
                    new KeyEntity
                    {
                        Key = Keys.W.ToString().ToUpper()
                    },
                    new KeyEntity
                    {
                        Key = Keys.S.ToString().ToUpper()
                    },
                    new KeyEntity
                    {
                        Key = Keys.A.ToString().ToUpper()
                    },
                    new KeyEntity
                    {
                        Key = Keys.D.ToString().ToUpper()
                    },
                    new KeyEntity
                    {
                        Key = Keys.Up.ToString().ToUpper()
                    },
                    new KeyEntity
                    {
                        Key = Keys.Down.ToString().ToUpper()
                    },
                    new KeyEntity
                    {
                        Key = Keys.Left.ToString().ToUpper()
                    },
                    new KeyEntity
                    {
                        Key = Keys.Right.ToString().ToUpper()
                    }
                );

            await
            _applicationDbContext
                .KeyModifierEntities
                .AddRangeAsync(new KeyModifierEntity
                {
                    Key = "WithRepeat",
                    KeyModifier = "0x0000"
                });

            await
            _applicationDbContext
                .SaveChangesAsync();

            var wsad = _applicationDbContext
                .KeysEntities
                .Where(k =>
                    k.Key == Keys.W.ToString().ToUpper() ||
                    k.Key == Keys.S.ToString().ToUpper() ||
                    k.Key == Keys.A.ToString().ToUpper() ||
                    k.Key == Keys.D.ToString().ToUpper())
                .ToList();

            var gameId = _applicationDbContext
                .GameEntities
                .Where(g => g.WindowGameName == "Company Of Heroes").FirstOrDefault().Id;

            var withRepeat = _applicationDbContext
                .KeyModifierEntities
                .Where(km => km.Key == "WithRepeat").FirstOrDefault().Id;

            var wsadToArrows = new Dictionary<string, string>()
            {
                {Keys.W.ToString().ToUpper(), Keys.Up.ToString().ToUpper() },
                {Keys.S.ToString().ToUpper(), Keys.Down.ToString().ToUpper() },
                {Keys.A.ToString().ToUpper(), Keys.Left.ToString().ToUpper() },
                {Keys.D.ToString().ToUpper(), Keys.Right.ToString().ToUpper() },
            };

            foreach (var k in wsad)
            {
                await
                _applicationDbContext
                    .GameKeysEntities
                    .AddAsync(new GameKeyEntity
                    {
                        KeyId_FK = k.Id,
                        KeyModifierId_FK = withRepeat,
                        WindowGameNameId_FK = gameId,
                        TargetKey_FK = _applicationDbContext
                            .KeysEntities
                            .Where(ke => ke.Key == wsadToArrows[k.Key]).FirstOrDefault().Id
                    });
            }

            await
            _applicationDbContext
                .SaveChangesAsync();

        }
    }
}
