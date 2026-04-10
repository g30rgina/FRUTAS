using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public Text goombaText;

    public bool _pause;

    public int killedEnemies = 0;

    public void AddKill()
    {
        killedEnemies++;
        goombaText.text = killedEnemies.ToString();
    }

    public void Pause()
    {
        if(_pause == false)
        {
            Time.timeScale = 0;
            _pause = true;
        }
        else
        {
            Time.timeScale = 1;
            _pause = false;
        }
        
        pauseCanvas.SetActive(_pause);        
    }
}