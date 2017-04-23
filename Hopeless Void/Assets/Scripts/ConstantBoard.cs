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
		popConstruction.Add (BuildingAction.NONE, 2);
		popConstruction.Add (BuildingAction.FACTORY, 5);
		popConstruction.Add (BuildingAction.CAPITALE, 0);
		//effectAction
		effectAction.Add (BuildingAction.IDLE, new Vector3 (0, 0, 0));
		effectAction.Add (BuildingAction.NONE, new Vector3 (5, 5, 0));
		effectAction.Add (BuildingAction.FACTORY, new Vector3 (0, -5, 10));
		effectAction.Add (BuildingAction.CAPITALE, new Vector3 (0, 0, 0));
		//effectConstruction
		effectConstruction.Add (BuildingAction.NONE, new Vector3 (0, 10, 0));
		effectConstruction.Add (BuildingAction.FACTORY, new Vector3 (0, -20, 0));
		effectConstruction.Add (BuildingAction.CAPITALE, new Vector3 (0, 0, 0));
		//sprites
		foreach (Sprite sprite in buildingSprites) {
			sprites.Add (sprite.name, sprite);
		}
		foreach (Sprite sprite in backgroundSprites) {
			backgrounds.Add (sprite.name, sprite);
		}
	}


	public Sprite[] buildingSprites;
	public Sprite[] backgroundSprites;
	public Sprite[] plainSprites;
	public Sprite[] mountainSprites;

	public static Dictionary<BuildingAction,int> popMax = new Dictionary<BuildingAction,int> ();
	public static Dictionary<BuildingAction,int> popAction = new Dictionary<BuildingAction,int> ();
	public static Dictionary<BuildingAction,int> popConstruction = new Dictionary<BuildingAction,int> ();

	public static Dictionary<BuildingAction,Vector3> effectAction = new Dictionary<BuildingAction,Vector3> ();
	public static Dictionary<BuildingAction,Vector3> effectConstruction = new Dictionary<BuildingAction,Vector3> ();

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
