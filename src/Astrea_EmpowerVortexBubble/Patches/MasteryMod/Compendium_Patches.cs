using Clearings;
using HarmonyLib;

namespace Astrea_EmpowerVortexBubble.Patches.MasteryMod
{
    public class Compendium_Patches
    {
        [HarmonyPatch(typeof(CompendiumCanvas), nameof(CompendiumCanvas.OnInitializeGameStatsAndCompendium))]
        public class CompendiumCanvas_OnInitializeGameStatsAndCompendium
        {
            public static void Prefix(CompendiumCanvas __instance)
            {
                MasteryModSaveUtil.Initialize(__instance?.diceCanvas?.allPlayerDicesList?.dice);
            }
        }
    }
}
