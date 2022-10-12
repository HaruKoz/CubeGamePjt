using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    //ゲームクリアした回数
    public static int GameClearCnt = 0;

    public enum GAME_STATUS { Play, Clear, Pause };
    public static GAME_STATUS status;

    [SerializeField]
    private ResultManager ResultManager;

    void Start()
    {
        // ステータスをPlayに
        status = GAME_STATUS.Play;
    }

    void Update()
    {
        //ゲームクリア
        if (status == GAME_STATUS.Clear)
        {
            //ゲームクリア処理
            ResultManager.ShowComplete();
            GameClearCnt++;
            //Debug.Log("GameClearCnt = " + GameClearCnt);

            //クリア処理後はポーズ状態にする
            status = GAME_STATUS.Pause;
        }
    }

    //ゲームリセット リセットボタン押下時に呼び出される
    public void ResetGame()
    {
        //Scene1を再読み込みすることでリセット
        SceneManager.LoadScene("Scenes/Scene1");
    }
}
