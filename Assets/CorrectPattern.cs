using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text;

public class CorrectPattern : MonoBehaviour
{
    //正解の文字列
    //GameManagerScriptから指定することにした
    //[SerializeField]
    private string StrCorrectPattern;

    //正解パターンの文字列　クリア回数1までは決め打ちのパターンとする
    //クリア回数2以降はランダムに生成する
    private string[] StrCP = {"000000aaa00aaa00a0a000e00",
                              "000000aaa00aaa00a0a000e00"};
                              //"a000b00a000aaa000a00c000d"};

    //フィールドの一辺の長さ（一辺に並べられるキューブの数）
    private int SideNum;

    //画像表示エリアとして使用するゲームオブジェクト（Image）
    [SerializeField]
    private GameObject ImageArea;

    [SerializeField]
    private GameObject Plane;

    private GameObject[] Images;

    // Start is called before the first frame update
    void Start()
    {
        //パターン画像のプレハブ
        GameObject ImagePrefab = (GameObject)Resources.Load("ImagePrefab");

        //各パターンのspriteを配列に格納
        Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        sprites.Add("a", Resources.Load<Sprite>("a"));
        sprites.Add("b", Resources.Load<Sprite>("b"));
        sprites.Add("c", Resources.Load<Sprite>("c"));
        sprites.Add("d", Resources.Load<Sprite>("d"));
        sprites.Add("e", Resources.Load<Sprite>("e"));
        sprites.Add("f", Resources.Load<Sprite>("f"));
        //sprites.Add("0", null);

        //フィールドの一辺の長さ（一辺に並べられるキューブの数）
        SideNum = Mathf.FloorToInt(Plane.transform.localScale.x * 10.0f);

        //正解パターンの文字列　ゲームクリア回数ごとに変化
        if (GameManagerScript.GameClearCnt < 2)
        {
            //クリア回数１までは決め打ち
            StrCorrectPattern = StrCP[GameManagerScript.GameClearCnt];
        }
        else
        {
            //クリア回数２以降はランダム生成
            StrCorrectPattern = GetRandomPattern(SideNum);
        }

        //画像表示エリアのサイズ
        RectTransform rtImageArea = ImageArea.GetComponent<RectTransform>();

        //画像表示エリアのサイズ
        float imageAreaSize = rtImageArea.sizeDelta.x;

        //画像表示エリアのサイズを一辺に並ぶパターンの数で等分してパターン１つ分のサイズ（一辺の長さ）を決定
        float imageSize = imageAreaSize / (float)SideNum;
        float imageSizeHalf = imageSize / 2.0f;

        Vector3 pos;
        Images = new GameObject[SideNum * SideNum];

        //プレハブのサイズを設定
        RectTransform rtImagePrefab = ImagePrefab.GetComponent<RectTransform>();
        rtImagePrefab.sizeDelta = new Vector2(imageSize, imageSize);

        //プレハブのImageコンポーネント
        Image image = ImagePrefab.GetComponent<Image>();

        //スタート地点　この時点では画像表示エリアは傾いていないことに注意
        pos.x = (imageAreaSize / -2.0f) + imageSizeHalf;
        pos.y = -pos.x;
        pos.z = 0;
        pos = pos + rtImageArea.position;

        int cnt = 0;
        for (int i = 0; i < SideNum; i++)
        {
            pos.x = (imageAreaSize / -2.0f) + imageSizeHalf;
            pos.x = pos.x + rtImageArea.position.x;
            for (int j = 0; j < SideNum; j++)
            {
                //パターン0のときは描画しない
                if (!StrCorrectPattern[cnt].ToString().Equals("0"))
                {
                    image.sprite = sprites[StrCorrectPattern[cnt].ToString()];
                    Images[cnt] = Instantiate(ImagePrefab, pos, Quaternion.identity, rtImageArea);
                    Images[cnt].transform.SetParent(rtImageArea);
                    //Images[cnt].name = SphereInstanceName + "(" + cnt.ToString().PadLeft(2, '0') + ")";
                }
                pos.x += imageSize;
                cnt++;
            }
            pos.y -= imageSize;
        }

        //プレイヤーがお手本（画像表示エリア）とフィールドを見比べやすいように、画像表示エリアを回転
        rtImageArea.Rotate(0.0f, 0.0f, -45.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetStrCorrectPattern()
    {
        return StrCorrectPattern;
    }

    //画像表示エリアに表示するパターン文字列をランダムに生成する
    //int sideNum:一辺に並べられるキューブの最大数
    private string GetRandomPattern(int sideNum)
    {
        //文字列生成に使用する文字
        string chars = "abcdef";

        //フィールドに置けるキューブの最大数
        int cubeNumMax = (sideNum * sideNum);

        //キューブを配置しないマスの数（パターン0で埋めるべき数）を求める
        //=フィールドの縁を一周するように配置したときのキューブの数
        int blankNum = (4 * (sideNum - 1));

        StringBuilder sb = new StringBuilder(cubeNumMax);
        System.Random r = new System.Random();
        for (int i = 0; i < cubeNumMax; i++)
        {
            int pos = r.Next(chars.Length);
            char c = chars[pos];
            sb.Append(c);
        }
        Debug.Log("1 sb.ToString() = " + sb.ToString());

        //重複しないように0で埋めるマスを決め、置換する
        int rNum = r.Next(cubeNumMax - 1);
        for (int i = 0; i < blankNum; i++)
        {
            while (sb[rNum] == '0')
            {   //重複しない場所が見つかるまで
                rNum = r.Next(cubeNumMax - 1);
            }
            sb[rNum] = '0';

        }
        Debug.Log("2 sb.ToString() = " + sb.ToString());

        return sb.ToString();
    }
}
