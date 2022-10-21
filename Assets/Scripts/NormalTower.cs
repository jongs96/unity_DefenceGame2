using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTower : Tower
{
    // Start is called before the first frame update
    void Start()
    {
        myStat.Initialize();
        GetComponent<SphereCollider>().radius = myStat.Range;

        base.fireAction = () =>
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet"), myMuzzle.position, myMuzzle.rotation) as GameObject;
            obj.GetComponent<Bullet>().OnFire(myTarget.myHitPos, myStat);
        };        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
