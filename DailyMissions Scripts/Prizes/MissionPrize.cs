using System;
using UnityEngine;

namespace MissionSystem.Prizes
{
    //Create your custom prizes from this class
    [Serializable]
    public abstract class MissionPrize
    {
        [Header("Prize Config:")]
        [SerializeField] private int prizeAmount = 10;
        [SerializeField] private Sprite prizeSprite;
        
        public int PrizeAmount => prizeAmount;
        public Sprite PrizeSprite => prizeSprite;

        public abstract void AwardPlayer();
    }
}
