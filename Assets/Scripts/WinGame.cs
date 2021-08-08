using UnityEngine;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    [SerializeField] private GameObject _gameWinMenu;
   
    [SerializeField] private Text _timerTime;
    [SerializeField] private Text _gameWinTime;
    
    private static int _countPonyInsidePaddock;

    void Update()
    {
        GameWin();
    }

    private void GameWin()
    {
        if (_countPonyInsidePaddock == SpawnAnimal.GetMaxCountPony())
        {
            Time.timeScale = 0;
            _gameWinTime.text = _timerTime.text;
            _gameWinMenu.SetActive(true);
        }
    }


    public static void AddCountPonyInsidePaddock()
    {
        _countPonyInsidePaddock++;
    }
}
