﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using TAPI;

namespace LolHens.Items
{
    class TornadoInABottle : LolHensItem
    {
        private static Color grayMultiplier = new Color(0.53f, 0.54f, 0.675f) * 1.3f;

        private bool jumpAgain = true, dJumpEffect = false;
        private int dJumpEffectTime = 0;

        public override void Effects(Player player)
        {
            base.Effects(player);

            if (player.grappling[0] == -1 && !player.tongued && (player.jump == 0 || player.frozen)
                || player.pulley
                || (!(player.itemAnimation > 0 && player.inventory[player.selectedItem].useStyle != 10)
                    && !player.mount.Active
                    && !(player.inventory[player.selectedItem].holdStyle == 1 && (!player.wet || !player.inventory[player.selectedItem].noWet))
                    && !(player.inventory[player.selectedItem].holdStyle == 2 && (!player.wet || !player.inventory[player.selectedItem].noWet))
                    && !(player.inventory[player.selectedItem].holdStyle == 3)
                    && (player.inventory[player.selectedItem].subClass == null || !player.inventory[player.selectedItem].subClass.SetHoldFrame(player))
                    && player.grappling[0] >= 0)) dJumpEffect = false;


            bool onGround = player.velocity.Y == 0f || player.sliding;
            bool onSlime = player.mount.Active && player.mount.Type == 3 && player.wetSlime > 0;

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
                        || player.jumpAgain
                        || player.jumpAgain2
                        || player.jumpAgain3
                        || player.jumpAgain4
                        || (player.wet && player.accFlipper && (!player.mount.Active || player.mount.Type != 6))))
                {
                    bool jump = !onSlime && jumpAgain;

                    if (onGround || (player.autoJump && player.justJumped)) jumpAgain = true;
                    else if (jump) jumpAgain = false;

                    if (onSlime || onGround || (player.wet && player.accFlipper))
                    {
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

            if (dJumpEffect)
            {
                dJumpEffectTime++;

                if (!jumpAgain && ((player.gravDir == 1f && player.velocity.Y < 0f) || (player.gravDir == -1f && player.velocity.Y > 0f)))
                {
                    int playerHeight = player.height;
                    if (player.gravDir == -1f)
                    {
                        playerHeight = -6;
                    }
                    float scale = ((float)player.jump / 75f + 1f) / 2f;
                    player.sandStorm = true;

                    float tornadoWidth = dJumpEffectTime / 20f + 1;
                    for (int i = 0; i < tornadoWidth; i++)
                    {
                        float xOff = Main.rand.NextFloat() * player.width * tornadoWidth * 2 - player.width * tornadoWidth;
                        int gore = Gore.NewGore(new Vector2(player.Center.X + xOff - 18, player.position.Y + (float)(playerHeight / 2)), new Vector2(-player.velocity.X, -player.velocity.Y), Main.rand.Next(61, 64)/*Main.rand.Next(220, 223)*/, scale);
                        Main.gore[gore].color = grayMultiplier;
                        Main.gore[gore].velocity = player.velocity * 0.3f * scale;
                        Main.gore[gore].velocity.X -= xOff / (player.width * tornadoWidth) * (1 + Main.rand.NextFloat() * tornadoWidth);
                        Main.gore[gore].velocity.Y += 3;
                        Main.gore[gore].alpha = 100;
                    }
                }

                if (player.sandStorm)
                {
                    if (player.miscCounter % 4 == 0 && player.itemAnimation == 0)
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
            else
            {
                dJumpEffectTime = 0;
            }
        }
    }
}
