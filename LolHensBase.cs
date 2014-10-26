using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using LitJson;

using TAPI;
using Terraria;
using LolHens.Items;
using LolHens.Projectiles;

namespace LolHens
{
    public class LolHensBase : TAPI.ModBase
    {
        public static LolHensBase instance;

        public List<LolHensItem> items = new List<LolHensItem>();
        public List<LolHensProjectile> projectiles = new List<LolHensProjectile>();

        public LolHensBase() : base() { instance = this; }

        public override void OnLoad()
        {
        }

        public override void OnAllModsLoaded()
        {
            ItemDef.byName["LolHens:IlluminantPearl"].MakeMobDrop(NPCDef.byName["Vanilla:Illuminant Bat"].type, 0.0167f);
            ItemDef.byName["LolHens:IlluminantPearl"].MakeMobDrop(NPCDef.byName["Vanilla:Illuminant Slime"].type, 0.0167f);
            ItemDef.byName["LolHens:BrokenHeroWings"].MakeMobDrop(NPCDef.byName["Vanilla:Eyezor"].type, 0.004f);

            ItemDef.byName["Vanilla:Acorn"].MakeAmmo("Acorn");

            ItemDef.byName["LolHens:Slingshot"].MakeChestLoot(0.2f, ChestType.SURFACE);
        }

        public override object OnModCall(TAPI.ModBase mod, params object[] args)
        {
            return base.OnModCall(mod, args);
        }
    }

    public static class LolHensBaseExtensions
    {
        public static void Resize<Key, Value>(this Dictionary<Key, Value> unused, ref Dictionary<Key, Value> dictionary, int newSize)
        {
            Dictionary<Key, Value> newDict = new Dictionary<Key, Value>(newSize);
            foreach (KeyValuePair<Key, Value> keyValPair in dictionary)
                newDict.Add(keyValPair.Key, keyValPair.Value);
            dictionary = newDict;
        }

        public static int Capacity<TKey, TValue>(this Dictionary<TKey, TValue> source)
        {
            FieldInfo entriesField = source.GetType().GetField("entries", BindingFlags.NonPublic | BindingFlags.Instance);
            if (entriesField == null) return 0;
            var entries = entriesField.GetValue(source) as Array;
            if (entries == null) return 0;
            return entries.Length;
        }


        public static void MakeChestLoot(this Item item, float chance = 1, int chestType = 15, bool rare = false, int numFrom = 1, int numTo = 1)
        {
            LolHensWorld.chestLoot.Add(new ChestLoot(item, chance, numFrom, numTo, chestType, rare));
        }

        public static void MakeAmmo(this Item item, String ammoName)
        {
            if (!ammoName.Contains(":")) ammoName = LolHensBase.instance.mod.InternalName + ":" + ammoName;
            if (!ItemDef.ammo.ContainsKey(ammoName)) ItemDef.ammo.Add(ammoName, 1000 + ItemDef.ammo.Count);
            item.ammo = ItemDef.ammo[ammoName];
        }

        public static void MakeMobDrop(this Item item, int type, float chance = 1, int numFrom = 1, int numTo = -1)
        {
            String drop = "{\"drop\":{\"item\":\"" + item.name + "\",";
            if (numTo == -1)
                drop = drop + "\"stack\":" + numFrom + ",";
            else
                drop = drop + "\"stack\":[" + numFrom + "," + numTo + "],";
            drop = drop + "\"chance\":" + chance.ToString().Replace(",", ".") + "}}";

            JsonData drops;
            if (NPCDef.npcDrops.ContainsKey(type))
                drops = NPCDef.npcDrops[type];
            else
                drops = new JsonData();
            drops.Add(JsonMapper.ToObject(drop)["drop"]);
            NPCDef.npcDrops[type] = drops;
        }

        public static float Interpolate(this float[] array, float index, int[] translation = null)
        {
            int length = array.Length;
            if (translation == null)
            {
                translation = new int[array.Length];
                for (int i = 0; i < translation.Length; i++) translation[i] = i;
            }
            else
            {
                length = Math.Min(length, translation.Length);
            }
            int floor = (int)index;
            float interp = index - floor;
            float val1 = array[translation[floor % length]];
            float val2 = array[translation[(floor + 1) % length]];
            return val1 + interp * (val2 - val1);
        }

        public static LolHensItem AsLolHensItem(this Item item)
        {
            if (item != null) {
                ModItem modItem = item.GetSubClass<ModItem>();
                if (modItem != null && modItem is LolHensItem) return modItem as LolHensItem;
            }
            return null;
        }

        public static LolHensItem GetWingsItem(this Player player)
        {
            if (player.wings != 0) foreach (LolHensItem item in LolHensBase.instance.items) if (item.item.wingSlot == player.wings) return item;
            return null;
        }
    }
}