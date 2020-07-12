using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    void Update()
    {
        float speed = 10f;
        transform.position += new Vector3(0, speed) * Time.deltaTime;

        Destroy(gameObject, .3f);
    }

    public static void DamagePop(GameObject Popup, string word, Vector3 spawnPos)
    {
        GameObject popUpGameObject = Instantiate(Popup, spawnPos, Quaternion.identity);
        TextMeshPro textMesh = popUpGameObject.GetComponent<TextMeshPro>();
        textMesh.SetText(word);
    }
}
