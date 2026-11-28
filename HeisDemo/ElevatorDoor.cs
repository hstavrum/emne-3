// Døren styrer selv hvor lenge den er åpen.
public class ElevatorDoor
{
    private const int OpenDurationTicks = 7;
    private int _ticksRemaining;

    public bool IsOpen => _ticksRemaining > 0;

    public void Open()
    {
        _ticksRemaining = OpenDurationTicks;
    }

    public void Tick()
    {
        if (_ticksRemaining > 0) _ticksRemaining--;
    }
}
