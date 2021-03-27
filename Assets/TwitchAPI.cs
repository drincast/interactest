using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TwitchLib.Unity;
using TwitchLib.Api.Core.Models.Undocumented.Chatters;
using TwitchLib.Client.Models;

public class TwitchAPI : MonoBehaviour
{
    public Api api;
    public Client client;
    private string channel_name = "drincastx";

    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
        api = new Api();
        api.Settings.AccessToken = Secret.bot_access_token;
        api.Settings.ClientId = Secret.client_id;

        // ConnectionCredentials credentials = new ConnectionCredentials("scolibot", Secret.bot_access_token);
        // client = new Client();
        // client.Initialize(credentials, channel_name);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            api.Invoke(api.Undocumented.GetChattersAsync(channel_name), GetChatterListCallback);
        }
    }

    private void GetChatterListCallback(List<ChatterFormatted> listOfChartter)
    {
        Debug.Log("List of " + listOfChartter.Count + " Viewers");
        foreach (var item in listOfChartter){
            Debug.Log(string.Format("usuario: {0}", item.Username));
        }        
    }
}
