using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickobject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);		
			ShootRay (ray);
		}*/

	}

	/*void ShootRay(Ray ray){
		RaycastHit rhit;
		bool objectHit = false;
		GameObject gObjectHit = null;

		if (Physics.Raycast (ray, out rhit, 1000.0f)) {
			Debug.Log("Ray Shoot and Hit");
			objectHit = true;
			gObjectHit = rhit.collider.gameObject;
		}

	}*/

}
