using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Tutorial : MonoBehaviour
{

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        UserInfoManager.Instance.setSaveTutorial(true);
        
        // JHSceneManager.Instance.m_PopupQueue.Enqueue(this.gameObject);
        // if (JHSceneManager.Instance.m_DicPopupQueue.ContainsKey(this.gameObject.name) == false)
        // {
        //     JHSceneManager.Instance.m_DicPopupQueue.Add(this.gameObject.name, this.gameObject);
        // }
    }

    public UIButton btnTutorialClose;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        btnTutorialClose.onClick.Add(new EventDelegate(onClose));
    }



    void onClose()
    {
        Destroy(this.gameObject);
    }

}
