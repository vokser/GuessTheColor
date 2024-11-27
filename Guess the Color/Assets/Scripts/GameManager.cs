using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _colorBlockPrefab;
    [SerializeField] private Vector3[] _blocksPositions;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameAudioManager _gameAudioManager;
    
    private GameObject[] blocks; 
    private GameObject correctBlock; 
    private Color mainColor; 
    private int score = 0; 
    private bool isGameOver = false;

    private void Start()
    {
        blocks = new GameObject[_blocksPositions.Length];
        InitializeBlocks();
        UpdateColors();
    }

    private void Update()
    {
        if (isGameOver) 
            return;
    }

    private void InitializeBlocks()
    {
        for (int i = 0; i < _blocksPositions.Length; i++)
        {
            blocks[i] = Instantiate(_colorBlockPrefab, _blocksPositions[i], Quaternion.identity);
            int index = i;
            blocks[i].AddComponent<BoxCollider>(); 
            blocks[i].GetComponent<Renderer>().material = new Material(Shader.Find("Standard"));
            blocks[i].AddComponent<Block>().Setup(this, index);
        }
    }

    private void UpdateColors()
    {
        mainColor = new Color(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
        int correctIndex = Random.Range(0, _blocksPositions.Length);
        
        Renderer mainRenderer = GetComponent<Renderer>();
        if(mainRenderer != null)
        {
            mainRenderer.material.color = mainColor;
        }

        for (int i = 0; i < blocks.Length; i++)
        {
            if (i == correctIndex)
            {
                blocks[i].GetComponent<Renderer>().material.color = mainColor;
                correctBlock = blocks[i];
            }
            else
            {
                blocks[i].GetComponent<Renderer>().material.color = GenerateSimilarColor(mainColor);
            }
        }
    }

    private Color GenerateSimilarColor(Color baseColor)
    {

        float rColor = Mathf.Clamp(Random.Range(-GetDifficultyFactor(), GetDifficultyFactor()), 0, 1);
        float gColor = Mathf.Clamp(Random.Range(-GetDifficultyFactor(), GetDifficultyFactor()), 0, 1);
        float bColor = Mathf.Clamp(Random.Range(-GetDifficultyFactor(), GetDifficultyFactor()), 0, 1);

        return new Color(
            Mathf.Clamp(baseColor.r + rColor, 0, 1),
            Mathf.Clamp(baseColor.g + gColor, 0, 1),
            Mathf.Clamp(baseColor.b + bColor, 0, 1)
        );
    }

    private float GetDifficultyFactor()
    {
        if (score < 5)
            return 1f;
        if (score > 5)
            return 0.8f;
        if (score > 10)
            return 0.4f;
        return 0.2f;
    }

    public void OnBlockClicked(GameObject clickedBlock)
    {
        if (isGameOver) 
            return;
        

        if (clickedBlock == correctBlock)
        {
            score++;
            _scoreText.text = score.ToString();
            _gameAudioManager.PlayWinSound();
            UpdateColors();
        }
        else
        {
            isGameOver = true;
            _gameAudioManager.PlayLoseSound();
            GameOver();
        }
    }

    private void GameOver()
    {
        _gameOverPanel.SetActive(true);
        if (PlayerPrefs.GetInt("Score") < score)
            PlayerPrefs.SetInt("Score", score);
    }

    private void ResetGame()
    {
        isGameOver = false;
        _gameOverPanel.SetActive(false);
        score = 0;
        _scoreText.text = score.ToString();
        UpdateColors();
    }
    private void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

public class Block : MonoBehaviour
{
    private GameManager gameController;
    private int blockIndex;   

    public void Setup(GameManager controller, int index)
    {
        gameController = controller;
        blockIndex = index;
    }

    private void OnMouseDown()
    {
        gameController.OnBlockClicked(gameObject);
    }
}
