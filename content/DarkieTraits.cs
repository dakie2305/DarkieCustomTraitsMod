using NeoModLoader.api.attributes;
using System.Collections.Generic;
using UnityEngine;
using NeoModLoader.General;
using System;


namespace DarkieCustomTraits.Content;

internal static class DarkieTraits
{
    private static string TraitGroupId = "darkie";
    private static string PathToTraitIcon = "ui/Icons/actor_traits/darkie_traits";


    private static int NoChance = 0;
    private static int Rare = 5;
    private static int LowChance = 15;
    private static int MediumChance = 30;
    private static int ExtraChance = 45;
    private static int HighChance = 75;

    [Hotfixable]
    public static void Init()
    {
        loadCustomTraitGroup();
        loadCustomTrait();

    }

    private static void loadCustomTraitGroup()
    {
        ActorTraitGroupAsset group = new ActorTraitGroupAsset()
        {
            id = TraitGroupId,
            name = $"trait_group_{TraitGroupId}",
            color = "#00f7ff", //TEAL
        };
        // Add trait group to trait group library
        AssetManager.trait_groups.add(group);
        LM.AddToCurrentLocale($"{group.name}", $"Darkie Traits");
    }

    private static void loadCustomTrait()
    {
        #region turtleguy
        ActorTrait turtleGuyTrait = new ActorTrait()
        {
            id = "turtleguy",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/turtleguy",
            rate_birth = Rare, // 5% chance
            rate_inherit = Rare, //Percentage
            rarity = Rarity.R0_Normal,
            can_be_removed_by_divine_light = true,
            can_be_given = true,
        };
        turtleGuyTrait.unlock(true);
        turtleGuyTrait.base_stats = new BaseStats();
        turtleGuyTrait.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, -0.7f);
        turtleGuyTrait.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, -0.6f); //Percentage
        turtleGuyTrait.base_stats.set(CustomBaseStatsConstant.MultiplierLifespan, 1.5f); //Percentage
        turtleGuyTrait.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.2f);
        turtleGuyTrait.base_stats.set("stamina", -15);
        turtleGuyTrait.type = TraitType.Negative;
        List<string> oppositeArrTur = new () { "flash", "berserker" };
        turtleGuyTrait.addOpposites(oppositeArrTur);
        //turtleGuyTrait.action_death = (WorldAction)Delegate.Combine(turtleGuyTrait.action_death, new WorldAction(ActionLibrary.fireDropsSpawn));
        // Add trait to trait library
        AssetManager.traits.add(turtleGuyTrait);
        LM.AddToCurrentLocale($"trait_{turtleGuyTrait.id}", "Turtle Guy");
        LM.AddToCurrentLocale($"trait_{turtleGuyTrait.id}_info", "The turtle. He is extremely slow but he can live a bit longer, a bit tougher, and with more kids.");
        #endregion

        #region flash
        ActorTrait flashTrait = new ActorTrait()
        {
            id = "flash",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/flash",
            rate_birth = Rare,
            rate_inherit = LowChance,
            rarity = Rarity.R1_Rare,
            can_be_given = true,
        };

        flashTrait.base_stats = new BaseStats();
        flashTrait.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, -0.5f);
        flashTrait.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, 3.0f);
        flashTrait.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 1.5f);
        flashTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 100f);
        flashTrait.base_stats.set(CustomBaseStatsConstant.Speed, 30f);
        flashTrait.base_stats.set(CustomBaseStatsConstant.ConstructionSpeed, 50f);

        flashTrait.type = TraitType.Positive;
        flashTrait.unlock(true);

        // Set opposites
        List<string> oppositesFlash = new() { "turtleguy", "berserker" };
        flashTrait.addOpposites(oppositesFlash);

        AssetManager.traits.add(flashTrait);
        LM.AddToCurrentLocale($"trait_{flashTrait.id}", "Flash");
        LM.AddToCurrentLocale($"trait_{flashTrait.id}_info", "The True Flash. Has extremely high speed and attack speed, but sacrifices health.");
        #endregion

        #region berserker
        ActorTrait berserkerTrait = new ActorTrait()
        {
            id = "berserker",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/berserker",
            rate_birth = LowChance,
            rate_inherit = MediumChance,
            rarity = Rarity.R0_Normal,
            can_be_given = true,
        };

        berserkerTrait.base_stats = new BaseStats();
        berserkerTrait.base_stats.set(CustomBaseStatsConstant.Health, 300f);
        berserkerTrait.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, -0.5f);
        berserkerTrait.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, -0.2f);
        berserkerTrait.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 2.5f);
        berserkerTrait.base_stats.set(CustomBaseStatsConstant.MultiplierMass, 1.0f);

        berserkerTrait.type = TraitType.Positive;
        berserkerTrait.unlock(true);

        List<string> oppositesBerserker = new() { "flash", "turtleguy" };
        berserkerTrait.addOpposites(oppositesBerserker);

        AssetManager.traits.add(berserkerTrait);
        LM.AddToCurrentLocale($"trait_{berserkerTrait.id}", "Berserker");
        LM.AddToCurrentLocale($"trait_{berserkerTrait.id}_info", "A warrior who battles a lot! Has great health and attack power, but is slower than normal.");
        #endregion

        //Hawkeye trait.
        #region hawkeye
        ActorTrait hawkeyeTrait = new ActorTrait()
        {
            id = "hawkeye",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/hawkeye",
            rate_birth = MediumChance,
            rate_inherit = MediumChance,
            rarity = Rarity.R0_Normal,
            can_be_given = true,
        };

        hawkeyeTrait.base_stats = new BaseStats();
        hawkeyeTrait.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.5f);
        hawkeyeTrait.base_stats.set(CustomBaseStatsConstant.Damage, 25f);
        hawkeyeTrait.base_stats.set(CustomBaseStatsConstant.Range, 25f);
        hawkeyeTrait.base_stats.set(CustomBaseStatsConstant.Accuracy, 100f);

        hawkeyeTrait.type = TraitType.Positive;
        hawkeyeTrait.unlock(true);

        List<string> oppositesHawkeye = new() { "flash" };
        hawkeyeTrait.addOpposites(oppositesHawkeye);

        AssetManager.traits.add(hawkeyeTrait);
        LM.AddToCurrentLocale($"trait_{hawkeyeTrait.id}", "Hawkeye");
        LM.AddToCurrentLocale($"trait_{hawkeyeTrait.id}_info", "I do not miss any shot.");
        #endregion

        #region titan
        ActorTrait titanTrait = new ActorTrait()
        {
            id = "titan",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/titan",
            rate_birth = LowChance,
            rate_inherit = MediumChance,
            rarity = Rarity.R0_Normal,
            can_be_given = true,
        };

        titanTrait.base_stats = new BaseStats();
        titanTrait.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, -0.4f); 
        titanTrait.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, 0.2f);
        titanTrait.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 2.0f);
        titanTrait.base_stats.set(CustomBaseStatsConstant.Scale, 0.28f);
        titanTrait.base_stats.set(CustomBaseStatsConstant.Health, 400f);
        titanTrait.base_stats.set(CustomBaseStatsConstant.Mass, 100f);
        titanTrait.base_stats.set(CustomBaseStatsConstant.Knockback, 0.5f); //percent
        titanTrait.type = TraitType.Positive;
        titanTrait.unlock(true);

        List<string> oppositesTitan = new() { "titan_shifter", "ant_man" };
        titanTrait.addOpposites(oppositesTitan);

        titanTrait.action_attack_target = new AttackAction(DarkieTraitActions.causeShockwave);
        AssetManager.traits.add(titanTrait);
        LM.AddToCurrentLocale($"trait_{titanTrait.id}", "The Titan");
        LM.AddToCurrentLocale($"trait_{titanTrait.id}_info", "Quick! Someone call Levi immediately!");
        #endregion

        //Titan Shifter trait.
        #region titan_shifter
        ActorTrait titanShifterTrait = new ActorTrait()
        {
            id = "titan_shifter",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/titanShifter",
            rate_birth = LowChance,
            rate_inherit = HighChance,
            rarity = Rarity.R1_Rare,
            can_be_given = true,
        };

        titanShifterTrait.base_stats = new BaseStats();
        titanShifterTrait.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, -0.4f);
        titanShifterTrait.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, 0.2f);
        titanShifterTrait.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.5f);
        titanShifterTrait.base_stats.set(CustomBaseStatsConstant.Health, 100f);

        titanShifterTrait.type = TraitType.Positive;
        titanShifterTrait.unlock(true);

        List<string> oppositesTitanShifter = new() { "titan", "ant_man" };
        titanShifterTrait.addOpposites(oppositesTitanShifter);

        //titanShifterTrait.action_attack_target = new AttackAction(DarkieTraitActions.titanShifterAttackEffect);
        titanShifterTrait.action_special_effect = (WorldAction)Delegate.Combine(titanShifterTrait.action_special_effect, new WorldAction(DarkieTraitActions.titanShifterSpecialEffect));
        //titanShifterTrait.action_get_hit = (GetHitAction)Delegate.Combine(titanShifterTrait.action_get_hit, new GetHitAction(DarkieTraitActions.titanShifterGetHit));
        titanShifterTrait.action_get_hit = (GetHitAction)Delegate.Combine(titanShifterTrait.action_get_hit, new GetHitAction(ActionLibrary.bubbleDefense));
        AssetManager.traits.add(titanShifterTrait);
        LM.AddToCurrentLocale($"trait_{titanShifterTrait.id}", "Titan Shifter");
        LM.AddToCurrentLocale($"trait_{titanShifterTrait.id}_info", "Shinzo wo Sasageyo!");
        #endregion


        //The old wise one trait
        #region wise_old_one
        ActorTrait wiseOldOneTrait = new ActorTrait()
        {
            id = "wise_old_one",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/old_wise",
            rate_birth = MediumChance,
            rate_inherit = NoChance,
            rarity = Rarity.R1_Rare,
        };

        wiseOldOneTrait.base_stats = new BaseStats();
        wiseOldOneTrait.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, -0.3f);
        wiseOldOneTrait.base_stats.set(CustomBaseStatsConstant.Speed, -5f);
        wiseOldOneTrait.base_stats.set(CustomBaseStatsConstant.Diplomacy, 175f);
        wiseOldOneTrait.base_stats.set(CustomBaseStatsConstant.Intelligence, 175f);
        wiseOldOneTrait.base_stats.set(CustomBaseStatsConstant.Warfare, 175f);
        wiseOldOneTrait.base_stats.set(CustomBaseStatsConstant.Stewardship, 55f);
        wiseOldOneTrait.base_stats.set(CustomBaseStatsConstant.Lifespan, 0.3f); //Percentage

        wiseOldOneTrait.type = TraitType.Positive;
        wiseOldOneTrait.unlock(true);

        List<string> oppositesWiseOldOne = new() { "idiot_savant" };
        wiseOldOneTrait.addOpposites(oppositesWiseOldOne);

        AssetManager.traits.add(wiseOldOneTrait);
        LM.AddToCurrentLocale($"trait_{wiseOldOneTrait.id}", "Wise Old One");
        LM.AddToCurrentLocale($"trait_{wiseOldOneTrait.id}_info", "The old, wise elder. Has high intelligence and diplomacy, and low HP.");
        #endregion

        //The idiot savant. Super idiot
        #region idiot_savant
        ActorTrait idiotSavantTrait = new ActorTrait()
        {
            id = "idiot_savant",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/idiot",
            rate_birth = LowChance,
            rate_inherit = LowChance,
            rarity = Rarity.R0_Normal,
        };

        idiotSavantTrait.base_stats = new BaseStats();
        idiotSavantTrait.base_stats.set(CustomBaseStatsConstant.Diplomacy, -555f);
        idiotSavantTrait.base_stats.set(CustomBaseStatsConstant.Intelligence, -655f);
        idiotSavantTrait.base_stats.set(CustomBaseStatsConstant.Warfare, -1000f);
        idiotSavantTrait.base_stats.set(CustomBaseStatsConstant.Stewardship, -2000f);

        idiotSavantTrait.type = TraitType.Negative;
        idiotSavantTrait.unlock(true);

        List<string> oppositesIdiotSavant = new() { "wise_old_one" };
        idiotSavantTrait.addOpposites(oppositesIdiotSavant);

        AssetManager.traits.add(idiotSavantTrait);
        LM.AddToCurrentLocale($"trait_{idiotSavantTrait.id}", "Idiot Savant");
        LM.AddToCurrentLocale($"trait_{idiotSavantTrait.id}_info", "The idiot savant. Has extremely low intelligence and diplomacy.");
        #endregion

        #region mountain
        ActorTrait mountainTrait = new ActorTrait()
        {
            id = "mountain",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/mountain",
            rate_birth = MediumChance,
            rate_inherit = LowChance,
            rarity = Rarity.R0_Normal,
        };

        mountainTrait.base_stats = new BaseStats();
        mountainTrait.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.5f);
        mountainTrait.base_stats.set(CustomBaseStatsConstant.Knockback, 1.0f);
        mountainTrait.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, -0.1f);
        mountainTrait.base_stats.set(CustomBaseStatsConstant.Damage, 50f);
        mountainTrait.base_stats.set(CustomBaseStatsConstant.Scale, 0.1f);
        mountainTrait.base_stats.set(CustomBaseStatsConstant.MultiplierMass, 3.0f);

        mountainTrait.type = TraitType.Positive;
        mountainTrait.unlock(true);

        AssetManager.traits.add(mountainTrait);
        LM.AddToCurrentLocale($"trait_{mountainTrait.id}", "The Mountain");
        LM.AddToCurrentLocale($"trait_{mountainTrait.id}_info", "The mountain. Immovable object with high resistance!");
        #endregion

        //The Almighty One. Unstopabble object! Super OP!
        #region almighty
        ActorTrait almightyTrait = new ActorTrait()
        {
            id = "almighty",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/almighty",
            rate_birth = LowChance,
            rate_inherit = MediumChance,
            rarity = Rarity.R1_Rare,
            can_be_given = true,
            can_be_removed_by_divine_light = true,
        };

        almightyTrait.base_stats = new BaseStats();
        almightyTrait.base_stats.set(CustomBaseStatsConstant.Health, 1000f);
        almightyTrait.base_stats.set(CustomBaseStatsConstant.Knockback, 1.0f); 
        almightyTrait.base_stats.set(CustomBaseStatsConstant.Speed, 60f);
        almightyTrait.base_stats.set(CustomBaseStatsConstant.Accuracy, 20f);
        almightyTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 20f);
        almightyTrait.base_stats.set(CustomBaseStatsConstant.Damage, 100f);
        almightyTrait.base_stats.set(CustomBaseStatsConstant.Scale, 0.05f);

        almightyTrait.type = TraitType.Positive;
        almightyTrait.unlock(true);

        almightyTrait.action_attack_target = new AttackAction(DarkieTraitActions.explosionAttackEffect);

        AssetManager.traits.add(almightyTrait);
        LM.AddToCurrentLocale($"trait_{almightyTrait.id}", "The Almighty");
        LM.AddToCurrentLocale($"trait_{almightyTrait.id}_info", "The Almighty One. Unstopabble object with boom boom attack! Super OP!");
        #endregion

        //Thor the god of hammer. Can summon lightning to strike his foe
        #region thor
        ActorTrait thorTrait = new ActorTrait()
        {
            id = "thor_odinson",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/thor",
            rate_birth = Rare,
            rate_inherit = NoChance,
            rarity = Rarity.R3_Legendary,
        };

        thorTrait.base_stats = new BaseStats();
        thorTrait.base_stats.set(CustomBaseStatsConstant.Health, 1300f);
        thorTrait.base_stats.set(CustomBaseStatsConstant.Knockback, 2.0f); // resistance to knockback
        thorTrait.base_stats.set(CustomBaseStatsConstant.Speed, 10f);
        thorTrait.base_stats.set(CustomBaseStatsConstant.Accuracy, 20f);
        thorTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 60f);
        thorTrait.base_stats.set(CustomBaseStatsConstant.Damage, 45f);
        thorTrait.base_stats.set(CustomBaseStatsConstant.Scale, 0.04f);
        thorTrait.base_stats.set(CustomBaseStatsConstant.Range, 15f);
        thorTrait.base_stats.set(CustomBaseStatsConstant.Lifespan, 15.0f);

        thorTrait.type = TraitType.Positive;
        thorTrait.unlock(true);

        thorTrait.action_attack_target = new AttackAction(DarkieTraitActions.thorGodThunderAttackEffect);
        thorTrait.action_special_effect = (WorldAction)Delegate.Combine(thorTrait.action_special_effect, new WorldAction(DarkieTraitActions.thorSparklingSpecialEffect));

        AssetManager.traits.add(thorTrait);
        LM.AddToCurrentLocale($"trait_{thorTrait.id}", "Thor Odinson");
        LM.AddToCurrentLocale($"trait_{thorTrait.id}_info", "The god of Hammer. It's hammer time!");
        #endregion



    }
}