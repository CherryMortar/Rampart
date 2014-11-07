using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class MainScript : MonoBehaviour
{
    protected List<GameObject> playField;
    protected PlayFieldSpawner playFieldSpawner;

    // Use this for initialization
    void Start()
    {
        playFieldSpawner = this.gameObject.GetComponent<PlayFieldSpawner>();
        playField = playFieldSpawner.GenerateJagtangularPlayField();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
