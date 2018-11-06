using DTO;

namespace Desktop.UserControls
{
    public enum State
    {
        Reserved,
        Free,
        Selected
    }
    public interface ISeatUserControl
    {
        Seat Seat { get; set; }
        State State { get; }
    }
}
