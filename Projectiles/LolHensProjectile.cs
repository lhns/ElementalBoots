using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Projectiles
{
    public abstract class LolHensProjectile : ModProjectile
    {
        new public LolHensBase modBase;

        public sealed override void Initialize()
        {
            base.modBase = projectile.def.modBase;
            modBase = base.modBase as LolHensBase;
            modBase.projectiles.Add(this);
            Init();
            if (!Main.dedServ) InitTextures();
            InitPost();
        }

        public virtual void Init() { }

        public virtual void InitTextures() { }

        public virtual void InitPost() { }

        public void Load(LolHensBase modBase)
        {
            this.modBase = modBase;
            Load();
            PreLoadTextures();
            if (!Main.dedServ) LoadTextures();
        }

        protected virtual void Load() { }

        public virtual void PreLoadTextures() { }

        public virtual void LoadTextures() { }

        public static Color GetLightColor(Vector2 position)
        {
            return Lighting.GetColor((int)(position.X / 16f), (int)(position.Y / 16f));
        }

        public static Vector2 RotateVector(Vector2 vecToRot, Vector2 origin, float rot)
        {
            float newPosX = (float)(Math.Cos(rot) * (vecToRot.X - origin.X) - Math.Sin(rot) * (vecToRot.Y - origin.Y) + origin.X);
            float newPosY = (float)(Math.Sin(rot) * (vecToRot.X - origin.X) + Math.Cos(rot) * (vecToRot.Y - origin.Y) + origin.Y);
            return new Vector2(newPosX, newPosY);
        }
    }
}