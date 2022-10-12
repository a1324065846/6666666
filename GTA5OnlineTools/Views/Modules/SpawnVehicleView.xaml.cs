﻿using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;
using GTA5OnlineTools.Views.Modules.SpeedMeter;

namespace GTA5OnlineTools.Views.Modules;

/// <summary>
/// SpawnVehicleView.xaml 的交互逻辑
/// </summary>
public partial class SpawnVehicleView : UserControl
{
    private long SpawnVehicleHash = 0;
    private int[] SpawnVehicleMod;

    private DrawWindow DrawWindow = null;

    private List<PVInfo> pVInfos = new();

    public SpawnVehicleView()
    {
        InitializeComponent();
        this.DataContext = this;
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        Task.Run(() =>
        {
            this.Dispatcher.Invoke(() =>
            {
                var windowData = GTA5Mem.GetGameWindowData();

                TextBlock_ScreenResolution.Text = $"屏幕分辨率 {SystemParameters.PrimaryScreenWidth} x {SystemParameters.PrimaryScreenHeight}";
                TextBlock_GameResolution.Text = $"游戏分辨率 {windowData.Width} x {windowData.Height}";
                TextBlock_ScreenScale.Text = $"缩放比例 {ScreenHelper.GetScalingRatio()}";
            });
        });

        // 载具列表
        for (int i = 0; i < VehicleData.VehicleClassData.Count; i++)
        {
            ListBox_VehicleClass.Items.Add(VehicleData.VehicleClassData[i].ClassName);
        }
        ListBox_VehicleClass.SelectedIndex = 0;
    }

    private void MainWindow_WindowClosingEvent()
    {
        if (DrawWindow != null)
        {
            DrawWindow.Close();
            DrawWindow = null;
        }
    }

    private void ListBox_VehicleClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int index = ListBox_VehicleClass.SelectedIndex;

