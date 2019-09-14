using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class BaseUnit : EventTrigger
{

    public Color color = Color.clear;
    protected RectTransform rectTransform = null;
    protected UnitManager unitManager;
    protected ChessBoardCell currentCell;

    public virtual void Setup(UnitManager unitManager, Color32 color)
    {
        this.unitManager = unitManager;
        this.color = color;
        GetComponent<Image>().color = color;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Place(ChessBoardCell cell) {
        currentCell = cell;
        currentCell.currentUnit = this;
        transform.position = cell.transform.position;
        Debug.Log(cell.transform.position);
        gameObject.SetActive(true);
    }

}
