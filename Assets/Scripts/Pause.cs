using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas; // Pause Canvasを格納するためのフィールド
    private bool isPaused = false; // ゲームが一時停止中かどうかを示すフラグ

    void Start()
    {
        // ゲーム開始時にはPause Canvasを非表示にする
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // エスケープキーが押されたとき
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // ゲームを再開する
            }
            else
            {
                PauseGame(); // ゲームを一時停止する
            }
        }
    }

    // ゲームを一時停止するメソッド
    void PauseGame()
    {
        Time.timeScale = 0f; // 時間を停止
        if (pauseCanvas != null)
        {
            // カーソルを表示、カーソル操作を可能にする
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            pauseCanvas.SetActive(true); // Pause Canvasを表示
        }
        isPaused = true; // 一時停止フラグを立てる
    }

    // ゲームを再開するメソッド
    public void ResumeGame()
    {
        Time.timeScale = 1f; // 時間を再開
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(false); // Pause Canvasを非表示
        }
        isPaused = false; // 一時停止フラグを解除
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }
}
