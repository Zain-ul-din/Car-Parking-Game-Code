using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SavingFolder",menuName = "ScriptableObject/SavingFolder")]
public class SaveSystemSO : ScriptableObject {
  //  [HideInInspector]
    public int levelToLoad;
  //  [HideInInspector]
    public int carToLoad;
   
    public bool isMuted;
}
