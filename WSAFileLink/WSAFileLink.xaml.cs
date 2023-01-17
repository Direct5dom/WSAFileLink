// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Diagnostics;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WSAFileLink
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WSAFileLink : Page
    {
        public WSAFileLink()
        {
            this.InitializeComponent();
            ToAndroidPath.Text = "/storage/emulated/0/Download/";
            PullFolderPath.Text = "/storage/emulated/0/Download/";
        }

        private void pushButton_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "PowerShell.exe";
            process.StartInfo.Arguments = "adb connect 127.0.0.1:58526; adb push '" + FileFolderPath.Text + "' '" + ToAndroidPath.Text + "'";
            process.StartInfo.UseShellExecute = false; //�Ƿ�ʹ�ò���ϵͳshell����
            process.StartInfo.CreateNoWindow = true; //�Ƿ����´����������ý��̵�ֵ (����ʾ���򴰿�)
            process.Start();
            process.WaitForExit(); //�ȴ�����ִ�����˳�����
            process.Close();
        }

        private void pullButton_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "PowerShell.exe";
            process.StartInfo.Arguments = "adb connect 127.0.0.1:58526; adb pull '" + PullFolderPath.Text + "' '" + ToWindowsPath.Text + "'; ";
            process.StartInfo.UseShellExecute = false; //�Ƿ�ʹ�ò���ϵͳshell����
            process.StartInfo.CreateNoWindow = false; //�Ƿ����´����������ý��̵�ֵ (����ʾ���򴰿�)
            process.Start();
            process.WaitForExit(); //�ȴ�����ִ�����˳�����
            process.Close();
        }

        private async void toAndroidPickerButton_Click(object sender, RoutedEventArgs e)
        {
            // ����һ��FilePicker
            var filePicker = new FileOpenPicker();
            // ͨ������App.m_window�����ȡ��ǰ���ڵ�HWND
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            // �� HWND ���ļ�ѡ���������
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);
            // ʹ���ļ�ѡ����
            filePicker.FileTypeFilter.Add("*");
            var file = await filePicker.PickSingleFileAsync();
            // ���file��Ϊ�գ���Path����FileFolderPath.Text
            if (file != null)
            {
                FileFolderPath.Text = file.Path;
            }
        }
        private async void toWindowsPickerButton_Click(object sender, RoutedEventArgs e)
        {
            // ����һ��FolderPicker
            var folderPicker = new FolderPicker();
            // ͨ������App.m_window�����ȡ��ǰ���ڵ�HWND
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            // �� HWND ���ļ���ѡ���������
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);
            // ʹ���ļ���ѡ����
            var folder = await folderPicker.PickSingleFolderAsync();
            // ���folder��Ϊ�գ���Path����ToWindowsPath.Text
            if (folder != null)
            {
                ToWindowsPath.Text = folder.Path;
            }
        }
    }
}
