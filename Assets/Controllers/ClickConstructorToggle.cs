using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickConstructorToggle : MonoBehaviour {

	public Button ConstructorToggle;
	// Use this for initialization
	void Start () {
		Button btn = ConstructorToggle.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		MouseController.Instance.Mode = MouseController.MouseMode.Constructor;
		Debug.Log (MouseController.Instance.Mode);
	}

}
