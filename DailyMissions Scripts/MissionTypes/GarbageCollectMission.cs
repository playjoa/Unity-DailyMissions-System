using System.Linq;
using GarbageCollectSystem.Components;
using GarbageCollectSystem.Scriptables;
using MissionSystem.Data;
using MissionSystem.Prizes;
using TranslationSystem.Controller;
using UnityEngine;

namespace MissionSystem.MissionTypes
{
    [CreateAssetMenu(menuName = "Mission - Garbage Collect", fileName = "New Garbage Collect Mission")]
    public class GarbageCollectMission : MissionItem
    {
        [Header("Garbage Collect target:")]
        [SerializeField] private GarbageStats targetGarbage;
        [SerializeField] private MissionCurrencyPrize currencyPrize;

        private string GarbageKey => targetGarbage.NameKey;
        
        // Description will be like: Collect @target@ of @garbage@ => Collect 150 of hamburgers
        public override string MissionDescription => Translate.GetText(missionDescriptionKey)
            .Replace("@target@", missionTarget.ToString()).Replace("@garbage@", targetGarbage.NameLocalized);

        public override MissionPrize MissionPrize => currencyPrize;
        
        public override bool ValidateMission(params object[] objects)
        {
            if (!objects.Any(obj => obj is Garbage)) return false;
            
            var garbageCollected = (Garbage) objects.First(obj => obj is Garbage);
            return garbageCollected.GarbageStats.NameKey.Equals(GarbageKey);
        }
    }
}