using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// シーン遷移を管理するクラス
public class SceneLoader : MonoBehaviour
{
    BgmManager bgmManager;

    void Start()
    {
        bgmManager = FindObjectOfType<BgmManager>();
    }

    // ゲームシーンをロードするメソッド
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
        ScoreManager.Instance.ResetScore();
        Time.timeScale = 1f;
    }

    // メインメニューシーンをロードするメソッド
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
        ScoreManager.Instance.ResetScore();
        Time.timeScale = 1f;
    }

    // チュートリアルシーンをロードするメソッド
    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
        ScoreManager.Instance.ResetScore();
        Time.timeScale = 1f;
    }

    // アプリを終了するメソッド
    public void QuitGame()
    {
        Application.Quit();
    }
}
