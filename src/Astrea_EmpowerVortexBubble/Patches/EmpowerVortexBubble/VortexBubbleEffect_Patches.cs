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
                var empowerAmount = 0;
                  
                if (effectOwner != null)
                {
                    Dictionary<int,EmpowerEffect> dict = EmpowerEffect_Patches.ownerInstanceIdToEmpowerEffectDict;
                    
                    var key = effectOwner.GetInstanceID();
                    if (dict.ContainsKey(key))
                    {
                        EmpowerEffect empowerEffect = dict[key];
                        var playerEffects = BattleHandler.Instance.PlayerGameObject.GetComponent<PlayerEffects>();
                        empowerAmount = playerEffects.IsEffectActiveGetAmount(empowerEffect);
                        Debug.Log("***CFLOG*** Found empower amount in VortexBubbleEffect_ActivateEffect: " + empowerAmount);
                    }
                    else 
                    {
                        Debug.Log("***CFLOG*** Failed to find empower amount in VortexBubbleEffect_ActivateEffect");
                    }
                }

                effectAmount = effectAmount + empowerAmount;
                Debug.Log("***CFLOG*** Setting effect amount in VortexBubbleEffect_ActivateEffect: " + effectAmount);
            }
        }
    }
}
