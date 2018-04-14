using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementalBoots.Tiles
{
    abstract class MTile: ModTile
    {
        public bool FrameImportant
        {
            get { return Main.tileFrameImportant[Type]; }
            set { Main.tileFrameImportant[Type] = value; }
        }

        public bool Solid
        {
            get { return Main.tileSolid[Type]; }
            set { Main.tileSolid[Type] = value; }
        }

        public bool SolidTop
        {
            get { return Main.tileSolidTop[Type]; }
            set { Main.tileSolidTop[Type] = value; }
        }

        public bool NoAttach
        {
            get { return Main.tileNoAttach[Type]; }
            set { Main.tileNoAttach[Type] = value; }
        }

        public bool Table
        {
            get { return Main.tileTable[Type]; }
            set { Main.tileTable[Type] = value; }
        }

        public bool LavaDeath
        {
            get { return Main.tileLavaDeath[Type]; }
            set { Main.tileLavaDeath[Type] = value; }
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public Point16 Origin { get; set; }

        public int[] CoordinateHeights { get; set; }

        private int _coordinateWidth = 16;

        public int CoordinateWidth
        {
            get { return _coordinateWidth; }
            set { _coordinateWidth = value; }
        }

        private int _coordinatePadding = 2;

        public int CoordinatePadding
        {
            get { return _coordinatePadding; }
            set { _coordinatePadding = value; }
        }

        public sealed override void SetDefaults()
        {
            SetDefaults2();

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);

            TileObjectData.newTile.Width = Width;
            TileObjectData.newTile.Height = Height;
            TileObjectData.newTile.Origin = Origin;


            if (CoordinateHeights == null)
            {
                CoordinateHeights = new int[Height];
                for (var i = 0; i < CoordinateHeights.Length; i++) CoordinateHeights[i] = 16;
            }
            TileObjectData.newTile.CoordinateHeights = CoordinateHeights;
            TileObjectData.newTile.CoordinateWidth = CoordinateWidth;
            TileObjectData.newTile.CoordinatePadding = CoordinatePadding;

            TileObjectData.newTile.LavaDeath = LavaDeath;

            TileObjectData.addTile(Type);
        }

        public virtual void SetDefaults2()
        {
        }
    }
}