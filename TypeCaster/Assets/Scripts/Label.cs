using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Label : MonoBehaviour {

    public Text labelText; // text object that is rendered to canvas

    private Targetable targetable;
    private float heightOffset = 0; // used to set text above enemy

    public void SetTargetable(Targetable t) {
        targetable = t;
        labelText.text = targetable.GetKillWord();
        targetable.TargetableTextChanged += c_TargetableTextChanged;
        heightOffset = targetable.gameObject.GetComponentInChildren<Renderer>().bounds.extents.y; // get height of enemy
    }

    public float GetHeightOffset() {
        return heightOffset;
    }

    public Targetable GetTargetable() {
        return this.targetable;
    }

    private void c_TargetableTextChanged(object sender, TargetableTextChangedEventArgs args) {
        labelText.text = args.newText;
    }

    private void OnDisable() {
        targetable.TargetableTextChanged -= c_TargetableTextChanged;
    }
}
