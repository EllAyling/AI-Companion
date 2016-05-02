using UnityEngine;
using System.Collections;

public interface IDamagable {

    void TakeHit(float damage, RaycastHit hit);
}
