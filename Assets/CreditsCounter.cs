using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsCounter : MonoBehaviour
{
    private Text _text;
    public bool player2;

    private GameController _gameController;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player2)
            _text.text = _gameController.credits2 + "";
        else
        {
            _text.text = _gameController.credits + "";
        }
    }
}
