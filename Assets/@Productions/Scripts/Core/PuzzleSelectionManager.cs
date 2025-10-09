using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleSelectionManager : MonoBehaviour
{
    [SerializeField] private PuzzleData[] puzzleDatas;
    [SerializeField] private Button[] puzzleSelectionButtons;

    private AsyncOperation preloadOperation;

    private void Start()
    {
        StartCoroutine(PreloadScene());
    }

    private void OnEnable()
    {
        for (int i = 0; i < puzzleDatas.Length; i++)
        {
            int index = i;
            puzzleSelectionButtons[index].image.sprite = puzzleDatas[index].WholeSprite;
            puzzleSelectionButtons[index].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private IEnumerator PreloadScene()
    {
        preloadOperation = SceneManager.LoadSceneAsync(1);
        preloadOperation.allowSceneActivation = false;

        while (preloadOperation.progress < 0.9f)
        {
            Debug.Log($"Loading progress: {preloadOperation.progress * 100}%");
            yield return null;
        }

        Debug.Log("Scene preloaded. Waiting for button press...");
    }

    private void OnButtonClicked(int index)
    {
        if (preloadOperation != null)
        {
            PuzzleSelection.SelectedPuzzleIndex = index;
            preloadOperation.allowSceneActivation = true;
        }
        else
        {
            Debug.LogWarning("Scene not preloaded yet!");
        }
    }
}
