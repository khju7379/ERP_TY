using System;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;

namespace TY.Service.Library.Controls
{
    public class TYDatePicker : TDatePicker, IControlFactory
    {
        public TYDatePicker()
            : base()
        {
            //this.Font = new System.Drawing.Font("맑은 고딕", 9F);
        }

        #region enum DateStringType
        /// <summary>
        /// 날짜를 변환할 문자형
        /// </summary>
        public enum DateStringType
        {
            /// <summary>
            /// 2012년 10월  5일 → "20121005" 형
            /// </summary>
            yyyyMMdd = 1,
            /// <summary>
            /// 2012년 10월  5일 → "201210" 형
            /// </summary>
            yyyyMM = 2,
            /// <summary>
            /// 2012년 10월  5일 → "121005" 형
            /// </summary>
            yyMMdd = 3,
            /// <summary>
            /// 2012년 10월  5일 → "1210" 형
            /// </summary>
            yyMM = 4,
            /// <summary>
            /// 2012년 10월  5일 → "1005" 형
            /// </summary>
            MMdd = 5
        } 
        #endregion

        #region GetString - 선택된 날짜를 문자형으로 가져옴
        /// <summary>
        /// 선택된 날짜를 문자형으로 가져옴
        /// </summary>
        /// <returns>문자형 날짜</returns>
        public string GetString()
        {
            return this.Value.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 선택된 날짜를 문자형으로 가져옴
        /// </summary>
        /// <param name="format">형태</param>
        /// <returns>문자형 날짜</returns>
        public string GetString(string format)
        {
            return this.Value.ToString(format);
        }

        /// <summary>
        /// 선택된 날짜를 문자형으로 가져옴
        /// </summary>
        /// <param name="dateStringType">형태</param>
        /// <returns>문자형 날짜</returns>
        public string GetString(DateStringType dateStringType)
        {
            string rtnValueFormat = string.Empty;

            switch (dateStringType)
            {
                case DateStringType.yyyyMMdd:
                    rtnValueFormat = "yyyyMMdd";
                    break;
                case DateStringType.yyyyMM:
                    rtnValueFormat = "yyyyMM";
                    break;
                case DateStringType.yyMMdd:
                    rtnValueFormat = "yyMMdd";
                    break;
                case DateStringType.yyMM:
                    rtnValueFormat = "yyMM";
                    break;
                case DateStringType.MMdd:
                    rtnValueFormat = "MMdd";
                    break;
                default:
                    rtnValueFormat = "yyyyMMdd";
                    break;
            }

            return this.Value.ToString(rtnValueFormat);
        } 
        #endregion

        #region GetYearString - 년도 4자리를 문자형으로 가져옴
        /// <summary>
        /// 년도 4자리를 문자형으로 가져옴
        /// </summary>
        /// <returns>문자형 년도 4자리</returns>
        public string GetYearString()
        {
            return this.Value.ToString("yyyy");
        } 
        #endregion

        #region GetMonthString - 월 2자리를 문자형으로 가져옴
        /// <summary>
        /// 월 2자리를 문자형으로 가져옴
        /// </summary>
        /// <returns>문자형 월 2자리</returns>
        public string GetMonthString()
        {
            return this.Value.ToString("MM");
        } 
        #endregion

        #region GetDayString - 일 2자리를 문자형으로 가져옴
        /// <summary>
        /// 일 2자리를 문자형으로 가져옴
        /// </summary>
        /// <returns>문자형 일 2자리</returns>
        public string GetDayString()
        {
            return this.Value.ToString("dd");
        } 
        #endregion
    }
}
