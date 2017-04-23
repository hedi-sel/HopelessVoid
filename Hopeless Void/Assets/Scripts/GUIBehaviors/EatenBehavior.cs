using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatenBehavior : MonoBehaviour {

	private SpriteRenderer img1;
	private SpriteRenderer img2;

	void Awake(){
		StartCoroutine (Disapear ());
		img1 = GetComponentInParent<SpriteRenderer> ();
		Destroy(GetComponentInParent<HexagonBehavior> ());
		img2 = GetComponent<SpriteRenderer> ();
	}

	IEnumerator Disapear(){
		yield return new WaitForSecondsRealtime (1.5f);
		Destroy (transform.parent.gameObject);
		yield break;
	}
}
