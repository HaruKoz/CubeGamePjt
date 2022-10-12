using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Complete;    //"COMPLETE"と表示する文字（TextMeshPro）

    private void Awake()
    {
        Complete.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowComplete()
    {
        Complete.SetActive(true);
    }
}
