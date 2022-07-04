using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Entities
{
    public class KeyModifierEntity
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string KeyModifier { get; set; }
        public List<GameKeyEntity> GameKeysEntity { get; set; }
    }
}
