using System;

namespace Boundaries
{
    public interface ILoginView
    {
        void ShowErrorMessage(string errorMessage);
        void SetCurrentWeek(LoginResponse response);
    }
}
