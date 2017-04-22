using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {
	public enum Direction{
		E,NE,SE,W,SW,NW
	};
	public Sprite[] plains;
	public Sprite[] mountains;

	public int MapHeight;
	public int MapWidth;
	public GameObject prefabHexagon;

	private Dictionary<DoubleInt,HexagonBehavior> map = new Dictionary<DoubleInt,HexagonBehavior>();
	private Dictionary<Direction,DoubleInt> dirToVect = new Dictionary<Direction,DoubleInt>();

	void Start () {
		createCell ( new DoubleInt(0, 0) , true );
		dirToVectInit ();

		createTestMap ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// TEST
	public void createTestMap(){
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				createCell ( new DoubleInt(i, j) , Random.Range(0,9) >=3 );
			}
		}
	}
	// /TEST

	private void dirToVectInit (){
		dirToVect.Add (Direction.NE,	new DoubleInt (0, 1));
		dirToVect.Add (Direction.E ,	new DoubleInt (1, 0));
		dirToVect.Add (Direction.NW,	new DoubleInt (1,-1));
		dirToVect.Add (Direction.W ,	new DoubleInt (-1,0));
		dirToVect.Add (Direction.SE,	new DoubleInt (-1,1));
		dirToVect.Add (Direction.SW,	new DoubleInt (0,-1));
		
	}

	void createCell(DoubleInt c, bool isFlat){
		Sprite[] sprites;
		if (isFlat) {
			sprites = plains;
		} else {
			sprites = mountains;
		}
		HexagonBehavior hex = Instantiate (prefabHexagon, c.getPosition(), Quaternion.identity).GetComponent<HexagonBehavior>();
		hex.HexagonInitialize ( isFlat, sprites[Random.Range (0, sprites.Length)], c );
		map.Add(c, hex);
	}

	private Dictionary<Direction,HexagonBehavior> getNeighbors(HexagonBehavior hex){
		Dictionary<Direction,HexagonBehavior> neighbors = new Dictionary<Direction,HexagonBehavior> ();
		foreach (Direction d in dirToVect.Keys) {
			if (  map.ContainsKey ( hex.coordinates.add (dirToVect[d]) )  ) {
				neighbors.Add (d, map [hex.coordinates.add (dirToVect [d])]);
			}
		}
		return neighbors;
	}
		
	public class DoubleInt{
		int x,y;
		public DoubleInt(int x, int y){
			this.x = x; this.y = y;
		}
		public Vector3 getPosition(){
			return 5* new Vector3 (x + y/(float)2.0, Mathf.Sqrt(3)*y/2, 0); //A modifier
		}
		public DoubleInt add(DoubleInt other){
			return new DoubleInt (other.x + x, other.y + y);
		}
		public virtual bool Equals(DoubleInt other){
			return x == other.x && y == other.y;
		}
	}
}
