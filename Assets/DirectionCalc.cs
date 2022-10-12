using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionCalc : MonoBehaviour
{
    // フリック最小移動距離
    [SerializeField]
    private Vector2 FlickMinRange = new Vector2(30.0f, 30.0f);

    // スワイプ最小移動距離
    [SerializeField]
    private Vector2 SwipeMinRange = new Vector2(50.0f, 50.0f);

    // TAPをNONEに戻すまでのカウント
    [SerializeField]
    private int NoneCountMax = 2;
    private int NoneCountNow = 0;

    // スワイプ入力距離
    private Vector2 SwipeRange;

    // 入力方向記録用
    //private Vector3 InputSTART;
    //private Vector2 InputMOVE;
    //private Vector3 InputEND;

    // フリックの方向
    public enum FlickDirection
    {
        NONE,
        TAP,
        UP,
        RIGHT,
        DOWN,
        LEFT,
        VERT_RIGHT,
        VERT_LEFT,
    }

    private FlickDirection NowFlick = FlickDirection.NONE;

    // スワイプの方向
    public enum SwipeDirection
    {
        NONE,
        TAP,
        UP,
        RIGHT,
        DOWN,
        LEFT,
    }

    private SwipeDirection NowSwipe = SwipeDirection.NONE;

    //// テスト用
    ////[SerializeField]
    //public Text _testText_dir; //オブジェクトの向き

    ////[SerializeField]
    //public Text _testText;

    ////[SerializeField]
    //public Text _testText_ptn; //キューブの出ている柄を表示するテキスト

    ////private RayCheck RC;
    //private Vector3 HitNormal;
    //private Vector3 HitPoint;
    //private bool HitFlg = false;    //キューブに触れたかどうかのフラグ

    ////DirectionCalcをインスタンス化して保持
    //public static DirectionCalc instance;

    ////DirectionCalcインスタンスがなければ、このDirectionCalcをインスタンスとする
    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 文字表示処理
        //_testText.text = "Flick:" + GetNowFlick().ToString();

        //_testText.text += "\nSwipe:" + GetNowSwipe().ToString();
        //_testText.text += "\nRange:" + GetSwipeRange();

        //_testText_ptn.text = "Pattern:" + JudgePtn.GetPattern().ToString();
    }

    // 入力内容からフリック方向を計算
    public void FlickCLC(Vector3 InputSTART, Vector3 InputEND, Vector3 HitNormal)
    {
        bool onAxisY = false;　  //Y軸回転があるかどうかのフラグ（XY平面またはYZ平面の場合）
        bool isRight = false;   //法線が右向きの場合
        bool isLeft = false;    //法線が左向きの場合
        Debug.Log("HitNormal = " + HitNormal);

        if (Mathf.Approximately(HitNormal.y, Vector3.up.y)) //XZ平面
        {
            InputSTART.y = InputSTART.z;
            InputEND.y = InputEND.z;
            Debug.Log("HitNormal = up");
        }
        else if (Mathf.Approximately(HitNormal.z, Vector3.back.z)) //XY平面
        {
            onAxisY = true;
            Debug.Log("HitNormal = back");
        }
        else if (Mathf.Approximately(HitNormal.x, Vector3.right.x)) //YZ平面
        {
            InputSTART.x = InputSTART.z;
            InputEND.x = InputEND.z;
            onAxisY = true;
            isRight = true;
            Debug.Log("HitNormal = right");
        }
        else if (Mathf.Approximately(HitNormal.x, Vector3.left.x)) //YZ平面
        {
            InputSTART.x = InputSTART.z;
            InputEND.x = InputEND.z;
            onAxisY = true;
            isLeft = true;
            Debug.Log("HitNormal = left");
        }

        Vector2 _work = new Vector2((new Vector3(InputEND.x, 0, 0) - new Vector3(InputSTART.x, 0, 0)).magnitude, (new Vector3(0, InputEND.y, 0) - new Vector3(0, InputSTART.y, 0)).magnitude);

        //動きが一定以下の場合はただのタップ
        if (_work.x <= FlickMinRange.x && _work.y <= FlickMinRange.y)
        {
            NowFlick = FlickDirection.TAP;
        }
        else if (_work.x > _work.y)
        {
            float _x = Mathf.Sign(InputEND.x - InputSTART.x); //正か0の場合は1を、負の場合は-1を返す

            if (onAxisY)
            {
                if (_x > 0) NowFlick = FlickDirection.VERT_RIGHT;
                else if (_x < 0) NowFlick = FlickDirection.VERT_LEFT;
            }
            else
            {
                if (_x > 0) NowFlick = FlickDirection.RIGHT;
                else if (_x < 0) NowFlick = FlickDirection.LEFT;
            }
        }
        else
        {
            if (onAxisY)
            {
                float _y = Mathf.Sign(InputEND.y - InputSTART.y);
                if (_y > 0)
                {
                    if (isRight) NowFlick = FlickDirection.LEFT;
                    else if (isLeft) NowFlick = FlickDirection.RIGHT;
                    else NowFlick = FlickDirection.UP;
                }
                else if (_y < 0)
                {
                    NowFlick = FlickDirection.NONE;
                }
            }
            else
            {
                float _y = Mathf.Sign(InputEND.y - InputSTART.y);
                if (_y > 0) NowFlick = FlickDirection.UP;
                else if (_y < 0) NowFlick = FlickDirection.DOWN;
            }
        }
    }

    // 入力内容からスワイプ方向を計算
    //private void SwipeCLC()
    //{
    //    SwipeRange = new Vector2((new Vector3(InputMOVE.x, 0, 0) - new Vector3(InputSTART.x, 0, 0)).magnitude, (new Vector3(0, InputMOVE.y, 0) - new Vector3(0, InputSTART.y, 0)).magnitude);

    //    //動きが一定以下の場合はただのタップ
    //    if (SwipeRange.x <= SwipeMinRange.x && SwipeRange.y <= SwipeMinRange.y)
    //    {
    //        NowSwipe = SwipeDirection.TAP;
    //    }
    //    else if (SwipeRange.x > SwipeRange.y)
    //    {
    //        float _x = Mathf.Sign(InputMOVE.x - InputSTART.x);
    //        if (_x > 0) NowSwipe = SwipeDirection.RIGHT;
    //        else if (_x < 0) NowSwipe = SwipeDirection.LEFT;
    //    }
    //    else
    //    {
    //        float _y = Mathf.Sign(InputMOVE.y - InputSTART.y);
    //        if (_y > 0) NowSwipe = SwipeDirection.UP;
    //        else if (_y < 0) NowSwipe = SwipeDirection.DOWN;
    //    }
    //}

    // NONEにリセット
    private void ResetParameter()
    {
        NoneCountNow++;
        if (NoneCountNow >= NoneCountMax)
        {
            NoneCountNow = 0;
            NowFlick = FlickDirection.NONE;
            NowSwipe = SwipeDirection.NONE;
            SwipeRange = new Vector2(0, 0);
        }
    }

    // フリック方向の取得
    public FlickDirection GetNowFlick()
    {
        return NowFlick;
    }

    //// スワイプ方向の取得
    //public SwipeDirection GetNowSwipe()
    //{
    //    return NowSwipe;
    //}

    //// スワイプ量の取得
    //public float GetSwipeRange()
    //{
    //    if (SwipeRange.x > SwipeRange.y)
    //    {
    //        return SwipeRange.x;
    //    }
    //    else
    //    {
    //        return SwipeRange.y;
    //    }
    //}

    // スワイプ量の取得
    //public Vector2 GetSwipeRangeVec()
    //{
    //    if (NowSwipe != SwipeDirection.NONE)
    //    {
    //        return new Vector2(InputMOVE.x - InputSTART.x, InputMOVE.y - InputSTART.y);
    //    }
    //    else
    //    {
    //        return new Vector2(0, 0);
    //    }
    //}
}
