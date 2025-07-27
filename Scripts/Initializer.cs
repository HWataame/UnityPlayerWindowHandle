/*
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
初期化処理を行うクラス

Initializer.cs
────────────────────────────────────────
バージョン: 1.0.0
2025 Wataame(HWataame)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
*/
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN) && !SUPPRESS_AUTO_GET_WINDOW_HANDLE_HW
using UnityEngine;

namespace HW.UnityPlayerWindowHandle
{
    /// <summary>
    /// 初期化処理を行うクラス
    /// </summary>
    internal static class Initializer
    {
        /// <summary>
        /// サブシステム初期化時の処理
        /// </summary>
        /// <remarks>現状のUnityPlayerで一番早い実行タイミングのキューで実行される</remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void OnSubsystemRegistration()
        {
            // ハンドルの有効状態を取得して破棄する
            // (この処理自体にウィンドウハンドルの取得が含まれる)
            _ = UnityPlayerWindow.IsHandleValid;
        }
    }
}
#endif
