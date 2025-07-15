using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public bool chave_fase2;
    public bool chave_fase3;
    [SerializeField] private GameObject fase2;
    [SerializeField] private GameObject fase3;
    public static DontDestroy instance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { }
}
