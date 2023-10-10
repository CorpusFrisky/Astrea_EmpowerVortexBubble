using Clearings;
using Clearings.BattleActions;
using HarmonyLib;
using UnityEngine;

namespace Astrea_EmpowerVortexBubble.Patches.EmpowerVortexBubble
{
    public class test_Patches
    {
        [HarmonyPatch(typeof(PurifyAction), nameof(PurifyAction.GetModifiedPurifyAmount))]
        public class PurifyAction_GetModifiedPurifyAmount
        {
            public static void Prefix(ref int purifyAmount, bool chanting, bool effectBasedAreaPurify, GameObject source)
            {
                //if (source != null)
                //{
                //    //PlayerEffects effects = source.GetComponent<PlayerEffects>();
                //    //Il2CppSystem.Object diceHolderObj = new PlayerDiceHolder();
                //    //effects.FieldGetter("PlayerDiceHolder", "playerDiceHolder", ref diceHolderObj);
                //    //PlayerDiceHolder diceHolder = diceHolderObj.Cast<PlayerDiceHolder>();
                //    //Debug.Log("DICEHOLDER CHECK!!!!");
                //    //Debug.Log(diceHolder != null ?
                //    //    diceHolder.GetEmpowerAmount(ScriptableObject.CreateInstance<InteractionTypeEnum>()) :
                //    //    "NOT FOUND!!!");
                //    PlayerEffects effects = source.GetComponent<PlayerEffects>();
                //    if (effects != null)
                //    {
                //        Il2CppSystem.Object playerEffectsListObj = new PlayerEffectsList();
                //        effects.FieldGetter("PlayerEffectsList", "playerEffectsList", ref playerEffectsListObj);
                //        if (playerEffectsListObj != null)
                //        {
                //            PlayerEffectsList effectsList = playerEffectsListObj.Cast<PlayerEffectsList>();
                //            //empowerAmount = diceHolder != null ?
                //            //    diceHolder.GetEmpowerAmount(ScriptableObject.CreateInstance<InteractionTypeEnum>()) :
                //            //    0;
                //            Debug.Log("***CFLOG*** Setting empower amount in VortexBubbleEffect_ActivateEffect: " + empowerAmount);
                //        }
                //    }
                //}

                ScriptableObject.CreateInstance<VortexBubbleEffect>().ActivateEffect(2, source);

                purifyAmount = 33;
            }
        }
    }
}
