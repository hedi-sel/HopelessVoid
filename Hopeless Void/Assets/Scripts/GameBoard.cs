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

	void Start () {
		dirToVectInit ();
		generateMap (9);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// TEST
	public void generateMap(int nMax){
		int n = 0; 
		Vector2 c = new Vector2 (0, 0);
		while (n<nMax) {
			if (!map.ContainsKey (c)) {
				createCell (c, Random.Range (0, 9) >= 3);
				n++;
			}
			c = c + new Vector2(Random.Range(0,3)-1,Random.Range(0,3)-1) ;
		}
	}
	// /TEST

	private bool destructible (HexagonBehavior hex){
		Dictionary<Direction,HexagonBehavior> hexNeighbors = getNeighbors (hex);
		if (hexNeighbors.Count > 3)
			return false;
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

	}

	void createCell(Vector2 c, bool isFlat){
		Sprite[] sprites;
		if (isFlat) {
			sprites = plains;
		} else {
			sprites = mountains;
		}
		HexagonBehavior hex = Instantiate (prefabHexagon, getPosition(c), Quaternion.identity, this.gameObject.GetComponent<Transform>()).GetComponent<HexagonBehavior>();
		hex.HexagonInitialize ( isFlat, sprites[Random.Range (0, sprites.Length)], c);
		map.Add(c, hex);
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
		

}
