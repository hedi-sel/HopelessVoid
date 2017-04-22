using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {
	public enum Direction{
		E,NE,NW,W,SW,SE
	};
	public Sprite[] plains;
	public Sprite[] mountains;

	public int MapHeight;
	public int MapWidth;
	public GameObject prefabHexagon;

	private Dictionary<Vector2,HexagonBehavior> map = new Dictionary<Vector2,HexagonBehavior>();
	private Dictionary<Direction,Vector2> dirToVect = new Dictionary<Direction,Vector2>();
	private Vector2[] directions = new Vector2[6];

	void Start () {
		dirToVectInit ();
		generateMap ( new Vector2(15,5) );


	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// TEST
	public void generateMap(Vector2 cells){
		foreach (Vector2 v in directions) {
			Debug.Log (v.x + " " + v.y);
		}
		Vector2 c = new Vector2 (0, 0);
		if (cells.x + cells.y >= 6)
			foreach (Vector2 dir in directions) {
				cells = cells + ( (createCell (dir, cells)) ? new Vector2 (-1, 0) : new Vector2 (0, -1) );
			}
		while (cells.x+cells.y > 0) {
			if (!map.ContainsKey (c) && constructible(c)) {
				cells = cells + ( (createCell (c, cells)) ? new Vector2 (-1, 0) : new Vector2 (0, -1) );
			}
			HexagonBehavior[] hexagons = new HexagonBehavior[map.Count];
			map.Values.CopyTo (hexagons, 0);
			c = hexagons[Random.Range(0,map.Count)].coordinates + directions[Random.Range(0,6)] ;
		
		}
	}

	// /TEST

	private bool destructible (HexagonBehavior hex){
		if (getNeighbors (hex).Count > 3)
			return false;
		return constructible (hex.coordinates);
	}

	private bool constructible (Vector2 coordinates){ //Doesn't create an anus!
		Dictionary<Direction,HexagonBehavior> hexNeighbors = getNeighbors (coordinates);
		bool last = hexNeighbors.ContainsKey (Direction.SE); 
		int transitions = 0; 
		bool current;
		foreach (Direction d in dirToVect.Keys) {
			current = hexNeighbors.ContainsKey (d);
			if (last != current)
				transitions++;
		}
		return transitions <=2;
	}

	private Vector3 getPosition(Vector2 c){
		return (float)4.9* new Vector3 (c.x + c.y/(float)2.0, Mathf.Sqrt(3)*c.y/2, 0);
	}

	private void dirToVectInit (){
		dirToVect.Add (Direction.E ,	new Vector2 (1, 0));
		dirToVect.Add (Direction.NE,	new Vector2 (0, 1));
		dirToVect.Add (Direction.NW,	new Vector2 (1,-1));
		dirToVect.Add (Direction.W ,	new Vector2 (-1,0));
		dirToVect.Add (Direction.SW,	new Vector2 (0,-1));
		dirToVect.Add (Direction.SE,	new Vector2 (-1,1));
		dirToVect.Values.CopyTo (directions, 0);

	}

	bool createCell(Vector2 c, Vector2 cells){
		bool isFlat = Random.Range (0, cells.x+cells.y) >= cells.y;
		Sprite[] sprites;
		if (isFlat)
			sprites = plains;
		else 
			sprites = mountains;
		HexagonBehavior hex = Instantiate (prefabHexagon, getPosition(c), Quaternion.identity, this.gameObject.GetComponent<Transform>()).GetComponent<HexagonBehavior>();
		hex.HexagonInitialize ( isFlat, sprites[Random.Range (0, sprites.Length)], c);
		map.Add(c, hex);
		return isFlat;
	}

	private Dictionary<Direction,HexagonBehavior> getNeighbors(HexagonBehavior hex){
		Dictionary<Direction,HexagonBehavior> neighbors = new Dictionary<Direction,HexagonBehavior> ();
		foreach (Direction d in dirToVect.Keys) {
			if (  map.ContainsKey ( hex.coordinates+ (dirToVect[d]) )  ) {
				neighbors.Add (d, map [hex.coordinates+ (dirToVect [d])]);
			}
		}
		return neighbors;
	}

	private Dictionary<Direction,HexagonBehavior> getNeighbors(Vector2 coordinates){
		Dictionary<Direction,HexagonBehavior> neighbors = new Dictionary<Direction,HexagonBehavior> ();
		foreach (Direction d in dirToVect.Keys) {
			if (  map.ContainsKey ( coordinates+ (dirToVect[d]) )  ) {
				neighbors.Add (d, map [coordinates+ (dirToVect [d])]);
			}
		}
		return neighbors;
	}
		

}
