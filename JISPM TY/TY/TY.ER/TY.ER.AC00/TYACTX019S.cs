using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 세무구분별 매입명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.14 16:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25EAH372 : 세무구분별 매입명세서 조회
    ///  TY_P_AC_25G19489 : 세무구분별 매입명세서 출력
    ///  TY_P_AC_25H3V532 : 세무구분별 매입명세서 집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25E4Y431 : 세무구분별 매입명세서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25GAZ484 : 세무 구분을 선택하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  VNCODE : 거래처코드
    ///  B4VLMI1 : 관리항목값１
    ///  B4VLMI2 : 관리항목값２
    ///  B4VLMI4 : 관리항목값４
    ///  GDATEGUBUN : 일자구분
    ///  CBO01_GPRTGN : 출력구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACTX019S : TYBase
    {
        private TYData DAT02_VSYEAR;
        private TYData DAT02_VSBRANCH;
        private TYData DAT02_VSVENDCD;
        private TYData DAT02_VSRPTGUBN;
        private TYData DAT02_VSCONFGB;
        private TYData DAT02_VS01ST01AMT;
        private TYData DAT02_VS01ST01NEM;
        private TYData DAT02_VS01ST01DEN;
        private TYData DAT02_VS01ST01TAX;
        private TYData DAT02_VS01ST02AMT;
        private TYData DAT02_VS01ST02NEM;
        private TYData DAT02_VS01ST02DEN;
        private TYData DAT02_VS01ST02TAX;
        private TYData DAT02_VS01ST03AMT;
        private TYData DAT02_VS01ST03NEM;
        private TYData DAT02_VS01ST03DEN;
        private TYData DAT02_VS01ST03TAX;
        private TYData DAT02_VS01ST04AMT;
        private TYData DAT02_VS01ST04NEM;
        private TYData DAT02_VS01ST04DEN;
        private TYData DAT02_VS01ST04TAX;
        private TYData DAT02_VS01ST05AMT;
        private TYData DAT02_VS01ST05NEM;
        private TYData DAT02_VS01ST05DEN;
        private TYData DAT02_VS01ST05TAX;
        private TYData DAT02_VS01ST06AMT;
        private TYData DAT02_VS01ST06NEM;
        private TYData DAT02_VS01ST06DEN;
        private TYData DAT02_VS01ST06TAX;
        private TYData DAT02_VS01ST07AMT;
        private TYData DAT02_VS01ST07NEM;
        private TYData DAT02_VS01ST07DEN;
        private TYData DAT02_VS01ST07TAX;
        private TYData DAT02_VS01ST08AMT;
        private TYData DAT02_VS01ST08NEM;
        private TYData DAT02_VS01ST08DEN;
        private TYData DAT02_VS01ST08TAX;
        private TYData DAT02_VS01ST09AMT;
        private TYData DAT02_VS01ST09NEM;
        private TYData DAT02_VS01ST09DEN;
        private TYData DAT02_VS01ST09TAX;
        private TYData DAT02_VS02PU10AMT;
        private TYData DAT02_VS02PU10NEM;
        private TYData DAT02_VS02PU10DEN;
        private TYData DAT02_VS02PU10TAX;
        private TYData DAT02_VS02PU11AMT;
        private TYData DAT02_VS02PU11NEM;
        private TYData DAT02_VS02PU11DEN;
        private TYData DAT02_VS02PU11TAX;
        private TYData DAT02_VS02PU12AMT;
        private TYData DAT02_VS02PU12NEM;
        private TYData DAT02_VS02PU12DEN;
        private TYData DAT02_VS02PU12TAX;
        private TYData DAT02_VS02PU13AMT;
        private TYData DAT02_VS02PU13NEM;
        private TYData DAT02_VS02PU13DEN;
        private TYData DAT02_VS02PU13TAX;
        private TYData DAT02_VS02PU14AMT;
        private TYData DAT02_VS02PU14NEM;
        private TYData DAT02_VS02PU14DEN;
        private TYData DAT02_VS02PU14TAX;
        private TYData DAT02_VS02PU15AMT;
        private TYData DAT02_VS02PU15NEM;
        private TYData DAT02_VS02PU15DEN;
        private TYData DAT02_VS02PU15TAX;
        private TYData DAT02_VS02PU16AMT;
        private TYData DAT02_VS02PU16NEM;
        private TYData DAT02_VS02PU16DEN;
        private TYData DAT02_VS02PU16TAX;
        private TYData DAT02_VS02PU17AMT;
        private TYData DAT02_VS02PU17NEM;
        private TYData DAT02_VS02PU17DEN;
        private TYData DAT02_VS02PU17TAX;
        private TYData DAT02_VSPAYMENEM;
        private TYData DAT02_VSPAYMEDEN;
        private TYData DAT02_VSPAYMETAX;
        private TYData DAT02_VS03RE18AMT;
        private TYData DAT02_VS03RE18NEM;
        private TYData DAT02_VS03RE18DEN;
        private TYData DAT02_VS03RE18TAX;
        private TYData DAT02_VS03RE19AMT;
        private TYData DAT02_VS03RE19NEM;
        private TYData DAT02_VS03RE19DEN;
        private TYData DAT02_VS03RE19TAX;
        private TYData DAT02_VS03RE20AMT;
        private TYData DAT02_VS03RE20NEM;
        private TYData DAT02_VS03RE20DEN;
        private TYData DAT02_VS03RE20TAX;
        private TYData DAT02_VS04SC21AMT;
        private TYData DAT02_VS04SC21NEM;
        private TYData DAT02_VS04SC21DEN;
        private TYData DAT02_VS04SC21TAX;
        private TYData DAT02_VS04SC22AMT;
        private TYData DAT02_VS04SC22NEM;
        private TYData DAT02_VS04SC22DEN;
        private TYData DAT02_VS04SC22TAX;

        private TYData DAT02_VS04YC23AMT; // 추가(2014.04.22)
        private TYData DAT02_VS04YC23NEM; // 추가(2014.04.22)
        private TYData DAT02_VS04YC23DEN; // 추가(2014.04.22)
        private TYData DAT02_VS04YC23TAX; // 추가(2014.04.22)

        private TYData DAT02_VS04SC23AMT;
        private TYData DAT02_VS04SC23NEM;
        private TYData DAT02_VS04SC23DEN;
        private TYData DAT02_VS04SC23TAX;
        private TYData DAT02_VS04SC24AMT;
        private TYData DAT02_VS04SC24NEM;
        private TYData DAT02_VS04SC24DEN;
        private TYData DAT02_VS04SC24TAX;
        private TYData DAT02_VS04SC25TAX;
        private TYData DAT02_VSTOTTAXAMT;
        private TYData DAT02_VSREBANKCD;
        private TYData DAT02_VSREACCCODE;
        private TYData DAT02_VSSHUTDATE;
        private TYData DAT02_VSSHUTSAYU;
        private TYData DAT02_VS05EV26BUS;
        private TYData DAT02_VS05EV26NAM;
        private TYData DAT02_VS05EV26COD;
        private TYData DAT02_VS05EV26AMT;
        private TYData DAT02_VS05EV27BUS;
        private TYData DAT02_VS05EV27NAM;
        private TYData DAT02_VS05EV27COD;
        private TYData DAT02_VS05EV27AMT;
        private TYData DAT02_VS05EV28BUS;
        private TYData DAT02_VS05EV28NAM;
        private TYData DAT02_VS05EV28COD;
        private TYData DAT02_VS05EV28AMT;
        private TYData DAT02_VS05EV29BUS;
        private TYData DAT02_VS05EV29NAM;
        private TYData DAT02_VS05EV29COD;
        private TYData DAT02_VS05EV29AMT;
        private TYData DAT02_VS05EV30AMT;
        private TYData DAT02_VS06MI31AMT;
        private TYData DAT02_VS06MI31NEM;
        private TYData DAT02_VS06MI31DEN;
        private TYData DAT02_VS06MI31TAX;
        private TYData DAT02_VS06MI32AMT;
        private TYData DAT02_VS06MI32NEM;
        private TYData DAT02_VS06MI32DEN;
        private TYData DAT02_VS06MI32TAX;
        private TYData DAT02_VS06MI33AMT;
        private TYData DAT02_VS06MI33NEM;
        private TYData DAT02_VS06MI33DEN;
        private TYData DAT02_VS06MI33TAX;
        private TYData DAT02_VS06MI34AMT;
        private TYData DAT02_VS06MI34NEM;
        private TYData DAT02_VS06MI34DEN;
        private TYData DAT02_VS06MI34TAX;
        private TYData DAT02_VS06MI35AMT;
        private TYData DAT02_VS06MI35NEM;
        private TYData DAT02_VS06MI35DEN;
        private TYData DAT02_VS06MI35TAX;
        private TYData DAT02_VS06MI36AMT;
        private TYData DAT02_VS06MI36NEM;
        private TYData DAT02_VS06MI36DEN;
        private TYData DAT02_VS06MI36TAX;
        private TYData DAT02_VS06MI37AMT;
        private TYData DAT02_VS06MI37NEM;
        private TYData DAT02_VS06MI37DEN;
        private TYData DAT02_VS06MI37TAX;
        private TYData DAT02_VS06MI38AMT;
        private TYData DAT02_VS06MI38NEM;
        private TYData DAT02_VS06MI38DEN;
        private TYData DAT02_VS06MI38TAX;
        private TYData DAT02_VS14ET39AMT;
        private TYData DAT02_VS14ET39NEM;
        private TYData DAT02_VS14ET39DEN;
        private TYData DAT02_VS14ET39TAX;
        private TYData DAT02_VS14ET40AMT;
        private TYData DAT02_VS14ET40NEM;
        private TYData DAT02_VS14ET40DEN;
        private TYData DAT02_VS14ET40TAX;
        private TYData DAT02_VS14ET41AMT;
        private TYData DAT02_VS14ET41NEM;
        private TYData DAT02_VS14ET41DEN;
        private TYData DAT02_VS14ET41TAX;
        private TYData DAT02_VS14ET42AMT;
        private TYData DAT02_VS14ET42NEM;
        private TYData DAT02_VS14ET42DEN;
        private TYData DAT02_VS14ET42TAX;
        //private TYData DAT02_VS14ET43AMT;
        //private TYData DAT02_VS14ET43NEM;
        //private TYData DAT02_VS14ET43DEN;
        //private TYData DAT02_VS14ET43TAX;
        private TYData DAT02_VS14ET44AMT;
        private TYData DAT02_VS14ET44NEM;
        private TYData DAT02_VS14ET44DEN;
        private TYData DAT02_VS14ET44TAX;
        private TYData DAT02_VS14ET45AMT;
        private TYData DAT02_VS14ET45NEM;
        private TYData DAT02_VS14ET45DEN;
        private TYData DAT02_VS14ET45TAX;
        private TYData DAT02_VS14ET46AMT;
        private TYData DAT02_VS14ET46NEM;
        private TYData DAT02_VS14ET46DEN;
        private TYData DAT02_VS14ET46TAX;

        // 2014.02.22 추가
        private TYData DAT02_VS14NT47AMT; //외국인관광객
        private TYData DAT02_VS14NT47NEM; //세율자
        private TYData DAT02_VS14NT47DEN; //세율모
        private TYData DAT02_VS14NT47TAX; //세액

        private TYData DAT02_VS14ET47AMT;
        private TYData DAT02_VS14ET47NEM;
        private TYData DAT02_VS14ET47DEN;
        private TYData DAT02_VS14ET47TAX;
        private TYData DAT02_VS16DE48AMT;
        private TYData DAT02_VS16DE48NEM;
        private TYData DAT02_VS16DE48DEN;
        private TYData DAT02_VS16DE48TAX;
        private TYData DAT02_VS16DE49AMT;
        private TYData DAT02_VS16DE49NEM;
        private TYData DAT02_VS16DE49DEN;
        private TYData DAT02_VS16DE49TAX;
        private TYData DAT02_VS16DE50AMT;
        private TYData DAT02_VS16DE50NEM;
        private TYData DAT02_VS16DE50DEN;
        private TYData DAT02_VS16DE50TAX;
        private TYData DAT02_VS16DE51AMT;
        private TYData DAT02_VS16DE51NEM;
        private TYData DAT02_VS16DE51DEN;
        private TYData DAT02_VS16DE51TAX;
        private TYData DAT02_VS18BE52AMT;
        private TYData DAT02_VS18BE52NEM;
        private TYData DAT02_VS18BE52DEN;
        private TYData DAT02_VS18BE52TAX;
        private TYData DAT02_VS18BE53AMT;
        private TYData DAT02_VS18BE53NEM;
        private TYData DAT02_VS18BE53DEN;
        private TYData DAT02_VS18BE53TAX;
        private TYData DAT02_VS18BE54AMT;
        private TYData DAT02_VS18BE54NEM;
        private TYData DAT02_VS18BE54DEN;
        private TYData DAT02_VS18BE54TAX;
        //private TYData DAT02_VS18BE55AMT;
        //private TYData DAT02_VS18BE55NEM;
        //private TYData DAT02_VS18BE55DEN;
        //private TYData DAT02_VS18BE55TAX;
        private TYData DAT02_VS18BE56AMT;
        private TYData DAT02_VS18BE56NEM;
        private TYData DAT02_VS18BE56DEN;
        private TYData DAT02_VS18BE56TAX;
        private TYData DAT02_VS18BE57AMT;
        private TYData DAT02_VS18BE57NEM;
        private TYData DAT02_VS18BE57DEN;
        private TYData DAT02_VS18BE57TAX;
        private TYData DAT02_VS18BE58AMT;
        private TYData DAT02_VS18BE58NEM;
        private TYData DAT02_VS18BE58DEN;
        private TYData DAT02_VS18BE58TAX;
        private TYData DAT02_VS24AD59AMT;
        private TYData DAT02_VS24AD59NEM;
        private TYData DAT02_VS24AD59DEN;
        private TYData DAT02_VS24AD59TAX;
        private TYData DAT02_VS24AD60AMT;
        private TYData DAT02_VS24AD60NEM;
        private TYData DAT02_VS24AD60DEN;
        private TYData DAT02_VS24AD60TAX;
        private TYData DAT02_VS24AD61AMT;
        private TYData DAT02_VS24AD61NEM;
        private TYData DAT02_VS24AD61DEN;
        private TYData DAT02_VS24AD61TAX;
        private TYData DAT02_VS24AD62AMT;
        private TYData DAT02_VS24AD62NEM;
        private TYData DAT02_VS24AD62DEN;
        private TYData DAT02_VS24AD62TAX;
        private TYData DAT02_VS24AD63AMT;
        private TYData DAT02_VS24AD63NEM;
        private TYData DAT02_VS24AD63DEN;
        private TYData DAT02_VS24AD63TAX;
        private TYData DAT02_VS24AD64AMT;
        private TYData DAT02_VS24AD64NEM;
        private TYData DAT02_VS24AD64DEN;
        private TYData DAT02_VS24AD64TAX;
        private TYData DAT02_VS24AD65AMT;
        private TYData DAT02_VS24AD65NEM;
        private TYData DAT02_VS24AD65DEN;
        private TYData DAT02_VS24AD65TAX;
        private TYData DAT02_VS24AD66AMT;
        private TYData DAT02_VS24AD66NEM;
        private TYData DAT02_VS24AD66DEN;
        private TYData DAT02_VS24AD66TAX;
        private TYData DAT02_VS24AD67AMT;
        private TYData DAT02_VS24AD67NEM;
        private TYData DAT02_VS24AD67DEN;
        private TYData DAT02_VS24AD67TAX;
        private TYData DAT02_VS24AD68AMT;
        private TYData DAT02_VS24AD68NEM;
        private TYData DAT02_VS24AD68DEN;
        private TYData DAT02_VS24AD68TAX;
        private TYData DAT02_VS24AD69AMT;
        private TYData DAT02_VS24AD69NEM;
        private TYData DAT02_VS24AD69DEN;
        private TYData DAT02_VS24AD69TAX;
        private TYData DAT02_VS24AD70AMT;
        private TYData DAT02_VS24AD70NEM;
        private TYData DAT02_VS24AD70DEN;
        private TYData DAT02_VS24AD70TAX;
        private TYData DAT02_VS24AD71AMT;
        private TYData DAT02_VS24AD71NEM;
        private TYData DAT02_VS24AD71DEN;
        private TYData DAT02_VS24AD71TAX;
        private TYData DAT02_VS24AD72AMT;
        private TYData DAT02_VS24AD72NEM;
        private TYData DAT02_VS24AD72DEN;
        private TYData DAT02_VS24AD72TAX;
        private TYData DAT02_VS24AD73AMT;
        private TYData DAT02_VS24AD73NEM;
        private TYData DAT02_VS24AD73DEN;
        private TYData DAT02_VS24AD73TAX;
        private TYData DAT02_VS24AD74AMT;
        private TYData DAT02_VS24AD74NEM;
        private TYData DAT02_VS24AD74DEN;
        private TYData DAT02_VS24AD74TAX;

        private TYData DAT02_VS24AR75AMT; // 추가(2014.04.22) 거래계좌미사용
        private TYData DAT02_VS24AR75NEM; // 추가(2014.04.22) 세율자
        private TYData DAT02_VS24AR75DEN; // 추가(2014.04.22) 세율모
        private TYData DAT02_VS24AR75TAX; // 추가(2014.04.22) 세액
        private TYData DAT02_VS24AR76AMT; // 추가(2014.04.22) 거래계좌지연입금
        private TYData DAT02_VS24AR76NEM; // 추가(2014.04.22) 세율자
        private TYData DAT02_VS24AR76DEN; // 추가(2014.04.22) 세율모
        private TYData DAT02_VS24AR76TAX; // 추가(2014.04.22) 세액

        private TYData DAT02_VS24AD75AMT;
        private TYData DAT02_VS24AD75NEM;
        private TYData DAT02_VS24AD75DEN;
        private TYData DAT02_VS24AD75TAX;
        private TYData DAT02_VS25EX76BUS;
        private TYData DAT02_VS25EX76NAM;
        private TYData DAT02_VS25EX76COD;
        private TYData DAT02_VS25EX76AMT;
        private TYData DAT02_VS25EX77BUS;
        private TYData DAT02_VS25EX77NAM;
        private TYData DAT02_VS25EX77COD;
        private TYData DAT02_VS25EX77AMT;
        private TYData DAT02_VS25EX78BUS;
        private TYData DAT02_VS25EX78NAM;
        private TYData DAT02_VS25EX78COD;
        private TYData DAT02_VS25EX78AMT;
        private TYData DAT02_VS25EX79AMT;
        private TYData DAT02_VS26IS80AMT;
        private TYData DAT02_VS26IS81AMT;

        private TYData DAT02_VSREFUNDGB;

        private int fiRow = 0;

        string fsVSYEAR    = string.Empty;
        string fsVSBRANCH  = string.Empty;
        string fsVSRPTGUBN = string.Empty;
        string fsVSCONFGB  = string.Empty;

        string fsPOPUP     = string.Empty;

        #region Description : 페이지 로드 
        public TYACTX019S()
        {
            InitializeComponent();

            this.DAT02_VSYEAR      = new TYData("DAT02_VSYEAR",      null);
            this.DAT02_VSBRANCH    = new TYData("DAT02_VSBRANCH",    null);
            this.DAT02_VSVENDCD    = new TYData("DAT02_VSVENDCD",    null);
            this.DAT02_VSRPTGUBN   = new TYData("DAT02_VSRPTGUBN",   null);
            this.DAT02_VSCONFGB    = new TYData("DAT02_VSCONFGB",    null);
            this.DAT02_VS01ST01AMT = new TYData("DAT02_VS01ST01AMT", null);
            this.DAT02_VS01ST01NEM = new TYData("DAT02_VS01ST01NEM", null);
            this.DAT02_VS01ST01DEN = new TYData("DAT02_VS01ST01DEN", null);
            this.DAT02_VS01ST01TAX = new TYData("DAT02_VS01ST01TAX", null);
            this.DAT02_VS01ST02AMT = new TYData("DAT02_VS01ST02AMT", null);
            this.DAT02_VS01ST02NEM = new TYData("DAT02_VS01ST02NEM", null);
            this.DAT02_VS01ST02DEN = new TYData("DAT02_VS01ST02DEN", null);
            this.DAT02_VS01ST02TAX = new TYData("DAT02_VS01ST02TAX", null);
            this.DAT02_VS01ST03AMT = new TYData("DAT02_VS01ST03AMT", null);
            this.DAT02_VS01ST03NEM = new TYData("DAT02_VS01ST03NEM", null);
            this.DAT02_VS01ST03DEN = new TYData("DAT02_VS01ST03DEN", null);
            this.DAT02_VS01ST03TAX = new TYData("DAT02_VS01ST03TAX", null);
            this.DAT02_VS01ST04AMT = new TYData("DAT02_VS01ST04AMT", null);
            this.DAT02_VS01ST04NEM = new TYData("DAT02_VS01ST04NEM", null);
            this.DAT02_VS01ST04DEN = new TYData("DAT02_VS01ST04DEN", null);
            this.DAT02_VS01ST04TAX = new TYData("DAT02_VS01ST04TAX", null);
            this.DAT02_VS01ST05AMT = new TYData("DAT02_VS01ST05AMT", null);
            this.DAT02_VS01ST05NEM = new TYData("DAT02_VS01ST05NEM", null);
            this.DAT02_VS01ST05DEN = new TYData("DAT02_VS01ST05DEN", null);
            this.DAT02_VS01ST05TAX = new TYData("DAT02_VS01ST05TAX", null);
            this.DAT02_VS01ST06AMT = new TYData("DAT02_VS01ST06AMT", null);
            this.DAT02_VS01ST06NEM = new TYData("DAT02_VS01ST06NEM", null);
            this.DAT02_VS01ST06DEN = new TYData("DAT02_VS01ST06DEN", null);
            this.DAT02_VS01ST06TAX = new TYData("DAT02_VS01ST06TAX", null);
            this.DAT02_VS01ST07AMT = new TYData("DAT02_VS01ST07AMT", null);
            this.DAT02_VS01ST07NEM = new TYData("DAT02_VS01ST07NEM", null);
            this.DAT02_VS01ST07DEN = new TYData("DAT02_VS01ST07DEN", null);
            this.DAT02_VS01ST07TAX = new TYData("DAT02_VS01ST07TAX", null);
            this.DAT02_VS01ST08AMT = new TYData("DAT02_VS01ST08AMT", null);
            this.DAT02_VS01ST08NEM = new TYData("DAT02_VS01ST08NEM", null);
            this.DAT02_VS01ST08DEN = new TYData("DAT02_VS01ST08DEN", null);
            this.DAT02_VS01ST08TAX = new TYData("DAT02_VS01ST08TAX", null);
            this.DAT02_VS01ST09AMT = new TYData("DAT02_VS01ST09AMT", null);
            this.DAT02_VS01ST09NEM = new TYData("DAT02_VS01ST09NEM", null);
            this.DAT02_VS01ST09DEN = new TYData("DAT02_VS01ST09DEN", null);
            this.DAT02_VS01ST09TAX = new TYData("DAT02_VS01ST09TAX", null);
            this.DAT02_VS02PU10AMT = new TYData("DAT02_VS02PU10AMT", null);
            this.DAT02_VS02PU10NEM = new TYData("DAT02_VS02PU10NEM", null);
            this.DAT02_VS02PU10DEN = new TYData("DAT02_VS02PU10DEN", null);
            this.DAT02_VS02PU10TAX = new TYData("DAT02_VS02PU10TAX", null);
            this.DAT02_VS02PU11AMT = new TYData("DAT02_VS02PU11AMT", null);
            this.DAT02_VS02PU11NEM = new TYData("DAT02_VS02PU11NEM", null);
            this.DAT02_VS02PU11DEN = new TYData("DAT02_VS02PU11DEN", null);
            this.DAT02_VS02PU11TAX = new TYData("DAT02_VS02PU11TAX", null);
            this.DAT02_VS02PU12AMT = new TYData("DAT02_VS02PU12AMT", null);
            this.DAT02_VS02PU12NEM = new TYData("DAT02_VS02PU12NEM", null);
            this.DAT02_VS02PU12DEN = new TYData("DAT02_VS02PU12DEN", null);
            this.DAT02_VS02PU12TAX = new TYData("DAT02_VS02PU12TAX", null);
            this.DAT02_VS02PU13AMT = new TYData("DAT02_VS02PU13AMT", null);
            this.DAT02_VS02PU13NEM = new TYData("DAT02_VS02PU13NEM", null);
            this.DAT02_VS02PU13DEN = new TYData("DAT02_VS02PU13DEN", null);
            this.DAT02_VS02PU13TAX = new TYData("DAT02_VS02PU13TAX", null);
            this.DAT02_VS02PU14AMT = new TYData("DAT02_VS02PU14AMT", null);
            this.DAT02_VS02PU14NEM = new TYData("DAT02_VS02PU14NEM", null);
            this.DAT02_VS02PU14DEN = new TYData("DAT02_VS02PU14DEN", null);
            this.DAT02_VS02PU14TAX = new TYData("DAT02_VS02PU14TAX", null);
            this.DAT02_VS02PU15AMT = new TYData("DAT02_VS02PU15AMT", null);
            this.DAT02_VS02PU15NEM = new TYData("DAT02_VS02PU15NEM", null);
            this.DAT02_VS02PU15DEN = new TYData("DAT02_VS02PU15DEN", null);
            this.DAT02_VS02PU15TAX = new TYData("DAT02_VS02PU15TAX", null);
            this.DAT02_VS02PU16AMT = new TYData("DAT02_VS02PU16AMT", null);
            this.DAT02_VS02PU16NEM = new TYData("DAT02_VS02PU16NEM", null);
            this.DAT02_VS02PU16DEN = new TYData("DAT02_VS02PU16DEN", null);
            this.DAT02_VS02PU16TAX = new TYData("DAT02_VS02PU16TAX", null);
            this.DAT02_VS02PU17AMT = new TYData("DAT02_VS02PU17AMT", null);
            this.DAT02_VS02PU17NEM = new TYData("DAT02_VS02PU17NEM", null);
            this.DAT02_VS02PU17DEN = new TYData("DAT02_VS02PU17DEN", null);
            this.DAT02_VS02PU17TAX = new TYData("DAT02_VS02PU17TAX", null);
            this.DAT02_VSPAYMENEM  = new TYData("DAT02_VSPAYMENEM",  null);
            this.DAT02_VSPAYMEDEN  = new TYData("DAT02_VSPAYMEDEN",  null);
            this.DAT02_VSPAYMETAX  = new TYData("DAT02_VSPAYMETAX",  null);
            this.DAT02_VS03RE18AMT = new TYData("DAT02_VS03RE18AMT", null);
            this.DAT02_VS03RE18NEM = new TYData("DAT02_VS03RE18NEM", null);
            this.DAT02_VS03RE18DEN = new TYData("DAT02_VS03RE18DEN", null);
            this.DAT02_VS03RE18TAX = new TYData("DAT02_VS03RE18TAX", null);
            this.DAT02_VS03RE19AMT = new TYData("DAT02_VS03RE19AMT", null);
            this.DAT02_VS03RE19NEM = new TYData("DAT02_VS03RE19NEM", null);
            this.DAT02_VS03RE19DEN = new TYData("DAT02_VS03RE19DEN", null);
            this.DAT02_VS03RE19TAX = new TYData("DAT02_VS03RE19TAX", null);
            this.DAT02_VS03RE20AMT = new TYData("DAT02_VS03RE20AMT", null);
            this.DAT02_VS03RE20NEM = new TYData("DAT02_VS03RE20NEM", null);
            this.DAT02_VS03RE20DEN = new TYData("DAT02_VS03RE20DEN", null);
            this.DAT02_VS03RE20TAX = new TYData("DAT02_VS03RE20TAX", null);
            this.DAT02_VS04SC21AMT = new TYData("DAT02_VS04SC21AMT", null);
            this.DAT02_VS04SC21NEM = new TYData("DAT02_VS04SC21NEM", null);
            this.DAT02_VS04SC21DEN = new TYData("DAT02_VS04SC21DEN", null);
            this.DAT02_VS04SC21TAX = new TYData("DAT02_VS04SC21TAX", null);
            this.DAT02_VS04SC22AMT = new TYData("DAT02_VS04SC22AMT", null);
            this.DAT02_VS04SC22NEM = new TYData("DAT02_VS04SC22NEM", null);
            this.DAT02_VS04SC22DEN = new TYData("DAT02_VS04SC22DEN", null);
            this.DAT02_VS04SC22TAX = new TYData("DAT02_VS04SC22TAX", null);

            this.DAT02_VS04YC23AMT = new TYData("DAT02_VS04YC23AMT", null);  // 추가(2014.04.22)
            this.DAT02_VS04YC23NEM = new TYData("DAT02_VS04YC23NEM", null);  // 추가(2014.04.22)
            this.DAT02_VS04YC23DEN = new TYData("DAT02_VS04YC23DEN", null);  // 추가(2014.04.22)
            this.DAT02_VS04YC23TAX = new TYData("DAT02_VS04YC23TAX", null);  // 추가(2014.04.22)

            this.DAT02_VS04SC23AMT = new TYData("DAT02_VS04SC23AMT", null);
            this.DAT02_VS04SC23NEM = new TYData("DAT02_VS04SC23NEM", null);
            this.DAT02_VS04SC23DEN = new TYData("DAT02_VS04SC23DEN", null);
            this.DAT02_VS04SC23TAX = new TYData("DAT02_VS04SC23TAX", null);
            this.DAT02_VS04SC24AMT = new TYData("DAT02_VS04SC24AMT", null);
            this.DAT02_VS04SC24NEM = new TYData("DAT02_VS04SC24NEM", null);
            this.DAT02_VS04SC24DEN = new TYData("DAT02_VS04SC24DEN", null);
            this.DAT02_VS04SC24TAX = new TYData("DAT02_VS04SC24TAX", null);
            this.DAT02_VS04SC25TAX = new TYData("DAT02_VS04SC25TAX", null);
            this.DAT02_VSTOTTAXAMT = new TYData("DAT02_VSTOTTAXAMT", null);
            this.DAT02_VSREBANKCD  = new TYData("DAT02_VSREBANKCD",  null);
            this.DAT02_VSREACCCODE = new TYData("DAT02_VSREACCCODE", null);
            this.DAT02_VSSHUTDATE  = new TYData("DAT02_VSSHUTDATE",  null);
            this.DAT02_VSSHUTSAYU  = new TYData("DAT02_VSSHUTSAYU",  null);
            this.DAT02_VS05EV26BUS = new TYData("DAT02_VS05EV26BUS", null);
            this.DAT02_VS05EV26NAM = new TYData("DAT02_VS05EV26NAM", null);
            this.DAT02_VS05EV26COD = new TYData("DAT02_VS05EV26COD", null);
            this.DAT02_VS05EV26AMT = new TYData("DAT02_VS05EV26AMT", null);
            this.DAT02_VS05EV27BUS = new TYData("DAT02_VS05EV27BUS", null);
            this.DAT02_VS05EV27NAM = new TYData("DAT02_VS05EV27NAM", null);
            this.DAT02_VS05EV27COD = new TYData("DAT02_VS05EV27COD", null);
            this.DAT02_VS05EV27AMT = new TYData("DAT02_VS05EV27AMT", null);
            this.DAT02_VS05EV28BUS = new TYData("DAT02_VS05EV28BUS", null);
            this.DAT02_VS05EV28NAM = new TYData("DAT02_VS05EV28NAM", null);
            this.DAT02_VS05EV28COD = new TYData("DAT02_VS05EV28COD", null);
            this.DAT02_VS05EV28AMT = new TYData("DAT02_VS05EV28AMT", null);
            this.DAT02_VS05EV29BUS = new TYData("DAT02_VS05EV29BUS", null);
            this.DAT02_VS05EV29NAM = new TYData("DAT02_VS05EV29NAM", null);
            this.DAT02_VS05EV29COD = new TYData("DAT02_VS05EV29COD", null);
            this.DAT02_VS05EV29AMT = new TYData("DAT02_VS05EV29AMT", null);
            this.DAT02_VS05EV30AMT = new TYData("DAT02_VS05EV30AMT", null);
            this.DAT02_VS06MI31AMT = new TYData("DAT02_VS06MI31AMT", null);
            this.DAT02_VS06MI31NEM = new TYData("DAT02_VS06MI31NEM", null);
            this.DAT02_VS06MI31DEN = new TYData("DAT02_VS06MI31DEN", null);
            this.DAT02_VS06MI31TAX = new TYData("DAT02_VS06MI31TAX", null);
            this.DAT02_VS06MI32AMT = new TYData("DAT02_VS06MI32AMT", null);
            this.DAT02_VS06MI32NEM = new TYData("DAT02_VS06MI32NEM", null);
            this.DAT02_VS06MI32DEN = new TYData("DAT02_VS06MI32DEN", null);
            this.DAT02_VS06MI32TAX = new TYData("DAT02_VS06MI32TAX", null);
            this.DAT02_VS06MI33AMT = new TYData("DAT02_VS06MI33AMT", null);
            this.DAT02_VS06MI33NEM = new TYData("DAT02_VS06MI33NEM", null);
            this.DAT02_VS06MI33DEN = new TYData("DAT02_VS06MI33DEN", null);
            this.DAT02_VS06MI33TAX = new TYData("DAT02_VS06MI33TAX", null);
            this.DAT02_VS06MI34AMT = new TYData("DAT02_VS06MI34AMT", null);
            this.DAT02_VS06MI34NEM = new TYData("DAT02_VS06MI34NEM", null);
            this.DAT02_VS06MI34DEN = new TYData("DAT02_VS06MI34DEN", null);
            this.DAT02_VS06MI34TAX = new TYData("DAT02_VS06MI34TAX", null);
            this.DAT02_VS06MI35AMT = new TYData("DAT02_VS06MI35AMT", null);
            this.DAT02_VS06MI35NEM = new TYData("DAT02_VS06MI35NEM", null);
            this.DAT02_VS06MI35DEN = new TYData("DAT02_VS06MI35DEN", null);
            this.DAT02_VS06MI35TAX = new TYData("DAT02_VS06MI35TAX", null);
            this.DAT02_VS06MI36AMT = new TYData("DAT02_VS06MI36AMT", null);
            this.DAT02_VS06MI36NEM = new TYData("DAT02_VS06MI36NEM", null);
            this.DAT02_VS06MI36DEN = new TYData("DAT02_VS06MI36DEN", null);
            this.DAT02_VS06MI36TAX = new TYData("DAT02_VS06MI36TAX", null);
            this.DAT02_VS06MI37AMT = new TYData("DAT02_VS06MI37AMT", null);
            this.DAT02_VS06MI37NEM = new TYData("DAT02_VS06MI37NEM", null);
            this.DAT02_VS06MI37DEN = new TYData("DAT02_VS06MI37DEN", null);
            this.DAT02_VS06MI37TAX = new TYData("DAT02_VS06MI37TAX", null);
            this.DAT02_VS06MI38AMT = new TYData("DAT02_VS06MI38AMT", null);
            this.DAT02_VS06MI38NEM = new TYData("DAT02_VS06MI38NEM", null);
            this.DAT02_VS06MI38DEN = new TYData("DAT02_VS06MI38DEN", null);
            this.DAT02_VS06MI38TAX = new TYData("DAT02_VS06MI38TAX", null);
            this.DAT02_VS14ET39AMT = new TYData("DAT02_VS14ET39AMT", null);
            this.DAT02_VS14ET39NEM = new TYData("DAT02_VS14ET39NEM", null);
            this.DAT02_VS14ET39DEN = new TYData("DAT02_VS14ET39DEN", null);
            this.DAT02_VS14ET39TAX = new TYData("DAT02_VS14ET39TAX", null);
            this.DAT02_VS14ET40AMT = new TYData("DAT02_VS14ET40AMT", null);
            this.DAT02_VS14ET40NEM = new TYData("DAT02_VS14ET40NEM", null);
            this.DAT02_VS14ET40DEN = new TYData("DAT02_VS14ET40DEN", null);
            this.DAT02_VS14ET40TAX = new TYData("DAT02_VS14ET40TAX", null);
            this.DAT02_VS14ET41AMT = new TYData("DAT02_VS14ET41AMT", null);
            this.DAT02_VS14ET41NEM = new TYData("DAT02_VS14ET41NEM", null);
            this.DAT02_VS14ET41DEN = new TYData("DAT02_VS14ET41DEN", null);
            this.DAT02_VS14ET41TAX = new TYData("DAT02_VS14ET41TAX", null);
            this.DAT02_VS14ET42AMT = new TYData("DAT02_VS14ET42AMT", null);
            this.DAT02_VS14ET42NEM = new TYData("DAT02_VS14ET42NEM", null);
            this.DAT02_VS14ET42DEN = new TYData("DAT02_VS14ET42DEN", null);
            this.DAT02_VS14ET42TAX = new TYData("DAT02_VS14ET42TAX", null);
            //this.DAT02_VS14ET43AMT = new TYData("DAT02_VS14ET43AMT", null);
            //this.DAT02_VS14ET43NEM = new TYData("DAT02_VS14ET43NEM", null);
            //this.DAT02_VS14ET43DEN = new TYData("DAT02_VS14ET43DEN", null);
            //this.DAT02_VS14ET43TAX = new TYData("DAT02_VS14ET43TAX", null);
            this.DAT02_VS14ET44AMT = new TYData("DAT02_VS14ET44AMT", null);
            this.DAT02_VS14ET44NEM = new TYData("DAT02_VS14ET44NEM", null);
            this.DAT02_VS14ET44DEN = new TYData("DAT02_VS14ET44DEN", null);
            this.DAT02_VS14ET44TAX = new TYData("DAT02_VS14ET44TAX", null);
            this.DAT02_VS14ET45AMT = new TYData("DAT02_VS14ET45AMT", null);
            this.DAT02_VS14ET45NEM = new TYData("DAT02_VS14ET45NEM", null);
            this.DAT02_VS14ET45DEN = new TYData("DAT02_VS14ET45DEN", null);
            this.DAT02_VS14ET45TAX = new TYData("DAT02_VS14ET45TAX", null);
            this.DAT02_VS14ET46AMT = new TYData("DAT02_VS14ET46AMT", null);
            this.DAT02_VS14ET46NEM = new TYData("DAT02_VS14ET46NEM", null);
            this.DAT02_VS14ET46DEN = new TYData("DAT02_VS14ET46DEN", null);
            this.DAT02_VS14ET46TAX = new TYData("DAT02_VS14ET46TAX", null);

            // 2014.02.22 추가
            this.DAT02_VS14NT47AMT = new TYData("DAT02_VS14NT47AMT", null);//외국인관광객
            this.DAT02_VS14NT47NEM = new TYData("DAT02_VS14NT47NEM", null);//세율자
            this.DAT02_VS14NT47DEN = new TYData("DAT02_VS14NT47DEN", null);//세율모
            this.DAT02_VS14NT47TAX = new TYData("DAT02_VS14NT47TAX", null);//세액

            this.DAT02_VS14ET47AMT = new TYData("DAT02_VS14ET47AMT", null);
            this.DAT02_VS14ET47NEM = new TYData("DAT02_VS14ET47NEM", null);
            this.DAT02_VS14ET47DEN = new TYData("DAT02_VS14ET47DEN", null);
            this.DAT02_VS14ET47TAX = new TYData("DAT02_VS14ET47TAX", null);
            this.DAT02_VS16DE48AMT = new TYData("DAT02_VS16DE48AMT", null);
            this.DAT02_VS16DE48NEM = new TYData("DAT02_VS16DE48NEM", null);
            this.DAT02_VS16DE48DEN = new TYData("DAT02_VS16DE48DEN", null);
            this.DAT02_VS16DE48TAX = new TYData("DAT02_VS16DE48TAX", null);
            this.DAT02_VS16DE49AMT = new TYData("DAT02_VS16DE49AMT", null);
            this.DAT02_VS16DE49NEM = new TYData("DAT02_VS16DE49NEM", null);
            this.DAT02_VS16DE49DEN = new TYData("DAT02_VS16DE49DEN", null);
            this.DAT02_VS16DE49TAX = new TYData("DAT02_VS16DE49TAX", null);
            this.DAT02_VS16DE50AMT = new TYData("DAT02_VS16DE50AMT", null);
            this.DAT02_VS16DE50NEM = new TYData("DAT02_VS16DE50NEM", null);
            this.DAT02_VS16DE50DEN = new TYData("DAT02_VS16DE50DEN", null);
            this.DAT02_VS16DE50TAX = new TYData("DAT02_VS16DE50TAX", null);
            this.DAT02_VS16DE51AMT = new TYData("DAT02_VS16DE51AMT", null);
            this.DAT02_VS16DE51NEM = new TYData("DAT02_VS16DE51NEM", null);
            this.DAT02_VS16DE51DEN = new TYData("DAT02_VS16DE51DEN", null);
            this.DAT02_VS16DE51TAX = new TYData("DAT02_VS16DE51TAX", null);
            this.DAT02_VS18BE52AMT = new TYData("DAT02_VS18BE52AMT", null);
            this.DAT02_VS18BE52NEM = new TYData("DAT02_VS18BE52NEM", null);
            this.DAT02_VS18BE52DEN = new TYData("DAT02_VS18BE52DEN", null);
            this.DAT02_VS18BE52TAX = new TYData("DAT02_VS18BE52TAX", null);
            this.DAT02_VS18BE53AMT = new TYData("DAT02_VS18BE53AMT", null);
            this.DAT02_VS18BE53NEM = new TYData("DAT02_VS18BE53NEM", null);
            this.DAT02_VS18BE53DEN = new TYData("DAT02_VS18BE53DEN", null);
            this.DAT02_VS18BE53TAX = new TYData("DAT02_VS18BE53TAX", null);
            this.DAT02_VS18BE54AMT = new TYData("DAT02_VS18BE54AMT", null);
            this.DAT02_VS18BE54NEM = new TYData("DAT02_VS18BE54NEM", null);
            this.DAT02_VS18BE54DEN = new TYData("DAT02_VS18BE54DEN", null);
            this.DAT02_VS18BE54TAX = new TYData("DAT02_VS18BE54TAX", null);
            //this.DAT02_VS18BE55AMT = new TYData("DAT02_VS18BE55AMT", null);
            //this.DAT02_VS18BE55NEM = new TYData("DAT02_VS18BE55NEM", null);
            //this.DAT02_VS18BE55DEN = new TYData("DAT02_VS18BE55DEN", null);
            //this.DAT02_VS18BE55TAX = new TYData("DAT02_VS18BE55TAX", null);
            this.DAT02_VS18BE56AMT = new TYData("DAT02_VS18BE56AMT", null);
            this.DAT02_VS18BE56NEM = new TYData("DAT02_VS18BE56NEM", null);
            this.DAT02_VS18BE56DEN = new TYData("DAT02_VS18BE56DEN", null);
            this.DAT02_VS18BE56TAX = new TYData("DAT02_VS18BE56TAX", null);
            this.DAT02_VS18BE57AMT = new TYData("DAT02_VS18BE57AMT", null);
            this.DAT02_VS18BE57NEM = new TYData("DAT02_VS18BE57NEM", null);
            this.DAT02_VS18BE57DEN = new TYData("DAT02_VS18BE57DEN", null);
            this.DAT02_VS18BE57TAX = new TYData("DAT02_VS18BE57TAX", null);
            this.DAT02_VS18BE58AMT = new TYData("DAT02_VS18BE58AMT", null);
            this.DAT02_VS18BE58NEM = new TYData("DAT02_VS18BE58NEM", null);
            this.DAT02_VS18BE58DEN = new TYData("DAT02_VS18BE58DEN", null);
            this.DAT02_VS18BE58TAX = new TYData("DAT02_VS18BE58TAX", null);
            this.DAT02_VS24AD59AMT = new TYData("DAT02_VS24AD59AMT", null);
            this.DAT02_VS24AD59NEM = new TYData("DAT02_VS24AD59NEM", null);
            this.DAT02_VS24AD59DEN = new TYData("DAT02_VS24AD59DEN", null);
            this.DAT02_VS24AD59TAX = new TYData("DAT02_VS24AD59TAX", null);
            this.DAT02_VS24AD60AMT = new TYData("DAT02_VS24AD60AMT", null);
            this.DAT02_VS24AD60NEM = new TYData("DAT02_VS24AD60NEM", null);
            this.DAT02_VS24AD60DEN = new TYData("DAT02_VS24AD60DEN", null);
            this.DAT02_VS24AD60TAX = new TYData("DAT02_VS24AD60TAX", null);
            this.DAT02_VS24AD61AMT = new TYData("DAT02_VS24AD61AMT", null);
            this.DAT02_VS24AD61NEM = new TYData("DAT02_VS24AD61NEM", null);
            this.DAT02_VS24AD61DEN = new TYData("DAT02_VS24AD61DEN", null);
            this.DAT02_VS24AD61TAX = new TYData("DAT02_VS24AD61TAX", null);
            this.DAT02_VS24AD62AMT = new TYData("DAT02_VS24AD62AMT", null);
            this.DAT02_VS24AD62NEM = new TYData("DAT02_VS24AD62NEM", null);
            this.DAT02_VS24AD62DEN = new TYData("DAT02_VS24AD62DEN", null);
            this.DAT02_VS24AD62TAX = new TYData("DAT02_VS24AD62TAX", null);
            this.DAT02_VS24AD63AMT = new TYData("DAT02_VS24AD63AMT", null);
            this.DAT02_VS24AD63NEM = new TYData("DAT02_VS24AD63NEM", null);
            this.DAT02_VS24AD63DEN = new TYData("DAT02_VS24AD63DEN", null);
            this.DAT02_VS24AD63TAX = new TYData("DAT02_VS24AD63TAX", null);
            this.DAT02_VS24AD64AMT = new TYData("DAT02_VS24AD64AMT", null);
            this.DAT02_VS24AD64NEM = new TYData("DAT02_VS24AD64NEM", null);
            this.DAT02_VS24AD64DEN = new TYData("DAT02_VS24AD64DEN", null);
            this.DAT02_VS24AD64TAX = new TYData("DAT02_VS24AD64TAX", null);
            this.DAT02_VS24AD65AMT = new TYData("DAT02_VS24AD65AMT", null);
            this.DAT02_VS24AD65NEM = new TYData("DAT02_VS24AD65NEM", null);
            this.DAT02_VS24AD65DEN = new TYData("DAT02_VS24AD65DEN", null);
            this.DAT02_VS24AD65TAX = new TYData("DAT02_VS24AD65TAX", null);
            this.DAT02_VS24AD66AMT = new TYData("DAT02_VS24AD66AMT", null);
            this.DAT02_VS24AD66NEM = new TYData("DAT02_VS24AD66NEM", null);
            this.DAT02_VS24AD66DEN = new TYData("DAT02_VS24AD66DEN", null);
            this.DAT02_VS24AD66TAX = new TYData("DAT02_VS24AD66TAX", null);
            this.DAT02_VS24AD67AMT = new TYData("DAT02_VS24AD67AMT", null);
            this.DAT02_VS24AD67NEM = new TYData("DAT02_VS24AD67NEM", null);
            this.DAT02_VS24AD67DEN = new TYData("DAT02_VS24AD67DEN", null);
            this.DAT02_VS24AD67TAX = new TYData("DAT02_VS24AD67TAX", null);
            this.DAT02_VS24AD68AMT = new TYData("DAT02_VS24AD68AMT", null);
            this.DAT02_VS24AD68NEM = new TYData("DAT02_VS24AD68NEM", null);
            this.DAT02_VS24AD68DEN = new TYData("DAT02_VS24AD68DEN", null);
            this.DAT02_VS24AD68TAX = new TYData("DAT02_VS24AD68TAX", null);
            this.DAT02_VS24AD69AMT = new TYData("DAT02_VS24AD69AMT", null);
            this.DAT02_VS24AD69NEM = new TYData("DAT02_VS24AD69NEM", null);
            this.DAT02_VS24AD69DEN = new TYData("DAT02_VS24AD69DEN", null);
            this.DAT02_VS24AD69TAX = new TYData("DAT02_VS24AD69TAX", null);
            this.DAT02_VS24AD70AMT = new TYData("DAT02_VS24AD70AMT", null);
            this.DAT02_VS24AD70NEM = new TYData("DAT02_VS24AD70NEM", null);
            this.DAT02_VS24AD70DEN = new TYData("DAT02_VS24AD70DEN", null);
            this.DAT02_VS24AD70TAX = new TYData("DAT02_VS24AD70TAX", null);
            this.DAT02_VS24AD71AMT = new TYData("DAT02_VS24AD71AMT", null);
            this.DAT02_VS24AD71NEM = new TYData("DAT02_VS24AD71NEM", null);
            this.DAT02_VS24AD71DEN = new TYData("DAT02_VS24AD71DEN", null);
            this.DAT02_VS24AD71TAX = new TYData("DAT02_VS24AD71TAX", null);
            this.DAT02_VS24AD72AMT = new TYData("DAT02_VS24AD72AMT", null);
            this.DAT02_VS24AD72NEM = new TYData("DAT02_VS24AD72NEM", null);
            this.DAT02_VS24AD72DEN = new TYData("DAT02_VS24AD72DEN", null);
            this.DAT02_VS24AD72TAX = new TYData("DAT02_VS24AD72TAX", null);
            this.DAT02_VS24AD73AMT = new TYData("DAT02_VS24AD73AMT", null);
            this.DAT02_VS24AD73NEM = new TYData("DAT02_VS24AD73NEM", null);
            this.DAT02_VS24AD73DEN = new TYData("DAT02_VS24AD73DEN", null);
            this.DAT02_VS24AD73TAX = new TYData("DAT02_VS24AD73TAX", null);
            this.DAT02_VS24AD74AMT = new TYData("DAT02_VS24AD74AMT", null);
            this.DAT02_VS24AD74NEM = new TYData("DAT02_VS24AD74NEM", null);
            this.DAT02_VS24AD74DEN = new TYData("DAT02_VS24AD74DEN", null);
            this.DAT02_VS24AD74TAX = new TYData("DAT02_VS24AD74TAX", null);

            this.DAT02_VS24AR75AMT = new TYData("DAT02_VS24AR75AMT", null); // 추가(2014.04.22) 거래계좌미사용
            this.DAT02_VS24AR75NEM = new TYData("DAT02_VS24AR75NEM", null); // 추가(2014.04.22) 세율자
            this.DAT02_VS24AR75DEN = new TYData("DAT02_VS24AR75DEN", null); // 추가(2014.04.22) 세율모
            this.DAT02_VS24AR75TAX = new TYData("DAT02_VS24AR75TAX", null); // 추가(2014.04.22) 세액
            this.DAT02_VS24AR76AMT = new TYData("DAT02_VS24AR76AMT", null); // 추가(2014.04.22) 거래계좌지연입금
            this.DAT02_VS24AR76NEM = new TYData("DAT02_VS24AR76NEM", null); // 추가(2014.04.22) 세율자
            this.DAT02_VS24AR76DEN = new TYData("DAT02_VS24AR76DEN", null); // 추가(2014.04.22) 세율모
            this.DAT02_VS24AR76TAX = new TYData("DAT02_VS24AR76TAX", null); // 추가(2014.04.22) 세액

            this.DAT02_VS24AD75AMT = new TYData("DAT02_VS24AD75AMT", null);
            this.DAT02_VS24AD75NEM = new TYData("DAT02_VS24AD75NEM", null);
            this.DAT02_VS24AD75DEN = new TYData("DAT02_VS24AD75DEN", null);
            this.DAT02_VS24AD75TAX = new TYData("DAT02_VS24AD75TAX", null);
            this.DAT02_VS25EX76BUS = new TYData("DAT02_VS25EX76BUS", null);
            this.DAT02_VS25EX76NAM = new TYData("DAT02_VS25EX76NAM", null);
            this.DAT02_VS25EX76COD = new TYData("DAT02_VS25EX76COD", null);
            this.DAT02_VS25EX76AMT = new TYData("DAT02_VS25EX76AMT", null);
            this.DAT02_VS25EX77BUS = new TYData("DAT02_VS25EX77BUS", null);
            this.DAT02_VS25EX77NAM = new TYData("DAT02_VS25EX77NAM", null);
            this.DAT02_VS25EX77COD = new TYData("DAT02_VS25EX77COD", null);
            this.DAT02_VS25EX77AMT = new TYData("DAT02_VS25EX77AMT", null);
            this.DAT02_VS25EX78BUS = new TYData("DAT02_VS25EX78BUS", null);
            this.DAT02_VS25EX78NAM = new TYData("DAT02_VS25EX78NAM", null);
            this.DAT02_VS25EX78COD = new TYData("DAT02_VS25EX78COD", null);
            this.DAT02_VS25EX78AMT = new TYData("DAT02_VS25EX78AMT", null);
            this.DAT02_VS25EX79AMT = new TYData("DAT02_VS25EX79AMT", null);
            this.DAT02_VS26IS80AMT = new TYData("DAT02_VS26IS80AMT", null);
            this.DAT02_VS26IS81AMT = new TYData("DAT02_VS26IS81AMT", null);
            this.DAT02_VSREFUNDGB  = new TYData("DAT02_VSREFUNDGB",  null);
        }

        public TYACTX019S(string sVSYEAR, string sVSBRANCH, string sVSCONFGB, string sPOPUP)
        {
            InitializeComponent();

            this.DAT02_VSYEAR      = new TYData("DAT02_VSYEAR",      null);
            this.DAT02_VSBRANCH    = new TYData("DAT02_VSBRANCH",    null);
            this.DAT02_VSVENDCD    = new TYData("DAT02_VSVENDCD",    null);
            this.DAT02_VSRPTGUBN   = new TYData("DAT02_VSRPTGUBN",   null);
            this.DAT02_VSCONFGB    = new TYData("DAT02_VSCONFGB",    null);
            this.DAT02_VS01ST01AMT = new TYData("DAT02_VS01ST01AMT", null);
            this.DAT02_VS01ST01NEM = new TYData("DAT02_VS01ST01NEM", null);
            this.DAT02_VS01ST01DEN = new TYData("DAT02_VS01ST01DEN", null);
            this.DAT02_VS01ST01TAX = new TYData("DAT02_VS01ST01TAX", null);
            this.DAT02_VS01ST02AMT = new TYData("DAT02_VS01ST02AMT", null);
            this.DAT02_VS01ST02NEM = new TYData("DAT02_VS01ST02NEM", null);
            this.DAT02_VS01ST02DEN = new TYData("DAT02_VS01ST02DEN", null);
            this.DAT02_VS01ST02TAX = new TYData("DAT02_VS01ST02TAX", null);
            this.DAT02_VS01ST03AMT = new TYData("DAT02_VS01ST03AMT", null);
            this.DAT02_VS01ST03NEM = new TYData("DAT02_VS01ST03NEM", null);
            this.DAT02_VS01ST03DEN = new TYData("DAT02_VS01ST03DEN", null);
            this.DAT02_VS01ST03TAX = new TYData("DAT02_VS01ST03TAX", null);
            this.DAT02_VS01ST04AMT = new TYData("DAT02_VS01ST04AMT", null);
            this.DAT02_VS01ST04NEM = new TYData("DAT02_VS01ST04NEM", null);
            this.DAT02_VS01ST04DEN = new TYData("DAT02_VS01ST04DEN", null);
            this.DAT02_VS01ST04TAX = new TYData("DAT02_VS01ST04TAX", null);
            this.DAT02_VS01ST05AMT = new TYData("DAT02_VS01ST05AMT", null);
            this.DAT02_VS01ST05NEM = new TYData("DAT02_VS01ST05NEM", null);
            this.DAT02_VS01ST05DEN = new TYData("DAT02_VS01ST05DEN", null);
            this.DAT02_VS01ST05TAX = new TYData("DAT02_VS01ST05TAX", null);
            this.DAT02_VS01ST06AMT = new TYData("DAT02_VS01ST06AMT", null);
            this.DAT02_VS01ST06NEM = new TYData("DAT02_VS01ST06NEM", null);
            this.DAT02_VS01ST06DEN = new TYData("DAT02_VS01ST06DEN", null);
            this.DAT02_VS01ST06TAX = new TYData("DAT02_VS01ST06TAX", null);
            this.DAT02_VS01ST07AMT = new TYData("DAT02_VS01ST07AMT", null);
            this.DAT02_VS01ST07NEM = new TYData("DAT02_VS01ST07NEM", null);
            this.DAT02_VS01ST07DEN = new TYData("DAT02_VS01ST07DEN", null);
            this.DAT02_VS01ST07TAX = new TYData("DAT02_VS01ST07TAX", null);
            this.DAT02_VS01ST08AMT = new TYData("DAT02_VS01ST08AMT", null);
            this.DAT02_VS01ST08NEM = new TYData("DAT02_VS01ST08NEM", null);
            this.DAT02_VS01ST08DEN = new TYData("DAT02_VS01ST08DEN", null);
            this.DAT02_VS01ST08TAX = new TYData("DAT02_VS01ST08TAX", null);
            this.DAT02_VS01ST09AMT = new TYData("DAT02_VS01ST09AMT", null);
            this.DAT02_VS01ST09NEM = new TYData("DAT02_VS01ST09NEM", null);
            this.DAT02_VS01ST09DEN = new TYData("DAT02_VS01ST09DEN", null);
            this.DAT02_VS01ST09TAX = new TYData("DAT02_VS01ST09TAX", null);
            this.DAT02_VS02PU10AMT = new TYData("DAT02_VS02PU10AMT", null);
            this.DAT02_VS02PU10NEM = new TYData("DAT02_VS02PU10NEM", null);
            this.DAT02_VS02PU10DEN = new TYData("DAT02_VS02PU10DEN", null);
            this.DAT02_VS02PU10TAX = new TYData("DAT02_VS02PU10TAX", null);
            this.DAT02_VS02PU11AMT = new TYData("DAT02_VS02PU11AMT", null);
            this.DAT02_VS02PU11NEM = new TYData("DAT02_VS02PU11NEM", null);
            this.DAT02_VS02PU11DEN = new TYData("DAT02_VS02PU11DEN", null);
            this.DAT02_VS02PU11TAX = new TYData("DAT02_VS02PU11TAX", null);
            this.DAT02_VS02PU12AMT = new TYData("DAT02_VS02PU12AMT", null);
            this.DAT02_VS02PU12NEM = new TYData("DAT02_VS02PU12NEM", null);
            this.DAT02_VS02PU12DEN = new TYData("DAT02_VS02PU12DEN", null);
            this.DAT02_VS02PU12TAX = new TYData("DAT02_VS02PU12TAX", null);
            this.DAT02_VS02PU13AMT = new TYData("DAT02_VS02PU13AMT", null);
            this.DAT02_VS02PU13NEM = new TYData("DAT02_VS02PU13NEM", null);
            this.DAT02_VS02PU13DEN = new TYData("DAT02_VS02PU13DEN", null);
            this.DAT02_VS02PU13TAX = new TYData("DAT02_VS02PU13TAX", null);
            this.DAT02_VS02PU14AMT = new TYData("DAT02_VS02PU14AMT", null);
            this.DAT02_VS02PU14NEM = new TYData("DAT02_VS02PU14NEM", null);
            this.DAT02_VS02PU14DEN = new TYData("DAT02_VS02PU14DEN", null);
            this.DAT02_VS02PU14TAX = new TYData("DAT02_VS02PU14TAX", null);
            this.DAT02_VS02PU15AMT = new TYData("DAT02_VS02PU15AMT", null);
            this.DAT02_VS02PU15NEM = new TYData("DAT02_VS02PU15NEM", null);
            this.DAT02_VS02PU15DEN = new TYData("DAT02_VS02PU15DEN", null);
            this.DAT02_VS02PU15TAX = new TYData("DAT02_VS02PU15TAX", null);
            this.DAT02_VS02PU16AMT = new TYData("DAT02_VS02PU16AMT", null);
            this.DAT02_VS02PU16NEM = new TYData("DAT02_VS02PU16NEM", null);
            this.DAT02_VS02PU16DEN = new TYData("DAT02_VS02PU16DEN", null);
            this.DAT02_VS02PU16TAX = new TYData("DAT02_VS02PU16TAX", null);
            this.DAT02_VS02PU17AMT = new TYData("DAT02_VS02PU17AMT", null);
            this.DAT02_VS02PU17NEM = new TYData("DAT02_VS02PU17NEM", null);
            this.DAT02_VS02PU17DEN = new TYData("DAT02_VS02PU17DEN", null);
            this.DAT02_VS02PU17TAX = new TYData("DAT02_VS02PU17TAX", null);
            this.DAT02_VSPAYMENEM  = new TYData("DAT02_VSPAYMENEM",  null);
            this.DAT02_VSPAYMEDEN  = new TYData("DAT02_VSPAYMEDEN",  null);
            this.DAT02_VSPAYMETAX  = new TYData("DAT02_VSPAYMETAX",  null);
            this.DAT02_VS03RE18AMT = new TYData("DAT02_VS03RE18AMT", null);
            this.DAT02_VS03RE18NEM = new TYData("DAT02_VS03RE18NEM", null);
            this.DAT02_VS03RE18DEN = new TYData("DAT02_VS03RE18DEN", null);
            this.DAT02_VS03RE18TAX = new TYData("DAT02_VS03RE18TAX", null);
            this.DAT02_VS03RE19AMT = new TYData("DAT02_VS03RE19AMT", null);
            this.DAT02_VS03RE19NEM = new TYData("DAT02_VS03RE19NEM", null);
            this.DAT02_VS03RE19DEN = new TYData("DAT02_VS03RE19DEN", null);
            this.DAT02_VS03RE19TAX = new TYData("DAT02_VS03RE19TAX", null);
            this.DAT02_VS03RE20AMT = new TYData("DAT02_VS03RE20AMT", null);
            this.DAT02_VS03RE20NEM = new TYData("DAT02_VS03RE20NEM", null);
            this.DAT02_VS03RE20DEN = new TYData("DAT02_VS03RE20DEN", null);
            this.DAT02_VS03RE20TAX = new TYData("DAT02_VS03RE20TAX", null);
            this.DAT02_VS04SC21AMT = new TYData("DAT02_VS04SC21AMT", null);
            this.DAT02_VS04SC21NEM = new TYData("DAT02_VS04SC21NEM", null);
            this.DAT02_VS04SC21DEN = new TYData("DAT02_VS04SC21DEN", null);
            this.DAT02_VS04SC21TAX = new TYData("DAT02_VS04SC21TAX", null);
            this.DAT02_VS04SC22AMT = new TYData("DAT02_VS04SC22AMT", null);
            this.DAT02_VS04SC22NEM = new TYData("DAT02_VS04SC22NEM", null);
            this.DAT02_VS04SC22DEN = new TYData("DAT02_VS04SC22DEN", null);
            this.DAT02_VS04SC22TAX = new TYData("DAT02_VS04SC22TAX", null);

            this.DAT02_VS04YC23AMT = new TYData("DAT02_VS04YC23AMT", null);  // 추가(2014.04.22)
            this.DAT02_VS04YC23NEM = new TYData("DAT02_VS04YC23NEM", null);  // 추가(2014.04.22)
            this.DAT02_VS04YC23DEN = new TYData("DAT02_VS04YC23DEN", null);  // 추가(2014.04.22)
            this.DAT02_VS04YC23TAX = new TYData("DAT02_VS04YC23TAX", null);  // 추가(2014.04.22)

            this.DAT02_VS04SC23AMT = new TYData("DAT02_VS04SC23AMT", null);
            this.DAT02_VS04SC23NEM = new TYData("DAT02_VS04SC23NEM", null);
            this.DAT02_VS04SC23DEN = new TYData("DAT02_VS04SC23DEN", null);
            this.DAT02_VS04SC23TAX = new TYData("DAT02_VS04SC23TAX", null);
            this.DAT02_VS04SC24AMT = new TYData("DAT02_VS04SC24AMT", null);
            this.DAT02_VS04SC24NEM = new TYData("DAT02_VS04SC24NEM", null);
            this.DAT02_VS04SC24DEN = new TYData("DAT02_VS04SC24DEN", null);
            this.DAT02_VS04SC24TAX = new TYData("DAT02_VS04SC24TAX", null);
            this.DAT02_VS04SC25TAX = new TYData("DAT02_VS04SC25TAX", null);
            this.DAT02_VSTOTTAXAMT = new TYData("DAT02_VSTOTTAXAMT", null);
            this.DAT02_VSREBANKCD  = new TYData("DAT02_VSREBANKCD",  null);
            this.DAT02_VSREACCCODE = new TYData("DAT02_VSREACCCODE", null);
            this.DAT02_VSSHUTDATE  = new TYData("DAT02_VSSHUTDATE",  null);
            this.DAT02_VSSHUTSAYU  = new TYData("DAT02_VSSHUTSAYU",  null);
            this.DAT02_VS05EV26BUS = new TYData("DAT02_VS05EV26BUS", null);
            this.DAT02_VS05EV26NAM = new TYData("DAT02_VS05EV26NAM", null);
            this.DAT02_VS05EV26COD = new TYData("DAT02_VS05EV26COD", null);
            this.DAT02_VS05EV26AMT = new TYData("DAT02_VS05EV26AMT", null);
            this.DAT02_VS05EV27BUS = new TYData("DAT02_VS05EV27BUS", null);
            this.DAT02_VS05EV27NAM = new TYData("DAT02_VS05EV27NAM", null);
            this.DAT02_VS05EV27COD = new TYData("DAT02_VS05EV27COD", null);
            this.DAT02_VS05EV27AMT = new TYData("DAT02_VS05EV27AMT", null);
            this.DAT02_VS05EV28BUS = new TYData("DAT02_VS05EV28BUS", null);
            this.DAT02_VS05EV28NAM = new TYData("DAT02_VS05EV28NAM", null);
            this.DAT02_VS05EV28COD = new TYData("DAT02_VS05EV28COD", null);
            this.DAT02_VS05EV28AMT = new TYData("DAT02_VS05EV28AMT", null);
            this.DAT02_VS05EV29BUS = new TYData("DAT02_VS05EV29BUS", null);
            this.DAT02_VS05EV29NAM = new TYData("DAT02_VS05EV29NAM", null);
            this.DAT02_VS05EV29COD = new TYData("DAT02_VS05EV29COD", null);
            this.DAT02_VS05EV29AMT = new TYData("DAT02_VS05EV29AMT", null);
            this.DAT02_VS05EV30AMT = new TYData("DAT02_VS05EV30AMT", null);
            this.DAT02_VS06MI31AMT = new TYData("DAT02_VS06MI31AMT", null);
            this.DAT02_VS06MI31NEM = new TYData("DAT02_VS06MI31NEM", null);
            this.DAT02_VS06MI31DEN = new TYData("DAT02_VS06MI31DEN", null);
            this.DAT02_VS06MI31TAX = new TYData("DAT02_VS06MI31TAX", null);
            this.DAT02_VS06MI32AMT = new TYData("DAT02_VS06MI32AMT", null);
            this.DAT02_VS06MI32NEM = new TYData("DAT02_VS06MI32NEM", null);
            this.DAT02_VS06MI32DEN = new TYData("DAT02_VS06MI32DEN", null);
            this.DAT02_VS06MI32TAX = new TYData("DAT02_VS06MI32TAX", null);
            this.DAT02_VS06MI33AMT = new TYData("DAT02_VS06MI33AMT", null);
            this.DAT02_VS06MI33NEM = new TYData("DAT02_VS06MI33NEM", null);
            this.DAT02_VS06MI33DEN = new TYData("DAT02_VS06MI33DEN", null);
            this.DAT02_VS06MI33TAX = new TYData("DAT02_VS06MI33TAX", null);
            this.DAT02_VS06MI34AMT = new TYData("DAT02_VS06MI34AMT", null);
            this.DAT02_VS06MI34NEM = new TYData("DAT02_VS06MI34NEM", null);
            this.DAT02_VS06MI34DEN = new TYData("DAT02_VS06MI34DEN", null);
            this.DAT02_VS06MI34TAX = new TYData("DAT02_VS06MI34TAX", null);
            this.DAT02_VS06MI35AMT = new TYData("DAT02_VS06MI35AMT", null);
            this.DAT02_VS06MI35NEM = new TYData("DAT02_VS06MI35NEM", null);
            this.DAT02_VS06MI35DEN = new TYData("DAT02_VS06MI35DEN", null);
            this.DAT02_VS06MI35TAX = new TYData("DAT02_VS06MI35TAX", null);
            this.DAT02_VS06MI36AMT = new TYData("DAT02_VS06MI36AMT", null);
            this.DAT02_VS06MI36NEM = new TYData("DAT02_VS06MI36NEM", null);
            this.DAT02_VS06MI36DEN = new TYData("DAT02_VS06MI36DEN", null);
            this.DAT02_VS06MI36TAX = new TYData("DAT02_VS06MI36TAX", null);
            this.DAT02_VS06MI37AMT = new TYData("DAT02_VS06MI37AMT", null);
            this.DAT02_VS06MI37NEM = new TYData("DAT02_VS06MI37NEM", null);
            this.DAT02_VS06MI37DEN = new TYData("DAT02_VS06MI37DEN", null);
            this.DAT02_VS06MI37TAX = new TYData("DAT02_VS06MI37TAX", null);
            this.DAT02_VS06MI38AMT = new TYData("DAT02_VS06MI38AMT", null);
            this.DAT02_VS06MI38NEM = new TYData("DAT02_VS06MI38NEM", null);
            this.DAT02_VS06MI38DEN = new TYData("DAT02_VS06MI38DEN", null);
            this.DAT02_VS06MI38TAX = new TYData("DAT02_VS06MI38TAX", null);
            this.DAT02_VS14ET39AMT = new TYData("DAT02_VS14ET39AMT", null);
            this.DAT02_VS14ET39NEM = new TYData("DAT02_VS14ET39NEM", null);
            this.DAT02_VS14ET39DEN = new TYData("DAT02_VS14ET39DEN", null);
            this.DAT02_VS14ET39TAX = new TYData("DAT02_VS14ET39TAX", null);
            this.DAT02_VS14ET40AMT = new TYData("DAT02_VS14ET40AMT", null);
            this.DAT02_VS14ET40NEM = new TYData("DAT02_VS14ET40NEM", null);
            this.DAT02_VS14ET40DEN = new TYData("DAT02_VS14ET40DEN", null);
            this.DAT02_VS14ET40TAX = new TYData("DAT02_VS14ET40TAX", null);
            this.DAT02_VS14ET41AMT = new TYData("DAT02_VS14ET41AMT", null);
            this.DAT02_VS14ET41NEM = new TYData("DAT02_VS14ET41NEM", null);
            this.DAT02_VS14ET41DEN = new TYData("DAT02_VS14ET41DEN", null);
            this.DAT02_VS14ET41TAX = new TYData("DAT02_VS14ET41TAX", null);
            this.DAT02_VS14ET42AMT = new TYData("DAT02_VS14ET42AMT", null);
            this.DAT02_VS14ET42NEM = new TYData("DAT02_VS14ET42NEM", null);
            this.DAT02_VS14ET42DEN = new TYData("DAT02_VS14ET42DEN", null);
            this.DAT02_VS14ET42TAX = new TYData("DAT02_VS14ET42TAX", null);
            //this.DAT02_VS14ET43AMT = new TYData("DAT02_VS14ET43AMT", null);
            //this.DAT02_VS14ET43NEM = new TYData("DAT02_VS14ET43NEM", null);
            //this.DAT02_VS14ET43DEN = new TYData("DAT02_VS14ET43DEN", null);
            //this.DAT02_VS14ET43TAX = new TYData("DAT02_VS14ET43TAX", null);
            this.DAT02_VS14ET44AMT = new TYData("DAT02_VS14ET44AMT", null);
            this.DAT02_VS14ET44NEM = new TYData("DAT02_VS14ET44NEM", null);
            this.DAT02_VS14ET44DEN = new TYData("DAT02_VS14ET44DEN", null);
            this.DAT02_VS14ET44TAX = new TYData("DAT02_VS14ET44TAX", null);
            this.DAT02_VS14ET45AMT = new TYData("DAT02_VS14ET45AMT", null);
            this.DAT02_VS14ET45NEM = new TYData("DAT02_VS14ET45NEM", null);
            this.DAT02_VS14ET45DEN = new TYData("DAT02_VS14ET45DEN", null);
            this.DAT02_VS14ET45TAX = new TYData("DAT02_VS14ET45TAX", null);
            this.DAT02_VS14ET46AMT = new TYData("DAT02_VS14ET46AMT", null);
            this.DAT02_VS14ET46NEM = new TYData("DAT02_VS14ET46NEM", null);
            this.DAT02_VS14ET46DEN = new TYData("DAT02_VS14ET46DEN", null);
            this.DAT02_VS14ET46TAX = new TYData("DAT02_VS14ET46TAX", null);

             // 추가 (2014.02.22)
            this.DAT02_VS14NT47AMT = new TYData("DAT02_VS14NT47AMT", null); //외국인관광객
            this.DAT02_VS14NT47NEM = new TYData("DAT02_VS14NT47NEM", null); //세율자
            this.DAT02_VS14NT47DEN = new TYData("DAT02_VS14NT47DEN", null); //세율모
            this.DAT02_VS14NT47TAX = new TYData("DAT02_VS14NT47TAX", null); //세액

            this.DAT02_VS14ET47AMT = new TYData("DAT02_VS14ET47AMT", null);
            this.DAT02_VS14ET47NEM = new TYData("DAT02_VS14ET47NEM", null);
            this.DAT02_VS14ET47DEN = new TYData("DAT02_VS14ET47DEN", null);
            this.DAT02_VS14ET47TAX = new TYData("DAT02_VS14ET47TAX", null);
            this.DAT02_VS16DE48AMT = new TYData("DAT02_VS16DE48AMT", null);
            this.DAT02_VS16DE48NEM = new TYData("DAT02_VS16DE48NEM", null);
            this.DAT02_VS16DE48DEN = new TYData("DAT02_VS16DE48DEN", null);
            this.DAT02_VS16DE48TAX = new TYData("DAT02_VS16DE48TAX", null);
            this.DAT02_VS16DE49AMT = new TYData("DAT02_VS16DE49AMT", null);
            this.DAT02_VS16DE49NEM = new TYData("DAT02_VS16DE49NEM", null);
            this.DAT02_VS16DE49DEN = new TYData("DAT02_VS16DE49DEN", null);
            this.DAT02_VS16DE49TAX = new TYData("DAT02_VS16DE49TAX", null);
            this.DAT02_VS16DE50AMT = new TYData("DAT02_VS16DE50AMT", null);
            this.DAT02_VS16DE50NEM = new TYData("DAT02_VS16DE50NEM", null);
            this.DAT02_VS16DE50DEN = new TYData("DAT02_VS16DE50DEN", null);
            this.DAT02_VS16DE50TAX = new TYData("DAT02_VS16DE50TAX", null);
            this.DAT02_VS16DE51AMT = new TYData("DAT02_VS16DE51AMT", null);
            this.DAT02_VS16DE51NEM = new TYData("DAT02_VS16DE51NEM", null);
            this.DAT02_VS16DE51DEN = new TYData("DAT02_VS16DE51DEN", null);
            this.DAT02_VS16DE51TAX = new TYData("DAT02_VS16DE51TAX", null);
            this.DAT02_VS18BE52AMT = new TYData("DAT02_VS18BE52AMT", null);
            this.DAT02_VS18BE52NEM = new TYData("DAT02_VS18BE52NEM", null);
            this.DAT02_VS18BE52DEN = new TYData("DAT02_VS18BE52DEN", null);
            this.DAT02_VS18BE52TAX = new TYData("DAT02_VS18BE52TAX", null);
            this.DAT02_VS18BE53AMT = new TYData("DAT02_VS18BE53AMT", null);
            this.DAT02_VS18BE53NEM = new TYData("DAT02_VS18BE53NEM", null);
            this.DAT02_VS18BE53DEN = new TYData("DAT02_VS18BE53DEN", null);
            this.DAT02_VS18BE53TAX = new TYData("DAT02_VS18BE53TAX", null);
            this.DAT02_VS18BE54AMT = new TYData("DAT02_VS18BE54AMT", null);
            this.DAT02_VS18BE54NEM = new TYData("DAT02_VS18BE54NEM", null);
            this.DAT02_VS18BE54DEN = new TYData("DAT02_VS18BE54DEN", null);
            this.DAT02_VS18BE54TAX = new TYData("DAT02_VS18BE54TAX", null);
            //this.DAT02_VS18BE55AMT = new TYData("DAT02_VS18BE55AMT", null);
            //this.DAT02_VS18BE55NEM = new TYData("DAT02_VS18BE55NEM", null);
            //this.DAT02_VS18BE55DEN = new TYData("DAT02_VS18BE55DEN", null);
            //this.DAT02_VS18BE55TAX = new TYData("DAT02_VS18BE55TAX", null);
            this.DAT02_VS18BE56AMT = new TYData("DAT02_VS18BE56AMT", null);
            this.DAT02_VS18BE56NEM = new TYData("DAT02_VS18BE56NEM", null);
            this.DAT02_VS18BE56DEN = new TYData("DAT02_VS18BE56DEN", null);
            this.DAT02_VS18BE56TAX = new TYData("DAT02_VS18BE56TAX", null);
            this.DAT02_VS18BE57AMT = new TYData("DAT02_VS18BE57AMT", null);
            this.DAT02_VS18BE57NEM = new TYData("DAT02_VS18BE57NEM", null);
            this.DAT02_VS18BE57DEN = new TYData("DAT02_VS18BE57DEN", null);
            this.DAT02_VS18BE57TAX = new TYData("DAT02_VS18BE57TAX", null);
            this.DAT02_VS18BE58AMT = new TYData("DAT02_VS18BE58AMT", null);
            this.DAT02_VS18BE58NEM = new TYData("DAT02_VS18BE58NEM", null);
            this.DAT02_VS18BE58DEN = new TYData("DAT02_VS18BE58DEN", null);
            this.DAT02_VS18BE58TAX = new TYData("DAT02_VS18BE58TAX", null);
            this.DAT02_VS24AD59AMT = new TYData("DAT02_VS24AD59AMT", null);
            this.DAT02_VS24AD59NEM = new TYData("DAT02_VS24AD59NEM", null);
            this.DAT02_VS24AD59DEN = new TYData("DAT02_VS24AD59DEN", null);
            this.DAT02_VS24AD59TAX = new TYData("DAT02_VS24AD59TAX", null);
            this.DAT02_VS24AD60AMT = new TYData("DAT02_VS24AD60AMT", null);
            this.DAT02_VS24AD60NEM = new TYData("DAT02_VS24AD60NEM", null);
            this.DAT02_VS24AD60DEN = new TYData("DAT02_VS24AD60DEN", null);
            this.DAT02_VS24AD60TAX = new TYData("DAT02_VS24AD60TAX", null);
            this.DAT02_VS24AD61AMT = new TYData("DAT02_VS24AD61AMT", null);
            this.DAT02_VS24AD61NEM = new TYData("DAT02_VS24AD61NEM", null);
            this.DAT02_VS24AD61DEN = new TYData("DAT02_VS24AD61DEN", null);
            this.DAT02_VS24AD61TAX = new TYData("DAT02_VS24AD61TAX", null);
            this.DAT02_VS24AD62AMT = new TYData("DAT02_VS24AD62AMT", null);
            this.DAT02_VS24AD62NEM = new TYData("DAT02_VS24AD62NEM", null);
            this.DAT02_VS24AD62DEN = new TYData("DAT02_VS24AD62DEN", null);
            this.DAT02_VS24AD62TAX = new TYData("DAT02_VS24AD62TAX", null);
            this.DAT02_VS24AD63AMT = new TYData("DAT02_VS24AD63AMT", null);
            this.DAT02_VS24AD63NEM = new TYData("DAT02_VS24AD63NEM", null);
            this.DAT02_VS24AD63DEN = new TYData("DAT02_VS24AD63DEN", null);
            this.DAT02_VS24AD63TAX = new TYData("DAT02_VS24AD63TAX", null);
            this.DAT02_VS24AD64AMT = new TYData("DAT02_VS24AD64AMT", null);
            this.DAT02_VS24AD64NEM = new TYData("DAT02_VS24AD64NEM", null);
            this.DAT02_VS24AD64DEN = new TYData("DAT02_VS24AD64DEN", null);
            this.DAT02_VS24AD64TAX = new TYData("DAT02_VS24AD64TAX", null);
            this.DAT02_VS24AD65AMT = new TYData("DAT02_VS24AD65AMT", null);
            this.DAT02_VS24AD65NEM = new TYData("DAT02_VS24AD65NEM", null);
            this.DAT02_VS24AD65DEN = new TYData("DAT02_VS24AD65DEN", null);
            this.DAT02_VS24AD65TAX = new TYData("DAT02_VS24AD65TAX", null);
            this.DAT02_VS24AD66AMT = new TYData("DAT02_VS24AD66AMT", null);
            this.DAT02_VS24AD66NEM = new TYData("DAT02_VS24AD66NEM", null);
            this.DAT02_VS24AD66DEN = new TYData("DAT02_VS24AD66DEN", null);
            this.DAT02_VS24AD66TAX = new TYData("DAT02_VS24AD66TAX", null);
            this.DAT02_VS24AD67AMT = new TYData("DAT02_VS24AD67AMT", null);
            this.DAT02_VS24AD67NEM = new TYData("DAT02_VS24AD67NEM", null);
            this.DAT02_VS24AD67DEN = new TYData("DAT02_VS24AD67DEN", null);
            this.DAT02_VS24AD67TAX = new TYData("DAT02_VS24AD67TAX", null);
            this.DAT02_VS24AD68AMT = new TYData("DAT02_VS24AD68AMT", null);
            this.DAT02_VS24AD68NEM = new TYData("DAT02_VS24AD68NEM", null);
            this.DAT02_VS24AD68DEN = new TYData("DAT02_VS24AD68DEN", null);
            this.DAT02_VS24AD68TAX = new TYData("DAT02_VS24AD68TAX", null);
            this.DAT02_VS24AD69AMT = new TYData("DAT02_VS24AD69AMT", null);
            this.DAT02_VS24AD69NEM = new TYData("DAT02_VS24AD69NEM", null);
            this.DAT02_VS24AD69DEN = new TYData("DAT02_VS24AD69DEN", null);
            this.DAT02_VS24AD69TAX = new TYData("DAT02_VS24AD69TAX", null);
            this.DAT02_VS24AD70AMT = new TYData("DAT02_VS24AD70AMT", null);
            this.DAT02_VS24AD70NEM = new TYData("DAT02_VS24AD70NEM", null);
            this.DAT02_VS24AD70DEN = new TYData("DAT02_VS24AD70DEN", null);
            this.DAT02_VS24AD70TAX = new TYData("DAT02_VS24AD70TAX", null);
            this.DAT02_VS24AD71AMT = new TYData("DAT02_VS24AD71AMT", null);
            this.DAT02_VS24AD71NEM = new TYData("DAT02_VS24AD71NEM", null);
            this.DAT02_VS24AD71DEN = new TYData("DAT02_VS24AD71DEN", null);
            this.DAT02_VS24AD71TAX = new TYData("DAT02_VS24AD71TAX", null);
            this.DAT02_VS24AD72AMT = new TYData("DAT02_VS24AD72AMT", null);
            this.DAT02_VS24AD72NEM = new TYData("DAT02_VS24AD72NEM", null);
            this.DAT02_VS24AD72DEN = new TYData("DAT02_VS24AD72DEN", null);
            this.DAT02_VS24AD72TAX = new TYData("DAT02_VS24AD72TAX", null);
            this.DAT02_VS24AD73AMT = new TYData("DAT02_VS24AD73AMT", null);
            this.DAT02_VS24AD73NEM = new TYData("DAT02_VS24AD73NEM", null);
            this.DAT02_VS24AD73DEN = new TYData("DAT02_VS24AD73DEN", null);
            this.DAT02_VS24AD73TAX = new TYData("DAT02_VS24AD73TAX", null);
            this.DAT02_VS24AD74AMT = new TYData("DAT02_VS24AD74AMT", null);
            this.DAT02_VS24AD74NEM = new TYData("DAT02_VS24AD74NEM", null);
            this.DAT02_VS24AD74DEN = new TYData("DAT02_VS24AD74DEN", null);
            this.DAT02_VS24AD74TAX = new TYData("DAT02_VS24AD74TAX", null);

            this.DAT02_VS24AR75AMT = new TYData("DAT02_VS24AR75AMT", null); // 추가(2014.04.22) 거래계좌미사용
            this.DAT02_VS24AR75NEM = new TYData("DAT02_VS24AR75NEM", null); // 추가(2014.04.22) 세율자
            this.DAT02_VS24AR75DEN = new TYData("DAT02_VS24AR75DEN", null); // 추가(2014.04.22) 세율모
            this.DAT02_VS24AR75TAX = new TYData("DAT02_VS24AR75TAX", null); // 추가(2014.04.22) 세액
            this.DAT02_VS24AR76AMT = new TYData("DAT02_VS24AR76AMT", null); // 추가(2014.04.22) 거래계좌지연입금
            this.DAT02_VS24AR76NEM = new TYData("DAT02_VS24AR76NEM", null); // 추가(2014.04.22) 세율자
            this.DAT02_VS24AR76DEN = new TYData("DAT02_VS24AR76DEN", null); // 추가(2014.04.22) 세율모
            this.DAT02_VS24AR76TAX = new TYData("DAT02_VS24AR76TAX", null); // 추가(2014.04.22) 세액

            this.DAT02_VS24AD75AMT = new TYData("DAT02_VS24AD75AMT", null);
            this.DAT02_VS24AD75NEM = new TYData("DAT02_VS24AD75NEM", null);
            this.DAT02_VS24AD75DEN = new TYData("DAT02_VS24AD75DEN", null);
            this.DAT02_VS24AD75TAX = new TYData("DAT02_VS24AD75TAX", null);
            this.DAT02_VS25EX76BUS = new TYData("DAT02_VS25EX76BUS", null);
            this.DAT02_VS25EX76NAM = new TYData("DAT02_VS25EX76NAM", null);
            this.DAT02_VS25EX76COD = new TYData("DAT02_VS25EX76COD", null);
            this.DAT02_VS25EX76AMT = new TYData("DAT02_VS25EX76AMT", null);
            this.DAT02_VS25EX77BUS = new TYData("DAT02_VS25EX77BUS", null);
            this.DAT02_VS25EX77NAM = new TYData("DAT02_VS25EX77NAM", null);
            this.DAT02_VS25EX77COD = new TYData("DAT02_VS25EX77COD", null);
            this.DAT02_VS25EX77AMT = new TYData("DAT02_VS25EX77AMT", null);
            this.DAT02_VS25EX78BUS = new TYData("DAT02_VS25EX78BUS", null);
            this.DAT02_VS25EX78NAM = new TYData("DAT02_VS25EX78NAM", null);
            this.DAT02_VS25EX78COD = new TYData("DAT02_VS25EX78COD", null);
            this.DAT02_VS25EX78AMT = new TYData("DAT02_VS25EX78AMT", null);
            this.DAT02_VS25EX79AMT = new TYData("DAT02_VS25EX79AMT", null);
            this.DAT02_VS26IS80AMT = new TYData("DAT02_VS26IS80AMT", null);
            this.DAT02_VS26IS81AMT = new TYData("DAT02_VS26IS81AMT", null);
            this.DAT02_VSREFUNDGB  = new TYData("DAT02_VSREFUNDGB",  null);

            fsVSYEAR    = sVSYEAR.ToString();
            fsVSBRANCH  = sVSBRANCH.ToString();
            fsVSCONFGB  = sVSCONFGB.ToString();
            fsPOPUP     = sPOPUP.ToString();

            this.MinimumSize = new System.Drawing.Size(0, 0);

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void TYACTX019S_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(this.DAT02_VSYEAR);
            this.ControlFactory.Add(this.DAT02_VSBRANCH);
            this.ControlFactory.Add(this.DAT02_VSVENDCD);
            this.ControlFactory.Add(this.DAT02_VSRPTGUBN);
            this.ControlFactory.Add(this.DAT02_VSCONFGB);
            this.ControlFactory.Add(this.DAT02_VS01ST01AMT);
            this.ControlFactory.Add(this.DAT02_VS01ST01NEM);
            this.ControlFactory.Add(this.DAT02_VS01ST01DEN);
            this.ControlFactory.Add(this.DAT02_VS01ST01TAX);
            this.ControlFactory.Add(this.DAT02_VS01ST02AMT);
            this.ControlFactory.Add(this.DAT02_VS01ST02NEM);
            this.ControlFactory.Add(this.DAT02_VS01ST02DEN);
            this.ControlFactory.Add(this.DAT02_VS01ST02TAX);
            this.ControlFactory.Add(this.DAT02_VS01ST03AMT);
            this.ControlFactory.Add(this.DAT02_VS01ST03NEM);
            this.ControlFactory.Add(this.DAT02_VS01ST03DEN);
            this.ControlFactory.Add(this.DAT02_VS01ST03TAX);
            this.ControlFactory.Add(this.DAT02_VS01ST04AMT);
            this.ControlFactory.Add(this.DAT02_VS01ST04NEM);
            this.ControlFactory.Add(this.DAT02_VS01ST04DEN);
            this.ControlFactory.Add(this.DAT02_VS01ST04TAX);
            this.ControlFactory.Add(this.DAT02_VS01ST05AMT);
            this.ControlFactory.Add(this.DAT02_VS01ST05NEM);
            this.ControlFactory.Add(this.DAT02_VS01ST05DEN);
            this.ControlFactory.Add(this.DAT02_VS01ST05TAX);
            this.ControlFactory.Add(this.DAT02_VS01ST06AMT);
            this.ControlFactory.Add(this.DAT02_VS01ST06NEM);
            this.ControlFactory.Add(this.DAT02_VS01ST06DEN);
            this.ControlFactory.Add(this.DAT02_VS01ST06TAX);
            this.ControlFactory.Add(this.DAT02_VS01ST07AMT);
            this.ControlFactory.Add(this.DAT02_VS01ST07NEM);
            this.ControlFactory.Add(this.DAT02_VS01ST07DEN);
            this.ControlFactory.Add(this.DAT02_VS01ST07TAX);
            this.ControlFactory.Add(this.DAT02_VS01ST08AMT);
            this.ControlFactory.Add(this.DAT02_VS01ST08NEM);
            this.ControlFactory.Add(this.DAT02_VS01ST08DEN);
            this.ControlFactory.Add(this.DAT02_VS01ST08TAX);
            this.ControlFactory.Add(this.DAT02_VS01ST09AMT);
            this.ControlFactory.Add(this.DAT02_VS01ST09NEM);
            this.ControlFactory.Add(this.DAT02_VS01ST09DEN);
            this.ControlFactory.Add(this.DAT02_VS01ST09TAX);
            this.ControlFactory.Add(this.DAT02_VS02PU10AMT);
            this.ControlFactory.Add(this.DAT02_VS02PU10NEM);
            this.ControlFactory.Add(this.DAT02_VS02PU10DEN);
            this.ControlFactory.Add(this.DAT02_VS02PU10TAX);
            this.ControlFactory.Add(this.DAT02_VS02PU11AMT);
            this.ControlFactory.Add(this.DAT02_VS02PU11NEM);
            this.ControlFactory.Add(this.DAT02_VS02PU11DEN);
            this.ControlFactory.Add(this.DAT02_VS02PU11TAX);
            this.ControlFactory.Add(this.DAT02_VS02PU12AMT);
            this.ControlFactory.Add(this.DAT02_VS02PU12NEM);
            this.ControlFactory.Add(this.DAT02_VS02PU12DEN);
            this.ControlFactory.Add(this.DAT02_VS02PU12TAX);
            this.ControlFactory.Add(this.DAT02_VS02PU13AMT);
            this.ControlFactory.Add(this.DAT02_VS02PU13NEM);
            this.ControlFactory.Add(this.DAT02_VS02PU13DEN);
            this.ControlFactory.Add(this.DAT02_VS02PU13TAX);
            this.ControlFactory.Add(this.DAT02_VS02PU14AMT);
            this.ControlFactory.Add(this.DAT02_VS02PU14NEM);
            this.ControlFactory.Add(this.DAT02_VS02PU14DEN);
            this.ControlFactory.Add(this.DAT02_VS02PU14TAX);
            this.ControlFactory.Add(this.DAT02_VS02PU15AMT);
            this.ControlFactory.Add(this.DAT02_VS02PU15NEM);
            this.ControlFactory.Add(this.DAT02_VS02PU15DEN);
            this.ControlFactory.Add(this.DAT02_VS02PU15TAX);
            this.ControlFactory.Add(this.DAT02_VS02PU16AMT);
            this.ControlFactory.Add(this.DAT02_VS02PU16NEM);
            this.ControlFactory.Add(this.DAT02_VS02PU16DEN);
            this.ControlFactory.Add(this.DAT02_VS02PU16TAX);
            this.ControlFactory.Add(this.DAT02_VS02PU17AMT);
            this.ControlFactory.Add(this.DAT02_VS02PU17NEM);
            this.ControlFactory.Add(this.DAT02_VS02PU17DEN);
            this.ControlFactory.Add(this.DAT02_VS02PU17TAX);
            this.ControlFactory.Add(this.DAT02_VSPAYMENEM);
            this.ControlFactory.Add(this.DAT02_VSPAYMEDEN);
            this.ControlFactory.Add(this.DAT02_VSPAYMETAX);
            this.ControlFactory.Add(this.DAT02_VS03RE18AMT);
            this.ControlFactory.Add(this.DAT02_VS03RE18NEM);
            this.ControlFactory.Add(this.DAT02_VS03RE18DEN);
            this.ControlFactory.Add(this.DAT02_VS03RE18TAX);
            this.ControlFactory.Add(this.DAT02_VS03RE19AMT);
            this.ControlFactory.Add(this.DAT02_VS03RE19NEM);
            this.ControlFactory.Add(this.DAT02_VS03RE19DEN);
            this.ControlFactory.Add(this.DAT02_VS03RE19TAX);
            this.ControlFactory.Add(this.DAT02_VS03RE20AMT);
            this.ControlFactory.Add(this.DAT02_VS03RE20NEM);
            this.ControlFactory.Add(this.DAT02_VS03RE20DEN);
            this.ControlFactory.Add(this.DAT02_VS03RE20TAX);
            this.ControlFactory.Add(this.DAT02_VS04SC21AMT);
            this.ControlFactory.Add(this.DAT02_VS04SC21NEM);
            this.ControlFactory.Add(this.DAT02_VS04SC21DEN);
            this.ControlFactory.Add(this.DAT02_VS04SC21TAX);
            this.ControlFactory.Add(this.DAT02_VS04SC22AMT);
            this.ControlFactory.Add(this.DAT02_VS04SC22NEM);
            this.ControlFactory.Add(this.DAT02_VS04SC22DEN);
            this.ControlFactory.Add(this.DAT02_VS04SC22TAX);

            this.ControlFactory.Add(this.DAT02_VS04YC23AMT);  // 추가(2014.04.22)
            this.ControlFactory.Add(this.DAT02_VS04YC23NEM);  // 추가(2014.04.22)
            this.ControlFactory.Add(this.DAT02_VS04YC23DEN);  // 추가(2014.04.22)
            this.ControlFactory.Add(this.DAT02_VS04YC23TAX);  // 추가(2014.04.22)

            this.ControlFactory.Add(this.DAT02_VS04SC23AMT);
            this.ControlFactory.Add(this.DAT02_VS04SC23NEM);
            this.ControlFactory.Add(this.DAT02_VS04SC23DEN);
            this.ControlFactory.Add(this.DAT02_VS04SC23TAX);
            this.ControlFactory.Add(this.DAT02_VS04SC24AMT);
            this.ControlFactory.Add(this.DAT02_VS04SC24NEM);
            this.ControlFactory.Add(this.DAT02_VS04SC24DEN);
            this.ControlFactory.Add(this.DAT02_VS04SC24TAX);
            this.ControlFactory.Add(this.DAT02_VS04SC25TAX);
            this.ControlFactory.Add(this.DAT02_VSTOTTAXAMT);
            this.ControlFactory.Add(this.DAT02_VSREBANKCD);
            this.ControlFactory.Add(this.DAT02_VSREACCCODE);
            this.ControlFactory.Add(this.DAT02_VSSHUTDATE);
            this.ControlFactory.Add(this.DAT02_VSSHUTSAYU);
            this.ControlFactory.Add(this.DAT02_VS05EV26BUS);
            this.ControlFactory.Add(this.DAT02_VS05EV26NAM);
            this.ControlFactory.Add(this.DAT02_VS05EV26COD);
            this.ControlFactory.Add(this.DAT02_VS05EV26AMT);
            this.ControlFactory.Add(this.DAT02_VS05EV27BUS);
            this.ControlFactory.Add(this.DAT02_VS05EV27NAM);
            this.ControlFactory.Add(this.DAT02_VS05EV27COD);
            this.ControlFactory.Add(this.DAT02_VS05EV27AMT);
            this.ControlFactory.Add(this.DAT02_VS05EV28BUS);
            this.ControlFactory.Add(this.DAT02_VS05EV28NAM);
            this.ControlFactory.Add(this.DAT02_VS05EV28COD);
            this.ControlFactory.Add(this.DAT02_VS05EV28AMT);
            this.ControlFactory.Add(this.DAT02_VS05EV29BUS);
            this.ControlFactory.Add(this.DAT02_VS05EV29NAM);
            this.ControlFactory.Add(this.DAT02_VS05EV29COD);
            this.ControlFactory.Add(this.DAT02_VS05EV29AMT);
            this.ControlFactory.Add(this.DAT02_VS05EV30AMT);
            this.ControlFactory.Add(this.DAT02_VS06MI31AMT);
            this.ControlFactory.Add(this.DAT02_VS06MI31NEM);
            this.ControlFactory.Add(this.DAT02_VS06MI31DEN);
            this.ControlFactory.Add(this.DAT02_VS06MI31TAX);
            this.ControlFactory.Add(this.DAT02_VS06MI32AMT);
            this.ControlFactory.Add(this.DAT02_VS06MI32NEM);
            this.ControlFactory.Add(this.DAT02_VS06MI32DEN);
            this.ControlFactory.Add(this.DAT02_VS06MI32TAX);
            this.ControlFactory.Add(this.DAT02_VS06MI33AMT);
            this.ControlFactory.Add(this.DAT02_VS06MI33NEM);
            this.ControlFactory.Add(this.DAT02_VS06MI33DEN);
            this.ControlFactory.Add(this.DAT02_VS06MI33TAX);
            this.ControlFactory.Add(this.DAT02_VS06MI34AMT);
            this.ControlFactory.Add(this.DAT02_VS06MI34NEM);
            this.ControlFactory.Add(this.DAT02_VS06MI34DEN);
            this.ControlFactory.Add(this.DAT02_VS06MI34TAX);
            this.ControlFactory.Add(this.DAT02_VS06MI35AMT);
            this.ControlFactory.Add(this.DAT02_VS06MI35NEM);
            this.ControlFactory.Add(this.DAT02_VS06MI35DEN);
            this.ControlFactory.Add(this.DAT02_VS06MI35TAX);
            this.ControlFactory.Add(this.DAT02_VS06MI36AMT);
            this.ControlFactory.Add(this.DAT02_VS06MI36NEM);
            this.ControlFactory.Add(this.DAT02_VS06MI36DEN);
            this.ControlFactory.Add(this.DAT02_VS06MI36TAX);
            this.ControlFactory.Add(this.DAT02_VS06MI37AMT);
            this.ControlFactory.Add(this.DAT02_VS06MI37NEM);
            this.ControlFactory.Add(this.DAT02_VS06MI37DEN);
            this.ControlFactory.Add(this.DAT02_VS06MI37TAX);
            this.ControlFactory.Add(this.DAT02_VS06MI38AMT);
            this.ControlFactory.Add(this.DAT02_VS06MI38NEM);
            this.ControlFactory.Add(this.DAT02_VS06MI38DEN);
            this.ControlFactory.Add(this.DAT02_VS06MI38TAX);
            this.ControlFactory.Add(this.DAT02_VS14ET39AMT);
            this.ControlFactory.Add(this.DAT02_VS14ET39NEM);
            this.ControlFactory.Add(this.DAT02_VS14ET39DEN);
            this.ControlFactory.Add(this.DAT02_VS14ET39TAX);
            this.ControlFactory.Add(this.DAT02_VS14ET40AMT);
            this.ControlFactory.Add(this.DAT02_VS14ET40NEM);
            this.ControlFactory.Add(this.DAT02_VS14ET40DEN);
            this.ControlFactory.Add(this.DAT02_VS14ET40TAX);
            this.ControlFactory.Add(this.DAT02_VS14ET41AMT);
            this.ControlFactory.Add(this.DAT02_VS14ET41NEM);
            this.ControlFactory.Add(this.DAT02_VS14ET41DEN);
            this.ControlFactory.Add(this.DAT02_VS14ET41TAX);
            this.ControlFactory.Add(this.DAT02_VS14ET42AMT);
            this.ControlFactory.Add(this.DAT02_VS14ET42NEM);
            this.ControlFactory.Add(this.DAT02_VS14ET42DEN);
            this.ControlFactory.Add(this.DAT02_VS14ET42TAX);
            //this.ControlFactory.Add(this.DAT02_VS14ET43AMT);
            //this.ControlFactory.Add(this.DAT02_VS14ET43NEM);
            //this.ControlFactory.Add(this.DAT02_VS14ET43DEN);
            //this.ControlFactory.Add(this.DAT02_VS14ET43TAX);
            this.ControlFactory.Add(this.DAT02_VS14ET44AMT);
            this.ControlFactory.Add(this.DAT02_VS14ET44NEM);
            this.ControlFactory.Add(this.DAT02_VS14ET44DEN);
            this.ControlFactory.Add(this.DAT02_VS14ET44TAX);
            this.ControlFactory.Add(this.DAT02_VS14ET45AMT);
            this.ControlFactory.Add(this.DAT02_VS14ET45NEM);
            this.ControlFactory.Add(this.DAT02_VS14ET45DEN);
            this.ControlFactory.Add(this.DAT02_VS14ET45TAX);
            this.ControlFactory.Add(this.DAT02_VS14ET46AMT);
            this.ControlFactory.Add(this.DAT02_VS14ET46NEM);
            this.ControlFactory.Add(this.DAT02_VS14ET46DEN);
            this.ControlFactory.Add(this.DAT02_VS14ET46TAX);
            // 추가(2014.02.22)
            this.ControlFactory.Add(this.DAT02_VS14NT47AMT); //외국인관광객
            this.ControlFactory.Add(this.DAT02_VS14NT47NEM); //세율자
            this.ControlFactory.Add(this.DAT02_VS14NT47DEN); //세율모
            this.ControlFactory.Add(this.DAT02_VS14NT47TAX); //세액

            this.ControlFactory.Add(this.DAT02_VS14ET47AMT);
            this.ControlFactory.Add(this.DAT02_VS14ET47NEM);
            this.ControlFactory.Add(this.DAT02_VS14ET47DEN);
            this.ControlFactory.Add(this.DAT02_VS14ET47TAX);
            this.ControlFactory.Add(this.DAT02_VS16DE48AMT);
            this.ControlFactory.Add(this.DAT02_VS16DE48NEM);
            this.ControlFactory.Add(this.DAT02_VS16DE48DEN);
            this.ControlFactory.Add(this.DAT02_VS16DE48TAX);
            this.ControlFactory.Add(this.DAT02_VS16DE49AMT);
            this.ControlFactory.Add(this.DAT02_VS16DE49NEM);
            this.ControlFactory.Add(this.DAT02_VS16DE49DEN);
            this.ControlFactory.Add(this.DAT02_VS16DE49TAX);
            this.ControlFactory.Add(this.DAT02_VS16DE50AMT);
            this.ControlFactory.Add(this.DAT02_VS16DE50NEM);
            this.ControlFactory.Add(this.DAT02_VS16DE50DEN);
            this.ControlFactory.Add(this.DAT02_VS16DE50TAX);
            this.ControlFactory.Add(this.DAT02_VS16DE51AMT);
            this.ControlFactory.Add(this.DAT02_VS16DE51NEM);
            this.ControlFactory.Add(this.DAT02_VS16DE51DEN);
            this.ControlFactory.Add(this.DAT02_VS16DE51TAX);
            this.ControlFactory.Add(this.DAT02_VS18BE52AMT);
            this.ControlFactory.Add(this.DAT02_VS18BE52NEM);
            this.ControlFactory.Add(this.DAT02_VS18BE52DEN);
            this.ControlFactory.Add(this.DAT02_VS18BE52TAX);
            this.ControlFactory.Add(this.DAT02_VS18BE53AMT);
            this.ControlFactory.Add(this.DAT02_VS18BE53NEM);
            this.ControlFactory.Add(this.DAT02_VS18BE53DEN);
            this.ControlFactory.Add(this.DAT02_VS18BE53TAX);
            this.ControlFactory.Add(this.DAT02_VS18BE54AMT);
            this.ControlFactory.Add(this.DAT02_VS18BE54NEM);
            this.ControlFactory.Add(this.DAT02_VS18BE54DEN);
            this.ControlFactory.Add(this.DAT02_VS18BE54TAX);
            //this.ControlFactory.Add(this.DAT02_VS18BE55AMT);
            //this.ControlFactory.Add(this.DAT02_VS18BE55NEM);
            //this.ControlFactory.Add(this.DAT02_VS18BE55DEN);
            //this.ControlFactory.Add(this.DAT02_VS18BE55TAX);
            this.ControlFactory.Add(this.DAT02_VS18BE56AMT);
            this.ControlFactory.Add(this.DAT02_VS18BE56NEM);
            this.ControlFactory.Add(this.DAT02_VS18BE56DEN);
            this.ControlFactory.Add(this.DAT02_VS18BE56TAX);
            this.ControlFactory.Add(this.DAT02_VS18BE57AMT);
            this.ControlFactory.Add(this.DAT02_VS18BE57NEM);
            this.ControlFactory.Add(this.DAT02_VS18BE57DEN);
            this.ControlFactory.Add(this.DAT02_VS18BE57TAX);
            this.ControlFactory.Add(this.DAT02_VS18BE58AMT);
            this.ControlFactory.Add(this.DAT02_VS18BE58NEM);
            this.ControlFactory.Add(this.DAT02_VS18BE58DEN);
            this.ControlFactory.Add(this.DAT02_VS18BE58TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD59AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD59NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD59DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD59TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD60AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD60NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD60DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD60TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD61AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD61NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD61DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD61TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD62AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD62NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD62DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD62TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD63AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD63NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD63DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD63TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD64AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD64NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD64DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD64TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD65AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD65NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD65DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD65TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD66AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD66NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD66DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD66TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD67AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD67NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD67DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD67TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD68AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD68NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD68DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD68TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD69AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD69NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD69DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD69TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD70AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD70NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD70DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD70TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD71AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD71NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD71DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD71TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD72AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD72NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD72DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD72TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD73AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD73NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD73DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD73TAX);
            this.ControlFactory.Add(this.DAT02_VS24AD74AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD74NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD74DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD74TAX);

            this.ControlFactory.Add(this.DAT02_VS24AR75AMT); // 추가(2014.04.22) 거래계좌미사용
            this.ControlFactory.Add(this.DAT02_VS24AR75NEM); // 추가(2014.04.22) 세율자
            this.ControlFactory.Add(this.DAT02_VS24AR75DEN); // 추가(2014.04.22) 세율모
            this.ControlFactory.Add(this.DAT02_VS24AR75TAX); // 추가(2014.04.22) 세액
            this.ControlFactory.Add(this.DAT02_VS24AR76AMT); // 추가(2014.04.22) 거래계좌지연입금
            this.ControlFactory.Add(this.DAT02_VS24AR76NEM); // 추가(2014.04.22) 세율자
            this.ControlFactory.Add(this.DAT02_VS24AR76DEN); // 추가(2014.04.22) 세율모
            this.ControlFactory.Add(this.DAT02_VS24AR76TAX); // 추가(2014.04.22) 세액

            this.ControlFactory.Add(this.DAT02_VS24AD75AMT);
            this.ControlFactory.Add(this.DAT02_VS24AD75NEM);
            this.ControlFactory.Add(this.DAT02_VS24AD75DEN);
            this.ControlFactory.Add(this.DAT02_VS24AD75TAX);
            this.ControlFactory.Add(this.DAT02_VS25EX76BUS);
            this.ControlFactory.Add(this.DAT02_VS25EX76NAM);
            this.ControlFactory.Add(this.DAT02_VS25EX76COD);
            this.ControlFactory.Add(this.DAT02_VS25EX76AMT);
            this.ControlFactory.Add(this.DAT02_VS25EX77BUS);
            this.ControlFactory.Add(this.DAT02_VS25EX77NAM);
            this.ControlFactory.Add(this.DAT02_VS25EX77COD);
            this.ControlFactory.Add(this.DAT02_VS25EX77AMT);
            this.ControlFactory.Add(this.DAT02_VS25EX78BUS);
            this.ControlFactory.Add(this.DAT02_VS25EX78NAM);
            this.ControlFactory.Add(this.DAT02_VS25EX78COD);
            this.ControlFactory.Add(this.DAT02_VS25EX78AMT);
            this.ControlFactory.Add(this.DAT02_VS25EX79AMT);
            this.ControlFactory.Add(this.DAT02_VS26IS80AMT);
            this.ControlFactory.Add(this.DAT02_VS26IS81AMT);

            this.ControlFactory.Add(this.DAT02_VSREFUNDGB);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (fsPOPUP.ToString() != "")
            {
                this.TXT01_VSYEAR.SetValue(fsVSYEAR.ToString());
                this.CBO01_VSBRANCH.SetValue(fsVSBRANCH.ToString());
                this.CBO01_VSCONFGB.SetValue(fsVSCONFGB.ToString());

                UP_CREATE();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                this.WindowState = FormWindowState.Normal;

                UP_Cookie_Load();

                SetStartingFocus(this.TXT01_VSYEAR);

                this.FPS91_TY_S_AC_41713969.Initialize();

                UP_Spread_Title();
            }
        }
        #endregion

        #region Description : 자동 생성
        private void UP_CREATE()
        {
            if (this.CBO01_VSBRANCH.GetValue().ToString() == "1")
            {
                this.DAT02_VSVENDCD.SetValue("102885");
            }
            else
            {
                this.DAT02_VSVENDCD.SetValue("349876");
            }

            // 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_41926058", this.TXT01_VSYEAR.GetValue().ToString(),
                                                        this.CBO01_VSBRANCH.GetValue().ToString(),
                                                        this.DAT02_VSVENDCD.GetValue().ToString(),
                                                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                                                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2));
            this.DbConnector.ExecuteNonQuery();

            this.BTN61_INQ_Click(null, null);

            // 필드변수에 저장
            UP_Set_Field();

            UP_SAVE();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_FieldClear();
            
            UP_Spread_Title();
            UP_Spread_Desc();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4194A061",
                this.TXT01_VSYEAR.GetValue().ToString(),
                this.CBO01_VSBRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 신고내용
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_4193A060",
                    this.TXT01_VSYEAR.GetValue().ToString(),
                    this.CBO01_VSBRANCH.GetValue().ToString(),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    UP_Spread_Dt1_Fill(dt);
                }

                // 국세환급금계좌신고, 폐업신고
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_41958062",
                    this.TXT01_VSYEAR.GetValue().ToString(),
                    this.CBO01_VSBRANCH.GetValue().ToString(),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 국세환급금계좌신고
                    this.CBH01_VSREBANKCD.SetValue(dt.Rows[0]["VSREBANKCD"].ToString());
                    this.TXT01_VSREACCCODE.SetValue(dt.Rows[0]["VSREACCCODE"].ToString());
                    // 폐업신고
                    this.TXT01_VSSHUTDATE.SetValue(dt.Rows[0]["VSSHUTDATE"].ToString());
                    this.TXT01_VSSHUTSAYU.SetValue(dt.Rows[0]["VSSHUTSAYU"].ToString());

                    // 환급구분
                    this.CBO01_VSREFUNDGB.SetValue(dt.Rows[0]["VSREFUNDGB"].ToString());
                }

                // 과세표준
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_4195Q064",
                    this.TXT01_VSYEAR.GetValue().ToString(),
                    this.CBO01_VSBRANCH.GetValue().ToString(),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    UP_Spread_Dt2_Fill(dt);
                }

                // 신고서2 조회 여기부터
                // 신고서2 조회(7~18)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_41A4G072",
                    this.TXT01_VSYEAR.GetValue().ToString(),
                    this.CBO01_VSBRANCH.GetValue().ToString(),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    UP_Spread_Dt3_Fill(dt);
                }

                // 신고서2 - 가산세 명세 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_41A4M073",
                    this.TXT01_VSYEAR.GetValue().ToString(),
                    this.CBO01_VSBRANCH.GetValue().ToString(),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    UP_Spread_Dt5_Fill(dt);
                }

                // 신고서2 - 면세사업 수입금액 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_41A4Q074",
                    this.TXT01_VSYEAR.GetValue().ToString(),
                    this.CBO01_VSBRANCH.GetValue().ToString(),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    UP_Spread_Dt6_Fill(dt);
                }

                // 계산서 발급 및 수취 명세
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_41A4T075",
                    this.TXT01_VSYEAR.GetValue().ToString(),
                    this.CBO01_VSBRANCH.GetValue().ToString(),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_VS26IS80AMT.SetValue(dt.Rows[0]["VS26IS80AMT"].ToString());
                    this.TXT01_VS26IS81AMT.SetValue(dt.Rows[0]["VS26IS81AMT"].ToString());
                }

                UP_Compute();
            }
            else
            {
                UP_Spread_Busok_Fill();

                UP_Compute();

                // 신고서2 가산세 명세 세액 합계 -> 신고서1 가산세액계 세액 합계
                // 수정(2014.04.22)
                //this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[24, 6].Value = this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[16, 6].Text.ToString();
                this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[25, 6].Value = this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[18, 6].Text.ToString();

                dt.Clone();
                dt.Dispose();

                //this.BTN61_SAV_ProcessCheck(null, null);
                //UP_SAVE();
            }

            // 마감
            for (int i = 1; i <= 2; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_42B8L317",
                    this.TXT01_VSYEAR.GetValue().ToString(),
                    Convert.ToString(i),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    if (i == 1)
                    {
                        this.TXT01_MAGAM1.SetValue("본점-마감완료");
                    }
                    else
                    {
                        this.TXT01_MAGAM2.SetValue("지점-마감완료");
                    }
                }
                else
                {
                    if (i == 1)
                    {
                        this.TXT01_MAGAM1.SetValue("본점-미마감");
                    }
                    else
                    {
                        this.TXT01_MAGAM2.SetValue("지점-미마감");
                    }
                }
            }

            // 마감시 필드 잠금
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B8L317",
                this.TXT01_VSYEAR.GetValue().ToString(),
                this.CBO01_VSBRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                UP_Field_ReadOnly();
            }

            this.SetFocus(TXT01_VSYEAR);

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 마감시 필드 잠금
        private void UP_Field_ReadOnly()
        {
            int i = 0;

            for (i = 0; i < this.FPS91_TY_S_AC_3CV3F913_Sheet1.RowCount; i++)
            {
                this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Locked = true;
            }

            for (i = 0; i < this.FPS91_TY_S_AC_4121M919_Sheet1.RowCount; i++)
            {
                this.FPS91_TY_S_AC_4121M919.ActiveSheet.Rows[i].Locked = true;
            }

            for (i = 0; i < this.FPS91_TY_S_AC_4179D965_Sheet1.RowCount; i++)
            {
                this.FPS91_TY_S_AC_4179D965.ActiveSheet.Rows[i].Locked = true;
            }

            for (i = 0; i < this.FPS91_TY_S_AC_417AN968_Sheet1.RowCount; i++)
            {
                this.FPS91_TY_S_AC_417AN968.ActiveSheet.Rows[i].Locked = true;
            }

            for (i = 0; i < this.FPS91_TY_S_AC_41713969_Sheet1.RowCount; i++)
            {
                this.FPS91_TY_S_AC_41713969.ActiveSheet.Rows[i].Locked = true;
            }

            // 국세환급금계좌신고
            this.CBH01_VSREBANKCD.SetReadOnlyButton(true);
            this.CBH01_VSREBANKCD.SetReadOnlyCode(true);
            this.CBH01_VSREBANKCD.SetReadOnlyText(true);
            this.TXT01_VSREACCCODE.SetReadOnly(true);
            this.CBO01_VSREFUNDGB.SetReadOnly(true);

            // 폐업신고
            this.TXT01_VSSHUTDATE.SetReadOnly(true);
            this.TXT01_VSSHUTSAYU.SetReadOnly(true);

            // 계산서발급 및 수취명세
            this.TXT01_VS26IS80AMT.SetReadOnly(true);
            this.TXT01_VS26IS81AMT.SetReadOnly(true);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            UP_SAVE();

            this.ShowMessage("TY_M_GB_23NAD873");

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 부속서류 저장
        private void UP_SAVE()
        {
            // 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_41926058", this.DAT02_VSYEAR.GetValue().ToString(),
                                                        this.DAT02_VSBRANCH.GetValue().ToString(),
                                                        this.DAT02_VSVENDCD.GetValue().ToString(),
                                                        this.DAT02_VSRPTGUBN.GetValue().ToString(),
                                                        this.DAT02_VSCONFGB.GetValue().ToString());
            this.DbConnector.ExecuteNonQuery();

            // 저장
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_41921059", this.ControlFactory, "02");
            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4183O053",
                this.TXT01_VSYEAR.GetValue().ToString(),
                this.CBO01_VSBRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C910655",
                this.TXT01_VSYEAR.GetValue().ToString(),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2),
                this.CBO01_VSBRANCH.GetValue().ToString()
                );

            DataTable dt2 = this.DbConnector.ExecuteDataTable();

            if (Convert.ToInt16(Get_Numeric(this.TXT01_VSYEAR.GetValue().ToString()).Substring(0, 4)) >= 2020)
            {
                if (Convert.ToInt16(Get_Numeric(this.TXT01_VSYEAR.GetValue().ToString()).Substring(0, 4)) == 2020 && this.CBO01_VSCONFGB.GetValue().ToString() == "11")
                {
                    SectionReport rpt = new TYACTX019R1(dt2, this.TXT01_VSYEAR.GetValue().ToString(), getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2));
                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    SectionReport rpt = new TYACTX019R2(dt2, this.TXT01_VSYEAR.GetValue().ToString(), getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2));
                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
            else if (Convert.ToInt16(this.TXT01_VSYEAR.GetValue().ToString().Substring(0, 4)) >= 2016 && Convert.ToInt16(this.TXT01_VSYEAR.GetValue().ToString().Substring(0, 4)) <= 2019)
            {
                SectionReport rpt = new TYACTX019R1(dt2, this.TXT01_VSYEAR.GetValue().ToString(), getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2));
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                SectionReport rpt = new TYACTX019R(dt2, this.TXT01_VSYEAR.GetValue().ToString(), getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2));
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }

            //SectionReport rpt = new TYACTX019R(dt2, this.TXT01_VSYEAR.GetValue().ToString(), getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2));
            //(new TYERGB001P(rpt, dt)).ShowDialog();

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 부속서류 바로가기 버튼
        private void BTN61_BTNBUSOK_Click(object sender, EventArgs e)
        {
            switch(fiRow)
            {
                // 세금계산서 - 매출
                case 0:

                    if ((new TYACTX011S(this.TXT01_VSYEAR.GetValue().ToString(),   this.CBO01_VSBRANCH.GetValue().ToString(),
                                        this.CBO01_VSCONFGB.GetValue().ToString(), "2",
                                        "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.TXT01_VSYEAR.Focus();

                    break;

                case 3:

                    this.ShowMessage("TY_M_AC_41E21089");

                break;

                // 영세율 첨부서류제출명세서
                case 4:

                if ((new TYACTX017S(this.TXT01_VSYEAR.GetValue().ToString(),   this.CBO01_VSBRANCH.GetValue().ToString(),
                                    this.CBO01_VSCONFGB.GetValue().ToString(), "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.TXT01_VSYEAR.Focus();

                    break;

                // 수출실적명세
                case 5:

                    if ((new TYACTX016S(this.TXT01_VSYEAR.GetValue().ToString(),   this.CBO01_VSBRANCH.GetValue().ToString(),
                                        this.CBO01_VSCONFGB.GetValue().ToString(), "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.TXT01_VSYEAR.Focus();

                    break;

                // 예정신고누락분 - 매출
                case 6:

                    if ((new TYACTX003S(this.TXT01_VSYEAR.GetValue().ToString(),                 this.CBO01_VSBRANCH.GetValue().ToString(),
                                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), "2",
                                        "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.TXT01_VSYEAR.Focus();

                    break;

                // 세금계산서 - 매입
                case 9:

                    if ((new TYACTX011S(this.TXT01_VSYEAR.GetValue().ToString(),   this.CBO01_VSBRANCH.GetValue().ToString(),
                                        this.CBO01_VSCONFGB.GetValue().ToString(), "1",
                                        "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.TXT01_VSYEAR.Focus();

                    break;

                // 건물등감가상각 자산취득
                case 10:

                    if ((new TYACTX013S(this.TXT01_VSYEAR.GetValue().ToString(),   this.CBO01_VSBRANCH.GetValue().ToString(),
                                        this.CBO01_VSCONFGB.GetValue().ToString(), "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.TXT01_VSYEAR.Focus();

                    break;

                // 예정신고누락분 - 매입
                case 11:

                    if ((new TYACTX003S(this.TXT01_VSYEAR.GetValue().ToString(),                 this.CBO01_VSBRANCH.GetValue().ToString(),
                                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), "1",
                                        "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.TXT01_VSYEAR.Focus();

                    break;

                // 신용카드매출전표등 수취명세
                case 13:

                    if ((new TYACTX014S(this.TXT01_VSYEAR.GetValue().ToString(),   this.CBO01_VSBRANCH.GetValue().ToString(),
                                        this.CBO01_VSCONFGB.GetValue().ToString(), "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.TXT01_VSYEAR.Focus();

                    break;

                // 공제받지 못할 매입명세
                case 15:

                    if ((new TYACTX015S(this.TXT01_VSYEAR.GetValue().ToString(),   this.CBO01_VSBRANCH.GetValue().ToString(),
                                        this.CBO01_VSCONFGB.GetValue().ToString(), "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.TXT01_VSYEAR.Focus();

                    break;
            }

        }
        #endregion

        #region Description : 신고내용 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3CV3F913_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Busok_Display(e.Row, this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[e.Row, 6].Text.ToString());
        }
        #endregion

        #region Description : 신고내용 스프레드 Enter 이벤트
        private void FPS91_TY_S_AC_3CV3F913_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            fiRow = 0;

            fiRow = e.Row;
        }

        private void FPS91_TY_S_AC_3CV3F913_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                UP_Busok_Display(fiRow, this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[fiRow, 6].Text.ToString());
            }
        }
        #endregion

        #region Description : 가산세 명세 합계 -> 신고서1의 가산세액계로 이동
        private void FPS91_TY_S_AC_417AN968_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            // 수정(2014.04.22)
            //this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[24, 6].Value = this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[16, 6].Text.ToString();
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[25, 6].Value = this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[18, 6].Text.ToString();
        }
        #endregion

        #region Description : 부속서류 보여주기
        private void UP_Busok_Display(int i, string sVAT)
        {
            string sSTDATE   = string.Empty;
            string sEDDATE   = string.Empty;
            string sTAXCDGN1 = string.Empty;
            string sTAXCDGN2 = string.Empty;
            string sCDAC     = string.Empty;

            DataTable dt = new DataTable();

            this.FPS91_TY_S_AC_4121J918.Initialize();

            switch (i)
            {
                // 세금계산서 합계표 - 매출
                case 0:

                    fiRow = i;

                    sTAXCDGN1 = "11,19,61,68,69";

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_4199W055",
                        this.TXT01_VSYEAR.GetValue().ToString(),
                        this.CBO01_VSBRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2),
                        "2",
                        sTAXCDGN1.ToString(),
                        sTAXCDGN1.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();
                    this.FPS91_TY_S_AC_4121J918.SetValue(dt);

                    break;

                // 기타과세분
                case 3:

                    fiRow = i;

                    sSTDATE = "";
                    sEDDATE = "";

                    sTAXCDGN1 = "20";

                    if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1) == "1") // 1기
                    {
                        if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2) == "1") // 예정
                        {
                            sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "01";
                            sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "03";
                        }
                        else
                        {
                            sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "04";
                            sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "06";
                        }
                    }
                    else // 2기
                    {
                        if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2) == "1") // 예정
                        {
                            sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "07";
                            sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "09";
                        }
                        else
                        {
                            sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "10";
                            sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "12";
                        }
                    }

                    if (this.CBO01_VSBRANCH.GetValue().ToString() == "1")
                    {
                        sCDAC = "21103101";
                    }
                    else
                    {
                        sCDAC = "21103102";
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_41DAY082",
                        sSTDATE.ToString(),
                        sEDDATE.ToString(),
                        sCDAC.ToString(),
                        sTAXCDGN1.ToString(),
                        this.TXT01_VSYEAR.GetValue().ToString(),                 // 년도
                        this.CBO01_VSBRANCH.GetValue().ToString(),               // 본점, 지점
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                        "2",                                                     // 매입, 매출
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                        sTAXCDGN1.ToString()                                     // 세무코드
                        );

                    dt = this.DbConnector.ExecuteDataTable();
                    this.FPS91_TY_S_AC_4121J918.SetValue(dt);

                    break;

                // 영세율 첨부서류 제출명세
                case 4:

                    fiRow = i;

                    sTAXCDGN1 = "12,62";

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_41E26093",
                        this.TXT01_VSYEAR.GetValue().ToString(),
                        this.CBO01_VSBRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                        "2",
                        sTAXCDGN1.ToString(),
                        sTAXCDGN1.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();
                    this.FPS91_TY_S_AC_4121J918.SetValue(dt);

                    break;

                // 수출실적명세
                case 5:

                    fiRow = i;

                    sTAXCDGN1 = "13";

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_41E28094",
                        this.TXT01_VSYEAR.GetValue().ToString(),
                        this.CBO01_VSBRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                        "2",
                        sTAXCDGN1.ToString(),
                        sTAXCDGN1.ToString(),
                        sTAXCDGN1.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();
                    this.FPS91_TY_S_AC_4121J918.SetValue(dt);

                    break;

                // 예정신고누락분 - 매출
                case 6:

                    fiRow = i;

                    sTAXCDGN1 = "11,19,61,68,69,20,12,62,13";

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_41E24095",
                        this.TXT01_VSYEAR.GetValue().ToString(),
                        this.CBO01_VSBRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                        sTAXCDGN1.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();
                    this.FPS91_TY_S_AC_4121J918.SetValue(dt);

                    break;

                // 세금계산서 합계표 - 매입
                case 9:

                    fiRow = i;

                    sTAXCDGN1 = "51,52,54,55,71,72,74,75";
                    sTAXCDGN2 = "51,54,55,71,74,75";

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_41RBG161",
                        this.TXT01_VSYEAR.GetValue().ToString(),
                        this.CBO01_VSBRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                        "1",
                        sTAXCDGN1.ToString(),
                        sTAXCDGN2.ToString(),
                        sTAXCDGN1.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();
                    this.FPS91_TY_S_AC_4121J918.SetValue(dt);

                    break;

                // 건물등감가상각 자산취득명세
                case 10:

                    fiRow = i;

                    sTAXCDGN1 = "51,71,54,55,74,75";

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_41E2E096",
                        this.TXT01_VSYEAR.GetValue().ToString(),
                        this.CBO01_VSBRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                        sTAXCDGN1.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();
                    this.FPS91_TY_S_AC_4121J918.SetValue(dt);

                    break;

                // 예정신고누락분 - 매입
                case 11:

                    fiRow = i;

                    sTAXCDGN1 = "51,52,54,55,71,72,74,75";

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_41E24095",
                        this.TXT01_VSYEAR.GetValue().ToString(),
                        this.CBO01_VSBRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                        sTAXCDGN1.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();
                    this.FPS91_TY_S_AC_4121J918.SetValue(dt);

                    break;

                // 신용카드매출전표등 수취명세서
                case 13:

                    fiRow = i;

                    sTAXCDGN1 = "57,58";

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_41E2H097",
                        this.TXT01_VSYEAR.GetValue().ToString(),
                        this.CBO01_VSBRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                        "1",
                        sTAXCDGN1.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();
                    this.FPS91_TY_S_AC_4121J918.SetValue(dt);

                    break;

                // 공제받지 못할 매입세액
                case 15:

                    fiRow = i;

                    sTAXCDGN1 = "54,55,74,75";

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_41E2J098",
                        this.TXT01_VSYEAR.GetValue().ToString(),
                        this.CBO01_VSBRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                        sTAXCDGN1.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();
                    this.FPS91_TY_S_AC_4121J918.SetValue(dt);

                    break;
            }

            if (this.FPS91_TY_S_AC_4121J918.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_4121J918, "CODE", "합 계", SumRowType.Total, "CNT", "AMT", "VAT");
            }

            string sBUSOK_VAT = string.Empty;

            for (int k = 0; k < FPS91_TY_S_AC_4121J918.CurrentRowCount; k++)
            {
                if (this.FPS91_TY_S_AC_4121J918.GetValue(k, "CODE").ToString() == "합 계")
                {
                    sBUSOK_VAT = this.FPS91_TY_S_AC_4121J918.ActiveSheet.Cells[k, 4].Value.ToString();
                }
            }

            this.TXT01_MESSAGE.SetValue("");

            if ((Get_Numeric(sVAT.ToString()).ToString() != Get_Numeric(sBUSOK_VAT.ToString())) && (Get_Numeric(sBUSOK_VAT.ToString()) != "0"))
            {
                this.TXT01_MESSAGE.SetValue("신고서와의 세액 차이: " + string.Format("{0:#,###}", Convert.ToDouble(Get_Numeric(sVAT.ToString())) - Convert.ToDouble(Get_Numeric(sBUSOK_VAT.ToString()))));
            }
        }
        #endregion

        #region Description : FieldClear
        private void UP_FieldClear()
        {
            this.FPS91_TY_S_AC_4121J918.Initialize();
            this.FPS91_TY_S_AC_4121M919.Initialize();

            this.FPS91_TY_S_AC_4179D965.Initialize();
            this.FPS91_TY_S_AC_417AN968.Initialize();
            this.FPS91_TY_S_AC_41713969.Initialize();

            this.CBH01_VSREBANKCD.SetValue("");
            this.TXT01_VSREACCCODE.SetValue("");
            this.TXT01_VSSHUTDATE.SetValue("");
            this.TXT01_VSSHUTSAYU.SetValue("");

            this.TXT01_VS26IS80AMT.SetValue("");
            this.TXT01_VS26IS81AMT.SetValue("");

            this.TXT01_MAGAM1.SetValue("");
            this.TXT01_MAGAM2.SetValue("");
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            #region Description : 신고서1

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 4);

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.ColumnHeader.Cells[0, 0].Value = "구   분";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.ColumnHeader.Cells[0, 4].Value = "금   액";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.ColumnHeader.Cells[0, 5].Value = "세   율";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.ColumnHeader.Cells[0, 6].Value = "세   액";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            #endregion

            #region Description : 신고서2

            this.FPS91_TY_S_AC_4179D965_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_4179D965_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_4179D965_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 6);

            this.FPS91_TY_S_AC_4179D965_Sheet1.ColumnHeader.Cells[0, 0].Value = "구   분";

            this.FPS91_TY_S_AC_4179D965_Sheet1.ColumnHeader.Cells[0, 6].Value = "금   액";
            this.FPS91_TY_S_AC_4179D965_Sheet1.ColumnHeader.Cells[0, 7].Value = "세   율";
            this.FPS91_TY_S_AC_4179D965_Sheet1.ColumnHeader.Cells[0, 8].Value = "세   액";

            this.FPS91_TY_S_AC_4179D965_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;




            this.FPS91_TY_S_AC_417AN968_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_417AN968_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_417AN968_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 4);

            this.FPS91_TY_S_AC_417AN968_Sheet1.ColumnHeader.Cells[0, 0].Value = "구   분";

            this.FPS91_TY_S_AC_417AN968_Sheet1.ColumnHeader.Cells[0, 4].Value = "금   액";
            this.FPS91_TY_S_AC_417AN968_Sheet1.ColumnHeader.Cells[0, 5].Value = "세   율";
            this.FPS91_TY_S_AC_417AN968_Sheet1.ColumnHeader.Cells[0, 6].Value = "세   액";

            this.FPS91_TY_S_AC_417AN968_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            #endregion
        }
        #endregion

        #region Description : 스프레드 틀 만들기
        private void UP_Spread_Desc()
        {
            #region Description : 부가세 - 신고서1

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.ColumnCount = 7;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.RowCount    = 28;

            #region Description : 과세표준 및 매출세액

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(0, 0, 9, 1); // 과세표준
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(0, 1, 4, 1); // 과세
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(4, 1, 2, 1); // 영세율

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(6, 1, 1, 2);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(7, 1, 1, 2);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(8, 1, 1, 2);

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0, 0].Value = "과세표준 및 매출세액";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0, 1].Value = "과  세";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[4, 1].Value = "영세율";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0, 2].Value = "세금계산서 발급분";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[1, 2].Value = "매입자발행세금계산서";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[2, 2].Value = "신용카드.현금영수증 발행분";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[3, 2].Value = "기타(정규영수증 외 매출분)";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[4, 2].Value = "세금계산서 발급분";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[5, 2].Value = "기             타";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[6, 1].Value = "예정신고누락분";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[7, 1].Value = "대손세액가감";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[8, 1].Value = "합       계";


            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0, 3].Value = "(1)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[1, 3].Value = "(2)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[2, 3].Value = "(3)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[3, 3].Value = "(4)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[4, 3].Value = "(5)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[5, 3].Value = "(6)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[6, 3].Value = "(7)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[7, 3].Value = "(8)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[8, 3].Value = "(9)";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0, 5].Value = "10/100";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[1, 5].Value = "10/100";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[2, 5].Value = "10/100";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[3, 5].Value = "10/100";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[4, 5].Value = "0/100";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[5, 5].Value = "0/100";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[8, 5].Value = "";




            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[4, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[2, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[3, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[4, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[5, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[6, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[7, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[8, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[2, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[3, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[4, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[5, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[6, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[7, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[8, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[2, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[3, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[4, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[5, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[8, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            #endregion

            #region Description : 매입세액

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(9, 0, 8, 1);  // 매입세액
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(9, 1, 2, 1);  // 세금계산서 수취분

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(17, 0, 1, 5); // 납부세액

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(11, 1, 1, 2);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(12, 1, 1, 2);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(13, 1, 1, 2);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(14, 1, 1, 2);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(15, 1, 1, 2);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(16, 1, 1, 2);

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[9, 0].Value = "매입세액";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[9, 1].Value = "세금계산서 수취분";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[9,  2].Value = "일반매입";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[10, 2].Value = "고정자산 매입";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[11, 1].Value = "예정신고누락분";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[12, 1].Value = "매입자발행세금계산서";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[13, 1].Value = "그 밖의 공제매입세액";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[14, 1].Value = "합계(10+11+12+13+14)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[15, 1].Value = "공제받지 못할 매입세액";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[16, 1].Value = "차감계(15-16)";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[17, 0].Value = "납부(환급)세액(매출세액㉮-매입세액㉯)";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[9, 3].Value  = "(10)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[10, 3].Value = "(11)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[11, 3].Value = "(12)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[12, 3].Value = "(13)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[13, 3].Value = "(14)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[14, 3].Value = "(15)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[15, 3].Value = "(16)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[16, 3].Value = "(17)";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[16, 5].Value = "";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[17, 5].Value = "";


            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[9,  0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[9,  1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[9,  2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[10, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[11, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[12, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[13, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[14, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[15, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[16, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[17, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[9, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[10, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[11, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[12, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[13, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[14, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[15, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[16, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[16, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[17, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            #endregion

            #region Description : 경감.공제 세액

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(18, 0, 3, 1); // 경감.공제 세액
            
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(18, 1, 1, 2);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(19, 1, 1, 2);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(20, 1, 1, 2);

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(21, 0, 1, 3);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(22, 0, 1, 3);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(23, 0, 1, 3);  // 추가(2014.04.22)
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(24, 0, 1, 3);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(25, 0, 1, 3);

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(26, 0, 1, 5);
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.AddSpanCell(27, 0, 1, 6);

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[18, 0].Value = "경감.공제 세액";


            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[18, 1].Value = "그 밖의 경감.공제세액";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[19, 1].Value = "신용카드매출전표 등 발행공제 등";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[20, 1].Value = "합     계";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[21, 0].Value = "예정신고미환급세액";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[22, 0].Value = "예정고지세액";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[23, 0].Value = "사업양수자의 대리납부 기납부세액"; // 추가(2014.04.22)
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[24, 0].Value = "매입자 납부특례 기납부세액"; // 수정(2014.04.22) 명변경--> 금지금 매입자 납부특례 기납부세액
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[25, 0].Value = "가산세액계";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[26, 0].Value = "차가감하여 납부할 세액(환급받을 세액) - (㉰-㉱-㉲-㉳-㉴-㉵+㉶)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[27, 0].Value = "총괄납부사업자가 납부할 세액(환급받을 세액)";


            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[18, 3].Value = "(18)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[19, 3].Value = "(19)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[20, 3].Value = "(20)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[21, 3].Value = "(21)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[22, 3].Value = "(22)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[23, 3].Value = "(23)"; // 추가(2014.04.22)
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[24, 3].Value = "(24)";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[25, 3].Value = "(25)";


            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[20, 5].Value = "";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[21, 5].Value = "";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[22, 5].Value = "";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[23, 5].Value = ""; // 추가(2014.04.22)
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[24, 5].Value = "";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[25, 5].Value = "";
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[26, 5].Value = "";

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[27, 6].Value = "";


            TTextCellType tmpCellType = new TTextCellType();

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[18, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[18, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[19, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[20, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[21, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[22, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[23, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center; // 추가(2014.04.22)
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[24, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[25, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[26, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[27, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[18, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[19, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[20, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[21, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[22, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[23, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center; // 추가(2014.04.22)
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[24, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[25, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[20, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[21, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[22, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[23, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center; // 추가(2014.04.22)
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[24, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[25, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[26, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            #endregion

            #region Description : 색깔 및 Lock

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0,  6].Locked = false;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[3,  6].Locked = false;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[21, 6].Locked = false;
            //this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[24, 6].Locked = false;

            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0,  6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[3,  6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[21, 6].BackColor = Color.Yellow;
            //this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[24, 6].BackColor = Color.Yellow;

            #endregion

            #region Description : 부가세 - 과세표준명세

            this.FPS91_TY_S_AC_4121M919_Sheet1.ColumnCount = 4;
            this.FPS91_TY_S_AC_4121M919_Sheet1.RowCount    = 5;

            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[3, 0].Value = "수입금액제외";
            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[4, 0].Value = "합  계";

            #region Description : 색깔 및 Lock

            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[0, 3].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[1, 3].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[2, 3].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[3, 3].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[4, 3].BackColor = Color.Yellow;

            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[4, 3].Locked = true;

            #endregion

            #endregion

            #endregion

            #region Description : 부가세 - 신고서2

            this.FPS91_TY_S_AC_4179D965_Sheet1.ColumnCount = 9;
            //수정 (2014.02.22)
            //this.FPS91_TY_S_AC_4179D965_Sheet1.RowCount    = 28;
            this.FPS91_TY_S_AC_4179D965_Sheet1.RowCount = 27;

            #region Description : 예정신고 누락분 명세

            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(0, 0, 8, 1);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(0, 1, 5, 1);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(5, 1, 3, 1);

            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(2, 2, 2, 1);

            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(0, 3, 1, 2);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(1, 3, 1, 2);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(2, 3, 1, 2);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(3, 3, 1, 2);

            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(4, 2, 1, 3);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(5, 2, 1, 3);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(6, 2, 1, 3);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(7, 2, 1, 3);
            

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[0, 0].Value = "예정신고 누락분 명세";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[0, 1].Value = "매  출";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[5, 1].Value = "매  입";

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[0, 2].Value = "과  세";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[2, 2].Value = "영세율";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[4, 2].Value = "합  계";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[5, 2].Value = "세금계산서";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[6, 2].Value = "그 밖의 공제매입세액";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[7, 2].Value = "합  계";

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[0, 3].Value = "세금계산서";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[1, 3].Value = "기  타";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[2, 3].Value = "세금계산서";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[3, 3].Value = "기  타";

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[0, 5].Value = "(32)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[1, 5].Value = "(33)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[2, 5].Value = "(34)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[3, 5].Value = "(35)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[4, 5].Value = "(36)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[5, 5].Value = "(37)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[6, 5].Value = "(38)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[7, 5].Value = "(39)";


            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[0, 7].Value = "10/100";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[1, 7].Value = "10/100";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[2, 7].Value = "0/100";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[3, 7].Value = "0/100";

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[5, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[2, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[4, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[5, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[6, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[7, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[2, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[3, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[2, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[3, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[4, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[5, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[6, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[7, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[2, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[3, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            #endregion

            #region Description : 기타공제 매입세액 명세

            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(8, 0, 9, 1);

            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(8, 1, 1, 3);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(9, 1, 1, 3);

            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(10, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(11, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(12, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(13, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(14, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(15, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(16, 1, 1, 4);

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[8,  0].Value = "그 밖의 공제 매입세액 명세";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[8,  1].Value = "신용카드매출전표등";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[9,  1].Value = "수령명세서 제출분";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[10, 1].Value = "의제매입세액";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[11, 1].Value = "재활용폐자원등매입세액";
            //this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[12, 1].Value = "고금 의제매입세액";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[12, 1].Value = "과세사업전환 매입세액";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[13, 1].Value = "재고매입세액";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[14, 1].Value = "변제대손세액";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[15, 1].Value = "외국인 관광객에 대한 환급세액";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[16, 1].Value = "합  계";

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[8,  4].Value = "일반매입";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[9,  4].Value = "고정자산매입";

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[8,  5].Value = "(40)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[9,  5].Value = "(41)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[10, 5].Value = "(42)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[11, 5].Value = "(43)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[12, 5].Value = "(44)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[13, 5].Value = "(45)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[14, 5].Value = "(46)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[15, 5].Value = "(47)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[16, 5].Value = "(48)";

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[10, 7].Value = "";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[11, 7].Value = "";
            //this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[10, 7].Value = "뒤쪽참조";
            //this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[11, 7].Value = "뒤쪽참조";


            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[8,  0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[8,  1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[9,  1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[10, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[11, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[12, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[13, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[14, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[15, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[16, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[8,  4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[9,  4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[8,  5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[9,  5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[10, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[11, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[12, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[13, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[14, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[15, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[16, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[10, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[11, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            #endregion

            #region Description : 공제받지 못할매입 명세

            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(17, 0, 3, 1);

            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(17, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(18, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(19, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(20, 1, 1, 4);

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[17, 0].Value = "공제받지 못할매입 명세";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[17, 1].Value = "공제받지 못할 매입세액";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[18, 1].Value = "공통매입세액 면세사업분";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[19, 1].Value = "대손처분받은세액";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[20, 1].Value = "합   계";

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[17, 5].Value = "(49)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[18, 5].Value = "(50)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[19, 5].Value = "(51)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[20, 5].Value = "(52)";

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[17, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[17, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[18, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[19, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[20, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[17, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[18, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[19, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[20, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


            #endregion

            #region Description : 기타 경감.공제세액 명세
            // 수정(2014.02.22) -1 처리됨
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(21, 0, 6, 1);

            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(21, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(22, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(23, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(24, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(25, 1, 1, 4);
            this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(26, 1, 1, 4);
            //this.FPS91_TY_S_AC_4179D965_Sheet1.AddSpanCell(27, 1, 1, 4);

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[21, 0].Value = "그 밖의 경감.공제세액 명세";

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[21, 1].Value = "전자신고세액공제";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[22, 1].Value = "전자세금계산서 발급세액 공제";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[23, 1].Value = "택시운송사업자경감세액";
           // this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[24, 1].Value = "원산지확인서 발급세액 공제";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[24, 1].Value = "현금영수증사업자세액공제";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[25, 1].Value = "기   타";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[26, 1].Value = "합   계";

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[21, 5].Value = "(53)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[22, 5].Value = "(54)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[23, 5].Value = "(55)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[24, 5].Value = "(56)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[25, 5].Value = "(57)";
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[26, 5].Value = "(58)";
            //this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[27, 5].Value = "(58)";


            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[21, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[21, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[22, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[23, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[24, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[25, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[26, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[27, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[21, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[22, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[23, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[24, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[25, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[26, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[27, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            #endregion
            
            #region Description : 가산세 명세

            // 가산세 명세
            this.FPS91_TY_S_AC_417AN968_Sheet1.ColumnCount = 7;
            this.FPS91_TY_S_AC_417AN968_Sheet1.RowCount    = 19;

            this.FPS91_TY_S_AC_417AN968_Sheet1.AddSpanCell(0, 0, 19, 1);

            this.FPS91_TY_S_AC_417AN968_Sheet1.AddSpanCell(0, 1, 1, 2); // 사업자미등록등 타이틀 합하기
            this.FPS91_TY_S_AC_417AN968_Sheet1.AddSpanCell(1, 1, 3, 1); // 세금계산서 타이틀 합하기
            this.FPS91_TY_S_AC_417AN968_Sheet1.AddSpanCell(8, 1, 4, 1); // 신고불성실 타이틀 합하기


            this.FPS91_TY_S_AC_417AN968_Sheet1.AddSpanCell(12, 1, 1, 2);
            this.FPS91_TY_S_AC_417AN968_Sheet1.AddSpanCell(13, 1, 1, 2);
            this.FPS91_TY_S_AC_417AN968_Sheet1.AddSpanCell(14, 1, 1, 2);
            this.FPS91_TY_S_AC_417AN968_Sheet1.AddSpanCell(15, 1, 1, 2);
            this.FPS91_TY_S_AC_417AN968_Sheet1.AddSpanCell(18, 1, 1, 2);

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[0,  0].Value = "가산세 명세";

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[0,  1].Value = "사 업 자 미 등 록 등";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[1,  1].Value = "세금계산서";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[4,  1].Value = "전자세금계산서";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[5,  1].Value = "발급명세전송";

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[6,  1].Value = "세금계산서";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[7,  1].Value = "합계표";

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[8,  1].Value = "신고불성실";

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[12, 1].Value = "납부불성실";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[13, 1].Value = "영세율 과세표준 불성실";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[14, 1].Value = "현금매출명세서 미제출 등";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[15, 1].Value = "부동산임대공금가액명세서 불성실";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[16, 1].Value = "매입자";   // 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[17, 1].Value = "납부특례"; // 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[18, 1].Value = "합   계";

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[1,  2].Value = "지연발급 등";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[2,  2].Value = "지연수취";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[3,  2].Value = "미발급 등";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[4,  2].Value = "지연전송";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[5,  2].Value = "미전송";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[6,  2].Value = "제출불성실";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[7,  2].Value = "지연제출";

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[8,  2].Value = "무신고(일반)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[9,  2].Value = "무신고(부담)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[10, 2].Value = "과소.초과환급신고(일반)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[11, 2].Value = "과소.초과환급신고(부담)";

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[16, 2].Value = "거래계좌미사용";    // 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[17, 2].Value = "거래계좌 지연입금"; // 추가(2014.02.22)


            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[0,  3].Value = "(59)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[1,  3].Value = "(60)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[2,  3].Value = "(61)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[3,  3].Value = "(62)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[4,  3].Value = "(63)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[5,  3].Value = "(64)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[6,  3].Value = "(65)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[7,  3].Value = "(66)";

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[8,  3].Value = "(67)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[9,  3].Value = "(68)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[10, 3].Value = "(69)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[11, 3].Value = "(70)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[12, 3].Value = "(71)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[13, 3].Value = "(72)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[14, 3].Value = "(73)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[15, 3].Value = "(74)";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[16, 3].Value = "(75)";// 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[17, 3].Value = "(76)";// 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[18, 3].Value = "(77)";


            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[0,  5].Value = "1/100";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[1,  5].Value = "1/100";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[2,  5].Value = "1/100";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[3,  5].Value = "2/100";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[4,  5].Value = "1/1000";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[5,  5].Value = "3/1000";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[6,  5].Value = "1/100";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[7,  5].Value = "5/1000";

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[8, 5].Value  = "";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[9, 5].Value  = "";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[10, 5].Value = "";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[11, 5].Value = "";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[12, 5].Value = "";

            //this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[8,  5].Value = "뒤쪽참조";
            //this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[9,  5].Value = "뒤쪽참조";
            //this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[10, 5].Value = "뒤쪽참조";
            //this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[11, 5].Value = "뒤쪽참조";
            //this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[12, 5].Value = "뒤쪽참조";

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[13, 5].Value = "5/1000";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[14, 5].Value = "1/100";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[15, 5].Value = "1/100";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[16, 5].Value = "";
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[17, 5].Value = "";

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[0,  0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[0,  1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[1,  1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[4,  1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[5,  1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[6,  1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[7,  1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[8,  1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[12, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[13, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[14, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[15, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[16, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;// 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[17, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;// 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[18, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[0,  3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[1,  3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[2,  3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[3,  3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[4,  3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[5,  3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[6,  3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[7,  3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[8,  3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[9,  3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[10, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[11, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[12, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[13, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[14, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[15, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;// 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[16, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;// 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[17, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[18, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[0,  5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[1,  5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[2,  5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[3,  5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[4,  5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[5,  5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[6,  5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[7,  5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[8,  5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[9,  5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[10, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[11, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[12, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[13, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[14, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[15, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[16, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center; // 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[17, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center; // 추가(2014.02.22)


            #region Description : 색깔 및 Lock

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[18, 4].Locked = true;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[18, 6].Locked = true;

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[0,  4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[1,  4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[2,  4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[3,  4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[4,  4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[5,  4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[6,  4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[7,  4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[8,  4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[9,  4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[10, 4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[11, 4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[12, 4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[13, 4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[14, 4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[15, 4].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[16, 4].BackColor = Color.Yellow;// 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[17, 4].BackColor = Color.Yellow;// 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[18, 4].BackColor = Color.Yellow;

            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[0,  6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[1,  6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[2,  6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[3,  6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[4,  6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[5,  6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[6,  6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[7,  6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[8,  6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[9,  6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[10, 6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[11, 6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[12, 6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[13, 6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[14, 6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[15, 6].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[16, 6].BackColor = Color.Yellow;// 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[17, 6].BackColor = Color.Yellow;// 추가(2014.02.22)
            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[18, 6].BackColor = Color.Yellow;

            #endregion

            #endregion

            #region Description : 부가세 - 과세표준명세

            this.FPS91_TY_S_AC_41713969_Sheet1.ColumnCount = 4;
            this.FPS91_TY_S_AC_41713969_Sheet1.RowCount    = 4;

            this.FPS91_TY_S_AC_41713969_Sheet1.Cells[2, 0].Value = "수입금액제외";
            this.FPS91_TY_S_AC_41713969_Sheet1.Cells[3, 2].Value = "합  계";

            this.FPS91_TY_S_AC_41713969_Sheet1.Cells[0, 3].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_41713969_Sheet1.Cells[1, 3].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_41713969_Sheet1.Cells[2, 3].BackColor = Color.Yellow;
            this.FPS91_TY_S_AC_41713969_Sheet1.Cells[3, 3].BackColor = Color.Yellow;

            #endregion

            #endregion

            // 국세환급금계좌신고
            this.CBH01_VSREBANKCD.SetReadOnlyButton(false);
            this.CBH01_VSREBANKCD.SetReadOnlyCode(false);
            this.CBH01_VSREBANKCD.SetReadOnlyText(false);
            this.TXT01_VSREACCCODE.SetReadOnly(false);
            this.CBO01_VSREFUNDGB.SetReadOnly(false);

            // 폐업신고
            this.TXT01_VSSHUTDATE.SetReadOnly(false);
            this.TXT01_VSSHUTSAYU.SetReadOnly(false);

            // 계산서발급 및 수취명세
            this.TXT01_VS26IS80AMT.SetReadOnly(false);
            this.TXT01_VS26IS81AMT.SetReadOnly(false);
        }
        #endregion

        #region Description : 스프레드 신고내용 채우기
        private void UP_Spread_Dt1_Fill(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.FPS91_TY_S_AC_3CV3F913_Sheet1.SetRowHeight(i, 23);

                this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = string.Format("{0:###0}", dt.Rows[i]["AMT"].ToString());
                this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Value = string.Format("{0:###0}", dt.Rows[i]["SEYUL"].ToString());
                this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = string.Format("{0:###0}", dt.Rows[i]["VAT"].ToString());
            }
        }
        #endregion

        #region Description : 스프레드 과세표준명세 채우기
        private void UP_Spread_Dt2_Fill(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 3 && i != 4)
                {
                    this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 0].Value = dt.Rows[i]["UPTAE"].ToString();
                    this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 1].Value = dt.Rows[i]["JONGMOK"].ToString();
                    this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 2].Value = dt.Rows[i]["JONGCODE"].ToString();
                }
                this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 3].Value = dt.Rows[i]["AMT"].ToString();
            }
        }
        #endregion

        #region Description : 스프레드 신고서2(7~18) 채우기
        private void UP_Spread_Dt3_Fill(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.FPS91_TY_S_AC_4179D965_Sheet1.SetRowHeight(i, 21);

                this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Value = dt.Rows[i]["AMT"].ToString();
                this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 7].Value = dt.Rows[i]["SEYUL"].ToString();
                this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Value = dt.Rows[i]["VAT"].ToString();
            }
        }
        #endregion

        #region Description : 스프레드 신고서2 - 가산세 명세 채우기
        private void UP_Spread_Dt5_Fill(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.FPS91_TY_S_AC_417AN968_Sheet1.SetRowHeight(i, 21);

                this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Value = dt.Rows[i]["AMT"].ToString();
                this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Value = dt.Rows[i]["SEYUL"].ToString();
                this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Value = dt.Rows[i]["VAT"].ToString();
            }
        }
        #endregion

        #region Description : 스프레드 신고서2 - 면세사업 수입금액 채우기
        private void UP_Spread_Dt6_Fill(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 2 && i != 3)
                {
                    this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 0].Value = dt.Rows[i]["UPTAE"].ToString();
                    this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 1].Value = dt.Rows[i]["JONGMOK"].ToString();
                    this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 2].Value = dt.Rows[i]["JONGCODE"].ToString();
                }

                this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 3].Value = dt.Rows[i]["AMT"].ToString();
            }
        }
        #endregion

        #region Description : 스프레드 내용 채우기
        private void UP_Spread_Busok_Fill()
        {
            string sTAXGUBN = string.Empty;

            string sTAXCDGN1 = string.Empty;
            string sTAXCDGN2 = string.Empty;
            string sTAXCDGN3 = string.Empty;

            string sWORKGUBN = string.Empty;

            string sSTDATE   = string.Empty;
            string sEDDATE   = string.Empty;
            string sCDAC     = string.Empty;

            string sMAECHULHAP_AMT   = string.Empty;
            string sMAECHULHAP_VAT   = string.Empty;

            string sMAEIPHAP_AMT     = string.Empty;
            string sMAEIPHAP_VAT     = string.Empty;

            string sMAEIPGONGHAP_AMT = string.Empty;
            string sMAEIPGONGHAP_VAT = string.Empty;

            string sMAEIPCHAHAP_AMT  = string.Empty;
            string sMAEIPCHAHAP_VAT  = string.Empty;

            string sHWANGUBHAP_VAT   = string.Empty;

            string sKYUNGGAMHAP_AMT  = string.Empty;
            string sKYUNGGAMHAP_VAT  = string.Empty;

            double dMAECHULHAP_AMT   = 0;
            double dMAECHULHAP_VAT   = 0;

            double dMAEIPHAP_AMT     = 0;
            double dMAEIPHAP_VAT     = 0;

            double dMAEIPGONGHAP_AMT = 0;
            double dMAEIPGONGHAP_VAT = 0;

            double dMAEIPCHAHAP_AMT  = 0;
            double dMAEIPCHAHAP_VAT  = 0;

            double dHWANGUBHAP_VAT   = 0;

            double dKYUNGGAMHAP_AMT  = 0;
            double dKYUNGGAMHAP_VAT  = 0;

            int i = 0;

            DataTable dt = new DataTable();

            // 신고내용
            for (i = 0; i < this.FPS91_TY_S_AC_3CV3F913_Sheet1.RowCount; i++)
            {
                this.FPS91_TY_S_AC_3CV3F913_Sheet1.SetRowHeight(i, 22);
 
                sWORKGUBN = "";

                this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = "0";
                this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = "0";

                switch (i)
                {
                    // 과세표준 및 매출세액
                    case 0:

                        sTAXGUBN  = "2";
                        sTAXCDGN1 = "11,19,61,68,69";
                        sTAXCDGN2 = "11,19,61,68,69";
                        sTAXCDGN3 = "11,19,61,68,69";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_41230923",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            sTAXGUBN.ToString(),
                            sTAXCDGN1.ToString(),
                            sTAXCDGN2.ToString(),
                            sTAXCDGN3.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = UP_DotDelete(Get_Numeric(dt.Rows[0]["VAT1"].ToString()));

                            dMAECHULHAP_AMT = double.Parse(string.Format("{0:####}", Get_Numeric(dt.Rows[0]["AMT"].ToString())));
                            dMAECHULHAP_VAT = double.Parse(string.Format("{0:####}", UP_DotDelete(Get_Numeric(dt.Rows[0]["VAT1"].ToString()))));
                        }

                        break;

                    case 3:

                        sSTDATE = "";
                        sEDDATE = "";

                        sTAXCDGN1 = "20";

                        if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1) == "1") // 1기
                        {
                            if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2) == "1") // 예정
                            {
                                sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "01";
                                sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "03";
                            }
                            else
                            {
                                sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "04";
                                sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "06";
                            }
                        }
                        else // 2기
                        {
                            if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2) == "1") // 예정
                            {
                                sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "07";
                                sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "09";
                            }
                            else
                            {
                                sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "10";
                                sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "12";
                            }
                        }

                        if (this.CBO01_VSBRANCH.GetValue().ToString() == "1")
                        {
                            sCDAC = "21103101";
                        }
                        else
                        {
                            sCDAC = "21103102";
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_42L41482",
                            sSTDATE.ToString(),
                            sEDDATE.ToString(),
                            sCDAC.ToString(),
                            sTAXCDGN1.ToString(),
                            this.TXT01_VSYEAR.GetValue().ToString(),                 // 년도
                            this.CBO01_VSBRANCH.GetValue().ToString(),               // 본점, 지점
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            "2",                                                     // 매입, 매출
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            sTAXCDGN1.ToString()                                     // 세무코드
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = UP_DotDelete(Get_Numeric(dt.Rows[0]["VAT1"].ToString()));

                            sMAECHULHAP_AMT = string.Format("{0:####}", dMAECHULHAP_AMT + double.Parse(Get_Numeric(dt.Rows[0]["AMT"].ToString())));
                            sMAECHULHAP_VAT = string.Format("{0:####}", dMAECHULHAP_VAT + double.Parse(UP_DotDelete(Get_Numeric(dt.Rows[0]["VAT1"].ToString()))));

                            dMAECHULHAP_AMT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAECHULHAP_AMT)));
                            dMAECHULHAP_VAT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAECHULHAP_VAT)));
                        }

                        break;

                    case 4:

                        sTAXCDGN1 = "12,62";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_41248927",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            sTAXCDGN1.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = Get_Numeric(dt.Rows[0]["VAT"].ToString());

                            sMAECHULHAP_AMT = string.Format("{0:####}", dMAECHULHAP_AMT + double.Parse(Get_Numeric(dt.Rows[0]["AMT"].ToString())));
                            sMAECHULHAP_VAT = string.Format("{0:####}", dMAECHULHAP_VAT + double.Parse(Get_Numeric(dt.Rows[0]["VAT"].ToString())));

                            dMAECHULHAP_AMT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAECHULHAP_AMT)));
                            dMAECHULHAP_VAT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAECHULHAP_VAT)));
                        }

                        break;

                    case 5:

                        sTAXCDGN1 = "13";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_4124G928",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            sTAXCDGN1.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = Get_Numeric(dt.Rows[0]["VAT"].ToString());

                            sMAECHULHAP_AMT = string.Format("{0:####}", dMAECHULHAP_AMT + double.Parse(Get_Numeric(dt.Rows[0]["AMT"].ToString())));
                            sMAECHULHAP_VAT = string.Format("{0:####}", dMAECHULHAP_VAT + double.Parse(Get_Numeric(dt.Rows[0]["VAT"].ToString())));

                            dMAECHULHAP_AMT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAECHULHAP_AMT)));
                            dMAECHULHAP_VAT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAECHULHAP_VAT)));
                        }

                        break;

                    // 예전신고누락분
                    case 6:

                        sTAXCDGN1 = "11,19,61,68,69,20,12,62,13";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_418AE046",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            sTAXCDGN1.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = Get_Numeric(dt.Rows[0]["VAT"].ToString());

                            sMAECHULHAP_AMT = string.Format("{0:####}", dMAECHULHAP_AMT + double.Parse(Get_Numeric(dt.Rows[0]["AMT"].ToString())));
                            sMAECHULHAP_VAT = string.Format("{0:####}", dMAECHULHAP_VAT + double.Parse(Get_Numeric(dt.Rows[0]["VAT"].ToString())));

                            dMAECHULHAP_AMT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAECHULHAP_AMT)));
                            dMAECHULHAP_VAT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAECHULHAP_VAT)));
                        }

                        break;

                    // 합계
                    case 8:

                        this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = string.Format("{0:####}", dMAECHULHAP_AMT);
                        this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = string.Format("{0:####}", dMAECHULHAP_VAT);

                        break;

                    // 매입세액
                    case 9:

                        sMAEIPHAP_AMT = "";
                        sMAEIPHAP_VAT = "";

                        dMAEIPHAP_AMT = 0;
                        dMAEIPHAP_VAT = 0;
                        
                        sTAXGUBN  = "1";
                        sTAXCDGN1 = "51,52,54,55,71,72,74,75";
                        sTAXCDGN2 = "51,52,54,55,71,72,74,75";
                        sTAXCDGN3 = "51,71,54,55,74,75";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_41230923",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            sTAXGUBN.ToString(),
                            sTAXCDGN1.ToString(),
                            sTAXCDGN2.ToString(),
                            sTAXCDGN3.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = Get_Numeric(dt.Rows[0]["VAT"].ToString());

                            dMAEIPHAP_AMT = double.Parse(string.Format("{0:####}", Get_Numeric(dt.Rows[0]["AMT"].ToString())));
                            dMAEIPHAP_VAT = double.Parse(string.Format("{0:####}", Get_Numeric(dt.Rows[0]["VAT"].ToString())));
                        }

                        break;

                    // 고정자산 매입(건물등감가상각자산취득명세)
                    case 10:

                        sTAXCDGN1 = "51,71,54,55,74,75";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_4189V043",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            sTAXCDGN1.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = Get_Numeric(dt.Rows[0]["VAT"].ToString());

                            sMAEIPHAP_AMT = string.Format("{0:####}", dMAEIPHAP_AMT + double.Parse(Get_Numeric(dt.Rows[0]["AMT"].ToString())));
                            sMAEIPHAP_VAT = string.Format("{0:####}", dMAEIPHAP_VAT + double.Parse(Get_Numeric(dt.Rows[0]["VAT"].ToString())));

                            dMAEIPHAP_AMT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAEIPHAP_AMT)));
                            dMAEIPHAP_VAT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAEIPHAP_VAT)));
                        }

                        break;
                    
                    // 예정신고누락분
                    case 11:

                        sTAXCDGN1 = "51,52,54,55,71,72,74,75";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_418AE046",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            sTAXCDGN1.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = Get_Numeric(dt.Rows[0]["VAT"].ToString());

                            sMAEIPHAP_AMT = string.Format("{0:####}", dMAEIPHAP_AMT + double.Parse(Get_Numeric(dt.Rows[0]["AMT"].ToString())));
                            sMAEIPHAP_VAT = string.Format("{0:####}", dMAEIPHAP_VAT + double.Parse(Get_Numeric(dt.Rows[0]["VAT"].ToString())));

                            dMAEIPHAP_AMT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAEIPHAP_AMT)));
                            dMAEIPHAP_VAT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAEIPHAP_VAT)));
                        }

                        break;

                    // 기타공제매입세액(신용카드매출전표등 수취명세서(57,58))
                    case 13:

                        sTAXCDGN1 = "57,58";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_418BJ050",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            sTAXCDGN1.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = Get_Numeric(dt.Rows[0]["VAT"].ToString());

                            sMAEIPHAP_AMT = string.Format("{0:####}", dMAEIPHAP_AMT + double.Parse(Get_Numeric(dt.Rows[0]["AMT"].ToString())));
                            sMAEIPHAP_VAT = string.Format("{0:####}", dMAEIPHAP_VAT + double.Parse(Get_Numeric(dt.Rows[0]["VAT"].ToString())));

                            dMAEIPHAP_AMT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAEIPHAP_AMT)));
                            dMAEIPHAP_VAT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAEIPHAP_VAT)));
                        }

                        break;

                    // 합계((10)+(11)+(12)+(13)+(14))
                    case 14:

                        this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = string.Format("{0:####}", dMAEIPHAP_AMT);
                        this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = string.Format("{0:####}", dMAEIPHAP_VAT);

                        break;

                    // 공제받지못할 매입세액
                    case 15:

                        sTAXCDGN1 = "54,55,74,75";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_418BV051",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            sTAXCDGN1.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = Get_Numeric(dt.Rows[0]["VAT"].ToString());

                            dMAEIPGONGHAP_AMT = double.Parse(string.Format("{0:####}", Get_Numeric(dt.Rows[0]["AMT"].ToString())));
                            dMAEIPGONGHAP_VAT = double.Parse(string.Format("{0:####}", Get_Numeric(dt.Rows[0]["VAT"].ToString())));
                        }

                        break;

                    // 차감계((15)-(16))
                    case 16:

                        sMAEIPCHAHAP_AMT = string.Format("{0:####}", dMAEIPHAP_AMT - dMAEIPGONGHAP_AMT);
                        sMAEIPCHAHAP_VAT = string.Format("{0:####}", dMAEIPHAP_AMT - dMAEIPGONGHAP_VAT);

                        dMAEIPCHAHAP_AMT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAEIPCHAHAP_AMT)));
                        dMAEIPCHAHAP_VAT = double.Parse(string.Format("{0:####}", Get_Numeric(sMAEIPCHAHAP_VAT)));

                        this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = string.Format("{0:####}", dMAEIPCHAHAP_AMT);
                        this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = string.Format("{0:####}", dMAEIPCHAHAP_VAT);

                        break;

                    // 납부(환급)세액(매출세액(가)-매입세액(나))
                    case 17:

                        sHWANGUBHAP_VAT = string.Format("{0:####}", dMAECHULHAP_VAT - dMAEIPCHAHAP_VAT);

                        dHWANGUBHAP_VAT = double.Parse(string.Format("{0:####}", Get_Numeric(sHWANGUBHAP_VAT)));
                        
                        this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = string.Format("{0:####}", dHWANGUBHAP_VAT);

                        break;

                    // 기타 경감.공제세액
                    case 18:

                        // 예정 = 0, 확정 = 10000
                        if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2) == "1")
                        {
                            dKYUNGGAMHAP_AMT = 0;
                            dKYUNGGAMHAP_VAT = 0;
                        }
                        else
                        {
                            dKYUNGGAMHAP_AMT = 0;
                            dKYUNGGAMHAP_VAT = 10000;
                        }

                        this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = string.Format("{0:####}", Convert.ToString(dKYUNGGAMHAP_AMT));
                        this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = string.Format("{0:####}", Convert.ToString(dKYUNGGAMHAP_VAT));

                        break;

                    // 합계
                    case 20:

                        this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = string.Format("{0:####}", Convert.ToString(dKYUNGGAMHAP_AMT));
                        this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = string.Format("{0:####}", Convert.ToString(dKYUNGGAMHAP_AMT));

                        break;

                    //매입자 납부특례 기납부세액(2016.12.21 임경화 추가) 
                    case 24:

                        sTAXGUBN = "2";
                        sTAXCDGN1 = "68";
                        sTAXCDGN2 = "68";
                        sTAXCDGN3 = "68";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_41230923",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            sTAXGUBN.ToString(),
                            sTAXCDGN1.ToString(),
                            sTAXCDGN2.ToString(),
                            sTAXCDGN3.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Value = UP_DotDelete(Get_Numeric(dt.Rows[0]["VAT1"].ToString()));

                            dMAECHULHAP_AMT = double.Parse(string.Format("{0:####}", Get_Numeric(dt.Rows[0]["AMT"].ToString())));
                            dMAECHULHAP_VAT = double.Parse(string.Format("{0:####}", UP_DotDelete(Get_Numeric(dt.Rows[0]["VAT1"].ToString()))));
                        }

                        break;
                }
            }

            // 과세표준명세
            for (i = 0; i < this.FPS91_TY_S_AC_4121M919_Sheet1.RowCount; i++)
            {
                switch (i)
                {
                    case 0:

                        // 제출자 정보
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_4185G054",
                            this.CBO01_VSBRANCH.GetValue().ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 0].Value = dt.Rows[0]["ASMUPTAE"].ToString();
                            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 1].Value = dt.Rows[0]["ASMEVENT"].ToString();
                            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 2].Value = dt.Rows[0]["ASMBUSTYPE"].ToString();
                        }

                        // 금액
                        sSTDATE = "";
                        sEDDATE = "";

                        sTAXCDGN1 = "11,12,13,61,62";

                        if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1) == "1") // 1기
                        {
                            if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2) == "1") // 예정
                            {
                                sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "01";
                                sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "03";
                            }
                            else
                            {
                                sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "04";
                                sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "06";
                            }
                        }
                        else // 2기
                        {
                            if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2) == "1") // 예정
                            {
                                sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "07";
                                sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "09";
                            }
                            else
                            {
                                sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "10";
                                sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "12";
                            }
                        }

                        if (this.CBO01_VSBRANCH.GetValue().ToString() == "1")
                        {
                            sCDAC = "21103101";
                        }
                        else
                        {
                            sCDAC = "21103102";
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_4125G929",
                            sSTDATE.ToString(),
                            sEDDATE.ToString(),
                            sCDAC.ToString(),
                            sTAXCDGN1.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 3].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                        }

                        break;

                    case 3:

                        sSTDATE = "";
                        sEDDATE = "";

                        //sTAXCDGN1 = "19,20,69";

                        sTAXCDGN1 = "19,20,68,69";

                        if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1) == "1") // 1기
                        {
                            if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2) == "1") // 예정
                            {
                                sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "01";
                                sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "03";
                            }
                            else
                            {
                                sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "04";
                                sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "06";
                            }
                        }
                        else // 2기
                        {
                            if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2) == "1") // 예정
                            {
                                sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "07";
                                sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "09";
                            }
                            else
                            {
                                sSTDATE = this.TXT01_VSYEAR.GetValue().ToString() + "10";
                                sEDDATE = this.TXT01_VSYEAR.GetValue().ToString() + "12";
                            }
                        }

                        if (this.CBO01_VSBRANCH.GetValue().ToString() == "1")
                        {
                            sCDAC = "21103101";
                        }
                        else
                        {
                            sCDAC = "21103102";
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_4125G929",
                            sSTDATE.ToString(),
                            sEDDATE.ToString(),
                            sCDAC.ToString(),
                            sTAXCDGN1.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 3].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                        }

                        break;
                }
            }

            // 신고서2 - 예정신고누락분 명세 ~ 기타경감.공제세액 명세
            for (i = 0; i < this.FPS91_TY_S_AC_4179D965_Sheet1.RowCount; i++)
            {
                this.FPS91_TY_S_AC_4179D965_Sheet1.SetRowHeight(i, 21);

                sTAXCDGN1 = "";

                switch (i)
                {
                    // (7) 매출
                    case 0:
                        
                        sTAXCDGN1 = "11,19,61,68,69";
                        UP_SET_AVMISSMF(i, sTAXCDGN1);

                        break;

                    case 1:

                        sTAXCDGN1 = "20";
                        UP_SET_AVMISSMF(i, sTAXCDGN1);

                        break;

                    case 2:

                        sTAXCDGN1 = "12,62";
                        UP_SET_AVMISSMF(i, sTAXCDGN1);

                        break;

                    case 3:

                        sTAXCDGN1 = "13";
                        UP_SET_AVMISSMF(i, sTAXCDGN1);

                        break;
                    // (12) 매입
                    case 5:

                        sTAXCDGN1 = "51,52,54,55,71,72,74,75";
                        UP_SET_AVMISSMF(i, sTAXCDGN1);

                        break;

                    case 6:

                        this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Value = "0";
                        this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Value = "0";

                        //sTAXCDGN1 = "58";
                        //UP_SET_AVMISSMF(i, sTAXCDGN1);

                        break;

                    // (14) 기타공제 매입세액 명세

                    case 8: // 신용카드매출전표등 수렴명세서 제출분(일반매입)

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_41EBC084",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            "1"
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Value = Get_Numeric(dt.Rows[0]["VAT"].ToString());
                        }

                        break;

                    case 9: // 신용카드매출전표등 수렴명세서 제출분(고정자산 매입)

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_41EBC084",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            "2"
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Value = Get_Numeric(dt.Rows[0]["VAT"].ToString());
                        }

                        break;

                    // 공제받지 못할매입 명세
                    case 17:

                        sTAXCDGN1 = "54,55,74,75";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_41EBG085",
                            this.TXT01_VSYEAR.GetValue().ToString(),
                            this.CBO01_VSBRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                            getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                            sTAXCDGN1.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());
                            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Value = Get_Numeric(dt.Rows[0]["VAT"].ToString());
                        }

                        break;

                    // (18) 기타 경감. 공제세액 명세
                    case 21:  // 전자신고 세액 공제

                        if (getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2) == "1")
                        {
                            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Value = "0";
                        }
                        else
                        {
                            this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Value = "10000";
                        }

                        break;
                }
            }

            string sAMT1       = string.Empty;
            string sAMITAXAMT  = string.Empty;
            string sAMT2       = string.Empty;
            string sAMIDELADD  = string.Empty;
            string sAMIPENADD  = string.Empty;
            string sAMIPENADD1 = string.Empty;
            string sAMIPAYADD  = string.Empty;


            // 가산세 명세 - 예정신고누락분(지연제출, 신고불성실, 과세표준신고 불성실 값 가져오기)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_41E1B086",
                this.TXT01_VSYEAR.GetValue().ToString(),
                this.CBO01_VSBRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                "2"
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sAMT1       = dt.Rows[0]["AMT1"].ToString();
                sAMITAXAMT  = dt.Rows[0]["AMITAXAMT"].ToString();
                sAMT2       = dt.Rows[0]["AMT2"].ToString();
                sAMIDELADD  = dt.Rows[0]["AMIDELADD"].ToString();
                sAMIPENADD  = dt.Rows[0]["AMIPENADD"].ToString();
                sAMIPENADD1 = dt.Rows[0]["AMIPENADD1"].ToString();
                sAMIPAYADD  = dt.Rows[0]["AMIPAYADD"].ToString();
            }

            string sSEYUL1 = string.Empty;
            string sSEYUL2 = string.Empty;
            string sSEYUL3 = string.Empty;
            string sSEYUL4 = string.Empty;

            // 가산세 명세 - 옵션에서 세율 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_41A64077",
                this.TXT01_VSYEAR.GetValue().ToString(),
                this.CBO01_VSBRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2) // 예정, 확정
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSEYUL1 = dt.Rows[0]["SEYUL1"].ToString();
                sSEYUL2 = dt.Rows[0]["SEYUL2"].ToString();
                sSEYUL3 = dt.Rows[0]["SEYUL3"].ToString();
                sSEYUL4 = dt.Rows[0]["SEYUL4"].ToString();
            }

            // 신고서2 - 가산세 명세
            for (i = 0; i < this.FPS91_TY_S_AC_417AN968_Sheet1.RowCount; i++)
            {
                this.FPS91_TY_S_AC_417AN968_Sheet1.SetRowHeight(i, 21);

                switch (i)
                {
                    case 7:

                        // 지연제출
                        if (Get_Numeric(sAMIDELADD.ToString()) != "0")
                        {
                            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Value = sAMT1.ToString(); // 공급가액
                        }
                        this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Value = sSEYUL1.ToString();
                        this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Value = sAMIDELADD.ToString();

                        break;

                    case 10:

                        // 과소.초과환급신고(일반)
                        if (Get_Numeric(sAMIPENADD.ToString()) != "0")
                        {
                            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Value = sAMITAXAMT.ToString(); // 세액
                        }
                        this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Value = sSEYUL2.ToString();
                        this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Value = sAMIPENADD.ToString();

                        break;

                    case 12:

                        // 납부불성실
                        if (Get_Numeric(sAMIPAYADD.ToString()) != "0")
                        {
                            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Value = sAMITAXAMT.ToString(); // 세액
                        }
                        this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Value = sSEYUL3.ToString();
                        this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Value = sAMIPAYADD.ToString();

                        break;

                    case 13:

                        // 영세율 과세표준신고 불성실
                        if (Get_Numeric(sAMIPENADD1.ToString()) != "0")
                        {
                            this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Value = sAMT2.ToString(); // 공급가액
                        }
                        this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Value = sSEYUL4.ToString();
                        this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Value = sAMIPENADD1.ToString();

                        break;
                }
            }

            




            //if (this.CBO01_VSBRANCH.GetValue().ToString() == "2")
            //{
                double dAMT = 0;

                // 면세사업 수입금액
                for (i = 0; i < this.FPS91_TY_S_AC_41713969_Sheet1.RowCount; i++)
                {
                    switch (i)
                    {
                        case 0:

                            // 금액
                            sTAXCDGN1 = "22,62";

                            // 계산서를 읽어옴
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_AC_41E1K087",
                                this.TXT01_VSYEAR.GetValue().ToString(),
                                this.CBO01_VSBRANCH.GetValue().ToString(),
                                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                                sTAXCDGN1.ToString()
                                );

                            dt = this.DbConnector.ExecuteDataTable();

                            if (dt.Rows.Count > 0)
                            {
                                this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 3].Value = Get_Numeric(dt.Rows[0]["AMT"].ToString());

                                dAMT = double.Parse(Get_Numeric(dt.Rows[0]["AMT"].ToString()));
                            }

                            if (dAMT != 0)
                            {
                                // 제출자 정보
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_AC_4185G054",
                                    this.CBO01_VSBRANCH.GetValue().ToString()
                                    );

                                dt = this.DbConnector.ExecuteDataTable();

                                if (dt.Rows.Count > 0)
                                {
                                    this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 0].Value = dt.Rows[0]["ASMUPTAE"].ToString();
                                    this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 1].Value = dt.Rows[0]["ASMEVENT"].ToString();
                                    this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 2].Value = dt.Rows[0]["ASMBUSTYPE"].ToString();
                                }
                            }

                            break;
                    }
                }
            //}

            // 계산서 교부금액
            sTAXCDGN1 = "22,62";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_41E1K087",
                this.TXT01_VSYEAR.GetValue().ToString(),
                this.CBO01_VSBRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                sTAXCDGN1.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 계산서 교부금액
                this.TXT01_VS26IS80AMT.SetValue(Get_Numeric(dt.Rows[0]["AMT"].ToString()));
            }



            // 계산서 수취금액
            sTAXCDGN1 = "59,79";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_41E1K087",
                this.TXT01_VSYEAR.GetValue().ToString(),
                this.CBO01_VSBRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                sTAXCDGN1.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 계산서 수취금액
                this.TXT01_VS26IS81AMT.SetValue(Get_Numeric(dt.Rows[0]["AMT"].ToString()));
            }
        }
        #endregion

        #region Description : 신고서2 예정신고 누락분 가져오기
        private void UP_SET_AVMISSMF(int i, string sAMITAXGUBN)
        {
            DataTable AV_dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_418AE046",
                this.TXT01_VSYEAR.GetValue().ToString(),
                this.CBO01_VSBRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2), // 예정, 확정
                sAMITAXGUBN.ToString()
                );

            AV_dt = this.DbConnector.ExecuteDataTable();

            if (AV_dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Value = Get_Numeric(AV_dt.Rows[0]["AMT"].ToString());
                this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Value = Get_Numeric(AV_dt.Rows[0]["VAT"].ToString());
            }
        }
        #endregion

        #region Description : 특정 Row와 Column 값 변경
        private void UP_Compute()
        {
            // 신고내용
            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_3CV3F913, "TITLE1", "합 계", Color.Yellow);

            // 과세표준 및 매출금액 - 합계
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.SetFormula(
                8,
                4,
                "R[-8]C[0] + R[-7]C[0] + R[-6]C[0] + R[-5]C[0] + R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            // 과세표준 및 매출세액 - 합계
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.SetFormula(
                8,
                6,
                "R[-8]C[0] + R[-7]C[0] + R[-6]C[0] + R[-5]C[0] + R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            // 매입세액 금액 합계
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.SetFormula(
                14,
                4,
                "R[-5]C[0] + R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            // 매입세액 세액 합계
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.SetFormula(
                14,
                6,
                "R[-5]C[0] + R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");




            // 차감계 금액 합계
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.SetFormula(
                16,
                4,
                "R[-2]C[0] - R[-1]C[0]");

            // 차감계 세액 합계
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.SetFormula(
                16,
                6,
                "R[-2]C[0] - R[-1]C[0]");




            // 납부(환급)세액(매출세액(가)-매입세액(나))
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.SetFormula(
                17,
                6,
                "R[-9]C[0] - R[-1]C[0]");

            // 경감. 공제 세액
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.SetFormula(
                20,
                6,
                "R[-2]C[0] + R[-1]C[0]");

            // 차가감하여 납부할 세액(환급받을 세액)
            // 수정(2014.04.22)
            //this.FPS91_TY_S_AC_3CV3F913_Sheet1.SetFormula(
            //    25,
            //    6,
            //    "R[-8]C[0] - R[-5]C[0] - R[-4]C[0] - R[-3]C[0] - R[-2]C[0] + R[-1]C[0]");
            this.FPS91_TY_S_AC_3CV3F913_Sheet1.SetFormula(
                26,
                6,
                "R[-9]C[0] - R[-6]C[0] - R[-5]C[0] - R[-4]C[0] - R[-3]C[0] - R[-2]C[0] + R[-1]C[0]");


            this.FPS91_TY_S_AC_3CV3F913.ActiveSheet.Rows[FPS91_TY_S_AC_3CV3F913.CurrentRowCount - 1].Visible = false;


            // 과세표준명세 - 합계
            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_4121M919, "UPTAE", "합 계", Color.Yellow);

            this.FPS91_TY_S_AC_4121M919_Sheet1.SetFormula(
                4,
                3,
                "R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            this.FPS91_TY_S_AC_4121M919.ActiveSheet.Rows[FPS91_TY_S_AC_4121M919.CurrentRowCount - 1].Visible = false;






            // 신고서2
            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_4179D965, "TITLE1", "합 계", Color.Yellow);
            
            // 예정신고누락분 명세
            // 매출 - 금액 합계
            this.FPS91_TY_S_AC_4179D965_Sheet1.SetFormula(
                4,
                6,
                "R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            // 매출 - 세액 합계
            this.FPS91_TY_S_AC_4179D965_Sheet1.SetFormula(
                4,
                8,
                "R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            // 매입 - 금액 합계
            this.FPS91_TY_S_AC_4179D965_Sheet1.SetFormula(
                7,
                6,
                "R[-2]C[0] + R[-1]C[0]");

            // 매입 - 세액 합계
            this.FPS91_TY_S_AC_4179D965_Sheet1.SetFormula(
                7,
                8,
                "R[-2]C[0] + R[-1]C[0]");

            // 기타공제매입세액 - 금액 합계
            this.FPS91_TY_S_AC_4179D965_Sheet1.SetFormula(
                16,
                6,
                "R[-8]C[0] + R[-7]C[0] + R[-6]C[0] + R[-5]C[0] + R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            // 기타공제매입세액 - 세액 합계
            this.FPS91_TY_S_AC_4179D965_Sheet1.SetFormula(
                16,
                8,
                "R[-8]C[0] + R[-7]C[0] + R[-6]C[0] + R[-5]C[0] + R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            // 공제받지 못할매입 - 금액 합계
            this.FPS91_TY_S_AC_4179D965_Sheet1.SetFormula(
                20,
                6,
                "R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            // 공제받지 못할매입 - 세액 합계
            this.FPS91_TY_S_AC_4179D965_Sheet1.SetFormula(
                20,
                8,
                "R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            // 기타경감 공제세액 - 금액 합계
            this.FPS91_TY_S_AC_4179D965_Sheet1.SetFormula(
                26,
                6,
                "R[-5]C[0] + R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            // 기타경감 공제세액 - 세액 합계
            this.FPS91_TY_S_AC_4179D965_Sheet1.SetFormula(
                26,
                8,
                "R[-5]C[0] + R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            this.FPS91_TY_S_AC_4179D965.ActiveSheet.Rows[FPS91_TY_S_AC_4179D965.CurrentRowCount - 1].Visible = false;







            // 가산세 명세
            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_417AN968, "TITLE1", "합 계", Color.Yellow);

            // 금액 합계
            this.FPS91_TY_S_AC_417AN968_Sheet1.SetFormula(
                18,
                4,
                "R[-18]C[0] + R[-17]C[0] + R[-16]C[0] + R[-15]C[0] + R[-14]C[0] + R[-13]C[0] + R[-12]C[0] + R[-11]C[0] + R[-10]C[0] + R[-9]C[0] + R[-8]C[0] + R[-7]C[0] + R[-6]C[0] + R[-5]C[0] + R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            // 세액 합계
            this.FPS91_TY_S_AC_417AN968_Sheet1.SetFormula(
                18,
                6,
                "R[-18]C[0] + R[-17]C[0] + R[-16]C[0] + R[-15]C[0] + R[-14]C[0] + R[-13]C[0] + R[-12]C[0] + R[-11]C[0] + R[-10]C[0] + R[-9]C[0] + R[-8]C[0] + R[-7]C[0] + R[-6]C[0] + R[-5]C[0] + R[-4]C[0] + R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            this.FPS91_TY_S_AC_417AN968.ActiveSheet.Rows[FPS91_TY_S_AC_417AN968.CurrentRowCount - 1].Visible = false;

            // 면세사업수입금액 - 합계
            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_41713969, "UPTAE", "합 계", Color.Yellow);

            this.FPS91_TY_S_AC_41713969_Sheet1.SetFormula(
                3,
                3,
                "R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            this.FPS91_TY_S_AC_41713969.ActiveSheet.Rows[FPS91_TY_S_AC_41713969.CurrentRowCount - 1].Visible = false;
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B8L317",
                this.TXT01_VSYEAR.GetValue().ToString(),
                this.CBO01_VSBRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)  // 예정, 확정
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_42B8N318");
                e.Successed = false;
                return;
            }

            if (this.CBH01_VSREBANKCD.GetValue().ToString() != "")
            {
                if (this.TXT01_VSREACCCODE.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_2455B473");
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (this.TXT01_VSREACCCODE.GetValue().ToString() != "")
                {
                    this.ShowMessage("TY_M_AC_2445M440");
                    e.Successed = false;
                    return;
                }
            }

            DataTable dt = new DataTable();

            if (this.TXT01_VSREACCCODE.GetValue().ToString() != "")
            {
                // 계좌번호 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_4195J063",
                    this.TXT01_VSREACCCODE.GetValue().ToString().Replace("-","")
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_AC_4163H945");
                    e.Successed = false;
                    return;
                }
            }

            double dAMT = 0;
            double dVAT = 0;

            // 세액 = (공급가 * 0.1) +-1000까지 허용함

            // 과세 세금계산서 발급분 세액체크
            dAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0, 4].Text.ToString()));
            dVAT = double.Parse(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[0, 6].Text.ToString()));

            if (dAMT != 0)
            {
                if (((dAMT * 0.1) + 1000) < dVAT)
                {
                    this.ShowMessage("TY_M_AC_41A5H076");
                    e.Successed = false;
                    return;
                }

                if (((dAMT * 0.1) - 1000) > dVAT)
                {
                    this.ShowMessage("TY_M_AC_41A5H076");
                    e.Successed = false;
                    return;
                }
            }

            // 영세율 세금계산서 발급분 세액체크
            dAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[3, 4].Text.ToString()));
            dVAT = double.Parse(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[3, 6].Text.ToString()));

            if (dAMT != 0)
            {
                if (((dAMT * 0.1) + 1000) < dVAT)
                {
                    this.ShowMessage("TY_M_AC_41A5H076");
                    e.Successed = false;
                    return;
                }

                if (((dAMT * 0.1) - 1000) > dVAT)
                {
                    this.ShowMessage("TY_M_AC_41A5H076");
                    e.Successed = false;
                    return;
                }
            }

            if (Get_Numeric(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[4, 3].Text.ToString()) !=
                Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[8, 4].Text.ToString()))
            {
                this.ShowMessage("TY_M_AC_41E1P088");
                e.Successed = false;
                return;
            }

            // 필드변수에 저장
            UP_Set_Field();


            // 저장버튼 처리시에만 체크함 -- 환급관련 부분 )
            if (fsPOPUP.ToString() == "")
            {
                // 환급금액이 존재할경우 환급구분 확인
                if (Convert.ToDouble(this.DAT02_VSTOTTAXAMT.GetValue().ToString().Trim()) < 0 && this.CBO01_VSREFUNDGB.GetValue().ToString() == "")
                {
                    SetFocus(this.CBO01_VSREFUNDGB);
                    this.ShowMessage("TY_M_AC_43JAV792");
                    e.Successed = false;
                    return;
                }
                // 환급없음이 아닐때 환급은행 및 계좌번호 체크
                if (this.CBO01_VSREFUNDGB.GetValue().ToString() != "")
                {
                    if (this.CBH01_VSREBANKCD.GetValue().ToString() == "") // 은행코드 
                    {
                        SetFocus(this.CBH01_VSREBANKCD);
                        this.ShowMessage("TY_M_AC_2445M440");
                        e.Successed = false;
                        return;
                    }

                    if (this.TXT01_VSREACCCODE.GetValue().ToString() == "") // 계좌번호 
                    {
                        SetFocus(this.TXT01_VSREACCCODE);
                        this.ShowMessage("TY_M_AC_2455B473");
                        e.Successed = false;
                        return;
                    }
                }
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

        }
        #endregion

        #region Description : 필드변수에 저장
        private void UP_Set_Field()
        {
            int i = 0;

            this.DAT02_VSYEAR.SetValue(this.TXT01_VSYEAR.GetValue().ToString());
            this.DAT02_VSBRANCH.SetValue(this.CBO01_VSBRANCH.GetValue().ToString());
            if (this.CBO01_VSBRANCH.GetValue().ToString() == "1")
            {
                this.DAT02_VSVENDCD.SetValue("102885");
            }
            else
            {
                this.DAT02_VSVENDCD.SetValue("349876");
            }
            this.DAT02_VSRPTGUBN.SetValue(getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1));
            this.DAT02_VSCONFGB.SetValue(getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2));

            #region Description : 신고 내용1
            for (i = 0; i < this.FPS91_TY_S_AC_3CV3F913_Sheet1.RowCount; i++)
            {
                switch (i)
                {
                    case 0:

                        this.DAT02_VS01ST01AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS01ST01NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST01DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST01TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 1:

                        this.DAT02_VS01ST02AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS01ST02NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST02DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST02TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 2:

                        this.DAT02_VS01ST03AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS01ST03NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST03DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST03TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 3:

                        this.DAT02_VS01ST04AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS01ST04NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST04DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST04TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 4:

                        this.DAT02_VS01ST05AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS01ST05NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST05DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST05TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 5:

                        this.DAT02_VS01ST06AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS01ST06NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST06DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST06TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 6:

                        this.DAT02_VS01ST07AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS01ST07NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST07DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST07TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 7:

                        this.DAT02_VS01ST08AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS01ST08NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST08DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS01ST08TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 8:

                        this.DAT02_VS01ST09AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS01ST09NEM.SetValue("0");
                        this.DAT02_VS01ST09DEN.SetValue("0");
                        this.DAT02_VS01ST09TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 9:

                        this.DAT02_VS02PU10AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS02PU10NEM.SetValue("0");
                        this.DAT02_VS02PU10DEN.SetValue("0");
                        this.DAT02_VS02PU10TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 10:

                        this.DAT02_VS02PU11AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS02PU11NEM.SetValue("0");
                        this.DAT02_VS02PU11DEN.SetValue("0");
                        this.DAT02_VS02PU11TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 11:

                        this.DAT02_VS02PU12AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS02PU12NEM.SetValue("0");
                        this.DAT02_VS02PU12DEN.SetValue("0");
                        this.DAT02_VS02PU12TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 12:

                        this.DAT02_VS02PU13AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS02PU13NEM.SetValue("0");
                        this.DAT02_VS02PU13DEN.SetValue("0");
                        this.DAT02_VS02PU13TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 13:

                        this.DAT02_VS02PU14AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS02PU14NEM.SetValue("0");
                        this.DAT02_VS02PU14DEN.SetValue("0");
                        this.DAT02_VS02PU14TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 14:

                        this.DAT02_VS02PU15AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS02PU15NEM.SetValue("0");
                        this.DAT02_VS02PU15DEN.SetValue("0");
                        this.DAT02_VS02PU15TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 15:

                        this.DAT02_VS02PU16AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS02PU16NEM.SetValue("0");
                        this.DAT02_VS02PU16DEN.SetValue("0");
                        this.DAT02_VS02PU16TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 16:

                        this.DAT02_VS02PU17AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS02PU17NEM.SetValue("0");
                        this.DAT02_VS02PU17DEN.SetValue("0");
                        this.DAT02_VS02PU17TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 17:

                        this.DAT02_VSPAYMENEM.SetValue("0");
                        this.DAT02_VSPAYMEDEN.SetValue("0");
                        this.DAT02_VSPAYMETAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 18:

                        this.DAT02_VS03RE18AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS03RE18NEM.SetValue("0");
                        this.DAT02_VS03RE18DEN.SetValue("0");
                        this.DAT02_VS03RE18TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 19:

                        this.DAT02_VS03RE19AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS03RE19NEM.SetValue("0");
                        this.DAT02_VS03RE19DEN.SetValue("0");
                        this.DAT02_VS03RE19TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 20:

                        this.DAT02_VS03RE20AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS03RE20NEM.SetValue("0");
                        this.DAT02_VS03RE20DEN.SetValue("0");
                        this.DAT02_VS03RE20TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 21:

                        this.DAT02_VS04SC21AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS04SC21NEM.SetValue("0");
                        this.DAT02_VS04SC21DEN.SetValue("0");
                        this.DAT02_VS04SC21TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 22:

                        this.DAT02_VS04SC22AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS04SC22NEM.SetValue("0");
                        this.DAT02_VS04SC22DEN.SetValue("0");
                        this.DAT02_VS04SC22TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;


                    case 23: // 추가(2014.04.22) : 사업양수자의 대리납부 기납부세액 

                        this.DAT02_VS04YC23AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS04YC23NEM.SetValue("0");
                        this.DAT02_VS04YC23DEN.SetValue("0");
                        this.DAT02_VS04YC23TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 24:  // 타이틀 변경 : 금지금 매입자 납부특례 기납부세액(구) --> 매입자 납부특례 가납부세액(신)

                        this.DAT02_VS04SC23AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS04SC23NEM.SetValue("0");
                        this.DAT02_VS04SC23DEN.SetValue("0");
                        this.DAT02_VS04SC23TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 25:  // 가산세액

                        this.DAT02_VS04SC24AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS04SC24NEM.SetValue("0");
                        this.DAT02_VS04SC24DEN.SetValue("0");
                        this.DAT02_VS04SC24TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 26:

                        this.DAT02_VS04SC25TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 27: 

                        this.DAT02_VSTOTTAXAMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_3CV3F913_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                }
            }
            #endregion

            #region Description : 국세환급금계좌신고 및 폐업신고
            // 국세환급금계좌신고
            this.DAT02_VSREBANKCD.SetValue(this.CBH01_VSREBANKCD.GetValue().ToString());
            this.DAT02_VSREACCCODE.SetValue(this.TXT01_VSREACCCODE.GetValue().ToString().Replace("-", ""));
            // 폐업신고
            this.DAT02_VSSHUTDATE.SetValue(this.TXT01_VSSHUTDATE.GetValue().ToString());
            this.DAT02_VSSHUTSAYU.SetValue(this.TXT01_VSSHUTSAYU.GetValue().ToString());
            #endregion

            #region Description : 과세표준명세
            for (i = 0; i < this.FPS91_TY_S_AC_4121M919_Sheet1.RowCount; i++)
            {
                switch (i)
                {
                    case 0:

                        this.DAT02_VS05EV26BUS.SetValue(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 0].Text.ToString());
                        this.DAT02_VS05EV26NAM.SetValue(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 1].Text.ToString());
                        this.DAT02_VS05EV26COD.SetValue(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 2].Text.ToString());
                        this.DAT02_VS05EV26AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 3].Text.ToString()));

                        break;

                    case 1:

                        this.DAT02_VS05EV27BUS.SetValue(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 0].Text.ToString());
                        this.DAT02_VS05EV27NAM.SetValue(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 1].Text.ToString());
                        this.DAT02_VS05EV27COD.SetValue(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 2].Text.ToString());
                        this.DAT02_VS05EV27AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 3].Text.ToString()));

                        break;

                    case 2:

                        this.DAT02_VS05EV28BUS.SetValue(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 0].Text.ToString());
                        this.DAT02_VS05EV28NAM.SetValue(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 1].Text.ToString());
                        this.DAT02_VS05EV28COD.SetValue(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 2].Text.ToString());
                        this.DAT02_VS05EV28AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 3].Text.ToString()));

                        break;

                    case 3:

                        this.DAT02_VS05EV29BUS.SetValue("");
                        this.DAT02_VS05EV29NAM.SetValue(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 1].Text.ToString());
                        this.DAT02_VS05EV29COD.SetValue(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 2].Text.ToString());
                        this.DAT02_VS05EV29AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 3].Text.ToString()));

                        break;

                    case 4:

                        this.DAT02_VS05EV30AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4121M919_Sheet1.Cells[i, 3].Text.ToString()));

                        break;
                }
            }
            #endregion

            #region Description : 신고서2 저장 - (7~18)
            for (i = 0; i < FPS91_TY_S_AC_4179D965_Sheet1.RowCount; i++)
            {
                switch (i)
                {
                    // 예정신고 누락분 명세        
                    case 0:

                        this.DAT02_VS06MI31AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS06MI31NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 7].Text.ToString()));
                        this.DAT02_VS06MI31DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 7].Text.ToString()));
                        this.DAT02_VS06MI31TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 1:

                        this.DAT02_VS06MI32AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS06MI32NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 7].Text.ToString()));
                        this.DAT02_VS06MI32DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 7].Text.ToString()));
                        this.DAT02_VS06MI32TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 2:

                        this.DAT02_VS06MI33AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS06MI33NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 7].Text.ToString()));
                        this.DAT02_VS06MI33DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 7].Text.ToString()));
                        this.DAT02_VS06MI33TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 3:

                        this.DAT02_VS06MI34AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS06MI34NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 7].Text.ToString()));
                        this.DAT02_VS06MI34DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 7].Text.ToString()));
                        this.DAT02_VS06MI34TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 4:

                        this.DAT02_VS06MI35AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS06MI35NEM.SetValue("0");
                        this.DAT02_VS06MI35DEN.SetValue("0");
                        this.DAT02_VS06MI35TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 5:

                        this.DAT02_VS06MI36AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS06MI36NEM.SetValue("0");
                        this.DAT02_VS06MI36DEN.SetValue("0");
                        this.DAT02_VS06MI36TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 6:

                        this.DAT02_VS06MI37AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS06MI37NEM.SetValue("0");
                        this.DAT02_VS06MI37DEN.SetValue("0");
                        this.DAT02_VS06MI37TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 7:

                        this.DAT02_VS06MI38AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS06MI38NEM.SetValue("0");
                        this.DAT02_VS06MI38DEN.SetValue("0");
                        this.DAT02_VS06MI38TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    // 기타공제매입세액 명세
                    case 8:

                        this.DAT02_VS14ET39AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS14ET39NEM.SetValue("0");
                        this.DAT02_VS14ET39DEN.SetValue("0");
                        this.DAT02_VS14ET39TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 9:

                        this.DAT02_VS14ET40AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS14ET40NEM.SetValue("0");
                        this.DAT02_VS14ET40DEN.SetValue("0");
                        this.DAT02_VS14ET40TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 10:

                        this.DAT02_VS14ET41AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS14ET41NEM.SetValue("0");
                        this.DAT02_VS14ET41DEN.SetValue("0");
                        this.DAT02_VS14ET41TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 11:  // 재활용폐자원등매입

                        this.DAT02_VS14ET42AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS14ET42NEM.SetValue("0");
                        this.DAT02_VS14ET42DEN.SetValue("0");
                        this.DAT02_VS14ET42TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    //case 12: //고금의제매입세액 삭제(2014.02.22)

                    //    this.DAT02_VS14ET43AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                    //    this.DAT02_VS14ET43NEM.SetValue("0");
                    //    this.DAT02_VS14ET43DEN.SetValue("0");
                    //    this.DAT02_VS14ET43TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                    //    break;

                    case 12: // 과세사업전환매입

                        this.DAT02_VS14ET44AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS14ET44NEM.SetValue("0");
                        this.DAT02_VS14ET44DEN.SetValue("0");
                        this.DAT02_VS14ET44TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 13: // 재고매입세액

                        this.DAT02_VS14ET45AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS14ET45NEM.SetValue("0");
                        this.DAT02_VS14ET45DEN.SetValue("0");
                        this.DAT02_VS14ET45TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 14: // 변제대손세액

                        this.DAT02_VS14ET46AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS14ET46NEM.SetValue("0");
                        this.DAT02_VS14ET46DEN.SetValue("0");
                        this.DAT02_VS14ET46TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 15: // 외국인관광객

                        this.DAT02_VS14NT47AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS14NT47NEM.SetValue("0");
                        this.DAT02_VS14NT47DEN.SetValue("0");
                        this.DAT02_VS14NT47TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 16: // 기타공제 합계

                        this.DAT02_VS14ET47AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS14ET47NEM.SetValue("0");
                        this.DAT02_VS14ET47DEN.SetValue("0");
                        this.DAT02_VS14ET47TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    // 공제받지 못할 매입 명세
                    case 17:

                        this.DAT02_VS16DE48AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS16DE48NEM.SetValue("0");
                        this.DAT02_VS16DE48DEN.SetValue("0");
                        this.DAT02_VS16DE48TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 18:

                        this.DAT02_VS16DE49AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS16DE49NEM.SetValue("0");
                        this.DAT02_VS16DE49DEN.SetValue("0");
                        this.DAT02_VS16DE49TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 19:

                        this.DAT02_VS16DE50AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS16DE50NEM.SetValue("0");
                        this.DAT02_VS16DE50DEN.SetValue("0");
                        this.DAT02_VS16DE50TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 20:

                        this.DAT02_VS16DE51AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS16DE51NEM.SetValue("0");
                        this.DAT02_VS16DE51DEN.SetValue("0");
                        this.DAT02_VS16DE51TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    // 기타 경감.공제 세액 명세
                    case 21:

                        this.DAT02_VS18BE52AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS18BE52NEM.SetValue("0");
                        this.DAT02_VS18BE52DEN.SetValue("0");
                        this.DAT02_VS18BE52TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 22:

                        this.DAT02_VS18BE53AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS18BE53NEM.SetValue("0");
                        this.DAT02_VS18BE53DEN.SetValue("0");
                        this.DAT02_VS18BE53TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 23:

                        this.DAT02_VS18BE54AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS18BE54NEM.SetValue("0");
                        this.DAT02_VS18BE54DEN.SetValue("0");
                        this.DAT02_VS18BE54TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    //case 24: // 원산지확인서 발급세액 공제 삭제

                    //    this.DAT02_VS18BE55AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                    //    this.DAT02_VS18BE55NEM.SetValue("0");
                    //    this.DAT02_VS18BE55DEN.SetValue("0");
                    //    this.DAT02_VS18BE55TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                    //    break;

                    case 24:

                        this.DAT02_VS18BE56AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS18BE56NEM.SetValue("0");
                        this.DAT02_VS18BE56DEN.SetValue("0");
                        this.DAT02_VS18BE56TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 25:

                        this.DAT02_VS18BE57AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS18BE57NEM.SetValue("0");
                        this.DAT02_VS18BE57DEN.SetValue("0");
                        this.DAT02_VS18BE57TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                    case 26:

                        this.DAT02_VS18BE58AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 6].Text.ToString()));
                        this.DAT02_VS18BE58NEM.SetValue("0");
                        this.DAT02_VS18BE58DEN.SetValue("0");
                        this.DAT02_VS18BE58TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_4179D965_Sheet1.Cells[i, 8].Text.ToString()));

                        break;

                }
            }
            #endregion

            #region Description : 가산세 명세
            for (i = 0; i < FPS91_TY_S_AC_417AN968_Sheet1.RowCount; i++)
            {
                switch (i)
                {
                    case 0:

                        this.DAT02_VS24AD59AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD59NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD59DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD59TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 1:

                        this.DAT02_VS24AD60AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD60NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD60DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD60TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 2:

                        this.DAT02_VS24AD61AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD61NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD61DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD61TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 3:

                        this.DAT02_VS24AD62AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD62NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD62DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD62TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 4:

                        this.DAT02_VS24AD63AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD63NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD63DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD63TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 5:

                        this.DAT02_VS24AD64AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD64NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD64DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD64TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 6:

                        this.DAT02_VS24AD65AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD65NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD65DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD65TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 7:

                        this.DAT02_VS24AD66AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD66NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD66DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD66TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 8:

                        this.DAT02_VS24AD67AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD67NEM.SetValue("0");
                        this.DAT02_VS24AD67DEN.SetValue("0");
                        this.DAT02_VS24AD67TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 9:

                        this.DAT02_VS24AD68AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD68NEM.SetValue("0");
                        this.DAT02_VS24AD68DEN.SetValue("0");
                        this.DAT02_VS24AD68TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 10:

                        this.DAT02_VS24AD69AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD69NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD69DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD69TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 11:

                        this.DAT02_VS24AD70AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD70NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD70DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD70TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 12:

                        this.DAT02_VS24AD71AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD71NEM.SetValue("0");
                        this.DAT02_VS24AD71DEN.SetValue("0");
                        this.DAT02_VS24AD71TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 13:

                        this.DAT02_VS24AD72AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD72NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD72DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD72TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 14:

                        this.DAT02_VS24AD73AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD73NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD73DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD73TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 15:

                        this.DAT02_VS24AD74AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD74NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD74DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));
                        this.DAT02_VS24AD74TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;

                    case 16: // 추가 (2014.02.22)   거래계좌미사용

                        this.DAT02_VS24AR75AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));      // 거래계좌미사용
                        this.DAT02_VS24AR75NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));  // 세율자
                        this.DAT02_VS24AR75DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString()));  // 세율모
                        this.DAT02_VS24AR75TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));      // 세액

                        break;

                    case 17: // 추가 (2014.02.22)  거래계좌지연입금

                        this.DAT02_VS24AR76AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));     // 거래계좌지연입금
                        this.DAT02_VS24AR76NEM.SetValue(UP_BUNJA_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString())); // 세율자
                        this.DAT02_VS24AR76DEN.SetValue(UP_BUNMO_Filter(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 5].Text.ToString())); // 세율모
                        this.DAT02_VS24AR76TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));     // 세액

                        break;

                    case 18: // 가산세명세 합계

                        this.DAT02_VS24AD75AMT.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 4].Text.ToString()));
                        this.DAT02_VS24AD75NEM.SetValue("0");
                        this.DAT02_VS24AD75DEN.SetValue("0");
                        this.DAT02_VS24AD75TAX.SetValue(Get_Numeric(this.FPS91_TY_S_AC_417AN968_Sheet1.Cells[i, 6].Text.ToString()));

                        break;
                }
            }
            #endregion

            #region Description : 면세사업 수입금액
            for (i = 0; i < this.FPS91_TY_S_AC_41713969_Sheet1.RowCount; i++)
            {
                switch (i)
                {
                    case 0:

                        this.DAT02_VS25EX76BUS.SetValue(this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 0].Text.ToString());
                        this.DAT02_VS25EX76NAM.SetValue(this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 1].Text.ToString());
                        this.DAT02_VS25EX76COD.SetValue(this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 2].Text.ToString());
                        this.DAT02_VS25EX76AMT.SetValue(this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 3].Text.ToString());

                        break;

                    case 1:

                        this.DAT02_VS25EX77BUS.SetValue(this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 0].Text.ToString());
                        this.DAT02_VS25EX77NAM.SetValue(this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 1].Text.ToString());
                        this.DAT02_VS25EX77COD.SetValue(this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 2].Text.ToString());
                        this.DAT02_VS25EX77AMT.SetValue(this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 3].Text.ToString());

                        break;

                    case 2:

                        this.DAT02_VS25EX78BUS.SetValue("");
                        this.DAT02_VS25EX78NAM.SetValue(this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 1].Text.ToString());
                        this.DAT02_VS25EX78COD.SetValue(this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 2].Text.ToString());
                        this.DAT02_VS25EX78AMT.SetValue(this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 3].Text.ToString());

                        break;

                    case 3:

                        this.DAT02_VS25EX79AMT.SetValue(this.FPS91_TY_S_AC_41713969_Sheet1.Cells[i, 3].Text.ToString());

                        break;
                }
            }
            #endregion

            #region Description : 계산서 교부 및 수취금액
            this.DAT02_VS26IS80AMT.SetValue(this.TXT01_VS26IS80AMT.GetValue().ToString());
            // 계산서 수취금액
            this.DAT02_VS26IS81AMT.SetValue(this.TXT01_VS26IS81AMT.GetValue().ToString());
            #endregion

            string sVS04SC25TAX1 = string.Empty;
            string sVS04SC25TAX2 = string.Empty;

            string sVSTOTTAXAMT  = string.Empty;
            
            DataTable dt = new DataTable();

            // 본점일 경우
            // 총괄납부사업자일 경우
            // 납부할 세액 = 본점(25)값 + 지점(25)값

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42D5G379",
                this.TXT01_VSYEAR.GetValue().ToString(),
                this.CBO01_VSBRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)  // 예정, 확정
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                // 마감체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_42D5I380",
                    this.TXT01_VSYEAR.GetValue().ToString(),
                    this.CBO01_VSBRANCH.GetValue().ToString(),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)  // 예정, 확정
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_42DA4360",
                        this.TXT01_VSYEAR.GetValue().ToString(),
                        this.CBO01_VSBRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                        getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)  // 예정, 확정
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sVS04SC25TAX2 = dt.Rows[0]["VS04SC25TAX"].ToString();
                    }

                    sVS04SC25TAX1 = this.DAT02_VS04SC25TAX.GetValue().ToString();

                    sVSTOTTAXAMT = string.Format("{0:####}", Convert.ToDouble(Get_Numeric(sVS04SC25TAX1.ToString())) + Convert.ToDouble(Get_Numeric(sVS04SC25TAX2.ToString())));

                    this.DAT02_VSTOTTAXAMT.SetValue(Get_Numeric(sVSTOTTAXAMT.ToString()));
                }
            }

            // 환급구분
            if (fsPOPUP.ToString() != "")
            {
                // 1. 옵션관리 총괄납부 = '2'  AND 총괄납부세액 < 0 AND (25)세액 > 0 THEN 1(일반환급) ELSE SPACE(환급없음)
                // 2. 옵션관리 총괄납부 <> '2' AND (25)세액 < 0 THEN 1(일반환급) ELSE SPACE(환급없음)
                
                // 옵션관리 총괄납부 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_42D5G379",
                    this.TXT01_VSYEAR.GetValue().ToString(),
                    this.CBO01_VSBRANCH.GetValue().ToString(),
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 1), // 1기, 2기
                    getCONFGB(this.CBO01_VSCONFGB.GetValue().ToString(), 2)  // 예정, 확정
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0) // 총괄납부
                {
                    if (double.Parse(this.DAT02_VSTOTTAXAMT.GetValue().ToString()) < 0 &&
                        double.Parse(this.DAT02_VS04SC25TAX.GetValue().ToString()) > 0)
                    {
                        this.CBO01_VSREFUNDGB.SetValue("1");
                    }
                    else
                    {
                        this.CBO01_VSREFUNDGB.SetValue("");
                    }

                }
                else // 총괄납부 아님
                {
                    if (double.Parse(this.DAT02_VS04SC25TAX.GetValue().ToString()) < 0)
                    {
                        this.CBO01_VSREFUNDGB.SetValue("1");
                    }
                    else
                    {
                        this.CBO01_VSREFUNDGB.SetValue("");
                    }
                }
            }

            this.DAT02_VSREFUNDGB.SetValue(this.CBO01_VSREFUNDGB.GetValue().ToString());
        }
        #endregion

        #region Description : 필터
        private string UP_BUNJA_Filter(string sBUNJA)
        {
            string sValue = "";
            for (int i = 0; i < sBUNJA.Length; i++)
            {
                if (sBUNJA.Substring(i, 1) != "/")
                {
                    sValue = sValue + sBUNJA.Substring(i, 1);
                }
                else
                {
                    break;
                }
            }

            return sValue;
        }

        private string UP_BUNMO_Filter(string sBUNMO)
        {
            string sValue = "";

            int iLength = 0;


            for (int i = 0; i < sBUNMO.Length; i++)
            {
                if (sBUNMO.Substring(i, 1) == "/")
                {
                    iLength = i + 1;

                    break;
                }
            }

            for (int j = iLength; j < sBUNMO.Length; j++)
            {
                sValue = sValue + sBUNMO.Substring(j, 1);
            }

            return sValue;
        }
        #endregion

        #region Description : 구분 텍스트 이벤트
        private void CBO01_VSCONFGB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(BTN61_INQ);
                //this.BTN61_INQ_Click(null, null);
            }

            e.Handled = false;
        }
        #endregion

        #region Description : 쿠키 불러오기
        private void UP_Cookie_Load()
        {
            if (TYCookie.Chk == "Cookie")
            {
                this.TXT01_VSYEAR.SetValue(TYCookie.Year);
                this.CBO01_VSBRANCH.SetValue(TYCookie.Branch);
                this.CBO01_VSCONFGB.SetValue(TYCookie.Confgb);
            }
            else
            {
                this.TXT01_VSYEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_Cookie_Save()
        {
            TYCookie.Save(this.TXT01_VSYEAR.GetValue().ToString(), this.CBO01_VSBRANCH.GetValue().ToString(), this.CBO01_VSCONFGB.GetValue().ToString());
        }
        #endregion
    }
}