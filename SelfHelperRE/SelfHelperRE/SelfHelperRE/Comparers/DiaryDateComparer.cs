using SelfHelperRE.Models;
using System.Collections.Generic;

namespace SelfHelperRE
{
    public class DiaryDateComparer : IEqualityComparer<DiaryData>
    {
        bool IEqualityComparer<DiaryData>.Equals(DiaryData x, DiaryData y)
        {
            if (x.DateTime.Date == y.DateTime.Date)
                return true;

            return false;
        }

        int IEqualityComparer<DiaryData>.GetHashCode(DiaryData obj)
        {
            return 0;
        }
    }
}
