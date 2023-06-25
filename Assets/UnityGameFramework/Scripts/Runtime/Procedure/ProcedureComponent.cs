using System;
using GameFramework;
using GameFramework.Procedure;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 流程组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Procedure")]
    public sealed class ProcedureComponent : GameFrameworkComponent
    {
        private IProcedureManager m_ProcedureManager;
        private ProcedureBase m_EntranceProcedure;

        [SerializeField]
        private string[] m_AvailableProcedureTypeName;

        [SerializeField] 
        private string m_EntranceProcedureTypeName;

        /// <summary>
        /// 获取当前流程
        /// </summary>
        public ProcedureBase CurrentProcedure => m_ProcedureManager.CurrentProcedure;

        /// <summary>
        /// 当前流程持续时间
        /// </summary>
        public float CurrentProcedureTime => m_ProcedureManager.CurrentProcedureTime;

        protected override void Awake()
        {
            base.Awake();

            m_ProcedureManager = GameFrameworkEntry.GetModule<IProcedureManager>();
            if (m_ProcedureManager == null)
            {
                Log.Fatal("Procedure manager is invalid.");
                return;
            }
        }

        private void Start()
        {
            throw new NotImplementedException();
        }
    }
}