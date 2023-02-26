﻿using GTA5Menu.Data;

using GTA5Core.RAGE;
using GTA5Core.RAGE.Vehicles;
using GTA5Core.Feature;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.SpawnVehicle;

/// <summary>
/// FindVehicleView.xaml 的交互逻辑
/// </summary>
public partial class FindVehicleView : UserControl
{
    public List<ModelInfo> AllVehicles { get; private set; } = new();
    public ObservableCollection<ModelInfo> FindVehicles { get; private set; } = new();

    public FindVehicleView()
    {
        InitializeComponent();
        this.DataContext = this;
        SpawnVehicleWindow.WindowClosingEvent += SpawnVehicleWindow_WindowClosingEvent;

        // 填充全部载具
        foreach (var vClass in VehicleHash.VehicleClasses)
        {
            if (vClass.Name == "我的收藏")
                continue;

            foreach (var info in vClass.VehicleInfos)
            {
                AllVehicles.Add(new()
                {
                    Class = vClass.Name,
                    Name = info.Name,
                    Value = info.Value,
                    Image = RAGEHelper.GetVehicleImage(info.Value),
                    Mod = info.Mod
                });
            }
        }
    }

    private void SpawnVehicleWindow_WindowClosingEvent()
    {

    }

    private void Button_FindVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        FindVehicles.Clear();

        var name = TextBox_ModelName.Text;
        if (string.IsNullOrWhiteSpace(name))
            return;

        var result = AllVehicles.FindAll(v => v.Name.Contains(name));
        if (result.Count == 0)
        {
            NotifierHelper.Show(NotifierType.Warning, "未搜索到任何载具");
            return;
        }

        foreach (var item in result)
        {
            FindVehicles.Add(item);
        }
    }

    private void TextBox_ModelName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            Button_FindVehicle_Click(null, null);
        }
    }

    private async void MenuItem_SpawnVehicleA_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (ListBox_VehicleInfo.SelectedItem is ModelInfo info)
        {
            await Vehicle2.SpawnVehicle(info.Value, -255.0f, 5, info.Mod);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请选择正确的载具，操作取消");
        }
    }

    private async void MenuItem_SpawnVehicleB_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (ListBox_VehicleInfo.SelectedItem is ModelInfo info)
        {
            await Vehicle2.SpawnVehicle(info.Value, 0.0f, 5, info.Mod);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请选择正确的载具，操作取消");
        }
    }

    private void MenuItem_AddMyFavorite_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (ListBox_VehicleInfo.SelectedItem is ModelInfo info)
        {
            MyVehicleView.ActionAddMyFavorite(info);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请选择正确的载具，操作取消");
        }
    }

    private void ListBox_VehicleInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        MenuItem_SpawnVehicleA_Click(null, null);
    }
}