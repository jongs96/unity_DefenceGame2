using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public Transform myGrid;

    public List<Monster> monsterList = new List<Monster>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawning()
    {
        while(true)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Monsters/MonsterDevil"),
                startPos.position,startPos.rotation, myGrid) as GameObject;
            Monster scp = obj.GetComponent<Monster>();
            scp.OnActivate(endPos.position);
            monsterList.Add(scp);
            yield return new WaitForSeconds(2.0f);
        }
    }
    void RemoveMonster(Monster scp)
    {
        monsterList.Remove(scp);
    }
}
