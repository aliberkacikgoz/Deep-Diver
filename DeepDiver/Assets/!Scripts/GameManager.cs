using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton game manager örneði
    public static GameManager Instance;
    
    //Oyunun hangi evrede olduðunu gösteren enum
    public GameState CurrentState;
    /*
     * Oyunun hangi evrede olduðunu öðrenmek isteyen
     * script içinde
     * 
     * OnEnable()
     * {
     *      GameManager.OnStateChanged += YapýlacakFonksiyon()
     * }
     * OnDisable()
     * {
     *   GameManager.OnStateChanged -= YapýlacakFonksiyon()
     *  }
     *  
     *  þeklinde hangi evrede olduðu bilgisini alýp ona göre iþlem yapabilir
     * */


    //Yine ayný þekilde bu bilginin iletimini saðlayan event burda
    public static event Action<GameState> OnStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        CurrentState = newState;

        //Game managerýn state göre yapmasýný istediðimiz
        //iþlemler buraya
        switch (newState)
        {
            case GameState.Diving:
                break;
            case GameState.InSubmarine:
                break;
            case GameState.DrivingSubmarine:
                break;
            default:
                break;
        }

        //State deðiþtiði zaman eventi tetikleyerek
        //dinleyen tüm scriptlere bilgi verir
        OnStateChanged?.Invoke(newState);
    }
}

//Aklýnýza gelen farklý stateleri buraya ekleyebilirsiniz
public enum GameState
{
    Diving,
    InSubmarine,
    DrivingSubmarine,
}