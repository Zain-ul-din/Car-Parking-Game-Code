using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour{

    private IEnumerator awaitTime;

    // On STart
    private void Start(){
        awaitTime = AwaitTime(2f);
        StartCoroutine(awaitTime);
    }

    private IEnumerator AwaitTime(float TimeInSecToLoad){
        while (true){
            yield return new WaitForSeconds(TimeInSecToLoad);
            SceneLoader.LoadScene(SceneLoader.Scene.ShowRoom);
            StopAllCoroutines();
        }
    }
}
