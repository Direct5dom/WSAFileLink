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
        }

        private void pushButton_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "PowerShell.exe";
            process.StartInfo.Arguments = "adb connect 127.0.0.1:58526; adb push '" + FileFolderPath.Text + "' '" + ToAndroidPath.Text + "'";
            process.StartInfo.UseShellExecute = false; //�Ƿ�ʹ�ò���ϵͳshell����
            process.StartInfo.CreateNoWindow = false; //�Ƿ����´����������ý��̵�ֵ (����ʾ���򴰿�)
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

        private void toAndroidPickerButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void toWindowsPickerButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}