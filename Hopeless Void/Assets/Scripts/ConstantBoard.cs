using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantBoard : MonoBehaviour {
	
	public void  HexagonPropertiesInit (){
		BuildingActionList = new BuildingAction[] { BuildingAction.IDLE, BuildingAction.NONE, BuildingAction.FACTORY};
		//popAction
		popAction.Add (BuildingAction.IDLE, 10);
		popAction.Add (BuildingAction.NONE, 1);
		popAction.Add (BuildingAction.ENERGY, 2);
		popAction.Add (BuildingAction.FACTORY, 5);
		popAction.Add (BuildingAction.CAPITALE, 0);
		//popConstruction
		popConstruction.Add (BuildingAction.IDLE, 0);
		popConstruction.Add (BuildingAction.NONE, 2);
		popConstruction.Add (BuildingAction.FACTORY, 5);
		popConstruction.Add (BuildingAction.CAPITALE, 0);
		//effectAction
		effectAction.Add (BuildingAction.IDLE, new int[] {0, 3, 0, 0, 0});
		effectAction.Add (BuildingAction.NONE, new int[] {3, 0, 0, 0, 0});
		effectAction.Add (BuildingAction.ENERGY, new int[] {0, 5, 0, 0, 0});
		effectAction.Add (BuildingAction.FACTORY, new int[] {0, -5, 0, 10, 0});
		effectAction.Add (BuildingAction.CAPITALE, new int[] {0, 0, 0, 0, 0});
		//effectConstruction
		effectConstruction.Add (BuildingAction.IDLE, new int[] {0, 0, 0, 0, 0});
		effectConstruction.Add (BuildingAction.NONE, new int[] {0, 10, 0, 0, 0});
		effectConstruction.Add (BuildingAction.FACTORY, new int[] {0, -20, -10, 0, 0});
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
		nameAction.Add(BuildingAction.IDLE,"Nothing");
		nameAction.Add(BuildingAction.NONE,"Harvest");
		nameAction.Add(BuildingAction.FACTORY,"Working");
		//Nom des batiments
		nameBuilding.Add(BuildingAction.IDLE,"Mountain");
		nameBuilding.Add(BuildingAction.NONE,"Plain");
		nameBuilding.Add(BuildingAction.FACTORY,"Factory");
		nameBuilding.Add(BuildingAction.CAPITALE,"Capital");
		//ID des sprites de batiments
		idBuilding.Add(BuildingAction.NONE,"Food");
		idBuilding.Add(BuildingAction.FACTORY,"Metal");
		idBuilding.Add(BuildingAction.CAPITALE,"Crystal");

		GameBoard.instance.updateInterfaceParameters ();

	}


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
}
