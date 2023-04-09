using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.IO;

///<summary>
/// 행동 후 스케쥴 구조체
///</summary>
public struct STAticveSceduleData
{

    public eScedule key;
    public string upKey_0;
    public string upKey_1;
    public int upPoint_0;
    public int upPoint_1;
    public string downKey_0;
    public string downKey_1;
    public int downPoint_0;
    public int downPoint_1;
    public string anim_img_1;
    public string anim_img_2;
    public string ment_1;
    public string ment_2;

    public STAticveSceduleData(eScedule _key, string _upKey_0, string _upKey_1, int _upPoint_0, int _upPoint_1,
    string _downKey_0, string _downKey_1, int _downPoint_0, int _downPoint_1, string _anim_img_1, string _anim_img_2,
    string _ment_1, string _ment_2)
    {
        key = _key;
        upKey_0 = _upKey_0;
        upKey_1 = _upKey_1;
        upPoint_0 = _upPoint_0;
        upPoint_1 = _upPoint_1;
        downKey_0 = _downKey_0;
        downKey_1 = _downKey_1;
        downPoint_0 = _downPoint_0;
        downPoint_1 = _downPoint_1;
        anim_img_1 = _anim_img_1;
        anim_img_2 = _anim_img_2;
        ment_1 = _ment_1;
        ment_2 = _ment_2;
    }

}

///<summary>
/// 행동 구조체
///</summary>
public struct STActiveData
{
    // #int	#eActice	#Image	#eScedule	#eScedule	#eScedule	#eScedule
    public string spriteImg;
    public eScedule[] arrScedule;

    public STActiveData(string _spriteImg, eScedule[] arr)
    {
        spriteImg = _spriteImg;
        arrScedule = arr;
    }
}

///<summary>
/// 멘트용 xml 
///<summary>
public class xmlData
{
    public int index;
    public string key;
    public string ment_1;
    public string ment_2;

}

public class xmlEnding
{

    public int index;
    public string ending_id;
    public string ending_Title;
    public string bgm;
    public string img;
    public string ment;
}

public class xmlStore
{
    public int index;
    public string key;
    public string title;
    public string image;
    public int price;

}

public class xmlEvent
{
    public int index;
    public string type;
    public string talk;
    public string checkState;
    public string checkL;
    public string checkR;
    public string checkValue;
    public string upState;
    public string upPoint;

}

public class xmlAlice
{
    public int index;
    public string type;
    public string talk;
    public string checkState;
    public string checkL;
    public string checkR;
    public string checkValue;
    public string upState;
    public string upPoint;

}



public class xmlStandTalkBox
{
    public int index = 0;
    public string type = "";
    public string standTalk = "";
}



public class JHDataManager : Ray_Singleton<JHDataManager>
{
    /*************************************************************************/
    /// 행동 구조체
    protected Dictionary<eActive, STActiveData> mDicActiveData;
    public Dictionary<eActive, STActiveData> GetDicActiveData()
    {
        return mDicActiveData;
    }
    public STActiveData GetSTActiveData(eActive active)
    {
        return mDicActiveData[active];
    }

    /*************************************************************************/
    /// 행동 선택 후 스케쥴 데이터 

    protected Dictionary<eActive, Dictionary<eScedule, STAticveSceduleData>> mDicActiveSceduleData;
    public Dictionary<eActive, Dictionary<eScedule, STAticveSceduleData>> GetDicActiveSceduleData()
    {
        return mDicActiveSceduleData;
    }
    public Dictionary<eScedule, STAticveSceduleData> GetDicSceduleData(eActive _active)
    {
        return mDicActiveSceduleData[_active];
    }

    /*************************************************************************/
    /// 엔딩 데이터 테이블 
    protected Dictionary<string, xmlEnding> mDicEndingData;
    public Dictionary<string, xmlEnding> GetmDicEndingData()
    {
        return mDicEndingData;
    }
    public xmlEnding GetEndingData(string endingType)
    {
        return mDicEndingData[endingType];
    }

    /*************************************************************************/
    /// 상점 데이터 테이블 
    protected Dictionary<string, xmlStore> mDicStoreData;
    public Dictionary<string, xmlStore> GetDicStoreData()
    {
        return mDicStoreData;
    }
    public xmlStore GetStoreData(string key)
    {
        return mDicStoreData[key];
    }

    protected Dictionary<string, xmlEvent> mDicEventStory;
    public Dictionary<string, xmlEvent> GetDicEventStory()
    {
        return mDicEventStory;
    }
    public xmlEvent GetListEventStory(string key)
    {
        return mDicEventStory[key];
    }

    /*************************************************************************/
    ///<summary>
    /// 대기화면 말풍선 대사
    ///<summary>
    protected Dictionary<string, List<xmlStandTalkBox>> mDicStandTalkData;
    public Dictionary<string, List<xmlStandTalkBox>> GetDicStandTalkData()
    {
        return mDicStandTalkData;
    }
    public List<xmlStandTalkBox> GetSandTalkData(string key)
    {
        return mDicStandTalkData[key];
    }


