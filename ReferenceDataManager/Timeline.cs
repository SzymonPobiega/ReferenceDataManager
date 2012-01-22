using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class Timeline
    {
        private readonly string name;
        private readonly IReferenceValueType referenceType;
        private readonly List<Point> points;

        public Timeline(string name, IReferenceValueType referenceType, IEnumerable<Point> points)
        {
            this.name = name;
            this.referenceType = referenceType;
            this.points = points.ToList();
        }

        public IReferenceValueType ReferenceType
        {
            get { return referenceType; }
        }

        public void AddPoint(Point point)
        {
            if (!referenceType.IsCompatible(point.ReferenceValue))
            {
                throw new InvalidOperationException(string.Format("Point's reference value {0} is not compatible with this timeline's reference value type: {1}",
                    point.ReferenceValue, referenceType.GetType().Name));
            }
            points.Add(point);
        }

        public void RemovePointFor(ChangeSetId changeSetId)
        {
            points.RemoveAll(x => x.ChangeSetId == changeSetId);
        }

        public ChangeSetId? GetLastValidChangeSet(IComparable actualValue)
        {
            Point previousPoint = null;
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

        public IEnumerable<Point> Points
        {
            get { return points; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}