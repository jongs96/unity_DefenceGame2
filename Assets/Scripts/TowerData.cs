using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Data", menuName = "Scriptable Object/Tower Data", order = -1)]
public class TowerData : ScriptableObject
{
    [SerializeField] float[] Damage;
    [SerializeField] float[] Range;
    [SerializeField] float[] Delay;
    [SerializeField] int[] Price;
    public int PriceLength { get => Price.Length; }
    [SerializeField] float[] Effect;

    public float GetDamage(int lv)
    {
        if (lv < 1 || lv > Damage.Length) return 0.0f;
        return Damage[lv - 1];
    }

    public float GetRange(int lv)
    {
        if (lv < 1 || lv > Range.Length) return 0.0f;
        return Range[lv - 1];
    }

    public float GetDelay(int lv)
    {
        if (lv < 1 || lv > Delay.Length) return 0.0f;
        return Delay[lv - 1];
    }

    public int GetPrice(int lv)
    {
        if (lv < 1 || lv > Delay.Length) return 0;
        return Price[lv - 1];
    }

    public float GetEffect(int lv)
    {
        if (Effect == null) return 0.0f;
        if (lv < 1 || lv > Effect.Length) return 0.0f;
        return Effect[lv - 1];
    }
}
