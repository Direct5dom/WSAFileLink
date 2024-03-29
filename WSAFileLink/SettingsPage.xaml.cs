// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace WSAFileLink
{
    public sealed partial class SettingsPage : Page
    {
        // 启用本地设置数据
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        
        // 材料ComboBox列表List
        public List<string> material { get; } = new List<string>()
        {
            "Mica",
            "Mica Alt",
            "Acrylic"
        };

        // 页面初始化
        public SettingsPage()
        {
            // 初始化
            this.InitializeComponent();

            //backgroundMaterial.PlaceholderText = localSettings.Values["materialStatus"] as string;
            // 读取本地设置数据，调整ComboBox状态
            if (localSettings.Values["materialStatus"] as string == "Mica")
            {
                backgroundMaterial.SelectedItem = material[0];
            }
            else if (localSettings.Values["materialStatus"] as string == "Mica Alt")
            {
                backgroundMaterial.SelectedItem = material[1];
            }
            else if (localSettings.Values["materialStatus"] as string == "Acrylic")
            {
                backgroundMaterial.SelectedItem = material[2];
            }
            else
            {
                // 非法输入，设置默认材料为Mica
                localSettings.Values["materialStatus"] = "Mica Alt";
                backgroundMaterial.SelectedItem = material[0];
            }

            adbPath.Text = localSettings.Values["adb"] as string;
        }

        // 读取本地设置数据，调整TextBox内容
        public void adbPath_TextChanged(object sender, RoutedEventArgs e)
        {
            localSettings.Values["adb"] = adbPath.Text;
        }

        // 背景材料设置ComboBox改动事件
        private void backgroundMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string materialStatus = e.AddedItems[0].ToString();
            switch (materialStatus)
            {
                case "Mica":
                    if (localSettings.Values["materialStatus"] as string != "Mica")
                    {
                        localSettings.Values["materialStatus"] = "Mica";
                        Microsoft.Windows.AppLifecycle.AppInstance.Restart("");
                    }
                    else
                    {
                        localSettings.Values["materialStatus"] = "Mica";
                    }
                    break;
                case "Mica Alt":
                    if (localSettings.Values["materialStatus"] as string != "Mica Alt")
                    {
                        localSettings.Values["materialStatus"] = "Mica Alt";
                        Microsoft.Windows.AppLifecycle.AppInstance.Restart("");
                    }
                    else
                    {
                        localSettings.Values["materialStatus"] = "Mica Alt";
                    }
                    break;
                case "Acrylic":
                    if (localSettings.Values["materialStatus"] as string != "Acrylic")
                    {
                        localSettings.Values["materialStatus"] = "Acrylic";
                        Microsoft.Windows.AppLifecycle.AppInstance.Restart("");
                    }
                    else
                    {
                        localSettings.Values["materialStatus"] = "Acrylic";
                    }
                    break;
                default:
                    throw new Exception($"Invalid argument: {materialStatus}");
            }
        }
    }
}
