using System;
using BlazorBusinessLogic.Models.General;
using BlazorBusinessLogic.Models.UI;
using ModelsInterfaces;

namespace BlazorBusinessLogic
{

    public class StateHolder
    {
        private bool logInState;
        public bool LogInState
        {
            get 
            { return logInState;} 
            set
            {
                logInState = value;
                globalStateChange?.Invoke();
                NaveChange?.Invoke();
            }
        }

        public Action globalStateChange { get; set; }
        public Action NaveChange { get; set; }
        public IUser<Company, XmlTemplate> User { get; set; }
        private PopUp pubUp;
        public PopUp PubUp
        {
            get
            { return pubUp; }
            set
            {
                if (value != null && value.Buttons == null)
                {
                    value.Buttons = new PubUpButtons[]
                    {
                        new PubUpButtons()
                        {
                            Text = "Ok",
                            Event = () => PubUp = null
                        }
                    };
                }
                pubUp = value;
                globalStateChange?.Invoke();
            }
        }

        public bool Online { get; set; } = true;
    }
}
