using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCredit : SceneBase
{

    public GameObject objRay;
    public GameObject objAlice;
    public GameObject objEmilie;
    public GameObject objTankyou;
    public GameObject objInfo;

    public UIButton btnBack;


    public GameObject objCredit;



    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        btnBack.onClick.Add(new EventDelegate(OnClickBack));
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {

        yield return new WaitForSeconds(0.3f);
        objEmilie.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        objAlice.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        objRay.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        objTankyou.SetActive(true);
        objInfo.SetActive(true);
        yield return new WaitForSeconds(0.7f);

        // 2133 여기까지 무빙 
        Transform trans_ = objCredit.transform;
        Vector2 movePos = objCredit.transform.localPosition;
        int endPos = 2133;
        // float move = objCredit.transform.localPosition.y;
        while (endPos > movePos.y)
        {
            movePos.y += 5;
            yield return new WaitForSeconds(0.07f);
            objCredit.transform.localPosition = movePos;
        }


    }

    void OnClickBack()
    {
        JHSceneManager.Instance.Action(JHSceneManager.ACTION.ACTION_POP);
    }

}
