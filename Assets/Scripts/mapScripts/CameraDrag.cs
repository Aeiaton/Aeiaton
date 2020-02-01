using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{

    private float speed = 2;
    private Vector3 dragStart;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            dragStart = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 dragEnd = Camera.main.ScreenToViewportPoint(
            Input.mousePosition - dragStart
        );

        Vector3 newPosition = new Vector3(
            dragEnd.x * speed,
            dragEnd.y * speed,
            0);

        transform.Translate(newPosition, Space.World);

    }
}
