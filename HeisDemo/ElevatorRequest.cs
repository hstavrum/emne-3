// Ett objekt per fysisk knapp. Etasje og type endres aldri.
public class ElevatorRequest
{
    public int Floor { get; }
    public RequestType Type { get; }
    public bool IsActive { get; private set; }

    public ElevatorRequest(int floor, RequestType type)
    {
        Floor = floor;
        Type = type;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Complete()
    {
        IsActive = false;
    }
}
