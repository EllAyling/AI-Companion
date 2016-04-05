using UnityEngine;
using System.Collections;

public interface IEnemyState {

    void UpdateState();

    void OnTriggerEnter(Collider other);

    void ToPatrolState();
    void FromPatrolState();

    void ToAlertState();
    void FromAlertState();

    void ToChaseState();
    void FromChaseState();
}
