using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect = null;
    [SerializeField] float moveSpeed = 10.0f;
    protected UnityAction hitAction = null;
    public void OnFire(Transform target, TowerStat data)
    {
        StartCoroutine(Moving(target, data.Damage));
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
        if (target != null)
        {
            transform.position = target.position;
            target.parent.GetComponent<IBattle>()?.OnDamage(dmg);
            Instantiate(hitEffect, transform.position, hitEffect.transform.localRotation);
            hitAction?.Invoke();
        }
        Destroy(gameObject);        
    }
}
