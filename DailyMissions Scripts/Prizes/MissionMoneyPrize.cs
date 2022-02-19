using System;
using UnityEngine;

namespace MissionSystem.Prizes
{
    [Serializable]
    public class MissionMoneyPrize : MissionPrize
    {
        [Header("Money Config:")] 
        [SerializeField] private int moneyAmount = 50;


        public override void AwardPlayer() 
        {
            //Add your money amount here
            //EconomyController.AddMoney(moneyAmount);    
        } 
    }
}