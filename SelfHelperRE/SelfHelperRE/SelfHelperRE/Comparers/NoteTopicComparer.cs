using SelfHelperRE.Models;
using System.Collections.Generic;

namespace SelfHelper.Comparers
{
    public class NoteTopicComparer : IEqualityComparer<NoteData>
    {
        bool IEqualityComparer<NoteData>.Equals(NoteData x, NoteData y)
        {
            if (x.Topic == y.Topic)
                return true;

            return false;
        }

        int IEqualityComparer<NoteData>.GetHashCode(NoteData obj)
        {
            return 0;
        }
    }
}
