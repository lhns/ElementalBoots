using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TAPI;
using Terraria;
using LolHens.Items;

namespace LolHens
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
            this.style = tile.frameX / 36;
            this.height = GetHeight();
        }

        private int GetHeight()
        {
            int height;
            int y = chest.y;

            if (style == Style.SKY)
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
            if (!(rare && rareItem) && WorldGen.genRand.Next(1, (int)(1f / chance)) == 1)
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

                LolHensEvent.ChestGenerated.Call(LolHensBase.instance.eventRegistry, new ChestInfo(chest));
            }
        }

        public static class Style
        {
            public const int WOOD = 0;
            public const int GOLD = 1;
            public const int GOLD_LOCKED = 2;
            public const int HELL = 4;
            public const int JUNGLE = 10;
            public const int ICE = 11;
            public const int SKY = 13;
            public const int SPIDERNEST = 15;
            public const int TEMPLE = 16;
            public const int WATER = 17;
            public const int DUNGEON_JUNGLE = 23;
            public const int DUNGEON_CORRUPTION = 24;
            public const int DUNGEON_CRIMSON = 25;
            public const int DUNGEON_HALLOW = 26;
            public const int DUNGEON_ICE = 27;
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
