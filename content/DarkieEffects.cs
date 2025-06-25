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
            customTeleportEffect.draw_light_size = 1.0f;
            customTeleportEffect.sorting_layer_id = "EffectsTop";
            customTeleportEffect.sprite_path = "effects/fx_tele_minato";
            AssetManager.effects_library.add(customTeleportEffect);

            EffectAsset customDarkBlueWave = new EffectAsset();
            customDarkBlueWave.id = "fx_DarkieDarkBlueWave_effect";
            customDarkBlueWave.use_basic_prefab = true;
            customDarkBlueWave.draw_light_size = 1.0f;
            customDarkBlueWave.sorting_layer_id = "EffectsTop";
            customDarkBlueWave.sprite_path = "effects/fx_dark_blue_wave";
            AssetManager.effects_library.add(customDarkBlueWave);

            EffectAsset customExplosionBlueOval = new EffectAsset();
            customExplosionBlueOval.id = "fx_DarkieExplosionBlueOval_effect";
            customExplosionBlueOval.use_basic_prefab = true;
            customExplosionBlueOval.draw_light_size = 1.0f;
            customExplosionBlueOval.sorting_layer_id = "EffectsTop";
            customExplosionBlueOval.sprite_path = "effects/fx_explosion_blue_oval";
            customExplosionBlueOval.sound_launch = "event:/SFX/EXPLOSIONS/ExplosionSmall";
            AssetManager.effects_library.add(customExplosionBlueOval);

            EffectAsset customExplosionBlueCircle = new EffectAsset();
            customExplosionBlueCircle.id = "fx_DarkieExplosionBlueCircle_effect";
            customExplosionBlueCircle.use_basic_prefab = true;
            customExplosionBlueCircle.draw_light_size = 1.0f;
            customExplosionBlueCircle.sorting_layer_id = "EffectsTop";
            customExplosionBlueCircle.sprite_path = "effects/fx_explosion_blue_circle";
            customExplosionBlueCircle.sound_launch = "event:/SFX/EXPLOSIONS/ExplosionSmall";
            AssetManager.effects_library.add(customExplosionBlueCircle);

            EffectAsset customExplosionTwoColor = new EffectAsset();
            customExplosionTwoColor.id = "fx_DarkieExplosionTwoColor_effect";
            customExplosionTwoColor.use_basic_prefab = true;
            customExplosionTwoColor.draw_light_size = 1.0f;
            customExplosionTwoColor.sorting_layer_id = "EffectsTop";
            customExplosionTwoColor.sprite_path = "effects/fx_explosion_two_colors";
            customExplosionTwoColor.sound_launch = "event:/SFX/EXPLOSIONS/ExplosionSmall";
            AssetManager.effects_library.add(customExplosionTwoColor);

            EffectAsset customExplosionCircle = new EffectAsset();
            customExplosionCircle.id = "fx_DarkieExplosionCircle_effect";
            customExplosionCircle.use_basic_prefab = true;
            customExplosionCircle.draw_light_size = 1.0f;
            customExplosionCircle.sorting_layer_id = "EffectsTop";
            customExplosionCircle.sprite_path = "effects/fx_circle_explosion";
            customExplosionCircle.sound_launch = "event:/SFX/EXPLOSIONS/ExplosionSmall";
            AssetManager.effects_library.add(customExplosionCircle);

            EffectAsset customWhiteAtomEffect = new EffectAsset();
            customWhiteAtomEffect.id = "fx_DarkieWhiteAtom_effect";
            customWhiteAtomEffect.use_basic_prefab = true;
            customWhiteAtomEffect.draw_light_size = 1.0f;
            customWhiteAtomEffect.sorting_layer_id = "EffectsTop";
            customWhiteAtomEffect.sprite_path = "effects/fx_white_atom";
            AssetManager.effects_library.add(customWhiteAtomEffect);
        }
    }
}
