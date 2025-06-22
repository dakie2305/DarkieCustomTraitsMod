using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkieCustomTraits.Content
{
    public class DarkieStatusEffectAction
    {
        public static bool titanShifterStatusSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            //Make grow in size
            pTarget.stats.set(CustomBaseStatsConstant.Scale, 0.3f);
            return true;
        }

    }
}
