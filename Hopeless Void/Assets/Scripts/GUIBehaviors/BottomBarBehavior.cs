using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBarBehavior : MonoBehaviour {

	public PeopleBarBehavior people;
	private BoxCollider2D box;
	private RectTransform rect;

	public int peopleMax;
	public bool hovered;

	public HexagonBehavior hexagon;

	public void Awake(){
		rect = GetComponent<RectTransform> ();
		box = GetComponent<BoxCollider2D> ();
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
		people.Refresh (hexagon.population, peopleMax);
	}
}
