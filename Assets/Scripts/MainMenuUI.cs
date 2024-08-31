using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// メインメニューのUIを管理するクラス
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] RankingManager rankingManager;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject rankingCanvas;

    void Start()
    {
        // カーソルを表示、カーソル操作を可能にする
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnClickRankingButton()
    {
        mainMenuCanvas.SetActive(false);
        rankingCanvas.SetActive(true);
        rankingManager.OnClickGetMessagesApi();
    }
}
