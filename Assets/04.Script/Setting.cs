using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    void Start()
    {
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            Debug.Log($"{count}/{totalCount}");

            if(count == totalCount)
            {
                Managers.Data.Init();
            }
        });
    }
}
