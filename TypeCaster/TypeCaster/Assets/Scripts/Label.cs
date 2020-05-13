using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Label : MonoBehaviour {

    public Text labelText;

    // public float dynamicOffset = 0; // used to position text so it doesn't overlap with other text

    private Targetable targetable;
    // private float initialOffset = 0; // used to set text above enemy

    public void SetTargetable(Targetable t) {
        targetable = t;
        labelText.text = targetable.GetKillWord();
        targetable.TargetableTextChanged += c_TargetableTextChanged;
        // initialOffset = enemy.gameObject.GetComponent<Renderer>().bounds.extents.y; // get height of enemy
    }

    private void c_TargetableTextChanged(object sender, TargetableTextChangedEventArgs args) {
        labelText.text = args.newText;
    }


    private void LateUpdate() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetable.transform.position);
        transform.position = screenPos;
    }

    private void OnDisable() {
        targetable.TargetableTextChanged -= c_TargetableTextChanged;
    }
}
