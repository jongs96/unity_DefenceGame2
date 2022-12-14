using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    [SerializeField] MonsterData myData = default;
    [field: SerializeField] public float CurHP { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set;}
    [field: SerializeField] public int Gold { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        CurHP = myData.HP;
        MoveSpeed = myData.Speed;
        Gold = myData.Gold;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHP(float v)
    {
        CurHP = Mathf.Clamp(CurHP + v, 0.0f, myData.HP);        
    }
}
