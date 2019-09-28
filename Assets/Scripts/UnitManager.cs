﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{

    public GameObject unitPrefab;

    private List<BaseUnit> playerUnits = null;

    public void Setup(ChessBoard board)
    {
        playerUnits = new List<BaseUnit>();
        for (int i = 0; i < 4; i++)
        {
            GameObject unit = Instantiate(unitPrefab);
            unit.transform.SetParent(transform);
            unit.transform.localScale = new Vector3(1, 1, 1);
            unit.transform.localRotation = Quaternion.identity;
            Type unitType = typeof(TestUnit);
            BaseUnit baseUnit = (BaseUnit) unit.AddComponent(unitType);
            playerUnits.Add(baseUnit);
<<<<<<< HEAD
            baseUnit.Setup(this, new Color32(66, 245, 230, 255));
            baseUnit.Place(board.cells[i, 0]);
=======
            baseUnit.Setup(this, board, new Color32(102, 116, 138, 255));
            baseUnit.Place(board.cells[0, i]);
>>>>>>> 52e948020654fbe90d11defb49ec3e3e23d2f0ac
        }
    }

}
