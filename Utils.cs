using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Exceptions;

namespace ElementalBoots
{
    public class Utils
    {
        public static Item NewItem(int type)
        {
            var item = new Item();

            item.SetDefaults(type);

            if (item.type != ItemID.None) return item;

            return null;
        }

        /*public static Item NewItem(Mod mod, string name)
        {
            if (mod == null)
            {
                var item = new Item();

                item.SetDefaults(name);

                if (item.type != ItemID.None) return item;
            }
            else
            {
                var type = mod.ItemType(name);

                if (type != ItemID.None) return NewItem(type);
            }

            return null;
        }*/

        private static Tuple<Mod, string> ResolveName(string name)
        {
            if (!name.Contains(":")) name = "Vanilla:" + name;

            var seperatorIndex = name.IndexOf(":", StringComparison.Ordinal);
            var modName = name.Substring(0, seperatorIndex);
            var itemName = name.Substring(seperatorIndex + 1);

            var mod = modName == "Vanilla" ? null : ModLoader.GetMod(modName);

            return new Tuple<Mod, string>(mod, itemName);
        }

        /*public static Item NewItem(string name)
        {
            var resolvedName = ResolveName(name);

            return NewItem(resolvedName.Item1, resolvedName.Item2);
        }*/

        private static readonly Dictionary<int, Item> ItemByType = new Dictionary<int, Item>();

        public static Item GetItem(int type)
        {
            Item item;

            ItemByType.TryGetValue(type, out item);
            if (item != null) return item;

            item = NewItem(type);
            if (item != null)
            {
                ItemByType.Add(type, item);
                return item;
            }

            return null;
        }

        private static readonly Dictionary<Tuple<Mod, string>, Item> ItemByName =
            new Dictionary<Tuple<Mod, string>, Item>();

        /*public static Item GetItem(Mod mod, string name)
        {
            Item item;
            var tuple = new Tuple<Mod, string>(mod, name);

            ItemByName.TryGetValue(tuple, out item);
            if (item != null) return item;

            item = NewItem(mod, name);
            if (item != null)
            {
                ItemByName.Add(tuple, item);
                return item;
            }

            return null;
        }*/

        /*public static Item GetItem(string name)
        {
            var resolvedName = ResolveName(name);

            return GetItem(resolvedName.Item1, resolvedName.Item2);
        }*/

        public static Projectile GetProjectile(Mod mod, string name)
        {
            return mod.GetProjectile(name).projectile;
        }
    }
}