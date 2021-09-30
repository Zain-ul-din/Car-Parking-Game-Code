using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class ShowRoomCameraController : MonoBehaviour{
    #region Variables
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 10.0f;
    [SerializeField] private float normalDis;
    [SerializeField] private float changeDisAfterAngle = 60f;
    [SerializeField] private float changeDis = 6f;

    [SerializeField] private float xSpeed = 250f;
    [SerializeField] private float ySpeed = 120f;

    [SerializeField] private float yMinLimit = -20f;
    [SerializeField] private float yMaxLimit = 80f;

    private float x = 0f;
    private float y = 0f;

    private float lastPosX;
    private float lastPosY;
    private float disChangeBreakPoint;

    [SerializeField] private bool carSpawn;

    #endregion

    #region Build-In
    void Start(){
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        lastPosX = x;
        lastPosY = y;
        disChangeBreakPoint = yMaxLimit / 2;
        Debug.Log(disChangeBreakPoint);
        ShowRoomManager.instance.OnIndexChange += Instance_OnIndexChange;
    }

    void LateUpdate(){
        if (target){
            OnDrag();
        }
    }
    #endregion
    #region Custom Methods

    private void OnDrag(){
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(Input.touchCount - 1);
            if(touch.phase == TouchPhase.Moved){
                if (!IsPointerOverUIObject()){// Prevent Intrection with UI Buttons
                    x += (touch.deltaPosition.x * xSpeed * 0.02f) / 40f;
                    y -= (touch.deltaPosition.y * ySpeed * 0.02f) / 40f;
                }
            }
        }

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        if (transform.eulerAngles.x <= disChangeBreakPoint)
            distance = Mathf.Lerp(distance,normalDis, distance*Time.deltaTime);
        else
            if(!(transform.eulerAngles.x > 180f))
            distance = Mathf.Lerp(distance, changeDis, distance*Time.deltaTime);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0f, 0f, -distance) + target.position;

        transform.rotation = rotation;
        transform.position = position;

        if (carSpawn){
            x = lastPosX;
            y = lastPosY;
            carSpawn = false;
        }
    }
    #endregion

    // Event Sub
    private void Instance_OnIndexChange(object sender, int e){
        carSpawn = true;
    }
    #region Helper Methods

    private bool IsPointerOverUIObject(){
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private static float ClampAngle(float angle, float min, float max){
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
    #endregion
}
