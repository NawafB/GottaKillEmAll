using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using InControl;

public class MyPlayerManager : MonoBehaviour {

	


	// This example roughly illustrates the proper way to add multiple players from existing
	// devices. Notice how InputManager.Devices is not used and no index into it is taken.
	// Rather a device references are stored in each player and we use InputManager.OnDeviceDetached
	// to know when one is detached.
	//
	// InputManager.Devices should be considered a pool from which devices may be chosen,
	// not a player list. It could contain non-responsive or unsupported controllers, or there could
	// be more connected controllers than your game supports, so that isn't a good strategy.
	//
	// To detect a joining player, we just check the current active device (which is the last
	// device to provide input) for a relevant button press, check that it isn't already assigned
	// to a player, and then create a new player with it.
	//
	// NOTE: Due to how Unity handles joysticks, disconnecting a single device will currently cause
	// all devices to detach, and the remaining ones to reattach. There is no reliable workaround
	// for this issue. As a result, a disconnecting controller essentially resets this example.
	// In a more real world scenario, we might keep the players around and throw up some UI to let
	// users activate controllers and pick their players again before resuming.
	//
	// This example could easily be extended to use bindings. The process would be very similar,
	// just creating a new instance of your action set subclass per player and assigning the
	// device to its Device property.
	//

    public GameObject virusPrefab;
    public GameObject medicPrefab;

    const int maxPlayers = 2;
  

    List<MyPlayer> playersList = new List<MyPlayer>(maxPlayers);



    void Start()
    {
        InputManager.OnDeviceDetached += OnDeviceDetached;
    }


    void Update()
    {
        var inputDevice = InputManager.ActiveDevice;

        if (JoinButtonWasPressedOnDevice(inputDevice))
        {
            if (ThereIsNoPlayerUsingDevice(inputDevice))
            {
                CreatePlayer(inputDevice);
            }
        }
    }


    bool JoinButtonWasPressedOnDevice(InputDevice inputDevice)
    {
        return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
    }


    MyPlayer FindPlayerUsingDevice(InputDevice inputDevice)
    {
        var playerCount = playersList.Count;
        for (var i = 0; i < playerCount; i++)
        {
            var player = playersList[i];
            if (player.Device == inputDevice)
            {
                return player;
            }
        }

        return null;
    }


    bool ThereIsNoPlayerUsingDevice(InputDevice inputDevice)
    {
        return FindPlayerUsingDevice(inputDevice) == null;
    }


    void OnDeviceDetached(InputDevice inputDevice)
    {
        var MyPlayer = FindPlayerUsingDevice(inputDevice);
        if (MyPlayer != null)
        {
            //RemovePlayer( player );
        }
    }

    //can add player entrance here
    MyPlayer CreatePlayer(InputDevice inputDevice)
    {
        if (playersList.Count < maxPlayers)
        {

            GameObject playerPrefab = virusPrefab;
            var playerCount = playersList.Count;

            if (playerCount == 0) {
                playerPrefab = virusPrefab;
               // print(" virusPrefab is playerPrefab");
            }
            else if (playerCount == 1)
            {
                playerPrefab = medicPrefab;
               // print(" medicPrefab is playerPrefab");

            }

            var instPlayer = (GameObject)Instantiate(playerPrefab, new Vector3 (0,1,0), Quaternion.identity);
            var player = instPlayer.GetComponent<MyPlayer>();
            player.Device = inputDevice;
            playersList.Add(player);

            return player;
        }

        return null;
    }


    void RemovePlayer(MyPlayer player)
    {
        playersList.Remove(player);
        player.Device = null;
      //  Destroy(player.gameObject);
    }


    void OnGUI()
    {
        const float h = 22.0f;
        var y = 10.0f;

        GUI.Label(new Rect(10, y, 300, y + h), "Active players: " + playersList.Count + "/" + maxPlayers);
        y += h;

        if (playersList.Count < maxPlayers)
        {
            GUI.Label(new Rect(10, y, 300, y + h), "Press a button to join!");
            y += h;
        }
    }
}

