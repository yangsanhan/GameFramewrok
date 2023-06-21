using System;
using GameFramework;
using UnityEngine;

namespace UnityGameFramework.Runtime
{ 
    /// <summary>
    /// 基础组件
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Base")]
    public sealed class BaseComponent : GameFrameworkComponent
    {
        [SerializeField]
        private int m_FrameRate = 30;
        
        [SerializeField]
        private string m_TextHelperTypeName = "UnityGameFramework.Runtime.DefaultTextHelper";
        
        [SerializeField]
        private string m_LogHelperTypeName = "UnityGameFramework.Runtime.DefaultLogHelper";

        /// <summary>
        /// 获取或设置游戏帧率。
        /// </summary>
        public int FrameRate
        {
            get => m_FrameRate;
            set => Application.targetFrameRate = m_FrameRate = value;
        }
        
        protected override void Awake()
        {
            base.Awake();

            InitTextHelper();
        }

        private void Update()
        {
            GameFrameworkEntry.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }
        
        private void OnDestroy()
        {
            GameFrameworkEntry.Shutdown();
        }
        
        internal void Shutdown()
        {
            Destroy(gameObject);
        } 
        
        private void InitTextHelper()
        {
            if (string.IsNullOrEmpty(m_TextHelperTypeName))
            {
                return;
            }

            Type textHelperType = Utility.Assembly.GetType(m_TextHelperTypeName);
            if (textHelperType == null)
            {
                Log.Error("Can not find text helper type '{0}'.", m_TextHelperTypeName);
                return;
            }

            Utility.Text.ITextHelper textHelper = (Utility.Text.ITextHelper)Activator.CreateInstance(textHelperType);
            if (textHelper == null)
            {
                Log.Error("Can not create text helper instance '{0}'.", m_TextHelperTypeName);
                return;
            }
            
            Utility.Text.SetTextHelper(textHelper);
        }

        private void InitLogHelper()
        {
            if (string.IsNullOrEmpty(m_LogHelperTypeName))
            {
                return;
            }

            Type logHelperType = Utility.Assembly.GetType(m_LogHelperTypeName);
            if (logHelperType == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not find log helper type '{0}'.", m_LogHelperTypeName));
            }

            GameFrameworkLog.ILogHelper logHelper = (GameFrameworkLog.ILogHelper)Activator.CreateInstance(logHelperType);
            if (logHelper == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not create log helper instance '{0}'.", m_LogHelperTypeName));
            }
            
            GameFrameworkLog.SetLogHelper(logHelper);
        }
    }
}