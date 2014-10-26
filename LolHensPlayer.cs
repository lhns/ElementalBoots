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
using LolHens.Items;

namespace LolHens
{
    public class LolHensPlayer : TAPI.ModPlayer
    {
        new public LolHensBase modBase;

        public static bool dead = false;
        private bool died = false;

        public override void Initialize()
        {
            modBase = base.modBase as LolHensBase;
        }

        public override void PostUpdate()
        {
            dead = false;
            if (player.dead) died = true;
            if (player.respawnTimer == 0 && died)
            {
                died = false;
                dead = true;
            }

            if (player.grappling[0] >= 0
                || (player.grappling[0] == -1 && !player.tongued && (player.velocity.Y == 0f || player.sliding))
                || (player.pulley)) ResetFlightTimer();

            if (player.grappling[0] == -1 && !player.tongued && player.velocity.Y != 0f) FlyVertically();
        }

        public override void ModifyDrawLayerList(System.Collections.Generic.List<PlayerLayer> list)
        {
            LolHensItem wings = Main.localPlayer.GetWingsItem();
            if (wings != null && wings.glowing) PlayerLayer.extraDrawInfo.colorArmorBody = new Color(255, 255, 255);
        }

        public void FlyVertically() { }

        public void ResetFlightTimer() { }
    }
}