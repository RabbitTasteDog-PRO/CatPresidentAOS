using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_State : UIPopup
{
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

    public UIButton btnBack;

    public UI2DSprite[] spriteStateImg;
    public UILabel[] labelStatePoint;
    public UIProgressBar[] progressState;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        JHSceneManager.Instance.m_PopupQueue.Enqueue(this.gameObject);
        if (JHSceneManager.Instance.m_DicPopupQueue.ContainsKey(this.gameObject.name) == false)
        {
            JHSceneManager.Instance.m_DicPopupQueue.Add(this.gameObject.name, this.gameObject);
        }

        btnBack.onClick.Add(new EventDelegate(onClose));
    }


    string[] STATE_KEY = new string[]{
            Strings.TYPE_CHAR, Strings.TYPE_AWAR, Strings.TYPE_NATU, Strings.TYPE_CHARM,
            Strings.TYPE_TALK, Strings.TYPE_DIPLO, Strings.TYPE_ECO, Strings.TYPE_CUR
        };
    /// 카리스마 성품 화술 경제지식 인지도 매력 외교지식 문화지식
    public void SetPopup_State()
    {
        this.gameObject.SetActive(true);
        string labelText = "";
        string strPoint = "";
        int pointIndex = 0;
        for (int i = 0; i < STATE_KEY.Length; i++)
        {
            spriteStateImg[i].sprite2D = Resources.Load<Sprite>(string.Format("Image/State/{0}", returnKey(STATE_KEY[i])));
            labelText = labelStatePoint[i].text;
            pointIndex = UserInfoManager.Instance.getSaveState(STATE_KEY[i]);
            // Debug.LogError("############## key : " + STATE_KEY[i] + " // point : " + pointIndex);
            strPoint = string.Format("{0}/100", pointIndex);
            labelStatePoint[i].text = string.Format("{0} {1}", labelText, strPoint);

            progressState[i].value = pointIndex * 0.01f;
        }

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

    protected override void onClose()
    {
        base.onClose();
    }


}
