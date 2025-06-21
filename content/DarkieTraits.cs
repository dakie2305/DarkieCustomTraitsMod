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
        addToLocale(turtleGuyTrait.id, "Turtle Guy", "The turtle. He is extremely slow but he can live a bit longer, a bit tougher, and with more kids.");
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
        addToLocale(flashTrait.id, "Flash", "The True Flash. Has extremely high speed and attack speed, but sacrifices health.");
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
        addToLocale(berserkerTrait.id, "Berserker", "A warrior who battles a lot! Has great health and attack power, but is slower than normal.");
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
        addToLocale(hawkeyeTrait.id, "Hawkeye", "I do not miss any shot.");
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
        addToLocale(titanTrait.id, "The Titan", "Quick! Someone call Levi immediately!");
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
        addToLocale(titanShifterTrait.id, "Titan Shifter", "Shinzo wo Sasageyo! Will turn into Titan when get hit");
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
        addToLocale(wiseOldOneTrait.id, "Wise Old One", "The old, wise elder. Has high intelligence and diplomacy, and low HP!");
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
        addToLocale(idiotSavantTrait.id, "Idiot Savant", "The idiot savant. Has extremely low intelligence and diplomacy!");
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
        addToLocale(mountainTrait.id, "The Mountain", "The mountain. Immovable object with high resistance!");
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
        addToLocale(thorTrait.id, "Thor Odinson", "The god of Hammer with lightning summon power. It's hammer time!");
        #endregion

        //Nightcrawler can teleport both him and his enemies
        #region nightcrawler
        ActorTrait nightcrawlerTrait = new ActorTrait()
        {
            id = "nightcrawler",
            group_id = TraitGroupId,
            path_icon = $"{PathToTraitIcon}/nightcrawler",
            rate_birth = Rare,
            rate_inherit = ExtraChance,
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
            rate_birth = LowChance,
            rate_inherit = NoChance,
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
            rate_birth = LowChance,
            rate_inherit = MediumChance,
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
            group_id = TraitGroupId,
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
            rate_birth = LowChance,
            rate_inherit = MediumChance,
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
            rate_birth = 5,
            rate_inherit = MediumChance,
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
            rate_birth = LowChance,
            rate_inherit = LowChance,
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
            rate_birth = NoChance,
            rate_inherit = Rare,
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
            rate_birth = LowChance,
            rate_inherit = MediumChance,
            rarity = Rarity.R2_Epic,
        };

        mageTrait.base_stats = new BaseStats();
        mageTrait.base_stats.set(CustomBaseStatsConstant.Speed, 30f);
        mageTrait.base_stats.set(CustomBaseStatsConstant.Health, 300f);
        mageTrait.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 30f);
        mageTrait.base_stats.set(CustomBaseStatsConstant.Range, 10f);

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
            rate_birth = MediumChance,
            rate_inherit = MediumChance,
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
            group_id = TraitGroupId,
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
            rate_birth = MediumChance,
            rate_inherit = Rare,
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
            rate_birth = LowChance,
            rate_inherit = MediumChance,
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
            rarity = Rarity.R0_Normal,
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


    }

    private static void addToLocale(string id, string name, string description)
    {
        LM.AddToCurrentLocale($"trait_{id}", name);
        LM.AddToCurrentLocale($"trait_{id}_info", description);
    }
}