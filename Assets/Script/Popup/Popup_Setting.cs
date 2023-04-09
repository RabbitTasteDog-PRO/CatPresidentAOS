using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Setting : MonoBehaviour
{
    public UIButton btnClosed;

    public UIButton btnSound;
    public UIButton btnTutorial;
    public UIButton btnCredit;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        btnClosed.onClick.Add(new EventDelegate(OnClickClosed));
        btnSound.onClick.Add(new EventDelegate(onClickSoundOnOff));
        btnTutorial.onClick.Add(new EventDelegate(onClickTutorial));
        btnCredit.onClick.Add(new EventDelegate(onClickCredit));
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // JHSceneManager.Instance.m_PopupQueue.Enqueue(this.gameObject);

        // if (JHSceneManager.Instance.m_DicPopupQueue.ContainsKey(this.gameObject.name) == false)
        // {
        //     JHSceneManager.Instance.m_DicPopupQueue.Add(this.gameObject.name, this.gameObject);
        // }
    }


    public void SetPopupSetting()
    {
        SetSound();
    }

    void SetSound()
    {
        bool sound_ = UserInfoManager.Instance.GetSaveSound();
        string strSoundBtn = sound_ == true ? "On_btn" : "Off_btn";

        btnSound.normalSprite = strSoundBtn;

    }


    void onClickSoundOnOff()
    {
        bool sound_ = UserInfoManager.Instance.GetSaveSound();

        // AudioController.SetCategoryVolume("MUSIC", sound_ == true ? 1 : 0);
        // AudioController.SetCategoryVolume("SFX", sound_ == true ? 1 : 0);
        switch (sound_)
        {
            case true:
                { AudioController.StopAll(); break; }
            case false:
                { SceneBase.Instance.PLAY_BGM("MAIN"); break; }
        }
        UserInfoManager.Instance.SetSaveSound(!sound_);
        SetSound();
    }
    string TEST = "";
    void onClickTutorial()
    {
        // TEST = "TUTORIAL";
        // Destroy(this.gameObject);

        Popup_Tutorial tutorial = Instantiate<Popup_Tutorial>(Resources.Load<Popup_Tutorial>("Prefab/Popup/Popup_Tutorial"));
        tutorial.transform.SetParent(this.gameObject.transform);
        tutorial.transform.localPosition = Vector2.zero;
        tutorial.transform.localScale = Vector3.one;
        tutorial.GetComponent<UIPanel>().depth = 300;
    }

    void onClickCredit()
    {
        // onClose();
        OnClickClosed();
        JHSceneManager.Instance.Action(JHSceneManager.ACTION.ACTION_PUSHFORADD, Strings.SCENE_CREDIT);

    }

    void OnClickClosed()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        // if (TEST == "TUTORIAL")
        // {
        //     Popup_Tutorial tutorial = Instantiate<Popup_Tutorial>(Resources.Load<Popup_Tutorial>("Prefab/Popup/Popup_Tutorial"));
        //     tutorial.transform.SetParent(this.gameObject.transform);
        //     tutorial.transform.localPosition = Vector2.zero;
        //     tutorial.transform.localScale = Vector3.one;
        //     tutorial.GetComponent<UIPanel>().depth = 300;
        // }

        bool sound_ = UserInfoManager.Instance.GetSaveSound();

        // AudioController.SetCategoryVolume("MUSIC", sound_ == true ? 1 : 0);
        // AudioController.SetCategoryVolume("SFX", sound_ == true ? 1 : 0);
        switch (sound_)
        {
            case true:
                { SceneBase.Instance.PLAY_BGM("MAIN"); break; }
            case false:
                { AudioController.StopAll(); break; }
        }
    }

}
