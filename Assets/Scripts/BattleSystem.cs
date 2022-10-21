using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle
{
    void OnDamage(float dmg);
}

public enum DeBuffType
{
    Slow
}
public struct DeBuff
{
    public DeBuff(DeBuffType t, float keep, float v)
    {
        type = t;
        keepTime = keep;
        value = v;
    }
    public DeBuffType type;
    public float keepTime;
    public float value;
}

public class BattleSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
