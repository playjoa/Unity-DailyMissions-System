using System.Linq;
using GarbageCollectSystem.Scriptables;
using MissionSystem.Data;
using MissionSystem.Prizes;
using TranslationSystem.Controller;
using UnityEngine;

namespace MissionSystem.MissionTypes
{
    [CreateAssetMenu(menuName = "Mission - Garbage Rarity Collect", fileName = "New Garbage Rarity Collect Mission")]
    public class GarbageRarityCollectMission : MissionItem
    {
        [Header("Garbage Collect target:")] 
        [SerializeField] private GarbageRarity targetGarbageRarity;
        [SerializeField] private MissionCurrencyPrize currencyPrize;

        // Description will be like: Collect @target@ rare garbages! => Collect 25 rare garbages!
        public override string MissionDescription => Translate.GetText(missionDescriptionKey)
            .Replace("@target@", missionTarget.ToString());

        public override MissionPrize MissionPrize => currencyPrize;
        
        public override bool ValidateMission(params object[] objects)
        {
            if (!objects.Any(obj => obj is GarbageRarity)) return false;

            var garbageRarityCollected = (GarbageRarity)objects.First(obj => obj is GarbageRarity);
            return garbageRarityCollected.Equals(targetGarbageRarity);
        }
    }
}