using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{

	void Start () {
		Button button = this.GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	void OnClick() {
		SceneManager.LoadScene("map");
	}
}
