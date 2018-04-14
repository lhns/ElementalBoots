using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LibEventManagerCSharp;
using Microsoft.Xna.Framework;

namespace ElementalBoots
{
    class ElementalBootsMod : Mod
    {
        internal static ElementalBootsMod instance;

        public EventRegistry eventRegistry = new EventRegistry();

        public readonly Dictionary<int, List<Drop>> npcDrops = new Dictionary<int, List<Drop>>();

        public ElementalBootsMod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }

        public override void Load()
        {
            instance = this;
        }

        public void AddDrop(int npc, int item, float chance = 1f, int from = 1, int to = 1)
        {
            List<Drop> drops;
            npcDrops.TryGetValue(npc, out drops);
            if (drops == null)
            {
                drops = new List<Drop>();
                npcDrops.Add(npc, drops);
            }
            drops.Add(new Drop(item, chance, from, to));
        }


        public Item NewItemType(int type)
        {
            var item = new Item();

            item.SetDefaults(type);

            if (item.type != ItemID.None)
                return item;
            else
                return null;
        }

        private readonly Dictionary<int, Item> ItemByType = new Dictionary<int, Item>();

        public Item GetItemType(int type)
        {
            if (type < Main.item.Length
                && Main.item[type] != null
                && Main.item[type].type != ItemID.None)
                return Main.item[type];

            Item item;

            instance.ItemByType.TryGetValue(type, out item);
            if (item != null) return item;

            item = NewItemType(type);
            if (item != null) instance.ItemByType.Add(type, item);

            return item;
        }

        public Item GetItemType(string name)
        {
            return GetItem(name).item;
        }

        public void Log(string text, Color color)
        {
            ErrorLogger.Log(text);
            Main.NewTextMultiline(text, false, color);
        }

        public void Log(string text)
        {
            Log(text, Color.White);
        }
    }
}
