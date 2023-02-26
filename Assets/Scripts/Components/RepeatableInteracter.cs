public class RepeatableInteracter : Interacter
{
    internal override void OnInteractionOccured(IInteractionResult[] recivers)
    {
        foreach (IInteractionResult item in recivers)
        {
            item.CompleteInteraction(); 
        }
    }
}