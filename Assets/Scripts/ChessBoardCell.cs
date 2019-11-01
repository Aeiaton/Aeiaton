using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoardCell : MonoBehaviour
{

    public Vector2Int position = Vector2Int.zero;
    public ChessBoard board = null;
    public RectTransform rectTransform = null;

    public BaseUnit currentUnit;

    public bool occupied = false;

    public void Setup(Vector2Int position, ChessBoard board)
    {
        this.position = position;
        this.board = board;
        rectTransform = GetComponent<RectTransform>();
    }

    public bool isOccupied() {
        return occupied || currentUnit != null;
    }
}
