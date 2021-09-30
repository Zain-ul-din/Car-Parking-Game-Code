using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class ShowRoomManager : MonoBehaviour{
    #region Variables
    private CarShowRoomSO carShowRoomSO;
    private int totalCars;
    [SerializeField] private Transform carSpawnPos;
    private GameObject temp_Car;

    [SerializeField] private Button rightButton,leftButton;
    public int currentIndex { get; private set; }
    public int unlockLevelReq { get; private set; }
    
    private const string CARLOADED_PATH = "ShowRoom";

    public event EventHandler<int> OnIndexChange;
    public static ShowRoomManager instance { get; private set; }
    #endregion

    #region Build-In Methods
    // On Awake
    private void Awake(){
        instance = this;
    }
    // On Start
    private void Start() { 
        if (rightButton && leftButton) {
            LoadCars();
            InstantiateCar(0);
            AddEventListner();
        }
    }

   
    #endregion

    #region Custom-Methods
    private void LoadCars(){
        carShowRoomSO = Resources.Load<CarShowRoomSO>(CARLOADED_PATH);
        totalCars = carShowRoomSO.carPrefabs.Count;
        Debug.Log("Total Car : " + totalCars);
        unlockLevelReq = 0;
    }

    private void AddEventListner(){
        currentIndex = 0;
        const int RESET_INDEX = 0;

        rightButton.onClick.AddListener(() =>{
            SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
            Destroy(temp_Car);
            currentIndex = (currentIndex < (totalCars-1)) ? ++currentIndex : RESET_INDEX;
            InstantiateCar(currentIndex);
            unlockLevelReq = carShowRoomSO.carPrefabs[currentIndex].unLockLevel;
            OnIndexChange?.Invoke(this, carShowRoomSO.carPrefabs[currentIndex].unLockLevel);
        });

        leftButton.onClick.AddListener(() => {
            SoundManager.InstanceOfSoundManager.PlaySound(SoundManager.Sound.OnClickSound);
            Destroy(temp_Car);
            currentIndex = (currentIndex > RESET_INDEX) ? --currentIndex : (totalCars-1);
            InstantiateCar(currentIndex);
            unlockLevelReq = carShowRoomSO.carPrefabs[currentIndex].unLockLevel;
            OnIndexChange?.Invoke(this, carShowRoomSO.carPrefabs[currentIndex].unLockLevel);
        });
    }

    private void InstantiateCar(int CarNum){
        temp_Car=Instantiate(carShowRoomSO.carPrefabs[CarNum].carPrefab, carSpawnPos.position, carSpawnPos.rotation);
        temp_Car.GetComponent<RCC_CarControllerV3>().enabled = false;
    }
    #endregion
}
