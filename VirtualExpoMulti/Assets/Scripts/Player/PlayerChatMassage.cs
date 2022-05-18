using System;
using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

using ChatListDump;

namespace VirtualExpo.Player.UIChat
{

    /// Color Note : 
    /// - Skyblue : Connection Granted (#87ceeb)
    /// - NeonGreen : Joined room / Joined to master (#39ff14)
    /// - Orange : left room / Disconnected (#FFA500)
    /// - NeonViolet : Joining Room Scene (#B026FF)
    /// - NeonYellow : Ping Connection (#FFF01F)
    /// - Snow : RoomUpdate

    public class PlayerChatMassage : MonoBehaviourPunCallbacks
    {

        #region Variable

        #region Public Variable
        [Header("UI")]
        public GameObject chatInputUI;
        public GameObject bubleSpeachUI;
        public TMP_InputField InputFieldChat;   
        public TMP_Text updatedText;

        [Header("Chat Message List")]
        public TextMeshProUGUI channelChatText;
        public int maxMessages = 15;

        #endregion

        #region Private Variable
        [Space(5)]
        [Header("Private and Internal Variables")]
        //private PlayerMovement playerMovement;
        private PhotonView pv;
        private string newMessage = "";

        [SerializeField]
        internal string userName { get; private set; }
        internal bool isActived { get; private set; }
        [SerializeField]
        internal bool isEnterSend = false;

        private float buildDelay = 0f;
        //[SerializeField]
        //private List<string> allMessages = new List<string>();
        private ChatListManager getChatListManager;

        #endregion

        #endregion

        #region Unity Methods

        #region Unity Core Methods

        private void Awake()
        {

            chatInputUI.SetActive(false);

            //playerMovement = this.GetComponent<PlayerMovement>();
            getChatListManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ChatListManager>();
            pv = GetComponent<PhotonView>();

            bubleSpeachUI.SetActive(false);

        }

        private void Update()
        {

            ChatController();

            UpdateChatMessageContent();

        }

        #endregion

        #region Input Action

        private void ChatController()
        {

            if (pv.IsMine)
            {

                if (Input.GetKeyDown(PlayerControllerConstant.ACTIVE_CHAT_MESSAGE_KEY))
                {

                    isActived = !isActived;

                }

                chatInputUI.SetActive(isActived);
                InputFieldChat.ActivateInputField();

                if (isActived)
                {

                    switch (isEnterSend)
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

        #endregion

        public void OnClickSend()
        {

            newMessage = InputFieldChat.text;

            if (newMessage == "")
            {
                InputFieldChat.ActivateInputField();
                return;
            }
            else
            {

                //call method send chat message Pun RPC
                pv.RPC("SendChatMessage", RpcTarget.AllBuffered, newMessage);

                //call method that send chat to public list message
                userName = PhotonNetwork.NickName;
                string newMsg = userName + " : " + newMessage;
                pv.RPC("RPCAddNewMessage", RpcTarget.AllBufferedViaServer, newMsg);

                //focus to input field and empty the input field
                InputFieldChat.ActivateInputField();
                InputFieldChat.text = "";

            }

        }

        [PunRPC]
        private void SendChatMessage(string inputLine)
        {

            updatedText.text = inputLine;

            bubleSpeachUI.SetActive(true);

            StartCoroutine(RemoveText());

        }

        IEnumerator RemoveText()
        {
            yield return new WaitForSeconds(4f);
            bubleSpeachUI.SetActive(false);
        }


        [PunRPC]
        void RPCAddNewMessage(string msg)
        {

            getChatListManager.messages.Add(msg);

        }

        void UpdateChatMessageContent()
        {

            if (PhotonNetwork.InRoom && PhotonNetwork.IsConnectedAndReady)
            {

                channelChatText.maxVisibleLines = maxMessages;

                if (getChatListManager.messages.Count > maxMessages)
                {
                    getChatListManager.messages.RemoveAt(0);
                }

                BuildTextContents();

            }
            else if (getChatListManager.messages.Count > 0)
            {
                getChatListManager.messages.Clear();
                channelChatText.text = "";
            }

        }

        void BuildTextContents()
        {

            string newTextContents = "";
            foreach (string msg in getChatListManager.messages)
            {
                newTextContents += msg + "\n";
            }

            channelChatText.text = newTextContents;

        }

        #endregion


        #region IPunObservable implementation


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

            if (stream.IsWriting)
            {
                stream.SendNext(bubleSpeachUI.active);
            }
            else if(stream.IsReading)
            {
                bubleSpeachUI.SetActive((bool)stream.ReceiveNext());
            }

        }


        #endregion

    }

}
