using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region CarBasicData
    [Serializable]
    public class CarBasicData
    {
        public int DataId;
        public int Speed;
        public int Acceleration;
        public int Deceleration;
        public int Handling;
    }

    [Serializable]
    public class CarBasicDataLoader : ILoader<int, CarBasicData>
    {
        public List<CarBasicData> cars = new List<CarBasicData>();
        public Dictionary<int, CarBasicData> MakeDict()
        {
            Dictionary<int, CarBasicData> dict = new Dictionary<int, CarBasicData>();
            foreach (CarBasicData carData in cars)
                dict.Add(carData.DataId, carData);
            return dict;
        }
    }
    #endregion
}
