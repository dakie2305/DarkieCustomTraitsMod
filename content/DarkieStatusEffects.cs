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
            var titanShifter = new StatusAsset()
            {
                id = "titan_shifter_effect",
                render_priority = 5,
                duration = 45f,
                animated = true,
                is_animated_in_pause = true,
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

            titanShifter.sprite_list  = new Sprite[] { sprite };
            titanShifter.action_on_receive = (WorldAction)Delegate.Combine(titanShifter.action_on_receive, new WorldAction(DarkieStatusEffectAction.titanShifterStatusSpecialEffect));
            titanShifter.action_finish = (WorldAction)Delegate.Combine(titanShifter.action_finish, new WorldAction(DarkieStatusEffectAction.titanShifterStatusOnFinish));
            AssetManager.status.add(titanShifter);
            addToLocale(titanShifter.id, "Titan Shifter", "One of the most dangerous titan shifter that will wreck everything!");

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

            var antSprite = Resources.Load<Sprite>("effects/fx_ant_man");
            antManEffect.sprite_list = new Sprite[] { antSprite };

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
                is_animated_in_pause = false,
                can_be_flipped = false,
                use_parent_rotation = false,
                removed_on_damage = false,
            };

            wolfAttackEffect.locale_id = $"status_title_{wolfAttackEffect.id}";
            wolfAttackEffect.locale_description = $"status_description_{wolfAttackEffect.id}";
            wolfAttackEffect.tier = StatusTier.Advanced;

            wolfAttackEffect.texture = "fx_wolf_form_attack"; // Make sure this folder exists in effects/
            wolfAttackEffect.path_icon = "ui/Icons/iconWolfAttack";

            wolfAttackEffect.base_stats = new();
            wolfAttackEffect.base_stats.set(CustomBaseStatsConstant.Damage, 50f);
            wolfAttackEffect.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.5f);
            wolfAttackEffect.base_stats.set(CustomBaseStatsConstant.Knockback, 1f);
            wolfAttackEffect.base_stats.set(CustomBaseStatsConstant.Scale, 0.01f);

            var wolfSprite = Resources.Load<Sprite>("effects/fx_wolf_form_attack");
            wolfAttackEffect.sprite_list = new Sprite[] { wolfSprite };

            AssetManager.status.add(wolfAttackEffect);
            addToLocale(wolfAttackEffect.id, "Wolf Form", "Don't piss him off");
            #endregion

        }

        private static void addToLocale(string id, string name, string description)
        {
            LM.AddToCurrentLocale($"status_title_{id}", name);
            LM.AddToCurrentLocale($"status_description_{id}", description);
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
