using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : CharacterProperty, IBattle
{
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float rotSpeed = 360.0f;    
    [SerializeField]public MonsterStat myStat;
    NavMeshPath myPath = null;
    public Transform myHitPos;
    public void OnDamage(float dmg)
    {
        myStat.UpdateHP(-dmg);
        if(Mathf.Approximately(myStat.CurHP, 0.0f))
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnActivate(Vector3 target)
    {
        myPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, myPath);
        StartCoroutine(MovingByPath(myPath.corners));
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
        Destroy(gameObject);
    }

    IEnumerator MovingToPosition(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        Coroutine rot = StartCoroutine(RotatingToDirection(dir));

        while(dist > Mathf.Epsilon)
        {
            float delta = moveSpeed * Time.deltaTime;
            if(delta > dist)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }

        StopCoroutine(rot);
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
