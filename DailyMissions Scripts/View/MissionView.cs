using MissionSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Tools;
using Utils.UI;

namespace MissionSystem.View
{
    public class MissionView : MonoBehaviour
    {
        [Header("Texts")] 
        [SerializeField] private TextMeshProUGUI missionDescriptionText;
        [SerializeField] private TextMeshProUGUI missionProgressText;
        [SerializeField] private TextMeshProUGUI missionProgressPercentageText;

        [Header("Images")] 
        [SerializeField] private Image missionProgressBarFill;

        [Header("Buttons")]
        [SerializeField] private ButtonComponent collectMissionButton;

        [Header("PrizeLabels")] 
        [SerializeField] private IconLabelComponent missionViewPrizeLabel;
        [SerializeField] private IconLabelComponent collectButtonPrizeLabel;
        
        private MissionHolder missionInDisplay;
        private string MissionProgressText =>
            missionInDisplay == null ? "-/-" : missionInDisplay.Count + "/" + missionInDisplay.Target;
        
        private void Awake()
        {
            if (collectMissionButton) collectMissionButton.onClick.AddListener(OnCollectMissionClick);
        }

        private void OnDestroy()
        {
            if (collectMissionButton) collectMissionButton.onClick.RemoveListener(OnCollectMissionClick);
        }
        
        public void DisplayMission(MissionHolder missionToDisplay)
        {
            if (missionToDisplay == null) return;
            
            missionInDisplay = missionToDisplay;
            
            SetTexts();
            SetImages();
            SetButtons();
            SetPrizesLabels();
        }

        private void SetTexts()
        {
            missionDescriptionText.SetCustomText(missionInDisplay.Description);
            missionProgressText.SetCustomText(MissionProgressText);
            missionProgressPercentageText.SetCustomText(missionInDisplay.Progress.FloatToPercentage());
        }

        private void SetImages()
        {
            if (missionProgressBarFill) missionProgressBarFill.fillAmount = missionInDisplay.Progress;
        }

        private void SetButtons()
        {
            ToggleButton(collectMissionButton, missionInDisplay.CanCollect);
        }

        private void SetPrizesLabels()
        {
            collectButtonPrizeLabel.gameObject.SetActive(missionInDisplay.CanCollect);
            missionViewPrizeLabel.gameObject.SetActive(!missionInDisplay.CanCollect);
            
            SetUpPrizeLabel(missionViewPrizeLabel);
            SetUpPrizeLabel(collectButtonPrizeLabel);
            
            void SetUpPrizeLabel(IconLabelComponent label)
            {
                if (!label.gameObject.activeSelf) return;
                label.Text.SetCustomText(missionInDisplay.MissionItem.PrizeValue.NiceCurrency());
                label.Icon.sprite = missionInDisplay.MissionItem.PrizeSprite;
            }
        }

        private void ToggleButton(ButtonComponent button, bool valueToSet = true)
        {
            if(button) button.gameObject.SetActive(valueToSet);
        }

        private void OnCollectMissionClick() => missionInDisplay.Collect();
    }
}