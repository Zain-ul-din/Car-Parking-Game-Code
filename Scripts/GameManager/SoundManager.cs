using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{

    #region Variables
    private AudioSource audioSource;
    public static SoundManager InstanceOfSoundManager { get; private set; }
    private Dictionary<Sound, AudioClip> soundManagerDictionary;
    private const string LOADED_PATH = "Sound/";
    public static bool isMuted;
    public enum Sound{
        OnClickSound
    }

    #endregion

    #region Build-In Methods
    // On Awake
    private void Awake(){
        isMuted = false;
        InstanceOfSoundManager = this;
        Debug.Log(InstanceOfSoundManager);
        audioSource = GetComponent<AudioSource>();
        soundManagerDictionary = new Dictionary<Sound, AudioClip>();
        LoadSoundClips();
    }
    #endregion

    #region Custom Methods

  

    private void LoadSoundClips(){
        foreach (Sound sound in System.Enum.GetValues(typeof(Sound))){
            soundManagerDictionary[sound] = Resources.Load<AudioClip>(LOADED_PATH + sound.ToString());
            Debug.Log(soundManagerDictionary);
        }
    }

    public void PlaySound(Sound soundToPlay){
        audioSource.PlayOneShot(soundManagerDictionary[soundToPlay]);
    }
    #endregion
}
