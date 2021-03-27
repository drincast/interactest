using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TwitchLib.Client.Models;
using TwitchLib.Unity;

public class TwitchClient : MonoBehaviour
{
    public Client client;
    private string channel_name = "drincastx";


    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;

        ConnectionCredentials credentials = new ConnectionCredentials("scolibot", Secret.bot_access_token);
        client = new Client();
        client.Initialize(credentials, channel_name);

        client.OnMessageReceived += MyMessageReceivedFunction;
        client.OnConnected += Client_OnConnected;

        client.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            client.SendMessage(client.JoinedChannels[0], "Hola guapos !!");
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            client.SendMessage(client.JoinedChannels[0], "te lo dijo la fenBot");
        }
    }

    private void MyMessageReceivedFunction(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
    {
        string sayBot = string.Format("The bot just read a message in chat of {0} [{1}]", e.ChatMessage.Username, e.ChatMessage.Message);
        Debug.Log(sayBot);

        if(e.ChatMessage.Message.Contains("linda"))
            client.SendMessage(client.JoinedChannels[0], "gracias pensare en ti por 3 minutos ;)");
            //client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "gracias pensare ne ti por 30 minute timeout!");


    }

    // private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
    // {
    //     if (e.ChatMessage.Message.Contains("badword"))
    //     client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
    // }

    private void Client_OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
    {
        Debug.Log($"Connected to {e.AutoJoinChannel}");
        client.SendMessage(client.JoinedChannels[0], "Hi, I am Scoli, a fenbot ;)");
    }
}
