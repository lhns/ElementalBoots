using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TAPI;
using Terraria;

namespace LolHens
{
    class CraftGroupFixResolver : Resolver
    {
        public override void Resolve()
        {
            foreach (String name in MBase.instance.craftGroups)
            {
                String groupName = "g:" + name;

                if (!ItemDef.byName.ContainsKey(groupName))
                {
                    Item item = new Item();
                    item.SetDefaults(groupName);
                    item.type = ItemDef.nextType++;
                    item.netID = item.type;
                    item.stack = 1;
                    item.name = groupName;
                    ItemDef.byName.Add(item.name, item);
                    ItemDef.byType.Add(item.type, item);
                }
            }
        }
    }
}
