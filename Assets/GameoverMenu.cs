using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverMenu : MonoBehaviour
{
    public void Setup(){
        gameObject.SetActive(true);
    }
    public void RestartButton(){
        SceneManager.LoadScene("SampleScene");
    }
}
