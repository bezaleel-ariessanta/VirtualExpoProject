using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif
using Photon.Chat;
using Photon.Realtime;
using AuthenticationValues = Photon.Chat.AuthenticationValues;
using ExitGames.Client.Photon;


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

        #region Variable

        #region Public Variable
        [Header("UI")]
        public ChatClient chatClient;
        public GameObject chatUIGameObject;
        public TMP_InputField InputFieldChat;   // set in inspector
        public TMP_Text CurrentChannelText;     // set in inspector

        [Header("Public Variables")]
        public int maximumMessages; // set in inspector

        #endregion

        #region Private Variable
        [Space(5)]
        [Header("Private and Internal Variables")]
        private PhotonView pv;
        private string newMessage = "";

        [SerializeField]
        private List<string> _messages = new List<string>();

        [SerializeField]
        internal string userName { get; private set; }
        internal bool isActived { get; private set; }
        [SerializeField]
        internal bool isEnterSend = false;
        

        #endregion

        #endregion

        #region Unity Methods

        #region Unity Core Methods

        private void Awake()
        {

            this.chatUIGameObject.SetActive(false);

            //photon chat connection
            this.chatClient = new ChatClient(this);
            if (PhotonNetwork.IsConnectedAndReady)
            {
                userName = PhotonNetwork.LocalPlayer.NickName;
            }
            ConnectToPhotonChat();

            if (pv == null)
            {

                this.pv = this.GetComponent<PhotonView>();
            }

        }

        private void Update()
        {
            this.chatClient.Service();// make sure to call this regularly! it limits effort internally, so calling often is ok!

            ChatController();

        }

        #endregion

        void ConnectToPhotonChat()
        {

            //making connection to photon server
            Debug.Log("<color=#FFF01F> Connecting to Photon Chat </color>");
            chatClient.AuthValues = new Photon.Chat.AuthenticationValues(userName);
            chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(userName));

        }

        private void ChatController()
        {

            if (Input.GetKeyDown(PlayerControllerConstant.ACTIVE_CHAT_MESSAGE_KEY))
            {

                isActived = !isActived;

            }

            this.chatUIGameObject.SetActive(isActived);

            if (isActived)
            {

                switch (this.isEnterSend)
                {

                    case true:
                        OnEnterSend();
                        break;

                    case false:
                        OnShiftEnterSend();
                        break;

                }

            }

        }

        //Send Message by pressing Enter
        private void OnEnterSend()
        {

            if (Input.GetKey(PlayerControllerConstant.SEND_THE_MESSAGE_ENTER))
            {

                OnClickSend();

            }

        }

        //Send Message by pressing Shift + Enter
        private void OnShiftEnterSend()
        {

            if (Input.GetKey(PlayerControllerConstant.LEFT_SHIFT) && Input.GetKey(PlayerControllerConstant.SEND_THE_MESSAGE_ENTER))
            {

                OnClickSend();

            }

        }

        public void OnClickSend()
        {

            newMessage = this.InputFieldChat.text;

            if (newMessage == "")
            {
                return;
            }
            else
            {

                SendChatMessage(newMessage);
                this.InputFieldChat.text = "";

            }

        }

        private void SendChatMessage(string inputLine)
        {

            //show in local chat log
            CurrentChannelText.text += "\n" + userName + " : " + inputLine;
            _messages.Add(newMessage);
            if (_messages.Count >= maximumMessages)
            {
                _messages.Remove(_messages[0]);
            }

            //send the new message to network

        }


        #endregion

        #region IChatClientListener implementation

        void IChatClientListener.DebugReturn(DebugLevel level, string message)
        {
            
        }

        void IChatClientListener.OnDisconnected()
        {
            Debug.Log("<color=#FFA500>Disconnected To Photon Chat</color>");
        }

        void IChatClientListener.OnConnected()
        {
            Debug.Log("<color=#87ceeb>Connected To Photon Chat</color>");
        }

        void IChatClientListener.OnChatStateChange(ChatState state)
        {
            
        }

        void IChatClientListener.OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            
        }

        void IChatClientListener.OnPrivateMessage(string sender, object message, string channelName)
        {
            
        }

        void IChatClientListener.OnSubscribed(string[] channels, bool[] results)
        {
            
        }

        void IChatClientListener.OnUnsubscribed(string[] channels)
        {
            
        }

        void IChatClientListener.OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            
        }

        void IChatClientListener.OnUserSubscribed(string channel, string user)
        {
            
        }

        void IChatClientListener.OnUserUnsubscribed(string channel, string user)
        {
            
        }


        #endregion

    }

}
