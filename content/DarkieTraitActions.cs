using NeoModLoader.api.attributes;
using static UnityEngine.GraphicsBuffer;

namespace DarkieCustomTraits.Content;

internal static class DarkieTraitActions
{
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