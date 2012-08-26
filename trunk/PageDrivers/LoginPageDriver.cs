using WatiN.Core;

namespace PageDrivers
{
    public class LoginPageDriver: PageDriver
    {
        public WatinTextField UserNameField { get; private set; }
        public WatinTextField PwdField { get; private set; }
        public WatinButton LoginButton { get; private set; }

        public LoginPageDriver(IE ie)
        {
            UserNameField = new WatinTextField(ie, this, "txtUserName");
            PwdField = new WatinTextField(ie, this, "txtPassword");
            LoginButton = new WatinButton(ie, this, "lBtnLogin");
        }
    }
}