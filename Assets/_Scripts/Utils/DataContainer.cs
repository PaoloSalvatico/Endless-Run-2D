using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DataContainer
{
    private int _recordPoints;

    public int RecordPoints { get => _recordPoints; set => _recordPoints = value; }
}
