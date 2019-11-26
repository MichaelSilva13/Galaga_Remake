using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameCursor : MonoBehaviour
{
    public Text[] texts;


    private int index;

    public char[] _name = new []{'A', 'A', 'A'};

    public AudioSource _audio;


    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void MoveCursor(int x)
    {
        if (x > 0)
        {
            index = (index + 1) % texts.Length;
        }else if (x < 0)
        {
            index--;
            if (index < 0) index = texts.Length - 1;
        }
        _audio.Play();
        RectTransform letterRect = texts[index].rectTransform;
        GetComponent<RectTransform>().position = new Vector3(texts[index].transform.position.x, transform.position.y);
    }

    public void ChangeLetter(int y)
    {
        char CurrentChar = texts[index].text.ToCharArray()[0];
        if (y > 0) CurrentChar++;
        else if (y < 0) CurrentChar--;
        if (CurrentChar > 'Z') CurrentChar = 'A';
        if (CurrentChar < 'A') CurrentChar = 'Z';
        _audio.Play();


        texts[index].text = CurrentChar + "";
        _name[index] = CurrentChar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
