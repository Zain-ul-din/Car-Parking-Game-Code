using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Loading : MonoBehaviour{

    private IEnumerator loadingCoroutine;
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private float awaitTime = 3.0f;
    public static Loading instance { get; private set; }

    // On Awake
    private void Awake(){
        instance = this;
    }

    // On Start
    private void Start(){

        AudioListener.volume = 0f;

        if (SceneLoader.HideUI){
            HideLoadingUI();
            SceneLoader.HideUI = false;
        }
    }
    
    // On Update
    private void Update(){
        loadingCoroutine= LoadingAwait(awaitTime);
        StartCoroutine(loadingCoroutine);
    }

    private IEnumerator LoadingAwait(float awaitTime){
        while (true){
            yield return new WaitForSeconds(awaitTime);
            if(loadingUI)
            HideLoadingUI();
            AudioListener.volume = 1f;
            if (BackgroundMusic.Instance)
                BackgroundMusic.Instance.PlayBackGroundMusic(BackgroundMusic.VolumeScale.Low);
            StopCoroutine(loadingCoroutine);
        }
    }

    public void ShowLoadingUI(){
        loadingUI.SetActive(true);
    }

    public void HideLoadingUI(){
        loadingUI.SetActive(false);
    }
}
