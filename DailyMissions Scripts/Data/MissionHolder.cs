using MissionSystem.Controller;

namespace MissionSystem.Data
{
    public class MissionHolder
    {
        public MissionItem MissionItem { get; private set; }
        public MissionData MissionData { get; private set; }
        
        public MissionHolder(MissionItem missionItem, MissionData missionData)
        {
            MissionItem = missionItem;
            MissionData = missionData;
        }
        
        public string Id => MissionItem.MissionId;
        public string Description => MissionItem.MissionDescription;
        public bool Complete => MissionData.Complete;
        public bool Collected => MissionData.Collected;
        public bool CanCollect => MissionData.CanCollect;
        public float Progress => MissionData.Progress;
        public int Target => MissionData.Target;
        public int Count => MissionData.Count;

        public void CountMission(int countAmount = 1) => MissionData.CountMission(countAmount);
        
        public void Collect()
        {
            if(Collected) return;
            
            MissionData.Collect();
            MissionItem.AwardPlayer();
            MissionController.ValidateMissionCollect(this);
        } 
    }
}