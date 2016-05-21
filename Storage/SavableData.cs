using UnityEngine;
using System.Collections;
using System;

[Serializable]
public abstract class SavableData {
    //public bool HasData = false;


    public abstract bool HasData();
    public abstract void PrepareDataAfterLoad();
    public abstract void PrepareDataForSave();
}
