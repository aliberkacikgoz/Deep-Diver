using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    //Singleton player manager örneði
    public static PlayerStateManager Instance;

    //Playerýn hangi evrede olduðunu gösteren enum
    public PlayerState CurrentState;
    /*
     * Playerýn hangi evrede olduðunu öðrenmek isteyen
     * script içinde
     * 
     * OnEnable()
     * {
     *      PlayerStateManager.OnPlayerStateChanged += YapýlacakFonksiyon()
     * }
     * OnDisable()
     * {
     *   PlayerStateManager.OnPlayerStateChanged -= YapýlacakFonksiyon()
     *  }
     *  
     *  þeklinde hangi evrede olduðu bilgisini alýp ona göre iþlem yapabilir
     * */


    //Yine ayný þekilde bu bilginin iletimini saðlayan event burda
    public static event Action<PlayerState> OnPlayerStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdatePlayerState(PlayerState newState)
    {
        CurrentState = newState;

        //Player managerýn state göre yapmasýný istediðimiz
        //iþlemler buraya

        switch (newState)
        {
            case PlayerState.Swiming:
                break;
            case PlayerState.Walking:
                break;
            case PlayerState.Fishing:
                break;
            case PlayerState.Idle:
                break;
            default:
                break;
        }

        //State deðiþtiði zaman eventi tetikleyerek
        //dinleyen tüm scriptlere bilgi verir
        OnPlayerStateChanged?.Invoke(newState);
    }
}

//Aklýnýza gelen farklý stateleri buraya ekleyebilirsiniz
public enum PlayerState
{
    Swiming,
    Walking,
    Fishing,
    Idle,
}