using System.Collections;
using System.Collections.Generic;

using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using System;

public class socketserver : MonoBehaviour {

	//CrossPlatformInputManager.VirtualAxis m_HVAxis;
	//CrossPlatformInputManager.VirtualAxis m_VVAxis;
	//string horizontalAxisName = "Horizontal";
	//string verticalAxisName = "Vertical";

	public GameObject go;
	public GameObject ball;
	private CharacterController cc;	

	public class MyMsgType {
		public static short Player = MsgType.Highest + 1;
	};

	void OnGUI()
	{
		string ipaddress = Network.player.ipAddress;
		GUI.Box (new Rect(10,Screen.height -50, 100, 50),ipaddress);
		GUI.Label (new Rect (20,Screen.height - 35, 100, 20),"Status:" +NetworkServer.active);
		GUI.Label (new Rect (20,Screen.height - 20, 100, 20),"Connected:" +NetworkServer.connections.Count);
	}

	public class PlayerMessage : MessageBase
	{
		public Vector3 forward;
		public Vector3 right;
		public float h;
		public float v;
	}

	// Use this for initialization
	void Start () {

		NetworkServer.Listen (25000);
		//NetworkServer.RegisterHandler (888, ServerReceiveMessage);
		NetworkServer.RegisterHandler (MyMsgType.Player, ServerReceiveMessage);

		cc = go.GetComponent<CharacterController> ();
	}

	private void ServerReceiveMessage(NetworkMessage message)
	{
		/*StringMessage msg = new StringMessage ();
		msg.value = message.ReadMessage<StringMessage> ().value;
		string[] deltas = msg.value.Split ('|');
		//Debug.Log (deltas [0] + ' ' + deltas [1]);
		go.transform.position += new Vector3 (Convert.ToSingle(deltas[0])*0.1f, 0f, Convert.ToSingle(deltas[1])*0.1f);*/

		PlayerMessage msg = message.ReadMessage<PlayerMessage>();

		Vector3 nu = new Vector3(0f,0f,0f);
		Vector3 forward;
		Vector3 right;
		float h = msg.h;
		float v = msg.v;

		if (v != 0) {
			forward = msg.forward;
			cc.SimpleMove (forward * 3f * v);
			Debug.Log (forward);
		}
		if (h != 0) {
			right = msg.right;
			cc.SimpleMove (right * 3f * h);
			Debug.Log (right);
		}
		if (v == 0 && h == 0) {
			cc.SimpleMove (nu);
			Debug.Log (nu);

		}

	}

	// Update is called once per frame
	void Update () {
		if(NetworkServer.connections.Count > 0){
			go.SetActive (true);
		}
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);		
			ShootRay (ray);
		}
	}

	void ShootRay(Ray ray){
		RaycastHit rhit;
		bool objectHit = false;
		GameObject gObjectHit = null;

		if (Physics.Raycast (ray, out rhit, 1000.0f)) {
			Debug.Log("Ray Shoot and Hit");
			objectHit = true;
			if (objectHit == true) {
				StringMessage msg = new StringMessage ();
				msg.value = "1";
				NetworkServer.SendToAll(888,msg);

			}
				
		}

	}
}
