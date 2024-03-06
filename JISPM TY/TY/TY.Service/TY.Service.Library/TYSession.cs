using System;
using System.Collections.Generic;

namespace TY.Service.Library
{
    /// <summary>
    /// 웹의 세션처럼 프로그램을 사용하는 동안 전역으로 사용하게 될 클래스를 정의
    /// </summary>
    public class TYSession
    {
        /*****************************
         * private 변수
         *****************************/ 
        /// <summary>
        /// 키와 값 Dictionary
        /// </summary>
        private Dictionary<string, object> _dicKeyValues;
        /// <summary>
        /// 세션 변경 이벤트를 발생시킨 폼
        /// </summary>
        private TYBase _currentForm;

        /*****************************
         * 이벤트 관련
         *****************************/
        //----- 세션 키/값 추가/변경 이벤트 -----
        /// <summary>
        /// 세션 키/값 추가/변경 이벤트를 처리하는 메서드
        /// </summary>
        /// <param name="sender">이벤트 소스</param>
        /// <param name="e">이벤트 데이터가 들어있는 TY.Service.Library.TYSession.SessionValueChangedEventArgs</param>
        public delegate void SessionValueChangedEventHandler(object sender, SessionValueChangedEventArgs e);

        /// <summary>
        /// 세션 키/값 추가/변경 이벤트
        /// </summary>
        public event SessionValueChangedEventHandler SessionValueChanged;

        #region SessionValueChangedEventArgs - 세션 키/값 추가/변경 이벤트에 데이터를 제공
        /// <summary>
        /// 세션 키/값 추가/변경 이벤트에 데이터를 제공
        /// </summary>
        public class SessionValueChangedEventArgs : EventArgs
        {
            private TYBase _eventForm;
            private string _key;
            private object _value;

            /// <summary>
            /// TY.Service.Library.Session.SessionValueChangedEventArgs 클래스 초기화
            /// </summary>
            public SessionValueChangedEventArgs(TYBase eventForm, string key, object value)
                : base()
            {
                this._eventForm = eventForm;
                this._key = key;
                this._value = value;
            }

            /// <summary>
            /// 이벤트가 발생한 폼
            /// </summary>
            public TYBase EventForm
            {
                get { return this._eventForm; }
            }

            /// <summary>
            /// 추가/변경된 키
            /// </summary>
            public string Key
            {
                get { return this._key; }
            }

            /// <summary>
            /// 추가/변경된 키의 값
            /// </summary>
            public object Value
            {
                get { return this._value; }
            }
        }
        #endregion

        #region OnSessionValueChanged - 세션 키/값 추가/변경 이벤트 발생 시 처리
        /// <summary>
        /// 세션 키/값 추가/변경 이벤트 발생 시 처리
        /// </summary>
        /// <param name="sender">세션</param>
        /// <param name="e">세션 키/값 추가/변경 이벤트 데이터</param>
        private void OnSessionValueChanged(object sender, SessionValueChangedEventArgs e)
        {
            if (this.SessionValueChanged != null)
            //this.ValueChanged(this, e);
            {
                TYBase tmpTYBase;
                foreach (Delegate tmpDelegate in this.SessionValueChanged.GetInvocationList())
                {
                    if (tmpDelegate != null)
                    {
                        tmpTYBase = tmpDelegate.Target as TYBase;
                        if (tmpTYBase != null && !tmpTYBase.IsDisposed)
                            tmpDelegate.DynamicInvoke(this, e);
                    }
                }
                
            }
        }
        #endregion

        //----- 세션 초기화 이벤트 -----
        /// <summary>
        /// 세션 초기화 이벤트를 처리하는 메서드
        /// </summary>
        /// <param name="sender">이벤트 소스</param>
        /// <param name="e">이벤트 데이터가 없는 EventArgs</param>
        public delegate void SessionClearedEventHandler(object sender, EventArgs e);

