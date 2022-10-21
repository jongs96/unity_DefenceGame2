using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data", order = -1)]
public class MonsterData : ScriptableObject
{
    [SerializeField] float MaxHp = 100;
    [SerializeField] float MoveSpeed = 1.0f;
    [SerializeField] int gold = 0;
    public float HP { get => MaxHp; }
    public float Speed { get => MoveSpeed; }
    public int Gold { get => gold; }
}
