using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ShowRoomUIHandler : MonoBehaviour{
    #region Variables
    private int unLockLevels;

    [SerializeField] private GameObject lockUI;
    [SerializeField] private GameObject lockTextUI;
    [SerializeField] private GameObject playButton;

    private SaveSystemSO saveSystemSO;
        
    private TextMeshProUGUI lockedLevelText;
    private const string UNLOCKLEVEL_TEXT = "UnLock At Level ";
    private const string SAVEFILELOADED_PATH = "SaveFile";

    private IEnumerator awaitTime;
    #endregion

    #region Build_In Methods
    private void Start(){
        SaveSystem.InitSaveSystem();// initializing SaveSystem
        unLockLevels = SaveSystem.LoadData();
        if (lockTextUI && lockTextUI && playButton){
            saveSystemSO = Resources.Load<SaveSystemSO>(SAVEFILELOADED_PATH);
            UIHandler();
        }
    }
    #endregion

    #region Custom Methods
    private void UIHandler(){
        lockedLevelText = lockTextUI.GetComponentInChildren<TextMeshProUGUI>();
        HideUI();

        playButton.GetComponent<Button>().onClick.AddListener(() => {
            if (IsUnLock(unLockLevels, ShowRoomManager.instance.unlockLevelReq)){
                SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
                saveSystemSO.carToLoad = ShowRoomManager.instance.currentIndex;
                awaitTime = AwaitTime(0.2f);
                StartCoroutine(awaitTime);
            }
        });

        ShowRoomManager.instance.OnIndexChange += Instance_OnIndexChange;
    }
    // Delay To Load Next Scene
    private IEnumerator AwaitTime(float TimeinSec){
        while (true){
            yield return new WaitForSeconds(TimeinSec);
            SceneLoader.LoadScene(SceneLoader.Scene.LevelSelection);
            StopAllCoroutines();
        }
    }
    private bool IsUnLock(int UnlockLevels, int levelToUnLock) => UnlockLevels >= levelToUnLock; 
    #endregion

    #region Sub Func
    private void Instance_OnIndexChange(object sender, int e_CarUnlockLevelReq){
        HideUI();
        if (!IsUnLock(unLockLevels, e_CarUnlockLevelReq)){
            lockUI.SetActive(true);
            lockTextUI.SetActive(true);
            lockedLevelText.text = UNLOCKLEVEL_TEXT + e_CarUnlockLevelReq;
        }
    }
    #endregion

    #region Helper Methods
    private void HideUI(){
        lockedLevelText.text = "";
        lockUI.SetActive(false);
        lockTextUI.SetActive(false);
    }
    #endregion
}
