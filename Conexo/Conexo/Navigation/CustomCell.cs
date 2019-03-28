using Xamarin.Forms;

namespace Conexo.Navigation
{
    public class CustomCell : ViewCell
    {

        public CustomCell()
        {
            Image image = new Image()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = 20,
                Aspect = Aspect.AspectFit
            };
            image.SetBinding(Image.SourceProperty, new Binding("Img"));

            Label label = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Color.Black
            };
            label.SetBinding(Label.TextProperty, new Binding("Title"));

            StackLayout stackLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(10, 5),
                Children = {image, label}
            };

            View = stackLayout;
        }

    }
}
