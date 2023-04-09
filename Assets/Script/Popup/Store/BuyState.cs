using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyState : MonoBehaviour
{
    public UISprite spriteState;
    public UILabel labelState;

    public UIButton btnBuy;
    public UILabel labelBuyPrice;

    public UIProgressBar progressBar;
    int depth;

    GameObject RootObject;

    public StatePurchase prefabStatePurch;

    xmlStore storeData ;


    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        StatePurchase.ACTION_STATE_CALC += SetUIProgress;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        StatePurchase.ACTION_STATE_CALC -= SetUIProgress;
    }
    public void SetBuyState(xmlStore data, GameObject root ,int depth)
    {
        storeData = data;
        spriteState.spriteName = data.image;
        labelState.text = data.title;
        labelBuyPrice.text = string.Format("{0} 쮸르", data.price);
        RootObject = root;

        if (btnBuy.onClick != null)
        {
            btnBuy.onClick.Clear();
        }
        SetUIProgress();
        btnBuy.onClick.Add(new EventDelegate(OnClickBuyState));
    }

    void OnClickBuyState()
    {
        StatePurchase popup = Instantiate(prefabStatePurch) as StatePurchase;
        popup.transform.SetParent(RootObject.transform);
        popup.transform.localPosition = Vector2.zero;
        popup.transform.localScale = Vector3.one;
        popup.SetPopupStatePurchase(storeData.key, storeData.title);
    }

    void SetUIProgress()
    {
        float getValeu = UserInfoManager.Instance.getSaveState(storeData.key) * 0.01f;
        progressBar.value = getValeu;
    }

}
