using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class FrozenTurtleShield : LolHensItem
    {
        public override void Effects(Player player)
        {
            Player localPlayer = Main.player[Main.myPlayer];

            player.knockbackResist = 0f;

            if (player.statLife > player.statLifeMax2 * player.paladinThreshold)
            {
                if (player == localPlayer) player.paladinGive = true;
                else if (player.miscCounter % 5 == 0)
                {
                    if (localPlayer.team == player.team && player.team != 0)
                    {
                        float distanceX = player.position.X - localPlayer.position.X;
                        float distanceY = player.position.Y - localPlayer.position.Y;
                        if (System.Math.Sqrt(distanceX * distanceX + distanceY * distanceY) < 800f) localPlayer.AddBuff(43, 10, true);
                    }
                }
            }

            if (player.statLife <= player.statLifeMax2 * 0.25f) player.AddBuff(62, 5, true);
        }
    }
}
