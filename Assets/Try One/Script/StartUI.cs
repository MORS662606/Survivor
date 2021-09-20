using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UserNamespace;

public class StartUI : MonoBehaviour
{
    private AsyncOperation async;
    private const string BagListPath = "D:/BagData.xml";
    private const string ItemInfoPath = "D:/Dictionary.xml";
    public void GameStart()
    {
        StartCoroutine(Loading());    
        Debug.Log(async.progress * 100);
    }
    IEnumerator Loading()
    {
        UserFunction.GameLoad(BagListPath, ItemInfoPath);
        async = SceneManager.LoadSceneAsync("GameScene");
        yield return async;
    }
}
