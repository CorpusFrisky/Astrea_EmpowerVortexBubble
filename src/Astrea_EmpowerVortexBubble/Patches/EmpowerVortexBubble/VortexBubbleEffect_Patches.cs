using Clearings;
using HarmonyLib;
using Mono.Cecil;
using RewiredConsts;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Astrea_EmpowerVortexBubble.Patches.EmpowerVortexBubble
{
    public class VortexBubbleEffect_Patches
    {
        [HarmonyPatch(typeof(VortexBubbleEffect), nameof(VortexBubbleEffect.ActivateEffect))]
        public class VortexBubbleEffect_ActivateEffect
        {
            public static void Prefix(int effectAmount, GameObject effectOwner)
            {
                int empowerAmount = 0;
                if (effectOwner != null)
                {
                    Dictionary<int,int> dict = EmpowerEffect_Patches.ownerInstanceIdToEmpowerAmountDict;
                    int key = effectOwner.GetInstanceID();
                    if (dict.ContainsKey(key))
                    {
                        empowerAmount = dict[key];
                        Debug.Log("***CFLOG*** Found empower amount in VortexBubbleEffect_ActivateEffect: " + empowerAmount);
                    }

                    //PlayerEffects effects = effectOwner.GetComponent<PlayerEffects>();
                    //if (effects != null)
                    //{
                    //    Il2CppSystem.Object playerEffectsListObj = new PlayerEffectsList();
                    //    effects.FieldGetter("PlayerEffectsList", "playerEffectsList", ref playerEffectsListObj);
                    //    if (playerEffectsListObj != null)
                    //    {
                    //        PlayerEffectsList effectsList = playerEffectsListObj.Cast<PlayerEffectsList>();
                    //        //empowerAmount = diceHolder != null ?
                    //        //    diceHolder.GetEmpowerAmount(ScriptableObject.CreateInstance<InteractionTypeEnum>()) :
                    //        //    0;
                    //        Debug.Log("***CFLOG*** Setting empower amount in VortexBubbleEffect_ActivateEffect: " + empowerAmount);
                    //    }
                    //}
                }

                effectAmount = effectAmount + empowerAmount;
                Debug.Log("***CFLOG*** Setting effect amount in VortexBubbleEffect_ActivateEffect: " + effectAmount);
            }
        }
    }
}
