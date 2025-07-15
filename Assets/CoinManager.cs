using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public Text coinText;
    private int coinatual;
    [SerializeField]  private int coinLevel;
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private GameObject door3;
    [SerializeField] private GameObject door4;
    [SerializeField] private GameObject door5;
    private bool doorDestroyed;
    public int currentCoins = 0;

    public static CoinManager instance;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IncreaseCoins(int v)
    {
        currentCoins += v;
    }

    // Update is called once per frame
    void Update()
    {
        coinatual = coinLevel - currentCoins;
        coinText.text = "colete todo o lixo! falta: " + coinatual.ToString();

        if(currentCoins >= coinLevel && !doorDestroyed)
        {
            doorDestroyed = true;
            Destroy(door1);
            Destroy(door2);
            Destroy(door3);
            Destroy(door4);
            Destroy(door5);

        }
    }
}
