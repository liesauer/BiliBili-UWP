﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace BiliBili3.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DMHideManagePage : Page
    {
        public DMHideManagePage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode== NavigationMode.New)
            {
                LoadSetting();
            }
        }

        private void LoadSetting()
        {
            txt_DM.Text="<d p=\"65.460998535156,1,25,16777215,1486119598,0,313ee262,2946614352\">这是一条正常的弹幕</d>";
            txt_SM.Text = "弹幕格式说明：<d p=\"弹幕出现时间,弹幕模式（1-3 滚动弹幕 4底端弹幕 5顶端弹幕 6.逆向弹幕 7精准定位 8高级弹幕）,弹幕大小（12非常小,16特小,18小,25中,36大,45很大,64特别大）,弹幕颜色（十进制）,弹幕发送时间（时间戳）,弹幕池（0普通池 1字幕池 2特殊池 【目前特殊池为高级弹幕专用】）,弹幕发送人,弹幕ID\">弹幕文本</d>";

            string a = SettingHelper.Get_Guanjianzi();
            if (a.Length != 0)
            {
                list_Guanjianzi.Items.Clear();
                foreach (var item in a.Split('|').ToList())
                {
                    list_Guanjianzi.Items.Add(item);
                }
                list_Guanjianzi.Items.Remove(string.Empty);
            }

            string b = SettingHelper.Get_Yonghu();
            if (b.Length != 0)
            {
                list_Yonghu.Items.Clear();
                foreach (var item in b.Split('|').ToList())
                {
                    list_Yonghu.Items.Add(item);
                }
                list_Yonghu.Items.Remove(string.Empty);
            }
            txt_ZZ.Text = SettingHelper.Get_DMZZ();


        }


        private void btn_AddYonghu_Click(object sender, RoutedEventArgs e)
        {
           // string b = (string)settings.GetSettingValue("Yonghu") + "|" + txt_Yonghu.Text;
            //settings.SetSettingValue("Yonghu", b);

            SettingHelper.Set_Yonghu(SettingHelper.Get_Yonghu() + "|" + txt_Yonghu.Text);
            list_Yonghu.Items.Add(txt_Yonghu.Text);
            txt_Yonghu.Text = string.Empty;
        }

        private void btn_AddGuanjianzi_Click(object sender, RoutedEventArgs e)
        {
            SettingHelper.Set_Guanjianzi(SettingHelper.Get_Guanjianzi() + "|" + txt_Guanjianzi.Text);
            list_Guanjianzi.Items.Add(txt_Guanjianzi.Text);
            txt_Guanjianzi.Text = string.Empty;
        }


        private void btn_DeleteGuanjianzi_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in list_Guanjianzi.SelectedItems)
            {
                string b = SettingHelper.Get_Guanjianzi();
                list_Guanjianzi.Items.Remove(item);
                SettingHelper.Set_Guanjianzi( b.Replace("|" + item, string.Empty));
            }
        }
           private void btn_DeleteYonghu_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in list_Yonghu.SelectedItems)
            {
                string b = SettingHelper.Get_Yonghu();
                list_Yonghu.Items.Remove(item);
                SettingHelper.Set_Yonghu(b.Replace("|" + item, string.Empty));
            }
        }

        private void btn_SaveZZ_Click(object sender, RoutedEventArgs e)
        {
            SettingHelper.Set_DMZZ(txt_ZZ.Text);
        }

        private void btn_TestZZ_Click(object sender, RoutedEventArgs e)
        {
            if (txt_ZZ.Text.Length==0)
            {
                txt_Results.Text = "正则表达式不能为空";
                return;
            }
            if (txt_DM.Text.Length==0)
            {
                txt_Results.Text = "测试弹幕文本不能为空";
                return;
            }

            try
            {
                if (Regex.IsMatch(txt_DM.Text, txt_ZZ.Text))
                {
                    txt_Results.Text = "弹幕测试通过";
                }
                else
                {
                    txt_Results.Text = "弹幕测试不通过";
                }
            }
            catch (Exception ex)
            {
                txt_Results.Text = "测试错误\r\n\r\n" + ex.Message;
            }


        }
    }
}
