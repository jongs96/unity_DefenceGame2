using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefenseGame : MonoBehaviour
{
    public UpgradeMenuUI UpgradeMenu = null;
    public static DefenseGame Inst = null;
    public MapManager myMap;
    public WaveSystem myWave;
    public TMPro.TMP_Text goldUI;
    int _gold = 0;
    public int myGold
    {
        get => _gold;
        set
        {
            _gold = value;
            goldUI.text = _gold.ToString();
        }
    }
    public TowerData[] towerDatas;
    private void Awake()
    {
        Inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        myGold = 200;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, 1<< LayerMask.NameToLayer("Tile")))
            {
                Tile tile = hit.transform.parent.GetComponent<Tile>();
                if(tile.myState == Tile.STATE.USED)
                {
                    UpgradeMenu.gameObject.SetActive(true);
                    UpgradeMenu.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(tile.transform.position);
                    UpgradeMenu.Upgrade.onClick.AddListener(()=>
                    {
                        tile.myTower.Upgrade();
                        DisableUpgradeMenu();
                    });
                    UpgradeMenu.Sell.onClick.AddListener(() =>
                    {
                        tile.myTower.Sell();
                        DisableUpgradeMenu();
                    });
                }
                else
                {
                    DisableUpgradeMenu();
                }
            }
            else
            {
                DisableUpgradeMenu();
            }
        }        
    }

    void DisableUpgradeMenu()
    {
        UpgradeMenu.Upgrade.onClick.RemoveAllListeners();
        UpgradeMenu.Sell.onClick.RemoveAllListeners();
        UpgradeMenu.gameObject.SetActive(false);
    }

    public void OnCreateTower(TowerType type)
    {
        if (myGold >= towerDatas[(int)type].GetPrice(1))
        {
            myMap.CreateTower(type);
        }
    }
}
