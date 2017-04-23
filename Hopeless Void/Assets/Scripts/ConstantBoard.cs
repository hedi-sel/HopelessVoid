using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantBoard : MonoBehaviour {
	
	public void  HexagonPropertiesInit (){
		BuildingActionList = new BuildingAction[] { BuildingAction.IDLE, BuildingAction.NONE, BuildingAction.FACTORY };
		//popMax
		popMax.Add (BuildingAction.NONE, 10);
		popMax.Add (BuildingAction.FACTORY, 20);
		popMax.Add (BuildingAction.CAPITALE, 0);
		//popAction
		popAction.Add (BuildingAction.IDLE, 0);
		popAction.Add (BuildingAction.NONE, 2);
		popAction.Add (BuildingAction.FACTORY, 5);
		popAction.Add (BuildingAction.CAPITALE, 0);
		//popConstruction
		popConstruction.Add (BuildingAction.IDLE, 0);
		popConstruction.Add (BuildingAction.NONE, 2);
		popConstruction.Add (BuildingAction.FACTORY, 5);
		popConstruction.Add (BuildingAction.CAPITALE, 0);
		//effectAction
		effectAction.Add (BuildingAction.IDLE, new int[] {0, 0, 0, 0, 0});
		effectAction.Add (BuildingAction.NONE, new int[] {5, 5, 0, 0, 0});
		effectAction.Add (BuildingAction.FACTORY, new int[] {0, -5, 10, 0, 0});
		effectAction.Add (BuildingAction.CAPITALE, new int[] {0, 0, 0, 0, 0});
		//effectConstruction
		effectConstruction.Add (BuildingAction.NONE, new int[] {0, 10, 0, 0, 0});
		effectConstruction.Add (BuildingAction.FACTORY, new int[] {0, -20, 0, 0, 0});
		effectConstruction.Add (BuildingAction.CAPITALE, new int[] {0, 0, 0, 0, 0});
		//sprites
		foreach (Sprite sprite in buildingSprites) {
			sprites.Add (sprite.name, sprite);
		}
		foreach (Sprite sprite in backgroundSprites) {
			backgrounds.Add (sprite.name, sprite);
		}
		//Nom des actions
		nameAction.Add(BuildingAction.IDLE,"Nothing");
		nameAction.Add(BuildingAction.NONE,"Harvesting");
		nameAction.Add(BuildingAction.FACTORY,"Working");
		//Nom des batiments
		nameBuilding.Add(BuildingAction.IDLE,"Mountain");
		nameBuilding.Add(BuildingAction.NONE,"Plaine");
		nameBuilding.Add(BuildingAction.FACTORY,"Working");
		nameBuilding.Add(BuildingAction.CAPITALE,"Working");
		//ID des sprites de batiments
		idBuilding.Add(BuildingAction.IDLE,"Food");
		idBuilding.Add(BuildingAction.NONE,"Food");
		idBuilding.Add(BuildingAction.FACTORY,"Metal");
		idBuilding.Add(BuildingAction.CAPITALE,"Crystal");

	}


	public Sprite[] buildingSprites;
	public Sprite[] backgroundSprites;
	public Sprite[] plainSprites;
	public Sprite[] mountainSprites;

	public static Dictionary<BuildingAction,int> popMax = new Dictionary<BuildingAction,int> ();
	public static Dictionary<BuildingAction,int> popAction = new Dictionary<BuildingAction,int> ();
	public static Dictionary<BuildingAction,int> popConstruction = new Dictionary<BuildingAction,int> ();

	public static Dictionary<BuildingAction,int[]> effectAction = new Dictionary<BuildingAction,int[]> ();
	public static Dictionary<BuildingAction,int[]> effectConstruction = new Dictionary<BuildingAction,int[]> ();

	public static Dictionary<BuildingAction,string> nameAction = new Dictionary<BuildingAction,string> ();
	public static Dictionary<BuildingAction,string> nameBuilding = new Dictionary<BuildingAction,string> ();
	public static Dictionary<BuildingAction,string> idBuilding = new Dictionary<BuildingAction,string> ();

	public static Dictionary<string,Sprite> sprites = new Dictionary<string,Sprite> ();
	public static Dictionary<string,Sprite> backgrounds = new Dictionary<string,Sprite> ();

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
}
