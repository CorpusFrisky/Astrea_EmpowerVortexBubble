using Astrea_EmpowerVortexBubble.Patches.MasteryMod;
using Clearings;
using HarmonyLib;
using UnityEngine;

namespace Astrea_EmpowerVortexBubble.Patches.CompendiumPlayground
{
    public class LocalizationManager_Patches
    {
        [HarmonyPatch(typeof(CompendiumCanvas), nameof(CompendiumCanvas.OnInitializeGameStatsAndCompendium))]
        public class CompendiumCanvas_OnInitializeGameStatsAndCompendium
        {
            public static void Prefix(CompendiumCanvas __instance)
            {
                Debug.Log("******CFLOG CompendiumCanvas_OnInitializeGameStatsAndCompendium prefix hit");

                if (__instance?.diceCanvas?.allPlayerDicesList?.dice != null)
                {
                    foreach (var die in __instance?.diceCanvas?.allPlayerDicesList?.dice)
                    {
                        MasteryModStringUtil.markDieNameForMasteryMod(die);
                        Debug.Log("******CFLOG CompendiumCanvas_OnInitializeGameStatsAndCompendium pre die customId " + die.customDiceNameID);
                        //******CFLOG CompendiumCanvas_OnInitializeGameStatsAndCompendium pre die customId DefensiveBoon+Die_CF_MASTERY
                    }
                }
            }
        }
    }
}
