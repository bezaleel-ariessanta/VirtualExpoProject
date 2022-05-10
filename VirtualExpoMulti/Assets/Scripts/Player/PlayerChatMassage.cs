using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


using Photon.Chat;
using Photon.Realtime;
using AuthenticationValues = Photon.Chat.AuthenticationValues;
#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif

namespace VirtualExpo.Player.UIChat
{

    /// Color Note : 
    /// - Skyblue : Connection Granted (#87ceeb)
    /// - NeonGreen : Joined room / Joined to master (#39ff14)
    /// - Orange : left room / Disconnected (#FFA500)
    /// - NeonViolet : Joining Room Scene (#B026FF)
    /// - NeonYellow : Ping Connection (#FFF01F)
    /// - Snow : RoomUpdate

    public class PlayerChatMassage : MonoBehaviour, IChatClientListener
    {

        #region IChatClientListener implementation

        public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
        {
            throw new System.NotImplementedException();
        }

        public void OnDisconnected()
        {
            throw new System.NotImplementedException();
        }

        public void OnConnected()
        {
            throw new System.NotImplementedException();
        }

        public void OnChatStateChange(ChatState state)
        {
            throw new System.NotImplementedException();
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            throw new System.NotImplementedException();
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            throw new System.NotImplementedException();
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            throw new System.NotImplementedException();
        }

        public void OnUnsubscribed(string[] channels)
        {
            throw new System.NotImplementedException();
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            throw new System.NotImplementedException();
        }

        void IChatClientListener.OnUserSubscribed(string channel, string user)
        {
            throw new NotImplementedException();
        }

        void IChatClientListener.OnUserUnsubscribed(string channel, string user)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
