﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text text;
	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
        text.text = ScoreKeeper.totalScore.ToString();
        ScoreKeeper.Reset();
	}
	
}