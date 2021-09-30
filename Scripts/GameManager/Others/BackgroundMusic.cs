using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour{
    [SerializeField] private AudioClip backGroundMusicClip;
    private AudioSource audioSource;
    public static BackgroundMusic Instance { get; private set; }


    public enum VolumeScale{
        High,
        Medium,
        Low,
    }

    private const float high = 1f, medium = .5f, low = .1f;
    // ON Awake
    private void Awake(){
        Instance = this;
    }
    // ON STart
    private void Start(){
      
        if (TryGetComponent<AudioSource>(out audioSource))
            if (!audioSource) gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
    }

    public bool IsPlayingBackGroundMusic() => audioSource.isPlaying;

    public void PlayBackGroundMusic(){
        audioSource.loop = true;
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = backGroundMusicClip;
        audioSource.Play();
    }

    public void PlayBackGroundMusic(VolumeScale volumeScale){
        PlayBackGroundMusic();
        switch (volumeScale){
            case VolumeScale.High:
                audioSource.volume = high;
                return;
            case VolumeScale.Medium:
                audioSource.volume = medium;
                return;
            case VolumeScale.Low:
                audioSource.volume = low;
                return;
        }
    }
    public void PlayBackGroundMusic(VolumeScale volumeScale,ulong Delay){
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = backGroundMusicClip;
        audioSource.Play(Delay);

        switch (volumeScale){
            case VolumeScale.High:
                audioSource.volume = high;
                return;
            case VolumeScale.Medium:
                audioSource.volume = medium;
                return;
            case VolumeScale.Low:
                audioSource.volume = low;
                return;
        }
    }
    public void StopBackGroundMusic(){
        if (!audioSource.isPlaying) return;
        audioSource.Stop();
    }
}
