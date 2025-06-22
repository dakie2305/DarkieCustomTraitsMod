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
                render_priority = 1,
                duration = 60f,
                animated = true,
                is_animated_in_pause = true,
                can_be_flipped = false,
                use_parent_rotation = false,
                removed_on_damage = false,

            };
            titanShifter.locale_id = $"status_title_{titanShifter.id}";
            titanShifter.locale_description = $"status_description_{titanShifter.id}";
            titanShifter.tier = StatusTier.Advanced;
            titanShifter.texture = "fx_tele_minato";
            titanShifter.path_icon = "ui/Icons/actor_traits/titan"; //Well, I don't have custom icon for this = StatusTier.Advanced;
            titanShifter.action_on_receive = (WorldAction)Delegate.Combine(titanShifter.action_on_receive, new WorldAction(DarkieStatusEffectAction.titanShifterStatusSpecialEffect));
            AssetManager.status.add(titanShifter);
        }

        private static void addToLocale(string id, string name)
        {
            LM.AddToCurrentLocale($"{id}", name);
        }
    }
}
