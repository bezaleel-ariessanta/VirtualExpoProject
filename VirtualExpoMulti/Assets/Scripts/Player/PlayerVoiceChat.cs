using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Voice.PUN;
using Photon.Pun;
using Photon.Voice.Unity;
using System;


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

    private void Awake()
    {

        photonVoiceView = this.GetComponent<PhotonVoiceView>();
        OnStartMicEvent();

    }

    private void Update()
    {

        OnUpdateMicEvent();

    }

    private void OnStartMicEvent()
    {

        this.micImage.enabled = false;
        this.speakerImage.enabled = false;
        this.micDisableImage.enabled = true;

    }

    private void OnUpdateMicEvent()
    {

        this.micImage.enabled = this.photonVoiceView.IsRecording;
        this.speakerImage.enabled = this.photonVoiceView.IsSpeaking;

        this.bufferLagText.enabled = this.photonVoiceView.IsSpeaking;
        if (this.bufferLagText.enabled)
        {
            this.bufferLagText.text = string.Format("{0}", this.photonVoiceView.SpeakerInUse.Lag);
        }

    }

}
