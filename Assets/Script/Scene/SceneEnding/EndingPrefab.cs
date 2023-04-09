using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPrefab : MonoBehaviour
{

    public UI2DSprite spriteThume;
    public UIButton btnEndMove;
    public UILabel labelEndTitle;

    public GameObject objLock;

    int endingIndex = 0;


    public void SetEndingPrefab(xmlEnding data)
    {
        spriteThume.sprite2D = Resources.Load<Sprite>(string.Format("Image/Ending/THUM/{0}", string.Format("{0}_THUM", data.img)));
        labelEndTitle.text = data.ending_Title;
        objLock.SetActive( !UserInfoManager.Instance.getSaveEnding(data.ending_id) );
        endingIndex = data.index;

        btnEndMove.onClick.Add(new EventDelegate(OnClickEnding));
    }

    void OnClickEnding()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        Dictionary<string, object> mDicData = new Dictionary<string, object>();
        mDicData.Add(Strings.MOVE_TYPE, string.Format("{0}@{1}", Strings.SCENE_ALBUM, endingIndex));
        JHSceneManager.Instance.Action(JHSceneManager.ACTION.ACTION_PUSH, Strings.SCENE_ENDING, mDicData);
    }


}
