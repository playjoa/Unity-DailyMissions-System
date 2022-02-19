using System.Linq;
using GarbageCollectSystem.Data;
using MissionSystem.Data;
using MissionSystem.Prizes;
using TranslationSystem.Controller;
using UnityEngine;

namespace MissionSystem.MissionTypes
{
    [CreateAssetMenu(menuName = "Mission - Garbage Location Collect",
        fileName = "New Garbage Location Collect Mission")]
    public class GarbageLocationCollectMission : MissionItem
    {
        [Header("Garbage Collect target:")] 
        [SerializeField] private Location targetGarbageCollectLocation;
        [SerializeField] private MissionCurrencyPrize currencyPrize;

        // Description will be like: Collect @target@ garbages at @location@ => Collect 15 garbages at beach
        public override string MissionDescription => Translate.GetText(missionDescriptionKey)
            .Replace("@target@", missionTarget.ToString())
            .Replace("@location@", Translate.GetText(targetGarbageCollectLocation.ToString()));

        public override MissionPrize MissionPrize => currencyPrize;

        public override bool ValidateMission(params object[] objects)
        {
            if (!objects.Any(obj => obj is Location)) return false;

            var garbageLocationCollected = (Location)objects.First(obj => obj is Location);
            return garbageLocationCollected.Equals(targetGarbageCollectLocation);
        }
    }
}