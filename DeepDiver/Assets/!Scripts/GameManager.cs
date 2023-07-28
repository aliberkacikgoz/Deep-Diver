using System;

public class GameManager : MonoSingleton<GameManager>
{
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
    public static event Action<GameState> OnGameStateChanged;

    public void UpdateGameState(GameState newState)
    {
        CurrentState = newState;

        //Game managerýn state göre yapmasýný istediðimiz
        //iþlemler buraya
        switch (newState)
        {
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }

        //State deðiþtiði zaman eventi tetikleyerek
        //dinleyen tüm scriptlere bilgi verir
        OnGameStateChanged?.Invoke(newState);
    }
}

//Aklýnýza gelen farklý stateleri buraya ekleyebilirsiniz
public enum GameState
{
    Playing,
    Paused,
    GameOver,
}