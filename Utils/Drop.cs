using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;

namespace ElementalBoots
{
    class Drop
    {
        private int item;
        private double chance;
        private int from, to;

        public Drop(int item, float chance = 1f, int from = 1, int to = 1)
        {
            this.item = item;
            this.chance = chance;
            this.from = from;
            this.to = to;
        }

        public void DropAt(Rectangle rectangle)
        {
            if (Main.rand.NextDouble() <= chance)
            {
                var count = Main.rand.Next(from, to);
                if (count > 0)
                {
                    Item.NewItem(rectangle, item, count);

                }
            }
        }
    }
}
