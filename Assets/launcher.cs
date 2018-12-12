using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcher : MonoBehaviour {

	public GameObject shooter;
	GameCo gc;
	public Vector3 spawn; 

	// Use this for initialization
	void Start () {
		gc = shooter.GetComponent<GameCo> ();
		spawn = gc.spawns;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void shoot()
	{
		gc.spawnwave ();
	}
}
