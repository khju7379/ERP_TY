using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TY.Service.Library
{
    public static class TYEdiLayout
    {

        public static string KniaEdiID = "yn11011055";
        //public static string KniaEdiPass = "pass1502@011"; 

        public static string KniaEdiPass = "tyculsan@3515"; 
        //public static string KniaEdiPass = "1111"; 


        #region Description : 반입보고서 
       
            //*---------------(35)-------------------------------------------*
            public static string TEMP_REC1;
            public static string TEMP_REC2;

            public static string UNH_PRN;
            public static string UNH_PRN1 = "UNH";
            public static string UNH_PRN2 = "+";
            public static string UNH_0101;
            public static string UNH_PRN3 = "+";
            public static string UNH_PRN4 = "CUSCAR";
            public static string UNH_PRN5 = ":";
            public static string UNH_PRN6 = "S";
            public static string UNH_PRN7 = ":";
            public static string UNH_PRN8 = "93A";
            public static string UNH_PRN9 = ":";
            public static string UNH_PRN10 = "UN";
            public static string UNH_PRN11 = "'";
            //*--------------------------(29)- 신고번호 ------------------*
            //       ***    장치장번호
            //       ***    화물관리번호 1  ----( 반입일련번호와　같은　역활 )
            //       ***    화물관리번호 2  __|
            // BGM-PRN.

            public static string BGM_PRN;              //  X(30)
            public static string BGM_PRN1 = "BGM";    //  X(03)  VALUE  "BGM".
            public static string BGM_PRN2 = "+";      //  X(01)  VALUE  "+".
            public static string BGM_PRN3 = "632";    //  X(03)  VALUE  "632".
            public static string BGM_PRN4 = "+";      //  X(01)  VALUE  "+".
            //05  BGM-0201.
            public static string BGM_02011;   //BGM-02011           X(08).
            public static string BGM_02012;   //BGM-02012           X(02).
            public static string BGM_02013;   //BGM-02013           X(08).
            public static string BGM_PRN5 = "+";      //  X(01)  VALUE  "+".
            public static string BGM_0202;     //BGM-0202       X(02).
            public static string BGM_PRN6 = "'";      //  X(01)  VALUE  "'".
            //*--------------------------(26)- 반입일시 ------------------*
            //DTM-PRN.
            public static string DTM_PRN;    //  X(26)
            public static string DTM_PRN1 = "DTM";    //  X(03)  VALUE  "DTM".
            public static string DTM_PRN2 = "+";      //  X(01)  VALUE  "+".
            public static string DTM_PRN3 = "50";     //  X(02)  VALUE  "50".
            public static string DTM_PRN4 = ":";      //  X(01)  VALUE  ":".
            //DTM-0102.
            public static string DTM_01021;    //DTM-01021           X(08).
            public static string DTM_01022;    //DTM-01022           X(06).
            public static string DTM_PRN5 = ":";      //  X(01)  VALUE  ":".
            public static string DTM_PRN6 = "204";    //  X(03)  VALUE  "204".
            public static string DTM_PRN7 = "'";      //  X(01)  VALUE  "'".

            //*--------------------------(32)- 신고세관 ------------------*
            //*- 과코드변경（감시과－＞통관지원과）  33->10 (2000.1.10~)
            //LOC-PRN.
            public static string LOC_PRN;           //  X(32) 
            public static string LOC_PRN1 = "LOC";           //  X(03)  VALUE  "LOC".
            public static string LOC_PRN2 = "+";             //  X(01)  VALUE  "+".
            public static string LOC_PRN3 = "41";            //  X(02)  VALUE  "41".
            public static string LOC_PRN4 = "+";             //  X(03)  VALUE  "+".
            public static string LOC_PRN5 = "110";           //  X(03)  VALUE  "016".
            public static string LOC_PRN6 = ":";             //  X(01)  VALUE  ":".
            public static string LOC_PRN7 = "113";           //  X(03)  VALUE  "113".
            public static string LOC_PRN8 = ":";             //  X(01)  VALUE  ":".
            public static string LOC_PRN9 = "KCS";           //  X(03)  VALUE  "KCS".
            public static string LOC_PRN10 = "+";             //  X(01)  VALUE  "+".
            public static string LOC_PRN11 = "10";            //  X(02)  VALUE  "10".
            public static string LOC_PRN12 = ":";             //  X(01)  VALUE  ":".
            public static string LOC_PRN13 = "5AB";           //  X(03)  VALUE  "5AB".
            public static string LOC_PRN14 = ":";             //  X(01)  VALUE  ":".
            public static string LOC_PRN15 = "KCS";           //  X(03)  VALUE  "KCS".
            public static string LOC_PRN16 = "'";             //  X(01)  VALUE  "'".
            //*--------------------------(15)- 품목분류 ------------------*
            //GIS-PRN.
            public static string GIS_PRN;                     // As String * 15
            public static string GIS_PRN1 = "GIS";            //  X(03)  VALUE  "GIS".
            public static string GIS_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string GIS_0101;                      //As String * 2           //GIS-0101                X(02).
            public static string GIS_PRN3 = ":";              //  X(01)  VALUE  ":".
            public static string GIS_PRN4 = "5CA";            //  X(03)  VALUE  "5CA".
            public static string GIS_PRN5 = ":";              //  X(01)  VALUE  ":".
            public static string GIS_PRN6 = "KCS";            //  X(03)  VALUE  "KCS".
            public static string GIS_PRN7 = "'";              //  X(01)  VALUE  "'".
            //*--------------------------(15)- 화물반입유형 --------------*
            //GIS2-PRN.
            public static string GIS2_PRN;                     // As String * 15
            public static string GIS2_PRN1 = "GIS";           //  X(03)  VALUE  "GIS".
            public static string GIS2_PRN2 = "+";             //  X(01)  VALUE  "+".
            public static string GIS2_0101;                    // As String * 2          //GIS2-0101               X(02).
            public static string GIS2_PRN3 = ":";             //  X(01)  VALUE  ":".
            public static string GIS2_PRN4 = "109";           //  X(03)  VALUE  "109".
            public static string GIS2_PRN5 = ":";             //  X(01)  VALUE  ":".
            public static string GIS2_PRN6 = "KCS";           //  X(03)  VALUE  "KCS".
            public static string GIS2_PRN7 = "'";             //  X(01)  VALUE  "'".

            //*--------------------------(14)- 분할반입구분 --------------*
            //GIS5-PRN.
            public static string GIS5_PRN;                   // As String * 14
            public static string GIS5_PRN1 = "GIS";           //  X(03)  VALUE  "GIS".
            public static string GIS5_PRN2 = "+";            //  X(01)  VALUE  "+".
            public static string GIS5_0101;                 //  As String * 1          //GIS5-0101    X(01).
            public static string GIS5_PRN3 = ":";             //  X(01)  VALUE  ":".
            public static string GIS5_PRN4 = "121";           //  X(03)  VALUE  "121".
            public static string GIS5_PRN5 = ":";             //  X(01)  VALUE  ":".
            public static string GIS5_PRN6 = "KCS";           //  X(03)  VALUE  "KCS".
            public static string GIS5_PRN7 = "'";             //  X(01)  VALUE  "'".
            //*--------------------------(28)- 화물관리번호 --------------*
            //       ***    적하목록관리번호
            //       ***   MSN   /    HSN
            //RFF-PRN.
            public static string RFF_PRN;                      // As String * 29
            public static string RFF_PRN1 = "RFF";            //  X(03)  VALUE  "RFF".
            public static string RFF_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string RFF_PRN3 = "XC";             //  X(02)  VALUE  "XC".
            public static string RFF_PRN4 = ":";              //  X(01)  VALUE  ":".
            public static string RFF_0102;                       //  As String * 11          //RFF-0102     X(11).
            public static string RFF_PRN5 = ":";              //  X(01)  VALUE  ":".
            public static string RFF_0103;                     //As String * 4           //RFF-0103     X(04).
            public static string RFF_PRN6 = ":";              //  X(01)  VALUE  ":".
            public static string RFF_0104;                     //As String * 4           //RFF-0104     X(03).
            public static string RFF_PRN7 = "'";              //X(01)  VALUE  "'".
            //*--------------------------(29)- 반입근거번호 --------------*
            public static string BANIP_AREA;                   //As String * 29
            //*--------------------------(17)- 반입갯수／포장종류 --------*
            //***    산물인　경우　갯수　＂０＂
            //GID-PRN.
            public static string GID_PRN;                     //As String * 23
            public static string GID_PRN1 = "GID";            //  X(03)  VALUE  "GID".
            public static string GID_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string GID_PRN3 = "+0:";             //  X(01)  VALUE  "+".
            public static string GID_0102;                     //As String * 2           //GID-0102      X(02).
            public static string GID_PRN4 = ":::";              //  X(01)  VALUE  ":".
            public static string GID_0101;                     //As String * 10          //GID-0101      Z(07)9.
            public static string GID_PRN5 = "'";              //  X(01)  VALUE  "'".
            //*--------------------------(14)- 반입사고유형 --------------*
            //FTX-PRN.
            public static string FTX_PRN;                     //As String * 14
            public static string FTX_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
            public static string FTX_PRN2 = "+";              //   X(01)  VALUE  "+".
            public static string FTX_PRN3 = "ACD";            //   X(03)  VALUE  "ACD".
            public static string FTX_PRN4 = "+";              //   X(01)  VALUE  "+".
            public static string FTX_PRN5 = "+";              //   X(01)  VALUE  "+".
            public static string FTX_PRN6 = "+";              //   X(01)  VALUE  "+".
            public static string FTX_0201;                     //As String * 3           // FTX-0201        X(03).
            public static string FTX_PRN7 = "'";              //   X(01)  VALUE  "'".
            //*--------------------------(26)- 반입중량 ------------------*
            //MEA-PRN.
            public static string MEA_PRN;                     // As String * 28
            public static string MEA_PRN1 = "MEA";            //  X(03)  VALUE  "MEA".
            public static string MEA_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string MEA_PRN3 = "WT";             //  X(02)  VALUE  "WT".
            public static string MEA_PRN4 = "+";              //  X(01)  VALUE  "+".
            public static string MEA_PRN5 = "+";              //  X(01)  VALUE  "+".
            public static string MEA_PRN6 = "KG";             //  X(02)  VALUE  "KG".
            public static string MEA_PRN7 = ":";              //  X(01)  VALUE  ":".
            public static string MEA_0202;                     // As String * 16          //MEA-0202     Z(12)9.9.
            public static string MEA_PRN8 = "'";              //  X(01)  VALUE  "'".
            //*--------------------------(55)- 반입사고 ------------------*
            public static string SAGO_AREA;                    //As String * 55         //  X(55).
            //*--------------------------(70)- 누계반입 ------------------*
            public static string NU_BANIP_AREA;                //As String * 70      //  X(70).
            //*--------------------------(26)-----------------------------*
            //UNT-PRN.
            public static string UNT_PRN;                      //As String * 21
            public static string UNT_PRN1 = "UNT";            //  X(03)  VALUE  "UNT".
            public static string UNT_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string UNT_0101;                     //As String * 6        //UNT-0101      Z(05)9.
            public static string UNT_PRN3 = "+";              //  X(01)  VALUE  "+".
            public static string UNT_0201;                     //As String * 8         //UNT-0201      X(08).
            public static string UNT_PRN4 = "'";              //  X(01)  VALUE  "'".
            public static string UNT_PRN5 = "#";              //  X(01)  VALUE  "#".
            //*--------------------------(29)- 반입근거번호 --------------*
            //      ***    입항반입인　경우　생략
            //BANIP-REC.
            //RFF2-PRN.
            public static string RFF2_PRN;                     //As String * 29
            public static string RFF2_PRN1 = "RFF";            //  X(03)  VALUE  "RFF".
            public static string RFF2_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string RFF2_PRN3 = "ABP";            //  X(03)  VALUE  "ABP".
            public static string RFF2_PRN4 = ":";              //  X(01)  VALUE  ":".
            public static string RFF2_0101;                    //As String * 20          //RFF2-0101     X(20).
            public static string RFF2_PRN5 = "'";              //  X(01)  VALUE  "'".

            //*--------------------------(31)- 반입사고중량 --------------*
            //SAGO-REC.
            //MEA2-PRN.
            public static string MEA2_PRN;                     //As String * 31
            public static string MEA2_PRN1 = "MEA";            //  X(03)  VALUE  "MEA".
            public static string MEA2_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string MEA2_PRN3 = "WT";             //  X(02)  VALUE  "WT".
            public static string MEA2_PRN4 = "+";              //  X(01)  VALUE  "+".
            public static string MEA2_PRN5 = ":";              //  X(01)  VALUE  ":".
            public static string MEA2_PRN6 = ":";              //  X(01)  VALUE  ":".
            public static string MEA2_PRN7 = "14";             //  X(02)  VALUE  "14".
            public static string MEA2_PRN8 = "+";              //  X(01)  VALUE  "+".
            public static string MEA2_PRN9 = "KG";             //  X(02)  VALUE  "KG".
            public static string MEA2_PRN10 = ":";             //  X(01)  VALUE  ":".
            public static string MEA2_0302;                    //As String * 15          //MEA2-0302    Z(12)9.9.
            public static string MEA2_PRN11 = "'";             //  X(01)  VALUE  "'".
            //*--------------------------(24)- 반입사고개수 --------------*
            //MEA3-PRN.
            public static string MEA3_PRN;                     //As String * 24
            public static string MEA3_PRN1 = "MEA";            //  X(03)  VALUE  "MEA".
            public static string MEA3_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string MEA3_PRN3 = "CT";             //  X(02)  VALUE  "CT".
            public static string MEA3_PRN4 = "+";              //  X(01)  VALUE  "+".
            public static string MEA3_PRN5 = ":";              //  X(01)  VALUE  ":".
            public static string MEA3_PRN6 = ":";              //  X(01)  VALUE  ":".
            public static string MEA3_PRN7 = "14";             //  X(02)  VALUE  "14".
            public static string MEA3_PRN8 = "+";              //  X(01)  VALUE  "+".
            public static string MEA3_PRN9 = "CT";             //  X(02)  VALUE  "CT".
            public static string MEA3_PRN10 = ":";             //  X(01)  VALUE  ":".
            public static string MEA3_0301;                    //As String * 8           //MEA3-0301     Z(07)9.
            public static string MEA3_PRN11 = "'";             //  X(01)  VALUE  "'".

            //*--------------------------(29)- 누계반입중량 --------------*
            //NU-BANIP-REC.
            //MEA4-PRN.
            public static string MEA4_PRN;                        //As String * 29
            public static string MEA4_PRN1 = "MEA";              //  X(03)  VALUE  "MEA".
            public static string MEA4_PRN2 = "+";                //  X(01)  VALUE  "+".
            public static string MEA4_PRN3 = "WT";               //  X(02)  VALUE  "WT".
            public static string MEA4_PRN4 = "+";                //  X(01)  VALUE  "+".
            public static string MEA4_PRN5 = ":";                //  X(01)  VALUE  ":".
            public static string MEA4_PRN6 = ":";                //  X(01)  VALUE  ":".
            public static string MEA4_PRN7 = "9";                //  X(01)  VALUE  "9".
            public static string MEA4_PRN8 = "+";                //  X(01)  VALUE  "+".
            public static string MEA4_PRN10 = "KG";              //  X(02)  VALUE  "KG".
            public static string MEA4_PRN11 = ":";               //  X(01)  VALUE  ":".
            public static string MEA4_0301;                      //As String * 15            //MEA4-0301     Z(12)9.9.
            public static string MEA4_PRN12 = "'";              //  X(01)  VALUE  "'".

            //*--------------------------(23)- 누계반입개수 --------------*
            //MEA5-PRN.
            public static string MEA5_PRN;                       //As String * 23
            public static string MEA5_PRN1 = "MEA";              //  X(03)  VALUE  "MEA".
            public static string MEA5_PRN2 = "+";                //  X(01)  VALUE  "+".
            public static string MEA5_PRN3 = "CT";               //  X(02)  VALUE  "CT".
            public static string MEA5_PRN4 = "+";                //  X(01)  VALUE  "+".
            public static string MEA5_PRN5 = ":";                //  X(01)  VALUE  ":".
            public static string MEA5_PRN6 = ":";                //  X(01)  VALUE  ":".
            public static string MEA5_PRN7 = "9";                //  X(01)  VALUE  "9".
            public static string MEA5_PRN8 = "+";                //  X(01)  VALUE  "+".
            public static string MEA5_PRN9 = "CT";               //  X(02)  VALUE  "CT".
            public static string MEA5_PRN10 = ":";               //  X(01)  VALUE  ":".
            public static string MEA5_0301;                       //As String * 8             //MEA5-0301    Z(07)9.
            public static string MEA5_PRN11 = "'";              //  X(01)  VALUE  "'".
            //*--------------------------(10)- 분할반입차수 --------------*
            //CNT-PRN.
            public static string CNT_PRN;                         //As String * 10
            public static string CNT_PRN1 = "CNT";               //  X(03)  VALUE  "CNT".
            public static string CNT_PRN2 = "+";                 //  X(01)  VALUE  "+".
            public static string CNT_PRN3 = "6";                 //  X(01)  VALUE  "6".
            public static string CNT_PRN4 = ":";                 //  X(01)  VALUE  ":".
            public static string CNT_0102;                       //As String * 3              //CNT-0102      ZZ9.
            public static string CNT_PRN5 = "'";                 //  X(01)  VALUE  "'".
        
        #endregion

        #region Description : 반출보고서
         //*-------------------------------------------------------------------------------------*
         //*     << 반출내역 EDI 항목 정의 >>                                                    *
         //*-------------------------------------------------------------------------------------*
          //UNH-PRN.
          //*---------------(35)-------------------------------------------*
       
            public static string CHUNH_PRN;                      //As String * 29
            public static string CHUNH_PRN1 = "UNH";             //  X(03)    "UNH".
            public static string CHUNH_PRN2 = "+";               //  X(01)    "+".
            public static string CHUNH_0101;  //As String * 8            //UNH-0101   X(06).
            public static string CHUNH_PRN3 = "+";               //  X(01)    "+".
            public static string CHUNH_PRN4 = "CUSBRR";          //  X(06)    "CUSCAR".
            public static string CHUNH_PRN5 = ":";               //  X(01)    ":".
            public static string CHUNH_PRN6 = "S";               //  X(01)    "S".
            public static string CHUNH_PRN7 = ":";               //  X(01)    ":".
            public static string CHUNH_PRN8 = "93A";             //  X(03)    "93A".
            public static string CHUNH_PRN9 = ":";               //  X(01)    ":".
            public static string CHUNH_PRN10 = "UN";             //  X(02)    "KE".
            public static string CHUNH_PRN11 = "'";              //  X(01)    "'".

            //*--------------------------(29)------------------*
            //*** 반출일련번호 (장치장번호 / 년/ 일련번호)
            //BGM-PRN.
            public static string CHBGM_PRN; //As String * 30
            public static string CHBGM_PRN1 = "BGM";             //  X(03)  VALUE  "BGM".
            public static string CHBGM_PRN2 = "+";               //  X(01)  VALUE  "+".
            public static string CHBGM_PRN3 = "6NB";             //  X(03)  VALUE  "6NB".
            public static string CHBGM_PRN4 = "+";               //  X(01)  VALUE  "+".
            //05  CHBGM-0201.
            public static string CHBGM_02011;  //As String * 8           //BGM-02011           X(08).
            public static string CHBGM_02012;  //As String * 2           //BGM-02012           X(02).
            public static string CHBGM_02013;  //As String * 8           //BGM-02013           X(06).
            public static string CHBGM_PRN5 = "+";               //X(01)  VALUE  "+".
            public static string CHBGM_0202;  //As String * 2            //BGM-0202       X(02).
            public static string CHBGM_PRN6 = "'";               //  X(01)  VALUE  "'".

            //*--------------------------(27)- 반출일시 ------------------*
            //DTM-PRN.
            public static string CHDTM_PRN; //As String * 27
            public static string CHDTM_PRN1 = "DTM";             //  X(03)  VALUE  "DTM".
            public static string CHDTM_PRN2 = "+";               //  X(01)  VALUE  "+".
            public static string CHDTM_PRN3 = "261";             //  X(02)  VALUE  "261".
            public static string CHDTM_PRN4 = ":";               //  X(01)  VALUE  ":".
            //DTM-0102.
            public static string CHDTM_01021;  //As String * 8           //DTM-01021           X(08).
            public static string CHDTM_01022;  //As String * 6           //DTM-01022           X(06).
            public static string CHDTM_PRN5 = ":";               //  X(01)  VALUE  ":".
            public static string CHDTM_PRN6 = "204";             //  X(03)  VALUE  "204".
            public static string CHDTM_PRN7 = "'";               //  X(01)  VALUE  "'".
            //*--------------------------(32)- 신고세관 ------------------*
            //*- 과코드변경（감시과－＞통관지원과）  33->10 (2000.1.10~)
            //LOC-PRN.
            public static string CHLOC_PRN; //As String * 32
            public static string CHLOC_PRN1 = "LOC";             //  X(03)  VALUE  "LOC".
            public static string CHLOC_PRN2 = "+";               //  X(01)  VALUE  "+".
            public static string CHLOC_PRN3 = "41";              //  X(02)  VALUE  "41".
            public static string CHLOC_PRN4 = "+";               //  X(03)  VALUE  "+".
            public static string CHLOC_PRN5 = "110";             //  X(03)  VALUE  "016".
            public static string CHLOC_PRN6 = ":";               //  X(01)  VALUE  ":".
            public static string CHLOC_PRN7 = "113";             //  X(03)  VALUE  "113".
            public static string CHLOC_PRN8 = ":";               //  X(01)  VALUE  ":".
            public static string CHLOC_PRN9 = "KCS";             //  X(03)  VALUE  "KCS".
            public static string CHLOC_PRN10 = "+";              //  X(01)  VALUE  "+".
            public static string CHLOC_PRN11 = "10";             //  X(02)  VALUE  "10".
            public static string CHLOC_PRN12 = ":";              //  X(01)  VALUE  ":".
            public static string CHLOC_PRN13 = "5AB";            //  X(03)  VALUE  "5AB".
            public static string CHLOC_PRN14 = ":";              //  X(01)  VALUE  ":".
            public static string CHLOC_PRN15 = "KCS";            //  X(03)  VALUE  "KCS".
            public static string CHLOC_PRN16 = "'";              //  X(01)  VALUE  "'".
            //*--------------------------(15)- 화물반출유형 ------------------*
            //GIS-PRN.
            public static string CHGIS_PRN; //As String * 15
            public static string CHGIS_PRN1 = "GIS";             //  X(03)  VALUE  "GIS".
            public static string CHGIS_PRN2 = "+";               //  X(01)  VALUE  "+".
            public static string CHGIS_0101;  //As String * 2            //GIS-0101   X(02).
            public static string CHGIS_PRN3 = ":";               //  X(01)  VALUE  ":".
            public static string CHGIS_PRN4 = "148";             //  X(03)  VALUE  "148".
            public static string CHGIS_PRN5 = ":";               //  X(01)  VALUE  ":".
            public static string CHGIS_PRN6 = "KCS";             //  X(03)  VALUE  "KCS".
            public static string CHGIS_PRN7 = "'";              //  X(01)  VALUE  "'".
            //*--------------------------(14)- 분할반출구분 --------------*
            //GIS2-PRN.
            public static string CHGIS2_PRN; //As String * 14
            public static string CHGIS2_PRN1 = "GIS";            //  X(03)  VALUE  "GIS".
            public static string CHGIS2_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string CHGIS2_0101;  //As String * 1           //GIS2-0101     X(01).
            public static string CHGIS2_PRN3 = ":";              //  X(01)  VALUE  ":".
            public static string CHGIS2_PRN4 = "121";            //  X(03)  VALUE  "121".
            public static string CHGIS2_PRN5 = ":";              //  X(01)  VALUE  ":".
            public static string CHGIS2_PRN6 = "KCS";            //  X(03)  VALUE  "KCS".
            public static string CHGIS2_PRN7 = "'";              //  X(01)  VALUE  "'".
            //*--------------------------(28)- 화물관리번호 --------------*
            //       ***    적하목록관리번호
            //       ***   MSN   /    HSN
            //RFF-PRN.
            public static string CHRFF_PRN; //As String * 29
            public static string CHRFF_PRN1 = "RFF";             //  X(03)  VALUE  "RFF".
            public static string CHRFF_PRN2 = "+";               //  X(01)  VALUE  "+".
            public static string CHRFF_PRN3 = "XC";              //  X(02)  VALUE  "XC".
            public static string CHRFF_PRN4 = ":";               //  X(01)  VALUE  ":".
            public static string CHRFF_0102;  //As String * 11           //RFF-0102     X(11).
            public static string CHRFF_PRN5 = ":";               //  X(01)  VALUE  ":".
            public static string CHRFF_0103;  //As String * 4            //RFF-0103     X(04).
            public static string CHRFF_PRN6 = ":";               //  X(01)  VALUE  ":".
            public static string CHRFF_0104;  //As String * 4            //RFF-0104     X(03).
            public static string CHRFF_PRN7 = "'";               //X(01)  VALUE  "'".
            //*--------------------------(29)- 반출근거번호 --------------*
            public static string BANCHUL_AREA;    //As String * 29
            //*--------------------------(14)- 반출총갯수--------*
            //GID-PRN.
            public static string CHGID_PRN; //As String * 21
            public static string CHGID_PRN1 = "GID";             //  X(03)  VALUE  "GID".
            public static string CHGID_PRN2 = "+";               //  X(01)  VALUE  "+".
            public static string CHGID_PRN3 = "+0";               //  X(01)  VALUE  "+".
            public static string CHGID_PRN4 = "::::";               //  X(01)  VALUE  "+".
            public static string CHGID_0101;  //As String * 10            //GID-0101      Z(07)9.
            public static string CHGID_PRN5 = "'";               //  X(01)  VALUE  "'".
            //*--------------------------(26)- 반출중량 ------------------*
            //MEA-PRN.
            public static string CHMEA_PRN; //As String * 27
            public static string CHMEA_PRN1 = "MEA";            //  X(03)  VALUE  "MEA".
            public static string CHMEA_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string CHMEA_PRN3 = "WT";             //  X(02)  VALUE  "WT".
            public static string CHMEA_PRN4 = "+";              //  X(01)  VALUE  "+".
            public static string CHMEA_PRN5 = "+";             //  X(01)  VALUE  "+".
            public static string CHMEA_PRN6 = "KG";             //  X(02)  VALUE  "KG".
            public static string CHMEA_PRN7 = ":";              //  X(01)  VALUE  ":".
            public static string CHMEA_0202;  //As String * 16          //MEA-0202     Z(12)9.9.
            public static string CHMEA_PRN8 = "'";              //  X(01)  VALUE  "'".
            //*--------------------------(63)- 누계반입 ------------------*
            public static string NU_AREA; //As String * 63      //  X(63).
            //*--------------------------(26)-----------------------------*
            //UNT-PRN.
            public static string CHUNT_PRN; //As String * 21
            public static string CHUNT_PRN1 = "UNT";            //  X(03)  VALUE  "UNT".
            public static string CHUNT_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string CHUNT_0101;  //As String * 6           //UNT-0101      Z(05)9.
            public static string CHUNT_PRN3 = "+";              //  X(01)  VALUE  "+".
            public static string CHUNT_0201;  //As String * 8           //UNT-0201      X(06).
            public static string CHUNT_PRN4 = "'";              //  X(01)  VALUE  "'".
            public static string CHUNT_PRN5 = "#";              //  X(01)  VALUE  "#".
            //*--------------------------(29)- 반출근거번호 --------------*
            //BANCHUL-REC.
            //RFF2-PRN.
            public static string CHRFF2_PRN; //As String * 29
            public static string CHRFF2_PRN1 = "RFF";            //  X(03)  VALUE  "RFF".
            public static string CHRFF2_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string CHRFF2_PRN3 = "ABP";            //  X(03)  VALUE  "ABP".
            public static string CHRFF2_PRN4 = ":";              //  X(01)  VALUE  ":".
            public static string CHRFF2_0101;  //As String * 20          //RFF2-0101     X(20).
            public static string CHRFF2_PRN5 = "'";              //  X(01)  VALUE  "'".

            //*--------------------------(30)- 누계반출중량 --------------*
            //NU-REC.
            public static string NU_REC; //As String * 63                //X(63).
            //MEA2-PRN.
            public static string CHMEA2_PRN; //As String * 30
            public static string CHMEA2_PRN1 = "MEA";            //  X(03)  VALUE  "MEA".
            public static string CHMEA2_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string CHMEA2_PRN3 = "WT";             //  X(02)  VALUE  "WT".
            public static string CHMEA2_PRN4 = "+";              //  X(01)  VALUE  "+".
            public static string CHMEA2_PRN5 = ":";              //  X(01)  VALUE  ":".
            public static string CHMEA2_PRN6 = ":";              //  X(01)  VALUE  ":".
            public static string CHMEA2_PRN7 = "9";              //  X(02)  VALUE  "9".
            public static string CHMEA2_PRN8 = "+";              //  X(01)  VALUE  "+".
            public static string CHMEA2_PRN9 = "KG";             //  X(02)  VALUE  "KG".
            public static string CHMEA2_PRN10 = ":";             //  X(01)  VALUE  ":".
            public static string CHMEA2_0101;  //As String * 15          //MEA2-0101    Z(12)9.9.
            public static string CHMEA2_PRN11 = "'";             //  X(01)  VALUE  "'".
            //*--------------------------(23)- 누계반출개수 --------------*
            //MEA3-PRN.
            public static string CHMEA3_PRN;                     //As String * 23
            public static string CHMEA3_PRN1 = "MEA";            //  X(03)  VALUE  "MEA".
            public static string CHMEA3_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string CHMEA3_PRN3 = "CT";             //  X(02)  VALUE  "CT".
            public static string CHMEA3_PRN4 = "+";              //  X(01)  VALUE  "+".
            public static string CHMEA3_PRN5 = ":";              //  X(01)  VALUE  ":".
            public static string CHMEA3_PRN6 = ":";              //  X(01)  VALUE  ":".
            public static string CHMEA3_PRN7 = "9";              //  X(02)  VALUE  "9".
            public static string CHMEA3_PRN8 = "+";              //  X(01)  VALUE  "+".
            public static string CHMEA3_PRN9 = "CT";             //  X(02)  VALUE  "CT".
            public static string CHMEA3_PRN10 = ":";             //  X(01)  VALUE  ":".
            public static string CHMEA3_0101;  //As String * 8           //MEA3-0101     Z(07)9.
            public static string CHMEA3_PRN11 = "'";             //  X(01)  VALUE  "'".
            //*--------------------------(10)- 분할반출차수 --------------*
            //CNT-PRN.
            public static string CHCNT_PRN;                        //As String * 10
            public static string CHCNT_PRN1 = "CNT";               //  X(03)  VALUE  "CNT".
            public static string CHCNT_PRN2 = "+";                 //  X(01)  VALUE  "+".
            public static string CHCNT_PRN3 = "6";                 //  X(01)  VALUE  "6".
            public static string CHCNT_PRN4 = ":";                 //  X(01)  VALUE  ":".
            public static string CHCNT_0101;                          //As String * 3              //CNT-0101      ZZ9.
            public static string CHCNT_PRN5 = "'";                 //  X(01)  VALUE  "'".
        
        #endregion

        #region Description : 체화예정보고서
         //*-------------------------------------------------------------------------------------*
         //*     << 체화내역 EDI 항목 정의 >>                                                    *
         //*-------------------------------------------------------------------------------------*
          //*---------------(35)-------------------------------------------*
        
            public static string CHEUNH_PRN; //As String * 35
            public static string CHEUNH_PRN1 = "UNH";    //  X(03)    "UNH".
            public static string CHEUNH_PRN2 = "+";      //  X(01)    "+".
            public static string CHEUNH_01011;  //As String * 4    //UNH-0101   X(04).
            public static string CHEUNH_01012;  //As String * 10    //UNH-0102   X(10).
            public static string CHEUNH_PRN3 = "+";      //  X(01)    "+".
            public static string CHEUNH_PRN4 = "CUSCAR"; //  X(06)    "CUSCAR".
            public static string CHEUNH_PRN5 = ":";      //  X(01)    ":".
            public static string CHEUNH_PRN6 = "S";      //  X(01)    "S".
            public static string CHEUNH_PRN7 = ":";      //  X(01)    ":".
            public static string CHEUNH_PRN8 = "93A";    //  X(03)    "93A".
            public static string CHEUNH_PRN9 = ":";      //  X(01)    ":".
            public static string CHEUNH_PRN10 = "UN";    //  X(02)    "UN".
            public static string CHEUNH_PRN11 = "'";     //  X(01)    "'".

            //*--------------------------(29)- 화물관리번호 ------------------*
            //       ***    장치장번호
            //       ***    화물관리번호 1  ----
            //       ***    화물관리번호 2  __|
            // BGM-PRN.
            public static string CHEBGM_PRN;//As String * 29
            public static string CHEBGM_PRN1 = "BGM";    //  X(03)  VALUE  "BGM".
            public static string CHEBGM_PRN2 = "+";      //  X(01)  VALUE  "+".
            public static string CHEBGM_PRN3 = "5II";    //  X(03)  VALUE  "5II".
            public static string CHEBGM_PRN4 = "+";      //  X(01)  VALUE  "+".
            //05  BGM-0101.
            public static string CHEBGM_01011;  //As String * 11 //BGM-01011   X(11).
            public static string CHEBGM_01012;  //As String * 4  //BGM-01012   X(4).
            public static string CHEBGM_01013;  //As String * 3  //BGM-01013   X(3).
            public static string CHEBGM_PRN5 = "+";      //  X(01)  VALUE  "+".
            public static string CHEBGM_PRN6 = "9";      //  X(01)  VALUE  "9".
            public static string CHEBGM_PRN7 = "'";      //  X(01)  VALUE  "//".

            //*--------------------------(21)- 반출통고일시 ------------------*
            //DTM-PRN.
            public static string CHEDTM_PRN; //As String * 21
            public static string CHEDTM_PRN1 = "DTM";    //  X(03)  VALUE  "DTM".
            public static string CHEDTM_PRN2 = "+";      //  X(01)  VALUE  "+".
            public static string CHEDTM_PRN3 = "108";     //  X(03)  VALUE  "108".
            public static string CHEDTM_PRN4 = ":";      //  X(01)  VALUE  ":".
            //DTM-0102.
            public static string CHEDTM_0101;  //As String * 8   //DTM-0101      X(08).
            public static string CHEDTM_PRN5 = ":";      //  X(01)  VALUE  ":".
            public static string CHEDTM_PRN6 = "102";    //  X(03)  VALUE  "102".
            public static string CHEDTM_PRN7 = "'";      //  X(01)  VALUE  "'".

            //*--------------------------(21)- 체화예정일자 ------------------*
            //DTM2-PRN.
            public static string CHEDTM2_PRN; //As String * 21
            public static string CHEDTM2_PRN1 = "DTM";    //  X(03)  VALUE  "DTM".
            public static string CHEDTM2_PRN2 = "+";      //  X(01)  VALUE  "+".
            public static string CHEDTM2_PRN3 = "206";    //  X(03)  VALUE  "206".
            public static string CHEDTM2_PRN4 = ":";      //  X(01)  VALUE  ":".
            //DTM-0102.
            public static string CHEDTM2_0101;  //As String * 8   //DTM-0101      X(08).
            public static string CHEDTM2_PRN5 = ":";      //  X(01)  VALUE  ":".
            public static string CHEDTM2_PRN6 = "102";    //  X(03)  VALUE  "102".
            public static string CHEDTM2_PRN7 = "'";      //  X(01)  VALUE  "'".
            //*--------------------------(32)- 신고세관 ------------------*
            //*- 과코드변경（감시과－＞통관지원과）  33->10 (2000.1.10~)
            //LOC-PRN.
            public static string CHELOC_PRN; //As String * 32
            public static string CHELOC_PRN1 = "LOC";           //  X(03)  VALUE  "LOC".
            public static string CHELOC_PRN2 = "+";             //  X(01)  VALUE  "+".
            public static string CHELOC_PRN3 = "41";            //  X(02)  VALUE  "41".
            public static string CHELOC_PRN4 = "+";             //  X(03)  VALUE  "+".
            public static string CHELOC_PRN5 = "110";           //  X(03)  VALUE  "110".
            public static string CHELOC_PRN6 = ":";             //  X(01)  VALUE  ":".
            public static string CHELOC_PRN7 = "113";           //  X(03)  VALUE  "113".
            public static string CHELOC_PRN8 = ":";             //  X(01)  VALUE  ":".
            public static string CHELOC_PRN9 = "KCS";           //  X(03)  VALUE  "KCS".
            public static string CHELOC_PRN10 = "+";             //  X(01)  VALUE  "+".
            public static string CHELOC_PRN11 = "10";            //  X(02)  VALUE  "10".
            public static string CHELOC_PRN12 = ":";             //  X(01)  VALUE  ":".
            public static string CHELOC_PRN13 = "5AB";           //  X(03)  VALUE  "5AB".
            public static string CHELOC_PRN14 = ":";             //  X(01)  VALUE  ":".
            public static string CHELOC_PRN15 = "KCS";           //  X(03)  VALUE  "KCS".
            public static string CHELOC_PRN16 = "'";             //  X(01)  VALUE  "'".
            //*--------------------------(28)- 반입장소부호 ------------------*
            //LOC2-PRN.
            public static string CHELOC2_PRN; //As String * 28
            public static string CHELOC2_PRN1 = "LOC";           //  X(03)  VALUE  "LOC".
            public static string CHELOC2_PRN2 = "+";             //  X(01)  VALUE  "+".
            public static string CHELOC2_PRN3 = "14";            //  X(02)  VALUE  "14".
            public static string CHELOC2_PRN4 = "+";             //  X(03)  VALUE  "+".
            public static string CHELOC2_0101;  //As String * 10         //LOC-0101      X(10).
            public static string CHELOC2_PRN5 = ":";             //  X(01)  VALUE  ":".
            public static string CHELOC2_PRN6 = "129";           //  X(03)  VALUE  "129".
            public static string CHELOC2_PRN7 = ":";             //  X(01)  VALUE  ":".
            public static string CHELOC2_PRN8 = "KCS";           //  X(03)  VALUE  "KCS".
            public static string CHELOC2_PRN9 = "'";             //  X(01)  VALUE  "'".

            //*--------------------------(143)- 화주 ------------------*
            //NAD-PRN.
            public static string CHENAD_PRN; //As String * 143
            public static string CHENAD_PRN1 = "NAD";           //  X(03)  VALUE  "NAD".
            public static string CHENAD_PRN2 = "+";             //  X(01)  VALUE  "+".
            public static string CHENAD_PRN3 = "GO";            //  X(02)  VALUE  "GO".
            public static string CHENAD_PRN4 = "+";             //  X(01)  VALUE  "+".
            public static string CHENAD_PRN5 = "+";             //  X(01)  VALUE  "+".
            public static string CHENAD_PRN6 = "+";             //  X(01)  VALUE  "+".
            public static string CHENAD_0102;  //As String * 35         //NAD-0102      X(35).
            public static string CHENAD_PRN7 = "+";             //  X(01)  VALUE  "+".
            public static string CHENAD_0103;  //As String * 35         //NAD-0103      X(35).
            public static string CHENAD_PRN8 = ":";             //  X(01)  VALUE  "+".
            public static string CHENAD_0104;  //As String * 35         //NAD-0104      X(35).
            public static string CHENAD_PRN9 = "+";             //  X(01)  VALUE  "+".
            public static string CHENAD_PRN10 = "+";             //  X(01)  VALUE  "+".
            public static string CHENAD_PRN11 = "+";             //  X(01)  VALUE  "+".
            public static string CHENAD_0105;  //As String * 6           //NAD-0105      X(6).
            public static string CHENAD_PRN12 = "'";             //  X(01)  VALUE  "'".
            //*--------------------------(7)- 연락처 ------------------*
            //CTA-PRN.
            public static string CHECTA_PRN; //As String * 7
            public static string CHECTA_PRN1 = "CTA";           //  X(03)  VALUE  "CTA".
            public static string CHECTA_PRN2 = "+";             //  X(01)  VALUE  "+".
            public static string CHECTA_PRN3 = "AC";            //  X(02)  VALUE  "AC".
            public static string CHECTA_PRN4 = "'";             //  X(01)  VALUE  "'".
            //*--------------------------(22)- 화주전화번호 ------------------*
            //COM-PRN.
            public static string CHECOM_PRN; //As String * 22
            public static string CHECOM_PRN1 = "COM";           //  X(03)  VALUE  "COM".
            public static string CHECOM_PRN2 = "+";             //  X(01)  VALUE  "+".
            public static string CHECOM_0104;  //As String * 14         //NAD-0104      X(14).
            public static string CHECOM_PRN3 = ":";             //  X(01)  VALUE  ":".
            public static string CHECOM_PRN4 = "TE";            //  X(02)  VALUE  "TE".
            public static string CHECOM_PRN5 = "'";             //  X(01)  VALUE  "'".
            //*--------------------------(14)- 반입갯수 --------*
            //GID-PRN.
            public static string CHEGID_PRN; //As String * 14
            public static string CHEGID_PRN1 = "GID";            //  X(03)  VALUE  "GID".
            public static string CHEGID_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string CHEGID_PRN3 = "+";              //  X(01)  VALUE  "+".
            public static string CHEGID_0101;  //As String * 8           //GID-0101      Z(07)9.
            public static string CHEGID_PRN4 = "'";              //  X(01)  VALUE  "'".

            //*--------------------------(27)- 반입중량 ------------------*
            //MEA-PRN.
            public static string CHEMEA_PRN; //As String * 27
            public static string CHEMEA_PRN1 = "MEA";            //  X(03)  VALUE  "MEA".
            public static string CHEMEA_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string CHEMEA_PRN3 = "WT";             //  X(02)  VALUE  "WT".
            public static string CHEMEA_PRN4 = "+";              //  X(01)  VALUE  "+".
            public static string CHEMEA_PRN5 = "+";              //  X(01)  VALUE  "+".
            public static string CHEMEA_PRN6 = "KG";             //  X(02)  VALUE  "KG".
            public static string CHEMEA_PRN7 = ":";              //  X(01)  VALUE  ":".
            public static string CHEMEA_0202;  //As String * 15          //MEA-0202     Z(12)9.9.
            public static string CHEMEA_PRN8 = "'";              //  X(01)  VALUE  "'".
            //*--------------------------(26)-----------------------------*
            //*** 화물번호(년도) / 일련번호
            //UNT-PRN.
            public static string CHEUNT_PRN; //As String * 26
            public static string CHEUNT_PRN1 = "UNT";            //  X(03)  VALUE  "UNT".
            public static string CHEUNT_PRN2 = "+";              //  X(01)  VALUE  "+".
            public static string CHEUNT_0101;  //As String * 6           //UNT-0101      Z(05)9.
            public static string CHEUNT_PRN3 = "+";              //  X(01)  VALUE  "+".
            public static string CHEUNT_02011;  //As String * 4          //UNT-0201      X(04).
            public static string CHEUNT_02012;  //As String * 10         //UNT-0201      X(10).
            public static string CHEUNT_PRN4 = "  ";             //  X(02)  VALUE  "  ".
            public static string CHEUNT_PRN5 = "'";              //  X(01)  VALUE  "'".
            public static string CHEUNT_PRN6 = "#";              //  X(01)  VALUE  "#".
        
        #endregion

        #region Description : 반출입정정신고서
         //*-------------------------------------------------------------------------------------*
         //*     << 반출입 정정 EDI 항목 정의 >>                                                    *
         //*-------------------------------------------------------------------------------------*
          //UNH-PRN.
          //*---------------(전자문서헤더)-------------------------------------------*
           public static string REUNH_PRN; //As String * 29
           public static string  REUNH_PRN1 = "UNH";             //  X(03)    "UNH".
           public static string  REUNH_PRN2 = "+";               //  X(01)    "+".
           public static string REUNH_0101;  //As String * 8            //UNH-0101   X(06).
           public static string  REUNH_PRN3 = "+";               //  X(01)    "+".
           public static string  REUNH_PRN4 = "CUSDMR";         //  X(06)    "CUSDMR".
           public static string  REUNH_PRN5 = ":";               //  X(01)    ":".
           public static string  REUNH_PRN6 = "S";               //  X(01)    "S".
           public static string  REUNH_PRN7 = ":";               //  X(01)    ":".
           public static string  REUNH_PRN8 = "93A";             //  X(03)    "93A".
           public static string  REUNH_PRN9 = ":";               //  X(01)    ":".
           public static string  REUNH_PRN10 = "KE";             //  X(02)    "KE".
           public static string  REUNH_PRN11 = "'";              //  X(01)    "'".
   
          //*--------------------------(29)------------------*
          //*** 반출입일련번호 (장치장번호 / 년/ 일련번호)
          //BGM-PRN.
           public static string REBGM_PRN; //As String * 27
           public static string  REBGM_PRN1 = "BGM";             //  X(03)  VALUE  "BGM".
           public static string  REBGM_PRN2 = "+";               //  X(01)  VALUE  "+".
           public static string REBGM_0101;  //As String * 3           //BGM-02011           X(08).
           public static string  REBGM_PRN3 = "+";               //  X(01)  VALUE  "+".
           //05  REBGM-0201.
           public static string REBGM_02011;  //As String * 8           //BGM-02011           X(08).
           public static string REBGM_02012;  //As String * 2           //BGM-02012           X(02).
           public static string REBGM_02013;  //As String * 8           //BGM-02013           X(06).
           public static string  REBGM_PRN4 = "'";               //  X(01)  VALUE  "'".

           //*--------------------------(15)- 신청구분 ------------------*
           //GIS-PRN.
           public static string REGIS_PRN; //As String * 15
           public static string  REGIS_PRN1 = "GIS";             //  X(03)  VALUE  "GIS".
           public static string  REGIS_PRN2 = "+";               //  X(01)  VALUE  "+".
           public static string REGIS_0101;  //As String * 2            //GIS-0101   X(02).
           public static string  REGIS_PRN3 = ":";               //  X(01)  VALUE  ":".
           public static string  REGIS_PRN4 = "5AI";             //  X(03)  VALUE  "148".
           public static string  REGIS_PRN5 = ":";               //  X(01)  VALUE  ":".
           public static string  REGIS_PRN6 = "KCS";             //  X(03)  VALUE  "KCS".
           public static string  REGIS_PRN7 = "'";               //  X(01)  VALUE  "'".
   
           //*--------------------------(21)- 정정신청일자 ------------------*
          //DTM-PRN.
           public static string REDTM_PRN; //As String * 21
           public static string  REDTM_PRN1 = "DTM";             //  X(03)  VALUE  "DTM".
           public static string  REDTM_PRN2 = "+";               //  X(01)  VALUE  "+".
           public static string  REDTM_PRN3 = "141";             //  X(02)  VALUE  "261".
           public static string  REDTM_PRN4 = ":";               //  X(01)  VALUE  ":".
          //DTM-0102.
           public static string REDTM_01021;  //As String * 8           //DTM-01021           X(08).
           public static string  REDTM_PRN5 = ":";               //  X(01)  VALUE  ":".
           public static string  REDTM_PRN6 = "102";             //  X(03)  VALUE  "204".
           public static string  REDTM_PRN7 = "'";               //  X(01)  VALUE  "'".
   
           //*--------------------------(32)- 신고세관 ------------------*
           //*- 과코드변경（감시과－＞통관지원과）  33->10 (2000.1.10~)
           //LOC-PRN.
           public static string RELOC_PRN; //As String * 32
           public static string  RELOC_PRN1 = "LOC";           //  X(03)  VALUE  "LOC".
           public static string  RELOC_PRN2 = "+";             //  X(01)  VALUE  "+".
           public static string  RELOC_PRN3 = "41";            //  X(02)  VALUE  "41".
           public static string  RELOC_PRN4 = "+";             //  X(03)  VALUE  "+".
           public static string  RELOC_PRN5 = "110";           //  X(03)  VALUE  "016".
           public static string  RELOC_PRN6 = ":";             //  X(01)  VALUE  ":".
           public static string  RELOC_PRN7 = "113";           //  X(03)  VALUE  "113".
           public static string  RELOC_PRN8 = ":";             //  X(01)  VALUE  ":".
           public static string  RELOC_PRN9 = "KCS";           //  X(03)  VALUE  "KCS".
           public static string  RELOC_PRN10 = "+";             //  X(01)  VALUE  "+".
           public static string  RELOC_PRN11 = "10";            //  X(02)  VALUE  "10".
           public static string  RELOC_PRN12 = ":";             //  X(01)  VALUE  ":".
           public static string  RELOC_PRN13 = "5AB";           //  X(03)  VALUE  "5AB".
           public static string  RELOC_PRN14 = ":";             //  X(01)  VALUE  ":".
           public static string  RELOC_PRN15 = "KCS";           //  X(03)  VALUE  "KCS".
           public static string  RELOC_PRN16 = "'";             //  X(01)  VALUE  "'".
   
           //*--------------------------(28)- 수량기재 --------------*
           public static string REQTY_PRN; //As String * 10
           public static string  REQTY_PRN1 = "QTY";
           public static string  REQTY_PRN2 = "+";
           public static string  REQTY_PRN3 = "5BB";
           public static string  REQTY_PRN4 = ":";
           public static string REQTY_0101;  //As String * 1
           public static string  REQTY_PRN5 = "'";
   
           //*--------------------------(14)- 정정사유기재 --------------*
           //FTX-PRN.
           public static string REFTX_PRN; //As String * 61
           public static string  REFTX_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
           public static string  REFTX_PRN2 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX_PRN3 = "ACD";            //   X(03)  VALUE  "ACD".
           public static string  REFTX_PRN4 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX_PRN5 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX_PRN6 = "+";              //   X(01)  VALUE  "+".
           public static string REFTX_0201;  //As String * 50          // FTX-0201        X(03).
           public static string  REFTX_PRN7 = "'";              //   X(01)  VALUE  "'".
   
   
           //*--------------------------신청자 기재 ------------------*
           //NAD-PRN.
           public static string RENAD_PRN; //As String * 109
           public static string  RENAD_PRN1 = "NAD";           //  X(03)  VALUE  "NAD".
           public static string  RENAD_PRN2 = "+";             //  X(01)  VALUE  "+".
           public static string  RENAD_PRN3 = "DT";            //  X(02)  VALUE  "GO".
           public static string  RENAD_PRN4 = "+";             //  X(01)  VALUE  "+".
           public static string  RENAD_PRN5 = "+";             //  X(01)  VALUE  "+".
           public static string RENAD_0102;  //As String * 100         //NAD-0102      X(35).
           public static string  RENAD_PRN6 = "'";             //  X(01)  VALUE  "'".
   
           //*-------------------------- 화물관리번호 --------------*
           //       ***    적하목록관리번호
           //       ***   MSN   /    HSN
           //RFF-PRN.
           public static string RERFF_PRN; //As String * 29
           public static string  RERFF_PRN1 = "RFF";            //  X(03)  VALUE  "RFF".
           public static string  RERFF_PRN2 = "+";             //  X(01)  VALUE  "+".
           public static string  RERFF_PRN3 = "XC";             //  X(02)  VALUE  "XC".
           public static string  RERFF_PRN4 = ":";              //  X(01)  VALUE  ":".
           public static string RERFF_0102;  //As String * 11          //RFF-0102     X(11).
           public static string  RERFF_PRN5 = ":";              //  X(01)  VALUE  ":".
           public static string RERFF_0103;  //As String * 4           //RFF-0103     X(04).
           public static string  RERFF_PRN6 = ":";              //  X(01)  VALUE  ":".
           public static string RERFF_0104;  //As String * 4           //RFF-0104     X(03).
           public static string  RERFF_PRN7 = "'";              //X(01)  VALUE  "'".
   
   
           //*---------------------정정신청할 반출입신고내역의 항목번호 --------------*
           public static string RELIN_PRN; //As String * 8
           public static string  RELIN_PRN1 = "LIN";            //  X(03)  VALUE  "RFF".
           public static string  RELIN_PRN2 = "+";              //  X(01)  VALUE  "+".
           public static string  RELIN_PRN3 = "+";              //  X(01)  VALUE  "+".
           public static string  RELIN_PRN4 = "+";              //  X(01)  VALUE  "+".
           public static string RELIN_0101;  //As String * 1           // RFF-0102     X(11).
           public static string  RELIN_PRN5 = "'";              //X(01)  VALUE  "'".
   
           //*--------------------------정정전내용및정정후내용 기재 --------------*
           //FTX1-PRN.
           public static string REFTX1_PRN; //As String * 61
           public static string  REFTX1_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
           public static string  REFTX1_PRN2 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX1_PRN3 = "AAO";            //   X(03)  VALUE  "ACD".
           public static string  REFTX1_PRN4 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX1_PRN5 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX1_PRN6 = "+";              //   X(01)  VALUE  "+".
           public static string REFTX1_0101;  //As String * 50          // FTX-0201        X(03).
           public static string  REFTX1_PRN7 = "'";              //   X(01)  VALUE  "'".
   
           //FTX1-PRN.
           public static string REFTX2_PRN; //As String * 61
           public static string  REFTX2_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
           public static string  REFTX2_PRN2 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX2_PRN3 = "CHG";            //   X(03)  VALUE  "ACD".
           public static string  REFTX2_PRN4 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX2_PRN5 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX2_PRN6 = "+";              //   X(01)  VALUE  "+".
           public static string REFTX2_0101;  //As String * 50          // FTX-0201        X(03).
           public static string  REFTX2_PRN7 = "'";              //   X(01)  VALUE  "'".
   
   
           //<< 정정신고시 반입개수(G) 반입중량(H) 일경우 항목추가 사항 START>>
           //*---------------------정정신청할 반출입신고내역의 항목번호 추가(1) --------------*
           public static string RELIN3_PRN; //As String * 8
           public static string  RELIN3_PRN1 = "LIN";            //  X(03)  VALUE  "RFF".
           public static string  RELIN3_PRN2 = "+";              //  X(01)  VALUE  "+".
           public static string  RELIN3_PRN3 = "+";              //  X(01)  VALUE  "+".
           public static string  RELIN3_PRN4 = "+";              //  X(01)  VALUE  "+".
           public static string RELIN3_0101;  //As String * 1          //RFF-0102     X(11).
           public static string  RELIN3_PRN5 = "'";              //X(01)  VALUE  "'".
   
           //*--------------------------정정전내용및정정후내용 기재 추가(1) --------------*
           //FTX1-PRN.
           public static string REFTX4_PRN; //As String * 61
           public static string  REFTX4_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
           public static string  REFTX4_PRN2 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX4_PRN3 = "AAO";            //   X(03)  VALUE  "ACD".
           public static string  REFTX4_PRN4 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX4_PRN5 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX4_PRN6 = "+";              //   X(01)  VALUE  "+".
           public static string REFTX4_0101;  //As String * 50          // FTX-0201        X(03).
           public static string  REFTX4_PRN7 = "'";              //   X(01)  VALUE  "'".
   
           //FTX1-PRN.
           public static string REFTX5_PRN; //As String * 61
           public static string  REFTX5_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
           public static string  REFTX5_PRN2 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX5_PRN3 = "CHG";            //   X(03)  VALUE  "ACD".
           public static string  REFTX5_PRN4 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX5_PRN5 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX5_PRN6 = "+";              //   X(01)  VALUE  "+".
           public static string REFTX5_0101;  //As String * 50          // FTX-0201        X(03).
           public static string  REFTX5_PRN7 = "'";              //   X(01)  VALUE  "'".
   
           //*---------------------정정신청할 반출입신고내역의 항목번호 추가(1) --------------*
           public static string RELIN6_PRN; //As String * 8
           public static string  RELIN6_PRN1 = "LIN";            //  X(03)  VALUE  "RFF".
           public static string  RELIN6_PRN2 = "+";             //  X(01)  VALUE  "+".
           public static string  RELIN6_PRN3 = "+";             //  X(01)  VALUE  "+".
           public static string  RELIN6_PRN4 = "+";              //  X(01)  VALUE  "+".
           public static string RELIN6_0101;  //As String * 1          //RFF-0102     X(11).
           public static string  RELIN6_PRN5 = "'";              //X(01)  VALUE  "'".
   
           //*--------------------------정정전내용및정정후내용 기재 추가(1) --------------*
           //FTX1-PRN.
           public static string REFTX7_PRN; //As String * 61
           public static string  REFTX7_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
           public static string  REFTX7_PRN2 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX7_PRN3 = "AAO";            //   X(03)  VALUE  "ACD".
           public static string  REFTX7_PRN4 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX7_PRN5 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX7_PRN6 = "+";              //   X(01)  VALUE  "+".
           public static string REFTX7_0101;  //As String * 50          // FTX-0201        X(03).
           public static string  REFTX7_PRN7 = "'";              //   X(01)  VALUE  "'".
   
           //FTX1-PRN.
           public static string REFTX8_PRN; //As String * 61
           public static string  REFTX8_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
           public static string  REFTX8_PRN2 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX8_PRN3 = "CHG";            //   X(03)  VALUE  "ACD".
           public static string  REFTX8_PRN4 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX8_PRN5 = "+";              //   X(01)  VALUE  "+".
           public static string  REFTX8_PRN6 = "+";              //   X(01)  VALUE  "+".
           public static string REFTX8_0101;  //As String * 50          // FTX-0201        X(03).
           public static string  REFTX8_PRN7 = "'";              //   X(01)  VALUE  "'".
           //---<< 정정신고시 반입개수(G) 반입중량(H) 일경우 항목추가 END >>--------------//
   
   
           //*----------------전자문서내 세부사항부분과요약부분 --------------*
           public static string REUNS_PRN; //As String * 6
           public static string  REUNS_PRN1 = "UNS";            //   X(03)  VALUE  "FTX".
           public static string  REUNS_PRN2 = "+";              //   X(01)  VALUE  "+".
           public static string  REUNS_PRN3 = "S";            //   X(03)  VALUE  "ACD".
           public static string  REUNS_PRN4 = "'";              //   X(01)  VALUE  "'".
  
          //*-----------------전자문서의종료를나타내는전송항목-----------------------------*
           //UNT-PRN.
           public static string REUNT_PRN; //As String * 20
           public static string  REUNT_PRN1 = "UNT";            //  X(03)  VALUE  "UNT".
           public static string  REUNT_PRN2 = "+";              //  X(01)  VALUE  "+".
           public static string REUNT_0101;  //As String * 6           //UNT-0101      Z(05)9.
           public static string  REUNT_PRN3 = "+";              //  X(01)  VALUE  "+".
           public static string REUNT_0102;  //As String * 8           //UNT-0201      X(04).
           public static string REUNT_PRN4 = "'";              //  X(01)  VALUE  "'".
   
        #endregion

        #region Description : 내국화물반입신고서
            //*-------------------------------------------------------------------------------------*
            //*     << 내국반입내역 EDI 항목 정의 >>                                                    *
            //*-------------------------------------------------------------------------------------*
              //UNH-PRN.
              //*---------------(전자문서 참조번호(29))-------------------------------------------*
               public static string BRUNH_PRN;  //As String * 29
               public static string  BRUNH_PRN1 = "UNH";    //  X(03)    "UNH".
               public static string  BRUNH_PRN2 = "+";      //  X(01)    "+".
               public static string BRUNH_0101;  //As String * 8    //UNH-0101   X(08).
               public static string  BRUNH_PRN3 = "+";      //  X(01)    "+".
               public static string  BRUNH_PRN4 = "CUSWBR"; //  X(06)    "CUSWBR".
               public static string  BRUNH_PRN5 = ":";      //  X(01)    ":".
               public static string  BRUNH_PRN6 = "S";      //  X(01)    "S".
               public static string  BRUNH_PRN7 = ":";      //  X(01)    ":".
               public static string  BRUNH_PRN8 = "93A";    //  X(03)    "93A".
               public static string  BRUNH_PRN9 = ":";      //  X(01)    ":".
               public static string  BRUNH_PRN10 = "KE";     //  X(02)    "KE".
               public static string  BRUNH_PRN11 = "'";      //  X(01)    "'".
              //*--------------------------(29)- 신고번호 ------------------*
              //       ***    장치장번호
              //       ***    화물관리번호 1  ----( 반입일련번호와　같은　역활 )
              //       ***    화물관리번호 2  __|
              // BGM-PRN.
               public static string BRBGM_PRN; //As String * 30
               public static string  BRBGM_PRN1 = "BGM";    //  X(03)  VALUE  "BGM".
               public static string  BRBGM_PRN2 = "+";      //  X(01)  VALUE  "+".
               public static string  BRBGM_PRN3 = "5HA";    //  X(03)  VALUE  "5HA".
               public static string  BRBGM_PRN4 = "+";      //  X(01)  VALUE  "+".
               //05  BGM-0201.
               public static string BRBGM_02011;  //As String * 8  //BGM-02011           X(08).
               public static string BRBGM_02012;  //As String * 2  //BGM-02012           X(02).
               public static string BRBGM_02013;  //As String * 8  //BGM-02013           X(06).
               public static string  BRBGM_PRN5 = "+";      //  X(01)  VALUE  "+".
               public static string BRBGM_0202;  //As String * 2   //BGM-0202       X(02).
               public static string  BRBGM_PRN6 = "'";      //  X(01)  VALUE  "'".
   
               //*--------------------------(14)- 보세구역구분 ------------------*
               // A - 자율관리보세구역
               // B - 비자율관리보세구역
               //GIS-PRN.
               public static string BRGIS_PRN; //As String * 14
               public static string  BRGIS_PRN1 = "GIS";            //  X(03)  VALUE  "GIS".
               public static string  BRGIS_PRN2 = "+";             //  X(01)  VALUE  "+".
               public static string BRGIS_0101;  //As String * 1           //GIS-0101                X(02).
               public static string  BRGIS_PRN3 = ":";              //  X(01)  VALUE  ":".
               public static string  BRGIS_PRN4 = "129";            //  X(03)  VALUE  "5CA".
               public static string  BRGIS_PRN5 = ":";              //  X(01)  VALUE  ":".
               public static string  BRGIS_PRN6 = "KCS";            //  X(03)  VALUE  "KCS".
               public static string  BRGIS_PRN7 = "'";              //  X(01)  VALUE  "'".
   
               //*--------------------------(61)- 반입신고건수 --------------*
               //PAC-PRN.
               public static string BRPAC_PRN; //As String * 8
               public static string  BRPAC_PRN1 = "PAC";
               public static string  BRPAC_PRN2 = "+";
               public static string BRPAC_0201;  //As String * 3
               public static string  BRPAC_PRN3 = "'";
   
              //*--------------------------(29)- 반입신고번호 ------------------*
              //       ***    장치장번호
              //       ***    화물관리번호 1  ----( 반입일련번호와　같은　역활 )
              //       ***    화물관리번호 2  __|
              // CST-PRN.
               public static string BRCST_PRN; //As String * 32
               public static string  BRCST_PRN1 = "CST";
               public static string  BRCST_PRN2 = "+";
               public static string  BRCST_PRN3 = "+";
               //05  CST-0201.
               public static string BRCST_02011;  //As String * 8
               public static string BRCST_02012;  //As String * 2
               public static string BRCST_02013;  //As String * 8
               public static string  BRCST_PRN4 = ":";
               public static string  BRCST_PRN5 = "116";
               public static string  BRCST_PRN6 = ":";
               public static string  BRCST_PRN7 = "KCS";
               public static string  BRCST_PRN8 = "'";
   
               //*--------------------------(61)- 품명 --------------*
               //FTX-PRN.
               public static string BRFTX_PRN1; //As String * 61
               public static string  BRFTX1_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
               public static string  BRFTX1_PRN2 = "+";              //   X(01)  VALUE  "+".
               public static string  BRFTX1_PRN3 = "AAA";            //   X(03)  VALUE  "ACD".
               public static string  BRFTX1_PRN4 = "+";              //   X(01)  VALUE  "+".
               public static string  BRFTX1_PRN5 = "+";              //   X(01)  VALUE  "+".
               public static string  BRFTX1_PRN6 = "+";              //   X(01)  VALUE  "+".
               public static string BRFTX1_0201;  //As String * 50          // FTX-0201        X(03).
               public static string  BRFTX1_PRN7 = "'";              //   X(01)  VALUE  "'".
   
               //*--------------------------(61)- 장치장위치 --------------*
               //FTX-PRN.
               public static string BRFTX_PRN2; //As String * 61
               public static string  BRFTX2_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
               public static string  BRFTX2_PRN2 = "+";              //   X(01)  VALUE  "+".
               public static string  BRFTX2_PRN3 = "AGW";            //   X(03)  VALUE  "ACD".
               public static string  BRFTX2_PRN4 = "+";              //   X(01)  VALUE  "+".
               public static string  BRFTX2_PRN5 = "+";              //   X(01)  VALUE  "+".
               public static string  BRFTX2_PRN6 = "+";              //   X(01)  VALUE  "+".
               public static string BRFTX2_0201;  //As String * 50          // FTX-0201        X(03).
               public static string  BRFTX2_PRN7 = "'";              //   X(01)  VALUE  "'".
   
               //*--------------------------(61)- 장치사유 --------------*
               //FTX-PRN.
               public static string BRFTX_PRN3; //As String * 61
               public static string  BRFTX3_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
               public static string  BRFTX3_PRN2 = "+";              //   X(01)  VALUE  "+".
               public static string  BRFTX3_PRN3 = "ACD";            //   X(03)  VALUE  "ACD".
               public static string  BRFTX3_PRN4 = "+";              //   X(01)  VALUE  "+".
               public static string  BRFTX3_PRN5 = "+";              //   X(01)  VALUE  "+".
               public static string  BRFTX3_PRN6 = "+";              //   X(01)  VALUE  "+".
               public static string BRFTX3_0201;  //As String * 50          // FTX-0201        X(03).
               public static string  BRFTX3_PRN7 = "'";              //   X(01)  VALUE  "'".
   
               //*--------------------------(32)- 원산지 ------------------*
               //LOC-PRN.
               public static string BRLOC_PRN; //As String * 22
               public static string  BRLOC_PRN1 = "LOC";           //  X(03)  VALUE  "LOC".
               public static string  BRLOC_PRN2 = "+";             //  X(01)  VALUE  "+".
               public static string  BRLOC_PRN3 = "106";           //  X(03)  VALUE  "5AB".
               public static string  BRLOC_PRN4 = "+";             //  X(01)  VALUE  "+".
               public static string BRLOC_0201;  //As String * 5          // FTX-0201        X(03).
               public static string  BRLOC_PRN5 = ":";             //  X(01)  VALUE  ":".
               public static string  BRLOC_PRN6 = "156";           //  X(03)  VALUE  "5AB".
               public static string  BRLOC_PRN7 = ":";             //  X(01)  VALUE  ":".
               public static string  BRLOC_PRN8 = "KCS";           //  X(03)  VALUE  "KCS".
               public static string  BRLOC_PRN9 = "'";             //  X(01)  VALUE  "'".
   
               //*--------------------------(20)- 반입일시 ------------------*
              //DTM-PRN.
               public static string BRDTM1_PRN; //As String * 20
               public static string  BRDTM1_PRN1 = "DTM";    //  X(03)  VALUE  "DTM".
               public static string  BRDTM1_PRN2 = "+";      //  X(01)  VALUE  "+".
               public static string  BRDTM1_PRN3 = "50";     //  X(02)  VALUE  "50".
               public static string  BRDTM1_PRN4 = ":";      //  X(01)  VALUE  ":".
              //DTM-0102.
               public static string BRDTM1_0102;  //As String * 8  //DTM-01021           X(08).
               public static string  BRDTM1_PRN5 = ":";      //  X(01)  VALUE  ":".
               public static string  BRDTM1_PRN6 = "102";    //  X(03)  VALUE  "204".
               public static string  BRDTM1_PRN7 = "'";      //  X(01)  VALUE  "'".
   
               //*--------------------------(20)- 장치종료일 ------------------*
              //DTM-PRN.
               public static string  BRDTM2_PRN; //As String * 21
               public static string  BRDTM2_PRN1 = "DTM";    //  X(03)  VALUE  "DTM".
               public static string  BRDTM2_PRN2 = "+";      //  X(01)  VALUE  "+".
               public static string  BRDTM2_PRN3 = "434";     //  X(02)  VALUE  "50".
               public static string  BRDTM2_PRN4 = ":";      //  X(01)  VALUE  ":".
              //DTM-0102.
               public static string BRDTM2_0102;  //As String * 8  //DTM-01021           X(08).
               public static string  BRDTM2_PRN5 = ":";      //  X(01)  VALUE  ":".
               public static string  BRDTM2_PRN6 = "102";    //  X(03)  VALUE  "204".
               public static string  BRDTM2_PRN7 = "'";      //  X(01)  VALUE  "'".
   
               //*--------------------------(20)- 반입개수 ------------------*
               //MEA-PRN.
               public static string BRMEA1_PRN; //As String * 22
               public static string  BRMEA1_PRN1 = "MEA";            //  X(03)  VALUE  "MEA".
               public static string  BRMEA1_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string  BRMEA1_PRN3 = "CT";             //  X(02)  VALUE  "WT".
               public static string  BRMEA1_PRN4 = "+";              //  X(01)  VALUE  "+".
               public static string  BRMEA1_PRN5 = "+";              //  X(01)  VALUE  "+".
               public static string  BRMEA1_PRN6 = "CT";             //  X(02)  VALUE  "KG".
               public static string  BRMEA1_PRN7 = ":";              //  X(01)  VALUE  ":".
               public static string BRMEA1_0202;  //As String * 10           //MEA-0202     Z(12)9.9.
               public static string  BRMEA1_PRN8 = "'";              //  X(01)  VALUE  "'".
   
               //*--------------------------(20)- 반입중량 ------------------*
               //MEA-PRN.
               public static string BRMEA2_PRN; //As String * 28
               public static string  BRMEA2_PRN1 = "MEA";            //  X(03)  VALUE  "MEA".
               public static string  BRMEA2_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string  BRMEA2_PRN3 = "WT";             //  X(02)  VALUE  "WT".
               public static string  BRMEA2_PRN4 = "+";              //  X(01)  VALUE  "+".
               public static string  BRMEA2_PRN5 = "+";              //  X(01)  VALUE  "+".
               public static string  BRMEA2_PRN6 = "KG";             //  X(02)  VALUE  "KG".
               public static string  BRMEA2_PRN7 = ":";              //  X(01)  VALUE  ":".
               public static string BRMEA2_0202;  //As String * 16          //MEA-0202     Z(12)9.9.
               public static string  BRMEA2_PRN8 = "'";              //  X(01)  VALUE  "'".
   
               //*--------------------------(86)- 화주(1) ------------------*
               //NAD-PRN.
               public static string BRNAD_PRN; //As String * 253
               public static string  BRNAD_PRN1 = "NAD";            //  X(03)  VALUE  "MEA".
               public static string  BRNAD_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string  BRNAD_PRN3 = "GO";             //  X(02)  VALUE  "WT".
               public static string  BRNAD_PRN4 = "+";              //  X(01)  VALUE  "+".
               public static string  BRNAD_PRN5 = "+";              //  X(01)  VALUE  "+".
               public static string BRNAD_0201;  //As String * 100         //MEA-0202     Z(12)9.9.
               public static string  BRNAD_PRN6 = "+";              //  X(01)  VALUE  ":".
               public static string BRNAD_0202;  //As String * 100         //MEA-0202     Z(12)9.9.
               public static string  BRNAD_PRN7 = "+";              //  X(01)  VALUE  ":".
               public static string BRNAD_0203;  //As String * 42          //MEA-0202     Z(12)9.9.
               public static string  BRNAD_PRN8 = "'";              //  X(01)  VALUE  "'".
   
   
               //*--------------------------(86)- 화주(2) ------------------*
               //NAD-PRN.
               public static string BRNAD1_PRN; //As String * 310
               public static string  BRNAD1_PRN1 = "NAD";            //  X(03)  VALUE  "MEA".
               public static string  BRNAD1_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string  BRNAD1_PRN3 = "CT";             //  X(02)  VALUE  "WT".
               public static string  BRNAD1_PRN4 = "+";              //  X(01)  VALUE  "+".
               public static string  BRNAD1_PRN5 = "+";              //  X(01)  VALUE  "+".
               public static string BRNAD1_0201;  //As String * 150         //MEA-0202     Z(12)9.9.
               public static string  BRNAD1_PRN6 = "+";              //  X(01)  VALUE  ":".
               public static string BRNAD1_0202;  //As String * 150         //MEA-0202     Z(12)9.9.
               public static string  BRNAD1_PRN7 = "'";              //  X(01)  VALUE  "'".
   
   
               //*--------------------------(29)- 반입물품종류부호 --------------*
               //RFF2-PRN.
               public static string BRRFF_PRN; //As String * 10
               public static string  BRRFF_PRN1 = "RFF";            //  X(03)  VALUE  "RFF".
               public static string  BRRFF_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string  BRRFF_PRN3 = "ACD";            //  X(03)  VALUE  "ABP".
               public static string  BRRFF_PRN4 = ":";              //  X(01)  VALUE  ":".
               public static string BRRFF_0101;  //As String * 1           //RFF2-0101     X(20).
               public static string  BRRFF_PRN5 = "'";              //  X(01)  VALUE  "'".
   
   
               //UNT-PRN.
               public static string BRUNT_PRN; //As String * 21
               public static string  BRUNT_PRN1 = "UNT";            //  X(03)  VALUE  "UNT".
               public static string  BRUNT_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string BRUNT_0101;  //As String * 6        //UNT-0101      Z(05)9.
               public static string  BRUNT_PRN3 = "+";              //  X(01)  VALUE  "+".
               public static string BRUNT_0201;  //As String * 8         //UNT-0201      X(08).
               public static string  BRUNT_PRN4 = "'";              //  X(01)  VALUE  "'".
               public static string BRUNT_PRN5 = "#";              //  X(01)  VALUE  "#".
              //*------------------------------------------------------------------------------------*

        #endregion

        #region Description : 내국화물반출신고서
            //*-------------------------------------------------------------------------------------*
            //*     << 내국반출내역 EDI 항목 정의 >>                                                    *
            //*-------------------------------------------------------------------------------------*
              //UNH-PRN.
              //*---------------(전자문서 참조번호(29))-------------------------------------------*
               public static string BRCHUNH_PRN; //As String * 29
               public static string  BRCHUNH_PRN1 = "UNH";    //  X(03)    "UNH".
               public static string  BRCHUNH_PRN2 = "+";      //  X(01)    "+".
               public static string BRCHUNH_0101;  //As String * 8    //UNH-0101   X(08).
               public static string  BRCHUNH_PRN3 = "+";      //  X(01)    "+".
               public static string  BRCHUNH_PRN4 = "CUSWBR"; //  X(06)    "CUSWBR".
               public static string  BRCHUNH_PRN5 = ":";      //  X(01)    ":".
               public static string  BRCHUNH_PRN6 = "S";      //  X(01)    "S".
               public static string  BRCHUNH_PRN7 = ":";      //  X(01)    ":".
               public static string  BRCHUNH_PRN8 = "93A";    //  X(03)    "93A".
               public static string  BRCHUNH_PRN9 = ":";      //  X(01)    ":".
               public static string  BRCHUNH_PRN10 = "KE";     //  X(02)    "KE".
               public static string  BRCHUNH_PRN11 = "'";      //  X(01)    "'".
              //*--------------------------(29)- 신고번호 ------------------*
              //       ***    장치장번호
              //       ***    화물관리번호 1  ----( 반입일련번호와　같은　역활 )
              //       ***    화물관리번호 2  __|
              // BGM-PRN.
               public static string BRCHBGM_PRN; //As String * 30
               public static string  BRCHBGM_PRN1 = "BGM";    //  X(03)  VALUE  "BGM".
               public static string  BRCHBGM_PRN2 = "+";      //  X(01)  VALUE  "+".
               public static string  BRCHBGM_PRN3 = "5HB";    //  X(03)  VALUE  "5HA".
               public static string  BRCHBGM_PRN4 = "+";      //  X(01)  VALUE  "+".
               //05  BGM-0201.
               public static string BRCHBGM_02011;  //As String * 8  //BGM-02011           X(08).
               public static string BRCHBGM_02012;  //As String * 2  //BGM-02012           X(02).
               public static string BRCHBGM_02013;  //As String * 8  //BGM-02013           X(06).
               public static string  BRCHBGM_PRN5 = "+";      //  X(01)  VALUE  "+".
               public static string BRCHBGM_0202;  //As String * 2   //BGM-0202       X(02).
               public static string  BRCHBGM_PRN6 = "'";      //  X(01)  VALUE  "'".
   
               //*--------------------------(14)- 보세구역구분 ------------------*
               // A - 자율관리보세구역
               // B - 비자율관리보세구역
               //GIS-PRN.
               public static string BRCHGIS_PRN; //As String * 14
               public static string  BRCHGIS_PRN1 = "GIS";            //  X(03)  VALUE  "GIS".
               public static string  BRCHGIS_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string BRCHGIS_0101;  //As String * 1           //GIS-0101                X(02).
               public static string  BRCHGIS_PRN3 = ":";              //  X(01)  VALUE  ":".
               public static string  BRCHGIS_PRN4 = "129";            //  X(03)  VALUE  "5CA".
               public static string  BRCHGIS_PRN5 = ":";              //  X(01)  VALUE  ":".
               public static string  BRCHGIS_PRN6 = "KCS";            //  X(03)  VALUE  "KCS".
               public static string  BRCHGIS_PRN7 = "'";              //  X(01)  VALUE  "'".
   
               //*--------------------------(61)- 반출신고건수 --------------*
               //PAC-PRN.
               public static string BRCHPAC_PRN; //As String * 8
               public static string  BRCHPAC_PRN1 = "PAC";
               public static string  BRCHPAC_PRN2 = "+";
               public static string BRCHPAC_0201;  //As String * 3
               public static string  BRCHPAC_PRN3 = "'";
   
              //*--------------------------(29)- 반출신고번호 ------------------*
              //       ***    장치장번호
              //       ***    화물관리번호 1  ----( 반입일련번호와　같은　역활 )
              //       ***    화물관리번호 2  __|
              // CST-PRN.
               public static string BRCHCST_PRN; //As String * 32
               public static string  BRCHCST_PRN1 = "CST";
               public static string  BRCHCST_PRN2 = "+";
               public static string  BRCHCST_PRN3 = "+";
               //05  CST-0201.
               public static string BRCHCST_02011;  //As String * 8
               public static string BRCHCST_02012;  //As String * 2
               public static string BRCHCST_02013;  //As String * 8
               public static string  BRCHCST_PRN4 = ":";
               public static string  BRCHCST_PRN5 = "116";
               public static string  BRCHCST_PRN6 = ":";
               public static string  BRCHCST_PRN7 = "KCS";
               public static string  BRCHCST_PRN8 = "'";
   
               //*--------------------------(61)- 품명 --------------*
               //FTX-PRN.
               public static string BRCHFTX_PRN1; //As String * 61
               public static string  BRCHFTX1_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
               public static string  BRCHFTX1_PRN2 = "+";              //   X(01)  VALUE  "+".
               public static string  BRCHFTX1_PRN3 = "AAA";            //   X(03)  VALUE  "ACD".
               public static string  BRCHFTX1_PRN4 = "+";              //   X(01)  VALUE  "+".
               public static string  BRCHFTX1_PRN5 = "+";              //   X(01)  VALUE  "+".
               public static string  BRCHFTX1_PRN6 = "+";              //   X(01)  VALUE  "+".
               public static string BRCHFTX1_0201;  //As String * 50          // FTX-0201        X(03).
               public static string  BRCHFTX1_PRN7 = "'";              //   X(01)  VALUE  "'".
   
 
               //*--------------------------(61)- 장치사유 --------------*
               //FTX-PRN.
               public static string BRCHFTX_PRN3; //As String * 61
               public static string  BRCHFTX3_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
               public static string  BRCHFTX3_PRN2 = "+";              //   X(01)  VALUE  "+".
               public static string  BRCHFTX3_PRN3 = "ACD";            //   X(03)  VALUE  "ACD".
               public static string  BRCHFTX3_PRN4 = "+";              //   X(01)  VALUE  "+".
               public static string  BRCHFTX3_PRN5 = "+";              //   X(01)  VALUE  "+".
               public static string  BRCHFTX3_PRN6 = "+";              //   X(01)  VALUE  "+".
               public static string BRCHFTX3_0201;  //As String * 50          // FTX-0201        X(03).
               public static string  BRCHFTX3_PRN7 = "'";              //   X(01)  VALUE  "'".
   
  
               //*--------------------------(20)- 반출일시 ------------------*
              //DTM-PRN.
               public static string BRCHDTM1_PRN; //As String * 21
               public static string  BRCHDTM1_PRN1 = "DTM";    //  X(03)  VALUE  "DTM".
               public static string  BRCHDTM1_PRN2 = "+";      //  X(01)  VALUE  "+".
               public static string  BRCHDTM1_PRN3 = "261";     //  X(02)  VALUE  "50".
               public static string  BRCHDTM1_PRN4 = ":";      //  X(01)  VALUE  ":".
              //DTM-0102.
               public static string BRCHDTM1_0102;  //As String * 8  //DTM-01021           X(08).
               public static string  BRCHDTM1_PRN5 = ":";      //  X(01)  VALUE  ":".
               public static string  BRCHDTM1_PRN6 = "102";    //  X(03)  VALUE  "204".
               public static string  BRCHDTM1_PRN7 = "'";      //  X(01)  VALUE  "'".
   
               //*--------------------------(20)- 반출개수 ------------------*
               //MEA-PRN.
               public static string BRCHMEA1_PRN; //As String * 23
               public static string  BRCHMEA1_PRN1 = "MEA";            //  X(03)  VALUE  "MEA".
               public static string  BRCHMEA1_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string  BRCHMEA1_PRN3 = "CT";             //  X(02)  VALUE  "WT".
               public static string  BRCHMEA1_PRN4 = "+";              //  X(01)  VALUE  "+".
               public static string  BRCHMEA1_PRN5 = "+";             //  X(01)  VALUE  "+".
               public static string  BRCHMEA1_PRN6 = "NMB";             //  X(02)  VALUE  "KG".
               public static string  BRCHMEA1_PRN7 = ":";              //  X(01)  VALUE  ":".
               public static string BRCHMEA1_0202;  //As String * 10           //MEA-0202     Z(12)9.9.
               public static string  BRCHMEA1_PRN8 = "'";              //  X(01)  VALUE  "'".
   
               //*--------------------------(20)- 반출중량 ------------------*
               //MEA-PRN.
               public static string BRCHMEA2_PRN; //As String * 28
               public static string  BRCHMEA2_PRN1 = "MEA";            //  X(03)  VALUE  "MEA".
               public static string  BRCHMEA2_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string  BRCHMEA2_PRN3 = "WT";             //  X(02)  VALUE  "WT".
               public static string  BRCHMEA2_PRN4 = "+";              //  X(01)  VALUE  "+".
               public static string  BRCHMEA2_PRN5 = "+";              //  X(01)  VALUE  "+".
               public static string  BRCHMEA2_PRN6 = "KG";             //  X(02)  VALUE  "KG".
               public static string  BRCHMEA2_PRN7 = ":";              //  X(01)  VALUE  ":".
               public static string BRCHMEA2_0202;  //As String * 16          //MEA-0202     Z(12)9.9.
               public static string  BRCHMEA2_PRN8 = "'";              //  X(01)  VALUE  "'".
   
               //*--------------------------(86)- 화주(1)------------------*
               //NAD-PRN.
               public static string BRCHNAD_PRN; //As String * 253
               public static string  BRCHNAD_PRN1 = "NAD";            //  X(03)  VALUE  "MEA".
               public static string  BRCHNAD_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string  BRCHNAD_PRN3 = "GO";             //  X(02)  VALUE  "WT".
               public static string  BRCHNAD_PRN4 = "+";              //  X(01)  VALUE  "+".
               public static string  BRCHNAD_PRN5 = "+";              //  X(01)  VALUE  "+".
               public static string BRCHNAD_0201;  //As String * 100          //MEA-0202     Z(12)9.9.
               public static string  BRCHNAD_PRN6 = "+";              //  X(01)  VALUE  ":".
               public static string BRCHNAD_0202;  //As String * 100          //MEA-0202     Z(12)9.9.
               public static string  BRCHNAD_PRN7 = "+";              //  X(01)  VALUE  ":".
               public static string BRCHNAD_0203;  //As String * 42          //MEA-0202     Z(12)9.9.
               public static string  BRCHNAD_PRN8 = "'";              //  X(01)  VALUE  "'".
   
   
               //*--------------------------(86)- 화주(2) ------------------*
               //NAD-PRN.
               public static string BRCHNAD1_PRN; //As String * 310
               public static string  BRCHNAD1_PRN1 = "NAD";            //  X(03)  VALUE  "MEA".
               public static string  BRCHNAD1_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string  BRCHNAD1_PRN3 = "CT";             //  X(02)  VALUE  "WT".
               public static string  BRCHNAD1_PRN4 = "+";              //  X(01)  VALUE  "+".
               public static string  BRCHNAD1_PRN5 = "+";              //  X(01)  VALUE  "+".
               public static string BRCHNAD1_0201;  //As String * 150          //MEA-0202     Z(12)9.9.
               public static string  BRCHNAD1_PRN6 = "+";              //  X(01)  VALUE  ":".
               public static string BRCHNAD1_0202;  //As String * 150          //MEA-0202     Z(12)9.9.
               public static string  BRCHNAD1_PRN7 = "'";              //  X(01)  VALUE  "'".
   
               //*--------------------------(29)- 반출물품종류부호 --------------*
               //RFF2-PRN.
               public static string BRCHRFF1_PRN; //As String * 10
               public static string  BRCHRFF1_PRN1 = "RFF";            //  X(03)  VALUE  "RFF".
               public static string  BRCHRFF1_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string  BRCHRFF1_PRN3 = "ACD";            //  X(03)  VALUE  "ABP".
               public static string  BRCHRFF1_PRN4 = ":";              //  X(01)  VALUE  ":".
               public static string BRCHRFF1_0101;  //As String * 1           //RFF2-0101     X(20).
               public static string  BRCHRFF1_PRN5 = "'";              //  X(01)  VALUE  "'".
   
              //*--------------------------(29)- 반출신고번호 ------------------*
              //       ***    장치장번호
              //       ***    화물관리번호 1  ----( 반입일련번호와　같은　역활 )
              //       ***    화물관리번호 2  __|
              // CST-PRN.
               public static string BRCHRFF2_PRN; //As String * 27
               public static string  BRCHRFF2_PRN1 = "RFF";
               public static string  BRCHRFF2_PRN2 = "+";
               public static string  BRCHRFF2_PRN3 = "ABT";
               public static string  BRCHRFF2_PRN4 = ":";
               //05  CST-0201.
               public static string BRCHRFF2_02011;  //As String * 8
               public static string BRCHRFF2_02012;  //As String * 2
               public static string BRCHRFF2_02013;  //As String * 8
               public static string  BRCHRFF2_PRN5 = "'";
   
               //UNT-PRN.
               public static string BRCHUNT_PRN; //As String * 21
               public static string  BRCHUNT_PRN1 = "UNT";            //  X(03)  VALUE  "UNT".
               public static string  BRCHUNT_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string BRCHUNT_0101;  //As String * 6        //UNT-0101      Z(05)9.
               public static string  BRCHUNT_PRN3 = "+";              //  X(01)  VALUE  "+".
               public static string BRCHUNT_0201;  //As String * 8         //UNT-0201      X(08).
               public static string  BRCHUNT_PRN4 = "'";              //  X(01)  VALUE  "'".
               public static string  BRCHUNT_PRN5 = "#";              //  X(01)  VALUE  "#".
              //*------------------------------------------------------------------------------------*
   
               //*-------------------------------------------------------------------------------------*
              //*     << 보세화물 반입내역 EDI 항목 정의( 보험 ) >>                                    *
              //*-------------------------------------------------------------------------------------*
               public static string BTEMP_REC1; //As String
               public static string BTEMP_REC2; //As String
              //UNH-PRN.
              //*---------------(22)-------------------------------------------*
               public static string BUNH_PRN;             //As String * 22
               public static string  BUNH_PRN1 = "UNH";    //  X(03)    "UNH".
               public static string  BUNH_PRN2 = "+";      //  X(01)    "+".
               public static string  BUNH_PRN3 = "1+";      //  X(01)    "+".
               public static string  BUNH_PRN4 = "CUSBCR"; //  X(06)    "CUSCAR".
               public static string  BUNH_PRN5 = ":";      //  X(01)    ":".
               public static string  BUNH_PRN6 = "S";      //  X(01)    "S".
               public static string  BUNH_PRN7 = ":";      //  X(01)    ":".
               public static string  BUNH_PRN8 = "93A";    //  X(03)    "93A".
               public static string  BUNH_PRN9 = ":";      //  X(01)    ":".
               public static string  BUNH_PRN10 = "KE";     //  X(02)    "KE".
               public static string  BUNH_PRN11 = "'";      //  X(01)    "'".
              //*--------------------------(28)- 신고번호 ------------------*
              //       ***    장치장번호
              //       ***    화물관리번호 1  ----( 반입일련번호와　같은　역활 )
              //       ***    화물관리번호 2  __|
              // BGM-PRN.
               public static string BBGM_PRN; //As String * 28
               public static string  BBGM_PRN1 = "BGM";      //  X(03)  VALUE  "BGM".
               public static string  BBGM_PRN2 = "+";        //  X(01)  VALUE  "+".
               public static string  BBGM_PRN3 = "5HJ";      //  X(03)  VALUE  "632".
               public static string  BBGM_PRN4 = "+";        //  X(01)  VALUE  "+".
               public static string  BBGM_PRN5 = "11011055"; // 세관등록번호
               public static string BBGM_02011;  //As String * 8    //BGM-02011           X(08).전송일자
               public static string  BBGM_PRN7 = "+";        //  X(01)  VALUE  "+".
               public static string BBGM_0202;  //As String * 2         //BGM-0202       X(02). 전송구분
               public static string  BBGM_PRN8 = "'";        //  X(01)  VALUE  "'".
               //*--------------------------(26)- 반입일시 ------------------*
              //DTM-PRN.
               public static string BDTM_PRN; //As String * 26
               public static string  BDTM_PRN1 = "DTM";    //  X(03)  VALUE  "DTM".
               public static string  BDTM_PRN2 = "+";      //  X(01)  VALUE  "+".
               public static string  BDTM_PRN3 = "150";    //  X(02)  VALUE  "50".
               public static string  BDTM_PRN4 = ":";      //  X(01)  VALUE  ":".
              //DTM-0102.
               public static string BDTM_01021;  //As String * 8  //DTM-01021           X(08).
               public static string  BDTM_PRN5 = ":";      //  X(01)  VALUE  ":".
               public static string  BDTM_PRN6 = "102";    //  X(03)  VALUE  "204".
               public static string  BDTM_PRN7 = "'";      //  X(01)  VALUE  "'".
               //*--------------------------(37)- 신고세관 ------------------*
               //*- 과코드변경（감시과－＞통관지원과）  33->10 (2000.1.10~)
               //LOC-PRN.
               public static string BLOC_PRN; //As String * 37
               public static string  BLOC_PRN1 = "LOC";           //  X(03)  VALUE  "LOC".
               public static string  BLOC_PRN2 = "+18+";             //  X(01)  VALUE  "+".
               public static string  BLOC_PRN3 = "(주)태영인더스트리";            //  X(02)  VALUE  "41".
               public static string  BLOC_PRN4 = ":::";             //  X(03)  VALUE  "+".
               public static string  BLOC_PRN5 = "11011055";       //  X(03)  VALUE  "016".
               public static string  BLOC_PRN6 = "'";             //  X(01)  VALUE  "'".
               //*--------------------------(  58          ) ------------------*
               //DOC-PRN.
               public static string BDOC_PRN; //As String * 58
               public static string  BDOC_PRN1 = "DOC";
               public static string  BDOC_PRN2 = "+";
               public static string  BDOC_PRN3 = "85";
               public static string  BDOC_PRN4 = ":::";
               public static string BDOC_PRN5; //As String * 11     // 화물관리번호
               public static string BDOC_PRN6; //As String * 4      //  MSN
               public static string BDOC_PRN7; //As String * 3      //  HSN
               public static string  BDOC_PRN8 = "+";
               public static string BDOC_PRN9; //As String * 16     // B/L번호
               public static string  BDOC_PRN10 = "::";
               public static string BDOC_PRN11; //As String * 4     //일련번호(년도)
               public static string BDOC_PRN12; //As String * 7     //일련번호(순번)
               public static string  BDOC_PRN13 = "'";
   
               //*--------------------------(  HAN  11       ) ------------------*
               //HAN-PRN.
               public static string BHAN_PRN; //As String * 11
               public static string  BHAN_PRN1 = "HAN";
               public static string  BHAN_PRN2 = "+";
               public static string  BHAN_PRN3 = ":::";
               public static string  BHAN_PRN4 = "UTT";
               public static string  BHAN_PRN5 = "'";
   
               //*--------------------------(  화주명   46       ) ------------------*
               //FTX-PRN.
               public static string BFTX_PRN; //As String * 46
               public static string  BFTX_PRN1 = "FTX";
               public static string  BFTX_PRN2 = "+";
               public static string  BFTX_PRN3 = "ICN";
               public static string  BFTX_PRN4 = "+++";
               public static string BFTX_PRN5; //As String * 35     //화주명
               public static string  BFTX_PRN6 = "'";
   
                //*--------------------------(  선박명   34       ) ------------------*
               //FTX1-PRN.
               public static string BFTX1_PRN; //As String * 34
               public static string  BFTX1_PRN1 = "FTX";
               public static string  BFTX1_PRN2 = "+";
               public static string  BFTX1_PRN3 = "TRA";
               public static string  BFTX1_PRN4 = "+++";
               public static string BFTX1_PRN5; //As String * 23     //선박명/선명
               public static string  BFTX1_PRN6 = "'";
   
               //*--------------------------(  반입사고유형   13   ) ------------------*
               //FTX2-PRN.
               public static string BFTX2_PRN; //As String * 13
               public static string  BFTX2_PRN1 = "FTX";
               public static string  BFTX2_PRN2 = "+";
               public static string  BFTX2_PRN3 = "ACD";
               public static string  BFTX2_PRN4 = "+++";
               public static string  BFTX2_PRN5 = "OK";     //반입사고유형
               public static string  BFTX2_PRN6 = "'";
   
               //*--------------------------(    20        ) ------------------*
               //MEA-PRN.
               public static string BMEA_PRN; //As String * 20
               public static string  BMEA_PRN1 = "MEA";
               public static string  BMEA_PRN2 = "+";
               public static string  BMEA_PRN3 = "CT";
               public static string  BMEA_PRN4 = "+";
               public static string  BMEA_PRN5 = "+";
               public static string BMEA_PRN6; //As String * 2     //포장단위
               public static string  BMEA_PRN7 = ":";
               public static string BMEA_PRN8; //As String * 8     //수량
               public static string  BMEA_PRN9 = "'";
   
               //*--------------------------(     26       ) ------------------*
               //MEA1-PRN.
               public static string BMEA1_PRN; //As String * 26
               public static string  BMEA1_PRN1 = "MEA";
               public static string  BMEA1_PRN2 = "+";
               public static string  BMEA1_PRN3 = "WT";
               public static string  BMEA1_PRN4 = "+";
               public static string  BMEA1_PRN5 = "+";
               public static string  BMEA1_PRN6 = "KG";     //킬로그램
               public static string  BMEA1_PRN7 = ":";
               public static string BMEA1_PRN8; //As String * 14     //수량
               public static string  BMEA1_PRN9 = "'";
   
               //*--------------------------(15)- 품목분류 ------------------*
               //GIS-PRN.
               public static string BGIS_PRN; //As String * 15
               public static string  BGIS_PRN1 = "GIS";            //  X(03)  VALUE  "GIS".
               public static string  BGIS_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string BGIS_0101;  //As String * 2           // 반입유형
               public static string  BGIS_PRN3 = ":";             //  X(01)  VALUE  ":".
               public static string  BGIS_PRN4 = "109";            //  X(03)  VALUE  "5CA".
               public static string  BGIS_PRN5 = ":";              //  X(01)  VALUE  ":".
               public static string  BGIS_PRN6 = "KCS";            //  X(03)  VALUE  "KCS".
               public static string  BGIS_PRN7 = "'";              //  X(01)  VALUE  "'".
               //*--------------------------(14)- 화물반입유형 --------------*
               //GIS2-PRN.
               public static string BGIS2_PRN; //As String * 14
               public static string  BGIS2_PRN1 = "GIS";           //  X(03)  VALUE  "GIS".
               public static string  BGIS2_PRN2 = "+";             //  X(01)  VALUE  "+".
               public static string BGIS2_0101;  //As String * 1          // 분할반입구분
               public static string  BGIS2_PRN3 = ":";             //  X(01)  VALUE  ":".
               public static string  BGIS2_PRN4 = "121";           //  X(03)  VALUE  "109".
               public static string  BGIS2_PRN5 = ":";             //  X(01)  VALUE  ":".
               public static string  BGIS2_PRN6 = "KCS";           //  X(03)  VALUE  "KCS".
               public static string  BGIS2_PRN7 = "'";             //  X(01)  VALUE  "'".
   
              //*--------------------------(43)- 품명 --------------*
               //PCI-PRN.
               public static string BPCI_PRN; //As String * 43
               public static string  BPCI_PRN1 = "PCI";           //  X(03)  VALUE  "GIS".
               public static string  BPCI_PRN2 = "+";             //  X(01)  VALUE  "+".
               public static string  BPCI_PRN3 = "23";             //  X(01)  VALUE  "+".
               public static string  BPCI_PRN4 = "+";             //  X(01)  VALUE  "+"
               public static string BPCI_PRN5;  //As String * 35          // 품명
               public static string BPCI_PRN6 = "'";             //  X(01)  VALUE  "'".
   
               //*--------------------------(21)- 입항일자 --------------*
               //DTM-PRN.
               public static string BDTM1_PRN; //As String * 21
               public static string  BDTM1_PRN1 = "DTM";           //  X(03)  VALUE  "GIS".
               public static string  BDTM1_PRN2 = "+";             //  X(01)  VALUE  "+".
               public static string  BDTM1_PRN3 = "178";             //  X(01)  VALUE  "+".
               public static string  BDTM1_PRN4 = ":";             //  X(01)  VALUE  "+"
               public static string BDTM1_PRN5;  //As String * 8           // 입항일자
               public static string  BDTM1_PRN6 = ":";             //  X(01)  VALUE  "'".
               public static string  BDTM1_PRN7 = "102";             //  X(01)  VALUE  "+".
               public static string  BDTM1_PRN8 = "'";             //  X(01)  VALUE  "+".
   
               //*--------------------------(7)- 입고 --------------*
               //PCI1-PRN.
               public static string BPCI1_PRN; //As String * 7
               public static string  BPCI1_PRN1 = "PCI";           //  X(03)  VALUE  "GIS".
               public static string  BPCI1_PRN2 = "+";             //  X(01)  VALUE  "+".
               public static string  BPCI1_PRN3 = "13";            //  X(01)  VALUE  "+".
               public static string  BPCI1_PRN4 = "'";             //  X(01)  VALUE  "+".
   
               //*--------------------------(  창고전송일시  ) ------------------*
               //FTX2-PRN.
               public static string BFTX4_PRN; //As String * 23
               public static string  BFTX4_PRN1 = "FTX";
               public static string  BFTX4_PRN2 = "+";
               public static string  BFTX4_PRN3 = "WHI";
               public static string  BFTX4_PRN4 = "+++";
               public static string BFTX4_PRN5;  //As String * 12           //창고전송일시
               public static string  BFTX4_PRN6 = "'";
   
               //*--------------------------(19)- 입고일자 --------------*
               //FTX-PRN.
               public static string BFTX3_PRN; //As String * 19
               public static string  BFTX3_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
               public static string  BFTX3_PRN2 = "+";              //   X(01)  VALUE  "+".
               public static string  BFTX3_PRN3 = "AEA";            //   X(03)  VALUE  "AEA".
               public static string  BFTX3_PRN4 = "+";              //   X(01)  VALUE  "+".
               public static string  BFTX3_PRN5 = "+";              //   X(01)  VALUE  "+".
               public static string  BFTX3_PRN6 = "+";              //   X(01)  VALUE  "+".
               public static string BFTX3_PRN7;  //As String * 8           // 입고일자
               public static string  BFTX3_PRN8 = "'";              //   X(01)  VALUE  "'".
   
               //*--------------------------(26)- 입고일시 --------------*
               //DTM-PRN.
               public static string BDTM2_PRN; //As String * 26
               public static string  BDTM2_PRN1 = "DTM";            //   X(03)  VALUE  "FTX".
               public static string  BDTM2_PRN2 = "+";              //   X(01)  VALUE  "+".
               public static string  BDTM2_PRN3 = "50";            //   X(03)  VALUE  "AEA".
               public static string  BDTM2_PRN4 = ":";              //   X(01)  VALUE  "+".
               public static string BDTM2_PRN5; //As String * 8            //   년월일
               public static string BDTM2_PRN6; //As String * 6            //   년월일시
               public static string  BDTM2_PRN7 = ":";              //   X(01)  VALUE  "+".
               public static string  BDTM2_PRN8 = "204";           // FTX-0201        X(03).
               public static string  BDTM2_PRN9 = "'";              //   X(01)  VALUE  "'".
   
               //*--------------------------(27)- 반입신고번호 --------------*
               //RFF-PRN.
               public static string BRFF_PRN; //As String * 27
               public static string  BRFF_PRN1 = "RFF";            //  X(03)  VALUE  "RFF".
               public static string  BRFF_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string  BRFF_PRN3 = "WE";             //  X(02)  VALUE  "XC".
               public static string  BRFF_PRN4 = ":";              //  X(01)  VALUE  ":".
               public static string  BRFF_PRN5 = "11011055";       //  반입신고번호(입고번호)
               public static string BRFF_PRN6; //As String * 4     //일련번호(년도)
               public static string BRFF_PRN7; //As String * 7     //일련번호(순번)
               public static string  BRFF_PRN8 = "'";              //X(01)  VALUE  "'".
   
               //*--------------------------(28)- 반입근거번호 --------------*
               //RFF-PRN.
               public static string BRFF1_PRN; //As String * 28
               public static string  BRFF1_PRN1 = "RFF";            //  X(03)  VALUE  "RFF".
               public static string  BRFF1_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string  BRFF1_PRN3 = "IB";             //  X(02)  VALUE  "XC".
               public static string  BRFF1_PRN4 = ":";              //  X(01)  VALUE  ":".
               public static string BRFF1_PRN5;  //As String * 20          //반입근거번호(입고번호)
               public static string  BRFF1_PRN6 = "'";              //X(01)  VALUE  "'".

               //*--------------------------(13)-           --------------*
               //UNT-PRN.
               public static string BUNT_PRN; //As String * 13
               public static string  BUNT_PRN1 = "UNT";            //  X(03)  VALUE  "UNT".
               public static string  BUNT_PRN2 = "+";              //  X(01)  VALUE  "+".
               public static string BUNT_0101;  //As String * 6        // UNT-0101      Z(05)9.
               public static string  BUNT_PRN3 = "+1'";              //  X(01)  VALUE  "+".

        #endregion

        #region Description : 반출기간연장신청서
        //*-------------------------------------------------------------------------------------*
        //*     << 반출기간연장 EDI 항목 정의 >>                                                    *
        //*-------------------------------------------------------------------------------------*
        //UNH-PRN.
        //*---------------(35)-------------------------------------------*

        public static string EXUNH_PRN;                      //As String * 29

        public static string EXUNH_PRN1 = "UNH";             //  X(03)    "UNH".
        public static string EXUNH_PRN2 = "+";               //  X(01)    "+".
        public static string EXUNH_0101;  //As String * 14            //UNH-0101   X(06).
        public static string EXUNH_PRN3 = "+";               //  X(01)    "+".
        public static string EXUNH_PRN4 = "CUSDMR";          //  X(06)    "CUSCAR".
        public static string EXUNH_PRN5 = ":";               //  X(01)    ":".
        public static string EXUNH_PRN6 = "S";               //  X(01)    "S".
        public static string EXUNH_PRN7 = ":";               //  X(01)    ":".
        public static string EXUNH_PRN8 = "93A";             //  X(03)    "93A".
        public static string EXUNH_PRN9 = ":";               //  X(01)    ":".
        public static string EXUNH_PRN10 = "KE";             //  X(02)    "KE".
        public static string EXUNH_PRN11 = "'";              //  X(01)    "'".

        //*--------------------------(29)------------------*
        //*** 반출일련번호 (장치장번호 / 년/ 일련번호)
        //BGM-PRN.
        public static string EXBGM_PRN; //As String * 30
        public static string EXBGM_PRN1 = "BGM";             //  X(03)  VALUE  "BGM".
        public static string EXBGM_PRN2 = "+";               //  X(01)  VALUE  "+".
        public static string EXBGM_PRN3 = "5HM";             //  X(03)  VALUE  "6NB".
        public static string EXBGM_PRN4 = "+";               //  X(01)  VALUE  "+".
        //05  EXBGM-0201.
        public static string EXBGM_02011;  //As String * 10           //BGM-02011           X(08).
        public static string EXBGM_02012;  //As String * 2           //BGM-02012           X(02).
        public static string EXBGM_02013;  //As String * 5           //BGM-02013           X(06).

        public static string EXBGM_PRN5 = "+";               //X(01)  VALUE  "+".
        public static string EXBGM_0202;  //As String * 2            //BGM-0202       X(02).
        public static string EXBGM_PRN6 = "'";               //  X(01)  VALUE  "'".

        
        //*-------------------------- 신청구분 ------------------*
        //GIS-PRN.
        public static string EXGIS_PRN; //As String * 15
        public static string EXGIS_PRN1 = "GIS";             //  X(03)  VALUE  "GIS".
        public static string EXGIS_PRN2 = "+";               //  X(01)  VALUE  "+".
        public static string EXGIS_0101;  //As String * 1            //GIS-0101   X(02).
        public static string EXGIS_PRN3 = ":";               //  X(01)  VALUE  ":".
        public static string EXGIS_PRN4 = "5AI";             //  X(03)  VALUE  "148".
        public static string EXGIS_PRN5 = ":";               //  X(01)  VALUE  ":".
        public static string EXGIS_PRN6 = "KCS";             //  X(03)  VALUE  "KCS".
        public static string EXGIS_PRN7 = "'";              //  X(01)  VALUE  "'".

        //*-------------------------- 연장기간구분 --------------*
        //GIS2-PRN.
        public static string EXGIS2_PRN; //As String * 14
        public static string EXGIS2_PRN1 = "GIS";            //  X(03)  VALUE  "GIS".
        public static string EXGIS2_PRN2 = "+";              //  X(01)  VALUE  "+".
        public static string EXGIS2_0101;  //As String * 2           //GIS2-0101     X(01).
        public static string EXGIS2_PRN3 = ":";              //  X(01)  VALUE  ":".
        public static string EXGIS2_PRN4 = "5DM";            //  X(03)  VALUE  "121".
        public static string EXGIS2_PRN5 = ":";              //  X(01)  VALUE  ":".
        public static string EXGIS2_PRN6 = "KCS";            //  X(03)  VALUE  "KCS".
        public static string EXGIS2_PRN7 = "'";              //  X(01)  VALUE  "'".

        //*-------------------------- 신청일자 ------------------*
        //DTM-PRN.
        public static string EXDTM_PRN;    //  X(26)
        public static string EXDTM_PRN1 = "DTM";    //  X(03)  VALUE  "DTM".
        public static string EXDTM_PRN2 = "+";      //  X(01)  VALUE  "+".
        public static string EXDTM_PRN3 = "141";     //  X(02)  VALUE  "50".
        public static string EXDTM_PRN4 = ":";      //  X(01)  VALUE  ":". \
        public static string EXDTM_0101;  //As String * 8   
        public static string EXDTM_PRN6 = ":";      //  X(01)  VALUE  ":". \
        public static string EXDTM_PRN7 = "102";     //  X(02)  VALUE  "50".
        public static string EXDTM_PRN8 = "'";              //  X(01)  VALUE  "'".

        //*-------------------------- 연장기간시작일자 ------------------*
        //DTM-PRN.
        public static string EXDTM1_PRN;    //  X(26)
        public static string EXDTM1_PRN1 = "DTM";    //  X(03)  VALUE  "DTM".
        public static string EXDTM1_PRN2 = "+";      //  X(01)  VALUE  "+".
        public static string EXDTM1_PRN3 = "504";     //  X(02)  VALUE  "50".
        public static string EXDTM1_PRN4 = ":";      //  X(01)  VALUE  ":". \
        public static string EXDTM1_0101;  //As String * 8   
        public static string EXDTM1_PRN6 = ":";      //  X(01)  VALUE  ":". \
        public static string EXDTM1_PRN7 = "102";     //  X(02)  VALUE  "50".
        public static string EXDTM1_PRN8 = "'";              //  X(01)  VALUE  "'".

        //*-------------------------- 연장기간종료일자 ------------------*
        //DTM-PRN.
        public static string EXDTM2_PRN;    //  X(26)
        public static string EXDTM2_PRN1 = "DTM";    //  X(03)  VALUE  "DTM".
        public static string EXDTM2_PRN2 = "+";      //  X(01)  VALUE  "+".
        public static string EXDTM2_PRN3 = "505";     //  X(02)  VALUE  "50".
        public static string EXDTM2_PRN4 = ":";      //  X(01)  VALUE  ":". \
        public static string EXDTM2_0101;  //As String * 8   
        public static string EXDTM2_PRN6 = ":";      //  X(01)  VALUE  ":". \
        public static string EXDTM2_PRN7 = "102";     //  X(02)  VALUE  "50".
        public static string EXDTM2_PRN8 = "'";              //  X(01)  VALUE  "'".

        //*-------------------------- 장치장소 ------------------*        
        //LOC-PRN.
        public static string EXLOC_PRN; //As String * 32
        public static string EXLOC_PRN1 = "LOC";           //  X(03)  VALUE  "LOC".
        public static string EXLOC_PRN2 = "+";             //  X(01)  VALUE  "+".
        public static string EXLOC_PRN3 = "14";            //  X(02)  VALUE  "14".
        public static string EXLOC_PRN4 = "+";             //  X(03)  VALUE  "+".
        public static string EXLOC_0101; //As String * 8   
        public static string EXLOC_PRN6 = ":";             //  X(01)  VALUE  ":".
        public static string EXLOC_PRN7 = "129";           //  X(03)  VALUE  "5AB".
        public static string EXLOC_PRN8 = ":";             //  X(01)  VALUE  ":".
        public static string EXLOC_PRN9 = "KCS";           //  X(03)  VALUE  "KCS".
        public static string EXLOC_PRN10 = "'";             //  X(01)  VALUE  "'".

        //*-------------------------- 신고세관 ------------------*        
        public static string EXLOC1_PRN; //As String * 32
        public static string EXLOC1_PRN1 = "LOC";           //  X(03)  VALUE  "LOC".
        public static string EXLOC1_PRN2 = "+";             //  X(01)  VALUE  "+".
        public static string EXLOC1_PRN3 = "41";            //  X(02)  VALUE  "41".
        public static string EXLOC1_PRN4 = "+";             //  X(03)  VALUE  "+".
        public static string EXLOC1_PRN5 = "110";           //  X(03)  VALUE  "110".
        public static string EXLOC1_PRN6 = ":";             //  X(01)  VALUE  ":".
        public static string EXLOC1_PRN7 = "113";           //  X(03)  VALUE  "113".
        public static string EXLOC1_PRN8 = ":";             //  X(01)  VALUE  ":".
        public static string EXLOC1_PRN9 = "KCS";           //  X(03)  VALUE  "KCS".
        public static string EXLOC1_PRN10 = "+";             //  X(01)  VALUE  "+".
        public static string EXLOC1_PRN11 = "10";            //  X(02)  VALUE  "10".
        public static string EXLOC1_PRN12 = ":";             //  X(01)  VALUE  ":".
        public static string EXLOC1_PRN13 = "5AB";           //  X(03)  VALUE  "5AB".
        public static string EXLOC1_PRN14 = ":";             //  X(01)  VALUE  ":".
        public static string EXLOC1_PRN15 = "KCS";           //  X(03)  VALUE  "KCS".
        public static string EXLOC1_PRN16 = "'";             //  X(01)  VALUE  "'".

        //*-------------------------- 연장사유 --------------*
        //FTX-PRN.
        public static string EXFTX_PRN; //As String * 61
        public static string EXFTX_PRN1 = "FTX";            //   X(03)  VALUE  "FTX".
        public static string EXFTX_PRN2 = "+";              //   X(01)  VALUE  "+".
        public static string EXFTX_PRN3 = "ACD";            //   X(03)  VALUE  "ACD".
        public static string EXFTX_PRN4 = "+";              //   X(01)  VALUE  "+".
        public static string EXFTX_PRN5 = "+";              //   X(01)  VALUE  "+".
        public static string EXFTX_PRN6 = "+";              //   X(01)  VALUE  "+".
        public static string EXFTX_0101;  //As String * 70          // FTX-0201        X(03).
        public static string EXFTX_PRN7 = "'";              //   X(01)  VALUE  "'".

        //*--------------------------신청자 ------------------*
        //NAD-PRN.
        public static string EXNAD_PRN; //As String * 109
        public static string EXNAD_PRN1 = "NAD";           //  X(03)  VALUE  "NAD".
        public static string EXNAD_PRN2 = "+";             //  X(01)  VALUE  "+".
        public static string EXNAD_PRN3 = "DT";            //  X(02)  VALUE  "GO".
        public static string EXNAD_PRN4 = "+";             //  X(01)  VALUE  "+".
        public static string EXNAD_PRN5 = "+";             //  X(01)  VALUE  "+".
        public static string EXNAD_0101;  //As String * 100         //NAD-0102      X(35).
        public static string EXNAD_PRN6 = "'";             //  X(01)  VALUE  "'".

        //*-------------------------- 화물관리번호 --------------*
        //       ***    적하목록관리번호
        //       ***   MSN   /    HSN
        //RFF-PRN.
        public static string EXRFF_PRN;                      // As String * 29
        public static string EXRFF_PRN1 = "RFF";            //  X(03)  VALUE  "RFF".
        public static string EXRFF_PRN2 = "+";              //  X(01)  VALUE  "+".
        public static string EXRFF_PRN3 = "XC";             //  X(02)  VALUE  "XC".
        public static string EXRFF_PRN4 = ":";              //  X(01)  VALUE  ":".
        public static string EXRFF_0102;                       //  As String * 11          //RFF-0102     X(11).
        public static string EXRFF_PRN5 = ":";              //  X(01)  VALUE  ":".
        public static string EXRFF_0103;                     //As String * 4           //RFF-0103     X(04).
        public static string EXRFF_PRN6 = ":";              //  X(01)  VALUE  ":".
        public static string EXRFF_0104;                     //As String * 4           //RFF-0104     X(03).
        public static string EXRFF_PRN7 = "'";              //X(01)  VALUE  "'".

        //*--------------------------- 수입신고번호 --------------*
        //BANIP-REC.
        //RFF2-PRN.
        public static string EXRFF2_PRN;                     //As String * 29
        public static string EXRFF2_PRN1 = "RFF";            //  X(03)  VALUE  "RFF".
        public static string EXRFF2_PRN2 = "+";              //  X(01)  VALUE  "+".
        public static string EXRFF2_PRN3 = "ABP";            //  X(03)  VALUE  "ABP".
        public static string EXRFF2_PRN4 = ":";              //  X(01)  VALUE  ":".
        public static string EXRFF2_0101;                    //As String * 20          //RFF2-0101     X(20).
        public static string EXRFF2_PRN5 = "'";              //  X(01)  VALUE  "'".


        //*----------------전자문서내 세부사항부분과요약부분 --------------*
        public static string EXUNS_PRN; //As String * 6
        public static string EXUNS_PRN1 = "UNS";            //   X(03)  VALUE  "FTX".
        public static string EXUNS_PRN2 = "+";              //   X(01)  VALUE  "+".
        public static string EXUNS_PRN3 = "S";            //   X(03)  VALUE  "ACD".
        public static string EXUNS_PRN4 = "'";              //   X(01)  VALUE  "'".

        //*-----------------전자문서의종료를나타내는전송항목-----------------------------*
        //UNT-PRN.
        public static string EXUNT_PRN; //As String * 20
        public static string EXUNT_PRN1 = "UNT";            //  X(03)  VALUE  "UNT".
        public static string EXUNT_PRN2 = "+";              //  X(01)  VALUE  "+".
        public static string EXUNT_0101;  //As String * 6           //UNT-0101      Z(05)9.
        public static string EXUNT_PRN3 = "+";              //  X(01)  VALUE  "+".
        public static string EXUNT_0201;  //As String * 8           //UNT-0201      X(04).
        public static string EXUNT_PRN4 = "'";              //  X(01)  VALUE  "'".
       
        #endregion

        #region Description : 마스타 파일 EDI 항목 정의
         //*------------------------------------------------------------------------------------*
         //*     << 마스타 파일 EDI 항목 정의 >>                                                *
         //*------------------------------------------------------------------------------------*
           public static string  MASTHEAD;  //As String
           public static string  MASTHEAD1 = "UNB+KECA:1+";
           public static string  MASTHEAD2 = "BWH11011055";
           public static string  MASTHEAD3 = ":57+KCS4G002:57+";  //test 수신식별자
           public static string  MASTHEAD_DATE; //As String * 8
           public static string  MASTHEAD4 = ":";
           public static string  MASTHEAD_TIME1; //As String * 2
           public static string  MASTHEAD_TIME2; //As String * 2
           public static string  MASTHEAD5 = "+1'";
         
           public static string MASTTAIL; //As String
           public static string  MASTTAIL1 = "UNZ+";
           public static string MASTTAIL_CNT; //As String * 3
           public static string  MASTTAIL2 = "+1'";

        #endregion

        public static string wco_Response_IssueDateTime;  //통보일시        
        public static string wco_Response_TypeCode;  //!--문서형태구분--
        public static string wco_AcceptanceDateTime;  //수신일시
        public static string wco_Declaration_ID;     //문서번호(제출번호)
        public static string wco_Declaration_TypeCode; //문서구분        
        public static string wco_Error_Declaration; //!--오류내역--


        /* --------------- 반출승인내역통보서 ---------------------------------------------------------------- */        
        public static string wco_Response_ID;						  //반출승인근거번호      
        public static string wco_Response_FunctionCode;					  //반출승인취하구분              
        public static string wco_Response_Declaration_AcceptanceDateTime;		  //반출승인취하일자      
        public static string wco_Response_Declaration_Consignment_Warehouse_ID;		  //보세구역              
        public static string wco_Response_AdditionalInformation_StatementTypeCode;	  //반출승인유형          
        public static string wco_Response_Declaration_ExaminationIndicatorCode;		  //검사대상여부  
        public static string wco_TransportContractDocument_ID;                     //적하목록 관리번호    
        public static string wco_TransportContractDocument_MasterBLSequenceID;     //MSN
        public static string wco_TransportContractDocument_HouseBLSequenceID;      //HSN

        public static string wco_Payment_TaxAssessedAmount;                        //관세액
        public static string wco_AdditionalDocument_ID;                 //장치확인번호, 기간연장승인번호
        public static string wco_TradeTerms_ConditionCode;              //인도조건

        public static string wco_DutyTaxFee_TypeCode;                   //세액구분 (관세:CUD,과세가격합계:5CZ)-->
        public static string wco_DutyTaxFee_Payment_TaxAssessedAmount;             //과세가격

        public static string wco_DutyTaxFee_TypeCode_Total;                   //세액구분 (관세:CUD,과세가격합계:5CZ)-->
        public static string wco_DutyTaxFee_Payment_TaxAssessedAmount_Total;             //과세가격 합계

        public static string wco_Consignment_TransportContractDocument_ID;          //B/L (AWB) 번호
        public static string wco_BorderTransportMeans_Name;             //선(기)명
        public static string wco_BorderTransportMeans_ArrivalDateTime;  //입항일자

        public static string wco_Payer_ID;                  //납세의무자 사업자등록번호          
        public static string wco_Payer_RoleCode;            //납세의무자 구분
        public static string wco_Payer_Name;                //납세의무자 상호
        public static string wco_Payer_Contact_Name;        //납세의무자 성명

        public static string wco_Commodity_SequenceNumeric;                           //란번호              
        public static string wco_AdditionalInformation_StatementCode;		      //보류/수리구분, 연장처리구분(1:승인, 2:거부)       
        public static string wco_Commodity_CargoDescription;			      //품명                
        public static string wco_Commodity_Description;				      //규격                
        public static string wco_GoodsMeasure_NetNetWeightMeasure_kcsUnitCode;	      //순중량(단위)        
        public static string wco_GoodsMeasure_NetNetWeightMeasure;		      //순중량              
        public static string wco_Commodity_CountQuantity_kcsUnitCode;		      //수량(단위)          
        public static string wco_Commodity_CountQuantity;			      //수량                
        public static string wco_AdditionalInformation_ApprovalCountQuantity;	      //반출승인개수        
        public static string wco_Packaging_TypeCode;				      //포장종류            
        public static string wco_AdditionalInformation_ApprovalWeightMeasure;	      //반출승인중량     
        /* ---------------------------------------------------------------------------------------- */


        /* --------------- 반출기간연장결과통보서 ------------------------------------------------- */

        public static string kcs_AuthenticationDateTime;        //승인(승인거부)일자(CCYYMMDD)-->
        public static string kcs_Reason;   //승인거부사유        
        public static string wco_AdditionalInformation_BeginningDateTime;     //연장기간시작일(CCYYMMDD)-->
        public static string wco_AdditionalInformation_EndingDateTime;     //연장기간종료일(CCYYMMDD)-->
        
        /* ---------------------------------------------------------------------------------------- */

        #region Description : XML 반출보고서 정의서
        public static string UP_Get_XmlGOVCBR6NB(string sFunctionCode,  //전자문서기능
                                                 string sID,            //신고번호
                                                 string sVersionID,     //분할반출차수
                                                 string sGrossVolumeMeasure, //반출중량
                                                 string sCarryOutTypeCode,   //화물반출유형
                                                 string sCarryOutSplitCode,  //분할반출구분
                                                 string sCarryOutExtensionCode, //반출기간연장 구분
                                                 string sAdditionalDocumentID, //반출근거번호
                                                 string sTotalPackageQuantity,  //반출총개수
                                                 string sConAdditionalDocumentID, //기간연장근거번호
                                                 string sCustomsAssignedReferenceID, //화물관리번호
                                                 string sDepartureDateTime //반출일시(CCYYMMDDHHMM)
            )
        {
            string sXml = string.Empty;

            sXml += "<?xml version='1.0' encoding='UTF-8'?>";
            sXml += "<wco:Declaration ";
            sXml += "  xmlns:kcs='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_6NBSchemaModule:1:0' ";
            sXml += "  xmlns:wco='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_6NBSchemaModule:1:0' ";
            sXml += "  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_6NBSchemaModule:1:0 ../../schema4G/kcs/data/standard/KCS_DeclarationOfCAG_6NBSchemaModule_1.0_standard.xsd'> ";
            //    <!--전자문서기능(9:원본, 35:재전송)-->
            sXml += " <wco:FunctionCode>" + sFunctionCode.Trim() + "</wco:FunctionCode> ";
            //    <!--신고번호-->
            sXml += "  <wco:ID>" + sID.Trim() + "</wco:ID> ";
            //     <!--문서형태구분-->
            sXml += "  <wco:TypeCode>GOVCBR6NB</wco:TypeCode> ";
            if (sCarryOutSplitCode.Trim() != "A")
            {
                if (sVersionID.Trim() != "0")
                {
                    //     <!--분할반출차수-->
                    sXml += "   <wco:VersionID>" + sVersionID.Trim() + "</wco:VersionID> ";
                }
            }
            //     <!--반출중량-->
            sXml += "  <kcs:GrossVolumeMeasure>" + sGrossVolumeMeasure.Trim() + "</kcs:GrossVolumeMeasure> ";
            sXml += "  <wco:AdditionalCode> ";
            //         <!--화물반출유형-->
            sXml += "  <kcs:CarryOutTypeCode>" + sCarryOutTypeCode.Trim() + "</kcs:CarryOutTypeCode> ";
            //         <!--분할반출구분-->
            sXml += "  <kcs:CarryOutSplitCode>" + sCarryOutSplitCode.Trim() + "</kcs:CarryOutSplitCode> ";
            //         <!--반출기간연장 구분-->
            sXml += "  <kcs:CarryOutExtensionCode>" + sCarryOutExtensionCode.Trim() + " </kcs:CarryOutExtensionCode> ";
            sXml += "  </wco:AdditionalCode> ";
            sXml += "  <wco:AdditionalDocument> ";
            //       <!--반출근거번호-->
            sXml += "  <wco:ID>" + sAdditionalDocumentID.Trim() + "</wco:ID> ";
            sXml += "  </wco:AdditionalDocument> ";
            
            sXml += "  <wco:Consignment> ";
            //      <!--반출총개수-->
            sXml += "    <wco:TotalPackageQuantity>" + sTotalPackageQuantity.Trim() + "</wco:TotalPackageQuantity> ";
            sXml += "  <wco:AdditionalDocument> ";
            //        <!--기간연장근거번호-->
            sXml += "  <wco:ID>" + sConAdditionalDocumentID.Trim() + "</wco:ID> ";
            sXml += " </wco:AdditionalDocument> ";
            sXml += "     <wco:UCR> ";
            //      <!--화물관리번호-->
            sXml += "   <kcs:CustomsAssignedReferenceID>" + sCustomsAssignedReferenceID.Trim() + "</kcs:CustomsAssignedReferenceID> ";
            sXml += " </wco:UCR> ";
            sXml += " <wco:Warehouse> ";
            //   <!--반출일시(CCYYMMDDHHMM)-->
            sXml += "  <wco:DepartureDateTime>" + sDepartureDateTime.Trim() + "</wco:DepartureDateTime> ";
            sXml += "  </wco:Warehouse> ";
            sXml += " </wco:Consignment> ";

            sXml += " </wco:Declaration> ";

            return sXml;
        }
        #endregion

        #region Description : XML 반입보고서 정의서
        public static string UP_Get_XmlGOVCBR632(string sFunctionCode,  //전자문서기능
                                                 string sID,            //신고번호
                                                 string sTypeCode,      //문서형태구분
                                                 string sVersionID,     //분할반입차수
                                                 string sExaminationIndicatorCode, //X-ray 검사여부
                                                 string sCarryInTypeCode,          //화물반입유형
                                                 string sCarryInSplitCode,         //분할반입구분(('A':전량, 'P':분할, 'L':최종))
                                                 string sCarryInUseCode,           //반입용도구분((P : 보세작업용 C : 수입통관용  B : BWT 물품    L : LME  G : 정부비축물품) )     
                                                 string sAdditionalDocumentID,     //반입근거번호
                                                 string sGrossVolumeMeasure,       //반입중량
                                                 string sTotalPackageQuantity,     //반입개수
                                                 string sAbnormalityTypeCode,      //반입사고 유형
                                                 string sAbnormalityWeightMeasure, //반입사고 중량
                                                 string sAbnormalityQuantityQuantity, //반입사고 개수
                                                 string sPackagingTypeCode,           //포장종류
                                                 string sCustomsAssignedReferenceID,  //화물관리번호
                                                 string sArrivalDateTime,             //반입일시
                                                 string sWarehouseID,                 //장치위치
                                                 string sTransportEquipmentID         //탱크번호                                          
            ) 
        {
            string sXml = string.Empty;

            sXml += "<?xml version='1.0' encoding='UTF-8'?> ";
            sXml += "<wco:Declaration ";
            sXml += "    xmlns:kcs='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_632SchemaModule:1:0' ";
            sXml += "    xmlns:wco='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_632SchemaModule:1:0' ";
            sXml += "    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_632SchemaModule:1:0 ../../schema4G/kcs/data/standard/KCS_DeclarationOfCAG_632SchemaModule_1.0_standard.xsd'> ";
            //   <!--전자문서기능(9:원본, 35:재전송)--> 
            sXml += "    <wco:FunctionCode>"+sFunctionCode.Trim()+"</wco:FunctionCode> ";
            //    <!--신고번호-->
            sXml += "    <wco:ID>" + sID.Trim() + "</wco:ID> ";
            //    <!--문서형태구분-->
            sXml += "    <wco:TypeCode>" + sTypeCode.Trim() + "</wco:TypeCode> ";
            //    <!--분할반입차수-->
            sXml += "    <wco:VersionID>" + sVersionID.Trim() + "</wco:VersionID> ";
            //    <!--X-ray 검사여부-->
            sXml += "    <kcs:ExaminationIndicatorCode>" + sExaminationIndicatorCode.Trim() + "</kcs:ExaminationIndicatorCode> ";
            sXml += "    <wco:AdditionalCode> ";
            //        <!--화물반입유형-->
            sXml += "        <kcs:CarryInTypeCode>" + sCarryInTypeCode.Trim() + "</kcs:CarryInTypeCode> ";
            //       <!--분할반입구분(('A':전량, 'P':분할, 'L':최종))-->
            sXml += "        <kcs:CarryInSplitCode>" + sCarryInSplitCode.Trim() + "</kcs:CarryInSplitCode> ";
            if (sCarryInUseCode.Trim() != "")
            {
                //        <!--반입용도구분((P : 보세작업용 C : 수입통관용  B : BWT 물품    L : LME  G : 정부비축물품) )-->
                sXml += "        <kcs:CarryInUseCode>" + sCarryInUseCode.Trim() + "</kcs:CarryInUseCode> ";
            }
            sXml += "    </wco:AdditionalCode> ";

            if (sAdditionalDocumentID.Trim() != "")
            {
                sXml += "    <wco:AdditionalDocument> ";
                //        <!--반입근거번호-->
                sXml += "        <wco:ID>" + sAdditionalDocumentID.Trim() + "</wco:ID> ";
                sXml += "    </wco:AdditionalDocument> ";
            }

            sXml += "    <wco:Consignment> ";
            //        <!--반입중량-->
            sXml += "        <wco:GrossVolumeMeasure>" + sGrossVolumeMeasure.Trim() + "</wco:GrossVolumeMeasure> ";
            //        <!--반입개수-->
            sXml += "        <wco:TotalPackageQuantity>" + sTotalPackageQuantity.Trim() + "</wco:TotalPackageQuantity> ";
            //        <!--반입사고 유형-->
            sXml += "        <kcs:AbnormalityTypeCode>" + sAbnormalityTypeCode.Trim() + "</kcs:AbnormalityTypeCode> ";
            if (sAbnormalityWeightMeasure.Trim() != "")
            {
                //       <!--반입사고 중량-->
                sXml += "        <kcs:AbnormalityWeightMeasure>" + sAbnormalityWeightMeasure.Trim() + "</kcs:AbnormalityWeightMeasure> ";
            }
            if (sAbnormalityQuantityQuantity.Trim() != "")
            {
                //        <!--반입사고 개수-->
                sXml += "        <kcs:AbnormalityQuantityQuantity>" + sAbnormalityQuantityQuantity.Trim() + "</kcs:AbnormalityQuantityQuantity> ";
            }
            sXml += "        <wco:Packaging> ";
            //            <!--포장종류-->
            sXml += "            <wco:TypeCode>" + sPackagingTypeCode.Trim() + "</wco:TypeCode> ";
            sXml += "        </wco:Packaging> ";
            sXml += "        <wco:UCR> ";
            //            <!--화물관리번호-->
            sXml += "            <kcs:CustomsAssignedReferenceID>" + sCustomsAssignedReferenceID.Trim() + "</kcs:CustomsAssignedReferenceID> ";
            sXml += "        </wco:UCR> ";
            sXml += "        <wco:Warehouse> ";
            //            <!--반입일시-->
            sXml += "            <wco:ArrivalDateTime>" + sArrivalDateTime.Trim() + "</wco:ArrivalDateTime> ";
            if (sWarehouseID.Trim() != "")
            {
                //            <!--장치위치-->
                sXml += "            <wco:ID>" + sWarehouseID.Trim() + "</wco:ID> ";
            }
            sXml += "        </wco:Warehouse> ";
            sXml += "    </wco:Consignment> ";
            if (sTransportEquipmentID.Trim() != "")
            {
                sXml += "    <wco:TransportEquipment> ";
                //        <!--탱크번호-->
                sXml += "        <wco:ID>" + sTransportEquipmentID.Trim() + "</wco:ID> ";
                sXml += "    </wco:TransportEquipment> ";
            }
            sXml += "</wco:Declaration> ";

            return sXml;
        }
        #endregion            

        #region Description : XML 반출입 정정신청서 정의서
        public static string UP_Get_XmlGOVCBR(string sGubn)
        {
            string sXml = string.Empty;

            sXml += "<?xml version='1.0' encoding='UTF-8'?> ";
            sXml += "<wco:Declaration ";
            sXml += "    xmlns:kcs='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5LCSchemaModule:1:0' ";
            sXml += "    xmlns:wco='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5LCSchemaModule:1:0' ";
            sXml += "    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5LCSchemaModule:1:0 ../../schema4G/kcs/data/standard/KCS_DeclarationOfCAG_5LCSchemaModule_1.0_standard.xsd'> ";
            //    <!--반입신고번호-->
            sXml += "    <wco:ID>000000000000000000</wco:ID> ";
            //    <!--신청일자(CCYYMMDD)--> 
            sXml += "    <wco:IssueDateTime>20130203</wco:IssueDateTime> ";
            //    <!--문서형태구분(5LC : 반입신고정정신청서)-->
            sXml += "    <wco:TypeCode>GOVCBR5LC</wco:TypeCode> ";
            //    <!--신청차수-->
            sXml += "    <wco:VersionID>3</wco:VersionID> ";
            //    <!--신청구분(01:정정, 03:삭제)-->
            sXml += "    <kcs:SubTypeCode>01</kcs:SubTypeCode> ";
            //	<!--정정사유-->
            sXml += "	<kcs:Reason>정정사유</kcs:Reason> ";
            //	<!--0..99 반복-->
            sXml += "    <wco:Amendment> ";
            //        <!--정정전-->
            sXml += "        <wco:StatementDescription>123</wco:StatementDescription> ";
            //        <!--정정후 -->
            sXml += "        <kcs:AdjustmentDescription>321</kcs:AdjustmentDescription> ";
            sXml += "        <wco:Pointer> ";
            //            <!--항목번호-->
            sXml += "            <wco:TagID>1</wco:TagID> ";
            sXml += "        </wco:Pointer> ";
            sXml += "    </wco:Amendment> ";
            sXml += "    <wco:Submitter> ";
            //        <!--신청자 성명-->
            sXml += "        <wco:Name>김신청</wco:Name> ";
            sXml += "    </wco:Submitter> ";
            sXml += "    <wco:UCR> ";
            //        <!--화물관리번호-->
            sXml += "        <kcs:CustomsAssignedReferenceID>00000000000000000</kcs:CustomsAssignedReferenceID> ";
            sXml += "    </wco:UCR> ";
            sXml += "</wco:Declaration> ";

            return sXml;
        }
        #endregion

        #region Description : XML 내국화물반입신고 정의서
        public static string UP_Get_XmlGOVCBR5HA(string sFunctionCode,         //전자문서 기능
                                                 string sID,                   //제출번호
                                                 string sLoadingListQuantity,  //반입신고건수
                                                 string sTypeCode,             //문서형태구분
                                                 string sSubTypeCode,          //보세구역구분(A:자율관리보세구역, B:비자율관리보세구역)
                                                 string sAdditionalDocumentID,           //반입신고번호
                                                 string sAdditionalInformationContent,    //장치사유
                                                 string sStatementCode,                   //반입물품종류부호
                                                 string sCargoDescription,                //품명
                                                 string sCountQuantity,                   //반입개수
                                                 string sSizeMeasure,                     //반입중량
                                                 string sCountryCode,                     //원산지
                                                 string sConsignorName,                   //화주 상호
                                                 string sCountrySubDivisionID,            //화주 도로명코드 
                                                 string sLine,                            //화주 상세주소
                                                 string sPostcodeID,                      //화주 우편번호
                                                 string sBuildingNumber,                  //화주 건물관리번호
                                                 string sAddressDescription,              //화주 기본주소 
                                                 string sName,                            //화주 성명
                                                 string sPackagingTypeCode,               //포장부호
                                                 string sArrivalDateTime,                 //반입일자
                                                 string sWarehouseName,                   //장치위치
                                                 string sDepartureDateTime                //장치기간 종료일
            )
        {
            string sXml = string.Empty;

            sXml += "<?xml version='1.0' encoding='UTF-8'?> ";
            sXml += "<wco:Declaration ";
            sXml += "    xmlns:kcs='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5HASchemaModule:1:0' ";
            sXml += "    xmlns:wco='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5HASchemaModule:1:0' ";
            sXml += "    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5HASchemaModule:1:0 ../../schema4G/kcs/data/standard/KCS_DeclarationOfCAG_5HASchemaModule_1.0_standard.xsd'> ";
                //<!--전자문서 기능(9:원본, 35:재전송)--> 
            sXml += "    <wco:FunctionCode>" + sFunctionCode.Trim() + "</wco:FunctionCode> ";
                //<!--제출번호-->
            sXml += "    <wco:ID>" + sID.Trim() + "</wco:ID> ";
                //<!--반입신고건수-->
            sXml += " <wco:LoadingListQuantity>" + sLoadingListQuantity.Trim() + "</wco:LoadingListQuantity> ";
                //<!--문서형태구분-->
            sXml += "     <wco:TypeCode>" + sTypeCode.Trim() + "</wco:TypeCode> ";
                //<!--보세구역구분(A:자율관리보세구역, B:비자율관리보세구역)-->
            sXml += "     <kcs:SubTypeCode>" + sSubTypeCode.Trim() + "</kcs:SubTypeCode>    ";
                //<!--1..999 반복-->
            sXml += "     <wco:Consignment> ";
            sXml += "         <wco:AdditionalDocument> ";
                        //<!--반입신고번호-->
            sXml += "             <wco:ID>" + sAdditionalDocumentID.Trim() + "</wco:ID> ";
            sXml += "         </wco:AdditionalDocument> ";
            sXml += "         <wco:AdditionalInformation> ";
                        //<!--장치사유-->
            sXml += "             <wco:Content>" + sAdditionalInformationContent.Trim() + "</wco:Content> ";
                        //<!--반입물품종류부호-->
            sXml += "             <wco:StatementCode>" + sStatementCode.Trim() + "</wco:StatementCode> ";
            sXml += "         </wco:AdditionalInformation> ";
            sXml += "         <wco:ConsignmentItem>        "; 
            sXml += "             <wco:Commodity>          ";
                            //<!--품명-->
            sXml += "                 <wco:CargoDescription>" + sCargoDescription.Trim() + "</wco:CargoDescription> ";
                            //<!--반입개수-->
            sXml += "                 <wco:CountQuantity>" + sCountQuantity.Trim() + "</wco:CountQuantity> ";
                            //<!--반입중량-->
            sXml += "                 <wco:SizeMeasure>" + sSizeMeasure.Trim() + "</wco:SizeMeasure> ";
            sXml += "             </wco:Commodity> ";
            sXml += "             <wco:Origin> ";
                            //<!--원산지-->
            sXml += "                 <wco:CountryCode>" + sCountryCode.Trim() + "</wco:CountryCode> ";
            sXml += "             </wco:Origin> ";
            sXml += "         </wco:ConsignmentItem> ";
            sXml += "         <wco:Consignor> ";
                        //<!--화주 상호-->
            sXml += "             <wco:Name>" + sConsignorName.Trim() + "</wco:Name> ";
            sXml += "             <wco:Address> ";
                            //<!--화주 도로명코드-->
            sXml += "                 <wco:CountrySubDivisionID>" + sCountrySubDivisionID.Trim() + "</wco:CountrySubDivisionID> ";
                            //<!--화주 상세주소-->
            sXml += "                 <wco:Line>" + sLine.Trim() + "</wco:Line> ";
                            //<!--화주 우편번호-->
            sXml += "                 <wco:PostcodeID>" + sPostcodeID.Trim() + "</wco:PostcodeID> ";
                            //<!--화주 건물관리번호-->
            sXml += "                 <kcs:BuildingNumber>" + sBuildingNumber.Trim() + "</kcs:BuildingNumber> ";
                            //<!--화주 기본주소-->
            sXml += "                 <kcs:Description>" + sAddressDescription.Trim() + "</kcs:Description> ";
            sXml += "             </wco:Address> ";
            sXml += "             <wco:Contact> ";
                            //<!--화주 성명-->
            sXml += "                 <wco:Name>" + sName.Trim() + "</wco:Name> ";
            sXml += "             </wco:Contact> ";
            sXml += "         </wco:Consignor> ";
            sXml += "         <wco:Packaging> ";
                        //<!--포장부호-->
            sXml += "             <wco:TypeCode>" + sPackagingTypeCode.Trim() + "</wco:TypeCode> ";
            sXml += "         </wco:Packaging> ";
            sXml += "         <wco:Warehouse> ";
                        //<!--반입일자(CCYYMMDD)-->
            sXml += "             <wco:ArrivalDateTime>" + sArrivalDateTime.Trim() + "</wco:ArrivalDateTime> ";
                        //<!--장치위치-->
            sXml += "             <wco:Name>" + sWarehouseName.Trim() + "</wco:Name> ";
                        //<!--장치기간 종료일(CCYYMMDD)-->
            sXml += "             <wco:DepartureDateTime>" + sDepartureDateTime.Trim() + "</wco:DepartureDateTime> ";
            sXml += "         </wco:Warehouse> ";
            sXml += "     </wco:Consignment> ";
                //<!--1..999 반복-->

            sXml += " </wco:Declaration> ";

            return sXml;
        }
        #endregion

        #region Description : XML 내국화물반출신고 정의서
        public static string UP_Get_XmlGOVCBR5HB(string sFunctionCode,         //전자문서 기능
                                                 string sID,                   //제출번호
                                                 string sLoadingListQuantity,  //반입신고건수
                                                 string sTypeCode,             //문서형태구분
                                                 string sSubTypeCode,          //보세구역구분(A:자율관리보세구역, B:비자율관리보세구역)
                                                 string sAdditionalDocumentID,           //반출신고번호
                                                 string sAdditionalInformationContent,    //반출사유
                                                 string sStatementCode,                   //반출물품종류부호
                                                 string sCargoDescription,                //품명
                                                 string sCountQuantity,                   //반출개수
                                                 string sSizeMeasure,                     //반출중량
                                                 string sConsignorName,                   //화주 상호
                                                 string sCountrySubDivisionID,            //화주 도로명코드 
                                                 string sLine,                            //화주 상세주소
                                                 string sPostcodeID,                      //화주 우편번호
                                                 string sBuildingNumber,                  //화주 건물관리번호
                                                 string sAddressDescription,              //화주 기본주소 
                                                 string sName,                            //화주 성명
                                                 string sPreviousDocumentID,               //반입신고번호
                                                 string sDepartureDateTime                 //반출일자
            )
        {
            string sXml = string.Empty;

            sXml += "<?xml version='1.0' encoding='UTF-8'?> ";
            sXml += "<wco:Declaration  ";
            sXml += "    xmlns:kcs='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5HBSchemaModule:1:0'  ";
            sXml += "    xmlns:wco='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5HBSchemaModule:1:0'  ";
            sXml += "    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5HBSchemaModule:1:0 ../../schema4G/kcs/data/standard/KCS_DeclarationOfCAG_5HBSchemaModule_1.0_standard.xsd'> ";
                //<!--전자문서 기능(9:원본, 35:재전송)--> 
            sXml += "    <wco:FunctionCode>" + sFunctionCode.Trim() + "</wco:FunctionCode> ";
                //<!--제출번호-->
            sXml += "    <wco:ID>" + sID.Trim() + "</wco:ID> ";
                //<!--반출신고건수-->
            sXml += "    <wco:LoadingListQuantity>" + sLoadingListQuantity.Trim() + "</wco:LoadingListQuantity> ";
                //<!--문서형태구분-->
            sXml += "    <wco:TypeCode>" + sTypeCode.Trim() + "</wco:TypeCode> ";
                //<!--보세구역구분(A:자율관리보세구역, B:비자율관리보세구역)-->
            sXml += "    <kcs:SubTypeCode>" + sSubTypeCode.Trim() + "</kcs:SubTypeCode> ";
            sXml += "    <wco:Consignment> ";
            sXml += "        <wco:AdditionalDocument> ";
                        //<!--반출신고번호-->
            sXml += "            <wco:ID>" + sAdditionalDocumentID.Trim() + "</wco:ID> ";
            sXml += "        </wco:AdditionalDocument>       ";
            sXml += "        <wco:AdditionalInformation>     ";
                        //<!--반출사유-->
            sXml += "            <wco:Content>" + sAdditionalInformationContent.Trim() + "</wco:Content> ";
                        //<!--반입물품종류부호-->
            sXml += "            <wco:StatementCode>" + sStatementCode.Trim() + "</wco:StatementCode> ";
            sXml += "        </wco:AdditionalInformation> ";
            sXml += "        <wco:ConsignmentItem> ";
            sXml += "            <wco:Commodity>   ";
                            //<!--품명-->
            sXml += "                <wco:CargoDescription>" + sCargoDescription.Trim() + "</wco:CargoDescription> ";
                            //<!--반출개수-->
            sXml += "                <wco:CountQuantity>" + sCountQuantity.Trim() + "</wco:CountQuantity> ";
                            //<!--반출중량-->
            sXml += "                <wco:SizeMeasure>" + sSizeMeasure.Trim() + "</wco:SizeMeasure> "; 
            sXml += "            </wco:Commodity>   ";
            sXml += "        </wco:ConsignmentItem> ";
            sXml += "        <wco:Consignor>        ";
                        //<!--화주 상호-->
            sXml += "            <wco:Name>" + sConsignorName.Trim() + "</wco:Name> ";
            sXml += "            <wco:Address>             ";
                            //<!--화주 도로명코드-->
            sXml += "                <wco:CountrySubDivisionID>" + sCountrySubDivisionID.Trim() + "</wco:CountrySubDivisionID> ";
                            //<!--화주 상세주소-->
            sXml += "                <wco:Line>" + sLine.Trim() + "</wco:Line> ";
                            //<!--화주 우편번호-->
            sXml += "                <wco:PostcodeID>" + sPostcodeID.Trim() + "</wco:PostcodeID> ";
                            //<!--화주 건물관리번호-->
            sXml += "                <kcs:BuildingNumber>" + sBuildingNumber.Trim() + "</kcs:BuildingNumber> ";
                            //<!--화주 기본주소-->
            sXml += "                <kcs:Description>" + sAddressDescription.Trim() + "</kcs:Description> ";
            sXml += "            </wco:Address> ";
            sXml += "            <wco:Contact>  ";
                            //<!--화주 성명-->
            sXml += "                <wco:Name>" + sName.Trim() + "</wco:Name> ";
            sXml += "            </wco:Contact> ";
            sXml += "        </wco:Consignor> ";
            sXml += "        <wco:PreviousDocument> ";
                        //<!--반입신고번호-->
            sXml += "            <wco:ID>" + sPreviousDocumentID.Trim() + "</wco:ID> ";
            sXml += "        </wco:PreviousDocument> ";
            sXml += "        <wco:Warehouse> ";
                        //<!--반출일자(CCYYMMDD)-->
            sXml += "            <wco:DepartureDateTime>" + sDepartureDateTime.Trim() + "</wco:DepartureDateTime> ";
            sXml += "        </wco:Warehouse> ";
            sXml += "    </wco:Consignment>   ";
            sXml += "</wco:Declaration>  ";

            return sXml;
        }
        #endregion

        #region Description : XML 반출입정정 신청서 정의서(수입)
        public static string UP_Get_XmlGOVCBR5LCD( string sID,                   //제출번호  
                                                   string sIssueDateTime,        //신청일자                              
                                                   string sTypeCode,             //문서형태구분
                                                   string sVersionID,            //신청차수
                                                   string sSubTypeCode,          //신청구분(01:정정, 03:삭제)
                                                   string sReason,               //정정사유  
                                                   string sSubmitterName,        //신청자 성명
                                                   string sCustomsAssignedReferenceID, //화물관리번호
                                                   string[] _AmendmentList             //정정항목
            )
        {
            string sXml = string.Empty;

            sXml += "<?xml version='1.0' encoding='UTF-8'?> ";
            sXml += "<wco:Declaration ";
            sXml += "    xmlns:kcs='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5LCSchemaModule:1:0' ";
            sXml += "    xmlns:wco='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5LCSchemaModule:1:0' ";
            sXml += "    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5LCSchemaModule:1:0 ../../schema4G/kcs/data/standard/KCS_DeclarationOfCAG_5LCSchemaModule_1.0_standard.xsd'> ";
            //    <!--반출입신고번호-->
            sXml += "    <wco:ID>"+sID.Trim()+"</wco:ID> ";
            //    <!--신청일자(CCYYMMDD)-->
            sXml += "    <wco:IssueDateTime>" + sIssueDateTime.Trim() + "</wco:IssueDateTime> ";
            //    <!--문서형태구분(5LC, 5LD : 반출입신고정정신청서)-->
            sXml += "    <wco:TypeCode>" + sTypeCode.Trim() + "</wco:TypeCode> ";
            //    <!--신청차수-->
            sXml += "    <wco:VersionID>" + sVersionID.Trim() + "</wco:VersionID> ";
            //    <!--신청구분(01:정정, 03:삭제)-->
            sXml += "    <kcs:SubTypeCode>" + sSubTypeCode.Trim() + "</kcs:SubTypeCode> ";
	        //    <!--정정사유-->
            sXml += "	<kcs:Reason>" + sReason.Trim() + "</kcs:Reason>	 ";

            for (int i = 0; i < _AmendmentList.Length; i++)
            {
                string[] _AmendmentItem = _AmendmentList[i].Split(';');

                if (_AmendmentItem.Length > 1)
                {
                    //    <!--0..99 반복-->
                    sXml += "    <wco:Amendment> ";
                    //        <!--정정전-->
                    sXml += "        <wco:StatementDescription>" + _AmendmentItem[1].ToString().Trim() + "</wco:StatementDescription> ";
                    //        <!--정정후 -->
                    sXml += "        <kcs:AdjustmentDescription>" + _AmendmentItem[2].ToString().Trim() + "</kcs:AdjustmentDescription> ";
                    sXml += "        <wco:Pointer> ";
                    //            <!--항목번호-->
                    sXml += "            <wco:TagID>" + _AmendmentItem[0].ToString().Trim() + "</wco:TagID> ";
                    sXml += "        </wco:Pointer> ";
                    sXml += "    </wco:Amendment> ";
                }
            }

            sXml += "    <wco:Submitter> ";
            //        <!--신청자 성명-->
            sXml += "        <wco:Name>" + sSubmitterName.Trim() + "</wco:Name> ";
            sXml += "    </wco:Submitter> ";
            sXml += "    <wco:UCR> ";
            //       <!--화물관리번호-->
            sXml += "        <kcs:CustomsAssignedReferenceID>" + sCustomsAssignedReferenceID.Trim() + "</kcs:CustomsAssignedReferenceID> ";
            sXml += "    </wco:UCR> ";
            sXml += "</wco:Declaration> ";            

            return sXml;
        }
        #endregion

        #region Description : XML 반출입정정 신청서 정의서(내국)
        public static string UP_Get_XmlGOVCBR0045(string sID,                   //제출번호  
                                                   string sIssueDateTime,        //신청일자                              
                                                   string sTypeCode,             //문서형태구분
                                                   string sVersionID,            //신청차수
                                                   string sSubTypeCode,          //신청구분(01:정정, 03:삭제)
                                                   string sReason,               //정정사유  
                                                   string sSubmitterName,        //신청자 성명                                                   
                                                   string[] _AmendmentList             //정정항목
            )
        {
            string sXml = string.Empty;

            sXml += "<?xml version='1.0' encoding='UTF-8'?> ";
            sXml += "<wco:Declaration ";
            sXml += "    xmlns:kcs='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5LCSchemaModule:1:0' ";
            sXml += "    xmlns:wco='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5LCSchemaModule:1:0' ";
            sXml += "    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5LCSchemaModule:1:0 ../../schema4G/kcs/data/standard/KCS_DeclarationOfCAG_5LCSchemaModule_1.0_standard.xsd'> ";
            //    <!--반출입신고번호-->
            sXml += "    <wco:ID>" + sID.Trim() + "</wco:ID> ";
            //    <!--신청일자(CCYYMMDD)-->
            sXml += "    <wco:IssueDateTime>" + sIssueDateTime.Trim() + "</wco:IssueDateTime> ";
            //    <!--문서형태구분(004, 005 : 반출입신고정정신청서)-->
            sXml += "    <wco:TypeCode>" + sTypeCode.Trim() + "</wco:TypeCode> ";
            //    <!--신청차수-->
            sXml += "    <wco:VersionID>" + sVersionID.Trim() + "</wco:VersionID> ";
            //    <!--신청구분(01:정정, 03:삭제)-->
            sXml += "    <kcs:TransactionNatureCode>" + sSubTypeCode.Trim() + "</kcs:TransactionNatureCode> ";
            //    <!--정정사유-->
            sXml += "	<kcs:Reason>" + sReason.Trim() + "</kcs:Reason>	 ";

            for (int i = 0; i < _AmendmentList.Length; i++)
            {
                string[] _AmendmentItem = _AmendmentList[i].Split(';');

                if (_AmendmentItem.Length > 1)
                {
                    //    <!--0..99 반복-->
                    sXml += "    <wco:Amendment> ";
                    //        <!--정정전-->
                    sXml += "        <wco:StatementDescription>" + _AmendmentItem[1].ToString().Trim() + "</wco:StatementDescription> ";
                    //        <!--정정후 -->
                    sXml += "        <kcs:AdjustmentDescription>" + _AmendmentItem[2].ToString().Trim() + "</kcs:AdjustmentDescription> ";
                    sXml += "        <wco:Pointer> ";
                    //            <!--항목번호-->
                    sXml += "            <wco:TagID>" + _AmendmentItem[0].ToString().Trim() + "</wco:TagID> ";
                    sXml += "        </wco:Pointer> ";
                    sXml += "    </wco:Amendment> ";
                }
            }

            sXml += "    <wco:Submitter> ";
            //        <!--신청자 성명-->
            sXml += "        <wco:Name>" + sSubmitterName.Trim() + "</wco:Name> ";
            sXml += "    </wco:Submitter> ";

            sXml += "</wco:Declaration> ";

            return sXml;
        }
        #endregion

        #region Description : XML 반출기간연장신청서 정의서
        public static string UP_Get_XmlGOVCBR5HM(string sDeclarationOfficeID,  //신고세관
                                                 string sFunctionCode,         //전자문서 기능(9:원본, 35:재전송)
                                                 string sID,                   //제출번호
                                                 string sIssueDateTime,        //신청일자
                                                 string sTypeCode,             //문서형태구분
                                                 string sTransactionNatureCode,  //신청구분
                                                 string sReason,                 //연장사유
                                                 string sStatementCode,          //연장기간구분(A:30일, B:60일. Z:연장기간입력)
                                                 string sBeginningDateTime,       //연장기간시작일
                                                 string sEndingDateTime,          //연장기간종료일
                                                 string sWarehouseID,             //장치장소
                                                 string sPreviousDocumentID,      //수입신고번호
                                                 string sSubmitterName,           //신청자 성명
                                                 string sCustomsAssignedReferenceID   //화물관리번호
           )
        {
            string sXml = string.Empty;

            sXml += "<?xml version='1.0' encoding='UTF-8'?> ";
            sXml += "<wco:Declaration  ";
            sXml += "    xmlns:kcs='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5HMSchemaModule:1:0'  ";
            sXml += "    xmlns:wco='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5HMSchemaModule:1:0'  ";
            sXml += "    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5HMSchemaModule:1:0 ../../schema4G/kcs/data/standard/KCS_DeclarationOfCAG_5HMSchemaModule_1.0_standard.xsd'>  ";
            //    <!--신고세관/과-->
            sXml += "    <wco:DeclarationOfficeID>"+sDeclarationOfficeID.Trim()+"</wco:DeclarationOfficeID> ";
            //    <!--전자문서 기능(9:원본, 35:재전송)-->
            sXml += "    <wco:FunctionCode>"+sFunctionCode.Trim()+"</wco:FunctionCode> ";
            //    <!--제출번호-->
            sXml += "    <wco:ID>"+sID.Trim()+"</wco:ID> ";
            //    <!--신청일자(CCYYMMDD)-->
            sXml += "    <wco:IssueDateTime>"+sIssueDateTime.Trim()+"</wco:IssueDateTime> ";
            //    <!--문서형태구분(GOVCBR5HM : 반출기간연장신청)-->
            sXml += "    <wco:TypeCode>"+sTypeCode+"</wco:TypeCode> ";
            //    <!--신청구분(1:신규신청, 2:변경신청)-->
            sXml += "    <wco:TransactionNatureCode>"+sTransactionNatureCode.Trim()+"</wco:TransactionNatureCode> ";
            //    <!--연장사유-->
            sXml += "    <kcs:Reason>"+sReason.Trim()+"</kcs:Reason> ";
            sXml += "    <wco:AdditionalInformation>       ";
            //        <!--연장기간구분(A:30일, B:60일. Z:연장기간입력)-->
            sXml += "        <wco:StatementCode>"+sStatementCode.Trim()+"</wco:StatementCode> ";
            if (sStatementCode.Trim() != "Z")
            {
                //        <!--연장기간시작일(CCYYMMDD)-->
                sXml += "        <kcs:BeginningDateTime>"+sBeginningDateTime.Trim()+"</kcs:BeginningDateTime> ";
                //        <!--연장기간종료일(CCYYMMDD)-->
                sXml += "        <kcs:EndingDateTime>"+sEndingDateTime.Trim()+"</kcs:EndingDateTime> ";
            }
            sXml += "    </wco:AdditionalInformation> ";
            sXml += "    <wco:Consignment> ";
            sXml += "        <wco:Warehouse> ";
            //            <!--장치장소-->
            sXml += "            <wco:ID>"+sWarehouseID.Trim()+"</wco:ID> ";
            sXml += "        </wco:Warehouse> ";
            sXml += "    </wco:Consignment>   ";
            sXml += "    <wco:PreviousDocument> ";
            //        <!--수입신고번호-->
            sXml += "        <wco:ID>"+sPreviousDocumentID.Trim()+"</wco:ID> ";
            sXml += "    </wco:PreviousDocument> ";
            sXml += "    <wco:Submitter>         ";
            //        <!--신청자 성명-->
            sXml += "        <wco:Name>"+sSubmitterName.Trim()+"</wco:Name> ";
            sXml += "    </wco:Submitter> ";
            sXml += "    <wco:UCR>        "; 
            //        <!--화물관리번호-->
            sXml += "        <kcs:CustomsAssignedReferenceID>"+sCustomsAssignedReferenceID.Trim()+"</kcs:CustomsAssignedReferenceID>  ";
            sXml += "    </wco:UCR>     ";
            sXml += "</wco:Declaration> ";

            return sXml;
        }
        #endregion

        #region Description : XML 반출통고목록서 정의서
        public static string UP_Get_XmlGOVCBR5II(string sDeclarationOfficeID,  //신고세관/과
                                                 string sFunctionCode,         //전자문서 기능(9:원본, 35:재전송)
                                                 string sID,                   //-화물관리번호
                                                 string sIssueDateTime,        //반출통고일자
                                                 string sTotalGrossMassMeasure,             //반입중량
                                                 string sTotalPackageQuantity,  //반입개수
                                                 string sTypeCode,                 //문서형태구분
                                                 string sLimitDateTime,          //체화예정일자
                                                 string sWarehouseID,       //반입장소
                                                 string sImporterName,          //  화주명
                                                 string sImporterCountrySubDivisionID,          //화주 도로명코드
                                                 string sImporterLine,             //화주 상세주소
                                                 string sImporterPostcodeID,       //화주 우편번호
                                                 string sImporterBuildingNumber,   //화주 건물관리번호
                                                 string sImporterDescription,   //화주 기본주소
                                                 string sBirthDate,   //화주 생년월일,
                                                 string sCommunicationID,   //화주 전화번호
                                                 string sTransportContractDocumentID   //B/L번호
           )
        {
            string sXml = string.Empty;

            sXml += "<?xml version='1.0' encoding='UTF-8'?> ";
            sXml += "<wco:Declaration  ";
            sXml += "    xmlns:kcs='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5HMSchemaModule:1:0'  ";
            sXml += "    xmlns:wco='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5HMSchemaModule:1:0'  ";
            sXml += "    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='urn:kr:gov:kcs:data:standard:KCS_DeclarationOfCAG_5HMSchemaModule:1:0 ../../schema4G/kcs/data/standard/KCS_DeclarationOfCAG_5HMSchemaModule_1.0_standard.xsd'>  ";
            //    <!--신고세관/과-->
            sXml += "    <wco:DeclarationOfficeID>" + sDeclarationOfficeID.Trim() + "</wco:DeclarationOfficeID> ";
            //    <!--전자문서 기능(9:원본, 35:재전송)-->
            sXml += "    <wco:FunctionCode>" + sFunctionCode.Trim() + "</wco:FunctionCode> ";
            //    <!---화물관리번호---->
            sXml += "    <wco:ID>" + sID.Trim() + "</wco:ID> ";
            // <!--반출통고일자(CCYYMMDD)-->
            sXml += "<wco:IssueDateTime>" + sIssueDateTime + "</wco:IssueDateTime> ";
            //<!--반입중량-->
            sXml += " <wco:TotalGrossMassMeasure>" + sTotalGrossMassMeasure + "</wco:TotalGrossMassMeasure> ";
            //<!--반입개수-->
            sXml += " <wco:TotalPackageQuantity>" + sTotalPackageQuantity + "</wco:TotalPackageQuantity> ";
            //<!--문서형태구분-->
            sXml += " <wco:TypeCode>GOVCBR5II</wco:TypeCode> ";
            sXml += " <wco:AdditionalInformation> ";
            //<!--체화예정일자(CCYYMMDD)-->
            sXml += " <wco:LimitDateTime>" + sLimitDateTime + "</wco:LimitDateTime> ";
            sXml += " </wco:AdditionalInformation> ";

            sXml += " <wco:Consignment> ";
                sXml += " <wco:Warehouse> ";
                //<!--반입장소-->
                sXml += " <wco:ID>" + sWarehouseID + "</wco:ID> ";
                sXml += " </wco:Warehouse> ";
            sXml += " </wco:Consignment> ";

            sXml += " <wco:Importer> ";
                //<!--화주  화주명-->
                sXml += " <wco:Name>" + sImporterName + "</wco:Name> ";
		        sXml += " <wco:Address> ";
			    //<!--화주 도로명코드-->
			    sXml += " <wco:CountrySubDivisionID>" + sImporterCountrySubDivisionID + "</wco:CountrySubDivisionID> ";
			    //<!--화주 상세주소-->
			    sXml += " <wco:Line>" + sImporterLine + "</wco:Line> ";
			    //<!--화주 우편번호-->
			    sXml += " <wco:PostcodeID>" +sImporterPostcodeID+ "</wco:PostcodeID> ";
			    //<!--화주 건물관리번호-->
			    sXml += " <kcs:BuildingNumber>"+sImporterBuildingNumber+ "</kcs:BuildingNumber> ";
			    //<!--화주 기본주소-->
			    sXml += " <kcs:Description>"+ sImporterDescription + "</kcs:Description> ";
		        sXml += " </wco:Address> ";
                sXml += " <wco:Contact> ";
                //<!--화주 생년월일-->
                sXml += " <kcs:BirthDate>"+ sBirthDate +"</kcs:BirthDate> ";
                sXml += " </wco:Contact> ";
                sXml += " <wco:Communication> ";
                //<!--화주  전화번호-->
                sXml += " <wco:ID>" + sCommunicationID + "</wco:ID> ";
                sXml += " </wco:Communication> ";
            sXml += " </wco:Importer> ";

            sXml += " <wco:TransportContractDocument> ";
              //<!--B/L 번호-->
              sXml += " <wco:ID>"+sTransportContractDocumentID+"</wco:ID> ";
            sXml += " </wco:TransportContractDocument> ";            

            sXml += "</wco:Declaration> ";

            return sXml;
        }
        #endregion

    }

   
}
