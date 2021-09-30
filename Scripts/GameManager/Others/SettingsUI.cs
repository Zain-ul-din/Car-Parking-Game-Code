using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class SettingsUI : MonoBehaviour{
    [SerializeField] private Toggle leftControlToggle , rightControlToggle;

    [SerializeField] private GameObject rightCanvas, leftCanvas;

    private Button exitBtn;
    public static SettingsUI instance { get;private set; }

    public GameObject activeRCCCanvas { get; private set; }
  //  public event EventHandler<Button> OnControlChange;

  //  [SerializeField] private Button leftCanvasSettingBtn, rightCanvasSettingBtn;
    // On Awake
    private void Awake(){
        instance = this;
    }

    // on Start
    private void Start(){

        rightControlToggle.isOn = false;
        leftControlToggle.isOn = true;
        activeRCCCanvas = leftCanvas;

        exitBtn = GetComponent<Button>();

        exitBtn.onClick.AddListener(() => {
            gameObject.SetActive(false);
            RCC_SceneManager.Instance.activePlayerVehicle.isMenuOpen = false;
            SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
        });

        leftControlToggle.onValueChanged.AddListener((bool e) => {
            rightControlToggle.isOn = !e;
            SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
            // Activating Left Canvas
            if (leftCanvas && rightCanvas){
                rightCanvas.SetActive(false);
                leftCanvas.SetActive(true);
                activeRCCCanvas = leftCanvas;
            }
            RCC_SceneManager.Instance.activePlayerVehicle.isMenuOpen = false;
            gameObject.SetActive(false);

        });

        rightControlToggle.onValueChanged.AddListener((bool e) => {
            leftControlToggle.isOn = !e;
            SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
            // Activating Right Canvas
            if (rightCanvas && leftCanvas){
                leftCanvas.SetActive(false);
                rightCanvas.SetActive(true);
                activeRCCCanvas = rightCanvas;
            }
            RCC_SceneManager.Instance.activePlayerVehicle.isMenuOpen = false;
            gameObject.SetActive(false);

        });
    }

    public void DestoryRCCCanvas(){
        if (activeRCCCanvas)
            activeRCCCanvas.SetActive(false);
        else leftCanvas.SetActive(false); 
    }
}
