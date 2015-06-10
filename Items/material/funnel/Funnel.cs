using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitJson;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class Funnel : LolHensItem
    {
        public override void InitType(Type type)
        {
            if (type != typeof(Funnel)) return;

            Events.registry.Register((Events.ChestGenerated e) =>
            {
                if (e.chestInfo.style == ChestInfo.Style.WATER) e.chestInfo.AddLoot(item, 0.4f, false);
            });

            if (modBase.options.GetBoolean("WeatherBottleConversion"))
            {
                ItemDef.byName["Vanilla:Cloud in a Bottle"].AddRecipe(@"{
                    ""items"": { ""g:WeatherBottle"": 1, ""LolHens:Funnel"": 1, ""Cloud"": 10 },
                    ""tiles"": [ ""Inventor's Workshop"" ],
                    ""creates"": 1
                }");

                ItemDef.byName["Vanilla:Sandstorm in a Bottle"].AddRecipe(@"{
                    ""items"": { ""g:WeatherBottle"": 1, ""LolHens:Funnel"": 1, ""Sand Block"": 20 },
                    ""tiles"": [ ""Inventor's Workshop"" ],
                    ""creates"": 1
                }");

                ItemDef.byName["Vanilla:Blizzard in a Bottle"].AddRecipe(@"{
                    ""items"": { ""g:WeatherBottle"": 1, ""LolHens:Funnel"": 1, ""Snow Block"": 20 },
                    ""tiles"": [ ""Inventor's Workshop"" ],
                    ""creates"": 1
                }");

                ItemDef.byName["LolHens:TornadoInABottle"].AddRecipe(@"{
                    ""items"": { ""g:WeatherBottle"": 1, ""LolHens:Funnel"": 1, ""Rain Cloud"": 20 },
                    ""tiles"": [ ""Inventor's Workshop"" ],
                    ""creates"": 1
                }");
            }
        }
    }
}
