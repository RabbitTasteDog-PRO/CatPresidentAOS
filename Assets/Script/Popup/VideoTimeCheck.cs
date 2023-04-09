using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
public class VideoTimeCheck : MonoBehaviour
{

    public UIButton btnTimeCheck;
    public UILabel labelTimeCheck;

    System.TimeSpan timespan;


    int TIME = 60;
    int timer = 0;

    public void SetTimeCheck()
    {
        Debug.LogError("SetTimeCheckSetTimeCheckSetTimeCheckSetTimeCheckSetTimeCheck");
        btnTimeCheck.onClick.Add(new EventDelegate(OnClickTimer));
        StartCoroutine(IETimeCheck());
    }

    IEnumerator IETimeCheck()
    {

        Debug.LogError("IETimeCheck IETimeCheck  IETimeCheck  IETimeCheck  IETimeCheck");
        while (true)
        {
            if (timer > TIME)
            {
                Destroy(this.gameObject);
            }
            timespan = System.TimeSpan.FromSeconds(TIME - timer);
            string timer_ = string.Format("{0:00}:{1:00}", timespan.Minutes, timespan.Seconds);
            labelTimeCheck.text = timer_;
            yield return YieldHelper.waitForSeconds(1000);
            Debug.LogError(timer_);
            timer++;
        }
    }

    bool isActionTimer = false;
    void OnClickTimer()
    {
        if (isActionTimer == false)
        {
            isActionTimer = true;
            Invoke("InvokeisActionTimer", 1.0f);

            SceneBase.Instance.ShowToast("1번 충전소는 1분에 한번씩만 볼 수 있습니다.\n잠시만 기다려주세요.");
        }
    }

    void InvokeisActionTimer()
    {
        isActionTimer = false;
    }


}
