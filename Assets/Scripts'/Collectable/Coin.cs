using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    public Transform _target;

    private void Awake()
    {
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            CoinManager.instance.IncreaseCoins(value);

        }
    }

    public void Move()
    {
        transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
