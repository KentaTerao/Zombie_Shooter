using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingUI : MonoBehaviour
{
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject rankingCanvas;

    void Start()
    {
        // カーソルを表示、カーソル操作を可能にする
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnClickBackButton()
    {
        rankingCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
