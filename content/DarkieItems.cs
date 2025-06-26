using HarmonyLib;
using NCMS.Utils;
using NeoModLoader.api.attributes;
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
        private const string PathSlash= "ui/effects/slashes";

        [Hotfixable]
        public static void Init()
        {
            loadCustomItems();
        }

        private static void loadCustomItems()
        {

            #region teleport dagger
            ItemAsset teleportDagger = AssetManager.items.clone("teleport_dagger", "$weapon");
            teleportDagger.id = "teleport_dagger";
            teleportDagger.material = "adamantine"; //Since they are special weapon, I think this is suitable, and I don't have time to use other materials
            teleportDagger.translation_key = "Teleport Dagger";
            teleportDagger.equipment_subtype = "teleport_dagger";
            teleportDagger.group_id = "sword";
            teleportDagger.animated = false;
            teleportDagger.is_pool_weapon = false;
            teleportDagger.unlock(true);

            teleportDagger.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");

            teleportDagger.base_stats = new();
            teleportDagger.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, 1.0f);
            teleportDagger.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 1.0f); //Percentage
            teleportDagger.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.2f);
            teleportDagger.base_stats.set(CustomBaseStatsConstant.ConstructionSpeed, 50f);
            teleportDagger.base_stats.set(CustomBaseStatsConstant.MultiplierMana, 0.5f);

            teleportDagger.equipment_value = 3000;
            teleportDagger.special_effect_interval = 0.4f;
            teleportDagger.quality = Rarity.R3_Legendary;
            teleportDagger.equipment_type = EquipmentType.Weapon;
            teleportDagger.name_class = "item_class_weapon";

            teleportDagger.path_slash_animation = "effects/slashes/slash_sword";
            teleportDagger.path_icon = $"{PathIcon}/icon_teleport_dagger"; //I do not have separate sprite for icon, I use also just use that
            teleportDagger.path_gameplay_sprite = $"weapons/{teleportDagger.id}"; //Make sure image share same name as id
            teleportDagger.gameplay_sprites = getWeaponSprites(teleportDagger.id); //Make sure this path is also valid

            teleportDagger.action_attack_target = new AttackAction(DarkieItemActions.teleportDaggerAttackEffect);        //special attack action
            AssetManager.items.list.AddItem(teleportDagger);
            addToLocale(teleportDagger.id, teleportDagger.translation_key, "The fastest dagger with teleport attack!");
            #endregion

            //The strongest weapon for the worthy.
            #region mjolnir
            ItemAsset mjolnir = AssetManager.items.clone("mjolnir", "$hammer");
            mjolnir.id = "mjolnir";
            mjolnir.material = "adamantine";
            mjolnir.translation_key = "Mjölnir";
            mjolnir.equipment_subtype = "hammer";
            mjolnir.group_id = "hammer";
            mjolnir.animated = false;
            mjolnir.is_pool_weapon = false;
            mjolnir.unlock(true);

            mjolnir.base_stats = new();
            mjolnir.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 1.0f);
            mjolnir.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, 0.5f);
            mjolnir.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.6f);
            mjolnir.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 1.0f);
            mjolnir.base_stats.set(CustomBaseStatsConstant.MultiplierMana, 0.5f);
            mjolnir.base_stats.set(CustomBaseStatsConstant.Knockback, 10f);
            mjolnir.base_stats.set(CustomBaseStatsConstant.Range, 10f);

            mjolnir.equipment_value = 20000;
            mjolnir.special_effect_interval = 0.3f;
            mjolnir.quality = Rarity.R3_Legendary;
            mjolnir.equipment_type = EquipmentType.Weapon;
            mjolnir.name_class = "item_class_weapon";

            mjolnir.path_slash_animation = "effects/slashes/slash_hammer";
            mjolnir.path_icon = $"{PathIcon}/icon_mjolnir";
            mjolnir.path_gameplay_sprite = $"weapons/{mjolnir.id}"; //Make sure image share same name as id
            mjolnir.gameplay_sprites = getWeaponSprites(mjolnir.id); //Make sure this path is also valid

            mjolnir.item_modifier_ids = AssetLibrary<EquipmentAsset>.a<string>(new string[]
                {
                    "stunned"
                });

            mjolnir.name_templates = AssetLibrary<EquipmentAsset>.l<string>(new string[]
                {
                    "hammer_name"
                });
            mjolnir.action_attack_target = new AttackAction(DarkieItemActions.thorWeaponAttackEffect);

            AssetManager.items.list.AddItem(mjolnir);
            addToLocale(mjolnir.id, mjolnir.translation_key, "The strongest weapon for only the worthy, with the power to summon lightning to strike foes!");
            #endregion

            //Ice sword, freeze enemies.
            #region ice sword
            ItemAsset iceSword = AssetManager.items.clone("ice_sword", "$weapon");
            iceSword.id = "ice_sword";
            iceSword.material = "adamantine";
            iceSword.translation_key = "Ice Sword";
            iceSword.equipment_subtype = "ice_sword";
            iceSword.group_id = "sword";
            iceSword.animated = false;
            iceSword.unlock(true);

            iceSword.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");

            iceSword.base_stats = new();
            iceSword.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.25f);
            iceSword.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, 0.27f);
            iceSword.base_stats.set(CustomBaseStatsConstant.MultiplierMana, 0.4f);

            iceSword.equipment_value = 5000;
            iceSword.special_effect_interval = 0.1f;
            iceSword.quality = Rarity.R3_Legendary;
            iceSword.equipment_type = EquipmentType.Weapon;
            iceSword.name_class = "item_class_weapon";

            iceSword.path_slash_animation = "effects/slashes/iceSwordEffect";
            iceSword.path_icon = $"{PathIcon}/icon_ice_sword";
            iceSword.path_gameplay_sprite = $"weapons/{iceSword.id}"; //Make sure image share same name as id
            iceSword.gameplay_sprites = getWeaponSprites(iceSword.id); //Make sure this path is also valid

            iceSword.action_attack_target = new AttackAction(DarkieItemActions.iceSwordAttack);

            AssetManager.items.list.AddItem(iceSword);
            addToLocale(iceSword.id, iceSword.translation_key, "A blade forged from eternal frost. Freezes all it touches.");
            #endregion

            //Shatteringly fast blade that causes bleeding.
            #region glass sword
            ItemAsset glassSword = AssetManager.items.clone("glass_sword", "$weapon");
            glassSword.id = "glass_sword";
            glassSword.material = "adamantine";
            glassSword.translation_key = "Glass Sword";
            glassSword.equipment_subtype = "glass_sword";
            glassSword.group_id = "sword";
            glassSword.animated = false;
            glassSword.unlock(true);

            glassSword.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");

            glassSword.base_stats = new();
            glassSword.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.25f);
            glassSword.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, 0.27f);

            glassSword.equipment_value = 5000;
            glassSword.special_effect_interval = 0.1f;
            glassSword.quality = Rarity.R3_Legendary;
            glassSword.equipment_type = EquipmentType.Weapon;
            glassSword.name_class = "item_class_weapon";

            glassSword.path_slash_animation = "effects/slashes/glassSwordEffect";
            glassSword.path_icon = $"{PathIcon}/icon_glass_sword";
            glassSword.path_gameplay_sprite = $"weapons/{glassSword.id}"; //Make sure image share same name as id
            glassSword.gameplay_sprites = getWeaponSprites(glassSword.id); //Make sure this path is also valid

            glassSword.action_attack_target = new AttackAction(DarkieItemActions.glassSwordAttack);

            AssetManager.items.list.AddItem(glassSword);
            addToLocale(glassSword.id, glassSword.translation_key, "A deadly blade that can slow and inflict bleeding on every slash.");
            #endregion

            addWeaponsToWorld();

        }


        public static void addWeaponsToWorld()
        {
            //now walker can spawn with ice sword
            var walker = AssetManager.actor_library.get("cold_one");
            if (walker != null)
            {
                walker.use_items = true;
                walker.default_weapons = AssetLibrary<ActorAsset>.a<string>(new string[]
                {
                    "ice_hammer", "ice_sword"
                });
                walker.take_items = false;
            }
        }

        private static void addToLocale(string id, string translation_key, string description)
        {
            //Already have Locales folder, so this is no need anymore
            //LM.AddToCurrentLocale(translation_key, translation_key);
            //LM.AddToCurrentLocale($"{id}_description", description);
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
