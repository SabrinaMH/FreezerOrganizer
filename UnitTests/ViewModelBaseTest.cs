using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FreezerOrganizer.ViewModel.BaseClasses;

namespace UnitTests
{
    [TestClass]
    public class ViewModelBaseTest
    {
        private TestViewModel _testViewModel;
        private bool _propertyChangedRaised;

        [TestInitialize]
        public void InitTest()
        { 
            _testViewModel = new TestViewModel();
            _propertyChangedRaised = false;
            _testViewModel.PropertyChanged += (sender, e) => _propertyChangedRaised = true;
        }

        [TestMethod]
        public void ValidPropertyNameTest()
        {
            _testViewModel.GoodProperty = "new value";
            Assert.IsTrue(_propertyChangedRaised, "The event wasn't raised for GoodProperty");
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exception), "myMessage")]
        public void InvalidPropertyNameTest()
        {
            _testViewModel.BadProperty = "new value";
        }

        // Need a test class, because I need dummy properties for the test
        private class TestViewModel : ViewModelBase
        {
            public TestViewModel()
            {
                ThrowOnInvalidPropertyName = true;
            }

            public string GoodProperty
            {
                set { OnPropertyChanged("GoodProperty"); }
            }

            public string BadProperty
            {
                set { OnPropertyChanged("PropertyNameWithTypo"); }
            }
        }
    }
}
