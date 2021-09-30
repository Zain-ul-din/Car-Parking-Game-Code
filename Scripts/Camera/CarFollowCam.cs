using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFollowCam : MonoBehaviour{
    #region Variables
    [SerializeField] private Transform tragetTransform;
    [SerializeField] private float zOffSet;
    [SerializeField] private float camHeight;

    [SerializeField] private GameObject tempCOM;

    private float lerpSpeed = 5f;
    #endregion

    #region Build-In Methods
    // On Start
    private void Start(){
        SetUpCamera();
    }

    private void LateUpdate(){
        
    }
    #endregion

    #region Custom Methods
    private void SetUpCamera(){
        Vector3 cameraStartPos = new Vector3(tragetTransform.position.x -zOffSet, camHeight, tragetTransform.position.z );
        transform.position = cameraStartPos;
        tempCOM = new GameObject("CamHelper");
        tempCOM.transform.position = tragetTransform.position;
        tempCOM.transform.rotation = tragetTransform.rotation;
    }

    private void followTarget(){
        
    }
    #endregion
}
