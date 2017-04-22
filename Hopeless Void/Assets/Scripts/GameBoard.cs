using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

	public Sprite[] plains;
	public Sprite[] mountains;

	public int MapHeight;
	public int MapWidth;
	public GameObject prefabHexagon;

	private Dictionary<DoubleInt,HexagonBehavior> map = new Dictionary<DoubleInt,HexagonBehavior>();

	void Start () {
		CreateCell (new DoubleInt (0, 0), true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void CreateCell(DoubleInt c, bool isFlat){
		Sprite[] sprites;
		if (isFlat) {
			sprites = plains;
		} else {
			sprites = mountains;
		}
		HexagonBehavior hex = Instantiate (prefabHexagon, c.getPosition(), Quaternion.identity).GetComponent<HexagonBehavior>();
		hex.HexagonInitialize ( isFlat, sprites[Random.Range (0, sprites.Length)] );
		map.Add(c, hex);
	}
		
	public class DoubleInt{
		int x,y;
		public DoubleInt(int x, int y){
			this.x = x; this.y = y;
		}
		public Vector3 getPosition(){
			return new Vector3 (0, 0, 0);
		}
	}
}
