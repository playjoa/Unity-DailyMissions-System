using System;
using EconomySystem.Components;
using EconomySystem.Controller;
using UnityEngine;

namespace MissionSystem.Prizes
{
    [Serializable]
    public class MissionCurrencyPrize : MissionPrize
    {
        [Header("Currency Config:")] 
        [SerializeField] private CurrencyType currencyType = CurrencyType.Coins;

        public CurrencyType Type => currencyType;

        public override void AwardPlayer() => EconomyController.AddCurrency(PrizeAmount, currencyType);
    }
}