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
        public static Dictionary<int, EmpowerEffect> ownerInstanceIdToEmpowerEffectDict = new Dictionary<int, EmpowerEffect>();

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
                if (ownerInstanceIdToEmpowerEffectDict.ContainsKey(key))
                {
                    Debug.Log("***CFLOG*** [EmpowerEffect_RemoveEffect] Removing effect from: " + key);
                    ownerInstanceIdToEmpowerEffectDict.Remove(key);
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
                if (!ownerInstanceIdToEmpowerEffectDict.ContainsKey(key))
                {
                    Debug.Log("***CFLOG*** [EmpowerEffect_Initialize] Adding key " + key + " with amount : " + effectAmount);
                    ownerInstanceIdToEmpowerEffectDict.Add(key, __instance);
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
                if (ownerInstanceIdToEmpowerEffectDict.ContainsKey(key))
                {
                    Debug.Log("***CFLOG*** [EmpowerEffect_OnRemoveEffectEndOfBattle] Removing effect from: " + key);
                    ownerInstanceIdToEmpowerEffectDict.Remove(key);
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
