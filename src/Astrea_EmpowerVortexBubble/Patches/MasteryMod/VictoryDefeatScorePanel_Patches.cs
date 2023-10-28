using Clearings;
using HarmonyLib;
using UnityEngine;

namespace Astrea_EmpowerVortexBubble.Patches.MasteryMod
{
    public class VictoryDefeatScorePanel_Patches
    {
        //VictoryDefeatScorePanel:EndOfTheRunSaving
        [HarmonyPatch(typeof(VictoryDefeatScorePanel), nameof(VictoryDefeatScorePanel.EndOfTheRunSaving))]
        public class VictoryDefeatScorePanel_EndOfTheRunSaving
        {
            public static void Prefix(VictoryDefeatScorePanel __instance)
            {
                Debug.Log("*******CFLOG VictoryDefeatScorePanel_EndOfTheRunSaving Prefix");

                MasteryModSaveUtil.UpdateMasteryFile(BattleHandler.Instance.diceBag);

              
            }

            //public static string Postfix(string __result)
            //{
            //    return __result;
            //}
        }
    }
}
