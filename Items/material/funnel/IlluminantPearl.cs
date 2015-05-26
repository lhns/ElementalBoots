using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TAPI;

namespace LolHens.Items
{
    public class IlluminantPearl: LolHensItem
    {
        public override void InitType(Type type)
        {
            const float dropChance = 0.0167f;

            NPCDef.byName["Vanilla:Illuminant Bat"].AddDrop(item, dropChance);
            NPCDef.byName["Vanilla:Illuminant Slime"].AddDrop(item, dropChance);
            NPCDef.byName["Vanilla:Chaos Elemental"].AddDrop(item, dropChance);
            NPCDef.byName["Vanilla:Enchanted Sword"].AddDrop(item, dropChance);
        }
    }
}
