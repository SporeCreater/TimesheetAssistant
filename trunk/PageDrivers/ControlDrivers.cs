using System;
using System.Collections.Specialized;
using WatiN.Core;

namespace PageDrivers
{
    public abstract class WatinControlDriver
    {
        protected Element _element;

        public string Id { get; private set; }

        protected WatinControlDriver(string id, Element element, PageDriver parent)
        {
            Id = id;
            _element = element;
            parent.Register(this);
        }

        public virtual bool Verify()
        {
            return _element != null && _element.Exists;
        }
    }

    public class WatinButton : WatinControlDriver
    {
        public WatinButton(IE ie, PageDriver parent, string id): base(id, ie.Button(Find.ById(id)), parent)
        {
        }

        public void Click()
        {
            _element.Click();
        }
    }

    public class WatinTextField : WatinControlDriver
    {
        public WatinTextField(IE ie, PageDriver parent, string id): base(id, ie.TextField(Find.ById(id)), parent)
        {
        }

        public void TypeText(string text)
        {
            ((TextField)_element).TypeText(text);
        }
    }

    public class WatinSpan: WatinControlDriver
    {
        public WatinSpan(IE ie, PageDriver parent, string id): base(id, ie.Span(Find.ById(id)), parent)
        {
        }

        public string Text()
        {
            return ((Span) _element).Text;
        }
    }

    public class WatinSelectList: WatinControlDriver
    {
        public WatinSelectList(IE ie, PageDriver parent, string id): base(id, ie.SelectList(Find.ById(id)), parent)
        {
        }

        public string SelectedValue
        {
            get { return ((SelectList) _element).SelectedItem; }
        }

        public void SelectByValue(string value)
        {
            ((SelectList)_element).SelectByValue(value);
        }

        public void Select(string value)
        {
            ((SelectList)_element).Select(value);
        }

        public StringCollection AllContents()
        {
            return ((SelectList) _element).AllContents;
        }
    }
}
