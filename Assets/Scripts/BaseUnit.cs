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

    public void Place(ChessBoardCell cell)
    {
        if (currentCell) currentCell.currentUnit = null;
        currentCell = cell;
        currentCell.currentUnit = this;
        rectTransform.position = currentCell.transform.position;
        gameObject.SetActive(true);
    }


    public override void OnBeginDrag(PointerEventData eventData) {}

    public override void OnDrag(PointerEventData eventData) {
        transform.position += (Vector3) eventData.delta;
    }

    public override void OnEndDrag(PointerEventData eventData) {
        ChessBoardCell targetCell = null;
        foreach (ChessBoardCell cell in currentCell.board.boardCells) {
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.rectTransform, Input.mousePosition)) {
                targetCell = cell;
                break;
            }
        }
        foreach (ChessBoardCell cell in currentCell.board.benchCells) {
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.rectTransform, Input.mousePosition)) {
                targetCell = cell;
                break;
            }
        }
        if (!targetCell) {
            transform.position = currentCell.gameObject.transform.position;
        } else {
            Place(targetCell);
            //currentCell = targetCell;
            //currentCell.currentUnit = this;
            transform.position = currentCell.transform.position;
        }
    }
}
