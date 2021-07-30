using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    private AsyncOperation async;
    void Start()
    {
        StartCoroutine(loading());
    }
    void Update()
    {
        Debug.Log(async.progress * 100);
    }
    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    IEnumerator loading()
    {
        async = SceneManager.LoadSceneAsync("GameScene");

        yield return async;

    }
}
