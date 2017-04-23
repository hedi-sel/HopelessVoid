using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarBehavior : MonoBehaviour {

	public Text food;
	public Text metal;
	public Text crystal;
	public Text people;
	public CapsuleBehavior capsule;

	public void SetFood(int value){
		food.text = value.ToString();
	}
		
	public void SetMetal(int value){
		metal.text = value.ToString();
	}
		
	public void SetCrystal(int value){
		crystal.text = value.ToString();
	}

	public void SetPeople(int value, int outOf){
		people.text = value + " / " + outOf;
	}

	public void SetCapsule(int value, int outOf){
		capsule.Display (value, outOf);
	}
}
