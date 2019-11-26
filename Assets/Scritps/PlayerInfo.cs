using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class PlayerInfo
{
    public string name;
    public int score;

    

    public PlayerInfo(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public override string ToString()
    {
        return name + " : " +score;
    }
}
