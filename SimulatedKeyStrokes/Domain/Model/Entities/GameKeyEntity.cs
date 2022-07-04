using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Entities
{
    public class GameKeyEntity
    {
        public int Id { get; set; }
        public int KeyId_FK { get; set; }
        public int KeyModifierId_FK { get; set; }
        public int WindowGameNameId_FK { get; set; }
        public int TargetKey_FK { get; set; }
        public KeyEntity KeyEntity { get; set; }
        public KeyEntity TargetKeyEntity { get; set; }
        public GameEntity GameEntity { get; set; }
        public KeyModifierEntity KeyModifierEntity { get; set; }
    }
}
