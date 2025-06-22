using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkieCustomTraits.Content
{
    public class DarkieEffects
    {
        public static void Init()
        {
            loadCustomEffects();
        }

        private static void loadCustomEffects()
        {
            EffectAsset customAntiMatterEffect = new EffectAsset();
            customAntiMatterEffect.id = "fx_YOYO_effect";
            customAntiMatterEffect.use_basic_prefab = true;
            customAntiMatterEffect.sorting_layer_id = "EffectsTop";
            customAntiMatterEffect.prefab_id = "effects/prefabs/PrefabAntimatterEffect";
            customAntiMatterEffect.sprite_path = "effects/antimatterEffect";
            customAntiMatterEffect.draw_light_area = false;
            customAntiMatterEffect.sound_launch = "event:/SFX/EXPLOSIONS/ExplosionAntimatterBomb";
            AssetManager.effects_library.add(customAntiMatterEffect);

            EffectAsset customTeleportEffect = new EffectAsset();
            customTeleportEffect.id = "fx_DarkieCustomTeleport_effect";
            customTeleportEffect.use_basic_prefab = true;
            customTeleportEffect.sorting_layer_id = "EffectsTop";
            customTeleportEffect.sprite_path = "effects/fx_tele_minato";
            AssetManager.effects_library.add(customTeleportEffect);

            //To-do: convert old effects here

            //Status antManEffect = new();
            //antManEffect.id = "Ant Man";
            //antManEffect.texture = "Ant";         //so this is the folder in effect
            //antManEffect.duration = 60.0f;
            //antManEffect.base_stats[S.armor] += 25f;
            //antManEffect.base_stats[S.health] += 500f;
            //antManEffect.base_stats[S.speed] += 300f;
            //antManEffect.base_stats[S.attack_speed] += 80f;
            //antManEffect.base_stats[S.damage] += 50f;
            //antManEffect.base_stats[S.knockback_reduction] += 100.0f;
            //antManEffect.opposite_status.Add("Titan Shifter");
            //antManEffect.animated = false;
            //antManEffect.animation_speed = 0.1f;
            //antManEffect.animation_speed_random = 0.08f;
            //antManEffect.base_stats[S.scale] -= 0.08f;

            //antManEffect.description = "status_description_antManEffect";
            //antManEffect.name = "status_title_antManEffect";
            //addTraitToLocalizedLibrary(antManEffect.name, antManEffect.description, "Ant Man Effect", "He is getting smaller");
            //AssetManager.status.add(antManEffect);
        }
    }
}
