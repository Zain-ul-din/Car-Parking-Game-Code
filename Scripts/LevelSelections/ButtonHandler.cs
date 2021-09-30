using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonHandler : MonoBehaviour{
    #region Variables
  //  [HideInInspector]
    public int btnIndex;
    private Button btn;
    private SaveSystemSO saveSystem;
    private IEnumerator awaitCoroutine;
    #endregion

    #region 
    public void AddListner(){
        btn = GetComponent<Button>();

        btn.onClick.AddListener(() =>{
            if(ButtonsHandler.IsUnLock(btnIndex, ButtonsHandler.instance.unLockLevels)){
                // On Click Func
                SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
                saveSystem = Resources.Load<SaveSystemSO>("SaveFile");
                saveSystem.levelToLoad = btnIndex;
                awaitCoroutine = AwaitForTime(0.1f);
                StartCoroutine(awaitCoroutine);
            }
        });
    }
    #endregion

   private IEnumerator AwaitForTime(float TimeToWaitInSec){
        while (true){
            yield return new WaitForSeconds(TimeToWaitInSec);
            SceneLoader.LoadScene(SceneLoader.Scene.MainScene);// Loading Main Scene
            StopAllCoroutines();
        }
   }
}
