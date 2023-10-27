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
        public static MasteryDieTypeEnum getDieTypeFromDieId(string id)
        {
            var ret = MasteryDieTypeEnum.NORMAL;
            if (id.Contains(Constants.PLUS_PLUS_PLUS_DIE_MASTERY_ID_SUFFIX))
            {
                ret = MasteryDieTypeEnum.PLUS_PLUS_PLUS;
            }
            else if (id.Contains(Constants.PLUS_PLUS_DIE_MASTERY_ID_SUFFIX))
            {
                ret = MasteryDieTypeEnum.PLUS_PLUS;
            }
            else if (id.Contains(Constants.PLUS_DIE_MASTERY_ID_SUFFIX))
            {
                ret = MasteryDieTypeEnum.PLUS;
            }
            else if (id.Contains(Constants.MOONIE_DIE_SUFFIX))
            {
                ret = MasteryDieTypeEnum.MOONIE;
            }
            else if (id.Contains(Constants.CELLARIUS_DIE_SUFFIX))
            {
                ret = MasteryDieTypeEnum.CELLARIUS;
            }
            else if (id.Contains(Constants.HEVELIUS_DIE_SUFFIX))
            {
                ret = MasteryDieTypeEnum.HEVELIUS;
            }
            else if (id.Contains(Constants.SOTHIS_DIE_SUFFIX))
            {
                ret = MasteryDieTypeEnum.SOTHIS;
            }
            else if (id.Contains(Constants.AUSTRA_DIE_SUFFIX))
            {
                ret = MasteryDieTypeEnum.AUSTRA;
            }
            else if (id.Contains(Constants.ORION_DIE_SUFFIX))
            {
                ret = MasteryDieTypeEnum.ORION;
            }

            return ret;
        }
    }
}
