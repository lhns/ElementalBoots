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

namespace LolHens
{
    public class LolHensBase : TAPI.ModBase
    {
        public static LolHensBase instance;

        public LolHensBase() : base() { instance = this; }

        public static Dictionary<int, Wings> wings = new Dictionary<int, Wings>();

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

        public static void ResizeDictionary<Key, Value>(ref Dictionary<Key, Value> dictionary, int newSize)
        {
            Dictionary<Key, Value> newDict = new Dictionary<Key, Value>(newSize);
            foreach (KeyValuePair<Key, Value> keyValPair in dictionary)
                newDict.Add(keyValPair.Key, keyValPair.Value);
            dictionary = newDict;
        }

        private static int AddPlayerAccessory(ref Dictionary<int, Texture2D> textureDict, ref Dictionary<int, bool> loadedDict, ref bool[] loadedArray, Texture2D texture, Action<int> AddNewEntry = null)
        {
            foreach (KeyValuePair<int, Texture2D> keyValPair in textureDict)
                if (keyValPair.Value == texture) return keyValPair.Key;
            int newID;
            if (loadedDict != null)
            {
                newID = loadedDict.Capacity();
            }
            else if (loadedArray != null)
            {
                newID = loadedArray.Length;
            }
            else
            {
                return 0;
            }
            ResizeDictionary(ref textureDict, newID + 1);
            textureDict.Add(newID, texture);
            if (loadedDict != null)
            {
                ResizeDictionary(ref loadedDict, newID + 1);
                loadedDict.Add(newID, true);
            }
            else if (loadedArray != null)
            {
                Array.Resize(ref loadedArray, newID + 1);
                loadedArray[newID] = true;
            }
            if (AddNewEntry != null) AddNewEntry(newID);
            return newID;
        }

        private static int AddPlayerAccessory(ref Dictionary<int, Texture2D> textureDict, ref Dictionary<int, bool> loadedDict, Texture2D texture, Action<int> AddNewEntry = null)
        {
            bool[] loadedArray = null;
            return AddPlayerAccessory(ref textureDict, ref loadedDict, ref loadedArray, texture, AddNewEntry);
        }

        private static int AddPlayerAccessory(ref Dictionary<int, Texture2D> textureDict, ref bool[] loadedArray, Texture2D texture, Action<int> AddNewEntry = null)
        {
            Dictionary<int, bool> loadedDict = null;
            return AddPlayerAccessory(ref textureDict, ref loadedDict, ref loadedArray, texture, AddNewEntry);
        }

        public static int AddWings(Item item, Texture2D texture, int wingTime, float wingSpeed, bool wingGlow = false)
        {
            /*int newID = -1;
            foreach (KeyValuePair<string, int> keyValPair in Defs.wingType)
                if (keyValPair.Key.Equals(item.name)) {
                    newID = keyValPair.Value;
                    break;
                }
            if (newID == -1) {
                newID = 24 + (Defs.wingNextType++);
                Defs.wingType[item.name] = newID;
            }
            Defs.wingTime[newID] = wingTime;
            if (!Main.dedServ) Main.wingsTexture[newID] = texture;
            if (!wings.ContainsKey(newID)) wings.Add(newID, new Wings(wingTime, wingSpeed, wingGlow));
            return newID;*/
            return 0;
            // return AddPlayerAccessory(ref Main.wingsTexture, ref Main.wingsLoaded, texture, (newID) => wings.Add(newID, new Wings(wingTime, wingSpeed, wingGlow)));
        }

        public static int AddHead(Texture2D texture)
        {
            return AddPlayerAccessory(ref Main.armorHeadTexture, ref Main.armorHeadLoaded, texture);
        }

        public static int AddBody(Texture2D texture, Texture2D female, Texture2D arm)
        {
            return AddPlayerAccessory(ref Main.armorBodyTexture, ref Main.armorBodyLoaded, texture, (newID) =>
            {
                ResizeDictionary(ref Main.femaleBodyTexture, newID + 1);
                ResizeDictionary(ref Main.armorArmTexture, newID + 1);
                Main.femaleBodyTexture.Add(newID, texture);
                Main.armorArmTexture.Add(newID, texture);
            });
        }

        public static int AddLeg(Texture2D texture)
        {
            return AddPlayerAccessory(ref Main.armorLegTexture, ref Main.armorLegsLoaded, texture);
        }
    }

    public static class LolHensBaseExtensions
    {
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
    }
}