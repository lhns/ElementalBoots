using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementalBoots.Tiles.InventorsWorkshop
{
    public class InventorsWorkshop: MTile
    {
        public override void SetDefaults2()
        {
            FrameImportant = true;

            Width = 4;
            Height = 4;
            Origin = new Point16(2, 3);

            SolidTop = true;
            Table = true;

            AddMapEntry(new Color(200, 50, 50), "Inventor's Workshop");

            dustType = 1;
            disableSmartCursor = true;

            adjTiles = new int[]
            {
                TileID.TinkerersWorkbench,
                TileID.Bookcases,
                TileID.WorkBenches
            };
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 48, mod.ItemType("InventorsWorkshopItem"));
        }
    }
}