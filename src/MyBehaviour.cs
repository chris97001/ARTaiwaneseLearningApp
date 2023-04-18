using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class MyBehaviour : MonoBehaviour
{
    public AudioSource source;
    public OptionController optionController;
    
    void Start()
    {
        if(optionController == null)
            optionController = GameObject.Find("Option").GetComponent<OptionController>();
    }

    public void OnClick1()
    {
        string data = optionController.str1_ch;
        string url = $"http://140.116.245.157:6000/taiwanese_tts/{data}";
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        StartCoroutine(MP3PlayToGoogle(url,AudioType.WAV));
    }

    public void OnClick2()
    {
        string data = optionController.str2_ch;
        string url = $"http://140.116.245.157:6000/taiwanese_tts/{data}";
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        StartCoroutine(MP3PlayToGoogle(url,AudioType.WAV));
    }

    IEnumerator MP3PlayToGoogle(string url, AudioType audioType)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                try
                {
                    AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);

                    source.clip = myClip;
                    source.Play();
                }
                catch(Exception ex)
                {
                    Debug.Log("0..0");
                }               
            }
        }
    }
}