using Clearings;
using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using System.Collections.ObjectModel;

namespace Astrea_EmpowerVortexBubble.Patches.MasteryMod
{
    public class LocalizationManager_Patches
    {
        static bool isDiceIdListLoaded = false;
        static Collection<int> diceIdHashList;


        [HarmonyPatch(typeof(LocalizationManager), nameof(LocalizationManager.GetLocalizedString))]
        public class LocalizationManager_GetLocalizedString
        {
            public static void Prefix(ref string ID, out MasteryStringState __state)
            {
                if (!isDiceIdListLoaded)
                {
                    loadDiceIdList();
                }

                UnityEngine.Debug.Log("*******CFLOG LocalizationManager_GetLocalizedString Prefix " + ID);

                string baseId = ID.Replace(Constants.PLUS_PLUS_DIE_MASTERY_ID_SUFFIX, "")
                    .Replace(Constants.PLUS_DIE_MASTERY_ID_SUFFIX, "")
                    .Replace(Constants.DIE_MASTERY_ID_SUFFIX, "");

                UnityEngine.Debug.Log("*******CFLOG getOriginalDieStringFromMarkedName1 " + ID + ": " + baseId);

                if (baseId.Length != ID.Length)
                {
                    // If we are dealing with the manipulation of dice names, we need to be sure to remove any 
                    baseId = baseId.Replace(Constants.MOONIE_SUFFIX, "")
                    .Replace(Constants.CELLARIUS_SUFFIX, "")
                    .Replace(Constants.HEVELIUS_SUFFIX, "")
                    .Replace(Constants.SOTHIS_SUFFIX, "")
                    .Replace(Constants.AUSTRA_SUFFIX, "")
                    .Replace(Constants.ORION_SUFFIX, "");

                    UnityEngine.Debug.Log("*******CFLOG getOriginalDieStringFromMarkedName2 " + ID + ": " + baseId);

                    __state = new MasteryStringState(baseId, true);
                    ID = baseId;
                }
                else
                {
                    UnityEngine.Debug.Log("*******CFLOG getOriginalDieStringFromMarkedName3 " + ID);
                    __state = new MasteryStringState(ID, false);
                }
            }

            public static string Postfix(string __result, MasteryStringState __state)
            {
                //UnityEngine.Debug.Log("*******CFLOG Getting Localized String with name of " + __state.baseId + ": " + __result);

                if (__state.shouldPerformMasteryCheck)
                {
                    return (diceIdHashList.Contains(__state.baseId.GetHashCode())) ? __result + " (Unmastered)" : __result;
                }

                return __result;
            }
        }
        private static void loadDiceIdList()
        {
            List<Dice> diceList = AnalyticsManager.Instance?.saveSystem?.allPlayerDicesList?.dice;
            if (diceList == null || diceList.Count == 0) { return; }

            isDiceIdListLoaded = true;
            diceIdHashList = new Collection<int>();
            foreach (Dice die in diceList)
            {
                UnityEngine.Debug.Log("*******CFLOG Adding to die id hash list " + die.GetNameID());

                diceIdHashList.Add(die.GetNameID().GetHashCode());
            }
        }
    }
}
