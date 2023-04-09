using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAlbum : SceneBase
{

    public UIButton btnBack;

    [Header("엔딩 스크롤 관련")]
    public EndingPrefab prefabEnd;
    public UIScrollView scrollView;
    public UIScrollBar scrollBar;
    public UIGrid gridEnd;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(gridEnd.transform.childCount != 0)
        {
            gridEnd.transform.DestroyChildren();
        }

        btnBack.onClick.Add(new EventDelegate(OnClickBack));

        Dictionary<string, xmlEnding> dicEndingData = JHDataManager.Instance.GetmDicEndingData();


        for (int i = 0; i < dicEndingData.Count; i++)
        {
            EndingPrefab obj = Instantiate(prefabEnd, gridEnd.transform) as EndingPrefab;
            obj.transform.localPosition = Vector2.zero;
            obj.transform.localScale = Vector3.one;
            obj.SetEndingPrefab(dicEndingData[i.ToString()]);
            obj.name = dicEndingData[i.ToString()].ending_id;
        }
        scrollBar.value = 0;
        gridEnd.Reposition();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        
    }

    void OnClickBack()
    {
        SceneBase.Instance.PLAY_SE("BTN_CLOSED");
        JHSceneManager.Instance.Action(JHSceneManager.ACTION.ACTION_POPFORNAME, Strings.SCENE_MAIN);
    }

}
