using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Level : MonoBehaviour
{
    private Button levelBtn;

    public int levelReq;
    void Start()
    {
        levelBtn = GetComponent<Button>();
        if (PlayerPrefs.GetInt("Level", 1) >= levelReq)
            levelBtn.onClick.AddListener(() => LoadLevel());
        else
            GetComponent<CanvasGroup>().alpha = .5f;
    }

    // Update is called once per frame
    void LoadLevel()
    {
        SceneManager.LoadScene(gameObject.name);
    }
}