    /*************************************************************************/

    ///<summary>
    /// 데이터 생성 
    ///<summary>
    public void initRelease()
    {
        if (mDicActiveData == null)
        {
            mDicActiveData = new Dictionary<eActive, STActiveData>();

        }

        if (mDicActiveSceduleData == null)
        {
            mDicActiveSceduleData = new Dictionary<eActive, Dictionary<eScedule, STAticveSceduleData>>();

        }

        if (mDicEndingData == null)
        {
            mDicEndingData = new Dictionary<string, xmlEnding>();
        }

        if (mDicStoreData == null)
        {
            mDicStoreData = new Dictionary<string, xmlStore>();
        }

        if (mDicEventStory == null)
        {
            mDicEventStory = new Dictionary<string, xmlEvent>();
        }

        if (mDicStandTalkData == null)
        {
            mDicStandTalkData = new Dictionary<string, List<xmlStandTalkBox>>();
        }
    }

    ///<summary>
    /// 데이터 초기화
    ///</summary>
    public void initData()
    {
        if (mDicActiveData != null)
        {
            mDicActiveData.Clear();
        }

        if (mDicActiveSceduleData != null)
        {
            mDicActiveSceduleData.Clear();
        }

        if (mDicEndingData != null)
        {
            mDicEndingData.Clear();
        }

        if (mDicStoreData != null)
        {
            mDicStoreData.Clear();
        }

        if (mDicEventStory != null)
        {
            mDicEventStory.Clear();
        }

        if (mDicStandTalkData != null)
        {
            mDicStandTalkData.Clear();
        }

    }

    public void LoadTextData()
    {
        /// DATA_ACTIVE
        SceneBase.Instance.dataReader.ReadTextData("DATA_ACTIVE", OnCatActiveDataReadLine);
        /// DATA_SCHEDULE
        SceneBase.Instance.dataReader.ReadTextData("DATA_SCHEDULE", OnCatActiveScheduleReadLine);
        // ENDING_TABLE
        OnCatEndingTableReadLine();
        // STORE_DATA
        OnCatStoreTableReadLine();

        /// 스케쥴 끝나고 이벤트
        // OnCatEventStoryTableReadLine();

        /// 화면 말풍선 대사
        OnCatBubbleTalkReadData();

        plusIndex = 0;

    }

    void OnCatBubbleTalkReadData()
    {
        List<xmlStandTalkBox> listData = XmlParser.Read<xmlStandTalkBox>("Data/Xml/CAT_BUBBLETALK");

        for (int i = 0; i < listData.Count; i++)
        {
            xmlStandTalkBox data = new xmlStandTalkBox();
            data.index = listData[i].index;
            data.type = listData[i].type;
            data.standTalk = listData[i].standTalk;

            if (mDicStandTalkData.ContainsKey(listData[i].type) == false)
            {
                List<xmlStandTalkBox> _list = new List<xmlStandTalkBox>();
                _list.Add(data);
                mDicStandTalkData.Add(listData[i].type, _list);
            }
            else
            {
                List<xmlStandTalkBox> _list = mDicStandTalkData[data.type];
                _list.Add(data);
            }
        }

    }

    void OnCatEventStoryTableReadLine()
    {
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.dataPath + "/Resources/Data/Xml/Event");
        System.IO.FileInfo[] fi = di.GetFiles("*.xml");

