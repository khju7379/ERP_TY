using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using TY.Service.Library;

namespace TY.ER.GB00
{
    /// <summary>
    /// 그룹웨어 공통  팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.14 11:40
    /// </summary>
    public partial class TYERGB012P : TYBase
    {

        private string fsGroupUrl = string.Empty;

        /// <summary>
        /// 그룹웨어 문서 보기 공통 팝업
        /// </summary>
        /// <param name="activeReport">액티브 레포트</param>
        /// <param name="source">바인딩 데이터</param>
        public TYERGB012P(string sGrUrl)
        {
            InitializeComponent();

            this.SetPopupStyle();
            
            fsGroupUrl = sGrUrl;

        }

        private void TYERGB012P_Load(object sender, System.EventArgs e)
        {
            webB1.Navigate(fsGroupUrl);
        }    
       
        
    }
}
