    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyLabel : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float initialOffset = 0; // used to set text above enemy
    public float dynamicOffset = 0; // used to position text so it doesn't overlap with other text

    private Enemy enemy; // enemy this Label is attached to 

    public void SetEnemy(Enemy enemy)
    {
        Debug.Log("Inside SetEnemy()");
        if (enemy == null)
        {
            Debug.Log("Inside SetEnemy() ENEMY IS NULL");
        }
        else
        {
            Debug.Log("Inside SetEnemy() ENEMY IS NOT NULL");
        }
        this.enemy = enemy;
        enemy.EnemyTextChanged += c_EnemyTextChanged;
    }

    private void c_EnemyTextChanged(object sender, EnemyTextChangedEventArgs args)
    {
        text.text = args.newText;
    }

    private void LateUpdate()
    {
        Vector3 totalOffset = (initialOffset + dynamicOffset) * Vector3.up; // TODO make sure this math is good
        transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + totalOffset);
    }


    private void OnDestroy()
    {
        enemy.EnemyTextChanged -= c_EnemyTextChanged;
    }
}
