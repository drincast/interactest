using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;

public class ChatTW : MonoBehaviour
{
    private TcpClient twClient;
    private StreamReader reader;
    private StreamWriter writer;

    public string username, password, channelName;  //https://twitchapps.com/

    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if(twClient.Connected){
            Connect();
        }

        ReadChat();
    }

    private void Connect(){
        twClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twClient.GetStream());
        writer = new StreamWriter(twClient.GetStream());

        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * :" + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.Flush();

    }

    private void ReadChat(){
        if(twClient.Available > 0){
            var message = reader.ReadLine();
            print(message);
            Debug.Log(message);
        }
    }
}
