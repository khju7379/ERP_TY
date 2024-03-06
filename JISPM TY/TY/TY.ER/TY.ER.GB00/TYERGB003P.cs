using System;
using TY.Service.Library;

namespace TY.ER.GB00
{
    /// <summary>
    /// 태영 전용 코드헬퍼 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김영우
    /// </summary>
    public partial class TYERGB003P : TYCodeBoxPopup
    {
        /// <summary>
        /// 태영 전용 코드헬퍼 팝업
        /// </summary>
        public TYERGB003P()
            : base()
        {
        }

        /// <summary>
        /// 태영 전용 코드헬퍼 팝업
        /// </summary>
        /// <param name="PROCEDURE_NO">프로시져NO</param>
        public TYERGB003P(string PROCEDURE_NO)
            : this()
        {
        }

        private void TYERGB003P_Load(object sender, EventArgs e)
        {
        }
    }
}
