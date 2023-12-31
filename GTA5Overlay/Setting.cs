﻿using GTA5HotKey;

namespace GTA5Overlay;

public static class Setting
{
    public static bool VSync = true;
    public static int FPS = 300;

    public static bool ESP_Player = true;
    public static bool ESP_NPC = true;
    public static bool ESP_Pickup = true;

    public static bool ESP_Crosshair = true;
    public static bool ESP_InfoText = true;

    public static bool ESP_2DBox = true;
    public static bool ESP_3DBox = false;
    public static bool ESP_Line = true;
    public static bool ESP_Bone = false;
    public static bool ESP_2DHealthBar = true;
    public static bool ESP_HealthText = false;
    public static bool ESP_NameText = false;

    public static bool AimBot_Enabled = false;
    public static int AimBot_BoneIndex = 0;
    public static float AimBot_Fov = 250.0f;
    public static WinVK AimBot_Key = WinVK.CONTROL;

    public static bool NoTopMostHide = false;

    public static void Reset()
    {
        VSync = true;
        FPS = 300;

        ESP_Player = true;
        ESP_NPC = true;
        ESP_Pickup = true;

        ESP_Crosshair = true;
        ESP_InfoText = true;

        ESP_2DBox = true;
        ESP_3DBox = false;
        ESP_Line = true;
        ESP_Bone = false;
        ESP_2DHealthBar = true;
        ESP_HealthText = false;
        ESP_NameText = false;

        AimBot_Enabled = false;
        AimBot_BoneIndex = 0;
        AimBot_Fov = 250.0f;
        AimBot_Key = WinVK.CONTROL;

        NoTopMostHide = false;
    }
}
