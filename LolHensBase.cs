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

namespace LolHens {
    public class LolHensBase : TAPI.ModBase {
		public static bool loaded = false;
		
		public static List<LolHensItem> items = new List<LolHensItem>();
		
		public static Dictionary<int, Wings> wings = new Dictionary<int, Wings>();
		
        public override void OnLoad() {
			CallOnItemLoaded();
			loaded = true;
		}
		
        public override void OnUnload() {}
		
		public override void OnAllModsLoaded() {
			AddMobDrop(NPCDef.byName["Vanilla:Illuminant Bat"].type, ItemDef.byName["LolHens:IlluminantPearl"], 0.0167f);
			AddMobDrop(NPCDef.byName["Vanilla:Illuminant Slime"].type, ItemDef.byName["LolHens:IlluminantPearl"], 0.0167f);
			
			AddMobDrop(NPCDef.byName["Vanilla:Eyezor"].type, ItemDef.byName["LolHens:BrokenHeroWings"], 0.004f);
			
			MakeAmmo("Acorn", ItemDef.byName["Vanilla:Acorn"]);
			
			AddChestLoot(ItemDef.byName["LolHens:Slingshot"], 0.2f, ChestType.SURFACE);
		}
		
		public override void PreGameDraw(SpriteBatch sb) {}
		
        public override void PostGameDraw(SpriteBatch sb) {}

        public override void ChooseTrack(ref string current) {}

        public override object OnModCall(TAPI.ModBase mod, params object[] args) {
			return base.OnModCall(mod, args);
		}
		
		private void CallOnItemLoaded() {
			foreach (LolHensItem item in items)
				if (!item.loaded) {
					item.OnItemLoaded();
					item.loaded = true;
				}
		}
		
		public static void AddMobDrop(int type, Item item, float chance = 1, int numFrom = 1, int numTo = -1) {
			String drop = "{\"drop\":{\"item\":\""+item.name+"\",";
			if (numTo == -1)
				drop = drop+"\"stack\":"+numFrom+",";
			else
				drop = drop+"\"stack\":["+numFrom+","+numTo+"],";
			drop = drop+"\"chance\":"+chance.ToString().Replace(",", ".")+"}}";
			
			JsonData drops;
			if (NPCDef.npcDrops.ContainsKey(type))
				drops = NPCDef.npcDrops[type];
			else
				drops = new JsonData();
			drops.Add(JsonMapper.ToObject(drop)["drop"]);
			NPCDef.npcDrops[type] = drops;
		}
		
		public static void AddChestLoot(Item item, float chance = 1, int chestType = 15, bool rare = false, int numFrom = 1, int numTo = 1) {
			LolHensWorld.chestLoot.Add(new ChestLoot(item, chance, numFrom, numTo, chestType, rare));
		}
		
		public static void MakeAmmo(String ammoName, Item item) {
			if (!Defs.ammo.ContainsKey(ammoName)) Defs.ammo.Add(ammoName, 1000 + Defs.ammo.Count);
			item.ammo = Defs.ammo[ammoName];
			item.notAmmo = false;
			item.ranged = true;
		}
		
		public static void ResizeDictionary<Key, Value>(ref Dictionary<Key, Value> dictionary, int newSize) {
			Dictionary<Key, Value> newDict = new Dictionary<Key, Value>(newSize);
			foreach (KeyValuePair<Key, Value> keyValPair in dictionary)
				newDict.Add(keyValPair.Key, keyValPair.Value);
			dictionary = newDict;
		}
		
		private static int AddPlayerAccessory(ref Dictionary<int, Texture2D> textureDict, ref Dictionary<int, bool> loadedDict, ref bool[] loadedArray, Texture2D texture, Action<int> AddNewEntry = null) {
			foreach (KeyValuePair<int, Texture2D> keyValPair in textureDict)
				if (keyValPair.Value == texture) return keyValPair.Key;
			int newID;
			if (loadedDict != null) {
				newID = loadedDict.Capacity();
			} else if (loadedArray != null) {
				newID = loadedArray.Length;
			} else {
				return 0;
			}
			ResizeDictionary(ref textureDict, newID + 1);
			textureDict.Add(newID, texture);
			if (loadedDict != null) {
				ResizeDictionary(ref loadedDict, newID + 1);
				loadedDict.Add(newID, true);
			} else if (loadedArray != null) {
				Array.Resize(ref loadedArray, newID + 1);
				loadedArray[newID] = true;
			}
			if (AddNewEntry != null) AddNewEntry(newID);
			return newID;
		}
		
		private static int AddPlayerAccessory(ref Dictionary<int, Texture2D> textureDict, ref Dictionary<int, bool> loadedDict, Texture2D texture, Action<int> AddNewEntry = null) {
			bool[] loadedArray = null;
			return AddPlayerAccessory(ref textureDict, ref loadedDict, ref loadedArray, texture, AddNewEntry);
		}
		
		private static int AddPlayerAccessory(ref Dictionary<int, Texture2D> textureDict, ref bool[] loadedArray, Texture2D texture, Action<int> AddNewEntry = null) {
			Dictionary<int, bool> loadedDict = null;
			return AddPlayerAccessory(ref textureDict, ref loadedDict, ref loadedArray, texture, AddNewEntry);
		}
		
		public static int AddWings(Item item, Texture2D texture, int wingTime, float wingSpeed, bool wingGlow = false) {
			int newID = -1;
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
			return newID;
			// return AddPlayerAccessory(ref Main.wingsTexture, ref Main.wingsLoaded, texture, (newID) => wings.Add(newID, new Wings(wingTime, wingSpeed, wingGlow)));
		}
		
		public static int AddHead(Texture2D texture) {
			return AddPlayerAccessory(ref Main.armorHeadTexture, ref Main.armorHeadLoaded, texture);
		}
		
		public static int AddBody(Texture2D texture, Texture2D female, Texture2D arm) {
			return AddPlayerAccessory(ref Main.armorBodyTexture, ref Main.armorBodyLoaded, texture, (newID) => {
				ResizeDictionary(ref Main.femaleBodyTexture, newID + 1);
				ResizeDictionary(ref Main.armorArmTexture, newID + 1);
				Main.femaleBodyTexture.Add(newID, texture);
				Main.armorArmTexture.Add(newID, texture);
			});
		}
		
		public static int AddLeg(Texture2D texture) {
			return AddPlayerAccessory(ref Main.armorLegTexture, ref Main.armorLegsLoaded, texture);
		}
		
		public static float Interpolate(float[] array, float index, int[] translation = null) {
			int length = array.Length;
			if (translation == null) {
				translation = new int[array.Length];
				for (int i = 0; i < translation.Length; i++) translation[i] = i;
			} else {
				length = Math.Min(length, translation.Length);
			}
			int floor = (int)index;
			float interp = index - floor;
			float val1 = array[translation[floor % length]];
			float val2 = array[translation[(floor + 1) % length]];
			return val1 + interp * (val2 - val1);
		}
    }
	
	public static class DictionaryHelper {
		public static int Capacity<TKey, TValue>(this Dictionary<TKey, TValue> source) {
			FieldInfo entriesField = source.GetType().GetField("entries", BindingFlags.NonPublic | BindingFlags.Instance);
			if (entriesField == null) return 0;
			var entries = entriesField.GetValue(source) as Array;
			if (entries == null) return 0;
			return entries.Length;
		}
	}
}