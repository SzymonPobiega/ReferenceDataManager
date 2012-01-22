using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ReferenceDataManager.Sample.Web.Models;

// ReSharper disable InconsistentNaming

namespace ReferenceDataManager.Tests
{
    [TestFixture]
    public class ChangeSetTreeViewModelFactoryTests
    {
        [Test]
        public void It_distinguishes_root_change_sets()
        {
            var firstRootChangeSet = CreateChangeSet(null);
            var secondRootChangeSet = CreateChangeSet(null);

            var firstChildChangeSet = CreateChangeSet(firstRootChangeSet.Id);
            var secondChildChangeSet = CreateChangeSet(secondRootChangeSet.Id);

            var model = CreateModel(firstRootChangeSet, secondRootChangeSet, firstChildChangeSet, secondChildChangeSet);

            Assert.AreEqual(2, model.RootNodes.Count);
            Assert.IsTrue(model.RootNodes.Any(x => x.Id == firstRootChangeSet.Id.ToString()));
            Assert.IsTrue(model.RootNodes.Any(x => x.Id == secondRootChangeSet.Id.ToString()));
        }
        
        [Test]
        public void It_attaches_child_change_sets_to_the_parent()
        {
            var rootChangeSet = CreateChangeSet(null);

            var firstChildChangeSet = CreateChangeSet(rootChangeSet.Id);
            var secondChildChangeSet = CreateChangeSet(rootChangeSet.Id);

            var model = CreateModel(rootChangeSet, firstChildChangeSet, secondChildChangeSet);

            Assert.AreEqual(1, model.RootNodes.Count);
            var rootModel = model.RootNodes[0];
            Assert.AreEqual(rootChangeSet.Id.ToString(), rootModel.Id);
            Assert.AreEqual(2, rootModel.Children.Count);
        }

        [Test]
        public void It_appends_comment_as_title_if_it_is_shorter_or_equal_to_30_characters()
        {
            var changeSet = new ChangeSet(ChangeSetId.NewUniqueId(), null, "Comment with exactly  30 chars", new AbstractCommand[] { });

            var model = CreateModel(changeSet);

            Assert.AreEqual("Comment with exactly  30 chars", model.RootNodes[0].Title);
        }

        [Test]
        public void It_appends_substring_of_comment_and_ellipsis_as_title_if_it_is_longer_than_30_characters()
        {
            var changeSet = new ChangeSet(ChangeSetId.NewUniqueId(), null, "Comment longer than 30 characters", new AbstractCommand[] { });

            var model = CreateModel(changeSet);

            Assert.AreEqual("Comment longer than 30 char...", model.RootNodes[0].Title);
        }

        private static ChangeSet CreateChangeSet(ChangeSetId? parentChangeSetId)
        {
            return new ChangeSet(ChangeSetId.NewUniqueId(), parentChangeSetId, "Some comment", new AbstractCommand[] { });
        }

        private static ChangeSetTreeViewModel CreateModel(params ChangeSet[] changeSets)
        {
            return new ChangeSetTreeViewModelFactory().Create(new List<ChangeSet>(changeSets));
        }
    }
}
// ReSharper restore InconsistentNaming
