using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria;
using TAPI;

namespace LolHens.Items
{
    public class IlluminantStaff : LolHensGun
    {
        public override void Init()
        {
            base.Init();
            diagonalTexture = true;
            bulletOffset.X = 40;
        }

        public override bool PreShootCustom(Player player, Vector2 position, Vector2 direction, int projType)
        {
            for (int m = 0; m < 5; m++)
            {
                Vector2 newDir = direction.Rotate((Main.rand.NextFloat() - 0.5f) * 2f);
                int dustID = Dust.NewDust(position, 0, 0, 62, newDir.X * 20, newDir.Y * 20, 100, default(Color), 2f);
            }
            return true; 
        }
    }
}
