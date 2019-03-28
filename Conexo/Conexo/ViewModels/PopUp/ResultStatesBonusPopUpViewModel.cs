using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Conexo.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Conexo.ViewModels.PopUp
{
    public class ResultStatesBonusPopUpViewModel : BaseViewModel
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string NoActionLabel { get; set; }

        public ICommand CancelTapCommand { get { return new Command(async () => { await DestroyThisModalAsync(); }); } }

        public ResultStatesBonusPopUpViewModel(string title, string description, string noActionLabel)
        {

            this.Title = title;
            this.Description = description;
            this.NoActionLabel = noActionLabel;

        }

        public async Task DestroyThisModalAsync()
        {
            await PopupNavigation.Instance.PopAsync();
        }

    }

}