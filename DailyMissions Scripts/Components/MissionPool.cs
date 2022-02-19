using System.Collections.Generic;
using System.Linq;
using MissionSystem.Controller;
using MissionSystem.Data;
using UnityEngine;
using Utils.Tools;

namespace MissionSystem.Components
{
    public static class MissionPool
    {
        public static int MissionsAvailable => _missionItemsPool.Count - MissionController.CurrentMissionsCount;
        public static bool NoMoreMissionsAvailable => MissionsAvailable <= 0;
        
        private static MissionItem[] _availableMissions;
        private static Dictionary<string, MissionItem> _missionItemsPool;
        
        private const string MissionsFolderInResources = "DailyMissions";
        
        public static void Initiate()
        {
            _availableMissions = Resources.LoadAll<MissionItem>(MissionsFolderInResources);
            BuildMissionDictionary();
        }

        private static void BuildMissionDictionary()
        {
            _missionItemsPool = new Dictionary<string, MissionItem>();

            foreach (var missionItem in _availableMissions)
                _missionItemsPool.Add(missionItem.MissionId, missionItem);
        }

        public static MissionItem SelectMissionItem(MissionData missionData)
        {
            if (_missionItemsPool.TryGetValue(missionData.Id, out var mission))
                return mission;
            
            Debug.LogError($"No mission found with {missionData.Id}");
            return null;
        }

        public static MissionItem RequestNewUniqueMission()
        {
            if (NoMoreMissionsAvailable) return null;
            
            var currentMissions = MissionController.CurrentMissions;
            var randomMission = _missionItemsPool.RandomValue();
            
            while (currentMissions.Any(mission => mission.Id.Equals(randomMission.MissionId)))
                randomMission = _missionItemsPool.RandomValue();

            return randomMission;
        }
    }
}