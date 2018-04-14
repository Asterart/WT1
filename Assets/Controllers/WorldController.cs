using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldController : MonoBehaviour {

	public static WorldController Instance { get; protected set; }


	public Sprite floorSprite;
	public Sprite designSprite;
	//public Sprite emptySprite;

	public World World { get; protected set; }

	// Use this for initialization
	void Start () {

		if (Instance != null) {
			Debug.LogError ("cannot be 2 worldcontrollers");
		}
		Instance = this;

		//create empty world
		World = new World ();


		//create gameobj for each tiles

		for (int x = 0; x < World.Width; x++) {
			for (int y = 0; y < World.Height; y++) {
				Tile tile_data = World.GetTileAt (x, y);

				GameObject tile_gameobj = new GameObject ();
				tile_gameobj.name = "Tile_" + x + "_" + y;
				tile_gameobj.transform.position = new Vector3 (tile_data.pos_X, tile_data.pos_Y, 0);
				tile_gameobj.transform.SetParent (this.transform, true);
				//add sprite render for empty tiles
				tile_gameobj.AddComponent<SpriteRenderer> ();
				tile_data.TileTypeChangedCallback ((tile) => {OnTileTypeChanged(tile, tile_gameobj); } );

			}
		}

		World.SetEDsgnTiles ();
	}
		
	void Update () {

	}

	void OnTileTypeChanged(Tile tile_data, GameObject tile_gameobj){
		if (tile_data.Type == Tile.TileType.Design) {
			tile_gameobj.GetComponent<SpriteRenderer> ().sprite = designSprite;
			//Debug.LogError ("dsgn sprite visited");
		} else if (tile_data.Type == Tile.TileType.Floor) {
			tile_gameobj.GetComponent<SpriteRenderer> ().sprite = floorSprite;
			//Debug.LogError ("floor sprite visited");
		}
		else {
				tile_gameobj.GetComponent<SpriteRenderer> ().sprite = null;
		}


	}

	public Tile GetTileAtWorldCoord(Vector3 coord){
		int x = Mathf.FloorToInt (coord.x);
		int y = Mathf.FloorToInt (coord.y);

		return WorldController.Instance.World.GetTileAt (x, y);
	}
}
