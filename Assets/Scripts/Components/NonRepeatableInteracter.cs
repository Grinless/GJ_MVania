public class NonRepeatableInteracter : Interacter
{
    private void Awake()
    {
        base.repeatable = false; 
    }

    internal override void OnInteractionOccured(IInteractionResult[] recivers)
    {
        foreach (IInteractionResult item in recivers)
        {
            item.CompleteInteraction();
        }
    }
}
