using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleBehavior : MonoBehaviour {

	public Text fraction;
	public RectTransform bar;

	public void Display(int numerator, int denumerator){
		fraction.text = numerator + " / " + denumerator;
		bar.anchorMax = new Vector2(((float) numerator)/ denumerator,1);
		bar.offsetMax = Vector2.zero;
	}
}
