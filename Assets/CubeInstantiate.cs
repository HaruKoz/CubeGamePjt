using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(DirectionCalc))]
public class CubeInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject CubePrefab;

    [SerializeField]
    public int CubeQuantity = 9;   //キューブの個数

    //CubeInstantiateをインスタンス化して保持
    public static CubeInstantiate instance;

    //CubeInstantiateインスタンスがなければ、このCubeInstantiateをインスタンスとする
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
        // ゲームオブジェクトを生成します。
        GameObject cube;
        Vector3 pos = Vector3.zero;
        float sqrt = Mathf.Sqrt(CubeQuantity);
        int floor = Mathf.FloorToInt(sqrt / 2.0f);

        for (int i = 0; i < CubeQuantity; i++)
        {
            cube = Instantiate(CubePrefab, pos, Quaternion.identity);
            if (pos.x < floor)
            {
                pos.x += 1.0f;
            }
            else
            {
                pos.x *= -1.0f;
                if (pos.z < floor)
                {
                    pos.z += 1.0f;
                }
                else
                {
                    pos.z *= -1.0f;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
