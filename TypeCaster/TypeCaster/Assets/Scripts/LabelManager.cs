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
            try {
                if (targetableLabels[t].gameObject != null) {
                    Destroy(targetableLabels[t].gameObject);
                }
            }
            catch (MissingReferenceException exc) {
                Debug.Log(exc);
            }

            targetableLabels.Remove(t);
        }
    }

    void Awake() {
        Targetable.TargetableAdded += c_TargetableAdded;
        Targetable.TargetableRemoved += c_TargetableRemoved;
    }
}
