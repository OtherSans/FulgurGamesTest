using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//класс для кнопки, сбрасывающей данные
public class DeleteSaves : MonoBehaviour
{
    public void ApplyReset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        SceneManager.LoadScene("ClickerGame");
    }
}
