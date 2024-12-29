using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameOver.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void GameO()
    {
        gameOver.gameObject.SetActive(true);
    }
}
