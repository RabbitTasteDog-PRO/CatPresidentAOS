using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{

    ///<summery>
    /// 스테이터스
    ///</summery>
    public enum eState
    {
        NONE = -1,
        CHAR,       // 카리스마 
        AWAR,    /// 인지도 
        NATU,       /// 성품
        CHARM,      /// 매력
        TALK,       /// 화술
        DIPLO,      /// 외교
        ECO,        /// 경제
        CUR,        /// 문화
        COUNT
    }


    ///</summary>
    /// 유세활동
    ///</summary>
    public enum eActive
    {
        NONE = -1,
        CAMPAIGN,   /// 유세하기
        VISITATION, /// 지역방문
        SERVICE,    /// 봉사활동
        ANALYSIS,   /// 언론분석
        STATE,      /// 상태보기
        SNS,        /// SNS
        IMAGE,      /// 이미지 관리 
        REST,       /// 휴직
        EVENT,      /// 대선토론 이벤트 
        COUNT

    }

    ///<summary>
    /// 팝업 이름
    ///<>
    public enum ePopupName
    {
        NONE = -1,
        Popup_Setting,
        Popup_State,
        Popup_StateUp,
        Popup_Tutorial,
        COUNT
    }


    public enum eScedule
    {
        NONE = -1,
        /***********************************************************************/
        // 유세하기
        eUniversity,        // 대학가 
        ePark,              // 공원 
        eMetro,             // 지하철역 
        eOfficeCenter,      // 오피스상가 
        /***********************************************************************/
        // 지역방문
        eTraditionalMarkets, // 전통시장 V
        eMilitary,          // 군인행사 
        eMarathon,          // 마라톤행사 
        eGameCenter,        // 게임센터
        /***********************************************************************/
        // 봉사활동
        eBriquette,         // 연탄 나르기  
        eSchool,            // 초등학교 
        eAnimalHospital,    // 동물병원 
        eBloodDonation,     // 헌혈의집 


        /***********************************************************************/
        // 언론분석
        eForeignPress,      // 외국언론 
        eDomesticNews,      // 국내뉴스 
        eEcoNews,           // ECO 신문
        eFashionMagazine,   // 패션잡지
        /***********************************************************************/
        /// STATE
        PASS_0,
        PASS_1,
        PASS_2,
        PASS_3,
        /***********************************************************************/
        // SNS
        eFacebook,          // 페이스북 
        eTwitter,           // 트위터 
        eKakoStory,         // 카카오스토리  
        eInstar,            // 인스타

        /***********************************************************************/
        // 이미지관리
        eHairstyle,         //헤어스타일
        eYoga,              //요가
        eClothingshopping,  //옷쇼핑
        eHealth,            //헬스
        /***********************************************************************/
        eRest,              /// 휴식
        eEvent,             /// 터치게임 이벤트
        /***********************************************************************/
        COUNT

    }

    ///<summary>
    /// 인앱 처리시 사용할 enum
    ///</summary>
    public enum eInAppActionCoce
    {
        NONE,
        COMPLETE, // 정상처리
        ERR_INITALIZED, // 초기화 안됨
        ERR_PURCHASING, // 구매인증 처리여부
        ERR_PRODUCT, // 상품 보유 여부 
    }


}
