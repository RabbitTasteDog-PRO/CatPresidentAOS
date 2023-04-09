using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PopupToast : MonoBehaviour
{
    public static Action ACTION_TOAST;

    public UILabel labelMessage;
    public GameObject Object_;

    public void SetPopupToast(string message)
    {
        Object_.SetActive(true);

        labelMessage.text = message;
        StartCoroutine(IEDestroy());
    }


    IEnumerator IEDestroy()
    {
        yield return YieldHelper.waitForSeconds(2000);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        if (ACTION_TOAST != null && ACTION_TOAST.GetInvocationList().Length > 0)
        {
            ACTION_TOAST();
        }
    }

}
