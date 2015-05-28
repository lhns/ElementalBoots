using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LitJson;

using TAPI;
using Terraria;
using LolHens.Items;

namespace LolHens
{
    public class MWorld : TAPI.ModWorld
    {
        public override void WorldGenPostGen()
        {
            ChestInfo.OnChestsGenerate();
        }

        public static bool worldLoaded = false;

        public override void Initialize()
        {
            MBase.instance.OnInitializeWorld(this);
        }

        public void OnWorldLoaded()
        {
            try
            {
                Option retrogen = MBase.instance.options["retrogen"];

                if ((bool)retrogen.Value)
                {
                    retrogen.SetValue(false);

                    ChestInfo.OnChestsGenerate();
                }
            }
            catch (Exception e)
            {
                TConsole.Print(e);
            }
        }
    }
}