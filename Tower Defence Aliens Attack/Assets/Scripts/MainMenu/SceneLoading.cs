using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour {

    [Header("Loading Scenes")]
    public int sceneID;

    [Header("Other objects")]
    public Image loadingImage;
    public Text progressText;

    void Start () {
        StartCoroutine(AsyncLoad());
	}
     

    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID); // загружаем сцену используя ID сцены .. Для многоразоваго использования.
        while(!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingImage.fillAmount = progress;
            progressText.text = string.Format("{0:0}%",progress*100);
            yield return null;
        }

    }
	
}
