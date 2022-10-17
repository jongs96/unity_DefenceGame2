using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Vector2 mapSize = Vector2.zero;
    GameObject[,] tileList;
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClearMap()
    {
        if (tileList == null) return;
        foreach(GameObject obj in tileList)
        {
            DestroyImmediate(obj);
        }
    }
    public void CreateTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, 1 << LayerMask.NameToLayer("Tile")))
        {
            hit.transform.parent.GetComponent<Tile>()?.ConstructTower();
        }
    }
    public void CreateMap()
    {
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
