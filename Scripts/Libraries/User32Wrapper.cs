/*
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
user32.dllの関数を呼び出せるようにするラッパークラス

User32Wrapper.cs
────────────────────────────────────────
バージョン: 1.0.0
2025 Wataame(HWataame)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
*/
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
using System.Runtime.InteropServices;
using System.Text;

namespace HW.UnityPlayerWindowHandle.Libraries
{
    /// <summary>
    /// user32.dllの関数を呼び出せるようにするラッパークラス
    /// </summary>
    internal static class User32Wrapper
    {
        /// <summary>
        /// EnumWindowsのコールバック用のデリゲート型
        /// </summary>
        /// <remarks>32ビット符号なし整数値1個をパラメーターとして渡す用に引数を変更した版</remarks>
        /// <param name="windowHandle">ウィンドウハンドル</param>
        /// <param name="value">パラメーター</param>
        /// <returns>列挙を続行するか</returns>
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool UInt32EnumWindowsProc(nint windowHandle, ref uint value);


        /// <summary>
        /// ウィンドウを列挙する
        /// </summary>
        /// <remarks>32ビット符号なし整数値1個をパラメーターとして渡す用に引数を変更した版</remarks>
        /// <param name="callback">列挙用のコールバック</param>
        /// <param name="value">パラメーター</param>
        /// <returns>処理結果</returns>
        [DllImport("user32.dll", EntryPoint = "EnumWindows", CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumWindows(UInt32EnumWindowsProc callback, ref uint value);

        /// <summary>
        /// ウィンドウを生成したスレッドとプロセスのIDを取得する
        /// </summary>
        /// <param name="windowHandle">ウィンドウのハンドル</param>
        /// <param name="processId">プロセスID</param>
        /// <returns>スレッドID</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", CallingConvention = CallingConvention.Winapi)]
        internal static extern uint GetWindowThreadProcessId(nint windowHandle, out uint processId);

        /// <summary>
        /// ウィンドウのクラス名を取得する
        /// </summary>
        /// <param name="windowHandle">ウィンドウハンドル</param>
        /// <param name="className">クラス名</param>
        /// <param name="maxCharCount">クラス名の最大文字数</param>
        /// <returns>取得されたクラス名の文字数</returns>
        [DllImport("user32.dll", EntryPoint = "GetClassNameW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        internal static extern int GetClassName(
            nint windowHandle,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder className,
            int maxCharCount);
    }
}
#endif
