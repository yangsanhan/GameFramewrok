﻿using System;
using UnityEngine;

namespace GameMain
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        private void Start()
        {
            InitBuiltinComponents();
            InitCustomComponents();
        }
    }
}