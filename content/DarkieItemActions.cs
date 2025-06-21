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
            if (pTarget.a != null)
            {
                if (Randy.randomChance(0.1f))
                {
                    //Get all units from other kingdoms in the area
                    var allClosestUnits = Finder.getUnitsFromChunk(pTile, 7);
                    if (allClosestUnits.Any())
                    {
                        foreach (var unit in allClosestUnits)
                        {
                            if(unit.a.kingdom != pSelf.a.kingdom)
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
                if (Randy.randomChance(0.6f))
                {
                    teleportToSpecificLocation(pSelf, pSelf, pTarget.current_tile);
                }
            }
            return true;
        }


        public static bool teleportToSpecificLocation(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            string text = "fx_CustomTeleport_effect";
            if (string.IsNullOrEmpty(text))
            {
                text = "fx_teleport_blue";
            }
            EffectsLibrary.spawnAt(text, pTarget.current_tile.pos, 1.0f);
            BaseEffect baseEffect = EffectsLibrary.spawnAt(text, pTile.posV3, 1.0f);
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
                            BaseEffect baseEffect = EffectsLibrary.spawnAtTile("fx_lightning_medium", pTile, 0.25f);
                            int pRad = (int)(0.15f * 25f);
                            MapAction.checkLightningAction(pTile.pos, pRad);
                            //MapAction.damageWorld(pTile, pRad, AssetManager.terraform.get("lightning_normal"), pActor);
                            baseEffect.sprite_renderer.flipX = Randy.randomBool();
                            MapAction.checkTornadoHit(pTile.pos, pRad);
                        }
                    }
                }
            }

            return true;
        }

        #endregion
    }
}