        for (int i = 0; i < fi.Length; i++)
        {
            string filekey = fi[i].ToString().Replace(".xml", "").Replace(string.Format("{0}/Resources/Data/Xml/Event/", Application.dataPath), "");
            string filePath = fi[i].ToString().Replace(".xml", "").Replace(string.Format("{0}/Resources/", Application.dataPath), "");
            // Debug.LogError("############################ fileName : " + filekey);
            // Debug.LogError("############################ filePath : " + filePath);

            List<xmlEvent> list_ = XmlParser.Read<xmlEvent>(filePath);

            xmlEvent data_ = new xmlEvent();
            for (int k = 0; k < list_.Count; k++)
            {
                data_.index = list_[k].index;
                data_.type = list_[k].type;

                data_.checkState = list_[k].checkState;
                data_.checkValue = list_[k].checkValue;

                data_.checkL = list_[k].checkL;
                data_.checkR = list_[k].checkR;

                data_.upPoint = list_[k].upPoint;
                data_.upState = list_[k].upState;
            }

            if (mDicEventStory.ContainsKey(filekey) == false)
            {
                // Debug.LogError("############################ fileName : " + filekey);
                mDicEventStory.Add(filekey, data_);
            }

        }
    }

    ///<summary>
    /// 상점 테이블 
    ///</summary>
    void OnCatStoreTableReadLine()
    {
        List<xmlStore> listData = XmlParser.Read<xmlStore>("Data/Xml/STORE_DATA");

        for (int i = 0; i < listData.Count; i++)
        {
            xmlStore data = new xmlStore();
            data.index = listData[i].index;
            data.key = listData[i].key;
            data.title = listData[i].title;
            data.image = listData[i].image;
            data.price = listData[i].price;

            if (mDicStoreData.ContainsKey(data.key) == false)
            {
                mDicStoreData.Add(data.key, data);
            }
        }
    }


    ///<summary>
    /// 엔딩 데이터 
    ///</summary>
    void OnCatEndingTableReadLine()
    {
        List<xmlEnding> listData = XmlParser.Read<xmlEnding>("Data/Xml/ENDING_TABLE");

        for (int i = 0; i < listData.Count; i++)
        {

            xmlEnding data = new xmlEnding();
            data.index = listData[i].index;
            data.ending_id = listData[i].ending_id;
            data.ending_Title = listData[i].ending_Title;
            data.img = listData[i].img;
            data.ment = listData[i].ment;
            data.bgm = listData[i].bgm;

            if (mDicEndingData.ContainsKey(listData[i].index.ToString()) == false)
            {
                mDicEndingData.Add(listData[i].index.ToString(), data);
            }

        }

    }

    void OnCatActiveDataReadLine(string _lines)
    {
        string[] word = _lines.Split('\t');
        int index = int.Parse(word[0]);
        eActive active = RayUtils.Utils.ConvertEnumData<eActive>(word[1]);
        if (active != eActive.REST || active != eActive.STATE)
        {

            string spriteImg = word[2];

            eScedule[] arrScedule = new eScedule[4];
            arrScedule[0] = RayUtils.Utils.ConvertEnumData<eScedule>(word[3]);
            arrScedule[1] = RayUtils.Utils.ConvertEnumData<eScedule>(word[4]);
            arrScedule[2] = RayUtils.Utils.ConvertEnumData<eScedule>(word[5]);
            arrScedule[3] = RayUtils.Utils.ConvertEnumData<eScedule>(word[6]);

            STActiveData data = new STActiveData(spriteImg, arrScedule);
            if (mDicActiveData.ContainsKey(active) == false)
            {
                mDicActiveData.Add(active, data);
            }
            else
            {
                mDicActiveData[active] = data;
            }

        }
    }

    int plusIndex = 0;
    void OnCatActiveScheduleReadLine(string _lines)
    {

        // #0	#1	#2	#3	#4	#5	#6	#7	#8	#9	#10	#11	#12	#13	#14
        // #int	#string	#string	#string	#string	#int	#int	#string	#string	#int	#int	#string	#string	#string	#string
        // #index	#event	#Key	#upKey_1	#upKey_2	#upPoint_1	#upPoint_2	#downKey_1	#downKey_2	#downPoint_1	#downPoint_2	#anim_img_1	#anim_img_2	#ment_1	#ment_2

        string[] word = _lines.Split('\t');
        // Debug.LogError(word[i].ToString());
        int index = int.Parse(word[0]);
        eActive _active = RayUtils.Utils.ConvertEnumData<eActive>(word[1]);

        if (_active != eActive.STATE)
        {
            eScedule _scedule = RayUtils.Utils.ConvertEnumData<eScedule>(word[2]);
            string upKey_0 = word[3];
            string upKey_1 = word[4];
            int upPoint_0 = int.Parse(word[5]);
            int upPoint_1 = int.Parse(word[6]);
            string downKey_0 = word[7];
            string downKey_1 = word[8];
            int downPoint_0 = int.Parse(word[9]);
            int downPoint_1 = int.Parse(word[10]);
            string anim_img_1 = word[11];
            string anim_img_2 = word[12];

            List<xmlData> list_ = XmlParser.Read<xmlData>("Data/Xml/CAT_MENT");

            string ment_1 = list_[Mathf.Min((list_.Count - 1), plusIndex)].ment_1;
            string ment_2 = list_[Mathf.Min((list_.Count - 1), plusIndex)].ment_2;

            STAticveSceduleData data_ = new STAticveSceduleData(_scedule, upKey_0, upKey_1, upPoint_0, upPoint_1,
                        downKey_0, downKey_1, downPoint_0, downPoint_1, anim_img_1, anim_img_2, ment_1, ment_2);

            if (mDicActiveSceduleData.ContainsKey(_active) == false)
            {
                Dictionary<eScedule, STAticveSceduleData> outDic = new Dictionary<eScedule, STAticveSceduleData>();
                outDic.Add(_scedule, data_);
                mDicActiveSceduleData.Add(_active, outDic);
            }
            else
            {
                Dictionary<eScedule, STAticveSceduleData> outDic = mDicActiveSceduleData[_active];
                if (outDic.ContainsKey(_scedule) == false)
                {
                    outDic.Add(_scedule, data_);
                }
            }
            plusIndex++;
        }

    }

    /// 대선토론 시에만 출력되는 xml 
    void onCatEventMentReadLine()
    {

    }

}


