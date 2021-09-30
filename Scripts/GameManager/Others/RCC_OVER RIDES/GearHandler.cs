using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class GearHandler : MonoBehaviour{

    #region variables
    [SerializeField] private GameObject gearHandle;
    private Button gearBtn;

    [SerializeField]private float onDriveGearPos;
    [SerializeField]private float onReverseGearPos;
    [SerializeField]private float slideSpeed;

    private bool isDrive = true;

    private float handlePosX, handlePosZ;

    public event EventHandler<bool> OnGearChange;

    public static GearHandler instance { get; private set; }

    private RCC_Settings rCC_Settings;
    private const string RCC_SETTINGSLOADEDPATH = "RCC Assets/RCC_Settings";
    #endregion

    #region Build-In Method
    // On Awake
    private void Awake(){
        instance = this;
    }
    // On Start
    private void Start(){
         gearBtn = GetComponent<Button>();

         handlePosX = gearHandle.transform.localPosition.x;
         handlePosZ = gearHandle.transform.localPosition.z;
         gearHandle.transform.localPosition = new Vector3(handlePosX, onDriveGearPos, handlePosZ);

        rCC_Settings = Resources.Load<RCC_Settings>(RCC_SETTINGSLOADEDPATH);
          /*
        RCC_Customization.SetFrontSuspensionsDistances(RCC_SceneManager.Instance.activePlayerVehicle, 0.20f);
        RCC_Customization.SetRearSuspensionsDistances(RCC_SceneManager.Instance.activePlayerVehicle, 0.10f);
        RCC_Customization.SetFrontSuspensionsSpringForce(RCC_SceneManager.Instance.activePlayerVehicle, .6f);
        RCC_Customization.SetRearSuspensionsSpringForce(RCC_SceneManager.Instance.activePlayerVehicle, .5f);
        RCC_Customization.SetRearSuspensionsSpringDamper(RCC_SceneManager.Instance.activePlayerVehicle, 500f);
        RCC_Customization.SetFrontSuspensionsSpringDamper(RCC_SceneManager.Instance.activePlayerVehicle, 500f);
        RCC_Customization.SetMaximumBrake(RCC_SceneManager.Instance.activePlayerVehicle, 2000f);
           */
        OnGearChange?.Invoke(this, !isDrive);
        OnGearChange?.Invoke(this, isDrive);
        rCC_Settings.autoReverse = false;
        // Debug.Log(rCC_Settings);

        // On CLick
        gearBtn.onClick.AddListener(() => {
            if (isDrive){  // Reverse
                rCC_Settings.useAutomaticGear = false;
                
                isDrive = !isDrive;
                OnGearChange?.Invoke(this, isDrive);
                Vector3 moveDir = new Vector3(handlePosX, onReverseGearPos, handlePosZ);
                gearHandle.transform.localPosition = Vector3.Lerp(gearHandle.transform.localPosition,
                moveDir, slideSpeed * Time.deltaTime);
                if(!RCC_SceneManager.Instance.activePlayerVehicle.stopInstantly)
                RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = RCC_CarControllerV3.IndicatorsOn.All;
            }
            else{  // Drive

                rCC_Settings.useAutomaticGear = true;

                isDrive = !isDrive;
                OnGearChange?.Invoke(this, isDrive);
                Vector3 moveDir = new Vector3(handlePosX, onDriveGearPos, handlePosZ);
                gearHandle.transform.localPosition = Vector3.Lerp(gearHandle.transform.localPosition,
                moveDir, slideSpeed * Time.deltaTime);
				RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Off;
            }
        });
    }
    #endregion
}
