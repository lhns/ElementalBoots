using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TAPI;

namespace LolHens.Items
{
    public class BrokenHeroWings: LolHensItem
    {
        public override void InitType(Type type)
        {
            NPCDef.byName["Vanilla:Eyezor"].AddDrop(item, 0.004f);
        }
    }
}
