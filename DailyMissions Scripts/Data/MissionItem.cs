using MissionSystem.Prizes;
using TranslationSystem.Controller;
using UnityEngine;

namespace MissionSystem.Data
{
    public abstract class MissionItem : ScriptableObject
    {
        [Header("Mission Data")]
        [Tooltip("Has to be an unique ID for the mission pool!")]
        [SerializeField] protected string missionId;
        [SerializeField] protected int missionTarget;
        [SerializeField] protected string missionDescriptionKey;

        //Watch on using targets to make an unique ID, in case of mission change
        public virtual string MissionId => missionId;
        public int MissionTarget => missionTarget;
        public virtual string MissionDescription => Translate.GetText(missionDescriptionKey);

        public abstract MissionPrize MissionPrize { get; }
        public int PrizeValue => MissionPrize.PrizeAmount;
        public Sprite PrizeSprite => MissionPrize.PrizeSprite;
        public void AwardPlayer() => MissionPrize.AwardPlayer();
        
        public abstract bool ValidateMission(params object[] objects);
    }
}