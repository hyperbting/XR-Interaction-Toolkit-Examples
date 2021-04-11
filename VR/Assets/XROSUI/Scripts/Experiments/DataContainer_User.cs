﻿using System;
using UnityEngine;

[Serializable]
public class DataContainer_User 
{
    public float height;
    public float LarmLength;
    public float RarmLength;
    public float Lshoulderx;
    public float Rshoulderx;
    public float Lshouldery;
    public float Rshouldery;
    public float chestWidth;
    public float Lelbowy;
    public float Relbowy;
    public float Lkneey;
    public float Rkneey;
    public string data;
    public string JSONdata;
    
    public override string ToString()
    {
        data = "\n" + this.height + "," +
               this.LarmLength + "," +
               this.RarmLength + "," +
               this.Lshoulderx + "," +
               this.Lshouldery + "," +
               this.Rshoulderx + "," +
               this.Rshouldery + "," +
               this.chestWidth + "," +
               this.Lelbowy + "," +
               this.Relbowy + "," +
               this.Lkneey + "," +
               this.Rkneey;
        return data;
    }
    private static string _headerString;
    public static string HeaderToString()
    {
        if (_headerString == null)
        {
            _headerString = nameof(DataContainer_User.height) + "," +
                            nameof(DataContainer_User.LarmLength) + "," +
                            nameof(DataContainer_User.RarmLength) + "," +
                            nameof(DataContainer_User.Lshoulderx) + "," +
                            nameof(DataContainer_User.Lshouldery) + "," +
                            nameof(DataContainer_User.Rshoulderx) + "," +
                            nameof(DataContainer_User.Rshouldery) + "," +
                            nameof(DataContainer_User.chestWidth) + "," +
                            nameof(DataContainer_User.Lelbowy) + "," +
                            nameof(DataContainer_User.Relbowy) + "," +
                            nameof(DataContainer_User.Lkneey) + "," +
                            nameof(DataContainer_User.Rkneey);

        }
        return _headerString;
    }
    
    public static DataContainer_User CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<DataContainer_User>(jsonString);
    }

    public string ConvertToJSON()
    {
        this.JSONdata = JsonUtility.ToJson(this);
        return JSONdata;
    }
}