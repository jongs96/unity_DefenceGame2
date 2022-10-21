using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerMenuIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TowerType myType = default;
    public GameObject orgTower;
    Vector2 orgPos = Vector2.zero;
    Vector2 dragOffset = Vector2.zero;
    public void OnBeginDrag(PointerEventData eventData)
    {
        orgTower.gameObject.SetActive(true);
        GetComponent<Image>().enabled = false;        
    }
    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, 1 << LayerMask.NameToLayer("Tile")))
        {
            orgTower.transform.position = hit.transform.position;
        }
        else
        {
            orgTower.transform.localPosition = Vector3.zero;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        DefenseGame.Inst.OnCreateTower(myType);
        orgTower.transform.localPosition = Vector3.zero;
        orgTower.gameObject.SetActive(false);
        GetComponent<Image>().enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
