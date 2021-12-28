using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] TextMeshProUGUI healthText;

    public void ChangeHealth(int healthAmount)
    {
        healthText.text = healthAmount.ToString();
    }
}
