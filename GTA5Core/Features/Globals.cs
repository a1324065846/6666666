﻿using GTA5Core.Native;
using GTA5Core.Offsets;

namespace GTA5Core.Features;

public static class Globals
{
    /// <summary>
    /// 全局地址
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static long GlobalAddress(int index)
    {
        return Memory.Read<long>(Pointers.GlobalPTR + 0x8 * (index >> 0x12 & 0x3F)) + 8 * (index & 0x3FFFF);
    }

    /// <summary>
    /// 泛型读取全局地址
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <returns></returns>
    public static T ReadGA<T>(int index) where T : struct
    {
        return Memory.Read<T>(GlobalAddress(index));
    }

    /// <summary>
    /// 泛型写入全局地址
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <param name="vaule"></param>
    public static void WriteGA<T>(int index, T vaule) where T : struct
    {
        Memory.Write(GlobalAddress(index), vaule);
    }

    /// <summary>
    /// 读取全局地址字符串
    /// </summary>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string ReadGAString(int index, int length = 20)
    {
        return Memory.ReadString(GlobalAddress(index), length);
    }

    /// <summary>
    /// 写入全局地址字符串
    /// </summary>
    /// <param name="index"></param>
    /// <param name="str"></param>
    public static void WriteGAString(int index, string str)
    {
        Memory.WriteString(GlobalAddress(index), str);
    }

    /////////////////////////////////////////////////////

    /// <summary>
    /// 判断是否在线上模式
    /// </summary>
    /// <returns></returns>
    public static bool IsOnlineMode()
    {
        return ReadGA<int>(113810 + 2366 + 539 + 4321) == 145;
    }

    /// <summary>
    /// 获取网络时间
    /// </summary>
    /// <returns></returns>
    public static int GetNetworkTime()
    {
        return ReadGA<int>(1574757 + 11);
    }

    /// <summary>
    /// 获取玩家ID
    /// </summary>
    /// <returns></returns>
    public static int GetPlayerID()
    {
        return ReadGA<int>(Base.oPlayerGA);
    }

    /// <summary>
    /// 获取玩家角色索引
    /// </summary>
    /// <returns></returns>
    public static int GetPlayerIndex()
    {
        return ReadGA<int>(1574918);
    }

    /// <summary>
    /// 获取游戏内产业索引
    /// </summary>
    /// <param name="ID">范围：0~5</param>
    /// <returns></returns>
    public static int GetBusinessIndex(int ID)
    {
        return 1853131 + 1 + GetPlayerID() * 888 + 267 + 187 + 1 + ID * 13;
    }

    /////////////////////////////////////////

    /// <summary>
    /// 字符串转Hash值
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static uint Joaat(string data)
    {
        uint hash = 0u;

        foreach (char c in data.ToLower())
        {
            hash += c;
            hash += hash << 10;
            hash ^= hash >> 6;
        }

        hash += hash << 3;
        hash ^= hash >> 11;
        hash += hash << 15;

        return hash;
    }

    /// <summary>
    /// 写入stat值，只支持int类型
    /// </summary>
    public static async Task WriteIntStat(string hash, int value)
    {
        await Task.Run(async () =>
        {
            if (hash.IndexOf("_") == 0)
                hash = $"MP{GetPlayerIndex()}{hash}";

            var oldHash = ReadGA<uint>(Base.statSetIntOldHash + 1 + 3);       // if (STATS::STAT_GET_INT(statHash,
            var oldValue = ReadGA<int>(Base.statSetIntOldValue + 5525);

            WriteGA(Base.statSetIntOldHash + 1 + 3, Joaat(hash));
            WriteGA(Base.statSetIntOldValue + 5525, value);
            WriteGA(Base.statSetIntMinusOne + 1139, -1);

            await Task.Delay(1000);

            WriteGA(Base.statSetIntOldHash + 1 + 3, oldHash);
            WriteGA(Base.statSetIntOldValue + 5525, oldValue);
        });
    }

    /// <summary>
    /// 掉落物品
    /// </summary>
    public static async Task CreateAmbientPickup(string pickup)
    {
        await Task.Run(async () =>
        {
            if (string.IsNullOrEmpty(pickup))
                return;

            uint pickupHash = Joaat(pickup);

            Vector3 vector3 = Teleport.GetPlayerPosition();

            WriteGA(2764405 + 3, vector3.X);
            WriteGA(2764405 + 4, vector3.Y);
            WriteGA(2764405 + 5, vector3.Z + 3.0f);
            WriteGA(2764405 + 1, 9999);

            WriteGA(4535172 + 1 + ReadGA<int>(2764405) * 85 + 66 + 2, 2);
            WriteGA(2764405 + 6, 1);

            await Task.Delay(200);

            long pReplayInterface = Memory.Read<long>(Pointers.ReplayInterfacePTR);
            long pCPickupInterface = Memory.Read<long>(pReplayInterface + 0x20);    // pCPickupInterface

            long oPickupNum = Memory.Read<long>(pCPickupInterface + 0x110);         // oPickupNum
            long pPickupList = Memory.Read<long>(pCPickupInterface + 0x100);        // pPickupList

            for (long i = 0; i < oPickupNum; i++)
            {
                long dwpPickup = Memory.Read<long>(pPickupList + i * 0x10);
                uint dwPickupHash = Memory.Read<uint>(dwpPickup + 0x468);

                if (dwPickupHash == 4263048111)
                {
                    Memory.Write(dwpPickup + 0x468, pickupHash);
                    break;
                }
            }
        });
    }
}
