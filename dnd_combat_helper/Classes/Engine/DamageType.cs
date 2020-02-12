using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dnd_combat_helper.Classes.Engine
{
    public enum DamageType
    {
        Bludgeoning,
        Piercing,
        Slashing,
        Fire,
        Cold,
        Poison,
        Acid,
        Psychic,
        Necrotic,
        Radiant,
        Lightning,
        Thunder,
        Force,
        Physical = Bludgeoning | Piercing | Slashing,
        Magic = Fire | Cold | Poison | Acid | Psychic | Necrotic | Radiant | Lightning | Thunder | Force
    }
}
