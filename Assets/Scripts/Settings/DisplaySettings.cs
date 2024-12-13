using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Settings
{
    public class DisplaySettings : MonoBehaviour
    {
        List<(int, int)> resolutionList = new List<(int, int)>() 
        { (960, 540), (1280, 720), (1366, 768), (1600, 900), 
            (1920, 1080), (2560, 1440), (2560, 1600), (3840, 2160) };

        [SerializeField] private Transform resolutionGO;
        [SerializeField] private Transform fullScreenModeGO;

        private (int, int) resolution;  // 해상도
        private int fullScreenMode;     // 화면 모드(전체 화면, 전체 창 모드, 창 모드)

        private TMP_Text resolutionText;
        private TMP_Text fullScreenModeText;

        private Button resolutionButtonLeft;
        private Button fullScreenModeButtonLeft;

        private Button resolutionButtonRight;
        private Button fullScreenModeButtonRight;

        private SettingsPanel settingsPanel;

        private void Awake()
        {
            for (int i = resolutionList.Count - 1; i >= 0; i--)
            {
                if (Screen.currentResolution.width < resolutionList[i].Item1 || Screen.currentResolution.height < resolutionList[i].Item2)
                {
                    resolutionList.RemoveAt(i);
                }
                else break;
            }

            InitOptionItem(resolutionGO, out resolutionText, out resolutionButtonLeft, out resolutionButtonRight,
                OnClickResolutionLeft, OnClickResolutionRight);
            InitOptionItem(fullScreenModeGO, out fullScreenModeText, out fullScreenModeButtonLeft, out fullScreenModeButtonRight,
                OnClickFullScreenModeLeft, OnClickFullScreenModeRight);

            settingsPanel = GetComponentInParent<SettingsPanel>();
        }

        private void OnEnable()
        {
            resolution.Item1 = SavedSettingData.ResolutionWidth;
            resolution.Item2 = SavedSettingData.ResolutionHeight;
            fullScreenMode = SavedSettingData.FullScreenMode;
            UpdateResolution();
            UpdateFullScreenMode();
            if (settingsPanel is not null)
            {
                settingsPanel.SetApplyOnClickListener(true, OnClickApplyBtn);
                settingsPanel.SetCloseOnClickListener(true, OnClickCloseBtn);
            }
            else
            {
                Debug.Log("settingsPanel이 null입니다.");
            }
        }


        public void OnClickApplyBtn()
        {
            // 여기에서 SavedSettingData에 저장하는 작업 수행, 이미 설정은 적용되어있는 상태
            SavedSettingData.ResolutionWidth = resolution.Item1;
            SavedSettingData.ResolutionHeight = resolution.Item2;
            SavedSettingData.FullScreenMode = fullScreenMode;
        }

        private void OnClickCloseBtn()
        {

            GameManager.Instance.SetSettingsPanelEnable(false);
        }

        private void OnClickResolutionLeft()
        {
            for (int i = 0; i < resolutionList.Count; i++)
            {
                if (i != 0 && resolution == resolutionList[i])
                {
                    resolution.Item1 = resolutionList[i - 1].Item1;
                    resolution.Item2 = resolutionList[i - 1].Item2;
                    break;
                }
            }
            UpdateResolution();
        }

        private void OnClickResolutionRight()
        {
            for (int i = 0; i < resolutionList.Count; i++)
            {
                if (i != resolutionList.Count - 1 && resolution == resolutionList[i])
                {
                    resolution.Item1 = resolutionList[i + 1].Item1;
                    resolution.Item2 = resolutionList[i + 1].Item2;
                    break;
                }
            }
            UpdateResolution();
        }


        private void OnClickFullScreenModeLeft()
        {
            if (fullScreenMode > 0) fullScreenMode--;
            if (fullScreenMode == 2) fullScreenMode = 1;
            UpdateFullScreenMode();
        }

        private void OnClickFullScreenModeRight()
        {
            if (fullScreenMode < 3) fullScreenMode++;
            if (fullScreenMode == 2) fullScreenMode = 3;
            UpdateFullScreenMode();
        }

        private void UpdateResolution()
        {
            resolutionText.text = resolution.Item1.ToString() + " x " + resolution.Item2.ToString();
            resolutionButtonLeft.interactable = resolution != resolutionList[0]; 
            resolutionButtonRight.interactable = resolution != resolutionList[resolutionList.Count - 1];
            Screen.SetResolution(resolution.Item1, resolution.Item2, (FullScreenMode)fullScreenMode);
        }

        private void UpdateFullScreenMode()
        {
            switch (fullScreenMode)
            {
                case 0:
                    fullScreenModeText.text = "FullScreen";
                    break;
                case 1:
                    fullScreenModeText.text = "Full Windowed";
                    break;

                case 3:
                    fullScreenModeText.text = "Windowed";
                    break;
                default:
                    fullScreenModeText.text = "Error";
                    break;
            }
            fullScreenModeButtonLeft.interactable = 0 < fullScreenMode;
            fullScreenModeButtonRight.interactable = fullScreenMode < 3;
            Screen.SetResolution(resolution.Item1, resolution.Item2, (FullScreenMode)fullScreenMode);
        }

        private void InitOptionItem(Transform itemObj, out TMP_Text valueText, out Button leftBtn, out Button rightBtn, UnityAction OnClickLeftListener, UnityAction OnClickRightListener)
        {
            valueText = itemObj.Find("OptionStateText").GetComponent<TMP_Text>();
            leftBtn = itemObj.Find("ButtonLeft").GetComponent<Button>();
            rightBtn = itemObj.Find("ButtonRight").GetComponent<Button>();

            if (valueText is null || leftBtn is null || rightBtn is null)
            {
                Debug.LogWarning("OptionItem이 모두 할당되지 않았습니다.");
                return;
            }

            leftBtn.onClick.AddListener(OnClickLeftListener);
            rightBtn.onClick.AddListener(OnClickRightListener);
        }

        private bool CheckDisplaySettingsChange()
        {
            return SavedSettingData.ResolutionWidth != resolution.Item1 ||
                SavedSettingData.ResolutionHeight != resolution.Item2 ||
                SavedSettingData.FullScreenMode != fullScreenMode;
        }
    }
}

