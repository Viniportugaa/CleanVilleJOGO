using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainStory : MonoBehaviour
{
    [SerializeField] private int Scene;

    public void OnEnable()
    {
        SceneManager.LoadScene(Scene);
    }

}
