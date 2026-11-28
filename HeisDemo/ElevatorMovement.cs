// Denne klassen kjenner den animerte posisjonen og flytter ett steg per Tick.
public class ElevatorMovement
{
    private const int StepsPerFloor = 4;
    private int _positionQuarter;

    public int FloorCount { get; }
    public int PositionQuarter => _positionQuarter;
    public bool IsAtFloor => _positionQuarter % StepsPerFloor == 0;

    // Brukes når heisen står stille, ved en bestemt etasje.
    public int CurrentFloor => _positionQuarter / StepsPerFloor + 1;

    public ElevatorMovement(int floorCount)
    {
        if (floorCount < 2 || floorCount > 9)
            throw new ArgumentOutOfRangeException(nameof(floorCount));

        FloorCount = floorCount;
    }

    // +1 betyr opp, -1 betyr ned, 0 betyr at vi er framme.
    public int DirectionTo(int floor)
    {
        if (floor < 1 || floor > FloorCount)
            throw new ArgumentOutOfRangeException(nameof(floor));

        int targetPosition = (floor - 1) * StepsPerFloor;
        return Math.Sign(targetPosition - _positionQuarter);
    }

    // Returnerer true når heisen er framme.
    public bool StepTowards(int floor)
    {
        if (floor < 1 || floor > FloorCount)
            throw new ArgumentOutOfRangeException(nameof(floor));

        int targetPosition = (floor - 1) * StepsPerFloor;

        if (_positionQuarter < targetPosition) _positionQuarter++;
        else if (_positionQuarter > targetPosition) _positionQuarter--;

        return _positionQuarter == targetPosition;
    }
}
