using System;
using System.Collections.Generic;
using System.Text;

namespace dnd_combat_helper.Classes.Entities
{
    interface IDeathSaveable
    {
        public bool IsUnconscious { get; }
        public int SuccessfulDeathSaves { get; set; }
        public int FailedDeathSaves { get; set; }
        public void DeathSave(int roll);
    }
}
