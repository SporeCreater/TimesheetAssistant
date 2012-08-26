using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;

namespace PageDrivers.Tests
{
    public class TestControlDriver : WatinControlDriver
    {
        public enum VerificationBehaviour { Pass, Fail, CallSuperClass }

        public bool WasVerified;
        public VerificationBehaviour VerificationResult = VerificationBehaviour.Pass;

        public TestControlDriver(string id, Element element, PageDriver parent) : base(id, element, parent)
        {
        }

        public override bool Verify()
        {
            WasVerified = true;

            if (VerificationResult == VerificationBehaviour.CallSuperClass)
            {
                return base.Verify();
            }
            return VerificationResult == VerificationBehaviour.Pass;
        }
    }

    public  class TestDriverPage : PageDriver
    {
        public ICollection RegisteredControls
        {
            get { return _controls; }
        }
    }

    [TestClass]
    public class ControlDriverTests : WatinDriverTestSuite
    {
        [TestMethod]
        public void control_drivers_have_id()
        {
            var ctrl = new TestControlDriver("foo", _ie.Element("foo"), new TestDriverPage());
            Assert.AreEqual("foo", ctrl.Id);
        }

        [TestMethod]
        public void control_driver_verification_passes_when_element_exists_on_the_page()
        {
            var ctrl = new TestControlDriver("lBtnLogin", _ie.Element("lBtnLogin"), new TestDriverPage())
                           {
                               VerificationResult = TestControlDriver.VerificationBehaviour.CallSuperClass
                           };
            Assert.IsTrue(ctrl.Verify());
        }

        [TestMethod]
        public void control_driver_verification_fails_if_doesnt_exist_in_page()
        {
            var ctrl = new TestControlDriver("foobar", _ie.Element("foobar"), new TestDriverPage())
                           {
                               VerificationResult = TestControlDriver.VerificationBehaviour.CallSuperClass
                           };
            Assert.IsFalse(ctrl.Verify());
        }

        [TestMethod]
        public void control_drivers_attach_to_parent_page_on_creation()
        {
            var parent = new TestDriverPage();

            var ctrl = new TestControlDriver("foo", _ie.Element(Find.ById("foo")), parent);

            CollectionAssert.Contains(parent.RegisteredControls, ctrl);
        }

        [TestMethod]
        public void page_drivers_verify_all_registered_controls()
        {
            var parent = new TestDriverPage();

            var c1 = new TestControlDriver("foo", _ie.Element(Find.ById("foo")), parent);
            var c2 = new TestControlDriver("bar", _ie.Element(Find.ById("bar")), parent);
            var c3 = new TestControlDriver("baz", _ie.Element(Find.ById("baz")), parent);

            parent.Verify();

            Assert.IsTrue(c1.WasVerified);
            Assert.IsTrue(c2.WasVerified);
            Assert.IsTrue(c3.WasVerified);
        }

        [TestMethod]
        public void page_verification_returns_ids_controls_that_failed_verification()
        {
            var parent = new TestDriverPage();

            var c1 = new TestControlDriver("foo", _ie.Element(Find.ById("foo")), parent)
                         {
                             VerificationResult = TestControlDriver.VerificationBehaviour.Fail
                         };
            var c2 = new TestControlDriver("bar", _ie.Element(Find.ById("bar")), parent);
            var c3 = new TestControlDriver("baz", _ie.Element(Find.ById("bar")), parent)
                         {
                             VerificationResult = TestControlDriver.VerificationBehaviour.Fail
                         };

            Assert.AreEqual("foo baz ", parent.Verify());
        }
    }
}
