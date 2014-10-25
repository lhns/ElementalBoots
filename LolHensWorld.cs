using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LitJson;

using TAPI;
using Terraria;
using LolHens.Items;

namespace LolHens {
    public class LolHensWorld : TAPI.ModWorld {
		public static List<ChestLoot> chestLoot = new List<ChestLoot>();
		
		public override void WorldGenPostGen() {
			foreach (Chest chest in Main.chest) {
				if (chest == null) continue;
				
				foreach (ChestLoot loot in chestLoot) chest.AddLoot(loot);
			}
		}
	}
	
	public class ChestLoot {
		public Item item;
		public float chance;
		public int numFrom, numTo;
		public int chestType;
		public bool rare;
		
		public ChestLoot(Item item, float chance, int numFrom, int numTo, int chestType, bool rare) {
			this.item = item;
			this.chance = chance;
			this.numFrom = numFrom;
			this.numTo = numTo;
			this.chestType = chestType;
			this.rare = rare;
		}
	}
	
	public static class ChestType {
		public const int SURFACE = 1;
		public const int UNDERGROUND = 2;
		public const int CAVERN = 4;
		public const int UNDERWORLD = 8;
	}
	
	public static class ChestHelper {
		public static bool AddItem(this Chest chest, Item item, int stack) {
			if (chest == null || item == null) return false;
			if (chest.item == null) return false;
			
			for (int i = 0; i < chest.item.Length; i++) {
				if (chest.item[i] == null) continue;
				
				if (chest.item[i].type == 0) {
					chest.item[i].SetDefaults(item.type, false);
					chest.item[i].stack = stack;
					
					return true;
				}
			}
			
			return false;
		}
		
		public static bool AddLoot(this Chest chest, ChestLoot loot) {
			if ((loot.chestType & chest.getChestType()) > 0) {
				if (WorldGen.genRand.Next(1, (int) (1f / loot.chance)) == 1) {
					int stack = WorldGen.genRand.Next(loot.numFrom, loot.numTo);
					if (stack <= 0) return true;
					
					return chest.AddItem(loot.item, stack);
				}
			}
			return false;
		}
		
		public static Tile GetTile(this Chest chest) {
			return Main.tile[chest.x, chest.y];
		}
		
		public static int getChestType(this Chest chest) {
			int y = chest.y;
			
			if (y < Main.worldSurface + 25.0) {
				return ChestType.SURFACE;
			} else if (y < Main.rockLayer) {
				return ChestType.UNDERGROUND;
			} else if (y < Main.maxTilesY - 250)  {
				return ChestType.CAVERN;
			} else {
				return ChestType.UNDERWORLD;
			}
		}
	}
}