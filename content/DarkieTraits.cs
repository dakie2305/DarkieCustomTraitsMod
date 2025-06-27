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
    private static int Rare = 3;
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
            rate_birth = DarkieTraitsBirthRate.TurtleGuyRate, // 5% chance
            rate_inherit = DarkieTraitsBirthRate.TurtleGuyRate, //Percentage
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
        addToLocale(turtleGuyTrait.id, "Turtle Guy", "The turtle. He is extremely slow but he can live a bit longer, a bit tougher, and with more kids.");
        #endregion

        #region flash
        ActorTrait flashTrait = new ActorTrait()
        {
            id = "flash",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/flash",
            rate_birth = DarkieTraitsBirthRate.FlashRate,
            rate_inherit = DarkieTraitsBirthRate.FlashRate,
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
        addToLocale(flashTrait.id, "Flash", "The True Flash. Has extremely high speed and attack speed, but sacrifices health.");
        #endregion

        #region berserker
        ActorTrait berserkerTrait = new ActorTrait()
        {
            id = "berserker",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/berserker",
            rate_birth = DarkieTraitsBirthRate.BerserkerRate,
            rate_inherit = DarkieTraitsBirthRate.BerserkerRate,
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
        addToLocale(berserkerTrait.id, "Berserker", "A warrior who battles a lot! Has great health and attack power, but is slower than normal.");
        #endregion

        //Hawkeye trait.
        #region hawkeye
        ActorTrait hawkeyeTrait = new ActorTrait()
        {
            id = "hawkeye",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/hawkeye",
            rate_birth = DarkieTraitsBirthRate.HawkEyeRate,
            rate_inherit = DarkieTraitsBirthRate.HawkEyeRate,
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
        addToLocale(hawkeyeTrait.id, "Hawkeye", "I do not miss any shot.");
        #endregion

        #region titan
        ActorTrait titanTrait = new ActorTrait()
        {
            id = "titan",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/titan",
            rate_birth = DarkieTraitsBirthRate.TitanRate,
            rate_inherit = DarkieTraitsBirthRate.TitanRate,
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
        addToLocale(titanTrait.id, "The Titan", "Quick! Someone call Levi immediately!");
        #endregion

        //Titan Shifter trait.
        #region titan_shifter
        ActorTrait titanShifterTrait = new ActorTrait()
        {
            id = "titan_shifter",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/titanShifter",
            rate_birth = DarkieTraitsBirthRate.TitanShifterRate,
            rate_inherit = DarkieTraitsBirthRate.TitanShifterRate,
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

        //For now, action_get_hit does not work so transformation effect will take place inside attack effect
        titanShifterTrait.action_attack_target = new AttackAction(DarkieTraitActions.titanShifterAttackEffect);
        titanShifterTrait.action_special_effect = (WorldAction)Delegate.Combine(titanShifterTrait.action_special_effect, new WorldAction(DarkieTraitActions.titanShifterSpecialEffect));
        titanShifterTrait.action_get_hit = (GetHitAction)Delegate.Combine(titanShifterTrait.action_get_hit, new GetHitAction(DarkieTraitActions.titanShifterGetHit));
        AssetManager.traits.add(titanShifterTrait);
        addToLocale(titanShifterTrait.id, "Titan Shifter", "Shinzo wo Sasageyo! Will turn into Titan when get hit");
        #endregion


        //The old wise one trait
        #region wise_old_one
        ActorTrait wiseOldOneTrait = new ActorTrait()
        {
            id = "wise_old_one",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/old_wise",
            rate_birth = DarkieTraitsBirthRate.WiseOldOneRate,
            rate_inherit = DarkieTraitsBirthRate.WiseOldOneRate,
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
        addToLocale(wiseOldOneTrait.id, "Wise Old One", "The old, wise elder. Has high intelligence and diplomacy, and low HP!");
        #endregion

        //The idiot savant. Super idiot
        #region idiot_savant
        ActorTrait idiotSavantTrait = new ActorTrait()
        {
            id = "idiot_savant",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/idiot",
            rate_birth = DarkieTraitsBirthRate.IdiotRate,
            rate_inherit = DarkieTraitsBirthRate.IdiotRate,
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
        addToLocale(idiotSavantTrait.id, "Idiot Savant", "The idiot savant. Has extremely low intelligence and diplomacy!");
        #endregion

        #region mountain
        ActorTrait mountainTrait = new ActorTrait()
        {
            id = "mountain",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/mountain",
            rate_birth = DarkieTraitsBirthRate.MountaintRate,
            rate_inherit = DarkieTraitsBirthRate.MountaintRate,
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
        addToLocale(mountainTrait.id, "The Mountain", "The mountain. Immovable object with high resistance!");
        #endregion

        //The Almighty One. Unstopabble object! Super OP!
        #region almighty
        ActorTrait almightyTrait = new ActorTrait()
        {
            id = "almighty",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/almighty",
            rate_birth = DarkieTraitsBirthRate.AlmightyRate,
            rate_inherit = DarkieTraitsBirthRate.AlmightyRate,
            rarity = Rarity.R2_Epic,
            can_be_given = true,
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
        addToLocale(almightyTrait.id, "The Almighty", "The Almighty One. Unstopabble object with boom boom attack! Super OP!");
        #endregion

        //Thor the god of hammer. Can summon lightning to strike his foe
        #region thor
        ActorTrait thorTrait = new ActorTrait()
        {
            id = "thor_odinson",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/thor",
            rate_birth = DarkieTraitsBirthRate.ThorRate,
            rate_inherit = DarkieTraitsBirthRate.ThorRate,
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
        thorTrait.base_stats.set(CustomBaseStatsConstant.Lifespan, 0f); //To be immortal

        thorTrait.type = TraitType.Positive;
        thorTrait.unlock(true);

        thorTrait.action_attack_target = new AttackAction(DarkieTraitActions.thorGodThunderAttackEffect);
        thorTrait.action_special_effect = (WorldAction)Delegate.Combine(thorTrait.action_special_effect, new WorldAction(DarkieTraitActions.thorSparklingSpecialEffect));

        AssetManager.traits.add(thorTrait);
        addToLocale(thorTrait.id, "Thor Odinson", "The god of Hammer with lightning summon power. It's hammer time!");
        #endregion

        //Nightcrawler can teleport both him and his enemies
        #region nightcrawler
        ActorTrait nightcrawlerTrait = new ActorTrait()
        {
            id = "nightcrawler",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/nightcrawler",
            rate_birth = DarkieTraitsBirthRate.NightCrawlerRate,
            rate_inherit = DarkieTraitsBirthRate.NightCrawlerRate,
            rarity = Rarity.R2_Epic,
            can_be_given = true,
        };

        nightcrawlerTrait.base_stats = new BaseStats();
        nightcrawlerTrait.base_stats.set(CustomBaseStatsConstant.Speed, 30f);
        nightcrawlerTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 50f);
        nightcrawlerTrait.base_stats.set(CustomBaseStatsConstant.Range, 10f);

        nightcrawlerTrait.type = TraitType.Positive;
        nightcrawlerTrait.unlock(true);

        nightcrawlerTrait.action_attack_target = new AttackAction(DarkieTraitActions.nightCrawlerAttackEffect);
        nightcrawlerTrait.action_special_effect = (WorldAction)Delegate.Combine(nightcrawlerTrait.action_special_effect, new WorldAction(DarkieTraitActions.nightCrawlerSparklingSpecialEffect)
        );

        AssetManager.traits.add(nightcrawlerTrait);
        addToLocale(nightcrawlerTrait.id, "Nightcrawler", "The lurker of the night! He can teleport around the map and teleport his enemies too.");
        #endregion

        //Shield guy, this guy has a shield
        #region shield_guy
        ActorTrait shieldGuyTrait = new ActorTrait()
        {
            id = "shield_guy",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/shield",
            rate_birth = DarkieTraitsBirthRate.ShieldGuyRate,
            rate_inherit = DarkieTraitsBirthRate.ShieldGuyRate,
            rarity = Rarity.R1_Rare,
            can_be_given = true,
        };

        shieldGuyTrait.base_stats = new BaseStats();
        shieldGuyTrait.base_stats.set(CustomBaseStatsConstant.Speed, -10f);
        shieldGuyTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, -20f);
        shieldGuyTrait.base_stats.set(CustomBaseStatsConstant.Health, 250f);
        shieldGuyTrait.base_stats.set(CustomBaseStatsConstant.Warfare, 10f);
        shieldGuyTrait.base_stats.set(CustomBaseStatsConstant.Scale, 0.04f);

        shieldGuyTrait.type = TraitType.Positive;
        shieldGuyTrait.unlock(true);

        shieldGuyTrait.action_attack_target = new AttackAction(DarkieTraitActions.shieldGuyAttackEffect);

        AssetManager.traits.add(shieldGuyTrait);
        addToLocale(shieldGuyTrait.id, "Shield Guy", "A total tanker of the team, can cast shield on themselves and sometime others too!");
        #endregion

        #region beasttamer
        ActorTrait beastTamerTrait = new ActorTrait()
        {
            id = "dej_wolf_tamer",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/beasttamer",
            rate_birth = DarkieTraitsBirthRate.WolfTamerRate,
            rate_inherit = DarkieTraitsBirthRate.WolfTamerRate,
            rarity = Rarity.R1_Rare,
        };

        beastTamerTrait.base_stats = new BaseStats();
        beastTamerTrait.base_stats.set(CustomBaseStatsConstant.Speed, 30f);
        beastTamerTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 50f);

        beastTamerTrait.type = TraitType.Positive;
        beastTamerTrait.unlock(true);

        List<string> oppositesBeastTamer = new() { "dej_bear_tamer", "dej_dragon_trainer" };
        beastTamerTrait.addOpposites(oppositesBeastTamer);
        //Spawn wolf pack on attack
        beastTamerTrait.action_attack_target = new AttackAction(DarkieTraitActions.spawnWolfBeastsAttackEffect);

        AssetManager.traits.add(beastTamerTrait);
        addToLocale(beastTamerTrait.id, "Wolf Tamer", "The master of the pack! Can summon 3 allied wolfs to aid them on combat.");
        #endregion

        #region tamed_beasts
        ActorTrait beastTrait = new ActorTrait()
        {
            id = "tamed_beasts",
            group_id = "special", //This trait belong to special group
            path_icon = $"{PathToTraitIcon}/beast",
            rate_birth = NoChance,
            rate_inherit = NoChance,
            rarity = Rarity.R0_Normal,
            can_be_given = false,
            can_be_removed = false,
        };

        beastTrait.base_stats = new BaseStats();
        beastTrait.base_stats.set(CustomBaseStatsConstant.Speed, 30f);
        beastTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 50f);
        beastTrait.base_stats.set(CustomBaseStatsConstant.Health, 100f);
        beastTrait.base_stats.set(CustomBaseStatsConstant.Scale, 0.04f);
        //Tamed beast will now share same kingdom with its master
        beastTrait.action_special_effect = (WorldAction)Delegate.Combine(beastTrait.action_special_effect, new WorldAction(DarkieTraitActions.tamedBeastSpecialEffect));
        //beastTrait.action_death 
        //Maybe reduce its master count
        beastTrait.type = TraitType.Other;
        beastTrait.unlock(true);

        AssetManager.traits.add(beastTrait);
        addToLocale(beastTrait.id, "Tamed Beasts", "The beast was summoned here by its Master.");
        #endregion

        //this trait will spawn out bear
        #region beartamer
        ActorTrait beartamerTrait = new ActorTrait()
        {
            id = "dej_bear_tamer",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/beartamer",
            rate_birth = DarkieTraitsBirthRate.BearTamerRate,
            rate_inherit = DarkieTraitsBirthRate.BearTamerRate,
            rarity = Rarity.R2_Epic,
            can_be_given = true,
        };

        beartamerTrait.base_stats = new BaseStats();
        beartamerTrait.base_stats.set(CustomBaseStatsConstant.Speed, 30f);
        beartamerTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 50f);

        beartamerTrait.type = TraitType.Positive;
        beartamerTrait.unlock(true);

        List<string> oppositesBearTamer = new() { "dej_wolf_tamer", "dej_dragon_trainer" };
        beartamerTrait.addOpposites(oppositesBearTamer);
        beartamerTrait.action_attack_target = new AttackAction(DarkieTraitActions.spawnBearAttackEffect);
        AssetManager.traits.add(beartamerTrait);
        addToLocale(beartamerTrait.id, "Bear Tamer", "The ruler of the Bear! Will summon 2 bear to aid in battle.");
        #endregion

        //this trait will spawn out a BIGGER DRAGON 
        #region dragon_trainer
        ActorTrait dragonTrainerTrait = new ActorTrait()
        {
            id = "dej_dragon_trainer",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/dragon_trainer",
            rate_birth = DarkieTraitsBirthRate.DragonTrainerRate,
            rate_inherit = DarkieTraitsBirthRate.DragonTrainerRate,
            rarity = Rarity.R3_Legendary,
            can_be_given = true,
        };

        dragonTrainerTrait.base_stats = new BaseStats();
        dragonTrainerTrait.base_stats.set(CustomBaseStatsConstant.Speed, 30f);
        dragonTrainerTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 50f);
        dragonTrainerTrait.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 1.0f);

        dragonTrainerTrait.type = TraitType.Positive;
        dragonTrainerTrait.unlock(true);

        List<string> oppositesDragonTrainer = new() { "dej_bear_tamer", "dej_wolf_tamer" };
        dragonTrainerTrait.addOpposites(oppositesDragonTrainer);

        dragonTrainerTrait.action_attack_target = new AttackAction(DarkieTraitActions.spawnDragonAttackEffect);
        AssetManager.traits.add(dragonTrainerTrait);
        addToLocale(dragonTrainerTrait.id, "Dragon Trainer", "The ultimate trainer ever alive! Will summon one big ass dragon to aid in battle.");
        #endregion

        //Medic, this guy can heal
        #region medic_healer
        ActorTrait medicHealerTrait = new ActorTrait()
        {
            id = "medic_healer",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/medic",
            rate_birth = DarkieTraitsBirthRate.MedicRate,
            rate_inherit = DarkieTraitsBirthRate.MedicRate,
            rarity = Rarity.R2_Epic,
            can_be_given = true,
        };

        medicHealerTrait.base_stats = new BaseStats();
        medicHealerTrait.base_stats.set(CustomBaseStatsConstant.Speed, -0.01f);
        medicHealerTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, -10f);
        medicHealerTrait.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.25f);

        medicHealerTrait.type = TraitType.Positive;
        medicHealerTrait.unlock(true);

        medicHealerTrait.action_special_effect = (WorldAction)Delegate.Combine(medicHealerTrait.action_special_effect, new WorldAction(DarkieTraitActions.medicSuperHealing));
        //medicHealerTrait.action_get_hit = (GetHitAction)Delegate.Combine(medicHealerTrait.action_get_hit, new GetHitAction(DarkieTraitActions.medicGetHit));
        //Not needed get hit, since action special already heal themselves 
        AssetManager.traits.add(medicHealerTrait);
        addToLocale(medicHealerTrait.id, "Medic Healer", "A medic to save the team! Can heal themselves and other allies.");
        #endregion

        //Gangster, can summon bandits to help fight
        #region gangster
        ActorTrait gangsterTrait = new ActorTrait()
        {
            id = "gangster",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/gangster",
            rate_birth = DarkieTraitsBirthRate.GangsterRate,
            rate_inherit = DarkieTraitsBirthRate.GangsterRate,
            rarity = Rarity.R1_Rare,
            can_be_given = true,
        };

        gangsterTrait.base_stats = new BaseStats();
        gangsterTrait.base_stats.set(CustomBaseStatsConstant.Speed, 30f);
        gangsterTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 30f);
        gangsterTrait.base_stats.set(CustomBaseStatsConstant.Army, 50f);
        gangsterTrait.base_stats.set(CustomBaseStatsConstant.Diplomacy, 20f);
        gangsterTrait.base_stats.set(CustomBaseStatsConstant.Warfare, 20f);
        gangsterTrait.base_stats.set(CustomBaseStatsConstant.Stewardship, -10f);
        gangsterTrait.base_stats.set(CustomBaseStatsConstant.LoyaltyTraits, -25f);

        gangsterTrait.type = TraitType.Positive;
        gangsterTrait.unlock(true);
        gangsterTrait.action_attack_target = new AttackAction(DarkieTraitActions.spawnBanditAttackEffect);
        AssetManager.traits.add(gangsterTrait);
        addToLocale(gangsterTrait.id, "Gangster", "What are you looking at huh!? Don't make me call my brothers in bandit here!");
        #endregion

        //Mage
        #region master_magister
        ActorTrait mageTrait = new ActorTrait()
        {
            id = "master_magister",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/mage",
            rate_birth = DarkieTraitsBirthRate.MagisterRate,
            rate_inherit = DarkieTraitsBirthRate.MagisterRate,
            rarity = Rarity.R3_Legendary,
        };

        mageTrait.base_stats = new BaseStats();
        mageTrait.base_stats.set(CustomBaseStatsConstant.Speed, 30f);
        mageTrait.base_stats.set(CustomBaseStatsConstant.Health, 300f);
        mageTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 30f);
        mageTrait.base_stats.set(CustomBaseStatsConstant.Range, 10f);
        mageTrait.base_stats.set(CustomBaseStatsConstant.MultiplierMana, 1.0f);
        mageTrait.base_stats.set(CustomBaseStatsConstant.SkillSpell, 1.0f); //Percentage

        mageTrait.type = TraitType.Positive;
        mageTrait.unlock(true);

        mageTrait.action_attack_target = new AttackAction(DarkieTraitActions.mageAttackEffect);
        mageTrait.action_special_effect = (WorldAction)Delegate.Combine(mageTrait.action_special_effect, new WorldAction(DarkieTraitActions.mageSparklingSpecialEffect));

        AssetManager.traits.add(mageTrait);
        addToLocale(mageTrait.id, "Master Magister", "A magister!? Unleash the chaos with magic!");
        #endregion

        //Wololo unit
        #region wololo
        ActorTrait wololoTrait = new ActorTrait()
        {
            id = "wololo",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/wololo",
            rate_birth = DarkieTraitsBirthRate.WololoRate,
            rate_inherit = DarkieTraitsBirthRate.WololoRate,
            rarity = Rarity.R1_Rare,
            can_be_given = true,
        };

        wololoTrait.base_stats = new BaseStats();
        wololoTrait.base_stats.set(CustomBaseStatsConstant.Speed, 30f);
        wololoTrait.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, -0.3f);
        wololoTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 40f);

        wololoTrait.type = TraitType.Positive;
        wololoTrait.unlock(true);

        wololoTrait.action_attack_target = new AttackAction(DarkieTraitActions.wololoConvertUnitAttackEffect);

        AssetManager.traits.add(wololoTrait);
        addToLocale(wololoTrait.id, "Wololo", "Wololo? Now you're on my side!");
        #endregion

        //chained unit
        #region chained
        ActorTrait chainedTrait = new ActorTrait()
        {
            id = "chained",
            group_id = "special",
            path_icon = $"{PathToTraitIcon}/chained",
            rate_birth = NoChance,
            rate_inherit = NoChance,
            rarity = Rarity.R0_Normal,
            can_be_given = false,
            can_be_removed = true,
            can_be_removed_by_divine_light = true,
        };

        chainedTrait.base_stats = new BaseStats();
        chainedTrait.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, -0.2f);
        chainedTrait.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, -0.2f);
        chainedTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, -30f);

        chainedTrait.type = TraitType.Negative;
        chainedTrait.unlock(true);

        AssetManager.traits.add(chainedTrait);
        addToLocale(chainedTrait.id, "Chained", "Someone converted me :(");
        #endregion

        //Ant Man
        #region ant_man
        ActorTrait antManTrait = new ActorTrait()
        {
            id = "ant_man",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/antman",
            rate_birth = DarkieTraitsBirthRate.AntManRate,
            rate_inherit = DarkieTraitsBirthRate.AntManRate,
            rarity = Rarity.R1_Rare,
            can_be_given = true,
        };

        antManTrait.base_stats = new BaseStats();
        antManTrait.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, 1.0f);
        antManTrait.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.2f);
        antManTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 20f);

        antManTrait.type = TraitType.Positive;
        antManTrait.unlock(true);

        antManTrait.addOpposites(new List<string> { "titan_shifter", "titan" });

        //Since action get hit does not work, we will use action attack to transform into ant man
        antManTrait.action_attack_target = new AttackAction(DarkieTraitActions.antManAttackEffect);
        antManTrait.action_get_hit = (GetHitAction)Delegate.Combine(antManTrait.action_get_hit, new GetHitAction(DarkieTraitActions.antManGetHit));

        AssetManager.traits.add(antManTrait);
        addToLocale(antManTrait.id, "Ant Man", "Bring me Kang!");
        #endregion

        //Esteemed Commander
        #region esteemed_commander
        ActorTrait esteemedCommanderTrait = new ActorTrait()
        {
            id = "esteemed_commander",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/commander",
            rate_birth = DarkieTraitsBirthRate.CommanderRate,
            rate_inherit = DarkieTraitsBirthRate.CommanderRate,
            rarity = Rarity.R1_Rare,
            can_be_given = true,
            can_be_removed_by_divine_light = true,
        };

        esteemedCommanderTrait.base_stats = new BaseStats();
        esteemedCommanderTrait.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.2f);
        esteemedCommanderTrait.base_stats.set(CustomBaseStatsConstant.Army, 100f);
        esteemedCommanderTrait.base_stats.set(CustomBaseStatsConstant.Warfare, 100f);
        esteemedCommanderTrait.base_stats.set(CustomBaseStatsConstant.Diplomacy, 100f);
        esteemedCommanderTrait.base_stats.set(CustomBaseStatsConstant.Damage, 50f);
        esteemedCommanderTrait.base_stats.set(CustomBaseStatsConstant.Intelligence, 20f);
        esteemedCommanderTrait.base_stats.set(CustomBaseStatsConstant.Scale, 0.02f);

        esteemedCommanderTrait.type = TraitType.Positive;
        esteemedCommanderTrait.unlock(true);

        esteemedCommanderTrait.action_special_effect = (WorldAction)Delegate.Combine(esteemedCommanderTrait.action_special_effect, new WorldAction(DarkieTraitActions.commanderSpecialEffect));

        AssetManager.traits.add(esteemedCommanderTrait);
        addToLocale(esteemedCommanderTrait.id, "Esteemed Commander", "The country needs YOU to join the ARMY! Will convert nearby units into soldier to serve the nation!");
        #endregion


        //my least favorite
        #region the_tortured
        ActorTrait theTorturedTrait = new ActorTrait()
        {
            id = "the_tortured",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/tortured",
            rate_birth = NoChance,
            rate_inherit = HighChance,
            rarity = Rarity.R2_Epic,
            can_be_given = true,
        };

        theTorturedTrait.base_stats = new BaseStats();
        theTorturedTrait.base_stats.set(CustomBaseStatsConstant.BirthRate, -1.0f);
        theTorturedTrait.base_stats.set(CustomBaseStatsConstant.Health, 9900f);
        theTorturedTrait.base_stats.set(CustomBaseStatsConstant.Speed, -100f);
        theTorturedTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, -100f);
        theTorturedTrait.base_stats.set(CustomBaseStatsConstant.Damage, -500f);
        theTorturedTrait.base_stats.set(CustomBaseStatsConstant.Intelligence, -200f);
        theTorturedTrait.base_stats.set(CustomBaseStatsConstant.Lifespan, 50000f);
        theTorturedTrait.base_stats.set(CustomBaseStatsConstant.Armor, -100f);

        theTorturedTrait.type = TraitType.Negative;
        theTorturedTrait.unlock(true);

        theTorturedTrait.action_special_effect = (WorldAction)Delegate.Combine(theTorturedTrait.action_special_effect, new WorldAction(DarkieTraitActions.livingHellEffect));

        AssetManager.traits.add(theTorturedTrait);
        addToLocale(theTorturedTrait.id, "The Tortured", "You should never have existed. Your mere existence is blasphemy and an insult to everyone.");
        #endregion

        //Blood of werewolf
        #region blood_of_wolf
        ActorTrait bloodOfWolfTrait = new ActorTrait()
        {
            id = "blood_of_wolf",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/bloodwerewolf",
            rate_birth = DarkieTraitsBirthRate.BloodWolfRate,
            rate_inherit = DarkieTraitsBirthRate.BloodWolfRate,
            rarity = Rarity.R1_Rare,
        };

        bloodOfWolfTrait.base_stats = new BaseStats();
        bloodOfWolfTrait.base_stats.set(CustomBaseStatsConstant.Health, 100f);
        bloodOfWolfTrait.base_stats.set(CustomBaseStatsConstant.Armor, 30f);

        bloodOfWolfTrait.type = TraitType.Positive;
        bloodOfWolfTrait.unlock(true);

        bloodOfWolfTrait.action_special_effect = (WorldAction)Delegate.Combine(bloodOfWolfTrait.action_special_effect, new WorldAction(DarkieTraitActions.turnWerewolvesSpecialEffect));

        AssetManager.traits.add(bloodOfWolfTrait);
        addToLocale(bloodOfWolfTrait.id, "Blood of Wolf", "Can turn into horrible creature during full moon era!");
        #endregion

        //werewolf only spawnable through blood of werewolf and be effective in moon age
        #region the_werewolf
        ActorTrait theWerewolfTrait = new ActorTrait()
        {
            id = "the_werewolf",
            group_id = "special", //This trait belong to special group
            path_icon = $"{PathToTraitIcon}/werewolf",
            rate_birth = NoChance,
            rate_inherit = NoChance,
            rarity = Rarity.R1_Rare,
            can_be_given = false,
            era_active_moon = true,
            era_active_night = true,
            only_active_on_era_flag = true,
        };

        theWerewolfTrait.base_stats = new BaseStats();
        theWerewolfTrait.base_stats.set(CustomBaseStatsConstant.Health, 100f);
        theWerewolfTrait.base_stats.set(CustomBaseStatsConstant.Armor, 30f);
        theWerewolfTrait.base_stats.set(CustomBaseStatsConstant.Damage, 40f);
        theWerewolfTrait.base_stats.set(CustomBaseStatsConstant.Speed, 90f);
        theWerewolfTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 130f);
        theWerewolfTrait.base_stats.set(CustomBaseStatsConstant.Scale, 0.02f);
        theWerewolfTrait.base_stats.set(CustomBaseStatsConstant.Knockback, 1f);

        theWerewolfTrait.type = TraitType.Other;
        theWerewolfTrait.unlock(true);

        theWerewolfTrait.action_attack_target = new AttackAction(DarkieTraitActions.werewolfSpecialAttack);

        AssetManager.traits.add(theWerewolfTrait);
        addToLocale(theWerewolfTrait.id,"The Werewolf", "Horrible creature emmerge from the dark era of full moon. Only those with Blood of Wolf can become this creature of nightmare");
        #endregion

        //no more hunger
        #region stuffed
        ActorTrait stuffedTrait = new ActorTrait()
        {
            id = "stuffed",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/cake",
            rate_birth = DarkieTraitsBirthRate.StuffedRate,
            rate_inherit = DarkieTraitsBirthRate.StuffedRate,
            rarity = Rarity.R0_Normal,
        };

        stuffedTrait.base_stats = new BaseStats();
        stuffedTrait.base_stats.set(CustomBaseStatsConstant.Health, 100f);
        stuffedTrait.type = TraitType.Positive;
        stuffedTrait.unlock(true);
        stuffedTrait.addOpposites(new List<string> { "insatiable_hunger" });
        stuffedTrait.action_special_effect = (WorldAction)Delegate.Combine(stuffedTrait.action_special_effect, new WorldAction(DarkieTraitActions.fullHungerSpecialEffect));

        AssetManager.traits.add(stuffedTrait);
        addToLocale(stuffedTrait.id, "Stuffed", "Sweetheart, you can never go hungry again!");
        #endregion

        //always hungry
        #region insatiable_hunger
        ActorTrait hungerTrait = new ActorTrait()
        {
            id = "insatiable_hunger",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/hunger",
            rate_birth = DarkieTraitsBirthRate.HungerRate,
            rate_inherit = DarkieTraitsBirthRate.HungerRate,
            rarity = Rarity.R0_Normal,
            can_be_given = true,
            can_be_removed_by_divine_light = true,
        };
        hungerTrait.addOpposites(new List<string> { "stuffed" });
        hungerTrait.base_stats = new BaseStats();
        hungerTrait.base_stats.set(CustomBaseStatsConstant.Health, 100f);
        hungerTrait.type = TraitType.Negative;
        hungerTrait.unlock(true);
        hungerTrait.action_special_effect = (WorldAction)Delegate.Combine(hungerTrait.action_special_effect, new WorldAction(DarkieTraitActions.insatiableSpecialEffect));

        AssetManager.traits.add(hungerTrait);
        addToLocale(hungerTrait.id, "Insatiable Hunger", "Too hungry. I can eat the whole world.");
        #endregion

        //duplikate
        #region duplikate
        ActorTrait duplikateTrait = new ActorTrait()
        {
            id = "duplikate",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/Dupli-Kate",
            rate_birth = DarkieTraitsBirthRate.DuplikateRate,
            rate_inherit = DarkieTraitsBirthRate.DuplikateRate,
            rarity = Rarity.R2_Epic,
        };

        duplikateTrait.base_stats = new BaseStats();
        duplikateTrait.base_stats.set(CustomBaseStatsConstant.Health, 100f);
        duplikateTrait.base_stats.set(CustomBaseStatsConstant.Damage, 90f);
        duplikateTrait.base_stats.set(CustomBaseStatsConstant.Armor, 80f);
        duplikateTrait.base_stats.set(CustomBaseStatsConstant.Accuracy, 80f);
        duplikateTrait.base_stats.set(CustomBaseStatsConstant.Scale, 0.03f);

        duplikateTrait.type = TraitType.Positive;
        duplikateTrait.unlock(true);

        duplikateTrait.action_special_effect = (WorldAction)Delegate.Combine(duplikateTrait.action_special_effect, new WorldAction(DarkieTraitActions.clonePowerSpecialEffect));
        duplikateTrait.action_death = new WorldAction(DarkieTraitActions.killAllClone);

        AssetManager.traits.add(duplikateTrait);
        addToLocale(duplikateTrait.id, "DupliKate", "You can never kill me enough.");
        #endregion

        //clone from duplikate
        #region clone
        ActorTrait cloneTrait = new ActorTrait()
        {
            id = "duplikate_clone",
            group_id = "special", //This trait belong to special group
            path_icon = $"{PathToTraitIcon}/clone",
            rate_birth = NoChance,
            rate_inherit = NoChance,
            rarity = Rarity.R0_Normal,
            can_be_given = false,
        };

        cloneTrait.base_stats = new BaseStats();
        cloneTrait.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, -0.2f);

        cloneTrait.type = TraitType.Other;
        cloneTrait.unlock(true);

        AssetManager.traits.add(cloneTrait);
        addToLocale(cloneTrait.id, "Duplikate Clone", "Just an unoriginal clone.");
        #endregion

        //From the ashes, I will reborn.
        #region pheonix
        ActorTrait pheonix = new ActorTrait()
        {
            id = "pheonix",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/phoenix",
            rate_birth = DarkieTraitsBirthRate.PhoenixRate,
            rate_inherit = DarkieTraitsBirthRate.PhoenixRate,
            rarity = Rarity.R2_Epic,
            can_be_given = true,
        };

        pheonix.base_stats = new BaseStats();
        pheonix.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.5f);
        pheonix.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 50f);
        pheonix.base_stats.set(CustomBaseStatsConstant.Speed, 50f);
        pheonix.base_stats.set(CustomBaseStatsConstant.Intelligence, 50f);

        pheonix.type = TraitType.Positive;
        pheonix.unlock(true);

        pheonix.action_special_effect = (WorldAction)Delegate.Combine(pheonix.action_special_effect, new WorldAction(DarkieTraitActions.pheonixPowerSpecialEffect));
        pheonix.action_death = (WorldAction)Delegate.Combine(pheonix.action_death, new WorldAction(DarkieTraitActions.rebornANew));

        AssetManager.traits.add(pheonix);
        addToLocale(pheonix.id, "Pheonix", "From the ashes, I will reborn.");
        #endregion

        //He is revived to the living
        #region revive
        ActorTrait revive = new ActorTrait()
        {
            id = "the_revived",
            group_id = "special",
            path_icon = $"{PathToTraitIcon}/priest",
            rate_birth = NoChance,
            rate_inherit = NoChance,
            rarity = Rarity.R1_Rare,
            can_be_given = false,
        };

        revive.base_stats = new BaseStats();
        revive.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.5f);

        revive.type = TraitType.Positive;
        revive.unlock(true);
        revive.action_special_effect = (WorldAction)Delegate.Combine(revive.action_special_effect, new WorldAction(DarkieTraitActions.reviveSpecialEffect));
        AssetManager.traits.add(revive);
        addToLocale(revive.id, "Revived", "This person has been revived to the living realm once again.");
        #endregion

        //Let me give you the taste of your own medicine.
        #region mimicry
        ActorTrait mimicry = new ActorTrait()
        {
            id = "power_mimicry",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/mimicry",
            rate_birth = DarkieTraitsBirthRate.PowerMimicryRate,
            rate_inherit = DarkieTraitsBirthRate.PowerMimicryRate,
            rarity = Rarity.R3_Legendary,
        };
        hungerTrait.addOpposites(new List<string> { "nullify" });
        mimicry.base_stats = new BaseStats();
        mimicry.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.5f);
        mimicry.base_stats.set(CustomBaseStatsConstant.Intelligence, 50f);

        mimicry.type = TraitType.Positive;
        mimicry.unlock(true);
        mimicry.action_attack_target = new AttackAction(DarkieTraitActions.powerMimicryAttackEffect);
        AssetManager.traits.add(mimicry);
        addToLocale(mimicry.id, "Power Mimicry", "Will be able to copy all traits and equipment from enemies who dare to fight this person.");
        #endregion

        //Errorr 404, traits not found.
        #region nullify
        ActorTrait nullify = new ActorTrait()
        {
            id = "nullify",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/nullify",
            rate_birth = DarkieTraitsBirthRate.NullifyRate,
            rate_inherit = DarkieTraitsBirthRate.NullifyRate,
            rarity = Rarity.R3_Legendary,
        };
        nullify.addOpposites(new List<string> { "power_mimicry" });
        nullify.base_stats = new BaseStats();
        nullify.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.2f);
        nullify.base_stats.set(CustomBaseStatsConstant.Offspring, 12f);

        nullify.type = TraitType.Negative;
        nullify.unlock(true);
        nullify.action_attack_target = new AttackAction(DarkieTraitActions.nullifyAttackEffect);

        AssetManager.traits.add(nullify);
        addToLocale(nullify.id, "Nullify", "Errorr 404, traits not found. Will slowly delete enemy traits, extremely dangerous to deal with!");
        #endregion

        //???
        #region question
        ActorTrait question = new ActorTrait()
        {
            id = "the_mysterious_trait",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/question",
            rate_birth = NoChance,
            rate_inherit = NoChance,
            rarity = Rarity.R3_Legendary,
        };

        question.base_stats = new BaseStats();
        question.base_stats.set(CustomBaseStatsConstant.Health, 4000f);
        question.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.5f);
        question.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.8f);
        question.base_stats.set(CustomBaseStatsConstant.MultiplierLifespan, 8.0f);
        question.base_stats.set(CustomBaseStatsConstant.Accuracy, 80f);
        question.base_stats.set(CustomBaseStatsConstant.Range, 10f);

        question.type = TraitType.Positive;
        question.unlock(true);

        question.action_special_effect = (WorldAction)Delegate.Combine(question.action_special_effect, new WorldAction(DarkieTraitActions.theMysteriousTraitSpecialEffect));
        question.action_attack_target = new AttackAction(DarkieTraitActions.theMysteriousTraitAttackSpecialEffect);

        AssetManager.traits.add(question);
        addToLocale(question.id, "?", "???");
        #endregion

        //cool trait to revive zombie
        #region reviver
        ActorTrait reviver = new ActorTrait()
        {
            id = "reviver_of_dead",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/lich",
            rate_birth = DarkieTraitsBirthRate.ReviverOfDeathRate,
            rate_inherit = DarkieTraitsBirthRate.ReviverOfDeathRate,
            rarity = Rarity.R2_Epic,
        };

        reviver.base_stats = new BaseStats();
        reviver.base_stats.set(CustomBaseStatsConstant.Range, 5f);
        reviver.base_stats.set(CustomBaseStatsConstant.Health, 50f);
        reviver.base_stats.set(CustomBaseStatsConstant.Offspring, -50f);
        reviver.type = TraitType.Positive;
        reviver.unlock(true);

        reviver.action_special_effect = (WorldAction)Delegate.Combine(reviver.action_special_effect, new WorldAction(DarkieTraitActions.undoZombify));

        AssetManager.traits.add(reviver);
        addToLocale(reviver.id, "Reviver of Dead", "He revives the zombie back to the living, the very opposite of the death bringer!");
        #endregion

        //Master of dark magic. Can summon and control wild skeletons
        #region necromancer
        ActorTrait necromancer = new ActorTrait()
        {
            id = "the_dark_necromancer",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/necromancer",
            rate_birth = DarkieTraitsBirthRate.DarkNecromancerRate,
            rate_inherit = DarkieTraitsBirthRate.DarkNecromancerRate,
            rarity = Rarity.R2_Epic,
            can_be_given = true,
        };

        necromancer.base_stats = new BaseStats();
        necromancer.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.3f);
        necromancer.base_stats.set(CustomBaseStatsConstant.Speed, 5f);
        necromancer.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, -0.5f);

        necromancer.type = TraitType.Positive;
        necromancer.unlock(true);

        necromancer.action_special_effect = (WorldAction)Delegate.Combine(necromancer.action_special_effect, new WorldAction(DarkieTraitActions.necromancerSpecialEffect));
        necromancer.action_attack_target = new AttackAction(DarkieTraitActions.necromancerAttackEffect);

        AssetManager.traits.add(necromancer);
        addToLocale(necromancer.id, "The Dark Necromancer", "Master of dark magic. Can summon and control wild skeletons.");
        #endregion

        //The ancient terrors, once a legend. Now the Vampire Lord is awaken.
        #region the_vampire
        ActorTrait the_vampire = new ActorTrait()
        {
            id = "the_vampire",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/the_vampire",
            rate_birth = DarkieTraitsBirthRate.VampireRate,
            rate_inherit = DarkieTraitsBirthRate.VampireRate,
            rarity = Rarity.R3_Legendary,
        };

        the_vampire.base_stats = new BaseStats();
        the_vampire.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.3f);
        the_vampire.base_stats.set(CustomBaseStatsConstant.Health, 300f);
        the_vampire.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 80f);
        the_vampire.base_stats.set(CustomBaseStatsConstant.Accuracy, 80f);
        the_vampire.base_stats.set(CustomBaseStatsConstant.Scale, 0.04f);
        the_vampire.base_stats.set(CustomBaseStatsConstant.Speed, 80f);
        the_vampire.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.5f);

        the_vampire.type = TraitType.Positive;
        the_vampire.unlock(true);

        the_vampire.action_special_effect = (WorldAction)Delegate.Combine(the_vampire.action_special_effect, new WorldAction(DarkieTraitActions.vampireSpecialEffect));
        the_vampire.action_attack_target = new AttackAction(DarkieTraitActions.vampireAttackEffect);

        AssetManager.traits.add(the_vampire);
        addToLocale(the_vampire.id, "Vampire Lord", "The ancient terrors, once a legend. Vampire Lord can make brainwawsh enemy or turn them mad forever, but is afraid of sunlight!");
        #endregion

        //What the... Is that me there? OMG I am a clone of Mirror Man!
        #region the_mirroed
        ActorTrait the_mirroed = new ActorTrait()
        {
            id = "the_mirroed",
            group_id = "special",
            path_icon = $"{PathToTraitIcon}/the_mirroed",
            rate_birth = NoChance,
            rate_inherit = NoChance,
            rarity = Rarity.R2_Epic,
            can_be_given = false,
        };
        the_mirroed.base_stats = new BaseStats();
        the_mirroed.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, -0.1f);
        the_mirroed.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, -0.5f);
        the_mirroed.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, -0.3f);
        the_mirroed.type = TraitType.Negative;
        the_mirroed.unlock(true);
        the_mirroed.action_special_effect = (WorldAction)Delegate.Combine(the_mirroed.action_special_effect, new WorldAction(DarkieTraitActions.theMirroedCloneEffect));
        AssetManager.traits.add(the_mirroed);
        addToLocale(the_mirroed.id, "The Mirroed", "What the... Is that me there? OMG I am a clone of Mirror Man!");
        #endregion

        //The Mirror Man can reflect damage and clone the enemies.
        #region mirror_man
        ActorTrait mirror_man = new ActorTrait()
        {
            id = "mirror_man",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/mirror_man",
            rate_birth = DarkieTraitsBirthRate.MirrorManRate,
            rate_inherit = DarkieTraitsBirthRate.MirrorManRate,
            rarity = Rarity.R3_Legendary,
        };

        mirror_man.base_stats = new BaseStats();
        mirror_man.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.1f);
        mirror_man.base_stats.set(CustomBaseStatsConstant.MultiplierMana, 0.1f);
        mirror_man.base_stats.set(CustomBaseStatsConstant.MultiplierStamina, 0.1f);

        mirror_man.type = TraitType.Positive;
        mirror_man.unlock(true);

        mirror_man.action_special_effect = (WorldAction)Delegate.Combine(mirror_man.action_special_effect, new WorldAction(DarkieTraitActions.mirrorManSpecialEffect));
        mirror_man.action_attack_target = new AttackAction(DarkieTraitActions.mirrorManSpecialAttack);
        //Reflect damage
        mirror_man.action_get_hit = (GetHitAction)Delegate.Combine(mirror_man.action_get_hit, new GetHitAction(DarkieTraitActions.mirrorManGetHit));
        AssetManager.traits.add(mirror_man);
        addToLocale(mirror_man.id, "Mirror Man", "The Mirror Man can reflect damage and clone the enemies.");
        #endregion

        //Time Stopper to stop enemy in track.
        #region time stopper
        ActorTrait timeStopper = new ActorTrait()
        {
            id = "time_stopper",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/time_stopper",
            rate_birth = DarkieTraitsBirthRate.TimeStopperRate,
            rate_inherit = DarkieTraitsBirthRate.TimeStopperRate,
            rarity = Rarity.R3_Legendary,
        };

        timeStopper.base_stats = new BaseStats();
        timeStopper.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, 0.1f);
        timeStopper.base_stats.set(CustomBaseStatsConstant.MultiplierMana, 0.1f);
        timeStopper.base_stats.set(CustomBaseStatsConstant.MultiplierStamina, 0.1f);

        timeStopper.type = TraitType.Positive;
        timeStopper.unlock(true);

        timeStopper.action_attack_target = new AttackAction(DarkieTraitActions.timeStopperSpecialAttack);
        timeStopper.action_get_hit = (GetHitAction)Delegate.Combine(timeStopper.action_get_hit, new GetHitAction(DarkieTraitActions.timeStopperGetHit));
        AssetManager.traits.add(timeStopper);
        addToLocale(timeStopper.id, "Time Stopper", "A dangerous entity that can freeze time the enemies, unfreeze their allies or make enemies grow older faster!");
        #endregion

        //Electro, can move, teleport fast and electrocute
        #region electro
        ActorTrait electro = new ActorTrait()
        {
            id = "the_electro",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/electro",
            rate_birth = DarkieTraitsBirthRate.ElectroRate,
            rate_inherit = DarkieTraitsBirthRate.ElectroRate,
            rarity = Rarity.R3_Legendary,
        };

        electro.base_stats = new BaseStats();
        electro.base_stats.set(CustomBaseStatsConstant.MultiplierHealth, -0.3f);
        electro.base_stats.set(CustomBaseStatsConstant.MultiplierAttackSpeed, 0.5f);
        electro.base_stats.set(CustomBaseStatsConstant.MultiplierSpeed, 0.5f);
        electro.base_stats.set(CustomBaseStatsConstant.ConstructionSpeed, 50f);
        electro.base_stats.set(CustomBaseStatsConstant.MultiplierStamina, 0.1f);

        electro.type = TraitType.Positive;
        electro.unlock(true);
        electro.action_special_effect = (WorldAction)Delegate.Combine(electro.action_special_effect, new WorldAction(DarkieTraitActions.electroSparklingSpecialEffect));
        electro.action_attack_target = new AttackAction(DarkieTraitActions.electroSpecialAttack);
        AssetManager.traits.add(electro);
        addToLocale(electro.id, "Electro", "Living electricity with the power to electrocute their enemies and teleporting really fast!");
        #endregion

    }

    private static void addToLocale(string id, string name, string description)
    {
        //Already have Locales folder, so this is no need anymore
        //LM.AddToCurrentLocale($"trait_{id}", name);
        //LM.AddToCurrentLocale($"trait_{id}_info", description);
    }
}