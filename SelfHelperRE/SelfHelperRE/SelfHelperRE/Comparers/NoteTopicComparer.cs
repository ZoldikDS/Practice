using SelfHelperRE;
using SelfHelperRE.Models;
using System.Collections.Generic;

namespace SelfHelper.Comparers
{
    public class NoteTopicComparer : IEqualityComparer<NoteCatch>
    {
        bool IEqualityComparer<NoteCatch>.Equals(NoteCatch x, NoteCatch y)
        {
            if (x.Topic == y.Topic)
                return true;

            return false;
        }

        int IEqualityComparer<NoteCatch>.GetHashCode(NoteCatch obj)
        {
            return 0;
        }
    }
}
