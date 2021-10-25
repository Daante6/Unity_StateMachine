using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchFor : IState
{
    private LayerMask searchLayer;
    private GameObject ownerGameObject;
    private float searchRadius;
    private string tagToLookFor;
    public bool SearchCompleted;
    private System.Action<SearchResults> searchResultCallback;

    public SearchFor(LayerMask searchLayer, GameObject ownerGameObject, float searchRadius, string tagToLookFor, System.Action<SearchResults> searchResultCallback)
    {
        this.searchLayer = searchLayer;
        this.ownerGameObject = ownerGameObject;
        this.searchRadius = searchRadius;
        this.tagToLookFor = tagToLookFor;
        this.searchResultCallback = searchResultCallback;
    }

    public void Enter()
    {
        
    }

    public void Execute()
    {
        if (!SearchCompleted)
        {
            var hitObjects = Physics.OverlapSphere(this.ownerGameObject.transform.position, this.searchRadius);
            List<Collider> allObjectsWithTheRequiredTag = new List<Collider>();
            for (int i = 0; i < hitObjects.Length; i++)
            {
                if (hitObjects[i].CompareTag(this.tagToLookFor))
                {
                    //this.navMeshAgent.SetDestination(hitObjects[i].transform.position);
                    allObjectsWithTheRequiredTag.Add(hitObjects[i]);
                }
            }
            var searchResult = new SearchResults(hitObjects, allObjectsWithTheRequiredTag);
            this.searchResultCallback(searchResult);
            this.SearchCompleted = true;
        }
    }

    public void Exit()
    {
        
    }
}

public class SearchResults
{
    public Collider[] allHitObjectsInSearchRadius;
    public List<Collider> allHitObjectsWithRequiredTag;
    //other data, like nearest/farthest object
    public SearchResults(Collider[] allHitObjectsInSearchRadius, List<Collider> allHitObjectsWithRequiredTag)
    {
        this.allHitObjectsInSearchRadius = allHitObjectsInSearchRadius;
        this.allHitObjectsWithRequiredTag = allHitObjectsWithRequiredTag;
        //methods involving more processing,math
    }
}
