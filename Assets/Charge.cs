using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Charge : MonoBehaviour
{
    static double K = 9 * Math.Pow(10, 9);  

	public static List<Charge> Charges;

	public Rigidbody rb;

	public int chargeMagnitude;

	void FixedUpdate ()
	{
		foreach (Charge charge in Charges)
		{
			if (charge != this)
				Force(charge);
		}
	}

	void OnEnable ()
	{
		if (Charges == null)
			Charges = new List<Charge>();

		Charges.Add(this);
	}

	void OnDisable ()
	{
		Charges.Remove(this);
	}

	void Force (Charge secondCharge)
	{
		Rigidbody secondChargeRb = secondCharge.rb;

		Vector3 direction = rb.position - secondChargeRb.position;
		float distance = direction.magnitude;

		if (distance == 0f)
			return;
        
		float forceMagnitude = (float)K * (this.chargeMagnitude * secondCharge.chargeMagnitude * Mathf.Pow(10,-10)) / Mathf.Pow(distance, 2);
		Vector3 force = -direction.normalized * forceMagnitude;

        secondChargeRb.AddForce(force);
	}
}
