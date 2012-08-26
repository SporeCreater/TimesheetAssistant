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
}
