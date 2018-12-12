﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;

public class socketclient : MonoBehaviour {

	static NetworkClient client;
	//public Transform vrcamera;
	bool cnd = false;

	//public GameObject enemy;

	public GameObject go;
	private CharacterController cc;
	public float speed = 2.0f;

	void OnGUI()
	{
		string ipaddress = Network.player.ipAddress;
		GUI.Box (new Rect(10,Screen.height -50, 100, 50),ipaddress);
		GUI.Label (new Rect (20,Screen.height - 35, 100, 20),"Status:" +client.isConnected);

		/*if (!client.isConnected) {
			if (GUI.Button (new Rect (10, 10, 60, 50), "Connect"))
				Connect ();
		}*/
		if (!cnd) {
			Connect ();
			cnd = true;
		}
	}

	public class PlayerMessage : MessageBase
	{
		public Vector3 forward;
		public Vector3 right;
		public float h;
		public float v;
	}

	public class EnemyMessage : MessageBase
	{
		public Vector3 pos;
		public bool shot;
	}

	public class PlayerUpdateMessage : MessageBase
	{
		public Vector3 position;
		public float speed;
		public int lives;
		public float remain;
	}

	public class PlayerTypeMessage : MessageBase
	{
		public bool type;
	}

	// Use this for initialization
	void Start () {
		client = new NetworkClient ();
		//client.Connect ("192.168.1.3", 25000);
		//enemy.SetActive(false);

		cc = go.GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {
		if (client.isConnected) 
		{
			Debug.Log (go.transform.position);
		}
	}

	void Connect()
	{
		client.Connect ("192.168.1.3", 25000);
		//client.RegisterHandler (MsgType.Connect, OnConnected);
		client.RegisterHandler (888, ClientReceiveMessage);
		//client.RegisterHandler (MyMsgType.Player, ClientReceiveMessage);
		//client.RegisterHandler (MsgType.Connect, ClientReceiveUpdate);


		//PlayerTypeMessage msg = new PlayerTypeMessage();
		//msg.type = true;
		//client.Send(MsgType.Connect, msg);
	}

	void OnConnected(NetworkMessage netMsg)
	{

	}

	private void ClientReceiveMessage(NetworkMessage message)
	{
		PlayerMessage msg = message.ReadMessage<PlayerMessage>();

		Vector3 nu = new Vector3(0f,0f,0f);
		Vector3 forward;
		Vector3 right;
		float h = msg.h;
		float v = msg.v;

		if (h != 0) {
			forward = msg.forward;
			cc.SimpleMove (forward * speed * h);
			Debug.Log (forward);
		}
		if (v != 0) {
			right = msg.right;
			cc.SimpleMove (right * speed * -v);
			Debug.Log (right);
		}
		if (v == 0 && h == 0) {
			cc.SimpleMove (nu);
			Debug.Log (nu);

		}
	}

	private void ClientReceiveUpdate(NetworkMessage message) //lo que recibe al conectarse
	{
		PlayerUpdateMessage msg = message.ReadMessage<PlayerUpdateMessage>();
		go.transform.position = msg.position;
		speed = msg.speed;
	}

}
