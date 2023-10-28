using Clearings;
using System;

namespace Astrea_EmpowerVortexBubble.Patches.MasteryMod
{
    internal class MasteryModStringUtil
    {
        public static string getMasterySaveFilePath()
        {
            string LocalLowPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");
            string masterSaveFilePath = LocalLowPath + Constants.MASTERY_SAVE_FILE_PATH;
            return masterSaveFilePath;
        }

        public static void markDieNameForMasteryMod(Dice die)
        {
            die.enableCustomDiceNameID = true;
            die.customDiceNameID = die.name + Constants.MASTERY_ID_SUFFIX;
        }

        internal static string getDieBaseIdFromId(string id)
        {
            string baseId = id.Replace(Constants.PLUS_PLUS_PLUS_DIE_MASTERY_ID_SUFFIX, "")
                    .Replace(Constants.PLUS_PLUS_DIE_MASTERY_ID_SUFFIX, "")
                    .Replace(Constants.PLUS_DIE_MASTERY_ID_SUFFIX, "")
                    .Replace(Constants.DIE_MASTERY_ID_SUFFIX, "");

            //UnityEngine.Debug.Log("*******CFLOG getOriginalDieStringFromMarkedName1 " + ID + ": " + baseId);

            if (baseId.Length != id.Length)
            {
                // If we are dealing with the manipulation of dice names, we need to be sure to remove any 
                baseId = baseId.Replace(Constants.MOONIE_SUFFIX, "")
                .Replace(Constants.CELLARIUS_SUFFIX, "")
                .Replace(Constants.HEVELIUS_SUFFIX, "")
                .Replace(Constants.SOTHIS_SUFFIX, "")
                .Replace(Constants.AUSTRA_SUFFIX, "")
                .Replace(Constants.ORION_SUFFIX, "");
            }

            return baseId;
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
