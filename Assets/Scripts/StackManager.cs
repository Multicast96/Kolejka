using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour {
    private Stack<GameObject> stack = new Stack<GameObject>();
    private float stackHeight = 0;
    private GameManager stackCount;
    public GameObject textMeshGameObject;
    public float gapBetweenObjectsInStack = 0.1f;


    // Use this for initialization
    void Start()
    {
        textMeshGameObject = new GameObject();
        textMeshGameObject.name = "stack";
        textMeshGameObject.transform.parent = transform;
        textMeshGameObject.transform.position = transform.position;
        textMeshGameObject.transform.rotation = Quaternion.Euler(90, 0, 90);

        var textMesh = textMeshGameObject.AddComponent<TextMesh>();

        textMesh.fontSize = 100;
        textMesh.characterSize = 0.1f;
        textMesh.text = "0";
        textMesh.color = Color.white;
        textMesh.fontStyle = FontStyle.Bold;
        textMesh.anchor = TextAnchor.MiddleCenter;
    }

    public void Push(GameObject newGameObject)
    {
        newGameObject.transform.position = new Vector3(transform.position.x, stackHeight, transform.position.z);
        stackHeight += gapBetweenObjectsInStack;

        newGameObject.transform.rotation = transform.rotation;
        newGameObject.transform.parent = transform;
        stack.Push(newGameObject);

        textMeshGameObject.GetComponent<TextMesh>().text = stack.Count.ToString();
        textMeshGameObject.transform.position = new Vector3(transform.position.x, stackHeight, transform.position.z);
    }

    public GameObject Pop()
    {
        if (stack.Count == 0) return null;
        stackHeight -= gapBetweenObjectsInStack;
        textMeshGameObject.GetComponent<TextMesh>().text = (stack.Count - 1).ToString();
        textMeshGameObject.transform.position = new Vector3(transform.position.x, stackHeight, transform.position.z);
        return stack.Pop();
    }

    public bool IsEmpty()
    {
        if (stack.Count == 0) return true;
        else return false;
    }

}
