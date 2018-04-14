using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]


public class Tile {

	public enum TileType {Empty, Design, Floor};

	TileType type = TileType.Empty;

	Action<Tile> tileTypeCallback;

	public TileType Type {
		get {
			return type;
		}
		set {
			TileType oldType = type;
			type = value;
			//call callback about changes
			if (tileTypeCallback != null && oldType != type) {
				tileTypeCallback(this);
			}
		}
	}

	Item Item;
	Construction Construction;

	World World;

	int pos_x;

	public int pos_X {
		get {
			return pos_x;
		}
	}

	int pos_y;

	public int pos_Y {
		get {
			return pos_y;
		}
	}

	public Tile(World World, int pos_x, int pos_y){
		this.World = World;
		this.pos_x = pos_x;
		this.pos_y = pos_y;
	}

	public void TileTypeChangedCallback(Action<Tile> callback){
		tileTypeCallback = callback;
	}
}
