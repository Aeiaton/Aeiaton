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

    protected ChessBoardCell targetCell = null;

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
        rectTransform.position = cell.transform.position;
        Debug.Log("unit position: "+cell.transform.position);
        gameObject.SetActive(true);
    }

    public override void OnBeginDrag(PointerEventData eventData) {
        targetCell = null;
    }

    public override void OnDrag(PointerEventData eventData) {
        transform.position += (Vector3) eventData.delta;
    }

    public override void OnEndDrag(PointerEventData eventData) {
        foreach (ChessBoardCell cell in currentCell.board.cells) {
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.rectTransform, Input.mousePosition)) {
                targetCell = cell;
                break;
            }
        }
        if (!targetCell) {
            transform.position = currentCell.gameObject.transform.position;
        } else {
            currentCell.currentUnit = null;
            currentCell = targetCell;
            currentCell.currentUnit = this;
            transform.position = currentCell.transform.position;
        }
    }

}
