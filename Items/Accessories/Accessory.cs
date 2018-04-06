using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementalBoots.Items.Accessories
{
    abstract class Accessory: MItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            
            item.accessory = true;
            item.maxStack = 1;
        }
    }
}
