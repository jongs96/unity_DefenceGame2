using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TowerType
{
    Normal, Ice
}

public class Tower : MonoBehaviour
{
    public LayerMask attackMask;
    public Monster myTarget = null;
    public Transform myCannon = null;
    public Transform myMuzzle = null;        
    [SerializeField] protected TowerStat myStat;
    [SerializeField] protected List<Monster> targetList = new List<Monster>();    
    protected UnityAction fireAction = null;

    public void Upgrade()
    {
        int needGold = myStat.GetUpgradePrice();
        if (needGold > 0 && DefenseGame.Inst.myGold >= needGold)
        {
            myStat.UpgradeTower();
            DefenseGame.Inst.myGold -= needGold;
        }
    }
    public void Sell()
    {
        DefenseGame.Inst.myGold += (int)((float)myStat.Price * 0.5f);
        transform.parent.GetComponent<Tile>().DestroyTower();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Monster scp = other.gameObject.GetComponent<Monster>();
            scp.deadAlarm += () =>
            {
                targetList.Remove(scp);
                if(myTarget == scp)
                {
                    myTarget = FindCloseTarget();
                }
            };
            if (scp != null) targetList.Add(scp);
            if (myTarget == null)
            {
                if ((attackMask & 1 << other.gameObject.layer) != 0)
                {
                    myTarget = other.gameObject.GetComponent<Monster>();
                    StopAllCoroutines();
                    StartCoroutine(Attacking());
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Monster scp = other.gameObject.GetComponent<Monster>();
            if (scp != null) targetList.Remove(scp);

            if (myTarget != null)
            {
                if ((attackMask & 1 << other.gameObject.layer) != 0)
                {
                    if (myTarget.gameObject == other.gameObject)
                    {
                        myTarget = FindCloseTarget();
                    }
                }
            }
        }
    }

    Monster FindCloseTarget()
    {
        if (targetList.Count == 0) return null;
        int sel = 0;
        float min = Mathf.Infinity;
        for(int i = 0; i < targetList.Count;++i)
        {
            float dist = Vector3.Distance(transform.position, targetList[i].transform.position);
            if(dist < min)
            {
                sel = i;
                min = dist;
            }
        }
        return targetList[sel];
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
                fireAction?.Invoke();
                playTime = 0.0f;                
            }

            yield return null;
        }
    }
}
