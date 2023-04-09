using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JHPopupManager : Ray_Singleton<JHPopupManager>
{
    ///<summery>
    /// 개인정보 처리방침 처리 
    ///</summery>
//     public void openPopup_AccountCheck()
//     {
//         Popup_AccountCheck popup = Instantiate<Popup_AccountCheck>(Resources.Load<Popup_AccountCheck>("Popup/Popup_AccountCheck"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//         // popup.SetPopup_DVD_Preview(ch_index, priview_index);
//     }

//     /// <summery>
//     /// 토스트 
//     /// </summery>
//     public void openPopup_Toast(string toast)
//     {
//         Popup_Toast popup = Instantiate<Popup_Toast>(Resources.Load<Popup_Toast>("Popup/Popup_Toast"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//         popup.SetPopup_Toast(toast);
//         // Base.Instance.PlayBgm(Strings.SFX, Strings.SE_TOAST);
//     }

//     ///<summery>
//     /// 룩변 공유 팝업
//     /// boy_index : 남주 여주 구분 -1 여주 , 나머지 0 부터 
//     /// share_id : id
//     /// item_name : 아이템 이름 
//     ///</summery>
//     public void openPopup_Look_Share(int boy_index, string share_id)
//     {
//         Popup_Look_Share popup = Instantiate<Popup_Look_Share>(Resources.Load<Popup_Look_Share>("Popup/Popup_Look_Share"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//         popup.SetPopup_Look_Share(boy_index, share_id);
//     }

//     ///<summery>
//     /// ch_type : 남주 여주 구분 -1 여주 , 나머지 0 부터
//     /// 룩 구매 팝업 
//     ///</summery>
//     public void openPopup_Buy_Look(int ch_type, int item_type, List<xmlItemData> list)
//     {
//         Popup_Buy_Look popup = Instantiate<Popup_Buy_Look>(Resources.Load<Popup_Buy_Look>("Popup/Popup_Buy_Look"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//         popup.SetPopup_Buy_Look(ch_type, item_type, list);
//     }

//     ///<summery>
//     /// 호감도 팝업 
//     ///</summery>
//     public void openPopup_Profile(int boy_index)
//     {
//         Popup_Profile popup = Instantiate<Popup_Profile>(Resources.Load<Popup_Profile>("Popup/Popup_Profile"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Profile(boy_index);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//     }

//     ///<summery>
//     /// 호감도 팝업 
//     ///</summery>
//     public void openPopup_Buy_Hint()
//     {
//         Popup_Buy_Hint popup = Instantiate<Popup_Buy_Hint>(Resources.Load<Popup_Buy_Hint>("Popup/Popup_Buy_Hint"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//         popup.SetPopup_Buy_Hint();
//     }

//     ///<summery>
//     /// 워너비 메세지 보기 팝업  
//     ///</summery>
//     public void openPopup_Test()
//     {
//         Popup_Test popup = Instantiate<Popup_Test>(Resources.Load<Popup_Test>("Popup/Popup_Test"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//         // popup.SetPopup_Katalk();
//     }

//     ///<summery>
//     /// 워너비 메세지 보기 팝업  
//     ///</summery>
//     public void openPopup_Ingame_Back()
//     {
//         Popup_Ingame_Back popup = Instantiate<Popup_Ingame_Back>(Resources.Load<Popup_Ingame_Back>("Popup/Popup_Ingame_Back"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//         // popup.SetPopup_WannerBMessage();
//     }


//     ///<summery>
//     /// 포기한다고 했을때 만류팝업
//     ///</summery>
//     public void openPopup_Empty_Gold()
//     {
//         Popup_Empty_Gold popup = Instantiate<Popup_Empty_Gold>(Resources.Load<Popup_Empty_Gold>("Popup/Popup_Empty_Gold"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//     }

//     ///<summery>
//     /// 데이트 선택 팝업 
//     ///</summery>
//     public void openPopup_Date_Select(int boy_index)
//     {
//         Popup_Date_Select popup = Instantiate<Popup_Date_Select>(Resources.Load<Popup_Date_Select>("Popup/Popup_Date_Select"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Date_Select(boy_index);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//     }

//     ///<summery>
//     /// 데이트 선택 팝업 
//     ///</summery>
//     public void openPopup_Date_Giveup()
//     {
//         Popup_Date_Giveup popup = Instantiate<Popup_Date_Giveup>(Resources.Load<Popup_Date_Giveup>("Popup/Popup_Date_Giveup"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Date_Giveup();
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//     }

//     ///<summery>
//     /// 썸 선택 팝업 
//     ///</summery>
//     public void openPopup_Sum_Select(int boy_index)
//     {
//         Popup_Sum_Select popup = Instantiate<Popup_Sum_Select>(Resources.Load<Popup_Sum_Select>("Popup/Popup_Sum_Select"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Sum_Select(boy_index);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//     }

//     ///<summery>
//     /// 썸 선택 팝업 
//     ///</summery>
//     public void openPopup_Sum_Buy(List<xmlItemData> list, int boy_index)
//     {
//         Popup_Buy_Sum popup = Instantiate<Popup_Buy_Sum>(Resources.Load<Popup_Buy_Sum>("Popup/Popup_Buy_Sum"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Buy_Sum(list, boy_index);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//     }

//     ///<summery>
//     /// 썸 선택 팝업 
//     ///</summery>
//     public void openPopup_Buy_Date(List<xmlItemData> list, int boy_index)
//     {
//         Popup_Buy_Date popup = Instantiate<Popup_Buy_Date>(Resources.Load<Popup_Buy_Date>("Popup/Popup_Buy_Date"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Buy_Date(list, boy_index);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//     }



//     ///<summery>
//     /// 일러스트 팝업 
//     ///</summery>
//     public void openPopup_Illust(string fileName, string ending_type)
//     {
//         Popup_Illust popup = Instantiate<Popup_Illust>(Resources.Load<Popup_Illust>("Popup/Popup_Illust"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Illust(fileName, ending_type);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//     }

//     ///<summery>
//     /// 리뷰 팝업 
//     ///</summery>
//     public void openPopup_Review()
//     {
//         if (!UserInfoManager.instance().getSaveReview())
//         {
// #if UNITY_IPHONE
//             if (!BecomeAdPlugin.Instance.isUseBecomeAd()) { }
//             else
//             {
//                 Popup_Review popup = Instantiate<Popup_Review>(Resources.Load<Popup_Review>("Popup/Popup_Review"));
//                 popup.transform.parent = this.transform;
//                 popup.transform.localScale = new Vector3(1f, 1f, 1f);
//                 popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//                 popup.gameObject.SetActive(true);
//                 popup.SetPopup_Review();
//                 BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//             }
// #elif UNITY_ANDROID
//             Popup_Review popup = Instantiate<Popup_Review>(Resources.Load<Popup_Review>("Popup/Popup_Review"));
//             popup.transform.parent = this.transform;
//             popup.transform.localScale = new Vector3(1f, 1f, 1f);
//             popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//             popup.gameObject.SetActive(true);
//             popup.SetPopup_Review();
//             BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
// #endif
//         }
//     }

//     ///<summery>
//     /// 무충에서 아무 동작 안하고 껐을때 나오는 팝업 팝업 
//     ///</summery>
//     public void openPopup_Move_Offerwall()
//     {
// #if UNITY_IPHONE
//         /// 아이폰 검수전 동작 안함 
//         if (!BecomeAdPlugin.Instance.isUseBecomeAd()) { }
//         else
//         {
//             Popup_Move_Offerwall popup = Instantiate<Popup_Move_Offerwall>(Resources.Load<Popup_Move_Offerwall>("Popup/Popup_Move_Offerwall"));
//             popup.transform.parent = this.transform;
//             popup.transform.localScale = new Vector3(1f, 1f, 1f);
//             popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//             popup.gameObject.SetActive(true);
//             popup.SetPopup_Move_Offerwall();
//             BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//         }
// #else
//         /// 나머지들은 동작 함 
//         Popup_Move_Offerwall popup = Instantiate<Popup_Move_Offerwall>(Resources.Load<Popup_Move_Offerwall>("Popup/Popup_Move_Offerwall"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Move_Offerwall();
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
// #endif

//     }

//     ///<summery>
//     /// 호감도 팝업 
//     ///</summery>
//     public void openPopup_Buy_Ending(int ch_index, List<xmlItemData> list)
//     {
//         Popup_Buy_Ending popup = Instantiate<Popup_Buy_Ending>(Resources.Load<Popup_Buy_Ending>("Popup/Popup_Buy_Ending"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Buy_Ending(ch_index, list);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//     }


//     public void openPopup_Get_Item(string type, string item_img, string item_name)
//     {
//         Popup_Get_Item popup = Instantiate<Popup_Get_Item>(Resources.Load<Popup_Get_Item>("Popup/Popup_Get_Item"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Get_Item(type, item_img, item_name);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//     }

//     public void openPopup_Idol()
//     {
//         Popup_Idol popup = Instantiate<Popup_Idol>(Resources.Load<Popup_Idol>("Popup/Popup_Idol"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Idol();
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);

