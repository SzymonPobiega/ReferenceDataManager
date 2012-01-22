using System;
using System.Linq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class TimelineTests
    {
        [Test]
        public void If_there_are_no_points_in_the_timeline_the_last_valid_change_set_is_null()
        {
            var timeline = new Timeline("Timeline", new TestingReferenceValueType(), new Point[] {});

            var lastValid = timeline.GetLastValidChangeSet(int.MaxValue);

            Assert.IsNull(lastValid);
        }

        [Test]
        public void If_throws_when_trying_to_add_point_with_incompatible_type()
        {
            var timeline = new Timeline("Timeline", new TestingReferenceValueType(), new Point[] { });

            TestDelegate act = () => timeline.AddPoint(new Point(ChangeSetId.NewUniqueId(), 100m));

            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void It_adds_point_to_the_timeline()
        {
            var timeline = new Timeline("Timeline", new TestingReferenceValueType(), new Point[] { });

            timeline.AddPoint(new Point(ChangeSetId.NewUniqueId(), timeline.ReferenceType.GetCurrentValue()));

            Assert.AreEqual(1, timeline.Points.Count());
        }

        [Test]
        public void It_removes_point_from_the_timeline()
        {
            var firstChangeSet = ChangeSetId.NewUniqueId();
            var secondChangeSet = ChangeSetId.NewUniqueId();
            var thirdChangeSet = ChangeSetId.NewUniqueId();

            var timeline = new Timeline("Timeline", new TestingReferenceValueType(),
                new[]
                    {
                        new Point(firstChangeSet, 10),
                        new Point(secondChangeSet, 20),
                        new Point(thirdChangeSet, 30)
                    });

            timeline.RemovePointFor(secondChangeSet);

            Assert.AreEqual(2, timeline.Points.Count());
            Assert.IsTrue(timeline.Points.Any(x => x.ChangeSetId == firstChangeSet));
            Assert.IsTrue(timeline.Points.Any(x => x.ChangeSetId == thirdChangeSet));
        }

        [Test]
        public void Last_valid_change_set_is_the_one_with_the_greatest_reference_value_smaller_or_equal_the_actual_value()
        {
            var firstChangeSet = ChangeSetId.NewUniqueId();
            var secondChangeSet = ChangeSetId.NewUniqueId();
            var thirdChangeSet = ChangeSetId.NewUniqueId();

            var timeline = new Timeline("Timeline", new TestingReferenceValueType(), 
                new[]
                    {
                        new Point(firstChangeSet, 10),
                        new Point(secondChangeSet, 20),
                        new Point(thirdChangeSet, 30)
                    });


            Assert.AreEqual(secondChangeSet, timeline.GetLastValidChangeSet(25));
            Assert.AreEqual(secondChangeSet, timeline.GetLastValidChangeSet(20));
            Assert.AreEqual(firstChangeSet, timeline.GetLastValidChangeSet(19));
        }

        private class TestingReferenceValueType : IReferenceValueType
        {
            private int currentValue;

            public IComparable GetCurrentValue()
            {
                currentValue++;
                return currentValue;
            }

            public bool IsCompatible(IComparable value)
            {
                return value.GetType() == typeof (int);
            }
        }
    }
}
// ReSharper restore InconsistentNaming
