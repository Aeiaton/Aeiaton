using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameReturnToMapButton : MonoBehaviour
{
    void Start () {
		Button button = this.GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	void OnClick() {
		SceneManager.LoadScene("map");
	}
}
