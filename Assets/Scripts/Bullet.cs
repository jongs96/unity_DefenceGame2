using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect = null;
    [SerializeField] float moveSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFire(Transform target, float dmg)
    {
        StartCoroutine(Moving(target, dmg));
    }

    IEnumerator Moving(Transform target, float dmg)
    {
        while(target != null)
        {
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude;
            dir.Normalize();
            float delta = Time.deltaTime * moveSpeed;
            if(delta >= dist)
            {
                //Hit
                delta = dist;
                break;
            }
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }
        transform.position = target.position;
        target.parent.GetComponent<IBattle>()?.OnDamage(dmg);
        Destroy(gameObject);
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }
}
