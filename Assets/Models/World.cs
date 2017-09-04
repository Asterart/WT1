using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

	Tile[,] tiles;
	int width;
	public int Width {
		get {
			return width;
		}
	}

	int height;
	public int Height {
		get {
			return height;
		}
	}

	public World(int width = 150, int height = 150) {
		this.width = width;
		this.height = height;

		tiles = new Tile[width, height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tiles [x, y] = new Tile (this, x, y);
			}
		}

	}


	public void SetEDsgnTiles(){
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tiles[x,y].Type = Tile.TileType.Design;
			}
		}
	}


	public Tile GetTileAt(int x, int y){
		if (x > width || x < 0 || y > height || y < 0) {
			return null;
		}
		return tiles [x, y];
	}
}
