using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour{
    private static readonly string FILE_PATH = Application.dataPath + "/SaveFile";

    private static int unLockLevel;
    public const int maxLevel = 15;

    /// <summary>
    /// Initialize  before Save
    /// </summary>
    public static void InitSaveSystem(){
        CheckDirectory();
    }

    /// <summary>
    /// Call On Level Finish
    /// </summary>
    public static void Save(){
        CheckDirectory();
        unLockLevel = LoadData();
        if(unLockLevel < maxLevel)
               unLockLevel++;
        File.WriteAllText(FILE_PATH + "/LevelData.txt", "" + unLockLevel);
    }

    /// <summary>
    /// No Need to Save Before
    /// </summary>
    /// <returns>int</returns>
    public static int LoadData(){
        CheckPrevData();
        string levelData = File.ReadAllText(FILE_PATH + "/LevelData.txt");
        return int.Parse(levelData);
    }

    // Helper Method

    private static void CheckDirectory(){
        if (!Directory.Exists(FILE_PATH)){
            Directory.CreateDirectory(FILE_PATH);
        }
    }

    private static void CheckPrevData(){
        if (!File.Exists(FILE_PATH + "/LevelData.txt")){
            const int firstLevel = 1;
            File.WriteAllText(FILE_PATH + "/LevelData.txt", "" + firstLevel);
        }
    }
}
