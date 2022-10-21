using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{
    [SerializeField] Vector2 mapSize = Vector2.zero;    
    GameObject[,] tileList;
    NavMeshPath checkPath = null;
    // Start is called before the first frame update
    void Start()
    {
        checkPath = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTower(TowerType type)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, 1 << LayerMask.NameToLayer("Tile")))
        {
            Collider[] list = Physics.OverlapBox(hit.transform.position + Vector3.up * 0.5f, new Vector3(0.5f, 0.5f, 0.5f),
                Quaternion.identity, 1 << LayerMask.NameToLayer("Monster"));
            if (list.Length == 0)
            {
                StartCoroutine(CheckingPath(hit.transform.parent.GetComponent<Tile>(), type));                
            }
        }        
    }

    IEnumerator CheckingPath(Tile tile, TowerType type)
    {
        if(tile.ConstructTower(type))
        {
            yield return new WaitForSeconds(0.1f);
            NavMesh.CalculatePath(DefenseGame.Inst.myWave.startPos.position, DefenseGame.Inst.myWave.endPos.position,
                NavMesh.AllAreas, checkPath);
            if(checkPath.status == NavMeshPathStatus.PathPartial || checkPath.status == NavMeshPathStatus.PathInvalid)
            {
                tile.DestroyTower();
            }
            else
            {
                DefenseGame.Inst.myWave.ResetPath();
                DefenseGame.Inst.myGold -= DefenseGame.Inst.towerDatas[(int)type].GetPrice(1);
            }
        }
    }

    void ClearMap()
    {
        if (tileList == null) return;
        foreach(GameObject obj in tileList)
        {
            DestroyImmediate(obj);
        }
    }

    public void CreateMap()
    {
        int oldX = 0, oldY = 0;
        oldX = PlayerPrefs.GetInt("mapSizeX");
        oldY = PlayerPrefs.GetInt("mapSizeY");
        if (oldX == (int)mapSize.x && oldY == (int)mapSize.y) return;
        PlayerPrefs.SetInt("mapSizeX", (int)mapSize.x);
        PlayerPrefs.SetInt("mapSizeY", (int)mapSize.y);

        ClearMap();
        mapSize.x = Mathf.Clamp((int)mapSize.x, 1, 30);
        mapSize.y = Mathf.Clamp((int)mapSize.y, 1, 30);
        tileList = new GameObject[(int)mapSize.y, (int)mapSize.x];
        Vector3 offset = new Vector3(-mapSize.x / 2.0f + 0.5f, 0.0f, -mapSize.y / 2.0f + 0.5f);
        for (int y = 0; y < (int)mapSize.y; ++y)
        {
            for(int x = 0; x < (int)mapSize.x; ++x)
            {
                Vector3 pos = Vector3.zero;
                pos.x = x;
                pos.z = y;
                tileList[y, x] = Instantiate(Resources.Load("Prefabs/Tiles/Tile"), pos + offset, Quaternion.identity, transform) as GameObject;
                tileList[y, x].name = $"Tile[{y},{x}]";
            }
        }
    }
}
