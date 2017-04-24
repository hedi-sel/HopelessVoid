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

	public static void DeleteInstance(){
		m_Instance = null;
	}

	public float xMax, xMin, yMax, yMin;
	public GameObject prefabHexagon;

	private Dictionary<Vector2,HexagonBehavior> map = new Dictionary<Vector2,HexagonBehavior>();
	private Dictionary<Direction,Vector2> dirToVect = new Dictionary<Direction,Vector2>();
	private Vector2[] directionsVector2 = new Vector2[6];	
	private Direction[] directions = new Direction[6];

	public int[] Parameters;  //food, metal, energy, population, capsule
	public int maxCapsule;
	public int occupiedPopulation;

	public void modifyParameters (int[] modif){
		modifyParameters (1, modif);
	}
	public void modifyParameters (int a, int[] modif){
		for (int i = 0; i < Parameters.Length; i++) {
			Parameters [i] += a*modif [i];
		}
	}
		
	public void updateInterfaceParameters() {
		GUIHandler.instance.top.SetFood (Parameters[0]);
		GUIHandler.instance.top.SetMetal (Parameters[1]);
		GUIHandler.instance.top.SetCrystal (Parameters[2]);
		GUIHandler.instance.top.SetPeople (Parameters[3]-occupiedPopulation, Parameters[3] );
		GUIHandler.instance.top.SetCapsule (Parameters[4], maxCapsule);
	}

	public Sprite voidSprite;

	public void commit() {
		
		if (map.Count == 1 || Parameters[3]<1 ){
			GameHandler.instance.SetState ("MenuScene");
			return;
		}

		HexagonBehavior[] hexagons = new HexagonBehavior[map.Count];
		map.Values.CopyTo (hexagons, 0);

		foreach (HexagonBehavior hex in map.Values) {
			hex.commit ();
		}

		HexagonBehavior hexagon = hexagons[0];
		while (!destructible(hexagon)) {
			hexagon = hexagons [Random.Range (0, map.Count)];
		}
		map.Remove (hexagon.coordinates);
		hexagon.collapse ();

		hexagon = hexagons[0];

		int food = Parameters [0];
		if (food < Parameters [3]) {
			Parameters [0] = 0;
			Parameters [3] = food;
			//Procédure de tuage de gens qui meurent de faim
			while (occupiedPopulation > Parameters[3]) {
				hexagon = hexagons [Random.Range (0, map.Count)];
				hexagon.addPopulation (-1);
			}
			
		} else
			Parameters [0] = food - Parameters [3];


		updateInterfaceParameters ();

		checkDestructibleCells ();


	}

	void checkDestructibleCells(){
		foreach (HexagonBehavior hex in map.Values) {
			if (destructible (hex)) {
				hex.effectRenderer.sprite = voidSprite;
			} else {
				hex.effectRenderer.sprite = null;
			}
		}
	}
		
	//HexagonProperties

	void Start () {
		dirToVectInit ();
		ConstantBoard.instance.HexagonPropertiesInit ();
		generateMap ( new Vector2(15,5) );
		checkDestructibleCells ();

	}
	
	private float currentScale=0.5f;

	void Update(){
		float delta = 1.5f*Input.GetAxis("Mouse Scroll");
		currentScale = (1 + delta)*currentScale;
		currentScale = Mathf.Min (1f, Mathf.Max (0.2f, currentScale));
		transform.localScale = currentScale*Vector2.one;
	}

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
		// Positions extremes
		foreach (HexagonBehavior hex in map.Values) {
			if (hex.transform.position.x > xMax) {
				xMax = hex.transform.position.x;
			} else if (hex.transform.position.x < xMin) {
				xMin = hex.transform.position.x;
			} else if (hex.transform.position.y > yMax) {
				yMax = hex.transform.position.y;
			} else if (hex.transform.position.y < yMin) {
				yMin = hex.transform.position.y;
			}
		}

	}
		

	public bool destructible (HexagonBehavior hex){
		/*if (hex.coordinates == new Vector2(0,0) )
			return false;*/
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
