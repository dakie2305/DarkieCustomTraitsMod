using ai;
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

    public static bool powerMimicryAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        //This one will steal enemy traits
        if (Randy.randomChance(0.2f) && !pTarget.a.hasTrait("power_mimicry"))
        {
            pSelf.a.removeTrait("miracle_born");
            pSelf.a.removeTrait("scar_of_divinity");

            pSelf.a.takeItems(pTarget.a, 1, 5);
            foreach(var trait in pTarget.a.getTraits())
            {
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
        if (pSelf.a.data.health < pSelf.a.getMaxHealth() / 2)
        {
            //healing and summon skeleton
            ActionLibrary.castBloodRain(pSelf, pSelf, null);
            if (Randy.randomChance(0.2f))
            {
                ActionLibrary.castSpawnSkeleton(pSelf, pTarget, pTile);
            }
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
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 3);
            if (allClosestUnits.Any())
            {
                foreach (var unit in allClosestUnits)
                {
                    if (unit.a != pSelf.a)
                    {
                        EffectsLibrary.spawnAtTile("fx_YOYO_effect", pSelf.current_tile, 0.35f);
                        unit.die();
                    }
                }
            }
        }

        if (Randy.randomChance(0.2f))
        {
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 3);
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
                    }
                }
            }
        }
        return true;
    }

    public static bool vampireAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pSelf.a.hasWeapon())
        {
            //If no weapon, give teleport dagger

            //ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("TeleportDagger"), "adamantine", 0, null, "", 1, null) as ItemData;
            //var pSlot = pSelf.a.equipment.getSlot(EquipmentType.Weapon);
            //pSlot.setItem(pData);
            //pSelf.setStatsDirty();
        }
        if (Randy.randomChance(0.1f))
        {
            pTarget.a.restoreHealth(10);
        }
        if (Randy.randomChance(0.1f))
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
        bool flagCreateMirroActor = true;
        if (Randy.randomChance(0.3f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 2);
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
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 3);
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
        if (Randy.randomChance(0.1f))
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
        if (!pTarget.isAlive())
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
                act.data.setName(pTarget.a.getName());
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
        if (!pTarget.isAlive())
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
        act.data.setName(pTarget.a.getName());
        act.data.favorite = pTarget.a.data.favorite;
        act.data.health += 1000;
        EffectsLibrary.spawnAtTile("fx_lightning_medium", pTile, 0.25f);
        act.addStatusEffect("phoenix", 7f);
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
        if (!pTarget.isAlive())
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
        if (pTarget.a.data.health/*???E??*/ < pTarget.a.getMaxHealth() / 8)
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
            ActionLibrary.teleportRandom(null, pTarget.a, null);
        }/*??ME!!??*/
        return true;
    }

    public static bool undoZombify(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (!pTarget.isAlive())
            return false;
        pTarget.a.removeTrait("infected");
        if (Randy.randomChance(0.2f))
        {
            //Get all units  in the area
            var allClosestUnits = Finder.getUnitsFromChunk(pTile, 3);
            if (allClosestUnits.Any())
            {
                foreach (var unit in allClosestUnits)
                {
                    if (unit.asset.id == "zombie_dwarf" || unit.asset.id == "zombie" || unit.asset.id == "zombie_human" || unit.asset.id == "zombie_orc" || unit.asset.id == "zombie_elf")
                    {
                        unit.kingdom = pTarget.kingdom;
                        reviveSpecialEffect(unit, pTile);
                        ActionLibrary.castBloodRain(null, unit, null);
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
        if (Randy.randomChance(0.4f))
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
        //for human form only
        if (!pTarget.a.asset.id.Equals("vampire_bat"))
        {
            pTarget.a.addTrait("immortal");
            if (pTarget.a.data.health < pTarget.a.getMaxHealth() / 2)
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
        pTarget.getHit(15, true, AttackType.Weapon, null, true, false);
        return true;
    }

    public static bool mirrorManSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
    {
        //Add glass sword if no weapon
        if (!pTarget.a.hasWeapon())
        {
            //ItemData pData = ItemGenerator.generateItem(AssetManager.items.get("GlassSword"), "adamantine", 0, null, "", 1, null) as ItemData;
            //var pSlot = pTarget.a.equipment.getSlot(EquipmentType.Weapon);
            //pSlot.setItem(pData);
            //pTarget.setStatsDirty();
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

    public static bool mirrorManGetHit(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
    {
        if (Randy.randomChance(0.5f))
        {
            pAttackedBy.getHit(15, true, AttackType.Weapon, null, true, false);    //reflect a bit of damage back to enemies
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
            pTarget.a.removeTrait("mush_spores");
            pTarget.a.removeTrait("tumor_infection");
            pTarget.a.removeTrait("scar_of_divinity");  //kinda hate this trait
        }
        return true;
    }


    #endregion




    //// This method will be called when config value set. ATTENTION: It might be called when game start.
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