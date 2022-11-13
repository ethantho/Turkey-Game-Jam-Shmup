using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EffectManager : MonoBehaviour
{
    
    public static Dictionary<int,float[]> parameters = new Dictionary<int,float[]>(){};
    public static Dictionary<int,IEnumerator> coros = new Dictionary<int,IEnumerator>(){};

    [SerializeField] public static Color[] glitchColors = new Color[] {Color.cyan,Color.green,Color.red};
    [SerializeField] public GameObject cameraRef;

    [SerializeField] public GameObject bulletPref;
    [SerializeField] public GameObject explosionPref;
    [SerializeField] public GameObject bigExplosionPref;


    public static EffectManager instance;
    private static int hashCounter = 0;

    void Awake(){
        instance = this;
    }

    private static int generateHash(){
        hashCounter++;
        return hashCounter;
    }

    public static void StopCoroutine(int key){
        instance.StopCoroutine(coros[key]);
        coros.Remove(key);
        parameters.Remove(key);
    }

    //Flicker - Toggles visibility at a speed modulated by intensity.
    #region FLICKER
    //PARAMS: intensity, alpha
    public static int Start_Flicker(Image image, float intensity = 1f, float alpha = 0f){
        int key = generateHash();
        parameters[key] = new float[] {intensity,alpha};
        coros[key] = Effect_Flicker(image,key);
        instance.StartCoroutine(coros[key]);
        return key;
    }
    

    public static IEnumerator Effect_Flicker(Image image, int key){
        
        float timer = 0.0f;
        bool visible = false;
        while(true){
            timer+=Time.deltaTime;
            if(timer>=(.2f/parameters[key][0])){
                var tempColor = image.color;
                if(visible){
                    tempColor.a = 1f;
                }
                else{
                    tempColor.a = parameters[key][1];
                }
                visible = !visible;

                image.color = tempColor;
                timer = 0f;
            }
            yield return null;
        }

    }

    public static int Start_Flicker(SpriteRenderer image, float intensity = 1f, float alpha = 0f){
        int key = generateHash();
        parameters[key] = new float[] {intensity,alpha};
        coros[key] = Effect_Flicker(image,key);
        instance.StartCoroutine(coros[key]);
        return key;
    }
    

    public static IEnumerator Effect_Flicker(SpriteRenderer image, int key){
        
        float timer = 0.0f;
        float timer2 = 0.0f;
        bool visible = false;
        while(true){
            timer+=Time.deltaTime;
            timer2+=Time.deltaTime;
            if(timer>=(.2f/parameters[key][0])){
                var tempColor = image.color;
                if(visible){
                    tempColor.a = 1f;
                    if(timer2>1f){
                        image.color = tempColor;
                        StopCoroutine(key);
                        break;
                    }
                }
                else{
                    tempColor.a = parameters[key][1];
                }
                image.color = tempColor;
                visible = !visible;

                timer = 0f;
            }
            yield return null;
        }

    }

    #endregion

    //Flash - Starts at init image_alpha and decreases to 0 over time seconds.
    #region FLASH

    public static int Start_Flash(Image image, float time = .5f, float init = 1f){
        int key = generateHash();
        parameters[key] = new float[] {time,init};
        coros[key] = Effect_Flash(image,key);
        instance.StartCoroutine(coros[key]);
        return key;
    }
    

    public static IEnumerator Effect_Flash(Image image, int key){
        var tempColor = image.color;
        tempColor.a = parameters[key][1];
        image.color = tempColor;

        float timer = 0.0f;
        bool visible = false;
        while(timer<parameters[key][0]){
            timer+=Time.deltaTime;
            tempColor = image.color;
            tempColor.a = 1-(timer/parameters[key][0]);
            image.color = tempColor;
            yield return null;
        }
        tempColor = image.color;
        tempColor.a = 0;
        image.color = tempColor;

    }

    #endregion

    //Glitch - Changes color to glitch colors at a speed modulated by intensity.
    #region GLITCH

    public static int Start_Glitch(Image image, float intensity = 1f){
        int key = generateHash();
        parameters[key] = new float[] {intensity};
        coros[key] = Effect_Glitch(image,key);
        instance.StartCoroutine(coros[key]);
        return key;
    }
    

    public static IEnumerator Effect_Glitch(Image image, int key){
        
        float timer = 0.0f;
        int colorIndex = 0;
        while(true){
            timer+=Time.deltaTime;
            if(timer>=(.2f/parameters[key][0])){
                image.color = glitchColors[colorIndex%glitchColors.Length];
                colorIndex++;
                timer = 0f;
            }
            yield return null;
        }

    }

    public static int Start_Glitch(SpriteRenderer image, float intensity = 1f){
        int key = generateHash();
        parameters[key] = new float[] {intensity};
        coros[key] = Effect_Glitch(image,key);
        instance.StartCoroutine(coros[key]);
        return key;
    }
    

    public static IEnumerator Effect_Glitch(SpriteRenderer image, int key){
        
        float timer = 0.0f;
        int colorIndex = 0;
        while(true){
            timer+=Time.deltaTime;
            if(timer>=(.2f/parameters[key][0])){
                image.color = glitchColors[colorIndex%glitchColors.Length];
                colorIndex++;
                timer = 0f;
            }
            yield return null;
        }

    }

    #endregion

    //Pulsate - Rotates and grows and shrinks at rates according to rotSpeed (degrees per frame), and pulseSpeed (seconds), with max size increase equal to scale.
    #region PULSATE

    public static int Start_Pulsate(Transform transformRef, float rotSpeed = 1f, float scale = .2f,float pulseSpeed = 4f){
        int key = generateHash();
        parameters[key] = new float[] {rotSpeed,scale,pulseSpeed};
        coros[key] = Effect_Pulsate(transformRef,key);
        instance.StartCoroutine(coros[key]);
        return key;
    }
    

    public static IEnumerator Effect_Pulsate(Transform transformRef, int key){
        
        Vector3 scale = transformRef.localScale;
        float timer = 0.0f;
        int colorIndex = 0;
        while(true){
            if(!transformRef){
                StopCoroutine(key);
            }
            timer+=Time.deltaTime;
            transformRef.Rotate(0f,0f,parameters[key][0]);
            transformRef.localScale = scale*(1+parameters[key][1]*Mathf.Sin(Mathf.Deg2Rad*360f*timer/parameters[key][2]));
        
            yield return null;
        }

    }


    #endregion

    //CShake - Shakes the camera each frame for lifespan seconds and with size equal to scale.
    #region CAMERASHAKE

    public static int Start_CShake(float lifespan = 1f, float scale = 3f){
        int key = generateHash();
        parameters[key] = new float[] {lifespan,scale};
        coros[key] = Effect_CShake(key);
        instance.StartCoroutine(coros[key]);
        return key;
    }
    

    public static IEnumerator Effect_CShake(int key){
        
        Vector3 pos = instance.cameraRef.transform.position;
        float timer = 0.0f;
        while(true){
            
            timer+=Time.deltaTime;
            if(timer>parameters[key][0]){
                instance.cameraRef.transform.position = pos;
                StopCoroutine(key);
                break;
            }
            var scale = parameters[key][1];
            Vector3 direction = new Vector3(Random.Range(-1*scale,scale),Random.Range(-1*scale,scale),pos.z);
            instance.cameraRef.transform.position = pos+direction;
        
            yield return null;
        }

    }

    #endregion

    //Shake - Shakes an object each frame for lifespan seconds and with size equal to scale.
    #region SHAKE

    public static int Start_Shake(Transform transformRef, float lifespan = 1f, float scale = 3f){
        int key = generateHash();
        parameters[key] = new float[] {lifespan,scale};
        coros[key] = Effect_Shake(transformRef, key);
        instance.StartCoroutine(coros[key]);
        return key;
    }
    

    public static IEnumerator Effect_Shake(Transform transformRef, int key){
        
        Vector3 pos = transformRef.position;

        Vector3 previousDir = new Vector3(0f,0f,0f);

        float timer = 0.0f;
        while(true){
            
            timer+=Time.deltaTime;
            if(timer>parameters[key][0]){
                transformRef.position = transformRef.position-previousDir;
                StopCoroutine(key);
                break;
            }
            var scale = parameters[key][1];
            Vector3 direction = new Vector3(Random.Range(-1*scale,scale),Random.Range(-1*scale,scale),pos.z);
            transformRef.position = transformRef.position
                +direction-previousDir;

            previousDir = direction;
        
            yield return null;
        }

    }
    #endregion

    //BigExplosion - Spawns amt explosion objects, one every time seconds, at location loc_x, loc_y, with max offset scale.
    #region BIGEXPLOSION

    public static int Start_BigExplosion(float loc_x, float loc_y, float scale = 3f, float amt = 10f, float time = .2f){
        int key = generateHash();
        parameters[key] = new float[] {loc_x,loc_y,scale,amt,time};
        coros[key] = Effect_BigExplosion(key);
        instance.StartCoroutine(coros[key]);
        return key;
    }
    

    public static IEnumerator Effect_BigExplosion(int key){
        

        float timer = 0.0f;
        while(true){
            
            timer+=Time.deltaTime;
            if(timer>parameters[key][4]){
                parameters[key][3] -= 1f;
                var scale = parameters[key][2];
                if(parameters[key][3]<0){
                    GameObject explos = Instantiate(instance.bigExplosionPref, new Vector3(parameters[key][0]+Random.Range(-1*scale,scale),parameters[key][1]+Random.Range(-1*scale,scale),0),Quaternion.identity);
                    explos.transform.localScale = new Vector3(16f, 16f, 16f);
                    StopCoroutine(key);
                    break;
                }
                timer = 0;
                parameters[key][1]+=.5f;
                Instantiate(instance.explosionPref, new Vector3(parameters[key][0]+Random.Range(-1*scale,scale),parameters[key][1]+Random.Range(-1*scale,scale),0),Quaternion.identity);
                Instantiate(instance.explosionPref, new Vector3(parameters[key][0]+Random.Range(-1*scale,scale),parameters[key][1]+Random.Range(-1*scale,scale),0),Quaternion.identity);
            }
        
            yield return null;
        }

    }
    #endregion

    //








}
