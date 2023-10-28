using Clearings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Astrea_EmpowerVortexBubble.Patches.MasteryMod
{
    internal class MasteryModSaveUtil
    {
        static public Dictionary<string, int> diceBaseNameHashToMasteredBitMapDictionary = new Dictionary<string, int>();

        static public bool isCardMastered(string baseName, MasteryDieTypeEnum dieType)
        {
            //Debug.Log("******CFLOG isCardMastered() called with " + baseName);

            return diceBaseNameHashToMasteredBitMapDictionary.ContainsKey(baseName) &&
                (diceBaseNameHashToMasteredBitMapDictionary[baseName] & (1 << (int)dieType)) != 0;
        }

        internal static void Initialize(Il2CppSystem.Collections.Generic.List<Dice> dice)
        {
            if (dice != null)
            {                                                                               
                foreach (var die in dice)                                                                                                               
                {
                    MasteryModStringUtil.markDieNameForMasteryMod(die);
                }
            }

            string masterSaveFilePath = MasteryModStringUtil.getMasterySaveFilePath();

            if (File.Exists(masterSaveFilePath))
            {
                var masterySaveObjectString = File.ReadAllText(masterSaveFilePath);

                var hashAndBitmapPairs = masterySaveObjectString.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var hashAndBitmapPair in hashAndBitmapPairs)
                {
                    var splitPair = hashAndBitmapPair.Split(':');

                    diceBaseNameHashToMasteredBitMapDictionary[splitPair[0]] = int.Parse(splitPair[1]);
                }
            }
        }

        internal static void UpdateMasteryFile(PlayerDiceInventoryData diceBag)
        {
            foreach (Dice die in diceBag.GetAllDices())
            {
                Debug.Log("*******CFLOG UpdateMasteryFile die present: " + die.GetNameID());

                var baseId = MasteryModStringUtil.getDieBaseIdFromId(die.GetNameID());
                var dieType = MasteryModStringUtil.getDieTypeFromDieId(die.GetNameID());
                var currentBitmapForDieBaseId = diceBaseNameHashToMasteredBitMapDictionary.ContainsKey(baseId) ?
                    diceBaseNameHashToMasteredBitMapDictionary[baseId] : 0;

                diceBaseNameHashToMasteredBitMapDictionary[baseId] = currentBitmapForDieBaseId | (1 << (int)dieType);
            }

            StringBuilder sb = new StringBuilder();
            foreach(var key in diceBaseNameHashToMasteredBitMapDictionary.Keys)
            {
                sb.Append(key + ":" + diceBaseNameHashToMasteredBitMapDictionary[key] + ",");
            }
            // Remove last comma
            sb.Remove(sb.Length - 1, 1);

            string masterSaveFilePath = MasteryModStringUtil.getMasterySaveFilePath();
            File.WriteAllText(masterSaveFilePath, sb.ToString());
        }
    }
}
