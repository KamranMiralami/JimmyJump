using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuHandlerScript : MonoBehaviour
{
    [SerializeField] int maxLevel;
    [SerializeField] Sprite[] levelImages;
    [SerializeField] Image[] uiImages;
    [SerializeField] Image[] uiBorders;
    [SerializeField] TextMeshProUGUI[] uiTexts;
    [SerializeField] float animationDuration = 0.1f;
    [SerializeField] Button playButton;
    int index = 0;
    int level = 0;
    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("currentLevel");
        if(level >= maxLevel)
        {
            level = maxLevel - 1;
        }
        if (levelImages.Length > level)
        {
            uiImages[index].sprite = levelImages[level];
        }
        HighScores highScores = new HighScores();
        highScores.names = new string[] { "me", "you", "them" };
        highScores.scores = new int[] { 1, 2, 3 };
        PlayerPrefs.SetString("scores", JsonUtility.ToJson(highScores));
        uiTexts[0].text = "" + (level + 1);
        playButton.interactable = true;
    }

    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Play()
    {
        SceneManager.LoadScene("Level" + (level + 1));
    }

    public void Next()
    {
        level = (level + 1) % maxLevel;
        uiTexts[1 - index].text = "" + (level + 1);
        if(PlayerPrefs.GetInt("currentLevel") < level)
        {
            playButton.interactable = false;
        }
        else
        {
            playButton.interactable = true;
        }
        Vector3 temp = uiTexts[1 - index].rectTransform.anchoredPosition3D;
        temp.x = uiTexts[index].rectTransform.sizeDelta.x;
        uiTexts[1 - index].rectTransform.anchoredPosition3D = temp;
        temp = uiBorders[1 - index].rectTransform.anchoredPosition3D;
        temp.x = 900;
        uiBorders[1 - index].rectTransform.anchoredPosition3D = temp;
        if (levelImages.Length > level)
        {
            uiImages[1 - index].sprite = levelImages[level];
        }
        StartCoroutine(nextLevel(animationDuration));
        index = 1 - index;
    }

    IEnumerator nextLevel(float duration)
    {
        float t = 0f;
        float initialText0 = uiTexts[0].rectTransform.anchoredPosition3D.x;
        float destText0 = initialText0 - uiTexts[0].rectTransform.sizeDelta.x;
        float initialText1 = uiTexts[1].rectTransform.anchoredPosition3D.x;
        float destText1 = initialText1 - uiTexts[1].rectTransform.sizeDelta.x;
        float initial0 = uiBorders[0].rectTransform.anchoredPosition3D.x;
        float dest0 = initial0 - 900;
        float initial1 = uiBorders[1].rectTransform.anchoredPosition3D.x;
        float dest1 = initial1 - 900;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            Vector3 temp = uiBorders[0].rectTransform.anchoredPosition3D;
            temp.x = Mathf.Lerp(initial0, dest0, t);
            uiBorders[0].rectTransform.anchoredPosition3D = temp;
            temp = uiBorders[1].rectTransform.anchoredPosition3D;
            temp.x = Mathf.Lerp(initial1, dest1, t);
            uiBorders[1].rectTransform.anchoredPosition3D = temp;
            temp = uiTexts[0].rectTransform.anchoredPosition3D;
            temp.x = Mathf.Lerp(initialText0, destText0, t);
            uiTexts[0].rectTransform.anchoredPosition3D = temp;
            temp = uiTexts[1].rectTransform.anchoredPosition3D;
            temp.x = Mathf.Lerp(initialText1, destText1, t);
            uiTexts[1].rectTransform.anchoredPosition3D = temp;
            yield return null;
        }
    }

    public void Previous()
    {
        level = (level - 1) % maxLevel;
        if (level < 0)
        {
            level += maxLevel;
        }
        uiTexts[1 - index].text = "" + (level + 1);
        if (PlayerPrefs.GetInt("currentLevel") < level)
        {
            playButton.interactable = false;
        }
        else
        {
            playButton.interactable = true;
        }
        Vector3 temp = uiTexts[1 - index].rectTransform.anchoredPosition3D;
        temp.x = -1 * uiTexts[index].rectTransform.sizeDelta.x;
        uiTexts[1 - index].rectTransform.anchoredPosition3D = temp;
        temp = uiBorders[1 - index].rectTransform.anchoredPosition3D;
        temp.x = -1 * 900;
        uiBorders[1 - index].rectTransform.anchoredPosition3D = temp;
        if (levelImages.Length > level)
        {
            uiImages[1 - index].sprite = levelImages[level];
        }
        StartCoroutine(previousLevel(animationDuration));
        index = 1 - index;
    }

    IEnumerator previousLevel(float duration)
    {
        float t = 0f;
        float initialText0 = uiTexts[0].rectTransform.anchoredPosition3D.x;
        float destText0 = initialText0 + uiTexts[0].rectTransform.sizeDelta.x;
        float initialText1 = uiTexts[1].rectTransform.anchoredPosition3D.x;
        float destText1 = initialText1 + uiTexts[1].rectTransform.sizeDelta.x;
        float initial0 = uiBorders[0].rectTransform.anchoredPosition3D.x;
        float dest0 = initial0 + 900;
        float initial1 = uiBorders[1].rectTransform.anchoredPosition3D.x;
        float dest1 = initial1 + 900;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            Vector3 temp = uiBorders[0].rectTransform.anchoredPosition3D;
            temp.x = Mathf.Lerp(initial0, dest0, t);
            uiBorders[0].rectTransform.anchoredPosition3D = temp;
            temp = uiBorders[1].rectTransform.anchoredPosition3D;
            temp.x = Mathf.Lerp(initial1, dest1, t);
            uiBorders[1].rectTransform.anchoredPosition3D = temp;
            temp = uiTexts[0].rectTransform.anchoredPosition3D;
            temp.x = Mathf.Lerp(initialText0, destText0, t);
            uiTexts[0].rectTransform.anchoredPosition3D = temp;
            temp = uiTexts[1].rectTransform.anchoredPosition3D;
            temp.x = Mathf.Lerp(initialText1, destText1, t);
            uiTexts[1].rectTransform.anchoredPosition3D = temp;
            yield return null;
        }
    }

    public void LoadHighScores()
    {
        SceneManager.LoadScene("HighScores");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
