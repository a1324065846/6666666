﻿using GTA5Core.Native;
using GTA5Core.Offsets;

namespace GTA5Core.Features;

public static class Weapon
{
    /// <summary>
    /// 补满当前武器弹药
    /// </summary>
    public static void FillCurrentAmmo()
    {
        long pCPed = Game.GetCPed();
        long pCPedWeaponManager = Memory.Read<long>(pCPed + CPed.CPedWeaponManager);
        long pCWeaponInfo = Memory.Read<long>(pCPedWeaponManager + CPedWeaponManager.CWeaponInfo);
        long pCAmmoInfo = Memory.Read<long>(pCWeaponInfo + CWeaponInfo.CAmmoInfo);
        if (!Memory.IsValid(pCAmmoInfo))
            return;

        int getMaxAmmo = Memory.Read<int>(pCAmmoInfo + 0x28);

        long my_offset_1 = pCAmmoInfo;
        long my_offset_2;
        byte ammo_type;
        do
        {
            my_offset_1 = Memory.Read<long>(my_offset_1 + 0x08);
            my_offset_2 = Memory.Read<long>(my_offset_1 + 0x00);

            if (my_offset_1 == 0 || my_offset_2 == 0)
                return;

            ammo_type = Memory.Read<byte>(my_offset_2 + 0x0C);

        } while (ammo_type == 0x00);

        Memory.Write(my_offset_2 + 0x18, getMaxAmmo);
    }

    /// <summary>
    /// 补满全部武器弹药
    /// </summary>
    public static void FillAllAmmo()
    {
        long pCPed = Game.GetCPed();
        long pCPedInventory = Memory.Read<long>(pCPed + CPed.CPedInventory);
        long pWeapon = Memory.Read<long>(pCPedInventory + 0x48);
        if (!Memory.IsValid(pWeapon))
            return;

        int count = 0;
        long offset_1 = Memory.Read<long>(pWeapon + count * 0x08);
        long offset_2 = Memory.Read<long>(offset_1 + 0x08);
        while (offset_1 != 0 && offset_2 != 0)
        {
            int ammo_1 = Memory.Read<int>(offset_2 + 0x28);
            int ammo_2 = Memory.Read<int>(offset_2 + 0x34);
            int max_ammo = Math.Max(ammo_1, ammo_2);
            Memory.Write(offset_1 + 0x20, max_ammo);

            count++;
            offset_1 = Memory.Read<long>(pWeapon + count * 0x08);
            offset_2 = Memory.Read<long>(offset_1 + 0x08);
        }
    }

    /// <summary>
    /// 无限弹药（旧版方式，已弃用）
    /// </summary>
    public static void InfiniteAmmo(bool isEnable)
    {
        if (isEnable)
        {
            long addrAmmo = Memory.FindPattern("41 2B D1 E8");
            if (Memory.IsValid(addrAmmo))
                addrAmmo = Memory.FindPattern("90 90 90 E8");

            Memory.WriteBytes(addrAmmo, new byte[] { 0x90, 0x90, 0x90 });
        }
        else
        {
            long addrAmmo = Memory.FindPattern("41 2B D1 E8");
            if (Memory.IsValid(addrAmmo))
                addrAmmo = Memory.FindPattern("90 90 90 E8");

            Memory.WriteBytes(addrAmmo, new byte[] { 0x41, 0x2B, 0xD1 });
        }
    }

    /// <summary>
    /// 无需换弹（旧版方式，已弃用）
    /// </summary>
    public static void NoReload(bool isEnable)
    {
        if (isEnable)
        {
            long addrAmmo = Memory.FindPattern("41 2B C9 3B C8 0F");
            if (Memory.IsValid(addrAmmo))
                addrAmmo = Memory.FindPattern("90 90 90 3B C8 0F");

            Memory.WriteBytes(addrAmmo, new byte[] { 0x90, 0x90, 0x90 });
        }
        else
        {
            long addrAmmo = Memory.FindPattern("41 2B C9 3B C8 0F");
            if (Memory.IsValid(addrAmmo))
                addrAmmo = Memory.FindPattern("90 90 90 3B C8 0F");

            Memory.WriteBytes(addrAmmo, new byte[] { 0x41, 0x2B, 0xC9 });
        }
    }

