using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStat : MonoBehaviour
{
    [SerializeField] TowerData myData;
    [field: SerializeField] public int myLevel { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float Delay { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public float Effect { get; private set; }

    public int GetUpgradePrice()
    {
        if (myLevel == myData.PriceLength) return -1;
        return myData.GetPrice(myLevel + 1);
    }
    public void UpgradeTower()
    {
        SetLevelData(++myLevel);
    }
    public void Initialize()
    {
        SetLevelData(1);
    }

    void SetLevelData(int lv)
    {
        myLevel = lv;
        Damage = myData.GetDamage(myLevel);
        Delay = myData.GetDelay(myLevel);
        Range = myData.GetRange(myLevel);
        Price += myData.GetPrice(myLevel);
        Effect = myData.GetEffect(myLevel);
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
