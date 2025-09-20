using ai;
using HarmonyLib.Tools;
using NeoModLoader.api.attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.CanvasScaler;

namespace DarkieCustomTraits.Content;

internal static class DarkieTraitActions
{
    private static Dictionary<Actor, Actor> listOfTamedBeasts = new Dictionary<Actor, Actor>();
    private static Dictionary<ActorData, Actor> listOfTamedBeastsData = new Dictionary<ActorData, Actor>();

    #region Attack Action
    public static bool causeShockwave(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        //Stun target
        if (!pTarget.isBuilding() && !pTarget.isRekt())
        {
            pTarget.addStatusEffect("stunned");
        }
        if (Randy.randomChance(0.1f))
        {
            //Shockwave
            World.world.applyForceOnTile(pTile, 3, 1.0f, pForceOut: true, 0, null, pByWho: pSelf); //Ignore force for self
            EffectsLibrary.spawnExplosionWave(pTile.posV3, 1f, 0.5f);
        }
        return true;
    }

    public static bool titanShifterAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        //Add effect to transform into titan. After effect end, the titan trait will be removed automatically
        if (!pSelf.a.hasStatus("titan_shifter_effect"))
        {
            BaseEffect baseEffect = EffectsLibrary.spawnAtTile("fx_DarkieExplosionBlueOval_effect", pTarget.current_tile, 0.1f);
            pSelf.a.addStatusEffect("titan_shifter_effect");
        }
        if (Randy.randomChance(0.1f)) //Percent
        {
            //Shockwave
            World.world.applyForceOnTile(pTile, 3, 0.5f, pForceOut: true, 0, null, pByWho: pSelf); //Ignore force for self
            EffectsLibrary.spawnExplosionWave(pTile.posV3, 3f, 0.5f);
        }
        return true;
    }

    public static bool antManAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        //Add effect to transform into ant man. After effect end, actor will be back to normal size
        if (!pSelf.a.hasStatus("ant_man_effect"))
            pSelf.a.addStatusEffect("ant_man_effect");
        return true;
    }

    public static bool explosionAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;

        //Spawn random explosion effect
        if (Randy.randomChance(0.1f)) //Percent
        {
            EffectsLibrary.spawnAtTile("fx_DarkieExplosionTwoColor_effect", pTarget.current_tile, 0.25f);
        }
        else if (Randy.randomChance(0.01f))
        {
            EffectsLibrary.spawnAtTile("fx_explosion_crab_bomb", pTarget.current_tile, 0.25f);
        }
        else if (Randy.randomChance(0.1f))
        {
            EffectsLibrary.spawnAtTile("fx_explosion_meteorite", pTarget.current_tile, 0.25f);
        }
        else if (Randy.randomChance(0.05f))
        {
            EffectsLibrary.spawnAtTile("fx_DarkieExplosionCircle_effect", pTarget.current_tile, 0.25f);
        }
        else if (Randy.randomChance(0.05f))
        {
            //Shockwave
            World.world.applyForceOnTile(pTile, 3, 0.5f, pForceOut: true, 0, null, pByWho: pSelf); //Ignore force for self
            EffectsLibrary.spawnExplosionWave(pTile.posV3, 3f, 0.5f);
        }
        return true;
    }

    public static bool thorGodThunderAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        if (Randy.randomChance(0.3f) && pTarget != null && pTarget.current_tile != null) //Percent
        {
            //Only spawn lightning effect without the actual damage
            EffectsLibrary.spawnAtTile("fx_lightning_medium", pTarget.current_tile, 0.4f);
            if (Randy.randomChance(0.05f))
                World.world.applyForceOnTile(pTarget.current_tile, 3, 0.5f, pForceOut: true, 0, null, pByWho: pSelf); //Ignore force for self
            //Small chance of electrocute everyone around
            if (Randy.randomChance(0.01f)) //Percent
            {
                //Get all units  in the area
                var allClosestUnits = Finder.getUnitsFromChunk(pTarget.current_tile, 1);
                if (allClosestUnits.Any())
                {
                    foreach (var unit in allClosestUnits)
                    {
                        if (pSelf.a == unit.a) continue;
                        unit.addStatusEffect("custom_electrocuted_effect", 4f);
                    }
                }
            }
            return true;
        }
        return false;
    }

    public static bool nightCrawlerAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        if (Randy.randomChance(0.3f)) //Percent
        {
            EffectsLibrary.spawnAtTile("fx_DarkieExplosionBlueCircle_effect", pTarget.current_tile, 0.05f);
            ActionLibrary.teleportRandom(pSelf, pTarget, null);
            return true;
        }
        return false;
    }


    public static bool shieldGuyAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        //he wil use shield when in combat
        if (!pSelf.a.hasStatus("shield"))
        {
            ActionLibrary.castShieldOnHimself(pSelf, pSelf, pTile);
        }
        //Rare chance to cast shield on ally of same kingdom
        if (Randy.randomChance(0.01f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 3);
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
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
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
            act.data.name = $"Wolf of {pSelf.a.getName()}";
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
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
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
            act.data.name = $"Bear of {pSelf.a.getName()}";
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
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
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
            act.data.name = $"Dragon of {pSelf.a.getName()}";
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
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
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
            act.data.name = $"Bandit Friend of {pSelf.a.getName()}";
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
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
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
        if (Randy.randomChance(0.04f))
        {
            pTarget.a.addStatusEffect("time_stop_effect");
        }
        if (Randy.randomChance(0.04f))
        {
            pTarget.a.addStatusEffect("custom_electrocuted_effect");
        }
        if (Randy.randomChance(0.05f))
        {
            ActionLibrary.castLightning(pSelf, pTarget, null);
        }
        if (Randy.randomChance(0.05f))
        {
            EffectsLibrary.spawnAtTile("fx_DarkieExplosionCircle_effect", pTarget.current_tile, 0.25f);
            World.world.applyForceOnTile(pTile, 3, 0.5f, pForceOut: true, 0, null, pByWho: pSelf); //Ignore force for self
            EffectsLibrary.spawnExplosionWave(pTile.posV3, 3f, 0.5f);
        }
        return true;
    }

    public static bool wololoConvertUnitAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
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
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        if (!pSelf.a.hasStatus("wolf_attack_effect"))
        {
            pSelf.a.addStatusEffect("wolf_attack_effect");
            return true;
        }
        return false;
    }

    public static bool powerMimicryAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        //This one will steal enemy traits
        if (Randy.randomChance(0.3f) && !pTarget.a.hasTrait("power_mimicry"))
        {
            pSelf.a.removeTrait("miracle_born");
            pSelf.a.removeTrait("scar_of_divinity");

            pSelf.a.takeItems(pTarget.a, 1, 5);
            foreach (var trait in pTarget.a.getTraits())
            {
                if (trait.id == "the_mysterious_trait") continue;
                pSelf.a.addTrait(trait, true);
            }
            pTarget.addStatusEffect("slowness", 3f);
            return true;
        }
        if (pSelf.a.getTraits().Count > 12)
        {
            pSelf.a.removeTrait("power_mimicry");
        }
        return false;
    }

    public static bool nullifyAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        //This one will del enemy traits
        if (Randy.randomChance(0.2f))
        {
            int randomIndex = Randy.randomInt(0, pTarget.a.getTraits().Count);
            List<ActorTrait> targetTraits = pTarget.a.getTraits().ToList();
            if (targetTraits.Count > 0)
            {
                var randomTrait = targetTraits[randomIndex];
                if (randomTrait.id == "nullify" || randomTrait.id == "the_mysterious_trait")
                    return false;
                pTarget.a.removeTrait(randomTrait.id);
            }
        }
        return false;
    }

    public static bool necromancerAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        if (pSelf.a.data.health < pSelf.a.getMaxHealth() / 2)
        {
            //healing and summon skeleton
            ActionLibrary.castBloodRain(pSelf, pSelf, null);
        }
        if (Randy.randomChance(0.2f))
        {
            ActionLibrary.castSpawnSkeleton(pSelf, pTarget, pTile);
        }
        if (Randy.randomChance(0.1f))
        {
            //make enemies cursed
            pTarget.a.addTrait("cursed");
        }
        return true;
    }

    /*???????*/
    public static bool theMysteriousTraitAttackSpecialEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        if (pTarget == null || pTarget.a == null) return false;
        if (!pTarget.a.isAlive()) return false;
        if (Randy.randomChance(0.5f))
        {
            pTarget.a.addTrait("madness");
        }
        if (Randy.randomChance(0.1f))
        {
            ActionLibrary.castTornado(pSelf, pTarget, pTile);
        }
        if (Randy.randomChance(0.1f))
        {
            ActionLibrary.unluckyMeteorite(pTarget, pTarget.current_tile);    //spawn 1 meteorite
        }
        //ulti
        if (Randy.randomChance(0.05f))
        {
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 1);
            if (allClosestUnits.Any())
            {
                string text = "fx_YOYO_effect";
                if (Randy.randomBool())
                {
                    text = "fx_DarkieDarkBlueWave_effect";
                }
                EffectsLibrary.spawnAtTile(text, pTarget.current_tile, 0.35f);
                foreach (var unit in allClosestUnits)
                {
                    if (unit.a != pSelf.a)
                    {
                        unit.die();
                    }
                }
            }
        }

        if (Randy.randomChance(0.01f))
        {
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 1);
            if (allClosestUnits.Any())
            {
                foreach (var unit in allClosestUnits)
                {
                    if (unit.a != pSelf.a)
                    {
                        //void
                        //banish
                        if (Randy.randomChance(0.4f))
                        {
                            ActionLibrary.teleportRandom(pSelf, unit, null);
                            return true;
                        }
                        //strike lightning
                        if (Randy.randomChance(0.3f))
                        {
                            ActionLibrary.castLightning(pSelf, unit, null);
                        }
                        if (Randy.randomChance(0.3f))
                        {
                            unit.addStatusEffect("slowness", 3f);
                        }
                        if (Randy.randomChance(0.1f))
                        {
                            unit.addStatusEffect("custom_electrocuted_effect");
                        }
                        if (Randy.randomChance(0.1f))
                        {
                            unit.addStatusEffect("time_stop_effect");
                        }
                    }
                }
            }
        }
        return true;
    }

    public static bool vampireAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        if (pSelf == null || pSelf.a == null || !pSelf.a.isAlive()) return false;
        if (!pSelf.a.hasWeapon() && !pSelf.a.asset.id.Equals("vampire_bat"))
        {
            //If no weapon, give teleport dagger
            var dagger = AssetManager.items.get("teleport_dagger");
            if (dagger != null)
            {
                var pData = World.world.items.generateItem(pItemAsset: dagger);
                if (pData != null)
                {
                    if (pSelf.a.equipment != null)
                    {
                        var pSlot = pSelf.a.equipment.getSlot(EquipmentType.Weapon);
                        if (pSlot != null)
                        {
                            pSlot.setItem(pData, pSelf.a);
                        }
                    }
                }
            }
        }
        if (Randy.randomChance(0.1f))
        {
            pTarget.a.restoreHealth(10);
        }
        if (Randy.randomChance(0.1f) && pSelf.kingdom != null)
        {
            pTarget.kingdom = pSelf.kingdom;
            pTarget.a.addTrait("chained");
        }
        if (Randy.randomChance(0.1f))
        {
            pTarget.a.addTrait("madness");
        }
        return true;
    }

    public static bool mirrorManSpecialAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        bool flagCreateMirroActor = true;
        if (Randy.randomChance(0.01f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 1);
            if (allClosestUnits.Any())
            {
                foreach (var unit in allClosestUnits)
                {
                    if (unit.a.hasTrait("the_mirroed"))
                    {
                        flagCreateMirroActor = false;
                    }
                }
            }
        }
        if (flagCreateMirroActor)
        {
            var act = World.world.units.createNewUnit(pTarget.a.asset.id, pTile);
            ActorTool.copyUnitToOtherUnit(pTarget.a, act);          //clone enemies
            act.kingdom = pSelf.kingdom;        //the caster kingdom
            act.addTrait("the_mirroed");
        }
        return true;
    }

    public static bool timeStopperSpecialAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        if (Randy.randomChance(0.01f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 2);
            if (allClosestUnits.Any())
            {
                //Spawn cool effect
                pSelf.a.addStatusEffect("time_stop_ultimate_effect");
                foreach (var unit in allClosestUnits)
                {
                    if (unit.kingdom != pSelf.kingdom && unit.a != pSelf.a && !unit.a.hasStatus("time_stop_effect"))
                    {
                        unit.a.addStatusEffect("time_stop_effect");
                    }
                    else if (unit.kingdom == pSelf.kingdom && unit.a != pSelf.a && unit.a.hasStatus("time_stop_effect"))
                    {
                        //Help allies remove status
                        unit.a.finishStatusEffect("time_stop_effect");
                        unit.a.finishStatusEffect("frozen");
                    }
                }
            }
        }
        if (Randy.randomChance(0.1f) && !pTarget.a.hasStatus("time_stop_effect"))
        {
            pTarget.a.addStatusEffect("time_stop_effect");
        }
        if (Randy.randomChance(0.1f))
        {
            //Make target older
            pTarget.a.data.age_overgrowth += 20;
        }

        return true;
    }

    public static bool electroSpecialAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        if (Randy.randomChance(0.01f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 1);
            if (allClosestUnits.Any())
            {
                //Spawn cool effect
                foreach (var unit in allClosestUnits)
                {
                    if (unit.kingdom != pSelf.kingdom && unit.a != pSelf.a && !unit.a.hasStatus("custom_electrocuted_effect"))
                    {
                        unit.a.addStatusEffect("custom_electrocuted_effect");
                    }
                }
            }
        }
        if (Randy.randomChance(0.1f) && !pTarget.a.hasStatus("custom_electrocuted_effect"))
        {
            pTarget.a.addStatusEffect("custom_electrocuted_effect");
        }
        return true;
    }


    internal static bool silencerSpecialAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;

        if (Randy.randomChance(0.01f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 1);
            if (allClosestUnits.Any())
            {
                //Spawn cool effect
                foreach (var unit in allClosestUnits)
                {
                    if (unit.kingdom != pSelf.kingdom && unit.a != pSelf.a && !unit.a.hasStatus("custom_muted_effect"))
                    {
                        unit.a.addStatusEffect("custom_muted_effect");
                    }
                }
            }
        }
        if (Randy.randomChance(0.1f) && !pTarget.a.hasStatus("custom_muted_effect"))
        {
            pTarget.a.addStatusEffect("custom_muted_effect");
        }

        //low health, summon weapon
        if (pSelf.a.data.health < pSelf.a.getMaxHealth() / 4)
        {
            var weapon = AssetManager.items.get("speechless_sword");
            var pData = World.world.items.generateItem(pItemAsset: weapon);
            var pSlot = pSelf.a.equipment.getSlot(EquipmentType.Weapon);
            pSlot.setItem(pData, pSelf.a);
            pSelf.setStatsDirty();
        }
        return true;
    }
    #endregion

    #region special effects
    public static bool titanShifterSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pTarget.a.hasStatus("titan_shifter_effect"))
        {
            BaseEffect baseEffect = EffectsLibrary.spawnAtTile("fx_DarkieExplosionBlueOval_effect", pTarget.current_tile, 0.1f);
            pTarget.a.addStatusEffect("titan_shifter_effect");
        }
        return false;
    }

    public static bool thorSparklingSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
            return false;
        if (!pTarget.a.hasTrait("fire_proof"))
            pTarget.a.addTrait("fire_proof");
        pTarget.a.spawnParticle(UnityEngine.Color.blue);
        pTarget.a.spawnParticle(UnityEngine.Color.white);
        pTarget.a.spawnParticle(UnityEngine.Color.blue);
        pTarget.a.spawnParticle(UnityEngine.Color.blue);
        pTarget.a.spawnParticle(UnityEngine.Color.red);
        //low health, summon Mjolnir
        if (pTarget.a.data.health < pTarget.a.getMaxHealth() / 4 && !pTarget.a.asset.id.Equals("vampire_bat"))
        {
            var weapon = AssetManager.items.get("mjolnir");
            var pData = World.world.items.generateItem(pItemAsset: weapon);
            var pSlot = pTarget.a.equipment.getSlot(EquipmentType.Weapon);
            pSlot.setItem(pData, pTarget.a);
            pTarget.setStatsDirty();
            EffectsLibrary.spawnAtTile("fx_lightning_small", pTile, 0.1f);
            ActionLibrary.castBloodRain(pTarget, pTarget, pTile);
            if (!pTarget.a.hasStatus("sparkling_effect"))
                pTarget.a.addStatusEffect("sparkling_effect");

        }
        return true;
    }

    public static bool nightCrawlerSparklingSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
        pTarget.a.spawnParticle(UnityEngine.Color.red);
        pTarget.a.spawnParticle(UnityEngine.Color.black);
        pTarget.a.spawnParticle(UnityEngine.Color.red);
        pTarget.a.spawnParticle(UnityEngine.Color.red);
        pTarget.a.asset.effect_teleport = "fx_DarkieExplosionBlueCircle_effect";
        if (Randy.randomChance(0.3f) && pTarget.a.is_moving) //Percent
        {
            teleportToSpecificLocation(pTarget, pTarget.a.tile_target);
        }
        return true;
    }

    public static bool mageSparklingSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
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
            ActionLibrary.teleportRandom(pTarget, pTarget, null);
        }
        if (Randy.randomChance(0.02f))
        {
            pTarget.a.addStatusEffect("sparkling_effect");
        }
        if (pTarget.a.data.health <= pTarget.a.getMaxHealth() / 2)
        {
            ActionLibrary.castBloodRain(pTarget, pTarget, null);
        }
        return true;
    }


    public static bool tamedBeastSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
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
            if (beast.data?.custom_data_long != null &&
                beast.data.custom_data_long.TryGetValue("master_id", out long masterId))
            {
                Actor master = World.world.units.get(masterId);
                if (master != null)
                {
                    if (listOfTamedBeasts == null)
                    {
                        return false;
                    }
                    listOfTamedBeasts[beast] = master;
                    string beastName = beast.getName() ?? "<null>";
                    string masterName = master.getName() ?? "<null>";
                    DarkieTraitsMain.LogInfo($"[TamedBeasts] Re-added beast {beastName} with master {masterName}");
                    if (master.kingdom != null)
                        beast.kingdom = master.kingdom;

                    return true;
                }
            }
        }

        return false;
    }

    public static bool medicSuperHealing(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
            return false;
        pTarget.a.spawnParticle(Toolbox.color_heal);
        pTarget.a.spawnParticle(Toolbox.color_heal);
        pTarget.a.spawnParticle(Toolbox.color_heal);
        if (!pTarget.a.hasTrait("immune"))
            pTarget.a.addTrait("immune");

        if (Randy.randomChance(0.01f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 1);
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
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
            return false;

        Actor actor = pTarget.a;
        // Ensure commander is Warrior
        if (!actor.isProfession(UnitProfession.Warrior))
        {
            actor.setProfession(UnitProfession.Warrior);
            if (!actor.is_army_captain && actor.army != null)
            {
                actor.army.setCaptain(actor);
            }
        }

        // Check army capacity
        var city = pTarget.getCity();
        if (city == null || city.isArmyFull() || city.isArmyOverLimit())
            return false;

        // Try to convert nearby allies to warriors
        if (Randy.randomChance(0.01f))
        {
            var nearbyUnits = Finder.getUnitsFromChunk(pTile, 3);
            foreach (var unit in nearbyUnits)
            {
                if (unit.a.kingdom == actor.kingdom)
                {
                    // Don't change profession of kings or leaders
                    if (!unit.a.isProfession(UnitProfession.King) && !unit.a.isProfession(UnitProfession.Leader))
                    {
                        unit.a.setProfession(UnitProfession.Warrior);
                    }
                }
            }
        }

        return true;
    }


    public static bool livingHellEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
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
            ActionLibrary.teleportRandom(pTarget, pTarget, null);
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
            ActionLibrary.castLightning(pTarget, pTarget, null);
        }
        if (Randy.randomChance(0.2f))
        {
            ActionLibrary.castCurses(pTarget, pTarget, null);
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

    public static bool fullHungerSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget.a.data.nutrition < 70)
        {
            pTarget.a.data.nutrition = 99;
            return true;
        }
        return false;
    }

    public static bool insatiableSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget.a.data.nutrition > 20)
        {
            pTarget.a.data.nutrition = 1;
            return true;
        }
        return false;
    }

    public static bool clonePowerSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
            return false;
        if (pTarget.a.hasTrait("duplikate_clone"))
        {
            pTarget.a.removeTrait("duplikate");
            return false;
        }
        if (Randy.randomChance(0.2f))
        {
            int count = 0;
            if (pTarget.a.data.custom_data_int == null || !pTarget.a.data.custom_data_int.TryGetValue("duplikateCloneCount", out count))
            {
                pTarget.a.data.set("duplikateCloneCount", 0);
            }

            if (count < 7)
            {
                var act = World.world.units.createNewUnit(pTarget.a.asset.id, pTile, false);
                ActorTool.copyUnitToOtherUnit(pTarget.a, act);
                if (pTarget.kingdom.isAlive())
                    act.kingdom = pTarget.kingdom;
                act.addTrait("duplikate_clone");
                act.data.name = pTarget.a.getName();
                act.data.health += 1300;
                act.removeTrait("duplikate");
                count++;
                pTarget.a.data.set("duplikateCloneCount", count);
            }
        }
        return true;
    }

    public static bool killAllClone(BaseSimObject pTarget, WorldTile pTile = null)
    {
        foreach (Actor clone in MapBox.instance.units)
        {
            if (clone.a.hasTrait("duplikate_clone") && clone.a.getName().Equals(pTarget.a.getName()))
            {
                ActionLibrary.castLightning(pTarget, clone, null);
                ActionLibrary.removeUnit(clone.a);
            }
        }
        return true;
    }

    public static bool pheonixPowerSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
            return false;
        if (Randy.randomChance(0.2f))
        {
            removeBadTrait(pTarget);
        }
        return true;
    }

    public static bool rebornANew(BaseSimObject pTarget, WorldTile pTile = null)
    {
        ActionLibrary.removeUnit(pTarget.a);
        pTarget.a.addTrait("fire_proof"); //what kind of phoenix that got burned lol
        pTarget.a.addTrait("the_revived");
        pTarget.a.removeTrait("pheonix");
        EffectsLibrary.spawn("fx_spawn", pTarget.a.current_tile, null, null, 0f, -1f, -1f);
        var act = World.world.units.createNewUnit(pTarget.a.asset.id, pTile);
        ActorTool.copyUnitToOtherUnit(pTarget.a, act);
        if (pTarget.kingdom.isAlive())
            act.kingdom = pTarget.kingdom;
        act.data.name = pTarget.a.getName();
        act.data.favorite = pTarget.a.data.favorite;
        act.data.health += 1000;
        EffectsLibrary.spawnAtTile("fx_lightning_medium", pTile, 0.25f);
        act.addStatusEffect("phoenix_rise_effect", 7f);
        act.a.makeWait(3);
        act.addStatusEffect("invincible", 5);
        //spawn effect for cooler looks
        World.world.fx_divine_light.playOn(pTile);
        EffectsLibrary.spawnExplosionWave(pTile.posV3, 1f, 1f);
        World.world.applyForceOnTile(pTile, 3, 1.5f, pForceOut: true, 0, null, pByWho: act); //Ignore force for self
        return true;
    }

    public static bool reviveSpecialEffect(BaseSimObject pTarget, WorldTile pTile)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
            return false;
        // Define a mapping from zombie unit IDs to their living counterparts.
        var zombieToLivingUnitMap = new Dictionary<string, string>
    {
        { "zombie", "human" },
        { "zombie_human", "human" },
        { "zombie_dwarf", "dwarf" },
        { "zombie_orc", "orc" },
        { "zombie_elf", "elf" }
    };
        // Attempt to get the corresponding living unit ID for the target's asset ID.
        if (zombieToLivingUnitMap.TryGetValue(pTarget.a.asset.id, out string? livingUnitId))
        {
            // Create the new living unit.
            var newUnit = World.world.units.createNewUnit(livingUnitId, pTile);
            ActorTool.copyUnitToOtherUnit(pTarget.a, newUnit);
            EffectsLibrary.spawn("fx_spawn", pTarget.a.current_tile);
            // Remove the original zombie unit.
            ActionLibrary.removeUnit(pTarget.a);
        }
        else
        {
            // If the target is not a known zombie type, simply remove zombie-related traits.
            pTarget.a.removeTrait("zombie");
            pTarget.a.removeTrait("infected");
        }
        return true;
    }

    public static bool theMysteriousTraitSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        ///*??H???*/
        if (pTarget.a.data.health/*???E??*/ < pTarget.a.getMaxHealth() / 3)
        {
            /*???????*/
            pTarget.a.restoreHealth(pTarget.a.getMaxHealth());
        }
        /*???????*/
        pTarget.a.addTrait("fire_proof");/*???L??*/
        pTarget.a.addTrait("acid_proof");/*???????*/
        pTarget.a.addTrait("freeze_proof");/*???P??*/
        if (!pTarget.a.getName().Equals("?"))/*???????*/
        {/*???M??*/
            pTarget.a.data.name = "?";/*???????*/
        }/*???????*/
        pTarget.a.data.favorite = true;
        /*???????*/
        if (pTarget.a.hasTrait("madness"))
        {/*???E??*/
            if (Randy.randomChance(0.3f)/*???????*/)
            {/*???????*/
                pTarget.a.removeTrait("madness");/*???????*/
            }
        }/*!!!!!!!!!!!!!!!!!!!!*/
        if (!pTarget.a.hasTrait("madness"))
        {
            if (Randy.randomChance(0.1f))
            {
                pTarget.a.addTrait("madness"/*??HE???*/);
            }
        }
        if (Randy.randomChance(0.05f))
        {
            ActionLibrary.teleportRandom(pTarget, pTarget, null);
        }/*??ME!!??*/
        return true;
    }

    public static bool undoZombify(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
            return false;
        pTarget.a.removeTrait("infected");
        if (Randy.randomChance(0.02f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 1);
            if (allClosestUnits.Any())
            {
                foreach (var unit in allClosestUnits)
                {
                    if (unit.asset.id == "zombie_dwarf" || unit.asset.id == "zombie" || unit.asset.id == "zombie_human" || unit.asset.id == "zombie_orc" || unit.asset.id == "zombie_elf")
                    {
                        unit.kingdom = pTarget.kingdom;
                        reviveSpecialEffect(unit, pTile);
                        ActionLibrary.castBloodRain(pTarget, unit, null);
                        unit.addTrait("the_revived");
                        unit.removeTrait("infected");

                    }
                }
            }
        }
        return true;
    }

    public static bool necromancerSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {

        //remove cursed trait
        if (pTarget.a.hasTrait("cursed"))
        {
            pTarget.a.removeTrait("cursed");
        }
        //convert all nearby skeletons to his side
        if (Randy.randomChance(0.04f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 1);
            if (allClosestUnits.Any())
            {
                foreach (var unit in allClosestUnits)
                {
                    if (unit.kingdom != pTarget.a.kingdom && !unit.hasTrait("tamed_beasts"))
                    {
                        if (unit.asset.id == "skeleton" || unit.asset.id == "skeleton_cursed")
                        {
                            unit.kingdom = pTarget.kingdom;
                            unit.addTrait("tamed_beasts");
                            //Set master id so that it can be re-populate later
                            unit.data.set("master_id", pTarget.a.data.id);
                            unit.stats.set(CustomBaseStatsConstant.Scale, 0.1f);
                            if (!listOfTamedBeasts.ContainsKey(unit))
                                listOfTamedBeasts.Add(unit, pTarget.a);     //add the beast and actor who spawned them into custom list
                        }
                    }
                }
            }
        }
        if (Randy.randomChance(0.1f))
        {
            ActionLibrary.castSpawnSkeleton(pTarget, pTarget, pTile);
        }
        return true;
    }

    public static bool vampireSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        pTarget.a.spawnParticle(UnityEngine.Color.red);
        pTarget.a.spawnParticle(UnityEngine.Color.black);
        pTarget.a.spawnParticle(UnityEngine.Color.black);
        pTarget.a.spawnParticle(UnityEngine.Color.red);
        pTarget.a.spawnParticle(UnityEngine.Color.black);
        pTarget.a.spawnParticle(UnityEngine.Color.red);
        //for human form only
        if (!pTarget.a.asset.id.Equals("vampire_bat"))
        {
            pTarget.a.addTrait("immortal");
            pTarget.a.addTrait("heliophobia"); //Vampire hate sunlight of course
            if (pTarget.a.data.health < pTarget.a.getMaxHealth() / 4)
            {
                var act = World.world.units.createNewUnit("vampire_bat", pTile);
                ActorTool.copyUnitToOtherUnit(pTarget.a, act);
                act.kingdom = pTarget.kingdom;
                act.data.set("original_asset_id", pTarget.a.asset.id);
                //spawn 4 bats for decoy
                for (int i = 0; i < 4; i++)
                {
                    var decoy = World.world.units.createNewUnit("vampire_bat", pTile);
                    if (pTarget.a.kingdom != null)
                    {
                        decoy.kingdom = pTarget.kingdom;
                    }
                    decoy.a.addTrait("the_mirroed"); //Add this one so it will despawn after awhile
                }
                EffectsLibrary.spawn("fx_spawn", pTarget.a.current_tile, null, null, 0f, -1f, -1f);
                ActionLibrary.removeUnit(pTarget.a);
            }

        }
        //for bat form only
        if (pTarget.a.asset.id.Equals("vampire_bat"))
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                if (Randy.randomChance(0.1f))
                {
                    ActionLibrary.castBloodRain(pTarget, pTarget, null);
                }
            }
            if (pTarget.a.data.health == pTarget.a.getMaxHealth())
            {
                pTarget.a.data.custom_data_string.TryGetValue("original_asset_id", out string? originalAssetId);
                if (string.IsNullOrEmpty(originalAssetId))
                {
                    originalAssetId = "human";
                }

                var act = World.world.units.createNewUnit(originalAssetId, pTile);
                ActorTool.copyUnitToOtherUnit(pTarget.a, act);
                EffectsLibrary.spawn("fx_spawn", pTarget.a.current_tile, null, null, 0f, -1f, -1f);
                act.data.health = act.a.getMaxHealth();
                ActionLibrary.removeUnit(pTarget.a);
            }
        }
        return true;
    }

    public static bool theMirroedCloneEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pTarget.a.isProfession(UnitProfession.Warrior))
        {
            pTarget.a.setProfession(UnitProfession.Warrior);
        }
        pTarget.a.removeTrait("mirror_man");
        // The Mirrored will take gradual damage over time until death
        float healthLoss = pTarget.a.getMaxHealth() / 10;
        if (healthLoss < 1000f)
            healthLoss = 1000f;
        pTarget.getHit(healthLoss, true, AttackType.Weapon, null, true, false);
        return true;
    }

    public static bool mirrorManSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        //Add glass sword if no weapon
        if (!pTarget.a.hasWeapon() && !pTarget.a.asset.id.Equals("vampire_bat"))
        {
            //If no weapon, give weapon
            var dagger = AssetManager.items.get("glass_sword");
            if (dagger != null)
            {
                var pData = World.world.items.generateItem(pItemAsset: dagger);
                if (pData != null)
                {
                    if (pTarget.a.equipment != null)
                    {
                        var pSlot = pTarget.a.equipment.getSlot(EquipmentType.Weapon);
                        if (pSlot != null)
                        {
                            pSlot.setItem(pData, pTarget.a);
                        }
                    }
                }
            }
        }
        return true;
    }

    public static bool electroSparklingSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
            return false;
        pTarget.a.spawnParticle(UnityEngine.Color.yellow);
        pTarget.a.spawnParticle(UnityEngine.Color.yellow);
        pTarget.a.spawnParticle(UnityEngine.Color.white);
        pTarget.a.spawnParticle(UnityEngine.Color.yellow);
        if (Randy.randomChance(0.3f) && pTarget.a.is_moving) //Percent
        {
            teleportToSpecificLocation(pTarget, pTarget.a.tile_target, "fx_DarkieCustomTeleport_effect");
        }
        return true;
    }

    #endregion

    #region get hit action
    public static bool titanShifterGetHit(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
    {
        if (pSelf == null || pSelf.a == null || !pSelf.a.isAlive()) return false;
        if (pAttackedBy == null || pAttackedBy.a == null || !pAttackedBy.a.isAlive()) return false;

        //Shockwave
        //Only spawn lightning effect without the actual damage
        EffectsLibrary.spawnAtTile("fx_lightning_medium", pSelf.current_tile, 0.25f);
        World.world.applyForceOnTile(pSelf.current_tile, 3, 0.5f, pForceOut: true, 0, null, pByWho: pSelf); //Ignore force for self
        EffectsLibrary.spawnExplosionWave(pSelf.current_tile.posV3, 1f, 0.5f);
        //Add effect titan shifter, it will add trait titan and remove trait titan on finish
        if (!pSelf.a.hasStatus("titan_shifter_effect"))
        {
            EffectsLibrary.spawnAtTile("fx_DarkieExplosionBlueOval_effect", pSelf.current_tile, 0.1f);
            pSelf.a.addStatusEffect("titan_shifter_effect");

        }

        return true;
    }

    public static bool medicGetHit(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
    {
        if (pSelf == null || pSelf.a == null || !pSelf.a.isAlive()) return false;
        if (pAttackedBy == null || pAttackedBy.a == null || !pAttackedBy.a.isAlive()) return false;

        //Heal themselves on being hit
        if (Randy.randomChance(0.1f) && (pSelf.a.data.health < pSelf.a.getMaxHealth()/2))
        {
            pSelf.a.restoreHealth(10);
            pSelf.a.spawnParticle(Toolbox.color_heal);
        }
        return true;
    }

    public static bool antManGetHit(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
    {
        if (pSelf == null || pSelf.a == null || !pSelf.a.isAlive()) return false;
        if (pAttackedBy == null || pAttackedBy.a == null || !pAttackedBy.a.isAlive()) return false;

        //shrink down size
        //Add effect to transform into ant man. After effect end, actor will be back to normal size
        if (!pSelf.a.hasStatus("ant_man_effect"))
            pSelf.a.addStatusEffect("ant_man_effect");
        return true;
    }

    public static bool mirrorManGetHit(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
    {
        if (pSelf == null || pSelf.a == null || !pSelf.a.isAlive()) return false;
        if (pAttackedBy == null || pAttackedBy.a == null || !pAttackedBy.a.isAlive()) return false;

        if (Randy.randomChance(0.3f))
        {
            pAttackedBy.getHit(15, true, AttackType.Weapon, null, true, false);    //reflect a bit of damage back to enemies
        }
        return true;
    }

    public static bool timeStopperGetHit(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
    {
        if (pSelf == null || pSelf.a == null || !pSelf.a.isAlive()) return false;
        if (pAttackedBy == null || pAttackedBy.a == null || !pAttackedBy.a.isAlive()) return false;

        if (Randy.randomChance(0.2f) && !pAttackedBy.a.hasStatus("time_stop_effect"))
        {
            pAttackedBy.a.addStatusEffect("time_stop_effect");
        }
        return true;
    }
    #endregion



    #region Custom Functions
    public static bool teleportToSpecificLocation(BaseSimObject pTarget, WorldTile pTile, string effect_text = "fx_teleport_blue")
    {
        var size = 0.1f;
        if (effect_text == "fx_DarkieExplosionBlueCircle_effect")
        {
            size = 0.05f; //My effect is too big
        }
        EffectsLibrary.spawnAt(effect_text, pTarget.current_position, size);
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
            pTarget.a.removeTrait("mush_spores");
            pTarget.a.removeTrait("tumor_infection");
            pTarget.a.removeTrait("scar_of_divinity");  //kinda hate this trait
        }
        return true;
    }
    #endregion

}