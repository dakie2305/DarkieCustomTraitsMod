using HarmonyLib;
using NCMS.Utils;
using NeoModLoader.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            ItemAsset teleportDagger = AssetManager.items.clone("teleport_dagger", "$weapon");
            teleportDagger.id = "teleport_dagger";
            teleportDagger.material = "adamantine"; //Since they are special weapon, I think this is suitable, and I don't have time to use other materials
            teleportDagger.translation_key = "Teleport Dagger";
            teleportDagger.equipment_subtype = "teleport_dagger";
            teleportDagger.group_id = "sword";
            teleportDagger.animated = false;

            teleportDagger.base_stats = new();
            teleportDagger.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, 0.5f);
            teleportDagger.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.6f); //Percentage
            teleportDagger.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.2f);

            teleportDagger.path_slash_animation = "effects/slashes/slash_sword";

            teleportDagger.equipment_value = 3000;
            teleportDagger.special_effect_interval = 0.4f;
            teleportDagger.quality = Rarity.R3_Legendary;
            teleportDagger.equipment_type = EquipmentType.Weapon;
            teleportDagger.name_class = "item_class_weapon";

            teleportDagger.path_icon = $"{PathIcon}/icon_teleport_dagger"; //I do not have separate sprite for icon, I use also just use that
            teleportDagger.path_gameplay_sprite = $"weapons/{teleportDagger.id}"; //Make sure image share same name as id
            teleportDagger.gameplay_sprites = getWeaponSprites(teleportDagger.id); //Make sure this path is also valid

            teleportDagger.action_attack_target = new AttackAction(DarkieItemActions.teleportDaggerAttackEffect);        //special attack action
            AssetManager.items.list.AddItem(teleportDagger);
            addToLocale(teleportDagger.id, teleportDagger.translation_key, "The fastest dagger with teleport attack!");
            #endregion

        }

        private static void addToLocale(string id, string translation_key, string description)
        {
            LM.AddToCurrentLocale(translation_key, translation_key);
            LM.AddToCurrentLocale($"{id}_description", description);
        }

        public static Sprite[] getWeaponSprites(string id)
        {
            var sprite = Resources.Load<Sprite>("weapons/" + id);
            if (sprite != null)
                return new Sprite[] { sprite };
            else
            {
                DarkieTraitsMain.LogError("Can not find weapon sprite for weapon with this id: " + id);
                return Array.Empty<Sprite>();
            }
        }
    }
}
