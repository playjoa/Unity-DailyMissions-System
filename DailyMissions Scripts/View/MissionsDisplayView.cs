using System.Collections.Generic;
using EconomySystem.Components;
using MissionSystem.Controller;
using MissionSystem.Data;
using UnityEngine;
using UnityEngine.UI;
using Utils.Tools;
using Utils.UI;

namespace MissionSystem.View
{
    public class MissionsDisplayView : MonoBehaviour
    {
        [Header("Missions View Configuration")] 
        [SerializeField] private MissionView missionViewPrefab;
        [SerializeField] private RectTransform rectTransformTarget;

        [Header("Request Mission Stuff")] 
        [SerializeField] private ButtonComponent requestNewMissionButton;
        [SerializeField] private GameObject requestButtonInfo;
        [SerializeField] private IconLabelComponent requestNewMissionPrizeLabel;
        [SerializeField] private ChargeFee requestNewMissionChargeFee;

        private SimpleObjectPool<MissionView> missionViewPool;
        private List<MissionHolder> CurrentPlayerMissions => MissionController.CurrentMissions;
        private int MissionsCount => CurrentPlayerMissions.Count;
        
        private void Awake()
        {
            missionViewPool = new SimpleObjectPool<MissionView>(missionViewPrefab, rectTransformTarget);
            UpdateView();
            
            requestNewMissionButton.onClick.AddListener(OnRequestMissionClick);
            MissionController.OnMissionDataChange += UpdateView;
        }

        private void OnDestroy()
        {
            requestNewMissionButton.onClick.RemoveListener(OnRequestMissionClick);
            MissionController.OnMissionDataChange -= UpdateView;
        }

        private void UpdateView()
        {
            missionViewPool.DeactivatePool();
            
            for (var i = 0; i < MissionsCount; i++)
            {
                var currentMissionView = missionViewPool.RequestActiveObject();
                
                if(currentMissionView != null)
                    currentMissionView.DisplayMission(CurrentPlayerMissions[i]);
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransformTarget);

            SetUpRequestNewMissionView();
        }

        private void SetUpRequestNewMissionView()
        {          
            requestNewMissionPrizeLabel.Text.SetCustomText(requestNewMissionChargeFee.Value.NiceCurrency());
            requestNewMissionPrizeLabel.Icon.sprite = requestNewMissionChargeFee.Icon;
            
            requestNewMissionButton.gameObject.SetActive(MissionController.CanRequestMissions);
            requestButtonInfo.SetActive(MissionController.CanRequestMissions);
        }

        private void OnRequestMissionClick()
        {
            if (requestNewMissionChargeFee.CanCharge)
            {
                if (MissionController.GetNewMissions())
                {
                    requestNewMissionChargeFee.Charge();
                    return;
                }
            }
            requestNewMissionButton.ButtonAnimations.NegativeFeedBack();
        } 
    }
}