        /// <summary>
        /// 세션 초기화 이벤트
        /// </summary>
        public event SessionClearedEventHandler SessionCleared;

        #region OnSessionCleared - 세션 초기화 이벤트 발생 시 처리
        /// <summary>
        /// 세션 초기화 이벤트 발생 시 처리
        /// </summary>
        /// <param name="sender">세션</param>
        /// <param name="e">세션 초기화 이벤트 데이터</param>
        private void OnSessionCleared(object sender, EventArgs e)
        {
            if (this.SessionCleared != null)
            //this.SessionCleared(this, e);
            {
                TYBase tmpTYBase;
                foreach (Delegate tmpDelegate in this.SessionCleared.GetInvocationList())
                {
                    if (tmpDelegate != null)
                    {
                        tmpTYBase = tmpDelegate.Target as TYBase;
                        if (tmpTYBase != null && !tmpTYBase.IsDisposed)
                            tmpDelegate.DynamicInvoke(this, e);
                    }
                }

            }
        }
        #endregion

        /*****************************
         * 멤버 변수
         *****************************/
        /// <summary>
        /// 이벤트 발생 시
        /// </summary>
        internal TYBase CurrentForm
        {
            set { this._currentForm = value; }
        }

        #region this[] - Session 클래스에 해당 키의 값
        /// <summary>
        /// Session 클래스에 해당 키의 값
        /// </summary>
        /// <param name="key">키</param>
        /// <returns>해당 키의 값</returns>
        public object this[string key]
        {
            get
            {
                object rtnValue;
                rtnValue = this._dicKeyValues.TryGetValue(key, out rtnValue) ? rtnValue : null;
                return rtnValue;
            }
            set
            {
                if (this._dicKeyValues.ContainsKey(key))
                    this._dicKeyValues[key] = value;
                else
                    this._dicKeyValues.Add(key, value);
                SessionValueChangedEventArgs e = new SessionValueChangedEventArgs(this._currentForm, key, value);
                this.OnSessionValueChanged(this, e);
            }
        } 
        #endregion

        /*****************************
         * 생성자
         *****************************/
        #region TYSession - TY.Service.Library.Session 클래스 초기화
        /// <summary>
        /// TY.Service.Library.Session 클래스 초기화
        /// </summary>
        public TYSession()
        {
            this._dicKeyValues = new Dictionary<string, object>();
        } 
        #endregion

        /*****************************
         * 클래스 함수
         *****************************/
        #region GetValue - Session 클래스에 해당 키의 값을 가져옴
        /// <summary>
        /// Session 클래스에 해당 키의 값을 가져옴
        /// </summary>
        /// <param name="key">키</param>
        /// <returns>해당 키의 값</returns>
        public object GetValue(string key)
        {
            object rtnValue;
            rtnValue = this._dicKeyValues.TryGetValue(key, out rtnValue) ? rtnValue : null;
            return rtnValue;
        } 
        #endregion

        #region SetValue - Session 클래스에 해당 키의 값을 설정
        /// <summary>
        /// Session 클래스에 해당 키의 값을 설정
        /// </summary>
        /// <param name="key">키</param>
        /// <param name="value">해당 키의 값</param>
        public void SetValue(string key, object value)
        {
            if (this._dicKeyValues.ContainsKey(key))
                this._dicKeyValues[key] = value;
            else
                this._dicKeyValues.Add(key, value);
            SessionValueChangedEventArgs e = new SessionValueChangedEventArgs(this._currentForm, key, value);
            this.OnSessionValueChanged(this, e);
        } 
        #endregion

        #region Clear - Session 클래스를 초기화
        /// <summary>
        /// Session 클래스를 초기화
        /// </summary>
        public void Clear()
        {
            this._dicKeyValues = new Dictionary<string, object>();
            this._currentForm = null;
            this.SessionValueChanged = null;
            this.OnSessionCleared(this, new EventArgs());
        } 
        #endregion
    }
}
