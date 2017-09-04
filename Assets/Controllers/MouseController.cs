using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {


	public GameObject selectCursorPrefab;

	Vector3 lastFramePosition;
	Vector3 dragStartPosition;
	Vector3 currentFramePosition;
	//list of selected tiles via dragging
	List<GameObject> dragPrevGO;

	// Use this for initialization
	void Start () {
		dragPrevGO = new List<GameObject> ();

		//SimplePool.Preload (selectCursorPrefab, 100);
	}
	
	// Update is called once per frame
	void Update () {

		currentFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		currentFramePosition.z = 0;

		//what tile is under the mouse
		Tile tileUnderMouse = WorldController.Instance.GetTileAtWorldCoord(currentFramePosition);

		UpdateDragging ();
		UpdateCameraMovement ();

		//mouse dragging view moving
		lastFramePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		currentFramePosition.z = 0;
		lastFramePosition.z = 0;

	}

	//main mouse movement
	void UpdateCameraMovement(){
		if (Input.GetMouseButton(1) || Input.GetMouseButton(2)) {
			Vector3 diff = lastFramePosition - currentFramePosition;
			//Debug.Log (Camera.main.S);
			var possition = Camera.main.transform.position.x;
			Debug.Log (possition);
			lastFramePosition.z = 0;
			Camera.main.transform.Translate(diff);
		}

		float maxScrollSize = 100f;
		Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis ("Mouse ScrollWheel");
		Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, 3f, maxScrollSize);
	}

	//mouse dragging action
	void UpdateDragging(){
		if (Input.GetMouseButtonDown(0)) {
			dragStartPosition = currentFramePosition;
		}

		int start_x = Mathf.FloorToInt (dragStartPosition.x);
		int end_x = Mathf.FloorToInt (currentFramePosition.x);
		if (end_x < start_x) {
			int tmp = end_x;
			end_x = start_x;
			start_x = tmp;
		}
		int start_y = Mathf.FloorToInt (dragStartPosition.y);
		int end_y = Mathf.FloorToInt (currentFramePosition.y);
		if (end_y < start_y) {
			int tmp = end_y;
			end_y = start_y;
			start_y = tmp;
		}

		//cleaning old dragging
		while (dragPrevGO.Count > 0) {
			GameObject go = dragPrevGO [0];
			dragPrevGO.RemoveAt (0);
			SimplePool.Despawn (go);
		}

		if (Input.GetMouseButton(0)) {
			for (int x = start_x; x <= end_x; x++) {
				for (int y = start_y; y <= end_y; y++) {
					Tile t = WorldController.Instance.World.GetTileAt (x, y);
					if (t != null) {
						GameObject go = SimplePool.Spawn(selectCursorPrefab, new Vector3(x,y,0), Quaternion.identity);
						go.transform.SetParent (this.transform, true);
						dragPrevGO.Add (go);
					}
				}
			}
		}

		//end drag
		if (Input.GetMouseButtonUp (0)) {
			
			for (int x = start_x; x <= end_x; x++) {
				for (int y = start_y; y <= end_y; y++) {
					Tile t = WorldController.Instance.World.GetTileAt (x, y);
					if (t != null) {
						t.Type = Tile.TileType.Floor;
					}
				}
			}
		}
	}



}
