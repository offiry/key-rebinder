using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Entities
{
    public class KeyEntity
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public List<GameKeyEntity> GameKeysEntity { get; set; }
        public List<GameKeyEntity> GameTargetKeysEntity { get; set; }
    }
}
