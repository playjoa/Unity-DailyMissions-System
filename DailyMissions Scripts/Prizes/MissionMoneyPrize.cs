using System;
using UnityEngine;

namespace MissionSystem.Prizes
{
    [Serializable]
    public class MissionMoneyPrize : MissionPrize
    {
        //Add your money amount here
        public override void AwardPlayer() 
        {
            //EconomyController.AddMoney(PrizeAmount);    
        } 
    }
}