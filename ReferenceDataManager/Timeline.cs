using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class Timeline<TRefence>
        where TRefence : IComparable<TRefence>
    {
        private readonly string name;
        private readonly List<Point<TRefence>> points;

        public Timeline(string name, IEnumerable<Point<TRefence>> points)
        {
            this.name = name;
            this.points = points.ToList();
        }

        public ChangeSetId? GetLastValidChangeSet(TRefence actualValue)
        {
            Point<TRefence> previousPoint = null;
            foreach (var point in Points)
            {
                if (!point.IsValidFor(actualValue))
                {
                    return previousPoint != null 
                        ? previousPoint.ChangeSetId 
                        : (ChangeSetId?)null;
                }
                previousPoint = point;
            }
            return null;
        }

        public IEnumerable<Point<TRefence>> Points
        {
            get { return points; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}