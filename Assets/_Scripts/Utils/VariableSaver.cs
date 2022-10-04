using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableSaver : Singleton<VariableSaver>
{
    private DataContainer _data;

    protected override void Awake()
    {
        base.Awake();
        _data = SaveManager.LoadData();
    }

    public int RecordPoints
    {
        get { return _data.recordPoints; }
        set
        {
            _data.recordPoints = value;
            SaveManager.Save(_data);
        }
    }
}
