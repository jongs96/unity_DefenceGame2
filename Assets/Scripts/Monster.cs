using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Monster : CharacterProperty, IBattle
{
    float modifySpeed = 1.0f;
    [SerializeField] float rotSpeed = 360.0f;
    [SerializeField] MonsterStat myStat;
    NavMeshPath myPath = null;
    public Transform myHitPos;
    UnityAction<Monster> disApear = null;
    Vector3 myTarget = Vector3.zero;

    public event UnityAction deadAlarm = null;

    [SerializeField] List<DeBuff> debuffList = new List<DeBuff>();
    void OnDisApear()
    {
        deadAlarm?.Invoke();
        disApear.Invoke(this);
        Destroy(gameObject);
    }

    public void AddDebuff(DeBuff deb)
    {
        for(int i = 0; i < debuffList.Count; ++i)
        {
            if(debuffList[i].type == deb.type)
            {
                DeBuff temp = debuffList[i];
                temp.keepTime = deb.keepTime;
                debuffList[i] = temp;
                return;
            }
        }
        switch(deb.type)
        {
            case DeBuffType.Slow:         
                foreach(Renderer ren in allRenderer)
                {
                    ren.material.SetColor("_Color", Color.blue);
                }                
                modifySpeed = deb.value;
                break;
        }
        debuffList.Add(deb);
    }

    public void OnDamage(float dmg)
    {
        myStat.UpdateHP(-dmg);
        if(Mathf.Approximately(myStat.CurHP, 0.0f))
        {
            DefenseGame.Inst.myGold += myStat.Gold;
            OnDisApear();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < debuffList.Count;)
        {
            DeBuff deb = debuffList[i];
            deb.keepTime -= Time.deltaTime;
            if(deb.keepTime < 0.0f)
            {
                switch(deb.type)
                {
                    case DeBuffType.Slow:
                        modifySpeed = 1.0f;
                        foreach (Renderer ren in allRenderer)
                        {
                            ren.material.SetColor("_Color", Color.white);
                        }
                        break;
                }
                debuffList.RemoveAt(i);
                continue;
            }
            debuffList[i] = deb;
            ++i;
        }
    }

    public void SetPath()
    {
        StopAllCoroutines();       
        NavMesh.CalculatePath(transform.position, myTarget, NavMesh.AllAreas, myPath);
        StartCoroutine(MovingByPath(myPath.corners));
    }

    public void OnActivate(Vector3 target, UnityAction<Monster> dis)
    {
        disApear = dis;
        myTarget = target;
        myPath = new NavMeshPath();
        SetPath();
    }

    IEnumerator MovingByPath(Vector3[] poslist)
    {
        if (poslist.Length < 2) yield break;
        int targetPos = 1;
        myAnim.SetBool("Run Forward", true);
        while (targetPos < poslist.Length)
        {
            yield return StartCoroutine(MovingToPosition(poslist[targetPos++]));
        }
        myAnim.SetBool("Run Forward", false);
        OnDisApear();
    }

    IEnumerator MovingToPosition(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        Coroutine rot = StartCoroutine(RotatingToDirection(dir));

        while(dist > Mathf.Epsilon)
        {
            float delta = myStat.MoveSpeed * modifySpeed * Time.deltaTime;
            if(delta > dist)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }

        if(rot != null) StopCoroutine(rot);
    }

    IEnumerator RotatingToDirection(Vector3 dir)
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if(Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -1.0f;
        }

        while(angle > Mathf.Epsilon)
        {
            float delta = rotSpeed * Time.deltaTime;
            if(delta > angle)
            {
                delta = angle;
            }
            angle -= delta;
            transform.Rotate(Vector3.up * delta * rotDir, Space.World);
            yield return null;
        }
    }
}
