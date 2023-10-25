using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astrea_EmpowerVortexBubble.Patches.MasteryMod
{
    public class MasteryStringState
    {
        public string baseId;
        public bool shouldPerformMasteryCheck;

        public MasteryStringState(string baseId, bool shouldPerformMasteryCheck)
        {
            this.baseId = baseId;
            this.shouldPerformMasteryCheck = shouldPerformMasteryCheck;
        }
    }
}
