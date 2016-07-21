using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Exceptions;

namespace ElementalBoots
{
    public class Defs
    {
        public static Item GetItem(int id)
        {
            if (id < 0 || id >= Main.item.Length)
            {
                return null;
            }
            else
            {
                return Main.item[id];
            }
        }

        private static Tuple<string, string> ResolveName(string name)
        {
            String modName, itemName;

            if (name.Contains(":"))
            {
                var seperatorIndex = name.IndexOf(":", StringComparison.Ordinal);
                modName = name.Substring(0, seperatorIndex);
                itemName = name.Substring(seperatorIndex + 1);
            }
            else
            {
                modName = null;
                itemName = name;
            }

            return new Tuple<string, string>(modName == "Vanilla" ? null : modName, itemName);
        }

        public static Item GetItem(string name)
        {
            var resolvedName = ResolveName(name);

            if (resolvedName.Item1 == null)
            {
                foreach (var item in Main.item)
                {
                    if (item.modItem == null && item.name == resolvedName.Item2)
                    {
                        return item;
                    }
                }
            }
            else
            {
                var mod = ModLoader.GetMod(resolvedName.Item1);
                var modItem = mod != null ? mod.GetItem(resolvedName.Item2) : null;

                if (modItem != null)
                {
                    return modItem.item;
                }
            }

            return null;
        }
    }
}