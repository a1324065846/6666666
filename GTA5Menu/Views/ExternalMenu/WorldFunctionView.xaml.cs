﻿using GTA5Core.Features;
using GTA5Core.GTA.Peds;
using GTA5Core.GTA.Onlines;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.ExternalMenu;

/// <summary>
/// WorldFunctionView.xaml 的交互逻辑
/// </summary>
public partial class WorldFunctionView : UserControl
{
    public WorldFunctionView()
    {
        InitializeComponent();
        GTA5MenuWindow.WindowClosingEvent += GTA5MenuWindow_WindowClosingEvent;

        // Ped列表
        foreach (var item in PedHash.PedHashData)
        {
            ListBox_PedModel.Items.Add(item.Name);
        }
        ListBox_PedModel.SelectedIndex = 0;
    }

    private void GTA5MenuWindow_WindowClosingEvent()
    {

    }

    /////////////////////////////////////////////////

    private void Button_LocalWeather_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        var btnContent = (e.OriginalSource as Button).Content.ToString();
        var index = OnlineData.LocalWeathers.FindIndex(t => t.Name == btnContent);
        if (index != -1)
        {
            World.SetLocalWeather(OnlineData.LocalWeathers[index].Value);
        }
    }

    private void Button_KillNPC_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        World.KillAllNPC(false);
    }

    private void Button_KillAllHostilityNPC_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        World.KillAllNPC(true);
    }

    private void Button_KillAllPolice_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        World.KillAllPolice();
    }

    private void Button_DestroyAllVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        World.DestroyAllVehicles();
    }

    private void Button_DestroyAllNPCVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        World.DestroyAllNPCVehicles(false);
    }

    private void Button_DestroyAllHostilityNPCVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        World.DestroyAllNPCVehicles(true);
    }

    private void Button_TPAllNPCToMe_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        World.TeleportAllNPCToMe(false);
    }

    private void Button_TPHostilityNPCToMe_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        World.TeleportAllNPCToMe(true);
    }

    private void Button_ToWaypoint_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Teleport.ToWaypoint();
    }

    private void Button_ToObjective_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Teleport.ToObjective();
    }

    ////////////////////////////////////////////////////////////////////////////////

    private void Button_ModelChange_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        var index = ListBox_PedModel.SelectedIndex;
        if (index != -1)
            Online.ModelChange(Globals.Joaat(PedHash.PedHashData[index].Value));
    }

    private void ListBox_PedModel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        Button_ModelChange_Click(null, null);
    }
}
