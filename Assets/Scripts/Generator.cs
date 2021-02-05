using System.Collections;
using UnityEngine;
class Generator : MonoBehaviour
{
    protected virtual void Log(Element element)
    {
        Debug.Log("<b>Element #" + element.id + "</b> - Type: " + element.type + ", Price: " + element.price);
    }
    protected IEnumerator LogCoroutine(GameObject _log)
    {
        _log.SetActive(true);
        yield return new WaitForSeconds(2);
        _log.SetActive(false);
    }
}