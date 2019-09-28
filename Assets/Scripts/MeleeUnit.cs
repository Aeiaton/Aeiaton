using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnit : BaseUnit
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ComputeMove(){
        int dir;
        if (isPlayer) {
            dir = 1;
        } else {
            dir = -1;
        }
        
        if (0 <= currentCell.position.y + dir <= 7) { 
            ChessBoardCell to = board.cells[currentCell.position.x, currentCell.position.y + dir];
            if (to.currentUnit == null) {
                destCell = to;
                destCell.currentUnit = this;
                return;
            }
        }
        destCell = currentCell;
    }

}
