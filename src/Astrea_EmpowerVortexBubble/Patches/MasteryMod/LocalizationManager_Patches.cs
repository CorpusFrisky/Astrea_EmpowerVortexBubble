using Clearings;
using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using System;
using System.Collections.ObjectModel;

namespace Astrea_EmpowerVortexBubble.Patches.MasteryMod
{
    public class LocalizationManager_Patches
    {
        static bool isDiceIdListLoaded = false;
        static Collection<string> diceIdHashList;


        [HarmonyPatch(typeof(LocalizationManager), nameof(LocalizationManager.GetLocalizedString))]
        public class LocalizationManager_GetLocalizedString
        {
            public static void Prefix(ref string ID, out MasteryModStringState __state)
            {
                if (!isDiceIdListLoaded)
                {
                    loadDiceIdList();
                }

                //UnityEngine.Debug.Log("*******CFLOG LocalizationManager_GetLocalizedString Prefix " + ID);

                MasteryDieTypeEnum dieType = MasteryModStringUtil.getDieTypeFromDieId(ID);
                string baseId = MasteryModStringUtil.getDieBaseIdFromId(ID);

                //UnityEngine.Debug.Log("*******CFLOG getOriginalDieStringFromMarkedName1 " + ID + ": " + baseId);

                if (baseId.Length != ID.Length)
                {
                    __state = new MasteryModStringState()
                    {
                        baseId = baseId,
                        dieType = dieType,
                        shouldPerformMasteryCheck = true
                    };
                    ID = baseId;
                }
                else
                {
                    //UnityEngine.Debug.Log("*******CFLOG getOriginalDieStringFromMarkedName3 " + ID);
                    __state = new MasteryModStringState()
                    {
                        baseId = ID,
                        dieType = dieType,
                        shouldPerformMasteryCheck = false
                    };
                }
            }

            public static string Postfix(string __result, MasteryModStringState __state)
            {
                //UnityEngine.Debug.Log("*******CFLOG Getting Localized String with name of " + __state.baseId + ": " + __result);

                if (__state.shouldPerformMasteryCheck)
                {
                    //UnityEngine.Debug.Log("*******CFLOG checking master on " + __state.baseId);

                    return (diceIdHashList.Contains(__state.baseId) && 
                            !MasteryModSaveUtil.isCardMastered(__state.baseId, __state.dieType))
                        ? __result + " (Unmastered)" 
                        : __result;
                }

                return __result;
            }
        }
        private static void loadDiceIdList()
        {
            List<Dice> diceList = AnalyticsManager.Instance?.saveSystem?.allPlayerDicesList?.dice;
            if (diceList == null || diceList.Count == 0) { return; }

            isDiceIdListLoaded = true;
            diceIdHashList = new Collection<string>();
            foreach (Dice die in diceList)
            {
                //UnityEngine.Debug.Log("*******CFLOG Adding to die id list " + die.GetNameID());

                diceIdHashList.Add(die.GetNameID());
            }
        }
    }
}
