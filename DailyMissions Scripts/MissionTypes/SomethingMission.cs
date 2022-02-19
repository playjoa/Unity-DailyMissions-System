using System.Linq;
using MissionSystem.Data;
using MissionSystem.Prizes;

namespace MissionSystem.MissionTypes
{
    [CreateAssetMenu(menuName = "Mission - Something", fileName = "New Something Mission")]
    public class GarbageCollectMission : MissionItem
    {
        [Header("Something target:")]
        [SerializeField] private Something targetSomething;
        [SerializeField] private MissionMoneyPrize currencyPrize;

        public override MissionPrize MissionPrize => currencyPrize;
        
        public override bool ValidateMission(params object[] objects)
        {
            if (!objects.Any(obj => obj is Something)) return false;
            
            var somethingToCheck = (Something) objects.First(obj => obj is Something);
            
            return targetSomething.Equals(somethingToCheck);
        }
    }
}