using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Voice.PUN;
using Photon.Pun;
using Photon.Voice.Unity;
using System;


namespace VirtualExpo.MainArea.PlayerUIVoice
{

    public class PlayerVoiceChat : MonoBehaviour
    {

        [SerializeField]
        private Image micImage;

        [SerializeField]
        private Image speakerImage;

        [SerializeField]
        private Image micDisableImage;

        [SerializeField]
        private TMP_Text bufferLagText;

        [SerializeField]
        private PhotonVoiceView photonVoiceView;

        private PhotonView PV;

        [SerializeField]
        private Recorder recorder;

        private void Awake()
        {

            photonVoiceView = this.GetComponent<PhotonVoiceView>();
            recorder = GameObject.FindGameObjectWithTag("VoiceChatManager").GetComponent<Recorder>();
            PV = this.GetComponent<PhotonView>();
            OnStartMicEvent();

        }

        private void Update()
        {

            PushToTalk();
            //OnUpdateMicEvent();

        }

        private void PushToTalk()
        {

            if (PV.IsMine)
            {

                //if user pressed "V" on Keyboard
                //then enable the TransmitEnabled
                if (Input.GetKey(PlayerControllerConstant.ACTIVE_MIC_KEY))
                {

                    recorder.TransmitEnabled = true;
                    OnUpdateMicEvent();
                    this.micDisableImage.enabled = !recorder.TransmitEnabled;

                }
                else
                {

                    recorder.TransmitEnabled = false;
                    this.micDisableImage.enabled = !recorder.TransmitEnabled;

                }

            }
            else
            {

                this.micDisableImage.enabled = false;

            }

        }

        private void OnStartMicEvent()
        {

            this.micImage.enabled = false;
            this.speakerImage.enabled = false;
            this.bufferLagText.enabled = false;
            this.micDisableImage.enabled = false;

        }

        private void OnUpdateMicEvent()
        {

            this.micImage.enabled = this.photonVoiceView.IsRecording;
            this.speakerImage.enabled = this.photonVoiceView.IsSpeaking;

            /*this.bufferLagText.enabled = this.photonVoiceView.IsSpeaking;
            if (this.bufferLagText.enabled)
            {
                this.bufferLagText.text = string.Format("{0}", this.photonVoiceView.SpeakerInUse.Lag);
            }*/

        }

    }


}