//     }

//     ///<summery>
//     /// 썸 분기 이후 나오는 썸체크 팝업
//     ///</summery>
//     public void openPopup_SumCheck(int boy_index)
//     {
//         Popup_SumCheck popup = Instantiate<Popup_SumCheck>(Resources.Load<Popup_SumCheck>("Popup/Popup_SumCheck"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_SumCheck(boy_index);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);

//     }

//     ///<summery>
//     /// 공통호감도 체크 
//     ///</summery>
//     public void openPopup_LoveCheck()
//     {
//         Popup_LoveCheck popup = Instantiate<Popup_LoveCheck>(Resources.Load<Popup_LoveCheck>("Popup/Popup_LoveCheck"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_LoveCheck();
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);

//     }

//     ///<summery>
//     /// 이름입력
//     ///</summery>
//     public void openPopup_Input_Name()
//     {
//         Popup_Input_Name popup = Instantiate<Popup_Input_Name>(Resources.Load<Popup_Input_Name>("Popup/Popup_Input_Name"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Input_Name();
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//     }

//     ///<summery>
//     /// 쿠폰입력
//     ///</summery>
//     public void openPopup_Coupon()
//     {
//         Popup_Coupon popup = Instantiate<Popup_Coupon>(Resources.Load<Popup_Coupon>("Popup/Popup_Coupon"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
//     }
//     ///<summery>
//     /// 0 : 인액 상품 // 1 : 이벤트 /// 2 : 프리미엄 
//     ///</summery>
//     public void openPopup_Gold_Event(int toggle_index)
//     {
//         if (toggle_index == 2)
//         {
//             if (UserInfoManager.instance().getSaveItem(Strings.INAPP_PRIMINUM_1) && UserInfoManager.instance().getSaveItem(Strings.INAPP_PRIMINUM_2))
//             {
//                 toggle_index = 0;
//             }
//         }
//         Popup_Gold_Event popup = Instantiate<Popup_Gold_Event>(Resources.Load<Popup_Gold_Event>("Popup/Popup_Gold_Event"));
//         popup.transform.parent = this.transform;
//         popup.transform.localScale = new Vector3(1f, 1f, 1f);
//         popup.transform.localPosition = new Vector3(0f, 0f, 0f);
//         popup.gameObject.SetActive(true);
//         popup.SetPopup_Gem_Event(toggle_index);
//         BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);

//     }
}
