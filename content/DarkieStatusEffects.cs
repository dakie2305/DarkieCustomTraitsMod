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
    public class DarkieStatusEffects
    {
        [Hotfixable]
        public static void Init()
        {
            loadCustomStatusEffects();
        }

        private static void loadCustomStatusEffects()
        {
            //Needed this material for status effects
            Material material = LibraryMaterials.instance.dict["mat_world_object_lit"];



            //I have risen from the ashes
            #region phoenix_rise_effect
            var phoenixRiseEffect = new StatusAsset()
            {
                id = "phoenix_rise_effect",
                render_priority = 5,
                duration = 5f,
                animated = true,
                is_animated_in_pause = true,
                can_be_flipped = true,
                use_parent_rotation = false,
                removed_on_damage = false,
                cancel_actor_job = true,
                need_visual_render = true,
                scale = 3.5f, //Scale of the effect
            };

            phoenixRiseEffect.locale_id = $"status_title_{phoenixRiseEffect.id}";
            phoenixRiseEffect.locale_description = $"status_description_{phoenixRiseEffect.id}";
            phoenixRiseEffect.tier = StatusTier.Advanced;

            //This is also needed for the sprite effect to show up correctly
            phoenixRiseEffect.material_id = "mat_world_object_lit";
            phoenixRiseEffect.material = material;

            phoenixRiseEffect.texture = "fx_phoenix"; // Make sure this folder exists in effects/
            phoenixRiseEffect.path_icon = "ui/Icons/iconPhoenix";
            phoenixRiseEffect.draw_light_area = true;
            phoenixRiseEffect.draw_light_size = 0.1f;

            phoenixRiseEffect.base_stats = new();
            phoenixRiseEffect.base_stats.set(CustomBaseStatsConstant.Damage, 50f);
            phoenixRiseEffect.base_stats.set(CustomBaseStatsConstant.Speed, 100f);

            var phoenixSprite = SpriteTextureLoader.getSpriteList($"effects/{phoenixRiseEffect.texture}", false);
            phoenixRiseEffect.sprite_list = phoenixSprite;
            AssetManager.status.add(phoenixRiseEffect);
            addToLocale(phoenixRiseEffect.id, "Phoenix Rise", "I have risen from the ashes");
            #endregion

            #region titan shifter
            var titanShifter = new StatusAsset()
            {
                id = "titan_shifter_effect",
                render_priority = 5,
                duration = 45f,
                animated = false,
                is_animated_in_pause = false,
                can_be_flipped = false,
                use_parent_rotation = false,
                removed_on_damage = false,

            };
            titanShifter.locale_id = $"status_title_{titanShifter.id}";
            titanShifter.locale_description = $"status_description_{titanShifter.id}";
            titanShifter.tier = StatusTier.Advanced;
            titanShifter.texture = "fx_titan_shifter";
            titanShifter.path_icon = "ui/Icons/iconTitanShifter"; //Well, I don't have custom icon for this
            titanShifter.tier = StatusTier.Advanced;
            var sprite = Resources.Load<Sprite>("effects/fx_titan_shifter"); //Folder

            titanShifter.opposite_status = new string[] { "ant_man_effect" };

            titanShifter.base_stats = new();
            titanShifter.base_stats.set(CustomBaseStatsConstant.Armor, 25f);
            titanShifter.base_stats.set(CustomBaseStatsConstant.Health, 500f);
            titanShifter.base_stats.set(CustomBaseStatsConstant.Damage, 50f);

            titanShifter.sprite_list = SpriteTextureLoader.getSpriteList($"effects/{titanShifter.texture}", false);
            titanShifter.action_on_receive = (WorldAction)Delegate.Combine(titanShifter.action_on_receive, new WorldAction(DarkieStatusEffectAction.titanShifterStatusSpecialEffect));
            titanShifter.action_finish = (WorldAction)Delegate.Combine(titanShifter.action_finish, new WorldAction(DarkieStatusEffectAction.titanShifterStatusOnFinish));
            AssetManager.status.add(titanShifter);
            addToLocale(titanShifter.id, "Titan Shifter", "One of the most dangerous titan shifter that will wreck everything!");
            #endregion

            //He is getting smaller
            #region ant_man_effect
            var antManEffect = new StatusAsset()
            {
                id = "ant_man_effect",
                render_priority = 5,
                duration = 30f,
                animated = true,
                is_animated_in_pause = false,
                can_be_flipped = false,
                use_parent_rotation = false,
                removed_on_damage = false,
            };

            antManEffect.locale_id = $"status_title_{antManEffect.id}";
            antManEffect.locale_description = $"status_description_{antManEffect.id}";
            antManEffect.tier = StatusTier.Advanced;

            antManEffect.texture = "fx_ant_man"; //Make sure this folder exists in effects/
            antManEffect.path_icon = "ui/Icons/iconAntMan";

            antManEffect.base_stats = new();
            antManEffect.base_stats.set(CustomBaseStatsConstant.Armor, 25f);
            antManEffect.base_stats.set(CustomBaseStatsConstant.Health, 40f);
            antManEffect.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.8f);
            antManEffect.base_stats.set(CustomBaseStatsConstant.Speed, 70f);
            antManEffect.base_stats.set(CustomBaseStatsConstant.Scale, -0.1f);

            antManEffect.opposite_status = new string[] { "titan_shifter_effect" };

            var antSprite = SpriteTextureLoader.getSpriteList($"effects/{antManEffect.texture}", false);
            antManEffect.sprite_list = antSprite;

            AssetManager.status.add(antManEffect);
            addToLocale(antManEffect.id, "Ant Man Effect", "He is getting smaller");
            #endregion

            //Don't piss him off
            #region wolf_attack_effect
            var wolfAttackEffect = new StatusAsset()
            {
                id = "wolf_attack_effect",
                render_priority = 5,
                duration = 5f,
                animated = true,
                is_animated_in_pause = true,
                can_be_flipped = true,
                use_parent_rotation = false,
                removed_on_damage = false,
                cancel_actor_job = false,
                need_visual_render = true,
                scale = 2.5f, //Scale of the effect
            };

            wolfAttackEffect.locale_id = $"status_title_{wolfAttackEffect.id}";
            wolfAttackEffect.locale_description = $"status_description_{wolfAttackEffect.id}";
            wolfAttackEffect.tier = StatusTier.Advanced;

            wolfAttackEffect.texture = "fx_wolf_form_attack"; // Make sure this folder exists in effects/
            wolfAttackEffect.path_icon = "ui/Icons/iconWolfAttack";

            //This is also needed for the sprite effect to show up correctly
            wolfAttackEffect.material_id = "mat_world_object_lit";
            wolfAttackEffect.material = material;

            wolfAttackEffect.base_stats = new();
            wolfAttackEffect.base_stats.set(CustomBaseStatsConstant.Damage, 50f);
            wolfAttackEffect.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.5f);
            wolfAttackEffect.base_stats.set(CustomBaseStatsConstant.Knockback, 1f);
            wolfAttackEffect.base_stats.set(CustomBaseStatsConstant.Scale, 0.01f);

            var wolfSprite = SpriteTextureLoader.getSpriteList($"effects/{wolfAttackEffect.texture}", false);
            wolfAttackEffect.sprite_list = wolfSprite;

            AssetManager.status.add(wolfAttackEffect);
            addToLocale(wolfAttackEffect.id, "Wolf Form", "Don't piss him off");
            #endregion


            //Sparkling
            #region sparkling_effect
            var sparklingEffect = new StatusAsset()
            {
                id = "sparkling_effect",
                render_priority = 5,
                duration = 15f,
                animated = true,
                is_animated_in_pause = false,
                can_be_flipped = true,
                use_parent_rotation = false,
                removed_on_damage = false,
                random_frame = true,
                cancel_actor_job = false,
                need_visual_render = true,
                scale = 1.5f,
            };

            sparklingEffect.locale_id = $"status_title_{sparklingEffect.id}";
            sparklingEffect.locale_description = $"status_description_{sparklingEffect.id}";
            sparklingEffect.tier = StatusTier.Advanced;

            sparklingEffect.texture = "fx_electric_sparkling"; // Make sure this folder exists in effects/
            sparklingEffect.path_icon = "ui/Icons/iconSparkling";

            sparklingEffect.material_id = "mat_world_object_lit";
            sparklingEffect.material = material;

            sparklingEffect.base_stats = new();
            sparklingEffect.base_stats.set(CustomBaseStatsConstant.Damage, 20f);
            sparklingEffect.base_stats.set(CustomBaseStatsConstant.Speed, 10f);
            sparklingEffect.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.2f);

            var sparklingSprite = SpriteTextureLoader.getSpriteList($"effects/{sparklingEffect.texture}", false);
            sparklingEffect.sprite_list = sparklingSprite;

            AssetManager.status.add(sparklingEffect);
            addToLocale(sparklingEffect.id, "Sparkling", "Sparkling");
            #endregion

            //Bleeding Out
            #region bleeding_effect
            var bleedingEffect = new StatusAsset()
            {
                id = "bleeding_effect",
                render_priority = 5,
                duration = 8f,
                animated = false,
                is_animated_in_pause = true,
                can_be_flipped = true,
                use_parent_rotation = false,
                removed_on_damage = false,
                cancel_actor_job = true,
                need_visual_render = true,
                scale = 2.0f,
                offset_y = 1.0f, //Higher
            };

            bleedingEffect.locale_id = $"status_title_{bleedingEffect.id}";
            bleedingEffect.locale_description = $"status_description_{bleedingEffect.id}";
            bleedingEffect.tier = StatusTier.Basic;

            bleedingEffect.texture = "fx_bleeding"; // Make sure this folder exists in effects/
            bleedingEffect.path_icon = "ui/Icons/iconBleeding";

            bleedingEffect.material_id = "mat_world_object_lit";
            bleedingEffect.material = material;

            bleedingEffect.base_stats = new();
            bleedingEffect.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, -0.3f);
            bleedingEffect.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, -0.1f);

            bleedingEffect.action = (WorldAction)Delegate.Combine(bleedingEffect.action, new WorldAction(DarkieStatusEffectAction.bleedingStatusSpecialEffect));

            var bleedingSprite = SpriteTextureLoader.getSpriteList($"effects/{bleedingEffect.texture}", false);
            bleedingEffect.sprite_list = bleedingSprite;

            AssetManager.status.add(bleedingEffect);
            addToLocale(bleedingEffect.id, "Bleeding", "Bleeding Out");
            #endregion

            //Let it gooo
            #region ice_storm_effect
            var iceStormEffect = new StatusAsset()
            {
                id = "ice_storm_effect",
                render_priority = 5,
                duration = 10f,
                animated = true,
                is_animated_in_pause = true,
                can_be_flipped = true,
                use_parent_rotation = false,
                removed_on_damage = false,
                cancel_actor_job = false,
                need_visual_render = true,
                scale = 1.0f,
            };

            iceStormEffect.locale_id = $"status_title_{iceStormEffect.id}";
            iceStormEffect.locale_description = $"status_description_{iceStormEffect.id}";
            iceStormEffect.tier = StatusTier.Advanced;

            iceStormEffect.texture = "fx_ice_storm_attack"; // Make sure this folder exists in effects/
            iceStormEffect.path_icon = "ui/Icons/iconIceStorm";

            iceStormEffect.material_id = "mat_world_object_lit";
            iceStormEffect.material = material;

            iceStormEffect.base_stats = new();
            iceStormEffect.base_stats.set(CustomBaseStatsConstant.Damage, 20f);
            iceStormEffect.base_stats.set(CustomBaseStatsConstant.Speed, 20f);

            var iceStormSprite = SpriteTextureLoader.getSpriteList($"effects/{iceStormEffect.texture}", false);
            iceStormEffect.sprite_list = iceStormSprite;

            AssetManager.status.add(iceStormEffect);
            addToLocale(iceStormEffect.id, "Ice Storm", "Let it gooo. Freeze them all");
            #endregion

        }

        private static void addToLocale(string id, string name, string description)
        {
            //Already have Locales folder, so this is no need anymore
            //LM.AddToCurrentLocale($"status_title_{id}", name);
            //LM.AddToCurrentLocale($"status_description_{id}", description);
        }

        public static Sprite[] getStatusSprites(string id)
        {
            var sprite = Resources.Load<Sprite>("effects/" + id);
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
