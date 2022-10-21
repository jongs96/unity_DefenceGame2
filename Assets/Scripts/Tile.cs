using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum STATE
    {
        BLANK, USED
    }
    public STATE myState = STATE.BLANK;
    public Tower myTower = null;
    public bool ConstructTower(TowerType type)
    {
        if (myState == STATE.BLANK)
        {
            GameObject obj = null;
            switch(type)
            {
                case TowerType.Normal:
                    obj = Instantiate(Resources.Load("Prefabs/Towers/Normal"), transform) as GameObject;
                    break;
                case TowerType.Ice:
                    obj = Instantiate(Resources.Load("Prefabs/Towers/IceTower"), transform) as GameObject;
                    break;
            }
            myTower = obj.GetComponent<Tower>();
            myState = STATE.USED;
            return true;
        }
        return false;
    }

    public void DestroyTower()
    {
        Destroy(myTower.gameObject);
        myState = STATE.BLANK;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
