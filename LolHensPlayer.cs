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
        public static bool flyingVertically = false;
        public static bool resetFlightTimer = false;

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

            resetFlightTimer = (player.grappling[0] >= 0
                || (player.grappling[0] == -1 && !player.tongued && (player.velocity.Y == 0f || player.sliding))
                || (player.pulley));

            flyingVertically = player.grappling[0] == -1 && !player.tongued && player.velocity.Y != 0f;
        }

        public override void ModifyDrawLayerList(System.Collections.Generic.List<PlayerLayer> list)
        {
            LolHensItem wings = Main.localPlayer.GetWingsItem();
            if (wings != null && wings.glowing) PlayerLayer.extraDrawInfo.colorArmorBody = new Color(255, 255, 255);
        }

        public virtual void PostHurt(bool pvp, bool quiet, int hitDirection, string deathtext, bool crit, float critMultiplier, ref double parsedDamage)
        {
            TConsole.Print("Dealt");
        }

        public virtual void PostKill(double damage, int hitDirection, bool pvp, string deathText)
        {
            TConsole.Print("Dealt");
        }
    }
}