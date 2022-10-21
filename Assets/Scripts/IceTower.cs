using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower
{
    // Start is called before the first frame update
    void Start()
    {
        myStat.Initialize();
        GetComponent<SphereCollider>().radius = myStat.Range;
        base.fireAction = () =>
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/IceBall"), myMuzzle.position, myMuzzle.rotation) as GameObject;
            obj.GetComponent<IceBall>().OnFire(myTarget.myHitPos, myStat, 2.0f);
        };        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
