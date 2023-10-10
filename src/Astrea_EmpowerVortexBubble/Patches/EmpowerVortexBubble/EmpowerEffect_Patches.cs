using Clearings;
using HarmonyLib;
using Mono.Cecil;
using RewiredConsts;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Astrea_EmpowerVortexBubble.Patches.EmpowerVortexBubble
{
    public class EmpowerEffect_Patches
    {
        public static Dictionary<int, int> ownerInstanceIdToEmpowerAmountDict = new Dictionary<int, int>();

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(Effect), nameof(Effect.EffectIncreased))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static string EmpowerEffect_EffectIncreased_BaseMethodDummy(EmpowerEffect instance) { return null; }

        [HarmonyPatch(typeof(EmpowerEffect), nameof(EmpowerEffect.EffectIncreased))]
        public class EmpowerEffect_EffectIncreased
        {
            public static bool Prefix(EmpowerEffect __instance, int effectAmount, GameObject effectOwner)
            {
                int key = effectOwner.GetInstanceID();
                if(!ownerInstanceIdToEmpowerAmountDict.ContainsKey(key))
                { 
                    Debug.Log("***CFLOG*** [EmpowerEffect_EffectIncreased] Adding key " + key + " with amount : " + effectAmount);
                    ownerInstanceIdToEmpowerAmountDict.Add(key, effectAmount);
                }
                else
                {
                    Debug.Log("***CFLOG*** [EmpowerEffect_EffectIncreased] Increasing key " + key + " with amount : " + effectAmount);
                    ownerInstanceIdToEmpowerAmountDict[key] += effectAmount;
                    Debug.Log("***CFLOG*** [EmpowerEffect_EffectIncreased] New amount for key " + key + ": " + ownerInstanceIdToEmpowerAmountDict[key]);
                }

                // Avoid infinite loop due to base method getting called
                EmpowerEffect_EffectIncreased_BaseMethodDummy(__instance);
                return false;
            }
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(Effect), nameof(Effect.EffectDecreased))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static string EmpowerEffect_EffectDecreased_BaseMethodDummy(EmpowerEffect instance) { return null; }

        [HarmonyPatch(typeof(EmpowerEffect), nameof(EmpowerEffect.EffectDecreased))]
        public class EmpowerEffect_EffectDecreased
        {
            public static bool Prefix(EmpowerEffect __instance, int effectAmount, GameObject effectOwner)
            {
                int key = effectOwner.GetInstanceID();
                if (!ownerInstanceIdToEmpowerAmountDict.ContainsKey(key))
                {
                    Debug.Log("***CFLOG*** [EmpowerEffect_EffectDecreased] Adding key " + key + " with amount : " + effectAmount);
                    ownerInstanceIdToEmpowerAmountDict.Add(key, effectAmount);
                }
                else
                {
                    Debug.Log("***CFLOG*** [EmpowerEffect_EffectDecreased] Decreasing key " + key + " with amount : " + effectAmount);
                    ownerInstanceIdToEmpowerAmountDict[key] -= effectAmount;
                    Debug.Log("***CFLOG*** [EmpowerEffect_EffectDecreased] New amount for key " + key + ": " + ownerInstanceIdToEmpowerAmountDict[key]);
                }

                // Avoid infinite loop due to base method getting called
                EmpowerEffect_EffectDecreased_BaseMethodDummy(__instance);
                return false;
            }
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(Effect), nameof(Effect.RemoveEffect))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static string EmpowerEffect_RemoveEffect_BaseMethodDummy(EmpowerEffect instance) { return null; }

        [HarmonyPatch(typeof(EmpowerEffect), nameof(EmpowerEffect.RemoveEffect))]
        public class EmpowerEffect_RemoveEffect
        {
            public static bool Prefix(EmpowerEffect __instance, int effectAmount, GameObject effectOwner)
            {
                int key = effectOwner.GetInstanceID();
                if (ownerInstanceIdToEmpowerAmountDict.ContainsKey(key))
                {
                    Debug.Log("***CFLOG*** [EmpowerEffect_RemoveEffect] Removing effect from: " + key);
                    ownerInstanceIdToEmpowerAmountDict.Remove(key);
                }
                else
                {
                    Debug.Log("***CFLOG*** [EmpowerEffect_RemoveEffect] REMOVING EFFECT FROM UNREGISTERED KEY: " + key);
                }

                // Avoid infinite loop due to base method getting called
                EmpowerEffect_RemoveEffect_BaseMethodDummy(__instance);
                return false;
            }
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(Effect), nameof(Effect.Initialize))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static string EmpowerEffect_Initialize_BaseMethodDummy(EmpowerEffect instance) { return null; }

        [HarmonyPatch(typeof(EmpowerEffect), nameof(EmpowerEffect.Initialize))]
        public class EmpowerEffect_Initialize
        {
            public static bool Prefix(EmpowerEffect __instance, int effectAmount, GameObject effectOwner)
            {
                int key = effectOwner.GetInstanceID();
                if (!ownerInstanceIdToEmpowerAmountDict.ContainsKey(key))
                {
                    Debug.Log("***CFLOG*** [EmpowerEffect_Initialize] Adding key " + key + " with amount : " + effectAmount);
                    ownerInstanceIdToEmpowerAmountDict.Add(key, effectAmount);
                }
                else
                {
                    Debug.Log("***CFLOG*** [EmpowerEffect_Initialize] Re-Initializing key " + key + " with amount : " + effectAmount);
                    ownerInstanceIdToEmpowerAmountDict[key] = effectAmount;
                    Debug.Log("***CFLOG*** [EmpowerEffect_Initialize] Amount for key " + key + ": " + ownerInstanceIdToEmpowerAmountDict[key]);
                }

                // Avoid infinite loop due to base method getting called
                EmpowerEffect_Initialize_BaseMethodDummy(__instance);
                return false;
            }
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(Effect), nameof(Effect.OnRemoveEffectEndOfBattle))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static string EmpowerEffect_OnRemoveEffectEndOfBattle_BaseMethodDummy(EmpowerEffect instance) { return null; }

        [HarmonyPatch(typeof(EmpowerEffect), nameof(EmpowerEffect.OnRemoveEffectEndOfBattle))]
        public class EmpowerEffect_OnRemoveEffectEndOfBattle
        {
            public static bool Prefix(EmpowerEffect __instance, int effectAmount, GameObject effectOwner)
            {
                int key = effectOwner.GetInstanceID();
                if (ownerInstanceIdToEmpowerAmountDict.ContainsKey(key))
                {
                    Debug.Log("***CFLOG*** [EmpowerEffect_OnRemoveEffectEndOfBattle] Removing effect from: " + key);
                    ownerInstanceIdToEmpowerAmountDict.Remove(key);
                }
                else
                {
                    Debug.Log("***CFLOG*** [EmpowerEffect_OnRemoveEffectEndOfBattle] REMOVING EFFECT FROM UNREGISTERED KEY: " + key);
                }

                // Avoid infinite loop due to base method getting called
                EmpowerEffect_OnRemoveEffectEndOfBattle_BaseMethodDummy(__instance);
                return false;
            }
        }
    }
}
