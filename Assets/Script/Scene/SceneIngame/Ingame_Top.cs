using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ingame_Top : MonoBehaviour
{

    public GameObject objPopupRoot;
    public GameObject ActionScedule;

    public UIButton btnBack;
    public UIButton btnStore;
    public UIButton btnSetting;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        btnBack.onClick.Add(new EventDelegate(OnClickBack));
        btnStore.onClick.Add(new EventDelegate(OnClickStore));
        btnSetting.onClick.Add(new EventDelegate(OnClickSetting));
    }

    void OnClickBack()
    {

        if (ActionScedule.activeSelf) { Debug.LogError("유세활동중"); }
        else
        {
            SceneBase.Instance.PLAY_SE("BTN_CLOSED");

            JHSceneManager.Instance.Action(JHSceneManager.ACTION.ACTION_POP);
            // Debug.Log("###################### 3333");
            // StartCoroutine(Move_Scene(Strings.SCENE_MAIN));
        }
    }
    void OnClickStore()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        Popup_Store popup = Instantiate<Popup_Store>(Resources.Load<Popup_Store>("Prefab/Popup/Popup_Store"), objPopupRoot.transform) as Popup_Store;
        popup.transform.localPosition = Vector2.zero;
        popup.transform.localScale = Vector3.one;
        popup.gameObject.SetActive(true);

    }
    void OnClickSetting()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        Popup_Setting popup = Instantiate<Popup_Setting>(Resources.Load<Popup_Setting>("Prefab/Popup/Popup_Setting"), objPopupRoot.transform) as Popup_Setting;
        popup.transform.localPosition = Vector2.zero;
        popup.transform.localScale = Vector3.one;
        popup.SetPopupSetting();
        popup.gameObject.SetActive(true);
    }

}
