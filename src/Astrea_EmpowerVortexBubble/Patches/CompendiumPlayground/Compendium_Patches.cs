using Astrea_EmpowerVortexBubble.Patches.MasteryMod;
using Clearings;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;

namespace Astrea_EmpowerVortexBubble.Patches.CompendiumPlayground
{
    public class Compendium_Patches
    {
        static public Dictionary<string, int> diceBaseNameHashToMasteredBitMapDictionary = new Dictionary<string, int>();

        static public bool isCardMastered(string baseName, MasteryDieTypeEnum dieType)
        {
            Debug.Log("******CFLOG isCardMastered() called with " + baseName);

            return diceBaseNameHashToMasteredBitMapDictionary.ContainsKey(baseName) &&
                (diceBaseNameHashToMasteredBitMapDictionary[baseName] & (1 << (int)dieType)) != 0;  
        }

        [HarmonyPatch(typeof(CompendiumCanvas), nameof(CompendiumCanvas.OnInitializeGameStatsAndCompendium))]
        public class CompendiumCanvas_OnInitializeGameStatsAndCompendium
        {
            public static void Prefix(CompendiumCanvas __instance)
            {
                if (__instance?.diceCanvas?.allPlayerDicesList?.dice != null)
                {
                    foreach (var die in __instance?.diceCanvas?.allPlayerDicesList?.dice)
                    {
                        MasteryModStringUtil.markDieNameForMasteryMod(die);
                    }
                }

                string LocalLowPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");
                string masterSaveFilePath = LocalLowPath + Constants.MASTERY_SAVE_FILE_PATH;

                if (File.Exists(masterSaveFilePath))
                {
                    var masterySaveObjectString = File.ReadAllText(masterSaveFilePath);

                    var hashAndBitmapPairs = masterySaveObjectString.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach(var hashAndBitmapPair in hashAndBitmapPairs)
                    {
                        var splitPair = hashAndBitmapPair.Split(':');

                        diceBaseNameHashToMasteredBitMapDictionary.Add(splitPair[0], int.Parse(splitPair[1]));
                    }
                }
            }
        }
    }
}
