using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Entities
{
    public class GameEntity
    {
        public int Id { get; set; }
        public string DisplayUIGameName { get; set; }
        public string WindowGameName { get; set; }
        public List<GameKeyEntity> GameKeysEntity { get; set; }
    }
}
