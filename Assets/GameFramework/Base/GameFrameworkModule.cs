namespace GameFramework
{ 
    /// <summary>
    /// 游戏框架模块抽象类
    /// </summary>
    internal abstract class GameFrameworkModule
    {
        /// <summary>
        /// 获取游戏框架模块优先级
        /// </summary>
        internal virtual int Priority => 0;

        /// <summary>
        /// 游戏框架轮询模块
        /// </summary>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        internal abstract void Update(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// 关闭并清理游戏框架模块
        /// </summary>
        internal abstract void Shutdown();
    }
}