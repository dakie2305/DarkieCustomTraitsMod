using NCMS.Utils;
using NeoModLoader.General;
using ReflectionUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DarkieCustomTraits.Content
{
    public class DarkieUnits
    {
        public static void Init()
        {
            loadCustomUnits();
        }

        private static void loadCustomUnits()
        {
            //this is bat
            var vampireBat = AssetManager.actor_library.clone("vampire_bat", "$mob$");
            vampireBat.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new string[]
                {
                "insect_set"
                });
            vampireBat.id = "vampire_bat";
            vampireBat.architecture_id = "civ_druid";
            vampireBat.banner_id = "civ_druid";
            vampireBat.use_phenotypes = false;
            vampireBat.can_flip = false; //no flipping texture
            vampireBat.unit_other = true;
            vampireBat.has_baby_form = false; //no baby form of course

            vampireBat.can_talk_with = false;
            vampireBat.control_can_talk = false;
            vampireBat.can_have_subspecies = false;
            vampireBat.inspect_home = false;
            vampireBat.flying = true;
            vampireBat.has_advanced_textures = false;
            vampireBat.has_soul = true;

            vampireBat.base_stats = new();
            vampireBat.base_stats.set(CustomBaseStatsConstant.Lifespan, 30f);
            vampireBat.base_stats.set(CustomBaseStatsConstant.Health, 100f);
            vampireBat.base_stats.set(CustomBaseStatsConstant.Damage, 15f);
            vampireBat.base_stats.set(CustomBaseStatsConstant.Speed, 15f);
            vampireBat.base_stats.set(CustomBaseStatsConstant.Scale, 0.09f);

            vampireBat.icon = "iconButterfly";
            vampireBat.name_taxonomic_kingdom = "animalia";
            vampireBat.name_taxonomic_phylum = "arthropoda";
            vampireBat.name_taxonomic_class = "insecta";
            vampireBat.name_taxonomic_order = "lepidoptera";
            vampireBat.name_taxonomic_family = "nymphalidae";
            vampireBat.name_taxonomic_genus = "danaus";
            vampireBat.name_taxonomic_species = "plexippus";
            vampireBat.collective_term = "group_kaleidoscope";
            vampireBat.name_locale = "Bat";
            vampireBat.icon = "iconButterfly";

            vampireBat.animation_walk = new string[] { "walk_0", "walk_1"};
            vampireBat.animation_swim = new string[] { "walk_0", "walk_1"}; //Well, it is a bat, it flies lol
            vampireBat.animation_idle = new string[] { "idle_0", "idle_1" };

            vampireBat._cached_sprite = Resources.Load<Sprite>("actors/species/other/DarkieUnit/vampire_bat/heads_male/walk_0");
            vampireBat.ignored_by_infinity_coin = false;


            vampireBat.max_random_amount = 6;
            vampireBat.action_death = (WorldAction)Delegate.Combine(vampireBat.action_death, new WorldAction(ActionLibrary.tryToCreatePlants));
            AssetManager.actor_library.loadShadow(vampireBat);
            AssetManager.actor_library.loadTexturesAndSprites(vampireBat);
            //AssetManager.actor_library.add(vampireBat);
            addToLocale(vampireBat.name_locale, vampireBat.name_locale);
        }

        private static void addToLocale(string id, string name)
        {
            LM.AddToCurrentLocale($"{id}", name);
        }
    }
}
