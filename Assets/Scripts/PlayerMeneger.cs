using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeneger : MonoBehaviour
{
    

    #region Singleton
    public static PlayerMeneger Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion


    public GameObject Player;
}
