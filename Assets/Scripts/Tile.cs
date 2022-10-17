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

    public void ConstructTower()
    {
        if (myState == STATE.BLANK)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Towers/Normal"), transform) as GameObject;
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
}
