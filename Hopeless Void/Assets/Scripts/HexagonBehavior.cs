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

	public SpriteRenderer selfRenderer;
	public bool isFlat;
	public GameBoard.DoubleInt coordinates;

	public void HexagonInitialize(bool isFlat, Sprite sprite, GameBoard.DoubleInt c){
		this.isFlat = isFlat;
		selfRenderer.sprite = sprite;
		this.coordinates = c;
	}
	void Awake() {
		
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
