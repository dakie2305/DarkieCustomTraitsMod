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

    public static bool tamedBeastSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pTarget.isAlive())
            return false;
        Actor beast = pTarget.a;
        if (beast.data.custom_data_long.TryGetValue("master_id", out long masterId))
        {
            Actor master = World.world.units.get(masterId);
            if (master != null && master.isAlive())
            {
                // Always sync beast into the list if not already there
                if (!listOfTamedBeasts.ContainsKey(beast))
                {
                    listOfTamedBeasts.Add(beast, master);
                    DarkieTraitsMain.LogInfo($"[Darkie TamedBeasts] Re-added beast {beast.getName()} with master {master.getName()}");
                    // Immediately update kingdom on reload to follow master's
                    beast.kingdom = master.kingdom;
                }

                // chance to follow master
                if (Randy.randomChance(0.3f))
                {
                    beast.goTo(master.current_tile);
                }
            }
        }

        return false;
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
    #endregion





    // This method will be called when config value set. ATTENTION: It might be called when game start.
    public static void ExampleSwitchConfigCallBack(bool pCurrentValue)
    {
        //DarkieTraitsMain.LogInfo($"Current value of a switch config is '{pCurrentValue}'");
    }

    // This method will be called when config value set. ATTENTION: It might be called when game start.
    public static void ExampleSliderConfigCallback(float pCurrentValue)
    {
        //ExampleModMain.LogInfo($"Current value of a slider config is '{pCurrentValue}'");
    }
}