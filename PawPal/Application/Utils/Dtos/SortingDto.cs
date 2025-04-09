namespace Application.Utils.Dtos;

public class SortingDto<TSortingType> where TSortingType : Enum
{
    public TSortingType? Type { get; set; }

    public SortingDirection? Direction { get; set; }
}
