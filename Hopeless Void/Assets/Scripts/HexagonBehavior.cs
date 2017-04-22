using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Building {
	NONE,
	CAPITAL,
	FARM,
	MINE
};

public class HexagonBehavior : MonoBehaviour {

	public Sprite[] plains;
	public Sprite[] mountains;

	public SpriteRenderer renderer;
	public bool isflat;
	public 

	void Awake() {
		Sprite[] sprites;
		if (isflat) {
			sprites = plains;
		} else {
			sprites = mountains;
		}
		renderer.sprite = sprites[Random.Range (0, sprites.Length)];
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
