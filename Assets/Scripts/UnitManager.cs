﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{

    public GameObject unitPrefab;

    private List<BaseUnit> playerUnits = null;
    private float elapsedTime;

    public void Setup(ChessBoard board)
    {
        playerUnits = new List<BaseUnit>();
        for (int i = 0; i < 4; i++)
        {
            GameObject unit = Instantiate(unitPrefab);
            unit.transform.SetParent(transform);
            unit.transform.localScale = new Vector3(1, 1, 1);
            unit.transform.localRotation = Quaternion.identity;
            Type unitType = typeof(MeleeUnit);
            BaseUnit baseUnit = (BaseUnit) unit.AddComponent(unitType);
            playerUnits.Add(baseUnit);
            baseUnit.Setup(this, board, true, new Color32(66, 245, 230, 255), 5);
            baseUnit.Place(board.cells[i, 0]);

            
        }

        for (int i = 0; i < 4; ++i) {
            GameObject unit = Instantiate(unitPrefab);
            unit.transform.SetParent(transform);
            unit.transform.localScale = new Vector3(1, 1, 1);
            unit.transform.localRotation = Quaternion.identity;
            Type unitType = typeof(MeleeUnit);
            BaseUnit baseUnit = (BaseUnit) unit.AddComponent(unitType);
            playerUnits.Add(baseUnit);
            baseUnit.Setup(this, board, false, new Color32(66, 245, 230, 255), 3);
            baseUnit.Place(board.cells[i+3, 7]);
        }
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.5f) {
            elapsedTime = 0;
            foreach(BaseUnit unit in playerUnits)
            {
                unit.Tick();
            }
        }
    }

    public void Remove(BaseUnit deadUnit) {
        playerUnits.Remove(deadUnit);
        deadUnit.currentCell.currentUnit = null;
        Destroy(deadUnit);
    }

}