        if (index != -1)
        {
            ListBox_VehicleInfo.Items.Clear();

            for (int i = 0; i < VehicleData.VehicleClassData[index].VehicleInfo.Count; i++)
            {
                ListBox_VehicleInfo.Items.Add(VehicleData.VehicleClassData[index].VehicleInfo[i].DisplayName);
            }

            ListBox_VehicleInfo.SelectedIndex = 0;
        }
    }

    private void ListBox_VehicleInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SpawnVehicleHash = 0;

        int index1 = ListBox_VehicleClass.SelectedIndex;
        int index2 = ListBox_VehicleInfo.SelectedIndex;

        if (index1 != -1 && index2 != -1)
        {
            SpawnVehicleHash = VehicleData.VehicleClassData[index1].VehicleInfo[index2].Hash;
            SpawnVehicleMod = VehicleData.VehicleClassData[index1].VehicleInfo[index2].Mod;
        }
    }

    private void Button_SpawnOnlineVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        string str = (e.OriginalSource as Button).Content.ToString();

        if (str == "刷出线上载具（空地）")
        {
            Vehicle.SpawnVehicle(SpawnVehicleHash, -255.0f, 5, SpawnVehicleMod);
            //Vehicle.SpawnVehicle(SpawnVehicleHash, -255.0f);
        }
        else
        {
            Vehicle.SpawnVehicle(SpawnVehicleHash, 0.0f, 5, SpawnVehicleMod);
            //Vehicle.SpawnVehicle(SpawnVehicleHash, -255.0f);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////

    private void CheckBox_VehicleGodMode_Click(object sender, RoutedEventArgs e)
    {
        Vehicle.GodMode(Settings.Vehicle.VehicleGodMode = true);
        Settings.Vehicle.VehicleGodMode = CheckBox_VehicleGodMode.IsChecked == true;
    }

    private void CheckBox_VehicleSeatbelt_Click(object sender, RoutedEventArgs e)
    {
        Vehicle.Seatbelt(CheckBox_VehicleSeatbelt.IsChecked == true);
        Settings.Vehicle.VehicleSeatbelt = CheckBox_VehicleSeatbelt.IsChecked == true;
    }

    private void CheckBox_VehicleParachute_Click(object sender, RoutedEventArgs e)
    {
        Vehicle.Parachute(CheckBox_VehicleParachute.IsChecked == true);
    }

    private void CheckBox_VehicleInvisibility_Click(object sender, RoutedEventArgs e)
    {
        Vehicle.Invisibility(CheckBox_VehicleInvisibility.IsChecked == true);
    }

    private void Button_FillVehicleHealth_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Vehicle.FillHealth();
    }

    private void RadioButton_VehicleExtras_None_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_VehicleExtras_None.IsChecked == true)
        {
            Vehicle.Extras(0);
        }
        else if (RadioButton_VehicleExtras_Jump.IsChecked == true)
        {
            Vehicle.Extras(40);
        }
        else if (RadioButton_VehicleExtras_Boost.IsChecked == true)
        {
            Vehicle.Extras(66);
        }
        else if (RadioButton_VehicleExtras_Both.IsChecked == true)
        {
            Vehicle.Extras(96);
        }
    }

    private void Button_RepairVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Vehicle.Fix1stfoundBST();
    }

    private void Button_TurnOffBST_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Online.InstantBullShark(false);
    }

    private void Button_GetInOnlinePV_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Online.GetInOnlinePV();
    }

    private void Button_UnlockVehicle161_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Hacks.WriteGA<int>(262145 + 33034, 1);        // Benefactor SM722                 - 33034
        Hacks.WriteGA<int>(262145 + 33035, 1);        // Declasse Draugur                 - 33035
        Hacks.WriteGA<int>(262145 + 33036, 1);        // Imponte Ruiner ZZ-8              - 33036
        Hacks.WriteGA<int>(262145 + 33037, 1);        // Grotti Brioso 300                - 33037
        Hacks.WriteGA<int>(262145 + 33038, 1);        // Declasse Virgero ZX              - 33038
        Hacks.WriteGA<int>(262145 + 33040, 1);        // DInka Kanjo SJ                   - 33040
        Hacks.WriteGA<int>(262145 + 33041, 1);        // Dinka Postlude                   - 33041
        Hacks.WriteGA<int>(262145 + 33042, 1);        // Obey 10F                         - 33042
        Hacks.WriteGA<int>(262145 + 33043, 1);        // Ubermacht Rhinehart              - 33043
        Hacks.WriteGA<int>(262145 + 33044, 1);        // BF Weevil Ratrod                 - 33044
        Hacks.WriteGA<int>(262145 + 33045, 1);        // Obey 10F Widebody                - 33045
        Hacks.WriteGA<int>(262145 + 33046, 1);        // Ubermacht Sentinel Widebody      - 33046
    }

    private void Button_RunDraw_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        GTA5Mem.SetForegroundWindow();

        if (DrawWindow == null)
        {
            DrawWindow = new DrawWindow();
            DrawWindow.Show();
        }

        var windowData = GTA5Mem.GetGameWindowData();

        TextBlock_ScreenResolution.Text = $"屏幕分辨率 {SystemParameters.PrimaryScreenWidth} x {SystemParameters.PrimaryScreenHeight}";
        TextBlock_GameResolution.Text = $"游戏分辨率 {windowData.Width} x {windowData.Height}";
        TextBlock_ScreenScale.Text = $"缩放比例 {ScreenHelper.GetScalingRatio()}";
    }

    private void Button_StopDraw_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        if (DrawWindow != null)
        {
            DrawWindow.Close();
            DrawWindow = null;
        }

        var windowData = GTA5Mem.GetGameWindowData();

        TextBlock_ScreenResolution.Text = $"屏幕分辨率 {SystemParameters.PrimaryScreenWidth} x {SystemParameters.PrimaryScreenHeight}";
        TextBlock_GameResolution.Text = $"游戏分辨率 {windowData.Width} x {windowData.Height}";
        TextBlock_ScreenScale.Text = $"缩放比例 {ScreenHelper.GetScalingRatio()}";
    }

    private void RadioButton_SpeedMeterPos_Center_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_SpeedMeterPos_Center.IsChecked == true)
        {
            DrawData.IsDrawCenter = true;
        }
        else
        {
            DrawData.IsDrawCenter = false;
        }
    }

    private void RadioButton_SpeedMeterUnit_MPH_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_SpeedMeterUnit_MPH.IsChecked == true)
        {
            DrawData.IsShowMPH = true;
        }
        else
        {
            DrawData.IsShowMPH = false;
        }
    }

    private void Button_RefushPersonalVehicleList_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        ListBox_PersonalVehicle.Items.Clear();
        pVInfos.Clear();

        Task.Run(() =>
        {
            int max_slots = Hacks.ReadGA<int>(1585857);
            for (int i = 0; i < max_slots; i++)
            {
                long hash = Hacks.ReadGA<long>(1585857 + 1 + (i * 142) + 66);
                if (hash == 0)
                    continue;

                string plate = Hacks.ReadGAString(1585857 + 1 + (i * 142) + 1);

                pVInfos.Add(new PVInfo()
                {
                    Index = i,
                    Name = Vehicle.FindVehicleDisplayName(hash, true),
                    hash = hash,
                    plate = plate
                });
            }

            foreach (var item in pVInfos)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ListBox_PersonalVehicle.Items.Add($"[{item.plate}]\t{item.Name}");
                });
            }
        });
    }

    private void Button_SpawnPersonalVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int index = ListBox_PersonalVehicle.SelectedIndex;
        if (index != -1)
        {
            Task.Run(() =>
            {
                Vehicle.SpawnPersonalVehicle(pVInfos[index].Index);
            });
        }
    }
}

public struct PVInfo
{
    public int Index;
    public string Name;
    public long hash;
    public string plate;
}
