public class NonRepeatableInteracter : Interacter
{
    public IInteractionResult interactResult;
    public bool interactedWith = false;

    internal override void OnInteractionOccured()
    {
        if (!interactedWith)
        {
            interactedWith = true;
            interactResult.CompleteInteraction();
        }
    }
}
