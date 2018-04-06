using ElementalBoots.Projectiles;
using Terraria;

namespace ElementalBoots.Buffs.Illuminant
{
    public class IlluminantCrystal: MBuff
    {
        //public Item Trigger { get; }

        public override void SetDefaults()
        {
            Debuff = true;
        }

        public override void OnEquip(Player player)
        {
            int damage = 30;
            float knockBack = 1;

            /*var trigger = Utils.GetItem(player.selectedItem);
            if (trigger != null)
            {
                damage = trigger.damage;
                knockBack = trigger.knockBack;
            }*/

            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, Utils.GetProjectile(mod, "IlluminantCrystal").type, damage, knockBack, player.whoAmI);
        }
    }
}