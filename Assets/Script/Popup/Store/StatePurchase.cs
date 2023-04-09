using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatePurchase : MonoBehaviour
{
    public static event Action ACTION_STATE_CALC;

    public UILabel labelMent;

    public UIButton butPlus;
    public UIButton btnMinus;

    public UILabel labelAmount;

    public UIButton btnBuy;
    public UILabel labelBtnBuy;

    public UIButton btnClosed;

    int stateAmount = 1;
    string stateKey;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        butPlus.onClick.Add(new EventDelegate(OnClickPlus));
        btnMinus.onClick.Add(new EventDelegate(OnClickMinus));
        btnBuy.onClick.Add(new EventDelegate(OnClickBuy));
        btnClosed.onClick.Add(new EventDelegate(onClose));
        labelAmount.text = stateAmount.ToString();
        labelBtnBuy.text = string.Format("{0} 쮸르", stateAmount);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            return;
        }
    }


    public void SetPopupStatePurchase(string _stateKey , string title)
    {
        stateKey = _stateKey;
        labelMent.text = string.Format("[{0}]\n얼마나 올리고 싶냥?\n최대치는 100 이다 냥!", title);
    }

    void OnClickPlus()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        stateAmount++;
        int crrState = UserInfoManager.Instance.getSaveState(stateKey) + stateAmount;

        if(crrState > 100)
        {
            stateAmount--;
        }

        labelAmount.text =  stateAmount.ToString();  // crrState > 100 ?  stateAmount--.ToString() :  stateAmount.ToString();  
        labelBtnBuy.text = string.Format("{0} 쮸르", stateAmount);
        // Debug.LogError("Crr Sate : " + UserInfoManager.Instance.getSaveState(stateKey) + " // up State : " + stateAmount + " // crrState : " + crrState);
    }

    void OnClickMinus()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        stateAmount--;
        int index_ =  stateAmount <= 0 ? stateAmount = 0 : stateAmount;
        labelAmount.text = index_.ToString();
        labelBtnBuy.text = string.Format("{0} 쮸르", stateAmount);
    }

    void OnClickBuy()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");
        
        int coin = int.Parse(UserInfoManager.Instance.GetCoin());
        if(stateAmount <= 0)
        {
            return;
        }

        if(stateAmount > 0 &&  (coin >= stateAmount)  )
        {
            UserInfoManager.Instance.SetSaveCoin((coin - stateAmount).ToString());
            UserInfoManager.Instance.setSaveState(stateKey, (UserInfoManager.Instance.getSaveState(stateKey) + stateAmount));
            SceneBase.Instance.ShowToast("구매에 성공했다냥!");
            onClose();
        }
        else 
        {
            SceneBase.Instance.ShowToast("가지고 있는 쮸르가 부족하다냥~");
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        if (ACTION_STATE_CALC != null && ACTION_STATE_CALC.GetInvocationList().Length > 0)
        {
            ACTION_STATE_CALC();
        }
    }

    void onClose()
    {
        Destroy(this.gameObject);
    }
}
