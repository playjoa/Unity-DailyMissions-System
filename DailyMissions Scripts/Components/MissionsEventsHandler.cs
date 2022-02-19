using GarbageCollectSystem.Components;
using GarbageCollectSystem.Controller;
using MissionSystem.MissionTypes;

namespace MissionSystem.Components
{
    public static class MissionsEventsHandler
    {
        public static void Initiate() => SubscribeToGameEvents();

        private static void SubscribeToGameEvents()
        {
            GarbageCollectController.OnGarbageCollect += HandleGarbageCollectMissions;
        }

        private static void HandleGarbageCollectMissions(Garbage garbageCollected)
        {
            MissionValidator.MissionValidation<GarbageCollectMission>(objectsToValidate: garbageCollected);
            MissionValidator.MissionValidation<GarbageRarityCollectMission>(objectsToValidate: garbageCollected.GarbageStats.Rarity);
            MissionValidator.MissionValidation<GarbageLocationCollectMission>(objectsToValidate: garbageCollected.Location);
        }
    }
}