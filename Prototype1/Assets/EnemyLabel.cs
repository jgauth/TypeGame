    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyLabel : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float dynamicOffset = 0; // used to position text so it doesn't overlap with other text

    private Enemy enemy; // enemy this Label is attached to 
    private float initialOffset = 0; // used to set text above enemy

    public void SetEnemy(Enemy newEnemy)
    {
        enemy = newEnemy;
        text.text = enemy.GetWord();
        enemy.EnemyTextChanged += c_EnemyTextChanged;
        initialOffset = enemy.gameObject.GetComponent<Renderer>().bounds.extents.y; // get height of enemy
    }

    private void c_EnemyTextChanged(object sender, EnemyTextChangedEventArgs args)
    {
        text.text = args.newText;
    }

    public void UpdatePosition()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position + initialOffset * Vector3.up);
        screenPos.z = 0;
        screenPos.y += dynamicOffset;
        transform.position = screenPos;
    }

    private void LateUpdate()
    {
        // Vector3 totalOffset = (initialOffset + dynamicOffset) * Vector3.up; // TODO make sure this math is good
        // Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position + totalOffset);
        // screenPos.z = 0;
        // transform.position = screenPos;
        UpdatePosition();
    }


    private void OnDestroy()
    {
        enemy.EnemyTextChanged -= c_EnemyTextChanged;
    }
}
