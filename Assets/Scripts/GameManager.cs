using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public ChessBoard board;
    public UnitManager unitManager;


    void Start()
    {
        board.CreateBoard();
        unitManager.Setup(board);
    }
}
