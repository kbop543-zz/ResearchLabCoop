using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1Status : MonoBehaviour {

    public bool frozen = false;
    public bool shrank = false;
    public bool blown = false;
    public bool oiled = false;
    public float duration = 7.0f;
    public float shrinkDuration = 7f;
    public float speedLimit = 500f;
    private bool hasInvincibility;

    public float originalSpeed;
    public float originalScale;
    private IEnumerator curUnshrink;
    private IEnumerator curUnfreeze;
    private IEnumerator curUnoiled;
    private IEnumerator curInvWindow;

    public Image shrinkFill;
    public Image freezeFill;
    public float shrinkStartTime;
    public float freezeStartTime;
    public float shrinkTimePassed; // timePassed = Time.time - (___)StartTime;
    public float freezeTimePassed;
    public ParticleSystem shatter;
    public ParticleSystem smoke;

    private Color originalMaterialColor;

    private void Start()
    {
        originalSpeed = gameObject.GetComponent<p1_movement>().speed;
        originalScale = transform.localScale.x;

        shrinkFill.fillAmount = 1.0f;
        freezeFill.fillAmount = 1.0f;

        GetMaterial();

        // SetInvincibility(true);
    }

    private void Update()
    {
        if (shrank)
        {
            shrinkTimePassed = Time.time - shrinkStartTime;
            shrinkFill.fillAmount = shrinkTimePassed / duration;
        }
        if (frozen)
        {
            freezeTimePassed = Time.time - freezeStartTime;
            freezeFill.fillAmount = freezeTimePassed / duration;
        }
    }
    //void FixedUpdate()
    //{
    //    if (shrank)
    //    {
    //        if (curUnshrink != null) {
    //            StopCoroutine(curUnshrink);
    //        }
    //        curUnshrink = Unshrink();
    //        StartCoroutine(Unshrink());

    //        // Debug.Log("Waiting for unshrink!");
    //    }

    //    if (frozen)
    //    {
    //        if (curUnfreeze != null)
    //        {
    //           StopCoroutine(curUnfreeze);
    //        }
    //        curUnfreeze = Unfreeze();
    //        StartCoroutine(Unfreeze());
    //        // Debug.Log("Waiting for unfreeze!");
    //    }
    //
    //}

    //IEnumerator waitForStatusEnd()
    //{
    //    yield return new WaitForSeconds(duraiton);

    //    Debug.Log("Effect debuffed!!");
    //}

    public void Shrink(float ratio)
    {
        shrank = true;
        //float estimatedSpeed = gameObject.GetComponent<p1_movement>().speed * ratio;
        // reduce speed after being shrunk
        // if (estimatedSpeed <= speedLimit)
        // {
        //     gameObject.GetComponent<p1_movement>().speed = estimatedSpeed;
        // }
        if (curUnshrink != null)
        {
            StopCoroutine(curUnshrink);
        }
        curUnshrink = Unshrink();
        StartCoroutine(curUnshrink);
        // Debug.Log("Waiting for unshrink!");

        // Mark the time when player is shrunk
        shrinkStartTime = Time.time;
        shrinkTimePassed = Time.time - shrinkStartTime;
        shrinkFill.fillAmount = shrinkTimePassed / duration;
    }

    IEnumerator Unshrink()
    {
        yield return new WaitForSeconds(shrinkDuration);

        // restore original size after being unshrink
        while (transform.localScale.x < originalScale)
        {
            transform.localScale = transform.localScale + new Vector3(1f, 1f, 1f) * (originalScale / 3) * Time.deltaTime;
            transform.position = new Vector3(transform.position.x,
                                             transform.localScale.y / 2,
                                             transform.position.z);
            yield return null;
        }

        // restore original speed after being unshrink
        if (!frozen)
        {
            gameObject.GetComponent<p1_movement>().speed = originalSpeed;
        }

        shrank = false;
        // Debug.Log("Unshrank!!");
    }

    public void Freeze()
    {
        frozen = true;
        // Enable iceCrystal renderer
        transform.GetChild(2).GetChild(0).GetComponent<MeshRenderer>().enabled = true;

        // make speed 0 after being frozen
        gameObject.GetComponent<p1_movement>().speed = 0;

        if (curUnfreeze != null)
        {
            StopCoroutine(curUnfreeze);
        }
        curUnfreeze = Unfreeze();
        StartCoroutine(curUnfreeze);
        // Debug.Log("Waiting for unfreeze!");

        freezeStartTime = Time.time;
        freezeTimePassed = Time.time - freezeStartTime;
        freezeFill.fillAmount = freezeTimePassed / duration;
    }

    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(duration);

        // restore original speed after being unfrozen
        gameObject.GetComponent<p1_movement>().speed = originalSpeed;

        frozen = false;
        transform.GetChild(2).GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        //Debug.Log("Unfrozen!!");
    }

    public void BlowAway(float seconds)
    {
        blown = true;
        float curSpeed = gameObject.GetComponent<p1_movement>().speed;
        gameObject.GetComponent<p1_movement>().speed = 0;

        StartCoroutine(UnBlown(seconds, curSpeed));
    }

    IEnumerator UnBlown(float seconds, float curSpeed)
    {
        yield return new WaitForSeconds(seconds);
        blown = false;

        // restore original speed after being unfrozen
        if (frozen)
        {
        }
        else if (shrank)
        {
            gameObject.GetComponent<p1_movement>().speed = curSpeed;
        }
        else
        {
            gameObject.GetComponent<p1_movement>().speed = originalSpeed;
        }

    }

    public void Fall()
    {
        if (shrank)
        {
            StopCoroutine(Unshrink());
            shrank = false;
        }

        gameObject.GetComponent<p1_movement>().speed = 0;
        gameObject.GetComponent<PlayerHealth>().InvokeFalling();
        gameObject.GetComponent<p1_movement>().speed = originalSpeed;
    }

    public void Shattered()
    {
        //Particle effect not gonna work as it .Die() will disable the player GameObject
        shatter.Play();
        gameObject.GetComponent<p1_movement>().speed = 0;
        gameObject.GetComponent<PlayerHealth>().InvokeDie();
        gameObject.GetComponent<p1_movement>().speed = originalSpeed;
    }

    public void Oiling()
    {
        oiled = true;

        // Darkened texture
        Darken(0.7f);

        gameObject.GetComponent<p1_movement>().speed = originalSpeed / 2;

        if (curUnoiled != null)
        {
            StopCoroutine(curUnoiled);
        }
        curUnoiled = Unoil();
        StartCoroutine(curUnoiled);
    }

    IEnumerator Unoil()
    {
        yield return new WaitForSeconds(duration);

        if (!frozen)
        {
            gameObject.GetComponent<p1_movement>().speed = originalSpeed;
        }

        oiled = false;

        // Original texture
        RestoreColor();
    }

    public void Shock()
    {


        if (gameObject.GetComponent<P1Status>().oiled)
        {
            Exploded();
        }
        //else
        //{
        //    gameObject.GetComponent<EnemyMovement>().forwardSpeed = gameObject.GetComponent<EnemyMovement>().forwardSpeed * 2;
        //}
    }

    public void Exploded()
    {
        //Particle effect not gonna work as it .Die() will disable the player GameObject
        smoke.Play();
        gameObject.GetComponent<p1_movement>().speed = 0;
        gameObject.GetComponent<PlayerHealth>().InvokeDie();
        gameObject.GetComponent<p1_movement>().speed = originalSpeed;
    }

    private void GetMaterial()
    {
        MeshRenderer[] allMesh = transform.GetChild(1).GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in allMesh)
        {
            originalMaterialColor = mesh.material.color;
        }
        SkinnedMeshRenderer[] allSkinMesh = transform.GetChild(1).GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer skinMesh in allSkinMesh)
        {
            originalMaterialColor = skinMesh.material.color;
        }
    }

    public void Darken(float percent)
    {
        percent = Mathf.Clamp01(percent);
        MeshRenderer[] allMesh = transform.GetChild(1).GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in allMesh)
        {
            mesh.material.color = new Color(originalMaterialColor.r * (1 - percent), originalMaterialColor.g * (1 - percent), originalMaterialColor.b * (1 - percent), originalMaterialColor.a);
        }
        SkinnedMeshRenderer[] allSkinMesh = transform.GetChild(1).GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer skinMesh in allSkinMesh)
        {
            skinMesh.material.color = new Color(originalMaterialColor.r * (1 - percent), originalMaterialColor.g * (1 - percent), originalMaterialColor.b * (1 - percent), originalMaterialColor.a);
        }
    }

    public void RestoreColor()
    {
        MeshRenderer[] allMesh = transform.GetChild(1).GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in allMesh)
        {
            mesh.material.color = originalMaterialColor;
        }
        SkinnedMeshRenderer[] allSkinMesh = transform.GetChild(1).GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer skinMesh in allSkinMesh)
        {
            skinMesh.material.color = originalMaterialColor;
        }
    }

    public void RestoreStatus()
    {
        if (curUnshrink != null) {
            StopCoroutine(curUnshrink);
        }
        if (curUnfreeze != null) {
            StopCoroutine(curUnfreeze);
        }
        frozen = false;
        shrank = false;
        blown = false;
        oiled = false;
        gameObject.GetComponent<p1_movement>().speed = originalSpeed;
        transform.localScale = new Vector3(1, 1, 1) * originalScale;

        RestoreColor();
    }

    public void SetInvincibility()
    {
        hasInvincibility = true;

        if (curInvWindow != null)
        {
            StopCoroutine(curInvWindow);
        }
        curInvWindow = EnterInvWindow(4f);
        StartCoroutine(curInvWindow);

    }

    private IEnumerator EnterInvWindow(float t) {
        // Transparency change
        SkinnedMeshRenderer[] allSkinMesh = transform.GetChild(1).GetComponentsInChildren<SkinnedMeshRenderer>();
        float tempTransparency = 0.1f;
        Color tempColor = new Color(originalMaterialColor.r, originalMaterialColor.g, originalMaterialColor.b, originalMaterialColor.a);
        tempColor.a = tempTransparency;

        float chgRate = 8f;
        bool goUp = true;

        float i = 0f;
        while (i < t) {
            if (goUp)
            {
                if (tempTransparency < originalMaterialColor.a) {
                    tempTransparency += chgRate * Time.deltaTime;
                    tempColor.a = tempTransparency;
                }
                else {
                    goUp = false;
                }

            }
            else //if it's going down
            {
                if (tempTransparency > 0.1f)
                {
                    tempTransparency -= chgRate * Time.deltaTime;
                    tempColor.a = tempTransparency;
                }
                else
                {
                    goUp = true;
                }

            }
            foreach (SkinnedMeshRenderer skinMesh in allSkinMesh)
            {
                skinMesh.material.color = tempColor;
            }

            i += Time.deltaTime;
            yield return null;
        }

        // Exit invincibility
        hasInvincibility = false;
        foreach (SkinnedMeshRenderer skinMesh in allSkinMesh)
        {
            skinMesh.material.color = originalMaterialColor;
        }
    }

    public bool isInvincible() {
        return hasInvincibility;
    }
}
