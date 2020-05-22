using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelManager : MonoBehaviour {

    // Empty gameobject to put labels inside of
    public GameObject labelHolder;

    public Label labelPrefab;

    private Dictionary<Targetable, Label> targetableLabels = new Dictionary<Targetable, Label>();

    void c_TargetableAdded(object sender, TargetableAddedEventArgs args) {
        Targetable t = args.targetableAdded;

        // if not already in the dict
        if (!targetableLabels.ContainsKey(t)) {
            Label newLabel = Instantiate(labelPrefab, labelHolder.transform);
            targetableLabels.Add(t, newLabel);
            newLabel.SetTargetable(t);
        }
    }

    void c_TargetableRemoved(object sender, TargetableRemovedEventArgs args) {
        Targetable t = args.targetableRemoved;

        if (targetableLabels.ContainsKey(t)) {
            
            if (targetableLabels[t]) {
                Destroy(targetableLabels[t].gameObject);
            }

            targetableLabels.Remove(t);
        }
    }

    void Awake() {
        Targetable.TargetableAdded += c_TargetableAdded;
        Targetable.TargetableRemoved += c_TargetableRemoved;
    }

    private void OnDisable() {
        Targetable.TargetableAdded -= c_TargetableAdded;
        Targetable.TargetableRemoved -= c_TargetableRemoved;
    }

    // ---------------------------------------------------
    // LABEL POSITION SORTING
    // ---------------------------------------------------

    // Used to sort Labels based on Y position. Low to high
    private int CompareLabelByPosition(Label a, Label b) {
        float ay = a.transform.position.y;
        float by = b.transform.position.y;

        if (ay < by) {
            return -1;
        } else if (ay > by) {
            return 1;
        }
        return 0;
    }

    private List<Label> CreatedSortedLabelList() {
        List<Label> labelList = new List<Label>();
        
        foreach (KeyValuePair<Targetable, Label> entry in targetableLabels) {
            if (entry.Value) {
                labelList.Add(entry.Value);
            }
        }

        labelList.Sort(CompareLabelByPosition);

        return labelList;
    }

    private void LateUpdate() {
        // if (Input.GetKeyDown(KeyCode.G)) {
            List<Label> labelList = CreatedSortedLabelList();

            int numLabels = labelList.Count;
            for (int i = 0; i < numLabels - 1; i++) {
                for (int j = i+1; j < numLabels; j++) {

                    Label a = labelList[i];
                    Label b = labelList[j];

                    RectTransform rectTrans_a = a.GetComponent<RectTransform>();
                    RectTransform rectTrans_b = b.GetComponent<RectTransform>();

                    Vector3[] v_a = new Vector3[4];
                    Vector3[] v_b = new Vector3[4];

                    rectTrans_a.GetWorldCorners(v_a);
                    rectTrans_b.GetWorldCorners(v_b);

                    Rect rect_a = new Rect(v_a[1].x, v_a[1].y, v_a[2].x - v_a[1].x, v_a[1].y - v_a[0].y);
                    Rect rect_b = new Rect(v_b[1].x, v_b[1].y, v_b[2].x - v_b[1].x, v_b[1].y - v_b[0].y);


                    if (rect_a.Overlaps(rect_b)) {

                        Debug.Log($"{a.labelText.text} overlaps {b.labelText.text}");

                        while(rect_a.Overlaps(rect_b)) {
                            rect_b.y += 2f;
                            b.dynamicOffset += 2f;
                        }
                        b.dynamicOffset += 2f;
                    }
                }
            }
        // }
    }
}
