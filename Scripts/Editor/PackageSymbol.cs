/*
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
プロジェクト設定にUnityPlayerWindowHandleのDefine Symbolを追加するクラス

PackageSymbol.cs
────────────────────────────────────────
バージョン: 1.0.0
2025 Wataame(HWataame)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
*/
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

namespace HW.UnityPlayerWindowHandle.Editor
{
    /// <summary>
    /// プロジェクト設定にUnityPlayerWindowHandleのDefine Symbolを追加するクラス
    /// </summary>
    public static class PackageSymbol
    {
        /// <summary>
        /// UnityPlayerWindowHandleが存在する時に定義されるシンボル
        /// </summary>
        private const string PackageSymbolName = "HAS_COMMON_MAIN_WINDOW_HANDLE_GETTER_HW";


        /// <summary>
        /// Define Symbolを追加する
        /// </summary>
        [InitializeOnLoadMethod]
        private static void AddSymbol()
        {
            // スタンドアロン用のDefine Symbolを取得する
            var standaloneTarget = NamedBuildTarget.Standalone;
            PlayerSettings.GetScriptingDefineSymbols(standaloneTarget, out var defines);

            // 既に追加予定のDefine Symbolが定義されている場合は何もしない
            if (defines.Contains(PackageSymbolName)) return;

            // Define Symbolを追加する
            var newDefines = new string[defines.Length + 1];
            defines.AsSpan().CopyTo(newDefines.AsSpan(0, defines.Length));
            newDefines[^1] = PackageSymbolName;
            PlayerSettings.SetScriptingDefineSymbols(standaloneTarget, newDefines);
        }
    }
}
