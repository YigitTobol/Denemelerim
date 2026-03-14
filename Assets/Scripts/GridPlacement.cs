using UnityEngine;

public class GridPlacement : MonoBehaviour
{
    
    float gridSize = 0.5f;  
    float maxMenzil = 2f; 
    [SerializeField] private LayerMask placementLayer; 
    
    private int kalanEsyaSayisi = 0; 
    [SerializeField] private Transform CloneKlasörü;

    [SerializeField] private Transform hologram; 
    
    [SerializeField] private GameObject objePrefab; 

    void Start()
    {
        // Oyun başladığında elimizde eşya yoksa hologramı gizle
        if (kalanEsyaSayisi == 0 && hologram != null)
        {
            hologram.gameObject.SetActive(false);
        }
    }

    void Update()   
    {
        
        // B tuşu ile satın alma 
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (kalanEsyaSayisi == 0) 
            {
                kalanEsyaSayisi++; 
                hologram.gameObject.SetActive(true); 
            }
            else
            {}
            
            
        }

      // Sağ tıkla yerdeki eşyayı geri al
        if (Input.GetMouseButtonDown(1)) 
        {
            if (kalanEsyaSayisi == 0)
            {
                EsyayiGeriAl();
            }
            else
            {}
            
          
        }    

        // Eğer elimizde eşya yoksa bekle 
        if (kalanEsyaSayisi == 0) return; 

        // Hologramı fareyle hareket ettirme
        Vector3 hedefNokta = HedefNoktayiBul();
        Vector3 snappedPosition = SnapToGrid(hedefNokta);
        hologram.position = snappedPosition;

        // Sol tık ile eşyayı yere koy
        if (Input.GetMouseButtonDown(0)) 
        {
            Yerlestir(snappedPosition);
        }
    }

    private void Yerlestir(Vector3 pozisyon)
    {
        // Objeyi yarat
        Instantiate(objePrefab, pozisyon, hologram.rotation,CloneKlasörü);
        
        kalanEsyaSayisi--; 
        
        // Eğer elinde eşya yoksa hologramı tekrar gizle
        if (kalanEsyaSayisi == 0)
        {
            hologram.gameObject.SetActive(false);
        }
            
    }

    private void EsyayiGeriAl()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxMenzil))
        {
            if (hitInfo.collider.CompareTag("Esya"))
            {
                Destroy(hitInfo.collider.gameObject);
                kalanEsyaSayisi++;
                hologram.gameObject.SetActive(true); 
               
            }
        }
    }

    private Vector3 HedefNoktayiBul()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, placementLayer))
        {
            Vector3 vurusNoktasi = hitInfo.point;
            float mesafe = Vector3.Distance(Camera.main.transform.position, vurusNoktasi);

            if (mesafe > maxMenzil)
            {
                Vector3 karakterYatayPozisyon = new Vector3(Camera.main.transform.position.x, vurusNoktasi.y, Camera.main.transform.position.z);
                Vector3 yon = (vurusNoktasi - karakterYatayPozisyon).normalized;
                return karakterYatayPozisyon + (yon * maxMenzil);
            }
            return vurusNoktasi + (hitInfo.normal * (gridSize / 2f));
        }
        return ray.GetPoint(maxMenzil);
    }

    private Vector3 SnapToGrid(Vector3 rawPosition)
    {
        float x = Mathf.Round(rawPosition.x / gridSize) * gridSize;
        float y = Mathf.Round(rawPosition.y / gridSize) * gridSize; 
        float z = Mathf.Round(rawPosition.z / gridSize) * gridSize;
        
       
        y = Mathf.Max(0.05f, y); // eşya yerin içine girmesin küp kullandığım için öyle grdm
        return new Vector3(x, y , z);
    }
}