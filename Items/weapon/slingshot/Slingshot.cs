using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using TAPI.UIKit;
using Terraria;

namespace LolHens.Items
{
    public class Slingshot : LolHensItem
    {
        public bool shot = false;
        private Texture2D[] textures = new Texture2D[7];

        public override void InitType(Type type)
        {
            ItemDef.byName["Vanilla:Acorn"].MakeAmmo("Acorn");

            modBase.eventRegistry.Register((Event.ChestGenerated e) =>
            {
                if ((e.chestInfo.height == ChestInfo.Height.SURFACE && e.chestInfo.style == ChestInfo.Style.WOOD)
                    || e.chestInfo.style == ChestInfo.Style.LIVING_WOOD) e.chestInfo.AddLoot(item, 0.2f, true);
            });

            modBase.eventRegistry.Register((Event.ChestGenerated e) =>
            {
                if ((e.chestInfo.height == ChestInfo.Height.SURFACE && e.chestInfo.style == ChestInfo.Style.WOOD)
                    || e.chestInfo.style == ChestInfo.Style.LIVING_WOOD) e.chestInfo.AddLoot(ItemDef.byName["Vanilla:Acorn"], 0.6f, false, 10, 40);
            });
        }

        public override void InitTextures()
        {
            textures[0] = modBase.textures["Items/weapon/slingshot/Slingshot"];
            textures[1] = modBase.textures["Items/weapon/slingshot/Slingshot2"];
            textures[2] = modBase.textures["Items/weapon/slingshot/Slingshot3"];
            textures[3] = modBase.textures["Items/weapon/slingshot/Slingshot4"];
            textures[4] = modBase.textures["Items/weapon/slingshot/Slingshot5"];
            textures[5] = modBase.textures["Items/weapon/slingshot/Slingshot6"];
            textures[6] = modBase.textures["Items/weapon/slingshot/Slingshot7"];
        }

        public override bool? UseItem(Player player)
        {
            base.UseItem(player);

            if (usedPercent > 0.9)
            {
                Main.itemTexture[item.type] = textures[0];
            }
            else if (usedPercent > 0.8f)
            {
                Main.itemTexture[item.type] = textures[5];
            }
            else if (usedPercent > 0.7f)
            {
                Main.itemTexture[item.type] = textures[6];
                if (!shot)
                {
                    shot = true;
                    Vector2 vector = new Vector2(player.Center.X, player.position.Y);
                    Vector2 velocity = new Vector2((float)Main.mouseX + Main.screenPosition.X - vector.X, (float)Main.mouseY + Main.screenPosition.Y - vector.Y);
                    float hyp = (float)System.Math.Sqrt((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));
                    double deg = (Math.Atan((-velocity.Y) / (velocity.X * player.direction)) / (2 * Math.PI) * 360);
                    if (velocity.X * player.direction < 0) deg *= -1;
                    deg = Math.Max(Math.Min(deg, 50), -50);
                    double ang = deg / 360 * (2 * Math.PI);
                    velocity.X = (float)(Math.Cos(ang) * hyp) * player.direction;
                    velocity.Y = (float)(Math.Sin(ang) * hyp) * -1;
                    float norm = 20f / hyp;
                    velocity.X *= norm;
                    velocity.Y *= norm;
                    player.UseAmmo();
                    Projectile.NewProjectile(vector.X, vector.Y, velocity.X, velocity.Y, ProjDef.byName["LolHens:Acorn"].type, item.damage, item.knockBack, player.whoAmI);
                    Main.PlaySound(2, (int)vector.X, (int)vector.Y, 5);
                }
            }
            else
            {
                Main.itemTexture[item.type] = textures[(int)Math.Min(4, usedPercent * 8)];
            }
            return false;
        }

        public override void UseItemPost(Player player)
        {
            Main.itemTexture[item.type] = textures[0];
            shot = false;
        }

        public override void DrawItemSlotItem(ref SpriteBatch sb, ref ItemSlot slot, ref Item item, ref Texture2D texture, ref Color color, ref float scale)
        {
            scale *= 1.6f;
        }
    }
}