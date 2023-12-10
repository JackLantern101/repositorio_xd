using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interfaz : MonoBehaviour
{
    public ControlGeneral controlGeneral;
    public TextMeshProUGUI puntos;

    // Update is called once per frame
    void Update()
    {
        puntos.text = controlGeneral.PuntosTotales.ToString();
    }
}