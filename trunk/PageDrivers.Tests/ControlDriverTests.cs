using System;
using System.Collections;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;

namespace PageDrivers.Tests
{
    public class ControlDriverForTesting : WatinControlDriver
    {
        public enum VerificationBehaviour { Pass, Fail, CallSuperClass }

        public bool WasVerified;
        public VerificationBehaviour VerificationResult = VerificationBehaviour.Pass;

        public ControlDriverForTesting(string id, Element element, PageDriver parent) : base(id, element, parent)
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

    public  class DriverPageForTesting : PageDriver
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
            var ctrl = new ControlDriverForTesting("foo", _ie.Element("foo"), new DriverPageForTesting());
            ctrl.Id.Should().Be("foo");
        }

        [TestMethod]
        public void control_driver_verification_passes_when_element_exists_on_the_page()
        {
            var ctrl = new ControlDriverForTesting("lBtnLogin", _ie.Element("lBtnLogin"), new DriverPageForTesting())
                           {
                               VerificationResult = ControlDriverForTesting.VerificationBehaviour.CallSuperClass
                           };
            ctrl.Verify().Should().BeTrue();
        }

        [TestMethod]
        public void control_driver_verification_fails_if_doesnt_exist_in_page()
        {
            var ctrl = new ControlDriverForTesting("foobar", _ie.Element("foobar"), new DriverPageForTesting())
                           {
                               VerificationResult = ControlDriverForTesting.VerificationBehaviour.CallSuperClass
                           };
            ctrl.Verify().Should().BeFalse();
        }

        [TestMethod]
        public void control_drivers_attach_to_parent_page_on_creation()
        {
            var parent = new DriverPageForTesting();

            var ctrl = new ControlDriverForTesting("foo", _ie.Element(Find.ById("foo")), parent);

            parent.RegisteredControls.Should().Contain(ctrl);
        }

        [TestMethod]
        public void page_drivers_verify_all_registered_controls()
        {
            var parent = new DriverPageForTesting();

            var c1 = new ControlDriverForTesting("foo", _ie.Element(Find.ById("foo")), parent);
            var c2 = new ControlDriverForTesting("bar", _ie.Element(Find.ById("bar")), parent);
            var c3 = new ControlDriverForTesting("baz", _ie.Element(Find.ById("baz")), parent);

            parent.Verify();

            c1.WasVerified.Should().BeTrue();
            c2.WasVerified.Should().BeTrue();
            c3.WasVerified.Should().BeTrue();
        }

        [TestMethod]
        public void page_verification_returns_ids_controls_that_failed_verification()
        {
            var parent = new DriverPageForTesting();

            var c1 = new ControlDriverForTesting("foo", _ie.Element(Find.ById("foo")), parent)
                         {
                             VerificationResult = ControlDriverForTesting.VerificationBehaviour.Fail
                         };
            var c2 = new ControlDriverForTesting("bar", _ie.Element(Find.ById("bar")), parent);
            var c3 = new ControlDriverForTesting("baz", _ie.Element(Find.ById("bar")), parent)
                         {
                             VerificationResult = ControlDriverForTesting.VerificationBehaviour.Fail
                         };
            
            parent.Verify().Should().Be("foo baz ");
        }
    }
}
