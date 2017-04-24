using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantBoard : MonoBehaviour {
	
	public void  HexagonPropertiesInit (){
		popAction = new Dictionary<BuildingAction,int> ();
		popConstruction = new Dictionary<BuildingAction,int> ();

		effectAction = new Dictionary<BuildingAction,int[]> ();
		effectConstruction = new Dictionary<BuildingAction,int[]> ();

		nameAction = new Dictionary<BuildingAction,string> ();
		nameBuilding = new Dictionary<BuildingAction,string> ();
		idBackground = new Dictionary<BuildingAction,string> ();
		idBuilding = new Dictionary<BuildingAction,string> ();

		sprites = new Dictionary<string,Sprite> ();
		backgrounds = new Dictionary<string,Sprite> ();
		ressources = new Dictionary<string,Sprite> ();

		BuildingActionList = new BuildingAction[] { /*BuildingAction.IDLE,*/ BuildingAction.NONE, BuildingAction.FACTORY};
		//World Size
		Vector2 worldSize = new Vector2(12,4);
		//popAction
		popAction.Add (BuildingAction.IDLE, 10);
		popAction.Add (BuildingAction.NONE, 1);
		popAction.Add (BuildingAction.ENERGY, 2);
		popAction.Add (BuildingAction.FACTORY, 3);
		popAction.Add (BuildingAction.CAPITALE, 0);
		//popConstruction
		popConstruction.Add (BuildingAction.IDLE, 1);
		popConstruction.Add (BuildingAction.NONE, 1);
		popConstruction.Add (BuildingAction.FACTORY, 5);
		popConstruction.Add (BuildingAction.CAPITALE, 0);
		//effectAction
		effectAction.Add (BuildingAction.IDLE, new int[] {0, 2, 0, 0, 0});
		effectAction.Add (BuildingAction.NONE, new int[] {2, 0, 0, 0, 0});
		effectAction.Add (BuildingAction.ENERGY, new int[] {0, 0, 1, 0, 0});
		effectAction.Add (BuildingAction.FACTORY, new int[] {0, -3, -1, 0, 1});
		effectAction.Add (BuildingAction.CAPITALE, new int[] {0, 0, 0, 0, 0});
		//effectConstruction
		effectConstruction.Add (BuildingAction.IDLE, new int[] {0, 0, 0, 0, 0});
		effectConstruction.Add (BuildingAction.NONE, new int[] {0, 10, 0, 0, 0});
		effectConstruction.Add (BuildingAction.FACTORY, new int[] {0, -10, -2, 0, 0});
		effectConstruction.Add (BuildingAction.CAPITALE, new int[] {0, 0, 0, 0, 0});
		//sprites
		foreach (Sprite sprite in buildingSprites) {
			sprites.Add (sprite.name, sprite);
		}
		foreach (Sprite sprite in backgroundSprites) {
			backgrounds.Add (sprite.name, sprite);
		}
		foreach (Sprite sprite in ressourceSprites) {
			ressources.Add (sprite.name, sprite);
		}
		//Nom des actions
		nameAction.Add(BuildingAction.IDLE,"Harvest montain");
		nameAction.Add(BuildingAction.NONE,"Harvest field");
		nameAction.Add(BuildingAction.FACTORY,"Working");
		nameAction.Add(BuildingAction.ENERGY,"Harvest crystal");
		//Nom des batiments
		nameBuilding.Add(BuildingAction.IDLE,"Mountain");
		nameBuilding.Add(BuildingAction.NONE,"Field");
		nameBuilding.Add(BuildingAction.FACTORY,"Factory");
		nameBuilding.Add(BuildingAction.CAPITALE,"Capital");
		nameBuilding.Add(BuildingAction.ENERGY,"Void");
		//ID des sprites de batiments
		idBuilding.Add(BuildingAction.NONE,"Hexagon_Plain_1");
		idBuilding.Add(BuildingAction.FACTORY,"Built");
		idBuilding.Add(BuildingAction.CAPITALE,"Factory");

		idBackground.Add(BuildingAction.NONE,"Food");
		idBackground.Add(BuildingAction.FACTORY,"Metal");
		idBackground.Add(BuildingAction.ENERGY,"Cristal");

		GameBoard.instance.updateInterfaceParameters ();

	}

	public Vector2 worldSize;
	public Sprite[] buildingSprites;
	public Sprite[] backgroundSprites;
	public Sprite[] ressourceSprites;
	public Sprite[] plainSprites;
	public Sprite[] mountainSprites;

	public static Dictionary<BuildingAction,int> popAction = new Dictionary<BuildingAction,int> ();
	public static Dictionary<BuildingAction,int> popConstruction = new Dictionary<BuildingAction,int> ();

	public static Dictionary<BuildingAction,int[]> effectAction = new Dictionary<BuildingAction,int[]> ();
	public static Dictionary<BuildingAction,int[]> effectConstruction = new Dictionary<BuildingAction,int[]> ();

	public static Dictionary<BuildingAction,string> nameAction = new Dictionary<BuildingAction,string> ();
	public static Dictionary<BuildingAction,string> nameBuilding = new Dictionary<BuildingAction,string> ();
	public static Dictionary<BuildingAction,string> idBackground = new Dictionary<BuildingAction,string> ();
	public static Dictionary<BuildingAction,string> idBuilding = new Dictionary<BuildingAction,string> ();

	public static Dictionary<string,Sprite> sprites = new Dictionary<string,Sprite> ();
	public static Dictionary<string,Sprite> backgrounds = new Dictionary<string,Sprite> ();
	public static Dictionary<string,Sprite> ressources = new Dictionary<string,Sprite> ();

	public static BuildingAction[] BuildingActionList;

	// Update is called once per frame

	static private ConstantBoard m_Instance;
	static public ConstantBoard instance { get { return m_Instance; } }
	void Awake(){
		if (m_Instance != null) {
			Destroy (this);
		} else {
			m_Instance = this;
		}
	}

	public static void DeleteInstance(){
		m_Instance = null;
	}
}
