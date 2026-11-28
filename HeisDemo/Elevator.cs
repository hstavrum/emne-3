// Elevator koordinerer tre objekter med hvert sitt ansvar:
// bestillinger, bevegelse og dør. Program.cs kan ikke endre tilstanden direkte.
public class Elevator
{
    private readonly ElevatorRequests _requests;
    private readonly ElevatorMovement _movement;
    private readonly ElevatorDoor _door;
    private ElevatorRequest? _currentRequest;

    // Samme offentlige grensesnitt som før: Program.cs og ElevatorView er urørt.
    public int FloorCount => _movement.FloorCount;
    public int PositionQuarter => _movement.PositionQuarter;
    public bool DoorOpen => _door.IsOpen;
    public int? TargetFloor => _currentRequest?.Floor;

    public Elevator(int floorCount)
    {
        _requests = new ElevatorRequests(floorCount);
        _movement = new ElevatorMovement(floorCount);
        _door = new ElevatorDoor();
    }

    public bool RequestFromCabin(int floor)
    {
        return _requests.Activate(floor, RequestType.Cabin);
    }

    public bool CallFromHall(int floor, bool up)
    {
        RequestType type = up ? RequestType.FloorUp : RequestType.FloorDown;
        return _requests.Activate(floor, type);
    }

    // Knapper kan leses, men klienten får aldri tilgang til request-arrayet.
    public bool CabButtonLit(int floor) => _requests.IsActive(floor, RequestType.Cabin);
    public bool UpButtonLit(int floor) => _requests.IsActive(floor, RequestType.FloorUp);
    public bool DownButtonLit(int floor) => _requests.IsActive(floor, RequestType.FloorDown);

    public void Tick()
    {
        if (_door.IsOpen)
        {
            _door.Tick();
            return; // Ikke kjør mens døren er åpen.
        }

        if (_currentRequest == null)
        {
            _currentRequest = _requests.FindNearestActive(_movement.CurrentFloor);
            if (_currentRequest == null) return;
        }

        // Behold hovedmålet, men sjekk hver etasje vi passerer.
        // Kabinknapper og uteknapper i samme kjøreretning gir mellomstopp.
        int direction = _movement.DirectionTo(_currentRequest.Floor);
        bool atDestination = _movement.StepTowards(_currentRequest.Floor);

        if (!_movement.IsAtFloor) return;

        int floor = _movement.CurrentFloor;
        if (!atDestination && !_requests.HasStopAtFloor(floor, direction)) return;

        // Ved mellomstopp betjenes bare riktig retning.
        // Ved endelig mål betjener vi alle knapper på den etasjen.
        _requests.CompleteAtFloor(floor, atDestination ? 0 : direction);

        if (atDestination) _currentRequest = null;
        _door.Open();
    }
}
