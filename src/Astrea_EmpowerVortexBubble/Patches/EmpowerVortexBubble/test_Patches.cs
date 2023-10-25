﻿using Clearings;
using Clearings.BattleActions;
using HarmonyLib;
using LittleLeo.Variables;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Astrea_EmpowerVortexBubble.Patches.EmpowerVortexBubble
{
    public class test_Patches
    {
        [HarmonyPatch(typeof(PurifyAction), nameof(PurifyAction.GetModifiedPurifyAmount))]
        public class PurifyAction_GetModifiedPurifyAmount
        {
            private static readonly bool testMode = true;
            public static void Prefix(PurifyAction __instance, ref int purifyAmount, bool chanting, bool effectBasedAreaPurify, GameObject source)
            {
                if(testMode)
                {
                    //var allDiceBag = BattleHandler.Instance.allDiceBag;
                    //allDiceBag.GetAllDices().ForEach((Action<Dice>)(dice =>
                    //{
                    //    Debug.Log("CFLOG***** " + dice.GetNameID());
                    //}));

                    ScriptableObject.CreateInstance<VortexBubbleEffect>().ActivateEffect(2, source);
                    purifyAmount = 20;
                }
            }
        }
    }
}
