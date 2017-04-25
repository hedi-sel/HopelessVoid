using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBarBehavior : MonoBehaviour {

	public PeopleBarBehavior people;
	private BoxCollider2D box;
	private RectTransform rect;

	public int peopleMax;
	public bool hovered;

	private ActionHolderBehavior actionHolder;

	[HideInInspector]
	public HexagonBehavior hexagon;

	public void Awake(){
		rect = GetComponent<RectTransform> ();
		box = GetComponent<BoxCollider2D> ();
		actionHolder = GetComponentInChildren<ActionHolderBehavior> ();
	}

	public void Off(){
		gameObject.SetActive (false);
	}

	public void Open(HexagonBehavior _hexagon){
		hexagon = _hexagon;
		peopleMax = hexagon.getMaxPopulation ();
		people.warning.SetActive (hexagon.Warned());
		Refresh ();
		actionHolder.Close ();
		actionHolder.SetActions( _hexagon.getActionPlanelList ());
	}

	public void Change(){
		peopleMax = hexagon.getMaxPopulation ();
		Refresh ();
	}

	public void Update(){
		box.size = new Vector2(rect.rect.width,rect.rect.height);
	}

	void OnMouseEnter(){
		hovered = true;
	}

	void OnMouseExit(){
		hovered = false;
	}

	public void Mod (int i){
		bool t = hexagon.addPopulation(i);
		Refresh ();
	}

	private void Refresh(){
		people.Refresh (hexagon.population, peopleMax);
		hexagon.populationOnHexagon.SetPeople (hexagon.population);
	}
}
