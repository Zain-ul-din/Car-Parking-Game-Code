using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelExitBtn : MonoBehaviour{
    // On Start
    private void Start(){
        if (TryGetComponent<Button>(out Button exitBtn))
            exitBtn.onClick.AddListener(() =>{
                SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
                SceneLoader.LoadScene(SceneLoader.Scene.ShowRoom); // Loading MainMenu Scene
            });
    }
}



