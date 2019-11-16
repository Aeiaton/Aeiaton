using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public ChessBoard board;
    public UnitManager unitManager;
    public ShopManager shopManager;

    public enum Stage {SETUP, PLAY};
    public Stage gameStage;

    // UI movement
    private float handMoveTime = 0.3f;
    private bool handMoving = false;
    private bool handHidden = false;
    private float handElapsedTimed;
    private Vector3 handShowingPosition;
    private Vector3 handHiddenPosition;

    void Start()
    {
        gameStage = Stage.SETUP;
        board.CreateBoard();
        board.CreateBench();
        unitManager.Setup(board);

        // Set the position of the card hand ui to whatever it is
        GameObject hand = GameObject.Find("Hand");
        handShowingPosition = hand.transform.position;
        handHiddenPosition = new Vector3(handShowingPosition.x, handShowingPosition.y - 300, 0);

    }

    public void SwitchGameStage(Stage newStage) {
        GameObject hand;
        switch (newStage) {
            case Stage.SETUP:

                // Show the hand selection UI
                hand = GameObject.Find("Hand");
                hand.SetActive(true);

                // Show the play button UI
                GameObject.Find("EndSetupButton").SetActive(true);

                break;
            case Stage.PLAY:

                // Hide the hand selection UI
                hand = GameObject.Find("Hand");
                hand.SetActive(false);

                // Hide the play button UI
                GameObject.Find("EndSetupButton").SetActive(false);


                // Start updating units
                GameObject unitManagerObject = GameObject.Find("UnitManager");
                unitManagerObject.GetComponent<UnitManager>().Begin();

                break;
        }
    }

    // Animate the hand ui moving up or down
    void updateHandUI() {
        if (handMoving) {
            GameObject hand = GameObject.Find("Hand");
            handElapsedTimed += Time.deltaTime;
            if (handHidden) {
                hand.transform.position = Vector3.Lerp(handShowingPosition, handHiddenPosition, handElapsedTimed / handMoveTime);
            } else {
                hand.transform.position = Vector3.Lerp(handHiddenPosition, handShowingPosition, handElapsedTimed / handMoveTime);
            }
            if (handElapsedTimed >= handMoveTime) {
                handMoving = false;
            }
        } else if (Input.GetKeyDown(KeyCode.C)) {
            if (!handMoving) {
                handElapsedTimed = 0f;
                handMoving = true;
                handHidden = !handHidden;
            }
        }
    }

    void Update() {
        updateHandUI();
    }

}
