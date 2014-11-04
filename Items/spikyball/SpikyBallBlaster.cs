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
    public class SpikyBallBlaster : LolHensGun
    {
        public override void Init()
        {
            base.Init();
            bulletOffset = 80;
        }
    }
}
