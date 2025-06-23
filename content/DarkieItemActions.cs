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
        public static bool teleportDaggerAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
            pSelf.a.asset.effect_teleport = "fx_DarkieCustomTeleport_effect"; //My very own effect
                                                                              //Small chance to teleport to enemy destination
            if (Randy.randomChance(0.01f) && pTarget.a.is_moving)
            {
                teleportToSpecificLocation(pSelf, pSelf, pTarget.a.tile_target);
            }
            if (Randy.randomChance(0.1f))
            {
                //Get all units from other kingdoms in the area
                var allClosestUnits = Finder.getUnitsFromChunk(pTile, 2);
                if (allClosestUnits.Any())
                {
                    foreach (var unit in allClosestUnits)
                    {
                        if (!unit.isAlive()) continue;
                        if (unit.a.kingdom != pSelf.a.kingdom && unit.a != pSelf.a)
                        {
                            if (Randy.randomChance(0.1f))
                                unit.a.addStatusEffect("stunned", 1f);
                            pSelf.a.makeWait(3f);
                            unit.getHit(10, true, AttackType.Weapon, pSelf, true, false);
                            if (unit.a.hasStatus("stunned") && Randy.randomChance(0.6f))
                            {
                                teleportToSpecificLocation(pSelf, pSelf, unit.a.current_tile);
                            }
                        }
                    }
                }
            }

            return true;
        }


        public static bool teleportToSpecificLocation(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
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
            if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
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
                var allClosestUnits = Finder.getUnitsFromChunk(pTile, 1);
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
            if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
            if (!pSelf.a.hasTrait("freeze_proof"))
            {
                pSelf.a.addTrait("freeze_proof");
            }
            if (!pTarget.a.hasTrait("freeze_proof") && Randy.randomChance(0.1f))
            {
                pTarget.a.addStatusEffect("frozen", 5f);
            }
            if (Randy.randomChance(0.05f))
            {
                if (!pSelf.a.hasStatus("ice_storm_effect"))
                {
                    pSelf.a.addStatusEffect("ice_storm_effect");
                    pSelf.a.makeWait(3f);
                    EffectsLibrary.spawnExplosionWave(pTile.posV3, 1f, 2f);
                    var allClosestUnits = Finder.getUnitsFromChunk(pTile, 1);
                    if (allClosestUnits.Any())
                    {
                        foreach (var unit in allClosestUnits)
                        {
                            if (unit.a.kingdom != pSelf.a?.kingdom && unit.a != pSelf.a)
                            {
                                unit.addStatusEffect("frozen", 4f);
                            }
                        }
                    }
                }
            }
            return true;
        }

        public static bool glassSwordAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
            if (Randy.randomChance(0.1f))
            {
                if (pTarget.a.isAlive() && !pTarget.a.hasStatus("bleeding_effect"))
                {
                    pTarget.a.addStatusEffect("bleeding_effect");
                    pTarget.a.addStatusEffect("slowness", 5f);
                }
            }
            return true;
        }

        #endregion
    }
}
