using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgePattern : MonoBehaviour
{
    //[SerializeField]
    //private GameObject cube;

    // キューブの柄
    public enum CubePattern
    {
        a, b, c, d, e, f,
    }
    private CubePattern NowPattern = CubePattern.a;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Exec(Transform transform)
    {
        if (transform == null)
        {
            return "0";
        }
        else
        {
            return Judge(transform).ToString();
        }
    }

    // キューブの柄を判定する
    private CubePattern Judge(Transform transform)
    {
        //キューブの白い面（キューブのニュートラル正面）とy軸の内積
        float dot_white_up = Vector3.Dot(transform.forward, Vector3.up);

        //キューブのニュートラル上面とy軸の内積
        float dot_up_up = Vector3.Dot(transform.up, Vector3.up);

        //キューブのニュートラル正面とz軸の内積
        float dot_fw_fw = Vector3.Dot(transform.forward, Vector3.forward);

        //キューブのニュートラル正面とx軸の内積
        float dot_fw_r = Vector3.Dot(transform.forward, Vector3.right);

        //Debug.Log("dot_white_up = " + dot_white_up);
        //Debug.Log("dot_up_up = " + dot_up_up);
        //Debug.Log("dot_fw_fw = " + dot_fw_fw);
        //Debug.Log("dot_fw_r = " + dot_fw_r);

        if (dot_white_up > 0.99f)   //白い面が出ている場合
        {
            NowPattern = CubePattern.e;
        }
        else if (dot_white_up < -0.99f) //赤い面が出ている場合
        {
            NowPattern = CubePattern.f;
        }
        else　//白と赤のハーフの面が出ている場合
        {
            //キューブのニュートラル上面または下面が出ている場合
            if (dot_up_up > 0.99f || dot_up_up < -0.99f)
            {
                if (dot_fw_fw > 0.99f)
                {
                    NowPattern = CubePattern.a;
                }
                else if (dot_fw_fw < -0.99f) 
                {
                    NowPattern = CubePattern.c;
                }
                else if (dot_fw_r > 0.99f)
                {
                    NowPattern = CubePattern.d;
                }
                else if (dot_fw_r < -0.99f) 
                {
                    NowPattern = CubePattern.b;
                }
            }
            else //キューブのニュートラル右面または左面が出ている場合
            {
                if (dot_fw_fw > 0.99f)
                {
                    NowPattern = CubePattern.b;
                }
                else if (dot_fw_fw < -0.99f)
                {
                    NowPattern = CubePattern.d;
                }
                else if (dot_fw_r > 0.99f)
                {
                    NowPattern = CubePattern.a;
                }
                else if (dot_fw_r < -0.99f)
                {
                    NowPattern = CubePattern.c;
                }
            }
        }
        return NowPattern;
    }
}
