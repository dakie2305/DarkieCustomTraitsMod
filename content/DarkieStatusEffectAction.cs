using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.UI.CanvasScaler;

namespace DarkieCustomTraits.Content
{
    public class DarkieStatusEffectAction
    {
        public static bool titanShifterStatusSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            //Just add titan trait to actor
            if (!pTarget.a.hasTrait("titan"))
                pTarget.a.addTrait("titan");
            return true;
        }

        public static bool titanShifterStatusOnFinish(BaseSimObject pTarget, WorldTile pTile = null)
        {
            //Just remove titan trait to actor
            if (pTarget.a.hasTrait("titan"))
                pTarget.a.removeTrait("titan");
            return true;
        }

        public static bool bleedingStatusSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (Randy.randomChance(0.1f) && pTarget.a.isAlive())
            {
                pTarget.getHit(10, true, AttackType.Weapon, null, true, false);
            }
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
            return true;
        }

        public static bool timeStopperStatusSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive()) return false;
            pTarget.a.cancelAllBeh();
            pTarget.a.makeWait(5f);
            return true;
        }

    }
}
