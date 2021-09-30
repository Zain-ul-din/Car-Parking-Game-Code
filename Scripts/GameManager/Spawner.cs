using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour{
    #region variables
    // variables
    private CarSO carSO;
    private CarShowRoomSO carShowRoomSO;
    private const string CARSHOWROOMLOADEDPATH = "ShowRoom";

    private LevelSO levelSO;
    private GameObject levelPF;
    private const string LEVELLOADEDPATH = "Levels/Level";

    private GameObject car;
    public Transform carSpawnPos { get; private set; }
    private const string CARSPAWNOBJECTREF_NAME = "CarPos";

    [SerializeField] private TextMeshProUGUI LevelInfoText;


    [SerializeField] private RCC_Camera rCCCamera;

    private SaveSystemSO saveSystemSO;
    private const string SAVESOLOADEDPATH = "SaveFile";

    public static Spawner instance { get; private set; }

    public int CurrentLevel { get; private set; }
    #endregion
    // On Awake
    private void Awake(){
        instance = this;
    }

    // On Start 
    void Start(){
        saveSystemSO = Resources.Load<SaveSystemSO>(SAVESOLOADEDPATH);
        carShowRoomSO = Resources.Load<CarShowRoomSO>(CARSHOWROOMLOADEDPATH);

        SpawnLevelPrefab(saveSystemSO.levelToLoad);//Spawing Level
        SpawnCar(saveSystemSO.carToLoad);// Spawing Car

        CurrentLevel = saveSystemSO.levelToLoad;

        if (LevelInfoText)
            LevelInfoText.text = "Level "+saveSystemSO.levelToLoad;
    }

    

    // Custom Methods

    private void SpawnLevelPrefab(int index){
        if (index > SaveSystem.maxLevel){
            index = 0;
            return;
        }

        levelSO = Resources.Load<LevelSO>(LEVELLOADEDPATH + index);
        Debug.Log(levelSO);

        levelPF= Instantiate(levelSO.levelPrefab, Vector3.zero, Quaternion.identity);

        carSpawnPos = levelPF.transform.Find(CARSPAWNOBJECTREF_NAME).transform;
    }

    private void SpawnCar(int index){
        if (index >= carShowRoomSO.carPrefabs.Count)
            return;
      
        car = Instantiate(carShowRoomSO.carPrefabs[index].carPrefab, carSpawnPos.position, carSpawnPos.rotation);

        if(rCCCamera)
          SetUpCamera();
        // Mutes Sound
        if (RCC_SceneManager.Instance.activePlayerVehicle)
              RCC_SceneManager.Instance.activePlayerVehicle.audioType = RCC_CarControllerV3.AudioType.Off;

        //Enabling GamePLay UI Manger
        gameObject.GetComponent<GamePlayUIHandler>().enabled = true;
    }

    private void SetUpCamera(){
        rCCCamera.gameObject.SetActive(false);
        rCCCamera.playerCar = RCC_SceneManager.Instance.activePlayerVehicle;
        rCCCamera.gameObject.SetActive(true);
    }
}
