using NeoModLoader.api.attributes;
using static UnityEngine.GraphicsBuffer;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

namespace DarkieCustomTraits.Content;

internal static class DarkieTraitActions
{
    private static Dictionary<Actor, Actor> listOfTamedBeasts = new Dictionary<Actor, Actor>();
    private static Dictionary<ActorData, Actor> listOfTamedBeastsData = new Dictionary<ActorData, Actor>();

    #region Attack Action
    [Hotfixable]
    public static bool causeShockwave(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        //Stun target
        if (!pTarget.isBuilding() && !pTarget.isRekt())
        {
            pTarget.addStatusEffect("stunned");
        }
        //Shockwave
        World.world.applyForceOnTile(pTile, 3, 1.0f, pForceOut: true, 0, null, pByWho:pSelf); //Ignore force for self
        EffectsLibrary.spawnExplosionWave(pTile.posV3, 3f, 0.5f);
        return true;
    }

    public static bool titanShifterAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        //Add new trait
        pSelf.a.addTrait("titan", true);
        //Shockwave
        BaseEffect baseEffect = EffectsLibrary.spawnAtTile("fx_lightning_medium", pTile, 0.25f);
        World.world.applyForceOnTile(pTile, 3, 0.5f, pForceOut: true, 0, null, pByWho: pSelf); //Ignore force for self
        EffectsLibrary.spawnExplosionWave(pTile.posV3, 3f, 0.5f);
        //Only spawn lightning effect without the actual damage
        return true;
    }

    public static bool explosionAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        //Shockwave
        World.world.applyForceOnTile(pTile, 3, 0.5f, pForceOut: true, 0, null, pByWho: pSelf); //Ignore force for self
        EffectsLibrary.spawnExplosionWave(pTile.posV3, 3f, 0.5f);
        //Spawn explosion effect
        if(Randy.randomChance(0.5f)) //Percent
        {
            //Chain explosion effect
            EffectsLibrary.spawnAtTile("fx_explosion_small", pTarget.current_tile, 0.25f);
            EffectsLibrary.spawnAtTile("fx_explosion_tiny", pTarget.current_tile, 0.25f);
            EffectsLibrary.spawnAtTile("fx_explosion_crab_bomb", pSelf.current_tile, 0.25f);
            EffectsLibrary.spawnAtTile("fx_explosion_crab_bomb", pTarget.current_tile, 0.25f);
        }
        else
        {
            EffectsLibrary.spawnAtTile("fx_explosion_meteorite", pTile, 0.25f);
        }

        //Only spawn lightning effect without the actual damage
        return true;
    }

    public static bool thorGodThunderAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (Randy.randomChance(0.3f)) //Percent
        {
            //Only spawn lightning effect without the actual damage
            EffectsLibrary.spawnAtTile("fx_lightning_medium", pTarget.current_tile, 0.4f);
            World.world.applyForceOnTile(pTarget.current_tile, 3, 0.5f, pForceOut: true, 0, null, pByWho: pSelf); //Ignore force for self
            EffectsLibrary.spawnExplosionWave(pTile.posV3, 3f, 0.5f);
            //Small chance of double striking
            if (Randy.randomChance(0.1f)) //Percent
            {
                EffectsLibrary.spawnAtTile("fx_lightning_big", pTarget.current_tile, 0.3f);
            }
            return true;
        }

        return false;
    }

    public static bool nightCrawlerAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (Randy.randomChance(0.3f)) //Percent
        {
            EffectsLibrary.spawnAtTile("fx_teleport_blue", pTarget.current_tile, 0.1f);
            ActionLibrary.teleportRandom(null, pTarget, null);
            return true;
        }

        return false;
    }


    public static bool shieldGuyAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        //he wil use shield when in combat
        if (!pSelf.a.hasStatus("shield"))
        {
            ActionLibrary.castShieldOnHimself(pSelf, pSelf, pTile);
        }
        //Rare chance to cast shield on ally of same kingdom
        if (Randy.randomChance(0.1f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 7);
            if (allClosestUnits.Any())
            {
                foreach (var unit in allClosestUnits)
                {
                    if (unit.a.kingdom == pSelf.a.kingdom && !unit.a.hasStatus("shield"))
                    {
                        unit.addStatusEffect("shield", 4f);
                        unit.addStatusEffect("caffeinated", 5f);
                    }
                }
            }
        }
        return false;
    }


    public static bool spawnWolfBeastsAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        int count = 0;
        if (pSelf.a.data.custom_data_int == null || !pSelf.a.data.custom_data_int.TryGetValue("wolfCount", out count))
        {
            pSelf.a.data.set("wolfCount", 0);
            count = 0;
        }
        if (count < 3)
        {
            //Spawn wolf and give its custom trait too
            var act = World.world.units.createNewUnit("wolf", pTile);
            act.setKingdom(pSelf.kingdom);
            act.addTrait("tamed_beasts");
            act.stats.set(CustomBaseStatsConstant.Scale, 0.1f);
            //Set master id so that it can be re-populate later
            act.data.set("master_id", pSelf.a.data.id);
            //This will help marks the ownership
            act.data.setName($"Wolf of {pSelf.a.getName()}");
            act.goTo(pSelf.current_tile);

            if (!listOfTamedBeasts.ContainsKey(act))
                listOfTamedBeasts.Add(act, pSelf.a);     //add the beast and actor who spawned them into custom list
            count++;
            pSelf.a.data.set("wolfCount", count);
            return true;
        }
        return false;
    }

    public static bool spawnBearAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        int count = 0;
        if (pSelf.a.data.custom_data_int == null || !pSelf.a.data.custom_data_int.TryGetValue("bearCount", out count))
        {
            pSelf.a.data.set("bearCount", 0);
            count = 0;
        }
        if (count < 2)
        {
            //Spawn bear and give its custom trait too
            var act = World.world.units.createNewUnit("bear", pTile);
            act.setKingdom(pSelf.kingdom);
            act.addTrait("tamed_beasts");
            act.stats.set(CustomBaseStatsConstant.Scale, 0.1f);
            //Set master id so that it can be re-populate later
            act.data.set("master_id", pSelf.a.data.id);
            //This will help marks the ownership
            act.data.setName($"Bear of {pSelf.a.getName()}");
            act.goTo(pSelf.current_tile);
            if (!listOfTamedBeasts.ContainsKey(act))
                listOfTamedBeasts.Add(act, pSelf.a);     //add the beast and actor who spawned them into custom list
            count++;
            pSelf.a.data.set("bearCount", count);
            return true;
        }
        return false;
    }

    public static bool spawnDragonAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        int count = 0;
        if (pSelf.a.data.custom_data_int == null || !pSelf.a.data.custom_data_int.TryGetValue("dragonCount", out count))
        {
            pSelf.a.data.set("dragonCount", 0);
            count = 0;
        }
        if (count < 1)
        {
            //Spawn dragon and give its custom trait too
            //Small chance to spawn zombie dragon
            Actor? act = null;
            if (Randy.randomChance(0.1f))
            {
                act = World.world.units.createNewUnit("zombie_dragon", pTile);
            }
            else
            {
                act = World.world.units.createNewUnit("dragon", pTile);
            }
            act.setKingdom(pSelf.kingdom);
            act.addTrait("tamed_beasts");
            //Set master id so that it can be re-populate later
            act.data.set("master_id", pSelf.a.data.id);
            act.stats.set(CustomBaseStatsConstant.Scale, 0.1f);
            //This will help marks the ownership
            act.data.setName($"Dragon of {pSelf.a.getName()}");
            act.goTo(pSelf.current_tile);
            if (!listOfTamedBeasts.ContainsKey(act))
                listOfTamedBeasts.Add(act, pSelf.a);     //add the beast and actor who spawned them into custom list
            count++;
            pSelf.a.data.set("dragonCount", count);
            return true;
        }
        return false;
    }

    public static bool spawnBanditAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        int count = 0;
        if (pSelf.a.data.custom_data_int == null || !pSelf.a.data.custom_data_int.TryGetValue("banditCount", out count))
        {
            pSelf.a.data.set("banditCount", 0);
            count = 0;
        }
        if (count < 3)
        {
            //Spawn bandit and give its custom trait too
            var act = World.world.units.createNewUnit("bandit", pTile);
            act.setKingdom(pSelf.kingdom);
            act.addTrait("tamed_beasts");
            act.stats.set(CustomBaseStatsConstant.Scale, 0.1f);
            //Set master id so that it can be re-populate later
            act.data.set("master_id", pSelf.a.data.id);
            //This will help marks the ownership
            act.data.setName($"Bandit Friend of {pSelf.a.getName()}");
            act.goTo(pSelf.current_tile);

            if (!listOfTamedBeasts.ContainsKey(act))
                listOfTamedBeasts.Add(act, pSelf.a);     //add the beast and actor who spawned them into custom list
            count++;
            pSelf.a.data.set("banditCount", count);
            return true;
        }
        return false;
    }

    public static bool mageAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        //This one will choose random special attack to fire at the target
        if (Randy.randomChance(0.2f))
        {
            MapBox.instance.drop_manager.spawn(pTarget.current_tile, "fire", 5f, -1f);
            MapBox.instance.drop_manager.spawn(pTarget.current_tile, "acid", 5f, -1f);
            MapBox.instance.drop_manager.spawn(pTarget.current_tile, "fire", 5f, -1f);
        }
        if (Randy.randomChance(0.02f))
        {
            ActionLibrary.castCurses(pSelf, pTarget, null);
        }
        if (Randy.randomChance(0.08f))
        {
            ActionLibrary.addFrozenEffectOnTarget(pSelf, pTarget, null);
        }
        if (Randy.randomChance(0.05f))
        {
            ActionLibrary.castShieldOnHimself(pSelf, pSelf, null);
        }
        if (Randy.randomChance(0.04f))
        {
            ActionLibrary.teleportRandom(pSelf, pTarget, null);
        }
        if (Randy.randomChance(0.05f))
        {
            ActionLibrary.castLightning(pSelf, pTarget, null);
        }
        if (Randy.randomChance(0.01f))
        {
            EffectsLibrary.spawn("fx_meteorite", pTarget.current_tile, null, null, 0f, -1f, -1f);    //spawn 1 meteorite
            pSelf.a.addStatusEffect("invincible", 5f);
        }
        return true;
    }

    public static bool wololoConvertUnitAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        //This one will convert target to self side if target enemy does not has this trait
        if (Randy.randomChance(0.2f) && !pTarget.a.hasTrait("wololo"))
        {
            pTarget.a.setKingdom(pSelf.kingdom);
            pTarget.a.addTrait("chained");
            return true;
        }
        else if (Randy.randomChance(0.01f))
        {
            pTarget.a.addTrait("madness");
            return true;
        }
        return false;

    }

    public static bool werewolfSpecialAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pSelf.a.hasStatus("wolf_attack"))
        {
            pSelf.a.addStatusEffect("wolf_attack");
            return true;
        }
        return false;
    }
    #endregion

    #region special effects
    public static bool titanShifterSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        //Randomly chance to remove the titan trait
        if (Randy.randomChance(0.2f) && pTarget.a.hasTrait("titan")) //Percent
        {
            pTarget.a.removeTrait("titan");
            DarkieTraitsMain.LogInfo($"{pTarget.a.name} has removed trait titan");
            return true;
        }
        return false;
    }

    public static bool thorSparklingSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pTarget.isAlive())
            return false;
        if (!pTarget.a.hasTrait("fire_proof"))
            pTarget.a.addTrait("fire_proof");
        pTarget.a.spawnParticle(UnityEngine.Color.blue);
        pTarget.a.spawnParticle(UnityEngine.Color.white);
        pTarget.a.spawnParticle(UnityEngine.Color.blue);
        pTarget.a.spawnParticle(UnityEngine.Color.blue);
        pTarget.a.spawnParticle(UnityEngine.Color.red);

        //low health, summon Mjolnir
        if (pTarget.a.data.health < pTarget.a.getMaxHealth() / 10)
        {
            //To do later
        }
        return true;
    }

    public static bool nightCrawlerSparklingSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pTarget.isAlive())
            return false;
        pTarget.a.spawnParticle(UnityEngine.Color.red);
        pTarget.a.spawnParticle(UnityEngine.Color.black);
        pTarget.a.spawnParticle(UnityEngine.Color.red);
        pTarget.a.spawnParticle(UnityEngine.Color.red);
        if (Randy.randomChance(0.3f) && pTarget.a.is_moving) //Percent
        {
            teleportToSpecificLocation(pTarget, pTarget.a.tile_target);
        }
        return true;
    }

    public static bool mageSparklingSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pTarget.isAlive())
            return false;
        pTarget.a.spawnParticle(UnityEngine.Color.white);
        pTarget.a.spawnParticle(UnityEngine.Color.cyan);
        pTarget.a.spawnParticle(UnityEngine.Color.white);
        pTarget.a.spawnParticle(UnityEngine.Color.white);
        pTarget.a.spawnParticle(UnityEngine.Color.white);
        pTarget.a.spawnParticle(UnityEngine.Color.white);
        if (!pTarget.a.hasTrait("fire_proof"))
            pTarget.a.addTrait("fire_proof");
        if (pTarget.a.hasTrait("cursed"))
            pTarget.a.removeTrait("cursed");
        if (Randy.randomChance(0.02f))
        {
            pTarget.a.doCastAnimation();
            ActionLibrary.teleportRandom(null, pTarget, null);
        }
        if (pTarget.a.data.health <= pTarget.a.getMaxHealth() / 2)
        {
            ActionLibrary.castBloodRain(null, pTarget, null);
        }
        return true;
    }


    public static bool tamedBeastSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pTarget.isAlive())
            return false;

        Actor beast = pTarget.a;
        //If already in list then just check if master is alive to follow
        if (listOfTamedBeasts.ContainsKey(beast))
        {
            Actor master = listOfTamedBeasts[beast];
            if (master != null && master.isAlive())
            {
                if (Randy.randomChance(0.09f))
                {
                    beast.goTo(master.current_tile);
                    return true;
                }
            }
        }
        else
        {
            //When reloading save or game, we need to re-populate the list tamed beasts
            if (beast.data.custom_data_long.TryGetValue("master_id", out long masterId))
            {
                Actor master = World.world.units.get(masterId);
                if (master != null && master.isAlive())
                {
                    listOfTamedBeasts.Add(beast, master);
                    DarkieTraitsMain.LogInfo($"[Darkie TamedBeasts] Re-added beast {beast.getName()} with master {master.getName()}");
                    beast.kingdom = master.kingdom;
                    return true;
                }
            }
        }
        return false;
    }

    public static bool medicSuperHealing(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pTarget.isAlive())
            return false;
        pTarget.a.spawnParticle(Toolbox.color_heal);
        pTarget.a.spawnParticle(Toolbox.color_heal);
        pTarget.a.spawnParticle(Toolbox.color_heal);
        if (!pTarget.a.hasTrait("immune"))
            pTarget.a.addTrait("immune");

        if (Randy.randomChance(0.2f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 5);
            if (allClosestUnits.Any())
            {
                foreach (var unit in allClosestUnits)
                {
                    if (unit.a.kingdom == pTarget.a.kingdom)
                    {
                        removeBadTrait(unit);
                        if (unit.a.data.health < unit.a.getMaxHealth())
                        {
                            unit.a.restoreHealth(10);
                            unit.a.spawnParticle(Toolbox.color_heal);
                        }
                    }
                }
            }
        }
        return true;
    }

    public static bool commanderSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pTarget.isAlive())
            return false;
        if (!pTarget.a.isProfession(UnitProfession.Warrior))
        {
            pTarget.a.setProfession(UnitProfession.Warrior);
            if (!pTarget.a.is_army_captain)
            {
                var army = pTarget.a.army;
                if (army != null)
                {
                    army.setCaptain(pTarget.a);
                }
            }
        }
        //If army is full then no need to convert units
        if (pTarget.getCity().isArmyFull() || pTarget.getCity().isArmyOverLimit())
        {
            return false;
        }
        //Gradually increasing warriors count
        if (Randy.randomChance(0.1f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 5);
            if (allClosestUnits.Any())
            {
                foreach (var unit in allClosestUnits)
                {
                    if (unit.a.kingdom == pTarget.a.kingdom)
                    {
                        //Convert into army
                        if (!unit.a.isProfession(UnitProfession.King) || !unit.a.isProfession(UnitProfession.Leader))
                        {
                            unit.a.setProfession(UnitProfession.Warrior);
                        }
                    }
                }
            }
        }
        return true;
    }

    public static bool livingHellEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pTarget.isAlive())
            return false;
        pTarget.a.spawnParticle(Toolbox.color_plague);
        pTarget.a.spawnParticle(Toolbox.color_plague);
        pTarget.a.spawnParticle(Toolbox.color_plague);
        //pTarget.a.asset.can_be_killed_by_life_eraser = false;        //not even life eraser can save you from this eternal nightmare that I made for you
        //pTarget.a.asset.can_be_hurt_by_powers = false;
        //pTarget.a.asset.can_be_cloned = false;
        //pTarget.a.asset.can_be_killed_by_divine_light = false;
        pTarget.a.addTrait("madness");
        pTarget.a.removeTrait("cursed");
        pTarget.a.data.favorite = true; //for easier finding

        if (pTarget.a.data.health < pTarget.a.getMaxHealth() / 2)        //You don't get to die that easy, little piece of sh!t
        {
            pTarget.a.restoreHealth(9999);
        }
        if (pTarget.a.data.health < pTarget.a.getMaxHealth() / 10)        //tele random
        {
            ActionLibrary.teleportRandom(null, pTarget, null);
        }
        if (Randy.randomChance(0.2f))
        {
            MapBox.instance.drop_manager.spawn(pTarget.current_tile, "acid", 10f, -1f);
            MapBox.instance.drop_manager.spawn(pTarget.current_tile, "fire", 5f, -1f);
            MapBox.instance.drop_manager.spawn(pTarget.current_tile, "acid", 10f, -1f);
            MapBox.instance.drop_manager.spawn(pTarget.current_tile, "fire", 5f, -1f);
            MapBox.instance.drop_manager.spawn(pTarget.current_tile, "acid", 10f, -1f);
            MapBox.instance.drop_manager.spawn(pTarget.current_tile, "fire", 5f, -1f);
        }
        if (Randy.randomChance(0.1f))
        {
            pTarget.addStatusEffect("frozen", 3);
        }
        if (Randy.randomChance(0.1f))
        {
            pTarget.addStatusEffect("poisoned", 13);
        }
        if (Randy.randomChance(0.1f))
        {
            pTarget.addStatusEffect("stunned", 13);
        }
        if (Randy.randomChance(0.1f))
        {
            pTarget.addStatusEffect("burning", 13);
        }
        if (Randy.randomChance(0.1f))
        {
            ActionLibrary.castLightning(null, pTarget, null);
        }
        if (Randy.randomChance(0.2f))
        {
            ActionLibrary.castCurses(null, pTarget, null);
        }
        if (Randy.randomChance(0.05f))
        {
            EffectsLibrary.spawn("fx_meteorite", pTarget.current_tile, "meteorite_disaster", null, 0f, -1f, -1f);    //spawn 1 meteorite
        }
        return true;
    }

    //if moon era is active, turn into werewolves
    public static bool turnWerewolvesSpecialEffect(BaseSimObject pTarget, WorldTile pTile)
    {
        if (pTarget.a != null)
        {
            if (World.world_era.id == "age_moon")       //only in age of moon
            {
                if (!pTarget.a.hasTrait("the_werewolf"))
                {
                    pTarget.a.addTrait("the_werewolf");
                }
            }
            else if (World.world_era.id == "age_dark")
            {
                if (Randy.randomChance(0.01f))
                {
                    if (!pTarget.a.hasTrait("the_werewolf"))
                    {
                        pTarget.a.addTrait("the_werewolf");
                    }
                }
            }
            else
            {
                if (pTarget.a.hasTrait("the_werewolf"))          //no other age can have this trait
                {
                    pTarget.a.removeTrait("the_werewolf");
                }
            }
        }
        return true;
    }


    #endregion

    #region get hit action
    public static bool titanShifterGetHit(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
    {
        DarkieTraitsMain.LogInfo($"Test");
        if (pSelf == null || !pSelf.isAlive() || pSelf.a.hasTrait("titan"))
        {
            DarkieTraitsMain.LogInfo($"No titan transformation");
            return false;
        }
        //Add new trait
        pSelf.a.addTrait("titan", true);
        //Shockwave
        BaseEffect baseEffect = EffectsLibrary.spawnAtTile("fx_lightning_medium", pTile, 0.25f);
        World.world.applyForceOnTile(pTile, 3, 0.5f, pForceOut: true, 0, null, pByWho: pSelf); //Ignore force for self
        EffectsLibrary.spawnExplosionWave(pTile.posV3, 3f, 0.5f);
        //Only spawn lightning effect without the actual damage
        return true;
    }

    public static bool medicGetHit(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
    {
        //Heal themselves on being hit
        if(Randy.randomChance(0.4f) && (pSelf.a.data.health < pSelf.a.getMaxHealth()))
        {
            pSelf.a.restoreHealth(10);
            pSelf.a.spawnParticle(Toolbox.color_heal);
        }
        return true;
    }

    public static bool antManGetHit(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
    {
        //shrink down size
        if (!pSelf.a.hasStatus("ant_man_effect"))
        {
            pSelf.a.addStatusEffect("ant_man_effect", 30f);
        }
        return true;
    }

    #endregion



    #region Custom Functions
    public static bool teleportToSpecificLocation(BaseSimObject pTarget, WorldTile pTile)
    {

        string text = pTarget.a.asset.effect_teleport;
        if (string.IsNullOrEmpty(text))
        {
            text = "fx_teleport_blue";
        }
        EffectsLibrary.spawnAt(text, pTarget.current_position, 0.1f);
        pTarget.a.cancelAllBeh();
        pTarget.a.spawnOn(pTile, 0f);
        return true;
    }

    private static bool removeBadTrait(BaseSimObject pTarget)
    {
        if (pTarget.a != null)
        {
            //insert bad traits here
            pTarget.a.removeTrait("madness");
            pTarget.a.removeTrait("evil");
            pTarget.a.removeTrait("cursed");
            pTarget.a.removeTrait("greedy");
            pTarget.a.removeTrait("deceitful");
            pTarget.a.removeTrait("pyromaniac");
            pTarget.a.removeTrait("infected");
            pTarget.a.removeTrait("mushSpores");
            pTarget.a.removeTrait("tumorInfection");
            pTarget.a.removeTrait("scar_of_divinity");  //kinda hate this trait
        }
        return true;
    }
    #endregion




    //// This method will be called when config value set. ATTENTION: It might be called when game start.
    //public static void ExampleSwitchConfigCallBack(bool pCurrentValue)
    //{
    //    //DarkieTraitsMain.LogInfo($"Current value of a switch config is '{pCurrentValue}'");
    //}

    //// This method will be called when config value set. ATTENTION: It might be called when game start.
    //public static void ExampleSliderConfigCallback(float pCurrentValue)
    //{
    //    //ExampleModMain.LogInfo($"Current value of a slider config is '{pCurrentValue}'");
    //}
}