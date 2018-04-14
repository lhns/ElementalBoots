using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace ElementalBoots
{
    public class ChestInfo
    {
        public readonly Chest chest;
        public readonly Tile tile;
        public readonly int style;
        public readonly int height;

        private bool rareItem = false;

        public ChestInfo(Chest chest)
        {
            this.chest = chest;
            this.tile = Main.tile[chest.x, chest.y];
            this.style = tile.frameX/36;
            this.height = GetHeight();
        }

        private int GetHeight()
        {
            int height;
            int y = chest.y;

            if (style == Style.SKYWARE)
            {
                height = Height.SKY;
            }
            else if (y < Main.worldSurface + 25)
            {
                height = Height.SURFACE;
            }
            else if (y < Main.rockLayer)
            {
                height = Height.UNDERGROUND;
            }
            else if (y < Main.maxTilesY - 250)
            {
                height = Height.CAVERN;
            }
            else
            {
                height = Height.UNDERWORLD;
            }

            return height;
        }

        public bool HasRareItem()
        {
            return rareItem;
        }

        public bool AddItem(Item item, int stack)
        {
            if (chest == null || item == null) return false;
            if (chest.item == null) return false;

            for (int i = 0; i < chest.item.Length; i++)
            {
                if (chest.item[i] == null) continue;

                if (chest.item[i].type == 0)
                {
                    chest.item[i].SetDefaults(item.type, false);
                    chest.item[i].stack = stack;

                    return true;
                }
            }

            return false;
        }

        public bool AddLoot(Item item, float chance = 1, bool rare = false, int numFrom = 1, int numTo = 1)
        {
            if (!(rare && rareItem) && WorldGen.genRand.Next(1, (int) (1f/chance)) == 1)
            {
                int stack = WorldGen.genRand.Next(numFrom, numTo);
                if (stack <= 0) return true;

                if (rare) rareItem = true;

                return AddItem(item, stack);
            }
            return true;
        }

        public static void OnChestsGenerate()
        {
            foreach (Chest chest in Main.chest)
            {
                if (chest == null) continue;

                Events.Registry().Call(new Events.ChestGenerated(new ChestInfo(chest)));
            }
        }

        public static class Style
        {
            public const int WOOD = 0;
            public const int GOLD = 1;
            public const int GOLD_LOCKED = 2;
            public const int SHADOW = 3;
            public const int SHADOW_LOCKED = 4;
            public const int BARREL = 5;
            public const int TRASHCAN = 6;
            public const int EBONWOOD = 7;
            public const int RICH_MAHOGANY = 8;
            public const int PEARLWOOD = 9;
            public const int IVY = 10;
            public const int ICE = 11;
            public const int LIVING_WOOD = 12;
            public const int SKYWARE = 13;
            public const int SHADEWOOD = 14;
            public const int WEB_COVERED = 15;
            public const int LIHZAHRD = 16;
            public const int WATER = 17;
            public const int JUNGLE = 18;
            public const int CORRUPTION = 19;
            public const int CRIMSON = 20;
            public const int HALLOWED = 21;
            public const int FROZEN = 22;
            public const int JUNGLE_LOCKED = 23;
            public const int CORRUPTION_LOCKED = 24;
            public const int CRIMSON_LOCKED = 25;
            public const int HALLOWED_LOCKED = 26;
            public const int FROZEN_LOCKED = 27;
            public const int DYNASTY = 28;
            public const int HONEY = 29;
            public const int STEAMPUNK = 30;
            public const int PALM_WOOD = 31;
            public const int MUSHROOM = 32;
            public const int BOREAL_WOOD = 33;
            public const int SLIME = 34;
            public const int DUNGEON_GREEN = 35;
            public const int DUNGEON_GREEN_LOCKED = 36;
            public const int DUNGEON_PINK = 37;
            public const int DUNGEON_PINK_LOCKED = 38;
            public const int DUNGEON_BLUE = 39;
            public const int DUNGEON_BLUE_LOCKED = 40;
            public const int BONE = 41;
            public const int CACTUS = 42;
            public const int FLESH = 43;
            public const int OBSIDIAN = 44;
            public const int PUMPKIN = 45;
            public const int SPOOKY = 56;
            public const int GLASS = 47;
            public const int MARTIAN = 48;
            public const int METEORITE = 49;
            public const int GRANITE = 50;
            public const int MARBLE = 51;
            public const int CRYSTAL = 52;
            public const int GOLDEN = 53;
        }

        public static class Height
        {
            public const int UNDERWORLD = 0;
            public const int CAVERN = 1;
            public const int UNDERGROUND = 2;
            public const int SURFACE = 3;
            public const int SKY = 4;
        }
    }
}