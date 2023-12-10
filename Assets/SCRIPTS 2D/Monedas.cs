using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : MonoBehaviour
{

    public int valor = 1;
    public ControlGeneral controlGeneral;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            controlGeneral.SumarPuntos(valor);
            Destroy(this.gameObject);
        }

    }
}