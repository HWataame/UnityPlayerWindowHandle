# UnityPlayer Window Handle

## 概要 / Overview
Windowsで動作するUnityのスタンドアロンプレイヤーのメインウィンドウのウィンドウハンドルを取得する機能を提供します

Get the Unity Standalone Player main window handle running on Windows

## 動作環境 / Requirements
- Windows 11(21H2以上)
  
  Windows 11 (version 21H2 or later)


- Unity 2021.3以上
  
  Unity 2021.3 or later

- Api Compatibility Level: .NET Standard 2.1 
  

## 動作を確認した環境 / Verified Environment
- Windows 11 24H2 (26100)
- Unity 2021.3.45f1, Unity 2022.3.62f1, Unity 6000.0.54f1, Unity 6000.1.13f1

## 使用方法 / English "Usage" is below this
名前空間`HW.UnityPlayerWindowHandle`内の`UnityPlayerWindow.IsHandleValid`の値が`true`であれば、`UnityPlayerWindow.MainWindowHandle`からスタンドアロンプレイヤーのメインウィンドウのウィンドウハンドルを取得できます
```csharp
if(HW.UnityPlayerWindowHandle.UnityPlayerWindow.IsHandleValid)
{
    // IsHandleValidがtrueであればウィンドウハンドルを取得できる
    nint windowHandle = HW.UnityPlayerWindowHandle.UnityPlayerWindow.MainWindowHandle;

    // 以下、ウィンドウハンドルを使った処理など
    // ...
}
```

## Usage / 日本語の「使用方法」は上にあります
If `UnityPlayerWindow.IsHandleValid`(namespace: `HW.UnityPlayerWindowVisual`) returns `true`, `UnityPlayerWindow.MainWindowHandle`(namespace: `HW.UnityPlayerWindowVisual`) returns the Unity Standalone Player main window handle
```csharp
if(HW.UnityPlayerWindowHandle.UnityPlayerWindow.IsHandleValid)
{
    // If IsHandleValid returns true, MainWindowHandle is available
    nint windowHandle = HW.UnityPlayerWindowHandle.UnityPlayerWindow.MainWindowHandle;

    // Process with window handle
    // ...
}
```

## その他の仕様 / Other specifications
- このパッケージを導入すると`HAS_COMMON_MAIN_WINDOW_HANDLE_GETTER_HW`というDefine Symbolがプロジェクトに設定されます。
  また、アンインストールすると`HAS_COMMON_MAIN_WINDOW_HANDLE_GETTER_HW`は自動で除去されます

  When install this package, a Define Symbol `HAS_COMMON_MAIN_WINDOW_HANDLE_GETTER_HW` will be added to installed project automatically.
  And, When uninstall this package, `HAS_COMMON_MAIN_WINDOW_HANDLE_GETTER_HW` will be removed from the project automatically

- 1.0.1以上では、`SubsystemRegistration`のタイミング（メインウィンドウが表示される前）に自動的にウィンドウハンドルを取得する処理が実行されます。
  プロジェクトのDefine Symbolに`SUPPRESS_AUTO_GET_WINDOW_HANDLE_HW`を指定すると、自動取得を無効にできます

  On 1.0.1 or greater, will be getting Standalone Player main window handle when `SubsystemRegistration` timing.
  If set `SUPPRESS_AUTO_GET_WINDOW_HANDLE_HW` to Define Symbol in Project Setting, auto getting will be disabled

## 導入方法 / English "How to introduction" is below this
1. Gitをインストールする
2. 追加したいプロジェクトを開き、Package Managerを開く
3. 以下のGit URLをコピー、またはこのページ上部の「<> Code」からClone URLをコピーする

   https://github.com/HWataame/UnityPlayerWindowHandle.git

4. 「Package Manager」ウィンドウの左上の「＋」ボタンをクリックし、「Install package from git URL...」を選択する

    <img alt="導入方法01" src="https://github.com/user-attachments/assets/aa01e955-2c34-463e-89d8-fa3d4e5809e1" />
5. 入力欄に手順2でコピーしたURLを貼り付け、「Install」ボタンを押す

    <img alt="導入方法02" src="https://github.com/user-attachments/assets/90796535-6f1b-4d44-a536-85fe97e86b44" />
6. (必要に応じて)Assembly Definition Assetの管理下のエディターコードで利用する場合は、`HW.UnityPlayerWindowHandle`をAssembly Definition Referencesに追加する

    <img alt="導入方法03(必要に応じて)" src="https://github.com/user-attachments/assets/70533759-2b2a-4ebe-80df-8aac5e6aa46d" />

## How to introduction / 日本語の「導入方法」は上にあります
1. Install Git to your computer.
2. Open Package Manager in the Unity project to which you want to add this feature.
3. Copy the following Git URL, or copy the Clone URL from "<> Code" at the top of this page

   https://github.com/HWataame/UnityPlayerWindowHandle.git

4. Click the "+" button in the "Package Manager" window and select "Install package from git URL...".

    <img alt="導入方法01" src="https://github.com/user-attachments/assets/aa01e955-2c34-463e-89d8-fa3d4e5809e1" />
5. Paste the URL copied in Step 2 into the input field and press the "Install" button.

    <img alt="導入方法02" src="https://github.com/user-attachments/assets/90796535-6f1b-4d44-a536-85fe97e86b44" />
6. (If necessary) For use in editor code under the control of Assembly Definition Asset...

   Add `HW.UnityPlayerWindowHandle` to "Assembly Definition References" in your Assembly Definition Asset.

    <img alt="導入方法03(必要に応じて)" src="https://github.com/user-attachments/assets/70533759-2b2a-4ebe-80df-8aac5e6aa46d" />

## ライセンス / License
[MITライセンス](/LICENSE)です

Using ["MIT license"](/LICENSE)
