using System;
using System.Collections;
using UnityEngine;

public class CarCollisionDetection : MonoBehaviour {

    [SerializeField] private RuntimeAnimatorController animController;

    public event EventHandler OnObjectGetHit;
    public event EventHandler OnHitWinCheckPoint;

    public static CarCollisionDetection Instance { get; private set; }
    

    private IEnumerator coroutine;
    private bool collideWithObject;
    // On Awake
    private void Awake(){
        Instance = this;
        collideWithObject = false;
    }

    public void SetInstance(){
        Instance = this;
    }
    

    // On Collision Enter
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag != "Untagged" && !collideWithObject){

            RCC_SceneManager.Instance.activePlayerVehicle.stopInstantly = true;
            RCC_SceneManager.Instance.activePlayerVehicle.KillEngine();
            RCC_SceneManager.Instance.activePlayerVehicle.enabled = false;
            RCC_SceneManager.Instance.activePlayerVehicle.gameObject.transform.Find("All Audio Sources").gameObject.SetActive(false);

            if (BackgroundMusic.Instance)
                BackgroundMusic.Instance.PlayBackGroundMusic(BackgroundMusic.VolumeScale.Low,100000);

            collideWithObject = true;
           if (!collision.gameObject.TryGetComponent<Animator>(out Animator anim))
                collision.gameObject.AddComponent<Animator>();
           
            collision.gameObject.GetComponent<Animator>().runtimeAnimatorController = animController;

            Handheld.Vibrate();
            OnObjectGetHit?.Invoke(this, EventArgs.Empty);// Test
            coroutine = WaitAndPrint(3.0f);
            StartCoroutine(coroutine);
        }
    }

    // Await (Sec)
    private IEnumerator WaitAndPrint(float waitTime){
        while (true){
            yield return new WaitForSeconds(waitTime);
            OnObjectGetHit?.Invoke(this, EventArgs.Empty);
            Debug.Log("Event Has Been Fired");
            StopAllCoroutines();
        }
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("Trigger");
        // Stop car
        RCC_SceneManager.Instance.activePlayerVehicle.SealRCC();
        RCC_SceneManager.Instance.activePlayerVehicle.gameObject.transform.Find("All Audio Sources").gameObject.SetActive(false);
        BackgroundMusic.Instance.PlayBackGroundMusic(BackgroundMusic.VolumeScale.Low, 100000);
        RCC_SceneManager.Instance.activePlayerVehicle.stopInstantly = true;
      //  RCC_SceneManager.Instance.activePlayerVehicle.KillEngine();
        OnHitWinCheckPoint?.Invoke(this, EventArgs.Empty);
    }


}
