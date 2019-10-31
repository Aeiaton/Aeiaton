using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeUnit : BaseUnit
{
    // Update is called once per frame
    void Update()
    {
   
    }

    override public void ComputeMove(){
        int dir;
        if (isPlayer) {
            dir = 1;
        } else {
            dir = -1;
        }
        
        if (0 <= currentCell.position.y + dir && currentCell.position.y + dir <= 7) { 
            ChessBoardCell to = board.cells[currentCell.position.x, currentCell.position.y + dir];
            if (to.currentUnit == null) {
                destCell = to;
                return;
            }
        }
        destCell = currentCell;
    }
    
    override public ChessBoardCell FindOpponent () {

        List<ChessBoardCell> opponents = new List<ChessBoardCell>();
        int y1 = Math.Min(currentCell.position.y + 1, 7);
        int x1 = Math.Max(currentCell.position.x - 1, 0);

        int y2 = Math.Max(currentCell.position.y - 1, 0);
        int x2 = Math.Min(currentCell.position.x + 1, 7);

        for (int y = y1; y >= y2; y--) {
            for (int x = x1; x <= x2; x++) {
                BaseUnit b = board.cells[x,y].currentUnit;
                if (b == null) continue;
                else if (b.isPlayer != this.isPlayer) {
                    opponents.Add(board.cells[x, y]);
                }
            }
        }
        int len = opponents.Count;
        if (len == 0) {
            opponentCell = null;
            return opponentCell;
        } else {
            System.Random r = new System.Random();
            opponentCell = opponents[r.Next(0, opponents.Count)];
            return opponentCell;
        }
    }

}
