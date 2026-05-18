    using TMPro;
using Unity.VisualScripting;
using UnityEngine;
    using UnityEngine.UI;

    public class UiManager : MonoBehaviour
    {
    public GameObject Ui;
        public Image type;
        public GameObject barre;
        public TMP_Text acidite;
        public TMP_Text temperature;
    private float baseScale;
        void Start()
        {
        baseScale = barre.transform.localScale.x;
            Ui.SetActive(false);
        }

    public void changeDatas(Sprite _type, float pvmax, float pv, float _acidite, float _temp)
    {
        Ui.SetActive(true);
        if (type!=null) type.sprite= _type;
        barre.transform.localScale = new Vector3(pv / pvmax*baseScale, barre.transform.localScale.y, barre.transform.localScale.z);
        acidite.text = _acidite.ToString();
        temperature.text = _temp.ToString();
    }

    }
