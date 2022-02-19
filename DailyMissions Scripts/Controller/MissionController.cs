using System.Collections.Generic;
using System.Linq;
using GameData.Controller;
using MissionSystem.Components;
using MissionSystem.Data;

namespace MissionSystem.Controller
{
    public static class MissionController
    {
        public const int MaxMissions = 3;

        public static List<MissionHolder> CurrentMissions { get; private set; } = new List<MissionHolder>();
        public static int CurrentMissionsCount => CurrentMissions.Count;
        public static bool HasNewMissions { get; private set; }
        public static bool CanCollectAny => CurrentMissions.Any(mission => mission.Complete && !mission.Collected);
        public static bool AtMaxMission => CurrentMissions.Count < MaxMissions;

        public static bool CanRequestMissions => CurrentMissions.All(mission => mission.Collected) || !CurrentMissions.Any();

        public delegate void DailyMissionDataEvent();
        public delegate void DailyMissionEvent(MissionHolder missionData);
        
        public static event DailyMissionEvent OnMissionCollect;
        public static event DailyMissionEvent OnMissionComplete;
        public static event DailyMissionDataEvent OnMissionDataChange;

        public static void Initiate()
        {
            ConstructMissionHolders();

            if (LogInController.IsNewDailyLogIn)
                GetNewMissions();
        }

        private static void ConstructMissionHolders()
        {
            CurrentMissions = new List<MissionHolder>();
            var playerMissionData = PlayerDataController.PlayerData.dailyMissions;
            
            foreach (var missionData in playerMissionData)
            {
                var missionItem = MissionPool.SelectMissionItem(missionData);
                var freshMissionData = new MissionData(missionData.Id, missionItem.MissionTarget, missionData.Count);
                var missionHolder = new MissionHolder(missionItem, freshMissionData);
                
                CurrentMissions.Add(missionHolder);
            }
        }

        public static void CountMission(MissionHolder missionHolder, int countAmount = 1)
        {
            if (missionHolder.Complete) return;

            missionHolder.CountMission(countAmount);
            DataChangeEvent();

            if (missionHolder.Complete)
                OnMissionComplete?.Invoke(missionHolder);
        }

        public static void ValidateMissionCollect(MissionHolder missionHolder)
        {
            if (!missionHolder.Collected) return;

            if (CurrentMissions.Contains(missionHolder))
                CurrentMissions.Remove(missionHolder);

            OnMissionCollect?.Invoke(missionHolder);
            DataChangeEvent();
        }

        public static bool GetNewMissions()
        {
            if (CurrentMissions.Count >= MaxMissions) return false;

            for (var id = CurrentMissions.Count; id < MaxMissions; id++)
            {
                if (MissionPool.NoMoreMissionsAvailable) break;

                var newMissionItem = MissionPool.RequestNewUniqueMission();
                var newMissionId = newMissionItem.MissionId;
                var newMissionTarget = newMissionItem.MissionTarget;

                CurrentMissions.Add(new MissionHolder(newMissionItem, new MissionData(newMissionId, newMissionTarget)));
                HasNewMissions = true;
            }

            DataChangeEvent();
            return true;
        }

        public static bool HasMissionOfType<TMissionType>() =>
            CurrentMissions.Any(mission => mission.MissionItem is TMissionType);

        public static IEnumerable<MissionHolder> MissionsOfType<TMissionType>() =>
            CurrentMissions.Where(mission => mission.MissionItem is TMissionType).ToList();

        private static void DataChangeEvent() => OnMissionDataChange?.Invoke();
    }
}