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
            var timeline = new Timeline<int>("Timeline", new Point<int>[] {});

            var lastValid = timeline.GetLastValidChangeSet(int.MaxValue);

            Assert.IsNull(lastValid);
        }

        [Test]
        public void Last_valid_change_set_is_the_one_with_the_greatest_reference_value_smaller_or_equal_the_actual_value()
        {
            var firstChangeSet = ChangeSetId.NewUniqueId();
            var secondChangeSet = ChangeSetId.NewUniqueId();
            var thirdChangeSet = ChangeSetId.NewUniqueId();

            var timeline = new Timeline<int>("Timeline", 
                new[]
                    {
                        new Point<int>(firstChangeSet, 10),
                        new Point<int>(secondChangeSet, 20),
                        new Point<int>(thirdChangeSet, 30)
                    });


            Assert.AreEqual(secondChangeSet, timeline.GetLastValidChangeSet(25));
            Assert.AreEqual(secondChangeSet, timeline.GetLastValidChangeSet(20));
            Assert.AreEqual(firstChangeSet, timeline.GetLastValidChangeSet(19));
        }
    }
}
// ReSharper restore InconsistentNaming
