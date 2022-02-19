using MissionSystem.Controller;

namespace MissionSystem.Components
{
    public static class MissionValidator
    {
        public static void MissionValidation<TMissionType>(int countAmount = 1, params object[] objectsToValidate)
        {
            if (!MissionController.HasMissionOfType<TMissionType>()) return;
            
            var missions = MissionController.MissionsOfType<TMissionType>();
            foreach (var mission in missions)
            {
                if (mission.MissionItem.ValidateMission(objectsToValidate))
                    MissionController.CountMission(mission, countAmount);
            }
        }
    }
}