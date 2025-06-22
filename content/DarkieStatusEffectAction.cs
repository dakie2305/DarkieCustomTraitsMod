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
            //pTarget.a.actor_scale = 0.5f;
            //pTarget.a.target_scale = 0.5f;
            //pTarget.a.setActorScale(0.5f);
            //pTarget.a.updateChangeScale(0.5f);
            return true;
        }

        public static bool titanShifterStatusOnFinish(BaseSimObject pTarget, WorldTile pTile = null)
        {
            //Just remove titan trait to actor
            if (pTarget.a.hasTrait("titan"))
                pTarget.a.removeTrait("titan");
            return true;
        }

    }
}
