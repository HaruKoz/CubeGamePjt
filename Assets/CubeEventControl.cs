using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeEventControl : MonoBehaviour
{
    // テスト用　インスタンスに参照を渡す用
    [SerializeField]
    public Text testText_dir; //オブジェクトの向き

    [SerializeField]
    private Text testText;

    [SerializeField]
    private Text testText_ptn; //キューブの出ている柄を表示するテキスト

    //[SerializeField]
    //private ResultManager ResultManager;

    private MoveCube NowMoveCube;

    //CubeEventControlをインスタンス化して保持
    public static CubeEventControl instance;

    //CubeEventControlインスタンスがなければ、このCubeEventControlをインスタンスとする
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NowMoveCube != null)
        {
            if (!NowMoveCube.IsRotate()) //キューブの回転が止まったとき
            {
                NowMoveCube = null;
                if (PatternMatching.instance.GetMatchingResult())
                {
                    GameManagerScript.status = GameManagerScript.GAME_STATUS.Clear;
                    //GameClear();
                }
            }
        }
    }

    public void EventListener(MoveCube moveCube)
    {
        if (NowMoveCube == null)
        {
            NowMoveCube = moveCube;
        }

        if (NowMoveCube.IsRotate()) //対象のキューブが回転中のとき
        {
            //return false;
        }
        else
        {
            testText.text = "Flick:" + NowMoveCube.FlickDir.ToString();
            //_testText.text += "\nSwipe:" + Input.GetNowSwipe().ToString();
            //_testText.text += "\nRange:" + Input.GetSwipeRange();

            //_testText_dir.text = "Direction_UP:" + transform.up;
            //_testText_dir.text += "\nDirection_FW:" + transform.forward;
            //_testText_dir.text += "\nDirection_RG:" + transform.right;

            //_testText_ptn.text = "Pattern:" + JudgePtn.GetPattern().ToString();

            NowMoveCube.Exec();
            //return true;
        }
    }

    //void GameClear()
    //{
        //Debug.Log("GameClear");
        //ResultManager.ShowComplete();
    //}
}
