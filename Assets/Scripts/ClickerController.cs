using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickerController : MonoBehaviour
{
    //���-�� ������
    [NonSerialized] public int totalAmount;
    [NonSerialized] public int silverAmount;
    [NonSerialized] public int goldAmount;

    //���� ������
    [SerializeField] private int silverRate;
    [SerializeField] private int goldRate;

    [SerializeField] private TextMeshProUGUI totalText;
    [SerializeField] private TextMeshProUGUI silverText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI silverUpgradeText;
    [SerializeField] private TextMeshProUGUI goldUpgradeText;

    [SerializeField] private GraphicRaycaster raycast;
    [SerializeField] private EventSystem eventSystem;
    //������� ���������
    private PointerEventData pointerEvent;
    //������ ����������� Raycast
    private List<RaycastResult> results = new List<RaycastResult>();

    private NoMoneyPictureController noMoneyImage;

    private void Start()
    {
        noMoneyImage = GetComponentInChildren<NoMoneyPictureController>();

        totalAmount = PlayerPrefs.GetInt("Total Amount", 0);
        silverAmount = PlayerPrefs.GetInt("Silver Amount", 0);
        goldAmount = PlayerPrefs.GetInt("Gold Amount", 0);

        silverRate = PlayerPrefs.GetInt("Silver Rate", silverRate);
        goldRate = PlayerPrefs.GetInt("Gold Rate", goldRate);
    }
    //����� ��������� ���-�� ������
    public void ApplyAmount()
    {
        if (results.Count >= 0)
        {
            if (results[0].gameObject.CompareTag("SilverCoin"))
            {
                totalAmount += silverRate;
                silverAmount += silverRate;
            }
            else if (results[0].gameObject.CompareTag("GoldCoin"))
            {
                totalAmount += goldRate;
                goldAmount += goldRate;
            }
        }
    }
    //�����, ���������� ������ ������
    public void ApplyUpgrade()
    {
        if (totalAmount >= results[0].gameObject.GetComponent<UpgradePriceController>().upgradePrice)
        {
            if (results[0].gameObject.CompareTag("SilverUpgrade") && 
                silverAmount >= results[0].gameObject.GetComponent<UpgradePriceController>().upgradePrice)
            {
                silverAmount -= results[0].gameObject.GetComponent<UpgradePriceController>().upgradePrice;
                silverRate += silverRate;
                totalAmount -= results[0].gameObject.GetComponent<UpgradePriceController>().upgradePrice;
                results[0].gameObject.GetComponent<UpgradePriceController>().upgradePrice += 10;
                silverUpgradeText.text = 
                    $"Upgrade Silver : {results[0].gameObject.GetComponent<UpgradePriceController>().upgradePrice} silver";
            }
            else if (results[0].gameObject.CompareTag("GoldUpgrade") && 
                goldAmount >= results[0].gameObject.GetComponent<UpgradePriceController>().upgradePrice)
            {
                goldAmount -= results[0].gameObject.GetComponent<UpgradePriceController>().upgradePrice;
                goldRate += goldRate;
                totalAmount -= results[0].gameObject.GetComponent<UpgradePriceController>().upgradePrice;
                results[0].gameObject.GetComponent<UpgradePriceController>().upgradePrice += 30;
                goldUpgradeText.text = 
                    $"Upgrade Gold : {results[0].gameObject.GetComponent<UpgradePriceController>().upgradePrice} gold";
            }
        }
        else if (totalAmount < results[0].gameObject.GetComponent<UpgradePriceController>().upgradePrice || totalAmount == 0)
        {
            noMoneyImage.ApplyColor();
        }
    }
    //Raycast ��� UI
    private void ApplyRaycast()
    {
        //������� ����� ������� ���������
        pointerEvent = new PointerEventData(eventSystem);
        //������ ��������� ��������� ������� ����
        pointerEvent.position = Input.mousePosition;
        //Raycast ���������� GraphicRaycaster � ������� ����� ����
        raycast.Raycast(pointerEvent, results);
    }
    private void ApplyText()
    {
        totalText.text = "Total Amount: " + totalAmount.ToString();
        silverText.text = silverAmount.ToString();
        goldText.text = goldAmount.ToString();

    }
    private void Update()
    {
        results.Clear();
        ApplyRaycast();

        ApplyText();
    }
    private void OnApplicationQuit()
    {
        //��������� ���-�� ��������� ������ � ��������� ��� ������ �� ����
        PlayerPrefs.SetInt("Total Amount", totalAmount);
        PlayerPrefs.SetInt("Silver Amount", silverAmount);
        PlayerPrefs.SetInt("Gold Amount", goldAmount);

        PlayerPrefs.SetInt("Silver Rate", silverRate);
        PlayerPrefs.SetInt("Gold Rate", goldRate);
    }
}
