using SelfHelperRE.Models;
using System;
using System.Collections.Generic;

namespace SelfHelperRE
{
    public class DiaryDateComparer : IEqualityComparer<DiaryCatch>
    {
        bool IEqualityComparer<DiaryCatch>.Equals(DiaryCatch x, DiaryCatch y)
        {
            if (Convert.ToDateTime(x.DateTime).Date == Convert.ToDateTime(y.DateTime).Date)
                return true;

            return false;
        }

        int IEqualityComparer<DiaryCatch>.GetHashCode(DiaryCatch obj)
        {
            return 0;
        }
    }
}
