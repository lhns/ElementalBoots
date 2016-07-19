using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Tornado
{
    class TornadoInABottle: MItem
    {
        private static Color grayMultiplier = new Color(0.53f, 0.54f, 0.675f) * 1.3f;

        private bool jumpAgain = true, dJumpEffect = false, tornado = false;
        private int dJumpEffectTime = 0;

        private float lastWingTime = 0;
        private int lastRocketTime = 0;

        public override void SetDefaults()
        {
            item.name = "Tornado in a Bottle";
            item.maxStack = 1;
            item.value = 1 * Value.GOLD;
            item.rare = 1;
            item.accessory = true;
            item.toolTip = "Allows the holder to double jump";
        }

        public override void OnUnEquip(Player player)
        {
            base.OnUnEquip(player);

            ErrorLogger.Log("UNEQUIP");

            lastWingTime = 0;
            lastRocketTime = 0;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            if (player.grappling[0] == -1 && !player.tongued && (player.jump == 0 || player.frozen)
                || player.pulley
                || (!(player.itemAnimation > 0 && player.inventory[player.selectedItem].useStyle != 10)
                    && !player.mount.Active
                    && !(player.inventory[player.selectedItem].holdStyle == 1 && (!player.wet || !player.inventory[player.selectedItem].noWet))
                    && !(player.inventory[player.selectedItem].holdStyle == 2 && (!player.wet || !player.inventory[player.selectedItem].noWet))
                    && !(player.inventory[player.selectedItem].holdStyle == 3)
                    /*&& (player.inventory[player.selectedItem].subClass == null || !player.inventory[player.selectedItem].subClass.SetHoldFrame(player))*/
                    && player.grappling[0] >= 0)) dJumpEffect = false;

            if (!player.sandStorm) tornado = false;

            var onGround = player.velocity.Y == 0f || player.sliding;
            var onSlime = player.mount.Active && player.mount.Type == 3 && player.wetSlime > 0;

            if (player.mount.Active && player.mount.BlockExtraJumps) jumpAgain = false;
            else if (onGround) jumpAgain = true;

            if (player.controlJump && (player.releaseJump || (player.autoJump && onGround)))
            {
                if (player.grappling[0] >= 0) { if (player.releaseJump) jumpAgain = true; }
                else if (!player.pulley
                    && !player.tongued
                    && player.jump <= 0
                    && (onGround
                        || onSlime
                        || jumpAgain
                        || player.jumpAgainCloud
                        || player.jumpAgainBlizzard
                        || player.jumpAgainSandstorm
                        || player.jumpAgainFart
                        || player.jumpAgainSail
                        || (player.wet && player.accFlipper && (!player.mount.Active || player.mount.Type != 6))))
                {
                    var jump = !onSlime && jumpAgain;

                    if (onGround || (player.autoJump && player.justJumped)) jumpAgain = true;
                    else if (jump) jumpAgain = false;

                    if (onSlime || onGround || (player.wet && player.accFlipper))
                    {
                        // deactivate wings and rocket boots on first jump
                        if (player.wingTime > 0)
                        {
                            lastWingTime = player.wingTime;
                            player.wingTime = -1;
                        }
                        if (player.rocketTime > 0)
                        {
                            lastRocketTime = player.rocketTime;
                            player.rocketTime = 0;
                        }

                        player.velocity.Y = -Player.jumpSpeed * player.gravDir;
                        player.jump = Player.jumpHeight;
                        if (player.sliding) player.velocity.X = 3f * -(float)player.slideDir;
                        jump = false;
                    }
                    else if (jump)
                    {

                        dJumpEffect = true;
                        Main.PlaySound(16, (int)player.position.X, (int)player.position.Y, 1);
                        player.velocity.Y = -Player.jumpSpeed * player.gravDir;
                        player.jump = Player.jumpHeight * 3;
                    }
                }
            }

            // reactivate wings and rocket boots after double jump
            if (!jumpAgain && !dJumpEffect)
            {
                if (lastWingTime > 0)
                {
                    player.wingTime = lastWingTime;
                    lastWingTime = 0;
                }
                if (lastRocketTime > 0)
                {
                    player.rocketTime = lastRocketTime;
                    lastRocketTime = 0;
                }
            }

            // jump effects
            if (dJumpEffect)
            {
                dJumpEffectTime++;

                if (!jumpAgain && ((player.gravDir >= 1f && player.velocity.Y < 0f) || (player.gravDir <= -1f && player.velocity.Y > 0f)))
                {
                    var playerHeight = player.height;
                    if (player.gravDir == -1f)
                    {
                        playerHeight = -6;
                    }
                    var scale = ((float)player.jump / 75f + 1f) / 2f;
                    player.sandStorm = true;
                    tornado = true;

                    var tornadoWidth = dJumpEffectTime / 20f + 1;
                    for (var i = 0; i < tornadoWidth; i++)
                    {
                        var xOff = Main.rand.NextFloat() * player.width * tornadoWidth * 2 - player.width * tornadoWidth;
                        var gore = Gore.NewGore(new Vector2(player.Center.X + xOff - 18, player.position.Y + (float)(playerHeight / 2)), new Vector2(-player.velocity.X, -player.velocity.Y), Main.rand.Next(61, 64)/*Main.rand.Next(220, 223)*/, scale);
                        //Main.gore[gore].color = grayMultiplier;
                        Main.gore[gore].velocity = player.velocity * 0.3f * scale;
                        Main.gore[gore].velocity.X -= xOff / (player.width * tornadoWidth) * (1 + Main.rand.NextFloat() * tornadoWidth);
                        Main.gore[gore].velocity.Y += 3;
                        Main.gore[gore].alpha = 100;
                    }
                }

                // player turning effect
                /*if (player.sandStorm && tornado)
                {
                    if (player.miscCounter % 4 == 0 && player.itemAnimation == 0 && player.wingTime == 0f)
                    {
                        player.ChangeDir(player.direction * -1);
                        if (player.inventory[player.selectedItem].holdStyle == 2)
                        {
                            if (player.inventory[player.selectedItem].type == 946)
                            {
                                player.itemLocation.X = player.position.X + (float)player.width * 0.5f - (float)(16 * player.direction);
                            }
                            if (player.inventory[player.selectedItem].type == 186)
                            {
                                player.itemLocation.X = player.position.X + (float)player.width * 0.5f + (float)(6 * player.direction);
                                player.itemRotation = 0.79f * -(float)player.direction;
                            }
                        }
                    }
                    player.legFrameCounter = 0.0;
                    player.legFrame.Y = 0;
                }*/
                SpinPlayerEffect(player, true);
            }
            else
            {
                dJumpEffectTime = 0;
            }
        }

        private void SpinPlayerEffect(Player player, bool enabled)
        {
            if (enabled)
            {
                player.sandStorm = true;

                    if (player.miscCounter % 4 == 0 && player.itemAnimation == 0 && player.wingTime == 0f)
                    {
                        player.ChangeDir(player.direction * -1);
                        if (player.inventory[player.selectedItem].holdStyle == 2)
                        {
                            if (player.inventory[player.selectedItem].type == 946)
                            {
                                player.itemLocation.X = player.position.X + (float)player.width * 0.5f - (float)(16 * player.direction);
                            }
                            if (player.inventory[player.selectedItem].type == 186)
                            {
                                player.itemLocation.X = player.position.X + (float)player.width * 0.5f + (float)(6 * player.direction);
                                player.itemRotation = 0.79f * -(float)player.direction;
                            }
                        }
                    }
                    player.legFrameCounter = 0.0;
                    player.legFrame.Y = 0;
                }
        }
    }
}
