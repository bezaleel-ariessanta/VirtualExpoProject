using System;
using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

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

        #endregion

        #region Private Variable
        [Space(5)]
        [Header("Private and Internal Variables")]
        private PlayerMovement playerMovement;
        private PhotonView pv;
        private string newMessage = "";

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

            chatInputUI.SetActive(false);

            playerMovement = this.GetComponent<PlayerMovement>();
            pv = this.GetComponent<PhotonView>();

            bubleSpeachUI.SetActive(false);

        }

        private void Update()
        {

            ChatController();

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
                return;
            }
            else
            {

                //call method send chat message Pun RPC
                pv.RPC("SendChatMessage", RpcTarget.AllBuffered, newMessage);
                InputFieldChat.text = "";

            }

        }

        [PunRPC]
        private void SendChatMessage(string inputLine)
        {

            //show in local chat log
            updatedText.text = "<color=#39ff14>" + userName + " : </color>" + inputLine;

            bubleSpeachUI.SetActive(true);

            StartCoroutine(RemoveText());

        }

        IEnumerator RemoveText()
        {
            yield return new WaitForSeconds(4f);
            bubleSpeachUI.SetActive(false);
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
