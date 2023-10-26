using Clearings;

namespace Astrea_EmpowerVortexBubble.Patches.MasteryMod
{
    internal class MasteryModStringUtil
    {
        public static void markDieNameForMasteryMod(Dice die)
        {
            die.enableCustomDiceNameID = true;
            die.customDiceNameID = die.name + Constants.MASTERY_ID_SUFFIX;
        }

        public static string getOriginalDieStringFromMarkedName(string markedName, string extraSuffix, int extraLength)
        {
            return markedName.Substring(0, markedName.Length - (Constants.MASTERY_ID_SUFFIX.Length + extraLength)) + extraSuffix;
        }
    }
}
