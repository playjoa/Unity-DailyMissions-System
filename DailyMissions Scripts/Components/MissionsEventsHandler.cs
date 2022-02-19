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
            //Subscribe To Your GameEvents Here
            MyGameManager.OnSomething += HandleOnSomethingEvent;
        }

        private static void HandleOnSomethingEvent(Something something)
        {
            MissionValidator.MissionValidation<SomethingMission>(objectsToValidate: something);
        }
    }
}