using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMonsterScriptHelper : MonoBehaviour
{

    [Header("Panels")]
    public GameObject MonsterPanel;
    public GameObject ButtonPanel;
    public GameObject OprionPanel;

    [Header("Text")]
    public Text HeroNames;
    public Text HeroDefence;
    public Text HeroHP;

    [Header("Image")]
    public Image HeroImage;

    [Header("Arrays")]
    private string[] HeroNameArray = { "Monty Neville" , "Finn Odonnell", "Everly Hansen" };
    private readonly int[] HeroHpArray = { 4, 3, 2 };
    private readonly int[] HeroDefenceArray = { 1, 2, 3 };
    public Sprite[] HeroImagesArray;
    public AudioClip[] audioSources;

    [Header("Variables")]
    private AudioSource audioSource;
    int index;

    // Изменение героя , а так эе показателей в главном меню
    private void HeroMainMenu()
    {
        index = Random.Range(0, HeroImagesArray.Length);

        HeroNames.text = HeroNameArray[index].ToString();
        HeroDefence.text = HeroDefenceArray[index].ToString();
        HeroHP.text = HeroHpArray[index].ToString();
        HeroImage.sprite = HeroImagesArray[index];

    }



    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        HeroMainMenu();
    }

    // Смена звуков при нажатии на имя героя в главном меню
    public void CallAudio()
    {

        audioSource.clip = audioSources[index];
        audioSource.Play();
        
    }

    public void OptionsActive()
    {
        IsSoundActive();
        MonsterPanel.SetActive(false);
        ButtonPanel.SetActive(false);
        OprionPanel.SetActive(true);
        OprionPanel.GetComponent<Animator>().SetTrigger("OptionsUp");


    }

    public void BackButtonOptions()
    {
        IsSoundActive();
        MonsterPanel.SetActive(true);
        ButtonPanel.SetActive(true);
        OprionPanel.SetActive(false);
    }

    public void Quit()
    {
        IsSoundActive();
        Application.Quit();
    }



    private void IsSoundActive()
    {
        if (PlayerPrefs.GetString("Musik") != "off")
        {
            GameObject.Find("ClickAudio").GetComponent<AudioSource>().Play();
        }
    }
}
