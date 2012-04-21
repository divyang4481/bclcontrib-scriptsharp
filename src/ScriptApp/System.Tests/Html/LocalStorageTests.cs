using System;
using SystemEx.Html;
using System.Specialized.JsTestRunner;
namespace Html
{
    public class LocalStorageTests
    {
        static LocalStorageTests() { TestCaseBuilder.TestCase("LocalStorage", typeof(LocalStorageTests).Prototype); }

        public void tearDown()
        {
            // clear localstorage
            while (LocalStorage.Length > 0)
                LocalStorage.RemoveItem(LocalStorage.Key(0));
        }

        public void TestGetItem_fails_for_missing_item()
        {
            //Asserts.AssertException(delegate()
            //{
            //    LocalStorage.GetItem(null);
            //}, "Error");
        }

        public void TestGetItem_passes_when_set_item_requested()
        {
            LocalStorage.SetItem("Key", "Value");
            Asserts.AssertEquals("Value", LocalStorage.GetItem("Key"));
        }

        public void TestKey_fails_for_outofbounds()
        {
            Asserts.AssertException(delegate()
            {
                LocalStorage.Key(0);
            }, "Error");
        }

        public void TestKey_passes_when_set_item_key_requested()
        {
            LocalStorage.SetItem("Key", "Value");
            Asserts.AssertEquals("Key", LocalStorage.Key(0));
        }

        public void TestLenght_passes_when_set_single_item_returns_one()
        {
            LocalStorage.SetItem("Key", "Value");
            Asserts.AssertEquals(1, LocalStorage.Length);
        }

        public void TestRemoveItem_fails_when_item_doenst_exist()
        {
            //Asserts.AssertException(delegate()
            //{
            //    LocalStorage.RemoveItem("Key");
            //}, "Error");
        }

        public void TestRemoveItem_passes_when_item_exists_and_items_removed()
        {
            LocalStorage.SetItem("Key", "Value");
            LocalStorage.RemoveItem("Key");
            Asserts.AssertEquals(0, LocalStorage.Length);
        }

        public void TestSetItem_fails_when_set_item_exists()
        {
            //Asserts.AssertException(delegate()
            //{
            //    LocalStorage.SetItem(null, "Value");
            //}, "Error");
        }

        public void TestSetItem_passes_when_set_item_exists()
        {
            LocalStorage.SetItem("Key", "Value");
            Asserts.AssertEquals("Value", LocalStorage.GetItem("Key"));
        }

        public void TestSetItem_passes_when_set_item_duplicated()
        {
            LocalStorage.SetItem("Key", "Value");
            LocalStorage.SetItem("Key", "Value2");
            Asserts.AssertEquals("Value2", LocalStorage.GetItem("Key"));
        }
    }
}