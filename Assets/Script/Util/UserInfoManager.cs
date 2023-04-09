using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{

    // private static SceneBase _instance = null;

    // public static SceneBase Instance
    // {
    //     ///중복 호출 방지
    //     // [MethodImpl(MethodImplOptions.Synchronized)]
    //     get
    //     {
    //         if (_instance == null)
    //         {
    //             ///싱글톤 객체를 찾아서 넣는다.
    //             _instance = (SceneBase)FindObjectOfType(typeof(SceneBase));

    //             ///없다면 생성한다.
    //             if (_instance == null)
    //             {
    //                 string goName = typeof(SceneBase).ToString();
    //                 GameObject go = GameObject.Find(goName);
    //                 if (go == null)
    //                 {
    //                     go = new GameObject();
    //                     go.name = goName;
    //                 }
    //                 _instance = go.AddComponent<SceneBase>();
    //             }
    //         }
    //         return _instance;
    //     }
    // }

    public static UserInfoManager _instance;
    // public static UserInfoManager Instance()
    // {
    //     if (_instance == null)
    //     {
    //         _instance = new UserInfoManager();
    //     }
    //     return _instance;
    // }

    public static UserInfoManager Instance
    {
        //중복 호출 방지
        // [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            if (_instance == null)
            {
                ///싱글톤 객체를 찾아서 넣는다.
                _instance = (UserInfoManager)FindObjectOfType(typeof(UserInfoManager));

                ///없다면 생성한다.
                if (_instance == null)
                {
                    string goName = typeof(UserInfoManager).ToString();
                    GameObject go = GameObject.Find(goName);
                    if (go == null)
                    {
                        go = new GameObject();
                        go.name = goName;
                    }
                    _instance = go.AddComponent<UserInfoManager>();
                }
            }
            return _instance;

        }
    }


    string TUTORIAL = "tutorial";
    string ENDING = "ending"; // 엔딩 타입 
    string DAY_CNT = "day_cnt"; // 데이 카운트 
    string COIN = "coin";
    string SOUND = "sound";



    public void setSaveTutorial(bool flag)
    {
        SecurityPlayerPrefs.SetBool(TUTORIAL, flag);
    }

    public bool getSaveTutorial()
    {
        return SecurityPlayerPrefs.GetBool(TUTORIAL, false);
    }


    public void setSaveState(string state_type, int state)
    {
        SecurityPlayerPrefs.SetInt(state_type, state);
    }

    public int getSaveState(string state_type)
    {
        return SecurityPlayerPrefs.GetInt(state_type, 0);
    }

    public void setSaveHP(int hp)
    {
        SecurityPlayerPrefs.SetInt(Strings.SAVE_CAT_HP, Mathf.Max(0, hp));
    }

    public int getSaveHP()
    {
        return SecurityPlayerPrefs.GetInt(Strings.SAVE_CAT_HP, 100);
    }

    public void setSaveEnding(string id_ed, bool flag)
    {
        SecurityPlayerPrefs.SetBool(ENDING + "_" + id_ed, flag);
    }

    public bool getSaveEnding(string id_ed)
    {
        return SecurityPlayerPrefs.GetBool(ENDING + "_" + id_ed, false);
    }

    public void setSaveDay(int day_cnt)
    {
        SecurityPlayerPrefs.SetInt(DAY_CNT, day_cnt);

    }

    public int getSaveDay()
    {
        return SecurityPlayerPrefs.GetInt(DAY_CNT, 0);
    }

    public void SetSaveCoin(string coin)
    {
        SecurityPlayerPrefs.SetString(COIN, coin);
    }

    public string GetCoin()
    {
        string coin = (SecurityPlayerPrefs.GetInt(COIN, 0)).ToString();
        return coin;
    }

    // 재생 중 true 
    public void SetSaveSound(bool check)
    {
        SecurityPlayerPrefs.SetBool(SOUND, check);
    }

    public bool GetSaveSound()
    {
        return SecurityPlayerPrefs.GetBool(SOUND, true);
    }
}
