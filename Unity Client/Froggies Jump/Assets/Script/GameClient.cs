using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Cinemachine;

using System;

using Colyseus;
using Colyseus.Schema;

using GameDevWare.Serialization;

[Serializable]
class Metadata
{
    public string str;
    public int number;
}

[Serializable]
class CustomRoomAvailable : RoomAvailable
{
	public Metadata metadata;
}

class TypeMessage
{
	public string sessionId;
}

class CustomData
{
	public float velX;
    public float velY;
    public float posX;
    public float posY;
}

class EndOfGame
{
    public string playername;
}

class UsernameClient
{
    public string username;
}

enum MessageType {
	ONE = 0
};

class MessageByEnum
{
	public string str;
}

public class GameClient : GenericSingleton<GameClient>
{
    public string playerSessionId;

    public Transform tFollowTarget;

    public float timeleft;

	// UI Buttons are attached through Unity Inspector
	public Button
		m_ConnectButton,
		m_CreateButton,
		m_JoinOrCreateButton,
		m_JoinButton,
		m_ReconnectButton,
		m_SendMessageButton,
		m_LeaveButton,
		m_GetAvailableRoomsButton;
	public InputField m_EndpointField;
	public Text m_IdText, m_SessionIdText;

    public GameObject playerPrefabs;

	public string roomName = "demo";

    public string emailplayer;

	protected Client client;
	protected Room<State> room;

	protected Room<IndexedDictionary<string, object>> roomFossilDelta;
	protected Room<object> roomNoneSerializer;

	protected IndexedDictionary<string, GameObject> entities = new IndexedDictionary<string, GameObject>();

    GameObject player;

	// Use this for initialization
	void Start () {
		/* Demo UI */
		// m_ConnectButton.onClick.AddListener(ConnectToServer);

		// m_CreateButton.onClick.AddListener(CreateRoom);
		// m_JoinOrCreateButton.onClick.AddListener(JoinOrCreateRoom);
		// m_JoinButton.onClick.AddListener(JoinRoom);
		// m_ReconnectButton.onClick.AddListener(ReconnectRoom);
		// m_SendMessageButton.onClick.AddListener(SendMessage);
		// m_LeaveButton.onClick.AddListener(LeaveRoom);
		// m_GetAvailableRoomsButton.onClick.AddListener(GetAvailableRooms);
	}

    private void FixedUpdate() {
        if (!String.IsNullOrEmpty(playerSessionId))
        {
            if (player == null)
            {
                entities.TryGetValue(playerSessionId, out player);

                if (!player.GetComponent<PlayerController>().enabled)
                {
                    CinemachineVirtualCamera cam = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();

                    tFollowTarget = player.transform;
                    cam.Follow = tFollowTarget;
                    cam.LookAt = tFollowTarget;
                    player.GetComponent<PlayerController>().enabled = true;
                }

                Debug.Log(player.GetComponent<Rigidbody2D>().position);
                // player = entities[en];
            } 
        }
    }

	async public void ConnectToServer (string email, string password)
	{
		/*
		 * Get Colyseus endpoint from InputField
		 */
		string endpoint = "ws://localhost:2567";

	
		Debug.Log("Connecting to " + endpoint);
		/*
		 * Connect into Colyeus Server
		 */
		client = ColyseusManager.Instance.CreateClient(endpoint);
        
        await client.Auth.Login(email, password);
		// var friends = await client.Auth.GetFriends();

        emailplayer = client.Auth.Email;
        Debug.Log(" email : " + client.Auth.Email);
		// Update username
		// client.Auth.Username = "Jake";
		await client.Auth.Save();
	}

	public async void CreateRoom()
	{
		room = await client.Create<State>(roomName, new Dictionary<string, object>() { });
		//roomNoneSerializer = await client.Create("no_state", new Dictionary<string, object>() { });
		//roomFossilDelta = await client.Create<IndexedDictionary<string, object>>("fossildelta", new Dictionary<string, object>() { });
		RegisterRoomHandlers();
	}

	public async void JoinOrCreateRoom()
	{
		room = await client.JoinOrCreate<State>(roomName, new Dictionary<string, object>() { });
		RegisterRoomHandlers();
	}

	public async void JoinRoom ()
	{
		room = await client.Join<State>(roomName, new Dictionary<string, object>() { });
		RegisterRoomHandlers();
	}

	async void ReconnectRoom ()
	{
		string roomId = PlayerPrefs.GetString("roomId");
		string sessionId = PlayerPrefs.GetString("sessionId");
		if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(roomId))
		{
			Debug.Log("Cannot Reconnect without having a roomId and sessionId");
			return;
		}

		room = await client.Reconnect<State>(roomId, sessionId);

