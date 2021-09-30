using UnityEngine;

// Car SO
[CreateAssetMenu(fileName = "CarPrefab",menuName = "ScriptableObject/CarPrefab")]
public class CarSO : ScriptableObject{
    public GameObject carPrefab;
    public int unLockLevel;
}
