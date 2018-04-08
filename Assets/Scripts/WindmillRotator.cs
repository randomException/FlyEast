using UnityEngine;
using System.Collections;

public class WindmillRotator : MonoBehaviour {

    // TODO: Rename "Rotator"
    // public variable gets rotating speed

	void Update () {
		transform.Rotate(new Vector3(0, 0, -200 * Time.deltaTime), Space.Self);
	}
}
