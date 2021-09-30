using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIHandler : MonoBehaviour{
    [SerializeField] private GameObject muteImg;
    private Image muteBtnImg;
    [SerializeField] private Sprite OnMuteSprite, OnUnMuteSprite;

    private void Start(){
        muteBtnImg = muteImg.GetComponent<Image>();
        AudioListener.volume = 1;
    }

    public void muteBtnOnClick(){
        Debug.Log("Click Sound");

        if(AudioListener.volume == 1f){
            // Mute
            SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
            muteBtnImg.sprite = OnMuteSprite;
            AudioListener.volume = 0f;
        }
        else{
            // Un-Mute
            SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
            muteBtnImg.sprite = OnUnMuteSprite;
            AudioListener.volume = 1f;
        }
    }
}