		Debug.Log("Reconnected into room successfully.");
		RegisterRoomHandlers();
	}

	public async void RegisterRoomHandlers()
	{
		// m_SessionIdText.text = "sessionId: " + room.SessionId;
        Debug.Log("RegisterRoom Handler");
		room.State.entities.OnAdd += OnEntityAdd;
		room.State.entities.OnRemove += OnEntityRemove;
		room.State.TriggerAll();

		PlayerPrefs.SetString("roomId", room.Id);
		PlayerPrefs.SetString("sessionId", room.SessionId);
		PlayerPrefs.Save();

		room.OnLeave += (code) => Debug.Log("ROOM: ON LEAVE");
		room.OnError += (code, message) => Debug.LogError("ERROR, code =>" + code + ", message => " + message);
		room.OnStateChange += OnStateChangeHandler;

		room.OnMessage((Message message) =>
		{
			Debug.Log("Received Schema message:");
			Debug.Log(message.num + ", " + message.str);
		});

		room.OnMessage<MessageByEnum>((byte) MessageType.ONE, (message) =>
		{
			Debug.Log(">> Received message by enum/number => " + message.str);
		});

		room.OnMessage<TypeMessage>("type", (message) =>
		{
			Debug.Log("Received 'type' message!");
			Debug.Log(message.sessionId);
            playerSessionId = message.sessionId;
		});

        room.OnMessage<EndOfGame>("win", (message) =>
		{
			Debug.Log("Player win " + message.playername );
            FinishLine finishPanel = GameObject.Find("Finish Point").GetComponent<FinishLine>();

            finishPanel.SetWinnerName(message.playername);
		});

        await room.Send("username", new UsernameClient(){
            username = client.Auth.Email
        });

		_ = room.Send((byte)MessageType.ONE, new MessageByEnum { str = "Sending message by enum/number" });
	}


	async void LeaveRoom()
	{
		await room.Leave(false);

		// Destroy player entities
		foreach (KeyValuePair<string, GameObject> entry in entities)
		{
			Destroy(entry.Value);
		}

		entities.Clear();
	}

	async void GetAvailableRooms()
	{
		var roomsAvailable = await client.GetAvailableRooms<CustomRoomAvailable>(roomName);

		Debug.Log("Available rooms (" + roomsAvailable.Length + ")");
		for (var i = 0; i < roomsAvailable.Length; i++)
		{
			Debug.Log("roomId: " + roomsAvailable[i].roomId);
			Debug.Log("maxClients: " + roomsAvailable[i].maxClients);
			Debug.Log("clients: " + roomsAvailable[i].clients);
			Debug.Log("metadata.str: " + roomsAvailable[i].metadata.str);
			Debug.Log("metadata.number: " + roomsAvailable[i].metadata.number);
		}
	}

    public async void UpdatePositionToServer(float posX, float posY) {
        if (room != null)
		{
            await room.Send("move_pos", new CustomData(){
                posX = posX,
                posY = posY
            });
		}
		else
		{
			Debug.Log("Room is not connected!");
		}
    }

    public async void Move(float velX, float velY, float posX, float posY) {
        if (room != null)
		{
            await room.Send("move", new CustomData(){
                velX = velX,
                velY = velY,
                posX = posX,
                posY = posY
            });
		}
		else
		{
			Debug.Log("Room is not connected!");
		}
    }

    public async void WinnerMoveToWaitingRoom(float posX, float posY) 
    {
        if (room != null)
		{
            await room.Send("move_to_waiting_room", new CustomData(){
                posX = posX,
                posY = posY
            });
		}
		else
		{
			Debug.Log("Room is not connected!");
		}
    }

	public async void Idle()
	{
		if (room != null)
		{
            await room.Send("idle");
		}
		else
		{
			Debug.Log("Room is not connected!");
		}
	}
	public async void MoveRight()
	{
		if (room != null)
		{
            await room.Send("move_right");
		}
		else
		{
			Debug.Log("Room is not connected!");
		}
	}

    public async void MoveLeft()
	{
		if (room != null)
		{
            await room.Send("move_left");
		}
		else
		{
			Debug.Log("Room is not connected!");
		}
	}

    public async void MoveJump()
	{
		if (room != null)
		{
            await room.Send("move_jump", new CustomData(){
                velY = 5,
            });
		}
		else
		{
			Debug.Log("Room is not connected!");
		}
	}

	void OnStateChangeHandler (State state, bool isFirstState)
	{
		// Setup room first state
		Debug.Log("State has been updated!");
	}

    public void DumpToConsole(object obj)
    {
        var output = JsonUtility.ToJson(obj, true);
        Debug.Log(output);
    }

	void OnEntityAdd(Entity entity, string key)
	{
        GameObject player = Instantiate(playerPrefabs, new Vector3(entity.posX, entity.posY, entity.posZ), Quaternion.identity);

		Debug.Log("Player add!");
        DumpToConsole(entity);

        // Add "player" to map of players
		entities.Add(entity.sessionId, player);

		// On entity update...
		entity.OnChange += (List<Colyseus.Schema.DataChange> changes) =>
		{
            Vector3 movement = new Vector3(entity.velX * 5, entity.velY, 0f);

            player.transform.position = new Vector3(entity.posX, entity.posY, entity.posZ);

            player.GetComponent<Rigidbody2D>().velocity = movement;


            Debug.Log("Player position on instantiate : " + entity.posX + " posY: " + entity.posY);

            // player.transform.position = new Vector3(entity.posX, entity.posY, entity.posZ);
		};
	}

	void OnEntityRemove(Entity entity, string key)
	{
		GameObject cube;
		entities.TryGetValue(entity.sessionId, out cube);
		Destroy(cube);

		entities.Remove(entity.sessionId);
	}

	void OnApplicationQuit()
    {
        
    }
}
