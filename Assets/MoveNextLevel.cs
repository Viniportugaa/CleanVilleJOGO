using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveNextLevel : MonoBehaviour
{
    [SerializeField] private int Scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player"))
        {
            SceneManager.LoadScene(Scene);
        }
    }
}