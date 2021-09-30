using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MuteBtnHandler : MonoBehaviour{
    [SerializeField] private Sprite spriteOnUnMute , spriteOnMute;
    private Image image;
    private Button muteBtn;
    // On Start
    private void Start(){
        image = GetComponent<Image>();
        muteBtn = GetComponent<Button>();

        if (Resources.Load<SaveSystemSO>("SaveFile").isMuted)
            image.sprite = spriteOnMute;
        else image.sprite = spriteOnUnMute;
      
        muteBtn.onClick.AddListener(() => {
            SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
            if (image.sprite == spriteOnUnMute)
                image.sprite = spriteOnMute;
            else image.sprite = spriteOnUnMute;
        });
    }


}
