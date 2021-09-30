using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GamePlayUIHandler : MonoBehaviour{
    #region Variables
    [Header("Buttons")]
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Button homeBtn;
    

    [Space(20)][Header("Game Objects")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject levelUpUI;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private TextMeshProUGUI levelFinshMsg;

 
    [SerializeField] private GameObject UI; // Main UI Canvas

    private SaveSystemSO saveSystemSO;
    private const string SAVESYSTEMSO_LOADEDPATH = "SaveFile";

    private const int RESETLEVELS = 0;

    private IEnumerator coronutine;
    public static GamePlayUIHandler instance { get; private set; }

    private CarCollisionDetection carCollisionDetection;
    #endregion

    #region Build-In Methods

    // On Awake
    private void Awake(){
        instance = this;    
    }

    public void Start(){

        Debug.Log("INstance  : " + CarCollisionDetection.Instance);

        if (gameOverUI && levelUpUI ){
            HideAll();
            CarCollisionDetection.Instance.OnObjectGetHit += Instance_OnObjectGetHit;// On Level Fail
            CarCollisionDetection.Instance.OnHitWinCheckPoint += Instance_OnHitWinCheckPoint;// On Level Success
            Debug.Log("Sub Sucess");
        }
        if (levelFinshMsg) levelFinshMsg.text = "";
    }

    #endregion
    
    

    private void HideAll(){
        gameOverUI.SetActive(false);
        levelUpUI.SetActive(false);
        UI.SetActive(false);
        if(settingUI)
        settingUI.SetActive(false);
    }


    #region Button OnClick's

    public void NextBtnOnClick(){
        SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
        saveSystemSO = Resources.Load<SaveSystemSO>(SAVESYSTEMSO_LOADEDPATH);
        if (saveSystemSO) // Reset levels if goes up to max
            saveSystemSO.levelToLoad = (saveSystemSO.levelToLoad < SaveSystem.maxLevel) ? ++saveSystemSO.levelToLoad : RESETLEVELS;
        SceneLoader.LoadScene(SceneLoader.Scene.MainScene);// Loading Gameplay Scene
    }

    public void RetryBtnOnClick(){
        SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
        SceneLoader.LoadScene(SceneLoader.Scene.MainScene);
    }

    public void HomeBtnOnClick(){
        SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
        SceneLoader.LoadScene(SceneLoader.Scene.ShowRoom);//  ShowRoom Scene
    }

    public void QuitBtnOnClick(){
        SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
        Application.Quit();
    }

    public void SettingButtonOnClick(){
        SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
        RCC_SceneManager.Instance.activePlayerVehicle.isMenuOpen = true;
        settingUI.SetActive(true);
    }

    public void MuteBtnOnClick(){
        if (SoundManager.isMuted){
            AudioListener.volume = 1f;
            SoundManager.isMuted = false;
        }
        else {
            AudioListener.volume = 0f;
            SoundManager.isMuted = true;
        }
    }
    #endregion

    #region Sub
    // On Level Fail
    private void Instance_OnObjectGetHit(object sender, System.EventArgs e){

        CarCollisionDetection.Instance.OnHitWinCheckPoint -= Instance_OnHitWinCheckPoint;// On Level Success
        CarCollisionDetection.Instance.OnObjectGetHit -= Instance_OnObjectGetHit;

        SettingsUI.instance.DestoryRCCCanvas();// hiding Active RCC Canvas
       
        if (settingUI)
            settingUI.SetActive(false);

        UI.SetActive(true);
        if (levelFinshMsg){
            levelFinshMsg.gameObject.SetActive(true);
            levelFinshMsg.text = "F A I L";
        }
        
        coronutine = Await(3.0f, gameOverUI);
        StartCoroutine(coronutine);
    }

    // On Level Complete
    private void Instance_OnHitWinCheckPoint(object sender, System.EventArgs e){
        CarCollisionDetection.Instance.OnObjectGetHit -= Instance_OnObjectGetHit;// On Level Fail
        CarCollisionDetection.Instance.OnHitWinCheckPoint -= Instance_OnHitWinCheckPoint;
    //    RCC_SceneManager.Instance.activePlayerVehicle.enabled = false;
        SettingsUI.instance.DestoryRCCCanvas();// hiding Active RCC Canvas
        if (settingUI)
            settingUI.SetActive(false);

        UI.SetActive(true);
        if (levelFinshMsg){
            levelFinshMsg.gameObject.SetActive(true);
            levelFinshMsg.text = "SCCUESS";
        }

        #region Saving Levels Data
         // !!! Saving Level Data
         if (SaveSystem.LoadData() < SaveSystem.maxLevel && Spawner.instance.CurrentLevel == SaveSystem.LoadData())
             SaveSystem.Save();// UnLocking new level
        #endregion

        coronutine = Await(1.0f, levelUpUI);
        StartCoroutine(coronutine);
    }
    #endregion

    private IEnumerator Await(float timeInSec,GameObject gameObjectToShow){
        while (true){
            yield return new WaitForSeconds(timeInSec);
            if (levelFinshMsg)
                levelFinshMsg.gameObject.SetActive(false);
            gameObjectToShow.SetActive(true);
            StopAllCoroutines();
        }
    }

   
}
