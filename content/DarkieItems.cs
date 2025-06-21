using HarmonyLib;
using NCMS.Utils;
using NeoModLoader.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkieCustomTraits.Content
{
    public class DarkieItems
    {
        private const string PathIcon = "ui/Icons/items";
        public static void Init()
        {
            loadCustomItems();

        }

        private static void loadCustomItems()
        {

            #region mjolnir
            //ItemAsset mjolnir = AssetManager.items.clone("Mjolnir", "$hammer");
            //mjolnir.id = "mjolnir";
            //mjolnir.material = "adamantine";
            //mjolnir.equipment_subtype = "hammer";
            //mjolnir.equipment_value = 500;
            //mjolnir.is_pool_weapon = false;
            //mjolnir.path_icon = $"{PathIcon}/w_Mjolnir_adamantine";
            //mjolnir.path_slash_animation = "effects/slashes/slash_hammer";
            //mjolnir.name_templates.AddTimes(30, "hammer_name");
            //mjolnir.name_templates.Add("weapon_name_city");
            //mjolnir.name_templates.Add("weapon_name_kingdom");
            //mjolnir.name_templates.Add("weapon_name_culture");
            //mjolnir.name_templates.Add("weapon_name_enemy_king");
            //mjolnir.name_templates.Add("weapon_name_enemy_kingdom");
            //mjolnir.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
            //    {
            //        "flame_hammer_name"
            //    });
            //mjolnir.group_id = "hammer";
            ////Stats
            //mjolnir.base_stats[CustomBaseStatsConstant.MultiplierDamage] = 1.0f;
            //mjolnir.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, 0.5f);
            //mjolnir.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.6f); //Percentage
            //mjolnir.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.2f);

            //mjolnir.item_modifier_ids = AssetLibrary<EquipmentAsset>.a<string>(new string[]
            //        {
            //        "stun"
            //        });

            //mjolnir.action_attack_target = new AttackAction(DarkieItemActions.thorWeaponAttackEffect);        //special attack action

            //mjolnir.equipment_value = 50;

            //AssetManager.items.list.AddItem(mjolnir);
            //LM.AddToCurrentLocale($"{mjolnir.id}", "Mjolnir");
            //LM.AddToCurrentLocale($"{mjolnir.id}_description", "The strongest weapon for the worthy.");
            #endregion

            #region teleport dagger
            //ItemAsset teleport = AssetManager.items.clone("TeleportDagger", "sword");   //clone sword
            //teleport.id = "TeleportDagger";
            //teleport.name_templates = Toolbox.splitStringIntoList(new string[]
            //{
            //  "sword_name#30",
            //  "sword_name_king#3",
            //  "weapon_name_city",
            //  "weapon_name_kingdom",
            //  "weapon_name_culture",
            //  "weapon_name_enemy_king",
            //  "weapon_name_enemy_kingdom"
            //});
            //teleport.action_attack_target = new AttackAction(DarkieItemActions.teleportDaggerAttackEffect);        //special attack action
            //teleport.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, 0.5f);
            //teleport.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.6f); //Percentage
            //teleport.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.2f);
            //teleport.equipment_value = 50;

            //AssetManager.items.list.AddItem(teleport);
            #endregion

        }
    }
}
