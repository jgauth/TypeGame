using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelManager : MonoBehaviour {

    // Empty gameobject to put labels inside of
    public GameObject labelHolder;
    public Camera mainCamera;
    public Label labelPrefab;

    private Dictionary<Targetable, Label> targetableLabels = new Dictionary<Targetable, Label>();

    // when new targetables are spawned, this will create the label for them
    void c_TargetableAdded(object sender, TargetableAddedEventArgs args) {
        Targetable t = args.targetableAdded;

        // if not already in the dict
        if (!targetableLabels.ContainsKey(t)) {
            Label newLabel = Instantiate(labelPrefab, labelHolder.transform);
            targetableLabels.Add(t, newLabel);
            newLabel.SetTargetable(t);
        }
    }

    // remove label when targetables are despawned
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

    // ---------------------------------------------------
    // DYNAMIC LABEL POSITIONING
    // ---------------------------------------------------
    private void LateUpdate() {
        List<Label> labelList = CreatedSortedLabelList();

        Label temp, current;
        RectTransform tempRectTrans, currentRectTrans;
        Vector3[] tempV = new Vector3[4];
        Vector3[] currentV = new Vector3[4];
        Rect tempRect, currentRect;
        for (int i = 0; i < labelList.Count; i++) {

            current = labelList[i];
            Vector3 idealPos = mainCamera.WorldToScreenPoint(current.GetTargetable().transform.position);
            idealPos.z = 0f;
            current.transform.position = idealPos;

            currentRectTrans = current.GetComponent<RectTransform>();
            currentRectTrans.GetWorldCorners(currentV);
            currentRect = new Rect(currentV[1].x, currentV[1].y, currentV[2].x - currentV[1].x, currentV[1].y - currentV[0].y);
            
            for (int j = 0; j < i; j++) {

                temp = labelList[j];
                tempRectTrans = temp.GetComponent<RectTransform>();
                tempRectTrans.GetWorldCorners(tempV);
                tempRect = new Rect(tempV[1].x, tempV[1].y, tempV[2].x - tempV[1].x, tempV[1].y - tempV[0].y);

                while (tempRect.Overlaps(currentRect)) {
                    currentRect.y += 2f;
                    idealPos.y += 2f;
                }
            }

            current.transform.position = idealPos;
        }
    }
}
