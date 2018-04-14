using System.Security.Cryptography.X509Certificates;
using Terraria;

namespace ElementalBoots
{
    public interface IEquipped
    {
        void OnEquip(Player player);

        void OnUnEquip(Player player);

        void SetLastEquippedTime(long time);

        long GetLastEquippedTime();
    }
}