    /// <summary>
    /// 弹药编辑，默认0，无限弹药1，无限弹夹2，无限弹药和弹夹3
    /// </summary>
    public static void AmmoModifier(byte flag)
    {
        long pCPed = Game.GetCPed();
        long pCPedInventory = Memory.Read<long>(pCPed + CPed.CPedInventory);

        Memory.Write(pCPedInventory + CPedInventory.AmmoModifier, flag);
    }

    /// <summary>
    /// 无后坐力
    /// </summary>
    public static void NoRecoil()
    {
        long pCPed = Game.GetCPed();
        long pCPedWeaponManager = Memory.Read<long>(pCPed + CPed.CPedWeaponManager);
        long pCWeaponInfo = Memory.Read<long>(pCPedWeaponManager + CPedWeaponManager.CWeaponInfo);

        Memory.Write(pCWeaponInfo + CWeaponInfo.Recoil, 0.0f);
    }

    /// <summary>
    /// 无子弹扩散
    /// </summary>
    public static void NoSpread()
    {
        long pCPed = Game.GetCPed();
        long pCPedWeaponManager = Memory.Read<long>(pCPed + CPed.CPedWeaponManager);
        long pCWeaponInfo = Memory.Read<long>(pCPedWeaponManager + CPedWeaponManager.CWeaponInfo);

        Memory.Write(pCWeaponInfo + CWeaponInfo.Spread, 0.0f);
    }

    /// <summary>
    /// 启用子弹类型，2:Fists，3:Bullet，5:Explosion
    /// </summary>
    public static void ImpactType(byte type)
    {
        long pCPed = Game.GetCPed();
        long pCPedWeaponManager = Memory.Read<long>(pCPed + CPed.CPedWeaponManager);
        long pCWeaponInfo = Memory.Read<long>(pCPedWeaponManager + CPedWeaponManager.CWeaponInfo);

        Memory.Write(pCWeaponInfo + CWeaponInfo.ImpactType, type);
    }

    /// <summary>
    /// 设置子弹类型
    /// </summary>
    public static void ImpactExplosion(int id)
    {
        long pCPed = Game.GetCPed();
        long pCPedWeaponManager = Memory.Read<long>(pCPed + CPed.CPedWeaponManager);
        long pCWeaponInfo = Memory.Read<long>(pCPedWeaponManager + CPedWeaponManager.CWeaponInfo);

        Memory.Write(pCWeaponInfo + CWeaponInfo.ImpactExplosion, id);
    }

    /// <summary>
    /// 武器射程
    /// </summary>
    public static void LongRange()
    {
        long pCPed = Game.GetCPed();
        long pCPedWeaponManager = Memory.Read<long>(pCPed + CPed.CPedWeaponManager);
        long pCWeaponInfo = Memory.Read<long>(pCPedWeaponManager + CPedWeaponManager.CWeaponInfo);

        Memory.Write(pCWeaponInfo + CWeaponInfo.LockRange, 1000.0f);
        Memory.Write(pCWeaponInfo + CWeaponInfo.Range, 2000.0f);
    }

    /// <summary>
    /// 武器快速换弹
    /// </summary>
    public static void FastReload(bool isEnable)
    {
        long pCPed = Game.GetCPed();
        long pCPedWeaponManager = Memory.Read<long>(pCPed + CPed.CPedWeaponManager);
        long pCWeaponInfo = Memory.Read<long>(pCPedWeaponManager + CPedWeaponManager.CWeaponInfo);

        if (isEnable)
            Memory.Write(pCWeaponInfo + CWeaponInfo.ReloadMult, 4.0f);
        else
            Memory.Write(pCWeaponInfo + CWeaponInfo.ReloadMult, 1.0f);
    }
}
