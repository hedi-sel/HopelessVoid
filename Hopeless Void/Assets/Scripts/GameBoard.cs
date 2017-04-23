using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Action {
	public BuildingAction action;
	public bool isAction;

	public Action(BuildingAction action, bool isAction){
		this.action = action;
		this.isAction = isAction;
	}
}

public class GameBoard : MonoBehaviour {
	
	static private GameBoard m_Instance;
	static public GameBoard instance { get { return m_Instance; } }
	void Awake(){
		if (m_Instance != null) {
			Destroy (this);
		} else {
			m_Instance = this;
		}
	}

	public enum Direction{
		E,NE,NW,W,SW,SE
	};


	public int MapHeight;
	public int MapWidth;
	public GameObject prefabHexagon;

	private Dictionary<Vector2,HexagonBehavior> map = new Dictionary<Vector2,HexagonBehavior>();
	private Dictionary<Direction,Vector2> dirToVect = new Dictionary<Direction,Vector2>();
	private Vector2[] directionsVector2 = new Vector2[6];	
	private Direction[] directions = new Direction[6];

	public int[] Parameters;  //food, metal, energy, population, capsule
	public void modifyParameters (int a, int[] modif){
		for (int i = 0; i < Parameters.Length; i++) {
			Parameters [i] += a*modif [i];
		}
	}


	public void commit() {
		foreach (HexagonBehavior hexagon in map.Values) {
			hexagon.commit ();
		}

	}
		
	//HexagonProperties

	void Start () {
		dirToVectInit ();
		generateMap ( new Vector2(15,5) );
		ConstantBoard.instance.HexagonPropertiesInit ();
	}
	
	// Update is called once per frame



	public void generateMap(Vector2 cells){
		Vector2 c = new Vector2 (0, 0);
		if (cells.x + cells.y >= 6)
			foreach (Vector2 dir in directionsVector2) {
				cells = cells + ( (createCell (dir, cells)) ? new Vector2 (-1, 0) : new Vector2 (0, -1) );
			}
		while (cells.x+cells.y > 0) {

			if ( (!map.ContainsKey(c)) && (constructible(c))){
				bool isFlat = createCell (c, cells);
				cells = cells + ( isFlat ? new Vector2 (-1, 0) : new Vector2 (0, -1) );
			}
			HexagonBehavior[] hexagons = new HexagonBehavior[map.Count];
			map.Values.CopyTo (hexagons, 0);
			c = hexagons[Random.Range(0,map.Count)].coordinates + directionsVector2[Random.Range(0,6)] ;
		
		}
	}
		

	private bool destructible (HexagonBehavior hex){
		if (getNeighbors (hex).Count > 3)
			return false;
		return constructible (hex.coordinates);
	}

	private bool constructible (Vector2 coordinates){ //Return true if the position creates an Anus
		Dictionary<Direction,HexagonBehavior> hexNeighbors = getNeighbors (coordinates);
		bool last = hexNeighbors.ContainsKey (Direction.SE); 
		int transitions = 0; 
		bool current;
		foreach (Direction d in directions) {
			current = hexNeighbors.ContainsKey (d);
			if (last != current)
				transitions++;
			last = current;
		}
		return transitions <=2;
	}

	private Vector3 getPosition(Vector2 c){
		return (float)4.9* new Vector3 (c.x + c.y/(float)2.0, Mathf.Sqrt(3)*c.y/2, 0);
	}

	private void dirToVectInit (){
		dirToVect.Add (Direction.E ,	new Vector2 (1,0));
		dirToVect.Add (Direction.NE,	new Vector2 (0,1));
		dirToVect.Add (Direction.NW,	new Vector2 (-1,1));
		dirToVect.Add (Direction.W ,	new Vector2 (-1,0));
		dirToVect.Add (Direction.SW,	new Vector2 (0,-1));
		dirToVect.Add (Direction.SE,	new Vector2 (1,-1));
		dirToVect.Values.CopyTo (directionsVector2, 0);
		dirToVect.Keys.CopyTo (directions, 0);
	}

	bool createCell(Vector2 c, Vector2 cells){
		bool isFlat = Random.Range (0, cells.x+cells.y) >= cells.y;
		Sprite[] sprites;
		if (isFlat)
			sprites = ConstantBoard.instance.plainSprites;
		else 
			sprites = ConstantBoard.instance.mountainSprites;
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
