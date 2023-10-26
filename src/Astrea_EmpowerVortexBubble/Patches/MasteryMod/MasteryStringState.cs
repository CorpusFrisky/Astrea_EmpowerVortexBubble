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
