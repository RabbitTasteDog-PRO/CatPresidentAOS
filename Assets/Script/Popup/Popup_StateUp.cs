using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_StateUp : UIPopup
{

    public UIButton btnClosed;
    public GameObject Object_Popup_StateUp;

    public GameObject objActiveState;
    public GameObject objRestState;
    public GameObject EventState;

    [Header("업 오브젝트")]
    public UISprite Sprite_Up_1;
    public UISprite Sprite_Up_2;
    public UILabel Label_Up_1;
    public UILabel Label_Up_2;

    [Header("다운 오브젝트")]
    public UISprite Sprite_Down_1;
    public UISprite Sprite_Down_2;
    public UILabel Label_Down_1;
    public UILabel Label_Down_2;

    [Header("휴식 시 사용")]
    public UILabel labelStateUp;
    public UISprite spriteStateUp;
    public UISprite[] spriteStateDown;
    public UILabel[] labelStateDown;

    [Header("대선토론")]
    public UIGrid gridEventState;
    public GameObject[] objEventState;
    public UISprite[] spriteEventState;
    public UILabel[] labelEventState;
    public UILabel labelNone;

    public UILabel[] labelTest;

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

        btnClosed.onClick.Add(new EventDelegate(onClose));
    }

    protected override void onStart()
    {
        base.onStart();
    }

    ///<summery>
    /// 오른 스테이터스 체크 
    ///</summery>
    public void SetPopupActiveStateUp(string[] up_key, int[] up_point, string[] down_key, int[] down_pont)
    {
        objActiveState.SetActive(true);
        objRestState.SetActive(false);
        EventState.SetActive(false);
        // this.gameObject.SetActive(true);
        /// Image/State/{0}
        Sprite_Up_1.spriteName = returnKey(up_key[0]);
        Label_Up_1.text = string.Format("+ {0}", up_point[0]);
        Sprite_Up_2.spriteName = returnKey(up_key[1]);
        Label_Up_2.text = string.Format("+ {0}", up_point[1]);

        Sprite_Down_1.spriteName = returnKey(down_key[0]);
        Label_Down_1.text = string.Format("- {0}", down_pont[0]);
        Sprite_Down_2.spriteName = returnKey(down_key[1]);
        Label_Down_2.text = string.Format("- {0}", down_pont[1]);

        Label_Up_1.color = up_point[0] > 0 ? Color.red : Color.blue;
        Label_Up_2.color = up_point[1] > 0 ? Color.red : Color.blue;
        Label_Down_1.color = down_pont[0] < 0 ? Color.red : Color.blue;
        Label_Down_2.color = down_pont[1] < 0 ? Color.red : Color.blue;

        
        labelTest[0].text = returnTestKey(up_key[0]);
        labelTest[1].text = returnTestKey(up_key[1]);
        labelTest[2].text = returnTestKey(down_key[0]);
        labelTest[3].text = returnTestKey(down_key[1]);
    }


    ///<summery>
    /// 휴식 스테이터스 체크 
    ///</summery>
    public void SetPopupRestStateUp(string up_key, int up_point, string[] down_key, int[] down_pont)
    {
        objActiveState.SetActive(false);
        objRestState.SetActive(true);
        EventState.SetActive(false);

        labelStateUp.text = string.Format(" {0}", up_point);
        labelStateUp.color = up_point > 0 ? Color.red : Color.blue;

        for (int i = 0; i < down_key.Length; i++)
        {
            spriteStateDown[i].spriteName = returnKey(down_key[i]);
            labelStateDown[i].text = string.Format("{0}", down_pont[i]);
            labelStateDown[i].color = down_pont[i] < 0 ? Color.blue : Color.red;
        }
    }

    ///<summary>
    /// 대선토론 이벤트 
    ///</summary>
    public void SetPopupEventState(List<string> state, List<int> point)
    {
        objActiveState.SetActive(false);
        objRestState.SetActive(false);
        EventState.SetActive(true);
        if (state.Count <= 0 || point.Count <= 0)
        {
            labelNone.text = "아무 변화도 없다냥...";
            labelNone.gameObject.SetActive(true);
            return;
        }

        for (int i = 0; i < objEventState.Length; i++)
        {
            objEventState[i].SetActive(state.Count > i);
            for (int k = 0; k < state.Count; k++)
            {
                spriteEventState[k].spriteName = returnKey(state[k]);
                spriteEventState[k].MakePixelPerfect();
                labelEventState[k].text = point[k] > 0 ? string.Format("+ {0}", point[k]) : string.Format("{0}", point[k]);
                labelEventState[k].color = point[k] > 0 ? Color.red : Color.blue;
            }

        }
        gridEventState.Reposition();
    }

    ///<summery>
    /// 키 종류
    /// 카리스마 / CHAR	/ charisma_img
    /// 인지도 / AWAR / popular_img
    /// 성품 / NATU / kind_img
    /// 매력 / CHARM / cute_img
    /// 화술  / TALK	/ talk_img
    /// 외교 / DIPLO	/ abcd_img
    /// 경제 / ECO	/ economy_img
    /// 문화 / CUR / culture_img
    ///</summery>
    string returnKey(string key)
    {
        string sprite_ = "";
        if (key.Equals(Strings.TYPE_CHAR)) { sprite_ = "state_charisma"; }
        if (key.Equals(Strings.TYPE_AWAR)) { sprite_ = "state_popular"; }
        if (key.Equals(Strings.TYPE_NATU)) { sprite_ = "state_kind"; }
        if (key.Equals(Strings.TYPE_CHARM)) { sprite_ = "state_cute"; }
        if (key.Equals(Strings.TYPE_TALK)) { sprite_ = "state_talk"; }
        if (key.Equals(Strings.TYPE_DIPLO)) { sprite_ = "state_abcd"; }
        if (key.Equals(Strings.TYPE_ECO)) { sprite_ = "state_economy"; }
        if (key.Equals(Strings.TYPE_CUR)) { sprite_ = "state_culture"; }
        if (key.Equals(Strings.SAVE_CAT_HP)) { sprite_ = "state_health"; }
        return sprite_;
    }

    ///<summery>
    /// 키 종류
    /// 카리스마 / CHAR	/ charisma_img
    /// 인지도 / AWAR / popular_img
    /// 성품 / NATU / kind_img
    /// 매력 / CHARM / cute_img
    /// 화술  / TALK	/ talk_img
    /// 외교 / DIPLO	/ abcd_img
    /// 경제 / ECO	/ economy_img
    /// 문화 / CUR / culture_img
    ///</summery>
    string returnTestKey(string key)
    {
        string sprite_ = "";
        if (key.Equals(Strings.TYPE_CHAR)) { sprite_ = "카리스마"; }
        if (key.Equals(Strings.TYPE_AWAR)) { sprite_ = "인지도"; }
        if (key.Equals(Strings.TYPE_NATU)) { sprite_ = "성품"; }
        if (key.Equals(Strings.TYPE_CHARM)) { sprite_ = "매력"; }
        if (key.Equals(Strings.TYPE_TALK)) { sprite_ = "화술"; }
        if (key.Equals(Strings.TYPE_DIPLO)) { sprite_ = "외교지식"; }
        if (key.Equals(Strings.TYPE_ECO)) { sprite_ = "경제지식"; }
        if (key.Equals(Strings.TYPE_CUR)) { sprite_ = "문화지식"; }
        if (key.Equals(Strings.SAVE_CAT_HP)) { sprite_ = "state_health"; }
        return sprite_;
    }

    protected override void onClose()
    {
        base.onClose();
    }

}
