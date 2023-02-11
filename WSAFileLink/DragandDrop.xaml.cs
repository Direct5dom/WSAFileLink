// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ViewManagement;

namespace WSAFileLink
{
    public sealed partial class DragandDrop : Page
    {
        // ���ñ�����������
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public DragandDrop()
        {
            this.InitializeComponent();
        }
        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }
        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Count > 0)
                {
                    var storageFile = items[0] as StorageFile;
                    String adbPath = localSettings.Values["adb"] as string;
                    localSettings.Values["adbCMDs"] = adbPath + " connect 127.0.0.1:58526; " + adbPath + " push '" + storageFile.Path + "' '/storage/emulated/0/Download/';";
                    abdCMD.Text = localSettings.Values["adbCMDs"] as string;
                    ThreadStart childref = new ThreadStart(CallToChildThread);
                    Thread childThread = new Thread(childref);
                    childThread.Start();
                }
            }
        }
        public void CallToChildThread()
        {
            Process process = new Process();
            process.StartInfo.FileName = "PowerShell.exe";
            process.StartInfo.Arguments = localSettings.Values["adbCMDs"] as string;
            //�Ƿ�ʹ�ò���ϵͳshell����
            process.StartInfo.UseShellExecute = false;
            //�Ƿ����´����������ý��̵�ֵ (����ʾ���򴰿�)
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            //�ȴ�����ִ�����˳�����
            process.WaitForExit();
            process.Close();
        }
    }
}
