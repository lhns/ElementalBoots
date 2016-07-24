using ElementalBoots.Projectiles;
using Terraria;

namespace ElementalBoots.Buffs.Illuminant
{
    public class IlluminantCrystal: MBuff
    {
        public override void SetDefaults()
        {
            Name = "Illuminant Crystal";
            Tooltip = "Follows enemies and damages them";
            Debuff = true;
        }

        private int damage = 0;
        private float knockBack = 0;
        private MProjectile illuminantCrystal = null;

        public override void OnEquip(Player player)
        {
            /*Item item = trigger as Item;
            if (item != null)
            {
                damage = item.damage;
                knockBack = item.knockBack;
            }
            illuminantCrystal = ProjDef.byName["LolHens:IlluminantCrystal"].New(entity.Center.X, entity.Center.Y, 0, 0, damage, knockBack, entity.whoAmI);*/
        }
    }
}