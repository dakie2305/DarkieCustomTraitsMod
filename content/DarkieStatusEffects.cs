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
            titanShifter.sprite_list  = new Sprite[] { sprite };
            titanShifter.action_on_receive = (WorldAction)Delegate.Combine(titanShifter.action_on_receive, new WorldAction(DarkieStatusEffectAction.titanShifterStatusSpecialEffect));
            titanShifter.action_finish = (WorldAction)Delegate.Combine(titanShifter.action_finish, new WorldAction(DarkieStatusEffectAction.titanShifterStatusOnFinish));
            AssetManager.status.add(titanShifter);
            addToLocale(titanShifter.id, "Titan Shifter", "One of the most dangerous titan shifter that will wreck everything!");
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
