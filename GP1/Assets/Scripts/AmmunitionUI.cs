using System;
using TMPro;
using UnityEngine;

public class AmmunitionUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _ammoText;
    [SerializeField] Player _player;


    void Update()
    {
        _ammoText.text = _player.CurrentAmmunition();
    }
}
