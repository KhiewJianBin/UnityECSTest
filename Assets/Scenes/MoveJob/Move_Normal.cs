using UnityEngine;
using Unity.Mathematics;

public class Move_Normal : MonoBehaviour
{
    public int SpawnSize = 10_000;

    [SerializeField] GameObject prefab;

    GameObject[] gos;

    void Awake()
    {
        gos = new GameObject[SpawnSize];
        for (int i = 0; i < SpawnSize; i++)
        {
            GameObject go = Instantiate(prefab);
            gos[i] = go;
        }
    }
    void Update()
    {
        var time = Time.time;

        for (int index = 0; index < gos.Length; index++)
        {
            //Move
            var y = math.sin(time + index) * math.sqrt(index);
            gos[index].transform.position = new Vector3(index, y, 0);
        }
    }
}