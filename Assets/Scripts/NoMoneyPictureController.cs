using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//класс, 
public class NoMoneyPictureController : MonoBehaviour
{
    private Image noMoney;
    private void Start()
    {
        noMoney = GetComponent<Image>();
    }
    public void ApplyColor()
    {
        noMoney.color = Color.Lerp(Color.clear, Color.white, 1);
        StartCoroutine(LerpTimer());
    }
    private IEnumerator LerpTimer()
    {
        yield return new WaitForSeconds(0.2f);
        noMoney.color = Color.Lerp(Color.white, Color.clear, 1);
    }
}
