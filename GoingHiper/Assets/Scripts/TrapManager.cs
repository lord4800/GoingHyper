using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    [SerializeField] private List<TrapController> traps = new List<TrapController>();
    private int trapId;

    // Start is called before the first frame update
    void Start()
    {
        trapId = 0;
        traps[trapId].SetActive(true);
    }

    private void OnEnable()
    {
        traps[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Input();
    }

    private void Input()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0) && traps.Count > trapId)
        {
            traps[trapId].ActivateTrap();
            trapId++;
            if (traps.Count > trapId)
                traps[trapId].SetActive(true);
        }
    }
}
