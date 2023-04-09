using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    public UIPanel panel;
    public GameObject objRoot;

    void Awake()
    {
        // btnClose.onClick.Add(new EventDelegate(onClose));
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        
    }

    public virtual void SetData(object data_)
    {

    }

    public virtual void SetData(object[] data_)
    {

    }

    protected virtual void onStart()
    {
        JHSceneManager.Instance.m_PopupQueue.Enqueue(objRoot);

        if (JHSceneManager.Instance.m_DicPopupQueue.ContainsKey(objRoot.name) == false)
        {
            JHSceneManager.Instance.m_DicPopupQueue.Add(objRoot.name, objRoot);
        }
    }

    protected virtual void onClose()
    {
        SceneBase.Instance.PLAY_SE("BTN_CLOSED");
        
        if (JHSceneManager.Instance.m_PopupQueue.Count > 0)
        {
            if (JHSceneManager.Instance.m_DicPopupQueue[JHSceneManager.Instance.m_PopupQueue.Peek().name] != null)
            {
                Destroy(JHSceneManager.Instance.m_DicPopupQueue[JHSceneManager.Instance.m_PopupQueue.Peek().name]);
            }
            JHSceneManager.Instance.m_DicPopupQueue.Remove(JHSceneManager.Instance.m_PopupQueue.Peek().name);
            JHSceneManager.Instance.m_PopupQueue.Dequeue();
        }

        Destroy(this.gameObject);
    }


}
