using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetInfo : MonoBehaviour
{
	private float thresDist = 80;
	private PlanetaryOrbit PO;
	private PlanetInfo[] pI;
	private List<Planet> planetList;
	private Planet planet;

	public bool isSelected = false;

	private void Start ()
	{
		planetList = Planet.planetList;        
		PO = GetComponent<PlanetaryOrbit> ();
		planet = GetComponent<Planet> ();
	}

    private void Update()
    {
        float width;

        if (planet.PlanetCameraDistance > thresDist)
            width = 18 * transform.localScale.x;
        else
            width = (18 * transform.localScale.x - 160) / thresDist * planet.PlanetCameraDistance + 160;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Rect rect = new Rect(screenPos.x - width / 2, screenPos.y - width / 2, width, width);

        // Verificăm input-ul pentru ambele platforme (mobile și desktop)
        bool isTapped = false;

        // Pentru dispozitive mobile (touch)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && rect.Contains(touch.position))
            {
                isTapped = true;
            }
        }
        // Pentru desktop (mouse)
        else if (rect.Contains(Input.mousePosition) && Input.GetMouseButtonDown(0))
        {
            isTapped = true;
        }

        if (isTapped)
        {
            foreach (Planet p in planetList)
            {
                p.IsSelected = false;
            }
            isSelected = true;

            // Adăugăm un feedback vizual pentru touch (opțional)
#if UNITY_IOS || UNITY_ANDROID
        Handheld.Vibrate(); // Vibrație pe dispozitive mobile
#endif
        }
    }

    private void OnGUI ()
	{
		if (isSelected) {
			Vector2 sizeOfLabel = GUI.skin.textField.CalcSize (new GUIContent (name));
			Vector2 screenPos = Camera.main.WorldToScreenPoint (transform.position);
			GUI.Label (new Rect (screenPos.x - sizeOfLabel.x / 2, Screen.height - screenPos.y, 100, 100), name);
        
			float vel = PO.GetVelMagnitude ();
			GUI.Label (new Rect (screenPos.x - sizeOfLabel.x / 2 - 5, Screen.height - screenPos.y + 12, 100, 100),
                (Mathf.Round (vel * 100f) / 100f).ToString () + "km/s");

			string distance;
			if (tag == "Planet")
				distance = (Mathf.Round ((transform.position - transform.parent.position).magnitude * Scales.mu2au * 1000f) / 1000f).ToString () + "au";
			else
				distance = (Mathf.Round ((transform.position - transform.parent.position).magnitude * Scales.mu2au * Scales.au2km * 1f) / 1f).ToString () + "km";

			GUI.Label (new Rect (screenPos.x - sizeOfLabel.x / 2 - 5, Screen.height - screenPos.y + 24, 100, 100), distance);
		}
	}
}
