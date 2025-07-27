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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Compilation;

namespace HW.UnityPlayerWindowHandle.Editor
{
    /// <summary>
    /// プロジェクト設定にUnityPlayerWindowHandleのDefine Symbolを追加するクラス
    /// </summary>
    internal static class PackageSymbol
    {
        /// <summary>
        /// 自身のパッケージが存在する時に定義されるシンボル
        /// </summary>
        private const string PackageSymbolName = "HAS_COMMON_MAIN_WINDOW_HANDLE_GETTER_HW";
        /// <summary>
        /// 自身のパッケージ名
        /// </summary>
        private const string PackageName = "com.hw.unityplayer_window_handle";
        /// <summary>
        /// 自身のパッケージ情報のアセットのGUID
        /// </summary>
        private const string PackageInfoGuid = "4254712e202deed4bb07a2a328a3f059";


        /// <summary>
        /// Define Symbolを処理する
        /// </summary>
        [InitializeOnLoadMethod]
        private static void ProcessSymbol()
        {
            // 再コンパイル直前にDefine Symbol除去判定が実行されるようにする
            CompilationPipeline.compilationStarted += RemoveSymbol;

            // Define Symbolを追加する
            AddSymbol();
        }

        /// <summary>
        /// Define Symbolを追加する
        /// </summary>
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

        /// <summary>
        /// Define Symbolを除去する
        /// </summary>
        /// <param name="obj">開始と終了を一致させるためのインスタンス</param>
        private static void RemoveSymbol(object obj)
        {
            // スタンドアロン用のDefine Symbolを取得する
            var standaloneTarget = NamedBuildTarget.Standalone;
            PlayerSettings.GetScriptingDefineSymbols(standaloneTarget, out var defineArray);

            // 除去予定のDefine Symbolが定義されていない場合は何もしない
            if (!defineArray.Contains(PackageSymbolName)) return;

            // パッケージ情報ファイルの存在を確認する
            var packagePath = string.Concat("Packages/", PackageName, "/package.json");
            if (File.Exists(Path.GetFullPath(packagePath)) &&
                AssetDatabase.AssetPathToGUID(packagePath).ToLower() == PackageInfoGuid)
            {
                // パッケージ情報ファイルが存在し、パッケージ情報のGUIDが想定されたものである場合は
                // パッケージが存在するものとして扱い、何もしない
                return;
            }

            // Define Symbolを除去する
            List<string> defines = new(defineArray);
            defines.Remove(PackageSymbolName);
            PlayerSettings.SetScriptingDefineSymbols(standaloneTarget, defines.ToArray());
        }
    }
}
