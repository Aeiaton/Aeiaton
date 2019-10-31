using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public ChessBoard board;
    public UnitManager unitManager;
    public ShopManager shopManager;


    void Start()
    {
        board.CreateBoard();
        board.CreateBench();
        unitManager.Setup(board);
    }
}
