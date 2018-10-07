using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LanguageHelper : MonoBehaviour {


    #region Variables

    [Header("Options")]
    public Text soundText;
    public Text languageText;
    public Text backButtonText;

    [Header("StartMenuButtons")]
    public Text playButtonText;
    public Text optionButtonText;
    public Text quitkButtonText;
    #endregion



    public Image langBttnImg;
    public Sprite[] flags; // изменения флага на кнопке
    private string json; // названия файла
    public static Lang lng = new Lang(); // статика для доступа из всего когда

    //LanguageHelper languageHelper;

    private int langIndex = 1;
    private string[] langArray = { "ru_RU", "en_EN" }; // массив языков


    private void Awake()
    {

        if (!PlayerPrefs.HasKey("Language")) // проверка системы на язык
        {

            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");

            }
            else
            {
                PlayerPrefs.SetString("Language", "en_EN");
            }
        }
        LangLoad();
    }

    private void Start()
    {
        //languageHelper = GetComponent<LanguageHelper>();


        for (int i = 0; i < langArray.Length; i++)
        {
            if (PlayerPrefs.GetString("Language") == langArray[i])
            {
                langIndex = i + 1;
                langBttnImg.sprite = flags[i];
            }
        }
    }

    public void SwithButton()// Смена картинки на кнопке событие Серый не тупи
    {
        if (langIndex != langArray.Length)
        {
            langIndex++;
        }
        else
        {
            langIndex = 1;
        }
        PlayerPrefs.SetString("Language", langArray[langIndex - 1]);
        langBttnImg.sprite = flags[langIndex - 1];

        if (PlayerPrefs.GetString("Musik") != "off")
        {
            GameObject.Find("ClickAudio").GetComponent<AudioSource>().Play();
        }

        LangLoad();
    }

    public void LangLoad()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, "/lang_values/" + PlayerPrefs.GetString("Language") + ".json");
        WWW reader = new WWW(path);

        while(!reader.isDone){ }
        json = reader.text;

#endif
#if UNITY_EDITOR
        json = File.ReadAllText(Application.streamingAssetsPath + "/lang_values/" + PlayerPrefs.GetString("Language") + ".json");
        lng = JsonUtility.FromJson<Lang>(json);
#endif

        #region MainMenu and Settings
        soundText.text = lng.settingsSoundText;
        languageText.text = lng.settingsLanguageText;
        backButtonText.text = lng.backButtonSettingsText;

        playButtonText.text = lng.mainMenuStartButtonText;
        optionButtonText.text = lng.mainMenuOptionButtonText;
        quitkButtonText.text = lng.mainMenuQuitButtonText;

    #endregion


}

}

[SerializeField]
public class Lang
{
    [Header("Options")]
    public string settingsLanguageText;
    public string settingsSoundText;
    public string backButtonSettingsText;

    [Header("MainMenu")]
    public string mainMenuStartButtonText;
    public string mainMenuOptionButtonText;
    public string mainMenuQuitButtonText;

}
