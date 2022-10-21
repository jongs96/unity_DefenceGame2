using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MONSTER
{
    DEVIL, MUMMY
}

[Serializable]
public struct WaveMonster
{
    public float delay;
    public MONSTER type;
    public WaveMonster(float d, MONSTER t)
    {
        delay = d;
        type = t;
    }
}

[CreateAssetMenu(fileName = "Wave Data", menuName = "Scriptable Object/Wave Data", order = -1)]

public class WaveData : ScriptableObject
{
    [SerializeField] WaveMonster[] Monsters;
    public int Length
    {
        get => Monsters.Length;
    }
    public WaveMonster Get(int i)
    {
        if (i < 0 || i >= Monsters.Length)
        {
            return new WaveMonster(1.0f, MONSTER.DEVIL);
        }
        return Monsters[i];
    }
}