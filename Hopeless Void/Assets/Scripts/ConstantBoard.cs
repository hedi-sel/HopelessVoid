using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantBoard : MonoBehaviour {
	
	public void  HexagonPropertiesInit (){
		//popMax
		popMax.Add (Building.NONE, 10);
		popMax.Add (Building.FACTORY, 20);
		popMax.Add (Building.CAPITALE, 0);
		//popAction
		popAction.Add (Building.NONE, 2);
		popAction.Add (Building.FACTORY, 5);
		popAction.Add (Building.CAPITALE, 0);
		//popConstruction
		popConstruction.Add (Building.NONE, 2);
		popConstruction.Add (Building.FACTORY, 5);
		popConstruction.Add (Building.CAPITALE, 0);
		//effectAction
		effectAction.Add (Building.NONE, new Vector3 (5, 5, 0));
		effectAction.Add (Building.FACTORY, new Vector3 (0, -5, 10));
		effectAction.Add (Building.CAPITALE, new Vector3 (0, 0, 0));
		//effectConstruction
		effectConstruction.Add (Building.NONE, new Vector3 (0, 10, 0));
		effectConstruction.Add (Building.FACTORY, new Vector3 (0, -20, 0));
		effectConstruction.Add (Building.CAPITALE, new Vector3 (0, 0, 0));
		//sprites
		foreach (Sprite sprite in buildingSprites) {
			sprites.Add (sprite.name, sprite);
		}
	}


	public Sprite[] buildingSprites;
	public Sprite[] plainSprites;
	public Sprite[] mountainSprites;

	public static Dictionary<Building,int> popMax = new Dictionary<Building,int> ();
	public static Dictionary<Building,int> popAction = new Dictionary<Building,int> ();
	public static Dictionary<Building,Vector3> effectAction = new Dictionary<Building,Vector3> ();
	public static Dictionary<Building,int> popConstruction = new Dictionary<Building,int> ();
	public static Dictionary<Building,Vector3> effectConstruction = new Dictionary<Building,Vector3> ();
	public static Dictionary<string,Sprite> sprites = new Dictionary<string,Sprite> ();



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
