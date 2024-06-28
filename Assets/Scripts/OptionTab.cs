using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionTab : MonoBehaviour
{
    public bool IsCorrect = false;
    public bool Answered = false;
    [HideInInspector] public OptionsContainer container;
    [SerializeField] private TextMeshProUGUI OptionText;

    public void Initialize(bool isCorrect, string answer)
    {
        OptionText.text = answer;
        this.IsCorrect = isCorrect;
    }

    public void SetMaterial(Material mat)
    {
        GetComponent<MeshRenderer>().sharedMaterial = mat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Answered) return;

        if (other.GetComponent<Runner>() == null) return;

        if (IsCorrect)
        {
            container.OnRightOptionHit();
        }
        else
        {
            container.OnWrongOptionHit();
        }
    }
}
