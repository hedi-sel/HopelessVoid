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

	public void HexagonInitialize(bool isFlat, Sprite sprite){
		this.isFlat = isFlat;
		selfRenderer.sprite = sprite;

	}
	void Awake() {
		
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
