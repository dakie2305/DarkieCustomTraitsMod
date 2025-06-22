using HarmonyLib;
using NCMS.Utils;
using NeoModLoader.api.attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DarkieCustomTraits.Content
{
    public class DarkieItemActions
    {
        #region Attack Action
        [Hotfixable]
        public static bool teleportDaggerAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            pSelf.a.asset.effect_teleport = "fx_DarkieCustomTeleport_effect"; //My very own effect
            if (Randy.randomChance(0.1f))
            {
                //Get all units from other kingdoms in the area
                var allClosestUnits = Finder.getUnitsFromChunk(pTile, 7);
                if (allClosestUnits.Any())
                {
                    foreach (var unit in allClosestUnits)
                    {
                        if (unit.a.kingdom != pSelf.a.kingdom && unit.a != pSelf.a)
                        {
                            unit.a.addStatusEffect("stunned", 3f);
                            pSelf.a.makeWait(1f);
                            if (unit.a.hasStatus("stunned") && Randy.randomChance(0.6f))
                            {
                                teleportToSpecificLocation(pSelf, pSelf, unit.a.current_tile);
                            }
                        }
                    }
                }
            }
            //Small chance to teleport to enemy destination
            if (Randy.randomChance(0.1f) && pTarget.a.is_moving)
            {
                teleportToSpecificLocation(pSelf, pSelf, pTarget.a.tile_target);
            }
            return true;
        }


        public static bool teleportToSpecificLocation(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            string? text = pSelf.a.asset.effect_teleport;
            if (string.IsNullOrEmpty(text))
            {
                text = "fx_teleport_blue";
            }
            EffectsLibrary.spawnAt(text, pTarget.current_tile.pos, 0.1f);
            BaseEffect baseEffect = EffectsLibrary.spawnAt(text, pTile.posV3, 0.1f);
            pTarget.a.cancelAllBeh();
            pTarget.a.spawnOn(pTile, 0f);
            return true;
        }

        public static bool thorWeaponAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            //only the worthy can wield the weapon
            if (pSelf.a != null)
            {
                pSelf.a.addTrait("fire_proof");
                if (pSelf.a.hasTrait("madness") || pSelf.a.hasTrait("cursed") || pSelf.a.hasTrait("evil") || pSelf.a.hasTrait("greedy") || pSelf.a.hasTrait("weak") || pSelf.a.hasTrait("deceitful") || pSelf.a.hasTrait("pyromaniac"))
                {
                    ActionLibrary.castLightning(pSelf, pSelf, null);
                    pSelf.a.die();
                    return false;
                }
                if (pSelf.a.asset.banner_id != "human" && pSelf.a.asset.banner_id != "elf" && pSelf.a.asset.banner_id != "orc" && pSelf.a.asset.banner_id != "dwarf")
                {
                    ActionLibrary.castLightning(null, pSelf, null);
                    pSelf.a.setHealth(0, false);
                    return false;
                }
            }

            //cast ligtning
            if (Randy.randomChance(0.3f))
            {
                //Get all units from other kingdoms in the area
                var allClosestUnits = Finder.getUnitsFromChunk(pTile, 7);
                if (allClosestUnits.Any())
                {
                    foreach (var unit in allClosestUnits)
                    {
                        if (unit.a.kingdom != pSelf.a?.kingdom)
                        {
                            BaseEffect baseEffect = EffectsLibrary.spawnAtTile("fx_lightning_medium", pTile, 0.1f);
                            //Just the effect
                            EffectsLibrary.spawnExplosionWave(pTile.posV3, 1f, 0.5f);
                        }
                    }
                }
            }
            return true;
        }


        public static bool iceSwordAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (!pSelf.a.hasTrait("freeze_proof"))
            {
                pSelf.a.addTrait("freeze_proof");
            }
            if (Randy.randomChance(0.05f))
            {
                if (!pSelf.a.hasStatus("ice_storm_effect"))
                {
                    pSelf.a.addStatusEffect("ice_storm_effect");
                    pSelf.a.makeWait(3f);
                    EffectsLibrary.spawnExplosionWave(pTile.posV3, 1f, 2f);
                    var allClosestUnits = Finder.getUnitsFromChunk(pTile, 7);
                    if (allClosestUnits.Any())
                    {
                        foreach (var unit in allClosestUnits)
                        {
                            if (unit.a.kingdom != pSelf.a?.kingdom && unit.a != pSelf.a)
                            {
                                unit.addStatusEffect("frozen", 6f);
                            }
                        }
                    }
                }
            }
            if (!pTarget.a.hasTrait("freeze_proof") && Randy.randomChance(0.1f))
            {
                pTarget.a.addStatusEffect("frozen", 5f);
            }
            return true;
        }

        public static bool glassSwordAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (Randy.randomChance(0.05f))
            {
                pTarget.a.addStatusEffect("bleeding", 10f);
                pTarget.a.addStatusEffect("slowness", 5f);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
                pTarget.a.spawnParticle(Toolbox.color_red);
            }
            return true;
        }

        #endregion
    }
}
