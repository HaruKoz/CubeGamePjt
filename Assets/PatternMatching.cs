using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternMatching : MonoBehaviour
{
    [SerializeField]
    private CorrectPattern CorrectPattern;

    [SerializeField]
    private ColliderGenerator ColliderGenerator;

    [SerializeField]
    private JudgePattern JudgePattern;

    private GameObject[] Spheres;
    private SphereHitInfo[] SphereHitInfos;

    //PatternMatchingをインスタンス化して保持
    public static PatternMatching instance;

    //PatternMatchingインスタンスがなければ、このPatternMatchingをインスタンスとする
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
        int sphereCnt = GameObject.FindGameObjectsWithTag("Sphere").Length;
        string sphereName = ColliderGenerator.SphereInstanceName;

        Spheres = new GameObject[sphereCnt];
        SphereHitInfos = new SphereHitInfo[sphereCnt];
        for (int i = 0; i < sphereCnt; i++)
        {
            Spheres[i] = GameObject.Find(sphereName + "(" + i.ToString().PadLeft(2, '0') + ")");
            SphereHitInfos[i] = new SphereHitInfo(Spheres[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string GetWholePattern()
    {
        string ret = null;

        for (int i = 0; i < SphereHitInfos.Length; i++)
        {
            SphereHitInfos[i].Reload();
            ret += JudgePattern.Exec(SphereHitInfos[i].hitObject);
        }

        Debug.Log("GetWholePattern " + ret);
        return ret;
    }

    //照合結果を返す
    public bool GetMatchingResult()
    {
        return GetWholePattern().Equals(CorrectPattern.GetStrCorrectPattern());
    }
}

public class SphereHitInfo
{
    public GameObject sphere;
    public Transform hitObject;

    private SphereColliderEvent SphereColliderEvent;

    public SphereHitInfo(GameObject _sphere)
    {
        this.sphere = _sphere;
        SphereColliderEvent = sphere.GetComponent<SphereColliderEvent>();
    }

    public void Reload()
    {
        this.hitObject = SphereColliderEvent.HitTransform;
    }
}