using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{

    public Boolean running;
    public GameObject unitPrefab;

    private List<BaseUnit> playerUnits = null;
    private float elapsedTime;

    public void Setup(ChessBoard board)
    {
        running = false;
        playerUnits = new List<BaseUnit>();
        for (int i = 3; i < 4; i++)
        {
            GameObject unit = Instantiate(unitPrefab);
            unit.transform.SetParent(transform);
            unit.transform.localScale = new Vector3(1, 1, 1);
            unit.transform.localRotation = Quaternion.identity;
            Type unitType = typeof(MeleeUnit);
            BaseUnit baseUnit = (BaseUnit) unit.AddComponent(unitType);
            playerUnits.Add(baseUnit);
            baseUnit.Setup(this, board, true, new Color32(255, 255, 255, 255), 5);
            baseUnit.Place(board.boardCells[i, 0], true);
        }

        for (int i = 3; i < 4; ++i) {
            GameObject unit = Instantiate(unitPrefab);
            unit.transform.SetParent(transform);
            unit.transform.localScale = new Vector3(1, 1, 1);
            unit.transform.localRotation = Quaternion.identity;
            Type unitType = typeof(MeleeUnit);
            BaseUnit baseUnit = (BaseUnit) unit.AddComponent(unitType);
            playerUnits.Add(baseUnit);
            baseUnit.Setup(this, board, false, new Color32(1, 1, 1, 255), 3);
            baseUnit.Place(board.boardCells[i+3, 7], true);
        }
    }

    public void Begin() {
        this.running = true;
        foreach(BaseUnit unit in playerUnits) {
            unit.Activate();
        }
    }

    public void Update()
    {
        if (!running) return;
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.5f) {
            elapsedTime = 0;

            List<BaseUnit> toRemove = new List<BaseUnit>();

            foreach(BaseUnit unit in playerUnits) {
                //unit.Tick();
                if (unit.health <= 0) toRemove.Add(unit);
            }
            
            foreach(BaseUnit unit in toRemove) {
                playerUnits.Remove(unit);
                unit.currentCell.currentUnit = null;
                Destroy(unit.gameObject);
            }
        }
    }

}
