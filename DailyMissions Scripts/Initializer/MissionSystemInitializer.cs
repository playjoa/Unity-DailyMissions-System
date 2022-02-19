using MissionSystem.Components;
using MissionSystem.Controller;
using UnityEngine;

namespace MissionSystem.Initializer
{
    public static class MissionSystemInitializer
    {
        public static void Initiate()
        {
            Debug.Log("...Initiating Missions...");
            MissionPool.Initiate();
            MissionController.Initiate();
            MissionsEventsHandler.Initiate();
            Debug.Log("...Missions Initiated...");
        }
    }
}