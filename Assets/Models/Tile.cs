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

	LooseObj looseObj;
	InstalledObj installedObj;

	World World;

	int x;

	public int X {
		get {
			return x;
		}
	}

	int y;

	public int Y {
		get {
			return y;
		}
	}

	public Tile(World World, int x, int y){
		this.World = World;
		this.x = x;
		this.y = y;
	}

	public void TileTypeChangedCallback(Action<Tile> callback){
		tileTypeCallback = callback;
	}
}
