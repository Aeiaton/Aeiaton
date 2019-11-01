using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndSetupButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = this.GetComponent<Button>();
		button.onClick.AddListener(OnClick);
    }

    void OnClick() {
        GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        gameManager.SwitchGameStage(GameManager.Stage.PLAY);
    }

}
