using Clearings;
using Clearings.BattleActions;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using LittleLeo.Variables;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Astrea_EmpowerVortexBubble.Patches.CompendiumPlayground
{
    public class LocalizationManager_Patches
    {
        [HarmonyPatch(typeof(CompendiumMenuPlatformSpecific), nameof(CompendiumMenuPlatformSpecific.Start))]
        public class CompendiumMenuPlatformSpecific_Start
        {
            public static void Postfix(CompendiumMenuPlatformSpecific __instance)
            {
                Debug.Log("******CFLOG CompendiumMenuPlatformSpecific post Start()");
                Debug.Log(__instance.ToString());
                Debug.Log(__instance.diceItems.ToString());
                Debug.Log(__instance.diceItems.Count);
            }
        }

        [HarmonyPatch(typeof(CompendiumMenuPlatformSpecific), nameof(CompendiumMenuPlatformSpecific.HandleDiceTab))]
        public class CompendiumMenuPlatformSpecific_HandleDiceTab
        {
            public static void Postfix(CompendiumMenuPlatformSpecific __instance)
            {
                Debug.Log("******CFLOG CompendiumMenuPlatformSpecific post HandleDiceTab()");
                Debug.Log(__instance.ToString());
                Debug.Log(__instance.diceItems.ToString());
                Debug.Log(__instance.diceItems.Count);
            }
        }

        [HarmonyPatch(typeof(DiceCanvas), nameof(DiceCanvas.DiceTabPressed))]
        public class DiceCanvas_DiceTabPressed
        {
            public static void Postfix(DiceCanvas __instance)
            {
                Debug.Log("******CFLOG CompendiumMenuPlatformSDiceCanvaspecific post DiceTabPressed()");
                Debug.Log(__instance.ToString());
                Debug.Log(__instance.currentDiceList.ToString());
                Debug.Log(__instance.currentDiceList.Count);

                Debug.Log(__instance.allPlayerDicesList.dice.ToString());
                Debug.Log(__instance.allPlayerDicesList.dice.Count);


                //foreach (Dice die in AnalyticsManager.Instance.saveSystem.allPlayerDicesList.dice)
                //{
                //    die.enableCustomDiceNameID = true;
                //    die.customDiceNameID = die.DiceName + "_NM";


                //    Debug.Log(die.DiceName);
                //}
                
              

                // FinalVictoryPanel
            }
        }

        [HarmonyPatch(typeof(DicePreview), nameof(DicePreview.Populate))]
        public class DicePreview_Populate
        {
            public static void Prefix(Dice dice, bool diceReward, CompendiumItemState compendiumItemState)
            {
                Debug.Log("*******CFLOG Changing name of " + dice.DiceName);
                //LocalizationManager.addDieID

                dice.enableCustomDiceNameID = true;
                dice.customDiceNameID = dice.name + Constants.MASTERY_ID_SUFFIX;
            }

            //public static void Postfix(Dice dice)
            //{
            //    Debug.Log("*******CFLOG Changing name of " + dice.DiceName);
            //    //LocalizationManager.addDieID

            //    dice.name += " (Unmastered)";
            //}
        }

    }
}
