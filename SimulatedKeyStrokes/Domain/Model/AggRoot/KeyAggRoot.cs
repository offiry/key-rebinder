using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.AggRoot
{
    public class KeyAggRoot
    {
        public GameEntity GameEntity { get; set; }
        public KeyEntity KeyEntity { get; set; }
        public KeyModifierEntity KeyModifierEntity { get; set; }
        public KeyEntity TargetKeyEntity { get; set; }
    }
}
