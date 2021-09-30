using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ShowRoom",menuName = "ScriptableObject/ShowRoom")]
public class CarShowRoomSO : ScriptableObject{
    public List<CarSO> carPrefabs;
}
