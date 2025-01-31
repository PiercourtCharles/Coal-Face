using UnityEngine;

public class Interactions : MonoBehaviour
{
    [SerializeField] float _distance;
    [SerializeField] LayerMask _interactions;
    [SerializeField] GameObject _uiText;

    public Hand[] Hands;

    private void Update()
    {
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Q) && Hands[0].ObjectInHand != null)
        {
            Hands[0].ObjectInHand.GetComponent<ObjectsComponents>().Grab(null);
            Hands[0].ObjectInHand = null;
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, _distance, _interactions))
        {
            _uiText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                //Doors
                var door = hit.transform.GetComponent<Doors>();

                if (door != null)
                {
                    //var objComp = Hands[0].ObjectInHand.GetComponent<ObjectsComponents>();

                    //if (objComp != null && door.IsLocked)
                    //    door.IsLocked = !objComp.Use();

                    if (!door.IsLocked)
                        door.ChangeTarget();
                    else
                        Debug.Log("Door close");
                }

                //Interactibles
                var inter = hit.transform.GetComponent<Interactible>();

                if (inter != null)
                    inter.ChangeTarget();

                //Control Panel
                var panelMod = hit.transform.GetComponent<ControlPanel>();

                if (panelMod != null)
                {
                    Debug.Log("Touch the panel");
                }

                //Objects
                var obj = hit.transform.GetComponent<ObjectsComponents>();

                if (obj != null && Hands[0].ObjectInHand == null)
                {
                    obj.Grab(Hands[0].HandTransform);
                    Hands[0].ObjectInHand = obj.gameObject;
                }

                //Objects placement
                var placement = hit.transform.GetComponent<ObjectPlacement>();

                if (placement != null && Hands[0].ObjectInHand != null)
                {
                    var objComp = Hands[0].ObjectInHand.GetComponent<ObjectsComponents>();

                    if (!placement.IsReplace && objComp.ObjectInfos.Type == ObjectInfos.ObjectType.Change && objComp.ObjectInfos.SubType == placement.SubType)
                    {
                        placement.IsReplace = objComp.Use();
                        Destroy(Hands[0].ObjectInHand);
                        Hands[0].ObjectInHand = null;

                        placement.Repair();
                    }
                    else
                        Debug.Log("Not the right one or not brake yet");
                }

                //Furnase
                var furnase = hit.transform.GetComponent<Furnase>();

                if (furnase != null && Hands[0].ObjectInHand != null)
                {
                    furnase.Repair(Hands[0].ObjectInHand.GetComponent<Fuel>());

                    Destroy(Hands[0].ObjectInHand);
                    Hands[0].ObjectInHand = null;
                }
            }
        }
        else
            _uiText.SetActive(false);

        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * _distance);
    }
}
