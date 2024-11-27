using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    private void Start()
    {
        _bestScoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void ToPlay()
    {
        SceneManager.LoadScene(1);
    }
}
