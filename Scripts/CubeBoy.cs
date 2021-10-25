using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class CubeBoy : MonoBehaviour
{
    StateMachine stateMachine = new StateMachine();
    [SerializeField]
    private LayerMask FoodItemsLayer;
    [SerializeField]
    private float viewRange;
    [SerializeField]
    string foodItemsTag;
    NavMeshAgent NavMeshAgent;
    private void Start()
    {
        this.NavMeshAgent = this.GetComponent<NavMeshAgent>();
        this.stateMachine.ChangeState(new SearchFor(this.FoodItemsLayer, this.gameObject, this.viewRange, this.foodItemsTag, this.FoodFound ));
    }
    private void Update()
    {
        this.stateMachine.ExecuteStateUpdate();
    }
    public void FoodFound(SearchResults searchResult)
    {
        var foundFoodItems = searchResult.allHitObjectsWithRequiredTag;
        //decide what to eat
        //trigger eating cardridge here
        NavMeshAgent.SetDestination(foundFoodItems[0].transform.position);
    }
    public void TriggerEating()
    {
        
    }
}
