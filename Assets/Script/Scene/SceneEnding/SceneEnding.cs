using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnding : SceneBase
{

    [Header("엔딩타이틀")]
    public UILabel labelTitle;
    [Header("엔딩 이미지")]
    public UI2DSprite spriteEnding;
    [Header("엔딩 스토리")]
    public UILabel labelEndingStory;
    public BoxCollider2D colliderEndingStory;
    public UIDragScrollView dragScrollView;

    public UIButton btnExit;

    Dictionary<string, object> mDicSceneData;
    string dataType = "";
    int endingIndex = -1;


    public override void StartWithData(Dictionary<string, object> datas)
    {
        // string[] aaa = "Replay_Date_Eunjae_0_1".Split('_');
        // Debug.Log(" INGAME_INDEX_INFOINGAME_INDEX_INFO :  " + aaa.Length);
        if (datas != null)
        {
            mDicSceneData = datas;
            dataType = mDicSceneData[Strings.MOVE_TYPE].ToString();

            if (dataType.Contains(Strings.SCENE_INGAME))
            {
                btnExit.onClick.Add(new EventDelegate(OnClickEndingBack));
                string[] arrIndex = dataType.Split('@'); //// 스플릿으로 자른 스트링 저장 

                xmlEnding data = JHDataManager.Instance.GetEndingData(arrIndex[1]);

                colliderEndingStory.enabled = UserInfoManager.Instance.getSaveEnding(data.ending_id);
                dragScrollView.enabled = UserInfoManager.Instance.getSaveEnding(data.ending_id);

                if( UserInfoManager.Instance.getSaveEnding(data.ending_id) == true)
                {
                    btnExit.gameObject.SetActive(true);
                }

                /// 엔딩 저장 
                UserInfoManager.Instance.setSaveEnding(data.ending_id, true);
                labelTitle.text = data.ending_Title;
                spriteEnding.sprite2D = Resources.Load<Sprite>(string.Format("Image/Ending/{0}", data.img));
                labelEndingStory.text = data.ment;
                
                SceneBase.Instance.PLAY_BGM(data.bgm);

                initData();

            }

            if (dataType.Contains(Strings.SCENE_ALBUM))
            {
                btnExit.onClick.Add(new EventDelegate(OnClickAlbumBack));
                string[] arrIndex = dataType.Split('@'); //// 스플릿으로 자른 스트링 저장 

                btnExit.gameObject.SetActive(true);

                xmlEnding data = JHDataManager.Instance.GetEndingData(arrIndex[1]);

                colliderEndingStory.enabled = UserInfoManager.Instance.getSaveEnding(data.ending_id);
                dragScrollView.enabled = UserInfoManager.Instance.getSaveEnding(data.ending_id);

                /// 엔딩 저장 
                labelTitle.text = data.ending_Title;
                spriteEnding.sprite2D = Resources.Load<Sprite>(string.Format("Image/Ending/{0}", data.img));
                labelEndingStory.text = data.ment;

                SceneBase.Instance.PLAY_BGM(data.bgm);
            }
        }
    }

    /// <summary>
    /// 인게임 에서 왔을 경우에만 데이터 초기화
    /// </summary>
    void initData()
    {
        string[] STATE_KEY = new string[]{
            Strings.TYPE_CHAR, Strings.TYPE_AWAR, Strings.TYPE_NATU, Strings.TYPE_CHARM,
            Strings.TYPE_TALK, Strings.TYPE_DIPLO, Strings.TYPE_ECO, Strings.TYPE_CUR
        };

        for (int i = 0; i < STATE_KEY.Length; i++)
        {
            UserInfoManager.Instance.setSaveState(STATE_KEY[i], 0);
        }
        UserInfoManager.Instance.setSaveDay(0);
        UserInfoManager.Instance.setSaveHP(100);

    }

    float labelPositionY = 0.0f;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    IEnumerator Start()
    {
        yield return YieldHelper.waitForSeconds(500);
        spriteEnding.gameObject.SetActive(true);

        yield return YieldHelper.waitForSeconds(1200);
        labelTitle.gameObject.SetActive(true);

        yield return YieldHelper.waitForSeconds(1000);
        labelPositionY = labelEndingStory.transform.localPosition.y;
        labelEndingStory.gameObject.SetActive(true);

        yield return YieldHelper.waitForSeconds(1000);
        if (dataType.Contains(Strings.SCENE_INGAME))
        {
            yield return StartCoroutine(EndingStoryAnimation());
        }
        colliderEndingStory.enabled = true;
        dragScrollView.enabled = true;
        btnExit.gameObject.SetActive(true);
    }


    IEnumerator EndingStoryAnimation()
    {

        float labelHeight = labelEndingStory.height;
        float endPosition = labelHeight - labelPositionY;

        // Debug.LogError("############## labelHeight : " + labelHeight + " // endPosition : " + endPosition);
        Transform posY = labelEndingStory.transform;
        Vector2 storyPos = labelEndingStory.transform.localPosition;
        while (true)
        {
            // posY.transform.localPosition.y += (posY.localPosition.y + 5);

            storyPos.y += 5;
            
            if (posY.localPosition.y >= endPosition)
            {
                break;
            }
            // labelEndingStory.transform.localPosition = new Vector2(-354, labelEndingStory.transform.localPosition.y + 5);// posY.localPosition.y;
            posY.localPosition = storyPos;
            yield return new WaitForSeconds(0.07f);
        }

    }


    void OnClickEndingBack()
    {
        SceneBase.Instance.PLAY_SE("BTN_CLOSED");
        JHSceneManager.Instance.Action(JHSceneManager.ACTION.ACTION_POPFORNAME, Strings.SCENE_MAIN);
    }

    void OnClickAlbumBack()
    {
        SceneBase.Instance.PLAY_SE("BTN_CLOSED");
        JHSceneManager.Instance.Action(JHSceneManager.ACTION.ACTION_POP);
    }

}
