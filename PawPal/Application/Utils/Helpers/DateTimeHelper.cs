namespace Application.Utils.Helpers;

public class DateTimeHelper
{
    public static bool AreTimesConflicting(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
    {
        if (start1 >= start2 && start1 < end2)
            return true;

        if (end1 <= end2 && end1 > start2)
            return true;

        return false;
    }
}
