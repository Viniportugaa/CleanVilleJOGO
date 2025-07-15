using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssencialObjects : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
