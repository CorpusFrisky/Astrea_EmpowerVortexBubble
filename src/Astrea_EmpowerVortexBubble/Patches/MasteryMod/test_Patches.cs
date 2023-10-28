using Clearings;
using Clearings.BattleActions;
using HarmonyLib;
using LittleLeo.Variables;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization;
using UnityEngine.Localization.Metadata;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Utilities;

namespace Astrea_EmpowerVortexBubble.Patches.MasteryMod
{
    public class test_Patches
    {
        [HarmonyPatch(typeof(PurifyAction), nameof(PurifyAction.GetModifiedPurifyAmount))]
        public class PurifyAction_GetModifiedPurifyAmount
        {
            private static readonly bool testMode = false;
            public static void Prefix(PurifyAction __instance, ref int purifyAmount, bool chanting, bool effectBasedAreaPurify, GameObject source)
            {
                if (testMode)
                {
                    foreach (Dice die in AnalyticsManager.Instance.saveSystem.allPlayerDicesList.dice)
                    {
                        Debug.Log(die.DiceName);
                    }

                    //LocalizedStringTable st = LocalizationManager.Instance.battleTable;
                    //.Database.GetAllTables().Result;
                    //Debug.Log("ST collection name: " + result.First<StringTable>().TableCollectionName);

                    purifyAmount = 1000;
                }
            }
        }
    }
}
