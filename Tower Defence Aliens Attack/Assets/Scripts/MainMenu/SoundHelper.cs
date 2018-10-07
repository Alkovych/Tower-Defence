using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundHelper : MonoBehaviour {

    public Image soundBttnImg;
    public Sprite[] flags; // изменения флага на кнопке
    private int soundIndex = 1;
    private string[] soundArray = { "on", "off" }; // массив языков

    private void Start()
    {



        for (int i = 0; i < soundArray.Length; i++)
        {
            if (PlayerPrefs.GetString("Musik") == soundArray[i])
            {
                soundIndex = i + 1;
                soundBttnImg.sprite = flags[i];
            }
        }
    }

    public void SwithButton()// Смена картинки на кнопке событие Серый не тупи
    {
        if (soundIndex != soundArray.Length)
        {
            soundIndex++;
        }
        else
        {
            soundIndex = 1;
        }
        PlayerPrefs.SetString("Musik", soundArray[soundIndex - 1]);
        soundBttnImg.sprite = flags[soundIndex - 1];

        IsSoundActive();
    }

    private void IsSoundActive()
    {
        if (PlayerPrefs.GetString("Musik") != "off")
        {
            GameObject.Find("ClickAudio").GetComponent<AudioSource>().Play();
        }
    }

    private void Awake()
    {
        IsSoundActive();
    }
}
