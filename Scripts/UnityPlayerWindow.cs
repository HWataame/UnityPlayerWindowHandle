/*
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Unityのスタンドアロンプレイヤーのウィンドウハンドルを保持するクラス

UnityPlayerWindow.cs
────────────────────────────────────────
バージョン: 1.0.0
2025 Wataame(HWataame)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
*/
using AOT;
using HW.UnityPlayerWindowHandle.Libraries;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace HW.UnityPlayerWindowHandle
{
    /// <summary>
    /// JP: Unityのスタンドアロンプレイヤーのウィンドウハンドルを保持するクラス<br />
    /// EN: Unity Standalone Player main window handle getter class
    /// </summary>
    public static class UnityPlayerWindow
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        /// <summary>
        /// スタンドアロンプレイヤーのウィンドウに割り当てられているクラス名
        /// </summary>
        private const string UnityWindowClassName = "UnityWndClass";
        /// <summary>
        /// ウィンドウのクラス名取得用のバッファの大きさ
        /// </summary>
        private const int WindowClassNameBufferCapacity = 16;

        /// <summary>
        /// ウィンドウのクラス名取得用のバッファ
        /// </summary>
        private static StringBuilder windowClassNameBuffer = null;
#endif

        /// <summary>
        /// 初期化が済んでいるか
        /// </summary>
        private static bool isInitialized = false;
        /// <summary>
        /// スタンドアロンプレイヤーのメインウィンドウのウィンドウハンドル
        /// </summary>
        /// <remarks>Windowsのスタンドアロンプレイヤー以外では常に0になる</remarks>
        private static nint mainWindowHandle = 0;
        /// <summary>
        /// ウィンドウハンドルの値が有効か
        /// </summary>
        private static bool isHandleValid = false;

        /// <summary>
        /// JP: スタンドアロンプレイヤーのメインウィンドウのウィンドウハンドル<br />
        /// EN: Standalone Player main window handle
        /// </summary>
        /// <remarks>
        /// JP: Windowsのスタンドアロンプレイヤー以外では常に0になる<br />
        /// EN: Always returns 0 except for running on Windows Standalone Player
        /// </remarks>
        public static nint MainWindowHandle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                // 必要な場合は初期化処理を実行する
                Initialze();

                // ウィンドウハンドルの値が有効であればウィンドウハンドルを返す
                return isHandleValid ? mainWindowHandle : 0;
            }
        }

        /// <summary>
        /// JP: ウィンドウハンドルの値が有効か<br />
        /// EN: Is MainWindowHandle value valid?
        /// </summary>
        public static bool IsHandleValid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                // 必要な場合は初期化処理を実行する
                Initialze();

                // ウィンドウハンドルの値が有効かを返す
                return isHandleValid;
            }
        }


        /// <summary>
        /// 必要があれば初期化処理を実行する
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Initialze()
        {
            // 既に初期化された場合は何もしない
            if (isInitialized) return;

            // 変数を初期化する
            isHandleValid = false;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            // Windowsで実行されている場合
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                // Windowsのスタンドアロンプレイヤーで実行されている場合
                // 自身のプロセスIDを取得する
                uint selfProcessId = Kernel32Wrapper.GetCurrentProcessId();

                // バッファを準備する
                windowClassNameBuffer = new(WindowClassNameBufferCapacity);

                // ウィンドウを列挙してメインウィンドウを探す
                User32Wrapper.EnumWindows(GetWindowHandleCallback, ref selfProcessId);

                // バッファの参照をクリアする
                windowClassNameBuffer = null;
            }
#endif

            // 初期化済みフラグを立てる
            isInitialized = true;
        }

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        /// <summary>
        /// ウィンドウハンドル取得処理の列挙用のコールバック
        /// </summary>
        /// <param name="windowHandle">ウィンドウハンドル</param>
        /// <param name="selfProcessId">自身のプロセスID</param>
        /// <returns>列挙を続行するか</returns>
        [MonoPInvokeCallback(typeof(User32Wrapper.UInt32EnumWindowsProc))]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool GetWindowHandleCallback(nint windowHandle, ref uint selfProcessId)
        {
            if (User32Wrapper.GetWindowThreadProcessId(windowHandle, out var processId) == 0 ||
                selfProcessId != processId)
            {
                // ウィンドウのプロセスIDが自身のプロセスIDと異なる場合は次のウィンドウの判定に進む
                return true;
            }

            if (IsWindowClassUnityStandalonePlayer(windowHandle))
            {
                // ウィンドウがスタンドアロンプレイヤーのメインウィンドウである場合はウィンドウハンドルを保持する
                mainWindowHandle = windowHandle;

                // ウィンドウハンドルの値が有効であるフラグを立てる
                isHandleValid = true;

                // 列挙を終了する
                return false;
            }

            // ウィンドウがスタンドアロンプレイヤーのメインウィンドウではない場合は次のウィンドウの判定に進む
            return true;
        }

        /// <summary>
        /// ウィンドウのクラス名がUnityのスタンドアロンプレイヤーのメインウィンドウのものであるか判定する
        /// </summary>
        /// <param name="windowHandle">ウィンドウハンドル</param>
        /// <returns>判定結果</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsWindowClassUnityStandalonePlayer(nint windowHandle)
        {
            // バッファをクリアする
            windowClassNameBuffer.Clear();

            // ウィンドウのクラス名を取得する
            int charCount = User32Wrapper.GetClassName(
                windowHandle, windowClassNameBuffer, WindowClassNameBufferCapacity - 1);

            // ウィンドウのクラス名を取得できなかった場合は失敗
            if (charCount == 0) return false;

            // バッファからウィンドウのクラス名の文字列を取得する
            var windowClassName = windowClassNameBuffer.ToString();

            // バッファをクリアする
            windowClassNameBuffer.Clear();

            // ウィンドウのクラス名がスタンドアロンプレイヤーのメインウィンドウのクラス名と一致するか判定する
            return windowClassName == UnityWindowClassName;
        }
#endif
    }
}
