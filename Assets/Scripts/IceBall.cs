using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : Bullet
{
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFire(Transform tr, TowerStat data, float keep)
    {
        base.OnFire(tr, data);
        base.hitAction = () =>
        {
            Collider[] list = Physics.OverlapSphere(transform.position, 1.0f, 1 << LayerMask.NameToLayer("Monster"));
            foreach (Collider col in list)
            {
                col.GetComponent<Monster>().AddDebuff(new DeBuff(DeBuffType.Slow, keep, data.Effect));
            }
        };
    }
}
