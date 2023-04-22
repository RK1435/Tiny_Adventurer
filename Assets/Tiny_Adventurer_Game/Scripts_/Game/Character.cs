using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class Character : MonoBehaviour, IPooledObject_2
{
    private CharacterController characterController_;
    private Vector3 movementVelocity_;
    private PlayerInput playerInput_;
    private float verticalVelocity_;
    private Animator animator_;
    public float moveSpeed_ = 5.0f;
    public float gravity_ = -9.8f;
    public int coin_;

    //Enemy
    public bool isPlayer_ = true;
    private UnityEngine.AI.NavMeshAgent navMeshAgent_;
    private Transform targetPlayer_;

    //Health
    private Health health_;

    //Damager
    private Damager damager_;

    //Player Slides
    private float attackStartTime_;
    [SerializeField] private float attackSlideDuration_ = 0.4f;
    [SerializeField] private float attackSlideSpeed_ = 0.06f;

    private Vector3 impactOnCharacter_;

    [SerializeField] private bool isInvincible_;
    [SerializeField] private float invincibleDuration_ = 2f;

    private float attackAnimationDuration_;

    [SerializeField] private float slideSpeed_ = 9f;

    //State Machine
    public enum CharacterState
    {
        Normal,
        Attacking,
        Dead,
        BeingHit,
        Slide,
        Spawn
    }

    public CharacterState currentState_;

    [SerializeField] private float spawnDuration_ = 2f;
    private float currentSpawnTime_;

    //Material Animation
    private MaterialPropertyBlock materialPropertyBlock_;
    private SkinnedMeshRenderer skinnedMeshRenderer_;

    //[SerializeField] private GameObject itemToDrop;

    private void Awake()
    {
        characterController_ = GetComponent<CharacterController>();
        animator_ = GetComponent<Animator>();
        health_ = GetComponent<Health>();
        damager_ = GetComponentInChildren<Damager>();

        skinnedMeshRenderer_ = GetComponentInChildren<SkinnedMeshRenderer>();
        materialPropertyBlock_ = new MaterialPropertyBlock();
        skinnedMeshRenderer_.GetPropertyBlock(materialPropertyBlock_);

        if(!isPlayer_)
        {
            navMeshAgent_ = GetComponent<UnityEngine.AI.NavMeshAgent>();
            targetPlayer_ = GameObject.FindWithTag("Player").transform;
            navMeshAgent_.speed = moveSpeed_;
            SwitchStateTo(CharacterState.Spawn);
        }
        else
        {
            playerInput_ = GetComponent<PlayerInput>();
        }

    }

    private void CalculatePlayerMovement()
    {
        if (playerInput_.mouseButtonDown_ && characterController_.isGrounded)
        {
            SwitchStateTo(CharacterState.Attacking);
            return;
        }
        else if(playerInput_.spaceKeyDown_ && characterController_.isGrounded)
        {
            SwitchStateTo(CharacterState.Slide);
            return;
        }

        movementVelocity_.Set(playerInput_.HorizontalInput_, 0f, playerInput_.VerticalInput_);
        movementVelocity_.Normalize();
        movementVelocity_ = Quaternion.Euler(0, -45, 0) * movementVelocity_;

        animator_.SetFloat("Speed", movementVelocity_.magnitude);

        movementVelocity_ *= moveSpeed_ * Time.deltaTime;

        if(movementVelocity_ != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity_);
        }

        animator_.SetBool("AirBorne", !characterController_.isGrounded);
    }

    private void CalculateEnemyMovement()
    {
        if(Vector3.Distance(targetPlayer_.position, transform.position) >= navMeshAgent_.stoppingDistance)
        {
            navMeshAgent_.SetDestination(targetPlayer_.position);
            animator_.SetFloat("Speed", 0.2f);
        }
        else
        {
            navMeshAgent_.SetDestination(transform.position);
            animator_.SetFloat("Speed", 0f);

            SwitchStateTo(CharacterState.Attacking);
        }
    }

    private void FixedUpdate()
    {
        switch(currentState_)
        {
            case CharacterState.Normal:

                if (isPlayer_)
                {
                    CalculatePlayerMovement();
                }
                else
                {
                    CalculateEnemyMovement();
                }

                break;

            case CharacterState.Attacking: 

                if(isPlayer_)
                {
                    if(Time.time < attackStartTime_ + attackSlideDuration_)
                    {
                        float timePassed_ = Time.time - attackStartTime_;
                        float lerpTime_ = timePassed_ / attackSlideDuration_;
                        movementVelocity_ = Vector3.Lerp(transform.forward * attackSlideSpeed_, Vector3.zero, lerpTime_);
                    }

                    if(playerInput_.mouseButtonDown_ && characterController_.isGrounded)
                    {
                        string currentClipName = animator_.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                        attackAnimationDuration_ = animator_.GetCurrentAnimatorStateInfo(0).normalizedTime;

                        if(currentClipName != "LittleAdventurerAndie_ATTACK_03" && attackAnimationDuration_ > 0.5f && attackAnimationDuration_ < 0.7f)
                        {
                            playerInput_.mouseButtonDown_ = false;
                            SwitchStateTo(CharacterState.Attacking);
                            //CalculatePlayerMovement(); 
                        }
                    }
                }

                break;

            case CharacterState.Dead:
                return;

            case CharacterState.BeingHit:
                break;

            case CharacterState.Slide:
                movementVelocity_ = transform.forward * slideSpeed_ * Time.deltaTime;
                break;

            case CharacterState.Spawn:
                currentSpawnTime_ -= Time.deltaTime;
                if(currentSpawnTime_ <= 0)
                {
                    SwitchStateTo(CharacterState.Normal);
                }
                break;

        }

        if (impactOnCharacter_.magnitude > 0.2f)
        {
            movementVelocity_ = impactOnCharacter_ * Time.deltaTime;
        }
        impactOnCharacter_ = Vector3.Lerp(impactOnCharacter_, Vector3.zero, Time.deltaTime * 5);

        if (isPlayer_)
        {
            if (characterController_.isGrounded == false)
            {
                verticalVelocity_ = gravity_;
            }
            else
            {
                verticalVelocity_ = gravity_ * 0.3f;
            }

            movementVelocity_ += verticalVelocity_ * Vector3.up * Time.deltaTime;

            characterController_.Move(movementVelocity_);
            movementVelocity_ = Vector3.zero;
        }
        else
        {
            if(currentState_ != CharacterState.Normal)
            {
                characterController_.Move(movementVelocity_);
                movementVelocity_ = Vector3.zero;
            }
        }
    }

    public void SwitchStateTo(CharacterState newState_)
    {
        if(isPlayer_)
        {
            //Clear Cache
            //playerInput_.mouseButtonDown_ = false;
            playerInput_.ClearCache();
        }
        
        //Exiting State

        switch(currentState_) 
        {
            case CharacterState.Normal: 
                break;

            case CharacterState.Attacking:

                if(damager_ != null)
                {
                    DisableDamager();
                }

                if(isPlayer_)
                {
                    GetComponent<PlayerVFXManager>().StopBlade();
                }

                break;

            case CharacterState.Dead:
                return;

            case CharacterState.BeingHit:
                break;

            case CharacterState.Slide:
                break;

            case CharacterState.Spawn:
                isInvincible_ = false;
                break;
        }

        //Entering State

        switch (newState_)
        {
            case CharacterState.Normal:
                break;

            case CharacterState.Attacking:

                if(!isPlayer_)
                {
                    Quaternion newRotation_ = Quaternion.LookRotation(targetPlayer_.position - transform.position);
                    transform.rotation = newRotation_;
                }

                animator_.SetTrigger("Attack");

                if(isPlayer_)
                {
                    attackStartTime_ = Time.time;
                    RotateToCursor();
                }

                break;

            case CharacterState.Dead:

                characterController_.enabled = false;
                animator_.SetTrigger("Dead");
                StartCoroutine(MaterialDissolve());

                if(!isPlayer_)
                {
                    SkinnedMeshRenderer mesh = GetComponentInChildren<SkinnedMeshRenderer>();
                    mesh.gameObject.layer = 0;
                }

                break;

            case CharacterState.BeingHit:

                animator_.SetTrigger("BeingHit");
                if(isPlayer_)
                {
                    isInvincible_ = true;
                    StartCoroutine(DelayCancelInvincible());
                }

                break;

            case CharacterState.Slide:
                animator_.SetTrigger("Slide");
                break;

            case CharacterState.Spawn:
                isInvincible_ = true;
                currentSpawnTime_ = spawnDuration_;
                StartCoroutine(MaterialAppear());
                break;
        }

        currentState_ = newState_;
    }

    public void SlideAnimationEnds()
    {
        SwitchStateTo(CharacterState.Normal);
    }

    public void AttackAnimationEnds()
    {
        SwitchStateTo(CharacterState.Normal);
    }

    public void BeingHitAnimationEnds()
    {
        SwitchStateTo(CharacterState.Normal);
    }

    public void HealthDamage(int damage_, Vector3 attackerPos_ = new Vector3())
    {
        if(isInvincible_)
        {
            return;
        }

        if(health_ != null)
        {
            health_.HealthDamage(damage_);
        }

        if(!isPlayer_)
        {
            GetComponent<EnemyVFXManager>().PlayerBeingHitVFX(attackerPos_);
        }

        StartCoroutine(MaterialBlink());

        if(isPlayer_)
        {
            SwitchStateTo(CharacterState.BeingHit);
            AddImpact(attackerPos_, 10f);
        }
        else
        {
            AddImpact(attackerPos_, 2.5f);
        }
    }

    IEnumerator DelayCancelInvincible()
    {
        yield return new WaitForSeconds(invincibleDuration_);
        isInvincible_ = false;
        yield break;
    }

    private void AddImpact(Vector3 attackerPos, float force)
    {
        Vector3 impactDir = transform.position - attackerPos;
        impactDir.Normalize();
        impactDir.y = 0;
        impactOnCharacter_ = impactDir * force;
    }

    public void EnableDamager()
    {
        damager_.EnableDamager();
    }

    public void DisableDamager()
    {
        damager_.DisableDamager();   
    }

    IEnumerator MaterialBlink()
    {
        materialPropertyBlock_.SetFloat("_blink", 0.4f);
        skinnedMeshRenderer_.SetPropertyBlock(materialPropertyBlock_);

        yield return new WaitForSeconds(0.2f);

        materialPropertyBlock_.SetFloat("_blink", 0f);
        skinnedMeshRenderer_.SetPropertyBlock(materialPropertyBlock_);
        yield break;
    }

    IEnumerator MaterialDissolve()
    {
        yield return new WaitForSeconds(2f);

        float dissolveTimeDuration = 2f;
        float currDissolveTime = 0;
        float dissolveHeight_start = 20f;
        float dissolveHeight_target = -10f;
        float dissolveHeight;

        materialPropertyBlock_.SetFloat("_enableDissolve", 1f);
        skinnedMeshRenderer_.SetPropertyBlock(materialPropertyBlock_);

        while(currDissolveTime < dissolveTimeDuration)
        {
            currDissolveTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(dissolveHeight_start, dissolveHeight_target, currDissolveTime / dissolveTimeDuration);
            materialPropertyBlock_.SetFloat("_dissolve_height", dissolveHeight);
            skinnedMeshRenderer_.SetPropertyBlock(materialPropertyBlock_);
            yield return null;
        }

        EnemyDropItem();

        yield break;
    }

    public void EnemyDropItem()
    {
        #region EnemyItemDrop - Old Code
        //if(itemToDrop != null)
        //{
        //    //Instantiate(itemToDrop, transform.position, Quaternion.identity);

        //}
        #endregion
        GameObject newEnemyDropItem_ = ObjectPooler.objectPoolerInstance.SpawnFromPool_2_GameObject("HealOrb", transform.position, Quaternion.identity);
    }

    public void PickUpItem(PickUp item)
    {
        switch(item.pickUpType_)
        {
            case PickUp.PickUpType.Heal:
                AddHealth(item.value_);
                break;
            case PickUp.PickUpType.Coin:
                AddCoin(item.value_);
                break;

        }
    }

    public void AddHealth(int health)
    {
        health_.AddHealth(health);
        GetComponent<PlayerVFXManager>().PlayHealVFX();
    }

    public void AddCoin(int coin)
    {
        coin_ += coin;
    }

    public void RotateToTarget()
    {
        if(currentState_ != CharacterState.Dead)
        {
            transform.LookAt(targetPlayer_, Vector3.up);
        }
    }

    IEnumerator MaterialAppear()
    {
        float dissolveTimeDuration = spawnDuration_;
        float currentDissolveTime = 0;
        float dissolveHeight_start = -10f;
        float dissolveHeight_target = 20f;
        float dissolveHeight;

        materialPropertyBlock_.SetFloat("_enableDissolve", 1f);
        skinnedMeshRenderer_.SetPropertyBlock(materialPropertyBlock_);

        while(currentDissolveTime < dissolveTimeDuration)
        {
            currentDissolveTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(dissolveHeight_start, dissolveHeight_target, currentDissolveTime / dissolveTimeDuration);
            materialPropertyBlock_.SetFloat("_dissolve_height", dissolveHeight);
            skinnedMeshRenderer_.SetPropertyBlock(materialPropertyBlock_);
            yield return null;
        }

        materialPropertyBlock_.SetFloat("_enableDissolve", 0f);
        skinnedMeshRenderer_.SetPropertyBlock(materialPropertyBlock_);

        yield break;
    }

    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;

        if (Physics.Raycast(ray, out hitResult, 1000, 1 << LayerMask.NameToLayer("CursorTest")))
        {
            Vector3 cursorPos = hitResult.point;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(cursorPos, 1);
        }
    }

    private void RotateToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;

        if (Physics.Raycast(ray, out hitResult, 1000, 1 << LayerMask.NameToLayer("CursorTest")))
        {
            Vector3 cursorPos = hitResult.point;
            transform.rotation = Quaternion.LookRotation(cursorPos - transform.position, Vector3.up);
        }
    }
}
