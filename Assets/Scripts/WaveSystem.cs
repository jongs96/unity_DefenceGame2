using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveSystem : MonoBehaviour
{    
    public Transform startPos;
    public Transform endPos;
    public Transform myGrid;

    public List<Monster> monsterList = new List<Monster>();

    public WaveData[] waveList;
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
        int curWave = 0;
        int curIndex = 0;
        while (true)
        {
            WaveMonster data = waveList[curWave].Get(curIndex++);
            
            yield return new WaitForSeconds(data.delay);
            GameObject org = null;
            switch (data.type)
            {
                case MONSTER.DEVIL:
                    org = Resources.Load("Prefabs/Monsters/MonsterDevil") as GameObject;
                    break;
                case MONSTER.MUMMY:
                    org = Resources.Load("Prefabs/Monsters/MonsterMummy") as GameObject;
                    break;
            }
            GameObject obj = Instantiate(org, startPos.position,startPos.rotation, myGrid) as GameObject;
            Monster scp = obj.GetComponent<Monster>();
            scp.OnActivate(endPos.position, RemoveMonster);
            monsterList.Add(scp);

            if (curIndex == waveList[curWave].Length)
            {
                curIndex = 0;
                if (++curWave == waveList.Length)
                {
                    yield break;
                }
            }
        }//wave clear
    }

    void RemoveMonster(Monster scp)
    {
        monsterList.Remove(scp);
    }

    Coroutine rePath = null;
    public void ResetPath()
    {
        if (rePath != null) StopCoroutine(rePath);
        rePath = StartCoroutine(RePathing());
    }

    IEnumerator RePathing()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (Monster scp in monsterList)
        {
            scp.SetPath();
        }
    }
}
