using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public LayerMask attackMask;
    public Monster myTarget = null;
    public Transform myCannon = null;
    public Transform myMuzzle = null;    
    [SerializeField] TowerStat myStat;
    // Start is called before the first frame update
    void Start()
    {
        myStat.Initialize();
        GetComponent<SphereCollider>().radius = myStat.Range;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(myTarget == null)
        {
            if((attackMask & 1 << other.gameObject.layer) != 0)
            {
                myTarget = other.gameObject.GetComponent<Monster>();
                StartCoroutine(Attacking());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (myTarget != null)
        {
            if ((attackMask & 1 << other.gameObject.layer) != 0)
            {
                if(myTarget.gameObject == other.gameObject)
                {
                    myTarget = null;
                }
            }
        }
    }

    IEnumerator Attacking()
    {
        float playTime = 0.0f;
        while(myTarget != null)
        {
            //myCannon.LookAt(myTarget.transform.position);
            Vector3 dir = (myTarget.myHitPos.position - myCannon.position).normalized;            
            myCannon.rotation = Quaternion.Slerp(myCannon.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5.0f);

            playTime += Time.deltaTime;
            if(playTime >= myStat.Delay)
            {
                playTime = 0.0f;
                GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet"), myMuzzle.position, myMuzzle.rotation) as GameObject;
                obj.GetComponent<Bullet>().OnFire(myTarget.myHitPos, myStat.Damage);
            }

            yield return null;
        }
    }
}
