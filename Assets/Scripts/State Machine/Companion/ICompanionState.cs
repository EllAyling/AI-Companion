using UnityEngine;
using System.Collections;

public interface ICompanionState {

    void UpdateState();

    void OnTriggerEnter(Collider other);

    void ToFollowState();
    void FromFollowState();
    void ToCombatState();
    void FromCombatState();
}
