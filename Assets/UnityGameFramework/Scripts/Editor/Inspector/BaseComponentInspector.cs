using GameFramework;
using System.Collections.Generic;
using UnityEditor;
using UnityGameFramework.Runtime;

namespace UnityGameFramework.Editor
{
    [CustomEditor(typeof(BaseComponent))]
    internal sealed class BaseComponentInspector : GameFrameworkInspector
    {
        private const string NoneOptionName = "<None>";
        
        private SerializedProperty m_TextHelperTypeName;
        private SerializedProperty m_LogHelperTypeName;
        private SerializedProperty m_FrameRate;

        private string[] m_TextHelperTypeNames;
        private int m_TextHelperTypeNameIndex;
        private string[] m_LogHelperTypeNames;
        private int m_LogHelperTypeNameIndex;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            BaseComponent t = (BaseComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                EditorGUILayout.BeginVertical("box");
                {
                    EditorGUILayout.LabelField("Global Helpers", EditorStyles.boldLabel);

                    int textHelperSelectedIndex = EditorGUILayout.Popup("Text Helper", m_TextHelperTypeNameIndex, m_TextHelperTypeNames);
                    if (textHelperSelectedIndex != m_TextHelperTypeNameIndex)
                    {
                        m_TextHelperTypeNameIndex = textHelperSelectedIndex;
                        m_TextHelperTypeName.stringValue = textHelperSelectedIndex <= 0 ? null : m_TextHelperTypeNames[textHelperSelectedIndex];
                    }

                    int logHelperSelectedIndex = EditorGUILayout.Popup("Log Helper", m_LogHelperTypeNameIndex, m_LogHelperTypeNames);
                    if (logHelperSelectedIndex != m_LogHelperTypeNameIndex)
                    {
                        m_LogHelperTypeNameIndex = logHelperSelectedIndex;
                        m_LogHelperTypeName.stringValue = logHelperSelectedIndex <= 0 ? null : m_LogHelperTypeNames[logHelperSelectedIndex];
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUI.EndDisabledGroup();

            int frameRate = EditorGUILayout.IntSlider("Frame Rate", m_FrameRate.intValue, 1, 120);
            if (frameRate != m_FrameRate.intValue)
            {
                if (EditorApplication.isPlaying)
                {
                    t.FrameRate = frameRate;
                }
                else
                {
                    m_FrameRate.intValue = frameRate;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();

            RefreshTypeNames();
        }

        private void OnEnable()
        {
            m_TextHelperTypeName = serializedObject.FindProperty("m_TextHelperTypeName");
            m_LogHelperTypeName = serializedObject.FindProperty("m_LogHelperTypeName");
            m_FrameRate = serializedObject.FindProperty("m_FrameRate");
            
            RefreshTypeNames();
        }

        private void RefreshTypeNames()
        {
            List<string> textHelperTypeNames = new List<string>
            {
                NoneOptionName
            }; 

            textHelperTypeNames.AddRange(Type.GetRuntimeTypeNames(typeof(Utility.Text.ITextHelper)));
            m_TextHelperTypeNames = textHelperTypeNames.ToArray(); 
            m_TextHelperTypeNameIndex = 0;
            if (!string.IsNullOrEmpty(m_TextHelperTypeName.stringValue))
            {
                m_TextHelperTypeNameIndex = textHelperTypeNames.IndexOf(m_TextHelperTypeName.stringValue);
                if (m_TextHelperTypeNameIndex <= 0)
                {
                    m_TextHelperTypeNameIndex = 0;
                    m_TextHelperTypeName.stringValue = null;
                }
            }
            
            List<string> logHelperTypeNames = new List<string>
            {
                NoneOptionName
            };

            logHelperTypeNames.AddRange(Type.GetRuntimeTypeNames(typeof(GameFrameworkLog.ILogHelper)));
            m_LogHelperTypeNames = logHelperTypeNames.ToArray();
            m_LogHelperTypeNameIndex = 0;
            if (!string.IsNullOrEmpty(m_LogHelperTypeName.stringValue))
            {
                m_LogHelperTypeNameIndex = logHelperTypeNames.IndexOf(m_LogHelperTypeName.stringValue);
                if (m_LogHelperTypeNameIndex <= 0)
                {
                    m_LogHelperTypeNameIndex = 0;
                    m_LogHelperTypeName.stringValue = null;
                }
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
