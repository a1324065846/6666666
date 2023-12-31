﻿using GTA5Core.Native;
using GTA5Core.Offsets;

namespace GTA5Core.Features;

public static class Game
{
    /// <summary>
    /// 获取 CPed 指针
    /// </summary>
    /// <returns></returns>
    public static long GetCPed()
    {
        var pCPedFactory = Memory.Read<long>(Pointers.WorldPTR);
        return Memory.Read<long>(pCPedFactory + CPedFactory.CPed);
    }

    /// <summary>
    /// 获取 CPlayerInfo 指针
    /// </summary>
    /// <returns></returns>
    public static long GetCPlayerInfo()
    {
        var pCPed = GetCPed();
        return Memory.Read<long>(pCPed + CPed.CPlayerInfo);
    }

    /// <summary>
    /// 获取 CVehicle 指针
    /// </summary>
    /// <param name="pCVehicle"></param>
    /// <returns></returns>
    public static bool GetCVehicle(out long pCVehicle)
    {
        pCVehicle = 0;

        var pCPed = GetCPed();
        var mInVehicle = Memory.Read<byte>(pCPed + CPed.InVehicle);

        if (mInVehicle == 0x01)
        {
            pCVehicle = Memory.Read<long>(pCPed + CPed.CVehicle);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 获取 CReplayInterface 指针
    /// </summary>
    /// <returns></returns>
    public static long GetCReplayInterface()
    {
        return Memory.Read<long>(Pointers.ReplayInterfacePTR);
    }

    /// <summary>
    /// 获取 CPedInterface 指针
    /// </summary>
    /// <returns></returns>
    public static long GetCPedInterface()
    {
        return Memory.Read<long>(GetCReplayInterface() + CReplayInterface.CPedInterface);
    }

    /// <summary>
    /// 获取 CVehicleInterface 指针
    /// </summary>
    /// <returns></returns>
    public static long GetCVehicleInterface()
    {
        return Memory.Read<long>(GetCReplayInterface() + CReplayInterface.CVehicleInterface);
    }

    /// <summary>
    /// 获取 CPedList 指针
    /// </summary>
    /// <returns></returns>
    public static long GetCPedList()
    {
        return Memory.Read<long>(GetCPedInterface() + CPedInterface.CPedList);
    }

    /// <summary>
    /// 获取 CVehicleList 指针
    /// </summary>
    /// <returns></returns>
    public static long GetCVehicleList()
    {
        return Memory.Read<long>(GetCVehicleInterface() + CVehicleInterface.CVehicleList);
    }
}
