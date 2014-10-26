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

namespace LolHens
{
    public class LolHensPlayer : TAPI.ModPlayer
    {
        public LolHensBase lolhensBase = null;

        public static int playerDeaths = 0;
        private bool playerDied = false;

        public LolHensPlayer()
        {
            if (modBase is LolHensBase) lolhensBase = (LolHensBase)modBase;
        }

        public override void PostUpdate()
        {
            if (player.dead) playerDied = true;
            if (player.respawnTimer == 0 && playerDied)
            {
                playerDied = false;
                playerDeaths++;
            }

            /*if (player.grappling[0] >= 0
                || (player.grappling[0] == -1 && !player.tongued && (player.velocity.Y == 0f || player.sliding))
                || (player.pulley)) ResetFlightTimer();*/

            if (player.grappling[0] == -1 && !player.tongued && player.velocity.Y != 0f) FlyVertically();
        }

        public override void ModifyDrawLayerList(System.Collections.Generic.List<PlayerLayer> list)
        {
            if (LolHensBase.wings.ContainsKey(Main.localPlayer.wings) && LolHensBase.wings[Main.localPlayer.wings].glow)
                PlayerLayer.extraDrawInfo.colorArmorBody = new Color(255, 255, 255);
        }

        public virtual void FlyVertically()
        {
            /* NOT WORKING
            if (LolHensBase.wings.ContainsKey(player.wings)) player.moveSpeedMax = LolHensBase.wings[player.wings].speed;*/
        }

        public virtual void ResetFlightTimer()
        {
            if (LolHensBase.wings.ContainsKey(player.wings)) player.wingTime = LolHensBase.wings[player.wings].time;
        }
    }
}