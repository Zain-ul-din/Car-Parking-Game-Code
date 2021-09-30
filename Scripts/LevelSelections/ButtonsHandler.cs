using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ButtonsHandler : MonoBehaviour{
    #region variables
    [SerializeField]private Sprite unLockSprite,lockSprite;
    private Color unlockTextColor, lockTextColor;

    private ButtonHandler btnhandler;
    private TextMeshProUGUI btnTMPro;
    private Transform buttonContainer;

    private int itr = 1;

    public int unLockLevels { get; private set; }
    public static ButtonsHandler instance { get; private set; }

    private bool isSprite;
    private const string levelTextPrefix = "0"; 
    #endregion

    #region Build-In
    // On Awake
    private void Awake(){
        instance = this;
        unLockLevels = SaveSystem.LoadData();
        lockTextColor = Color.white;
        unlockTextColor = Color.black;
    }
    // On Start
    private void Start(){
        isSprite = (unLockSprite && lockSprite) ? true : false;
        buttonContainer = GetComponent<Transform>();
        LoadButtons();
    }
    #endregion

    #region Custom Methods
    private void LoadButtons(){
        foreach(Transform btn in buttonContainer){
            btnhandler = btn.GetComponent<ButtonHandler>();
            btnhandler.btnIndex = itr;
            btnhandler.AddListner();
            // if Sprite Assign in Inspector
            if (isSprite){
                Image btnImageComp;
                btnImageComp = btn.GetComponent<Image>();
                if (IsUnLock(itr, unLockLevels))
                    btnImageComp.sprite = unLockSprite;
                else 
                   btnImageComp.sprite =  lockSprite;
            }
            btnTMPro = btn.GetComponentInChildren<TextMeshProUGUI>();
            btnTMPro.text = (itr <= 9) ? levelTextPrefix + itr : "" + itr;
            btnTMPro.color = (IsUnLock(itr, unLockLevels)) ? unlockTextColor : lockTextColor;
            
            itr++;
        }
    }
    #endregion

    #region Helper Methods
    public static bool IsUnLock(int BtnVal, int UnlockLevel) => BtnVal <= UnlockLevel;
    #endregion
}
