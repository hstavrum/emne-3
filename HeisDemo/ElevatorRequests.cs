// Alle mulige knappetrykk opprettes på forhånd.
// En bestilling aktiveres/deaktiveres i stedet for å legges til eller fjernes.
public class ElevatorRequests
{
    private readonly ElevatorRequest[] _requests;

    public ElevatorRequests(int floorCount)
    {
        if (floorCount < 2 || floorCount > 9)
            throw new ArgumentOutOfRangeException(nameof(floorCount));

        // N knapper i heisen, N-1 oppknapper og N-1 nedknapper.
        _requests = new ElevatorRequest[3 * floorCount - 2];
        int index = 0;

        for (int floor = 1; floor <= floorCount; floor++)
        {
            _requests[index++] = new ElevatorRequest(floor, RequestType.Cabin);
        }

        for (int floor = 1; floor < floorCount; floor++)
        {
            _requests[index++] = new ElevatorRequest(floor, RequestType.FloorUp);
        }

        for (int floor = 2; floor <= floorCount; floor++)
        {
            _requests[index++] = new ElevatorRequest(floor, RequestType.FloorDown);
        }
    }

    public bool Activate(int floor, RequestType type)
    {
        ElevatorRequest? request = Find(floor, type);
        if (request == null) return false;

        request.Activate();
        return true;
    }

    public bool IsActive(int floor, RequestType type)
    {
        ElevatorRequest? request = Find(floor, type);
        return request != null && request.IsActive;
    }

    // Heisen velger nærmeste aktive bestilling når den står stille.
    public ElevatorRequest? FindNearestActive(int currentFloor)
    {
        ElevatorRequest? nearest = null;
        int shortestDistance = int.MaxValue;

        foreach (ElevatorRequest request in _requests)
        {
            if (!request.IsActive) continue;

            int distance = Math.Abs(request.Floor - currentFloor);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = request;
            }
        }

        return nearest;
    }

    // Mellomstopp: kabinknapper og uteknapper i kjøreretningen.
    // direction = 0 betyr at vi har kommet til hovedmålet; da betjener vi
    // alle knappene i den etasjen, også ved motsatt uteknapp.
    public bool HasStopAtFloor(int floor, int direction)
    {
        return IsActive(floor, RequestType.Cabin)
            || direction > 0 && IsActive(floor, RequestType.FloorUp)
            || direction < 0 && IsActive(floor, RequestType.FloorDown);
    }

    public void CompleteAtFloor(int floor, int direction)
    {
        foreach (ElevatorRequest request in _requests)
        {
            if (request.Floor != floor) continue;

            bool sameDirection = direction > 0 && request.Type == RequestType.FloorUp
                || direction < 0 && request.Type == RequestType.FloorDown;

            if (direction == 0 || request.Type == RequestType.Cabin || sameDirection)
                request.Complete();
        }
    }

    private ElevatorRequest? Find(int floor, RequestType type)
    {
        foreach (ElevatorRequest request in _requests)
        {
            if (request.Floor == floor && request.Type == type)
                return request;
        }

        return null;
    }
}
