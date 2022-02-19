using System;
using UnityEngine;

namespace MissionSystem.Data
{
    [Serializable]
    public class MissionData 
    {
        public string Id;
        public int Target;
        public int Count;
        public bool Collected = false;

        public float Progress => (float) Count / Target;
        public bool Complete => Count >= Target;
        public bool CanCollect => Complete && !Collected;
        
        public MissionData(string id, int target, int count = 0)
        {
            Id = id;
            Target = target;
            Count = count;
            Collected = false;
        }

        public void Collect()
        {
            if (CanCollect)
                Collected = true;
        }

        public void CountMission(int countAmount = 1)
        {
            if (Complete) return;
            Count = Complete ? Count : Count + countAmount;
        }
    }
}