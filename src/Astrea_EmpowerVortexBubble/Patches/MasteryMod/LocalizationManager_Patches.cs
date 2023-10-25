using Clearings;
using Clearings.BattleActions;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.Threading;
using LittleLeo.Variables;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

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
                if(!isDiceIdListLoaded)
                {
                    loadDiceIdList();
                }

                if(ID.EndsWith(Constants.MASTERY_ID_SUFFIX + "_N"))
                {
                    // Length of suffix plus 2 for _N plus 3 for _DIE
                    string baseId = ID.Substring(0, ID.Length - (Constants.MASTERY_ID_SUFFIX.Length + 5)) +"_N";
                    UnityEngine.Debug.Log("*******CFLOG Setting State " + ID + ": " + baseId);

                    __state = new MasteryStringState(baseId, true);
                    ID = baseId;
                }
                else 
                {
                    __state = new MasteryStringState(ID, false);
                }
            }

            public static string Postfix(string __result, MasteryStringState __state)
            {
                UnityEngine.Debug.Log("*******CFLOG Getting Localized String with name of " + __state.baseId + ": " + __result);

                if(__state.shouldPerformMasteryCheck)
                {
                    return (diceIdHashList.Contains(__state.baseId.GetHashCode())) ? __result + " (Unmastered)" : __result;
                }

                return __result;
            }
        }
        private static void loadDiceIdList() 
        {
            List<Dice> diceList = AnalyticsManager.Instance?.saveSystem?.allPlayerDicesList?.dice;
            if(diceList == null || diceList.Count == 0) { return; }

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
