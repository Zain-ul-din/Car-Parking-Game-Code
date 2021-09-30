using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LoadingBarHandler : MonoBehaviour{
   [SerializeField] private Transform loadingBar;
   [SerializeField] private float loadingBarStartPos,loadingBarEndPos;
   [SerializeField] [Range(0.5f,5f)] private float timeToTake;
    public event EventHandler OnLoadingFinshe;
    public static LoadingBarHandler Instance { get; private set; }

    private float velocity;
    private Vector2 startPos,endPos;
    private bool soundHasbeenplayed;

    // On Awake
    private void Awake(){
        Instance = this;
    }
    // On Start
    private void Start(){
        soundHasbeenplayed = false;
        AudioListener.volume = 0f;
        velocity = Mathf.Abs(loadingBarEndPos) / timeToTake; //  Using v = delta X / delta Time
        Debug.Log(velocity);
        startPos = new Vector3(loadingBarStartPos, loadingBar.localPosition.y, loadingBar.localPosition.z);
        endPos = new Vector3(loadingBarEndPos, loadingBar.localPosition.y, loadingBar.localPosition.z);
        loadingBar.localPosition = startPos;
    }
    // On Update
    private void Update(){
        loadingBar.localPosition = Vector3.Lerp(loadingBar.localPosition, endPos, velocity*Time.fixedDeltaTime);
        if (loadingBar.localPosition.x >= loadingBarEndPos - 0.1f )
                OnLoadingFinish();
    }

    
    private void OnLoadingFinish(){
        if (!soundHasbeenplayed){
            AudioListener.volume = 1f;
            soundHasbeenplayed = true;
            OnLoadingFinshe?.Invoke(this, EventArgs.Empty);
            if(!BackgroundMusic.Instance.IsPlayingBackGroundMusic())
              BackgroundMusic.Instance.PlayBackGroundMusic(BackgroundMusic.VolumeScale.Medium);
        }
        Destroy(gameObject);
    }
}
